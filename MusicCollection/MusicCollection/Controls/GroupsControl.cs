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
    /// <summary>Контрол списка исполнителей </summary>
    public partial class GroupsControl : UserControl
    {
        //БД
        private readonly IDatabase _database;
        //сохранение позиции выбранной строки при обновлении
        private int _focusIndex;
        // флаг обновления кэша, если true - будет обновление.необходим при фокусе на контроле - тогда false
        private bool _needCacheChange = true;
        //индексы сохраненных записей, чтобы не слетало при переключении контролов
        private int[] _focusedRows;
        /// <summary>
        ///  инициализация комонентов
        /// </summary>
        protected GroupsControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// коннект к бд и добавление данных в грид
        /// </summary>
        /// <param name="database"></param>
        public GroupsControl(IDatabase database) : this()
        {
            _database = database;
            
            _database.OnCacheChanged += OnCacheChanged;  //подписка на обновление кэша и данных на форме
            SelectClick(toolStrip1, EventArgs.Empty);   //обновление данных
        }
        /// <summary>
        /// обновление данных в таблице
        /// </summary>
        public void OnCacheChanged(Type type, ICollection collection)
        {
            if (!_needCacheChange) //проверка на необходимость обновления кэша. если контрол активен - не надо
                return;
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();     // сохранение выделенной записи при обновлении данных
            if (type != typeof(Group))
                return;
            // отрабатывание в основном потоке
            if (!gridControl1.InvokeRequired)
            {
                gridControl1.DataSource = collection;//коллекция из кэша
                gridView1.RefreshData();
                gridView1.FocusedRowHandle = _focusIndex;//курсор
            }
            // отрабатывание во вторичном
            else
            {
                Invoke((MethodInvoker)(() => gridControl1.DataSource = collection));
                Invoke((MethodInvoker)(() => gridView1.RefreshData()));
                Invoke((MethodInvoker)(() => gridView1.FocusedRowHandle = _focusIndex));
            }
        }                    
        /// <summary>Обновляет таблицу</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectClick(object sender, EventArgs e)
        {
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            gridControl1.DataSource = _database.Select<Group>();
            gridView1.RefreshData();
            gridView1.FocusedRowHandle = _focusIndex;
            }

        /// <summary>Изменение записи</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClick(object sender, EventArgs e)
        {                                                           
            var f = gridView1.GetFocusedRow() as Group;
            _focusIndex = gridView1.GetFocusedDataSourceRowIndex();
            if (f == null )
                return;
            var groupEditorForm = new GroupEditor {Group = f};
            groupEditorForm.ShowDialog();
            if (groupEditorForm.DialogResult != DialogResult.OK)
                return;
            _database.Update(groupEditorForm.Group);
            gridView1.FocusedRowHandle = _focusIndex;
        }

        /// <summary>Добавление записи</summary>     
        ///  <param name="sender"></param>
        /// <param name="e"></param>                                                
        private void InsertClick(object sender, EventArgs e)
        {
            var groupEditorForm = new GroupEditor();
            groupEditorForm.ShowDialog();
            if (groupEditorForm.DialogResult != DialogResult.OK) 
                return;
            _database.Insert(groupEditorForm.Group);
            gridView1.FocusedRowHandle = gridView1.RowCount-1;
            SelectClick(sender, EventArgs.Empty);
        }
        /// <summary>
        /// Удаление записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {

            var toDelete = new Collection<Group>();
            var selected = gridView1.GetSelectedRows();
            // Нет отмеченных
            if (selected.Length == 0)
            {
                // Берем текущую
                var f = gridView1.GetFocusedRow() as Group;
                if (f == null)
                    return;
                if (
                    MessageBox.Show(
                        string.Format("Группа '{0}' будет безвозвратно удалена!{1}Вы уверены?", f.GroupName,
                            Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                toDelete.Add(f);
            }
            else
            {
                if (
                    MessageBox.Show(
                        string.Format("Отмечено групп '{0}'. Они будут безвозвратно удалены!{1}Вы уверены?",
                            selected.Length, Environment.NewLine), @"Удаление", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes)
                    return;

                selected.ForEach(i =>
                {
                    var f = gridView1.GetRow(i) as Group;
                    if (f == null)
                        return;
                    toDelete.Add(f);
                });
            }
            _database.Delete(toDelete);
            SelectClick(sender, EventArgs.Empty);
        }


        /// <summary>                                         
        /// события клавиатуры </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // enter
            {
                UpdateClick(sender, e);
            }
        }
        #region GroupControl Focus/Unfocus
        /// <summary> Фокус на контроле. Обновлять кэш не надо</summary>
        private void GroupsControl_Enter(object sender, EventArgs e)
        {
            _needCacheChange = false;
            LoadSelectedRows();
        }

        /// <summary> Фокус на контроле теряется. Обновлять кэш.</summary>
        private void GroupsControl_Leave(object sender, EventArgs e)
        {
            _needCacheChange = true;
            _focusedRows = gridView1.GetSelectedRows();
        } 
        #endregion

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
                                                                           