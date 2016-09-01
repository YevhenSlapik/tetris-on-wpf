using System.Data;

namespace MusicCollection.Classes
{
    
    /// <summary>Класс, который связывает конкретную песню с альбомом </summary>
    public class AlbumSong
    {
        #region приватные поля

        
         /// <summary>
         /// обьект для доступа к песне альбома
         /// </summary>
        public Song Song { get; set; }
        /// <summary>
        /// обьект альбома
        /// </summary>
        public Album Album { get; private set; }

        #endregion

        #region Свойства 

        /// <summary>
        /// ИД песни
        /// </summary>
        public int SongId
        {
            get { return Song.SongId; }
        }
        /// <summary>
        /// ИД группы песни
        /// </summary>
        public int GroupId
        {
            get { return Song.GroupId; }
        }

        /// <summary>
        /// ИД альбома
        /// </summary>
        public int AlbumId
        {
            get { return Album.AlbumId; }
        }
        /// <summary>
        /// имя песни
        /// </summary>
        public string SongName
        {
            get
            {
                return Song.SongName;
            }
        }
        /// <summary>
        /// имя группы песни
        /// </summary>
        public string GroupName
        {
            get
            {
                return Song.GroupName;
            }
        }

        #endregion

        #region Конструкторы
        /// <summary>  Конструтор который строит альбом с песнями по таблице из БД </summary>
        public AlbumSong(IDataRecord record)
        {
            Song=new Song(record);
            Album = new Album(record);
        }

        /// <summary>
        /// Конструктор который создает альбом
        /// </summary>
        public AlbumSong(Song song, Album album)
        {
            Song = song;
            Album = album;
        } 
        
        #endregion

        #region Equals и хэшкод
        /// <summary>  Метод сравнения для класса </summary>
        /// <param name="obj">Экземпляр класса</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(AlbumSong obj)
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
            return Equals(obj as AlbumSong);
        }

        /// <summary> Serves as a hash function for a particular type.  </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object"/>. </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return SongId.GetHashCode() ^ AlbumId.GetHashCode();
        } 
        #endregion

        
    }
}
