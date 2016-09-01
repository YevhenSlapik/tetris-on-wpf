using System;
using System.Data;

namespace MusicCollection.Classes
{
    /// <summary> Класс группы </summary>
    public class Group
    {

        #region Свойства
        /// <summary>
        /// ИД группы
        /// </summary>
        public int GroupId
        {
            get;
            set;
        }
        /// <summary>
        /// Имя группы
        /// </summary>
        public string GroupName
        {
            get;
            set;
        }
        /// <summary>
        /// Время последнего изменения
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        } 
        #endregion

        #region Конструкторы
        /// <summary>  Конструтор </summary>
        public Group(string groupName)
        {
            GroupName = groupName;
        }

        /// <summary>  Конструтор, который строит по данным из БД </summary>
        public Group(IDataRecord record)
        {
            GroupId = record.GetValue<int>("groupId");
            GroupName = record.GetValue<string>("groupName");
            UpdateTime = record.ContainsColumn("updateTime")
                ? record.GetValue<DateTime>("updateTime")
                : DateTime.Now;
        }

        #endregion

        #region Equals и хэшкод
        /// <summary>  Метод сравнения для класса </summary>
        /// <param name="obj">Экземпляр класса</param>
        /// <returns>Результат сравнения</returns>
        public bool Equals(Group obj)
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
            return Equals(obj as Group);
        }

        /// <summary> Serves as a hash function for a particular type.  </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object"/>. </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return GroupId;
        }

        public override string ToString()
        {
            return string.Format("{0}", GroupName);
        } 
        #endregion
    }
}
