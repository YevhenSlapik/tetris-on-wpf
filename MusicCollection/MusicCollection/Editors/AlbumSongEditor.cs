using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using MusicCollection.Classes;
using MusicCollection.Database;

namespace MusicCollection.Editors
{
    /// <summary>
    /// Форма для редактирования песен в конкретном альбоме
    /// </summary>
    public partial class AlbumSongEditor : Form
    {
        #region Конструкторы
        /// <summary>
        /// инициализация компонентов
        /// </summary>
        protected AlbumSongEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// коннект к бд, закугрзка данных в грид
        /// </summary>
        /// <param name="database"></param>
        /// <param name="album"></param>
        public AlbumSongEditor(IDatabase database, Album album)
            : this()
        {

            _database = database;
            _nonAlbumSongs = _database.Select<Song>(true);
            var albumSongs = _database.Select<AlbumSong>(true).Where(al=>al.AlbumId==album.AlbumId);
            albumSongs.ForEach(als => _nonAlbumSongs.Remove(als.Song));
            lookUpEdit1.Properties.DataSource = _nonAlbumSongs.Select(s => s);

            Text = string.Format("Добавить в  \"{0}\"", album.AlbumName);
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        
                                                           
        //бд
        private readonly IDatabase _database;                    
        // песни которых нет в альбоме
        private readonly ICollection<Song> _nonAlbumSongs = new Collection<Song>();
        //песня для редактирования
        private Song _song;

        /// <summary>
        /// песня для редактирования
        /// </summary>
        public Song Song 
        {                                                                                                                   
            get { return _song; }
            set
            {    _song = value;
                if (_song == null)
                    return;
                lookUpEdit1.EditValue = _song;
            }
        }
        /// <summary>
        /// Добавить песню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var f = new SongEditor(_database);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            _song = _database.Insert(f.Song);
            _nonAlbumSongs.Add(_song);
            lookUpEdit1.Properties.DataSource = _nonAlbumSongs.Select(s => s);
            lookUpEdit1.EditValue = _song.SongId;
        }
        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();                                                                
        }
        /// <summary>
        /// ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var s = lookUpEdit1.GetSelectedDataRow() as Song;
            if (s==null)
            {
                return;
            }
            _song = new Song(s);
            DialogResult = DialogResult.OK;
            Close();
        }

        
                                                      
    }                       
}
