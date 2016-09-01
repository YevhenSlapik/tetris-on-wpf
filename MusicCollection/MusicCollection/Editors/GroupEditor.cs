using System;
using System.Windows.Forms;
using MusicCollection.Classes;

namespace MusicCollection.Editors
{
    /// <summary>
    /// редактор группы
    /// </summary>
    public partial class GroupEditor : Form
    {
        /// <summary>
        /// к-тор инициализации компонентов
        /// </summary>
        public GroupEditor()
        {
            InitializeComponent();
        }

        
        private Group _group;

        /// <summary>
        /// Запись/чтение группы с которой работает редактор
        /// </summary>
        public Group Group
        {
            get { return _group; }
            set
            {
                _group = value;
                if (_group == null)
                    return;
                textEdit1.EditValue = _group.GroupName;
            }
        }
        /// <summary>
        ///  ОК 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (FieldErrorCheck())
                return;
            if (_group == null)
                _group = new Group(string.Format("{0}", textEdit1.EditValue));
            else
                _group.GroupName = string.Format("{0}", textEdit1.EditValue);
            DialogResult = DialogResult.OK;
            Close();
        }


        /// <summary>
        /// отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
           
        }
        /// <summary>
        /// проверка  заполенности полей
        /// </summary>
        /// <returns></returns>
        private bool FieldErrorCheck()
        {
            if (!string.IsNullOrWhiteSpace(textEdit1.Text)) return false;
            MessageBox.Show(@"Нужно указать имя группы!");
            return true;
        }
         

    }
}
