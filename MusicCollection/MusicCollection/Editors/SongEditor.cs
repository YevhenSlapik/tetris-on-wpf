using System;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Database;

namespace MusicCollection.Editors
{
    /// <summary>
    /// Форма редактора таблицы песен (добавление/изменение записей ) 
    /// </summary>
    public partial class SongEditor : Form
    {
        /// <summary>
        /// инициализация компонентов
        /// </summary>
        protected SongEditor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// к-тор запрашивает у бд таблицу групп для возможного изменения группы песни
        /// </summary>
        /// <param name="database">коннект</param>
        public SongEditor(IDatabase database):this()
        {
            
            _database = database;
            lookUpEdit1.Properties.DataSource = _database.Select<Group>(true);
        }
        /// <summary>
        /// песня для редактирования или добавления
        /// </summary>
        private Song _song;
        private readonly IDatabase _database;
        /// <summary>
        /// Песня с которой форма работает.
        /// </summary>
        public  Song Song
        {
            get
            {
                return _song;
            }
            set
            {
                _song = value;
                if (_song == null)
                    return;
                textEdit1.EditValue = _song.SongName;
                lookUpEdit1.EditValue = _song.GroupId;
            }
        } 
        /// <summary>
        /// ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
             if(FieldErrorCheck())
                 return;
            var gr = lookUpEdit1.GetSelectedDataRow() as Group;

            if (gr==null)
            {
                return;
            }
            if (_song==null)
            {
                _song = new Song(string.Format("{0}", textEdit1.EditValue), gr);   
            }
            else
            {
                _song.UpdateGroup(gr);                             
                _song.SongName = textEdit1.EditValue.ToString();
            
            }
            
            DialogResult = DialogResult.OK;
            Close();


        }
        /// <summary>
        /// Проверка полей формы на заполненность
        /// </summary>
        /// <returns></returns>
        private bool FieldErrorCheck()
        {                                                                         
            if (string.IsNullOrWhiteSpace(string.Format("{0}", textEdit1.EditValue)))
            {
                MessageBox.Show(@"Нужно указать имя песни!");
                return true;
            }
            if (!string.IsNullOrWhiteSpace(string.Format("{0}", lookUpEdit1.EditValue))) return false;
            MessageBox.Show(@"Группа не должна быть пустой!");
            return true;
        }

        /// <summary>
        /// отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        /// <summary>
        /// добавить группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var f = new GroupEditor();
            if(f.ShowDialog()!=DialogResult.OK)return;
            var g = _database.Insert(f.Group);
            lookUpEdit1.Properties.DataSource = _database.Select<Group>(true);
            lookUpEdit1.EditValue = g.GroupId;
        }                                          
    }
}
