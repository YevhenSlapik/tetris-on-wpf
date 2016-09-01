using System.Data;

namespace MusicCollection.Classes
{
    /// <summary>Класс альбома </summary>
    public class Album
    {
        #region Поля альбома
        /// <summary>
        /// ID альбома
        /// </summary>
        public int AlbumId
        {
            get;
            set;
        }
        /// <summary>
        /// имя альбома
        /// </summary>
        public string AlbumName
        {
            get;
            set;
        }
       #endregion


        #region Конструкторы
        /// <summary>
        ///   Создает альбом по строке </summary>
        /// <param name="albumName"></param>
        public Album(string albumName)
        {
            AlbumName = albumName;
        }
        /// <summary>Создает альбом по данным из БД </summary>
        /// <param name="data">данные из таблицы альбомов</param>
        public Album(IDataRecord data)
        {
            AlbumId = data.GetValue<int>("albumId");
            AlbumName = data.GetValue<string>("albumTitle");
        }
        
        #endregion

        /// <summary>  Метод сравнения для класса </summary>
        /// <param name="obj">Экземпляр класса</param>
        /// <returns>Результат сравнения</returns>
        #region Equals & Hashcode
        public bool Equals(Album obj)
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
            return Equals(obj as Album);
        }

        /// <summary> Serves as a hash function for a particular type.  </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object"/>. </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return AlbumId;
        } 
        #endregion
    }
}
