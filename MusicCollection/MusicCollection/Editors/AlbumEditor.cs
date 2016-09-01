using System;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Database;

namespace MusicCollection.Editors
{
    /// <summary>
    /// Редактор альбома
    /// </summary>
    public partial class AlbumEditor : Form
    {
        /// <summary>
        /// инициализация компонентов
        /// </summary>
        public AlbumEditor()
        {
            
            InitializeComponent();
            
        }

        /// <summary>
        /// коннект к бд
        /// </summary>
        /// <param name="database"></param>
        public AlbumEditor(IDatabase database)
            : this()
        {
        }

        private Album _album;

        /// <summary>
        /// свойство альбома для редактирования
        /// </summary>
        public Album Album
        {
            get
            {
                return _album;
            }
            set
            {
                _album = value;
                if (_album == null)
                    return;
                textEdit1.EditValue = _album.AlbumName;
                
            }
        }



         /// <summary>
         /// Проверка заполнены ли поля
         /// </summary>
         /// <returns></returns>
        private bool FieldErrorCheck()
        {
            if (string.IsNullOrWhiteSpace(string.Format("{0}", textEdit1.EditValue)))
            {
                MessageBox.Show(@"Нужно указать имя альбома!");
                return true;
            }
            
            return false;                                                                                          
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
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (FieldErrorCheck())                                                                                                                                
                return;
            if (Album == null)
                Album = new Album(string.Format("{0}", textEdit1.EditValue));
            else
            {                                                                  
                Album.AlbumName = string.Format("{0}", textEdit1.EditValue);}
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
                              