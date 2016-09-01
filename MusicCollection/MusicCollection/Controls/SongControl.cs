using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Database;
using MusicCollection.Editors;

namespace MusicCollection.Controls
{
    /// <summary>Контрол для песен </summary>
    public partial class SongControl : UserControl
    {
        //БД
        private readonly IDatabase _database;
        //альбом песни - нужен для редактирования песен в альбоме
        private readonly Album _album;
         /// <summary>
         /// сохраненный индекс для фокуса на гриде
         /// </summary>
        private  int _focusIndex;
        // флаг обновления кэша, если true - будет обновление.необходим при фокусе на контроле - тогда false
        private bool _needCacheChange = true;
        //индексы сохраненных записей, чтобы не слетало при переключении контролов
        private int[] _focusedRows;
        /// <summary>
        /// инициализация компонентов
        /// </summary>
        protected SongControl()
        {
            InitializeComponent();
        }

         /// <summary>
         /// конструктор контрола для коннекта с бд, получения оттуда данных и их  связь с гридом
         /// </summary>                                       
         ///  <param name="database">бд</param>
         /// <param name="album">конкретный альбом.нужен для вызова таблицы песен в альбоме. если null - вызов вне таблицы альбома</param>
        public SongControl(IDatabase database, Album album = null)
            : this()
        {
            _database = database;                                      
            _album = album;
            _database.OnCacheChanged += OnCacheChanged;
            updateToolStripButton.Visible = _album == null;
            selectStripButton_Click(selectStripButton, EventArgs.Empty);
        }                                                      
        /// <summary>
        /// обновление данных в таблице
        /// </summary>
        public void OnCacheChanged(Type type, ICollection collection)
        {
            
            if (!_needCacheChange) //проверка на необходимость обновления кэша. если контрол активен - обновлять не надо
                return;
                
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            
            // если есть альбом 
            if (_album != null && type == typeof (AlbumSong))
            {
                // отрабатывание в основном потоке
                if (!gridControl1.InvokeRequired)
                {
                    gridControl1.DataSource = collection.OfType<AlbumSong>().Where(als => als.AlbumId == _album.AlbumId);
                    gridView1.RefreshData();
                    gridView1.FocusedRowHandle = _focusIndex;
                }
                //отрабатывание во вторичном
                else
                {
                    Invoke((MethodInvoker) (() => gridControl1.DataSource = collection.OfType<AlbumSong>().Where(als => als.AlbumId == _album.AlbumId)));
                    Invoke((MethodInvoker)(() => gridView1.RefreshData()));
                    Invoke((MethodInvoker)(() => gridView1.FocusedRowHandle = _focusIndex));

                }
            }
             
            //если нет альбома
            if(_album == null && type == typeof (Song))
            {
                // отрабатывание основном потоке
                if (!gridControl1.InvokeRequired)
                {
                    gridControl1.DataSource = collection;
                    gridView1.RefreshData();
                    gridView1.FocusedRowHandle = _focusIndex;
                }
                //отрабатывание во вторичном
                else
                {
                    Invoke((MethodInvoker)(() => gridControl1.DataSource = collection));
                    Invoke((MethodInvoker)(() => gridView1.RefreshData()));
                    Invoke((MethodInvoker)(() => gridView1.FocusedRowHandle = _focusIndex));

                }
            }}

         /// <summary>
         /// обновление таблицы
         /// </summary>
         /// <param name="sender"></param>          
         ///  <param name="e"></param>
        private void selectStripButton_Click(object sender, EventArgs e)
        {
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            if (_album != null)
                gridControl1.DataSource = _database.Select<AlbumSong>().Where(als=>als.AlbumId==_album.AlbumId);
            else
                gridControl1.DataSource = _database.Select<Song>();
            gridView1.RefreshData();
            gridView1.FocusedRowHandle = _focusIndex;
            }
        /// <summary>
        /// добавление в таблицу
        /// </summary> 
        private void insertStripButton_Click(object sender, EventArgs e)
        {

            // альбома нет - добавляется просто в таблицу песен
            if (_album == null)
            {
                var f = new SongEditor(_database);
                f.ShowDialog();
                if (f.DialogResult != DialogResult.OK)
                    return;
                _database.Insert(f.Song);
           
            }
            // альбом есть - добавляется в таблицу песен и в таблицу альбомы-песни
            else
            {
                var d = new AlbumSongEditor(_database, _album);
                d.ShowDialog();
                if (d.DialogResult == DialogResult.OK)
                {
                    var s = d.Song;
                    _database.Insert(new AlbumSong(s, _album));
                }

            }
            selectStripButton_Click(sender, EventArgs.Empty);
        }

