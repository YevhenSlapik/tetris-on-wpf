using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Database;
using MusicCollection.Editors;
using MusicCollection.Forms;

namespace MusicCollection.Controls
{
    /// <summary>Контрол альбома</summary>
    public partial class AlbumControl : UserControl
    {
        #region Поля класса

        /// <summary> Коллекция которую использует грид для отображения </summary>
        private readonly IDictionary<int, Album> _albums = new Dictionary<int, Album>();

        private readonly IDictionary<Album, AlbumSongs> _openedForms = new Dictionary<Album, AlbumSongs>();

        //запоминание выделенной строки
        private int _focusIndex;
        // флаг обновления кэша, если true - будет обновление.необходим при фокусе на контроле - тогда false
        private bool _needCacheChange = true;
        //индексы сохраненных записей, чтобы не слетало при переключении контролов
        private int[] _focusedRows;
        /// <summary> БД </summary>
        private readonly IDatabase _database;

        #endregion

        #region Конструкторы

        /// <summary>
        /// инициализация компонентов
        /// </summary>
        protected AlbumControl()
        {
            InitializeComponent();
            //закрытие форм с песнями при закрытии формы с альбомами
            HandleDestroyed += (sender, args) =>
            {
                while (true)
                {
                    var f = _openedForms.FirstOrDefault();
                    if (f.Value != null)
                        f.Value.Close();
                    return;
                }
            };
            
                ActiveAlbumsDropDownButton.Visible = false; 

        }

        /// <summary>
        /// коннект к бд и рефреш табов </summary>
        /// <param name="database"></param>
        public AlbumControl(IDatabase database)
            : this()
        {
            _database = database;

            _database.OnCacheChanged += OnCacheChanged;

            //Загружает данные из БД и отдает их гриду для отображения
            selectStripButton_Click(toolStrip1, EventArgs.Empty);
        }

        /// <summary>
        /// обновление данных в таблице
        /// </summary>
        public void OnCacheChanged(Type type, ICollection collection)
        {

            if (!_needCacheChange)
                return;_focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            if (type != typeof (Album))
                return;
            // отрабатывание в основном потоке
            if (!gridControl1.InvokeRequired)
            {
                gridControl1.DataSource = collection;
                gridView1.RefreshData();
                gridView1.FocusedRowHandle = _focusIndex;
            }
            //отрабатывание во вторичном
            else
            {
                Invoke((MethodInvoker) (() => gridControl1.DataSource = collection));
                Invoke((MethodInvoker) (() => gridView1.RefreshData()));
                Invoke((MethodInvoker) (() => gridView1.FocusedRowHandle = _focusIndex));
            }
        }

        /// <summary>
        /// кнопка обновить
        /// </summary>
        private void selectStripButton_Click(object sender, EventArgs e)
        {
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            var s = _database.Select<Album>();
            _albums.Clear();
            s.ForEach(i => _albums.Add(i.AlbumId, i));
            UpdateSource();
            gridView1.FocusedRowHandle = _focusIndex;
        }

        #endregion

        #region Методы редактирования формы/таблицы

        /// <summary>Рефреш грида </summary>
        private void UpdateSource()
        {
            gridControl1.DataSource = _albums.Values.Select(c => c);
            gridView1.RefreshData();
            gridView1.FocusedRowHandle = _focusIndex;
        }

        /// <summary>
        /// Вывод формы с песнями альбома
        /// </summary>
        /// <param name="sender">кнопка "Песни" в меню</param>
        /// <param name="e">клик</param>
        private void songslStripButton1_Click(object sender, EventArgs e)
        {
            var f = gridView1.GetFocusedRow() as Album;
            if (f == null)
                return;
            if (_openedForms.ContainsKey(f))
            {
                _openedForms[f].Activate();       // чтобы дважды не открывать форму песен альбома - активирует уже открытую
            }
            else
            {
                var g = new AlbumSongs(_database, f);
                _openedForms.Add(f, g);
                var dbtn = new ToolStripMenuItem(g.Text,g.Icon.ToBitmap(),(o, args) => g.Activate()){   //кнопка по открытой форме песен в альбоме с
                                                                                                       //текстом, иконкой, событием на клик(делать привязанную ф-му активной)
                    Tag = f   //тег - альбом
                };
                ActiveAlbumsDropDownButton.DropDownItems.Add(dbtn);                 //добавление в кнопки в toolstripbutton
                ActiveAlbumsDropDownButton.Visible = true;                         //если есть открытые альбомы - делаем видимым
                g.FormClosed += GOnFormClosed;                                    //добавляем в коллекцию откыртых альбомов
                g.Show();                                                        //активируем        
            }

        }
         /// <summary>
         /// Закрытие списка песен в альбоме </summary>
         /// <param name="sender"></param>                          
         ///  <param name="formClosedEventArgs"></param>
        private void GOnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            var f = sender as AlbumSongs;
            if (f == null)
                return;

