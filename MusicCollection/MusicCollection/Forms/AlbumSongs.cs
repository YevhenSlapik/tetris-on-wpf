using System;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Controls;
using MusicCollection.Database;

namespace MusicCollection.Forms
{
    /// <summary>
    /// форма альбомов
    /// </summary>
    public partial class AlbumSongs : Form
    {
        /// <summary>
        /// бд
        /// </summary>
        private readonly IDatabase _database;
        /// <summary>
        /// контрол который привязывается к форме
        /// </summary>
        private SongControl _songControl;
        /// <summary>
        /// альбом с которым оперирует
        /// </summary>
        private Album _album;

        /// <summary>
        /// свойство альбома
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
                
            }
        }
        /// <summary>
        /// инициализация компонентов
        /// </summary>
        public AlbumSongs()
        {
            InitializeComponent();
        }
        /// <summary>
        /// коннект к бд, получение альбома для работы
        /// </summary>
        /// <param name="database"></param>
        /// <param name="album"></param>
        public AlbumSongs(IDatabase database,Album album)
            : this()
        {
            _database = database;
            Album = album;
            InitializeComponentEx();
           
        }

        /// <summary>
        /// Остановка Обновления таблицы песен в альбоме
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _songControl.LoadSelectedRows();
            _songControl.SetCacheFlag(false);
        }

         /// <summary>
         /// Запуск обновления таблицы песен в альбоме
         /// </summary>
         /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);               
            _songControl.SetCacheFlag(true);
        }


        /// <summary>   Инициализация дополнительных компонентов  </summary>
        private void InitializeComponentEx()
        {

            Text = _album.AlbumName;
            _songControl = new SongControl(_database,  _album)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_songControl);                                      
        }



        #region хэшкод и equals
        public bool Equals(AlbumSongs obj)
        {
            if (obj == null)
                return false;
            return GetHashCode() == obj.GetHashCode();
        }

        /// <summary> Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>. </summary>
        /// <returns> true if the specified object  is equal to the current object; otherwise, false. </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as AlbumSongs);
        }

        /// <summary> Serves as a hash function for a particular type.  </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object"/>. </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Album.GetHashCode();
        } 
        #endregion
    }                                                     
}
                                    