        /// <summary>
        /// изменение записи в таблице
        /// </summary>
        private void updateToolStripButton_Click(object sender, EventArgs e)
        {
            var f = gridView1.GetFocusedRow() as Song;
             _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            if (f == null)
                return;
            var songEditorForm = new SongEditor(_database) {Song = f};
            songEditorForm.ShowDialog();
            if (songEditorForm.DialogResult != DialogResult.OK)
                return;

            _database.Update(songEditorForm.Song);
            gridView1.FocusedRowHandle = _focusIndex; 
                 
        }
          /// <summary>
          /// удаление записи из таблицы                      
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            
            //если альбом есть - удаление по albumsong - то есть из таблицы песен и таблицы альбомов-песен
            //формируя коллекцию к удалению
            if (_album != null)
            {
                var toDelete = new Collection<AlbumSong>();
                var selected = gridView1.GetSelectedRows();
                // Нет отмеченных
                if (selected.Length == 0)
                {

                    // Берем текущую
                    var f = gridView1.GetFocusedRow() as AlbumSong;
                    if (f == null)
                        return;
                    if (
                        MessageBox.Show(
                            string.Format("Песня '{0}' будет безвозвратно удалена!{1}Вы уверены?", f.SongName,
                                Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                    toDelete.Add(f);

                }
                else
                {
                    if (
                        MessageBox.Show(
                            string.Format("Отмечено песен '{0}'. Они будут безвозвратно удалены!{1}Вы уверены?",
                                selected.Length, Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) !=
                        DialogResult.Yes)
                        return;

                    selected.ForEach(i =>
                    {
                        var f = gridView1.GetRow(i) as AlbumSong;
                        if (f == null)
                            return;
                        toDelete.Add(f);
                    });
                }
                _database.Delete(toDelete);
                
            }
            //контрол вне альбома - удаление только песен. аналогично через формирование коллекции
            else
            {
                var toDelete = new Collection<Song>();
                var selected = gridView1.GetSelectedRows();
                // Нет отмеченных
                if (selected.Length == 0)
                {

                    // Берем текущую
                    var f = gridView1.GetFocusedRow() as Song;
                    if (f == null)
                        return;
                    if (
                        MessageBox.Show(
                            string.Format("Песня '{0}' будет безвозвратно удалена!{1}Вы уверены?", f.SongName,
                                Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                    toDelete.Add(f);

                }
                else
                {
                    if (
                        MessageBox.Show(
                            string.Format("Отмечено песен '{0}'. Они будут безвозвратно удалены!{1}Вы уверены?",
                                selected.Length, Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) !=
                        DialogResult.Yes)
                        return;

                    selected.ForEach(i =>
                    {
                        var f = gridView1.GetRow(i) as Song;
                        if (f == null)
                            return;
                        toDelete.Add(f);
                    });
                }
                _database.Delete(toDelete);
            }
            selectStripButton_Click(sender, EventArgs.Empty);}

        /// <summary>
        ///  события клавиатуры </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // enter
            {
                updateToolStripButton_Click(sender, e);
            }
        }
         /// <summary>
         ///  Приостановка обновления контрола
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void SongControl_Enter(object sender, EventArgs e)
        {
            LoadSelectedRows();
            _needCacheChange = false;
        }
        /// <summary>
        /// Восстановление обновления контрола
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SongControl_Leave(object sender, EventArgs e)
        {
            _needCacheChange = true;
            _focusedRows = gridView1.GetSelectedRows();
        }
        /// <summary>
        /// Загрузчик буфера отмеченніх строк
        /// </summary>
        public void LoadSelectedRows()
        {
            
            if (_focusedRows != null)
                _focusedRows.ForEach(a => gridView1.SelectRow(a)); 
        }
        /// <summary>
        /// Уставновщик флага обновления данных в таблице
        /// </summary>
        /// <param name="set"></param>
        public void SetCacheFlag(bool set)
        {
            _needCacheChange = set;
        }
    }                          
}                                                                                