            var of = _openedForms.FirstOrDefault(als => Equals(als.Value, f));
            if (of.Value != null)
            {
                _openedForms.Remove(of);
                var al = ActiveAlbumsDropDownButton.DropDownItems.Cast<ToolStripMenuItem>().FirstOrDefault(a => Equals(a.Tag, of.Key)); //находим кнопку, которая связана с альбомом по тегу
                if (al == null)
                    return;
                ActiveAlbumsDropDownButton.DropDownItems.Remove(al);    //Удаление кнопки при закрытии формы песен альбома     
                ActiveAlbumsDropDownButton.Visible = ActiveAlbumsDropDownButton.DropDownItems.Count != 0; //если открытых форм нет - прячем stripbutton

            }
               
        }


        /// <summary>
        ///   Вызов редактора альбома  для добавления нового
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertStripButton_Click(object sender, EventArgs e)
        {

            var f = new AlbumEditor(_database);
            f.ShowDialog();
            if (f.DialogResult != DialogResult.OK)
                return;
            var g = _database.Insert(f.Album);
            _albums.Add(g.AlbumId, g);
            UpdateSource();
            _focusIndex = gridView1.RowCount + 1;
            gridView1.FocusedRowHandle = _focusIndex;

        }

        /// <summary>
        ///  Вызов редактора альбома  для изменения выделенного
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateToolStripButton_Click(object sender, EventArgs e)
        {
            //Выделенный на гриде альбом
            var f = gridView1.GetFocusedRow() as Album;
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            if (f == null)
                return;
            var albumEditorForm = new AlbumEditor(_database) {Album = f};     //редактор альбомов

            albumEditorForm.ShowDialog();

            if (albumEditorForm.DialogResult != DialogResult.OK)
                return;

            _albums[f.AlbumId] = _database.Update(f);     //изменение записи альбома в бд
            UpdateSource();
            gridView1.FocusedRowHandle = _focusIndex;
        }


        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            var toDelete = new Collection<Album>();
            var selected = gridView1.GetSelectedRows();
            // Нет отмеченных
            if (selected.Length == 0)
            {

                // Берем текущую
                var f = gridView1.GetFocusedRow() as Album;
                if (f == null)
                    return;
                if (
                    MessageBox.Show(
                        string.Format("Альбом '{0}' будет безвозвратно удален!{1}Вы уверены?", f.AlbumName,
                            Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                if (_openedForms.ContainsKey(f))
                {
                    _openedForms[f].Close();

                }
                toDelete.Add(f);


            }
            else
            {
                if (
                    MessageBox.Show(
                        string.Format("Отмечено альбомов '{0}'. Они будут безвозвратно удалены!{1}Вы уверены?",
                            selected.Length, Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes)
                    return;

                selected.ForEach(i =>
                {
                    var f = gridView1.GetRow(i) as Album;
                    if (f == null)
                        return;
                    toDelete.Add(f);
                    if (_openedForms.ContainsKey(f))
                    {
                        _openedForms[f].Close();
                    }
                });
            }
            _database.Delete(toDelete);
            selectStripButton_Click(sender, EventArgs.Empty);}

        #endregion

        /// <summary>
        ///  события клавиатуры </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13) // enter
            {
                updateToolStripButton_Click(sender, e);
            }

        }
        /// <summary>
        /// Восстановление обновления контрола
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlbumControl_Leave(object sender, EventArgs e)
        {
            _needCacheChange = true;
            _focusedRows = gridView1.GetSelectedRows();
        }
        /// <summary>
        ///  Приостановка обновления контрола
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlbumControl_Enter(object sender, EventArgs e)
        {
            _needCacheChange = false;
            LoadSelectedRows();
        }

        /// <summary>
        /// Загрузчик буфера отмеченных строк
        /// </summary>
        public void LoadSelectedRows()
        {
            
            if (_focusedRows != null)
                _focusedRows.ForEach(a => gridView1.SelectRow(a));
        }

    }
}
                                                    