using System.Windows.Forms;
using MusicCollection.Controls;
using MusicCollection.Database;

namespace MusicCollection.Forms
{
    /// <summary> Основная форма </summary>
    public partial class MainForm : Form
    {
        private readonly IDatabase _database = new SqlDatabase(10000);
        //private readonly IDatabase _database = new MySqlDatabase();


        /// <summary> Контрол по управлению группами(исполинтелями)  </summary>
        private GroupsControl _groupsControl;
        /// <summary> Контрол по управлению песнями  </summary>
        private SongControl _songControl;
        /// <summary> Контрол по управлению связкой группы - песни  </summary>
        private AlbumControl _albumControl;

        /// <summary>  Конструтор гл.формы </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeComponentEx();
        }

        /// <summary>   Инициализация дополнительных компонентов (загрузка контролов и их привязка к табам)   </summary>
        private void InitializeComponentEx() 
        {
            _groupsControl = new GroupsControl(_database) //загрузка контролов
            {
                Dock = DockStyle.Fill //заполнит форму
            };

            tabPage1.Controls.Add(_groupsControl);  //привязка к табам

            _songControl = new SongControl(_database) {Dock = DockStyle.Fill};

            tabPage2.Controls.Add(_songControl);

            _albumControl = new AlbumControl(_database)
            {
                Dock = DockStyle.Fill
            };

            tabPage3.Controls.Add(_albumControl);
        }


        /// <summary>
        ///  Загрузка отмеченных строк при переключении табов + установка фокуса на контрол.
        ///  </summary>
        /// <param name="sender">TabControl</param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            var tabCtrl = sender as TabControl;
            if (tabCtrl == null)
                return;
            
            switch (tabCtrl.SelectedIndex)
            {
                case 0:  //Отмеченные Исполнители
                    _groupsControl.LoadSelectedRows();  //отмеченные строки
                    _groupsControl.Select();            //фокус
                    break;
                case 1: //Отмеченные Песни
                    _songControl.LoadSelectedRows();
                    _songControl.Select();
                    break;
                case 2: //Отмеченные группы
                    _albumControl.LoadSelectedRows();
                    _albumControl.Select();
                    break;
            }
        }
    }
}                      