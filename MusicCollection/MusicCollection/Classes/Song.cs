using System.Data;


namespace MusicCollection.Classes
{
    /// <summary>Класс песни </summary>
    public class Song
    {
        /// <summary>
        /// обьект группы песни
        /// </summary>
        public Group Group { get; private set; }

        #region Свойства
        /// <summary>
        /// имя песни
        /// </summary>
        public string SongName
        {
            get;
            set;
        }
        /// <summary>
        /// ид песни
        /// </summary>
        public int SongId
        {
            get;
            set;
        }

        /// <summary>
        /// ид группы песни
        /// </summary>
        public int GroupId
        {
            get { return Group.GroupId; }
        }

        /// <summary>
        /// имя группы песни
        /// </summary>
        public string GroupName
        {
            get { return Group.GroupName; }
            set { Group.GroupName = value; }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// конструктор обьекта песни  для создания новой
        /// </summary>
        public Song(Song sourceSong)
        {
            SongId = sourceSong.SongId;
            SongName = sourceSong.SongName;
            Group = sourceSong.Group;
        }

        /// <summary>
        /// конструктор песни для редактирования
        /// </summary>
        public Song(string songName, Group group)
        {
            SongName = songName;
            Group = group;
        }

        /// <summary>
        /// конструктор обьекта песни для контакта с БД
        /// </summary>
        public Song(IDataRecord record)
        {
            SongId = record.GetValue<int>("songId");
            SongName = record.GetValue<string>("songName");
            Group = new Group(record);
            
        }
        /// <summary>
        /// изменение группы песни
        /// </summary>
        /// <param name="group"></param>
        public void UpdateGroup(Group group)
        { 
            Group = group;
        }
        #endregion


        public override string ToString()
        {
            return string.Format("{0} - {1}",SongName,GroupName);
        }

        /// <summary>  Метод сравнения для класса </summary>
        /// <param name="obj">Экземпляр класса</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(Song obj)
        {
            if (obj == null)
                return false;
            return obj.GetHashCode() == GetHashCode();
        }

        /// <summary> Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>. </summary>
        /// <returns> true if the specified object  is equal to the current object; otherwise, false. </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as Song);
        }

        /// <summary> Serves as a hash function for a particular type.  </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object"/>. </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return SongId;
        }
    }
}
                                                      