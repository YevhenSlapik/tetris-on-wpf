using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Timers;
using MusicCollection.Classes;

namespace MusicCollection.Database
{
    /// <summary> Класс, который координирует работу с БД (Подключение к бд, удаление, вставка, изменение и выборка записей), реализуя интерфейс IDatabase </summary>
    public class SqlDatabase : IDatabase
    {
        #region Приватные поля делегата,адреса коннекта,списка типов
        /// <summary> Делегат события изменения кэша</summary>
        private Action<Type, ICollection> _cacheChanged;

        /// <summary>Адрес коннекта </summary>
        private const string ConnectionString = @"Data Source=s-kv-center-x62;Initial Catalog=modSADevel;User ID=j-SAEntries;Password=JdctHI8DvFp79r2TCTVo";

        /// <summary> Список типов </summary>
        private readonly IList<Type> _enabledTypes = new List<Type>
        {
            typeof (Group),
            typeof (Album),
            typeof (Song),
            typeof (AlbumSong)
        }; 
        #endregion

        #region КЭШИ ТАБЛИЦ
        /// <summary>
        /// кэш группы.ключ - ид группы
        /// </summary>
        private readonly IDictionary<int, Group> _groups = new Dictionary<int, Group>();
        /// <summary>
        /// кэш песни.ключ - ид песни
        /// </summary>
        private readonly IDictionary<int, Song> _songs = new Dictionary<int, Song>();
        /// <summary>
        /// кэш альбома. ключ - ид альбома
        /// </summary>
        private readonly IDictionary<int, Album> _albums = new Dictionary<int, Album>();
        /// <summary>
        /// кэш песен в альбоме. листом потому что в обьектах,  ид песни и альбомов могут совпадать
        /// </summary>
        private readonly IList<AlbumSong> _albumSongs = new List<AlbumSong>(); 
        #endregion
 
        #region ТАЙМЕР
        /// <summary>
        ///   тамер для рефреша данных с бд в таблицах с определенным интервалом
        /// </summary>
        private readonly Timer _timer;
        /// <summary>
        /// идет таймер или нет
        /// </summary>
        private bool _isTimerRunning;
        /// <summary>
        /// интервал таймера
        /// </summary>
        private readonly double _interval; 
        

        
        /// <summary>
        ///  к-тор бд с таймером
        /// </summary>
        /// <param name="interval">интервал таймера</param>
        public SqlDatabase(double interval = 30000)
        {
            _interval = interval;
            _timer = new Timer(_interval <= 0 ? 30000 : _interval); //если меньше нуля - 30 секунд
            _isTimerRunning = false;

            _timer.Elapsed += (sender, args) =>              // при 30 сек - обновление всех кэшей
            {
                _timer.Enabled = false;

                UpdateCache<Group>();
                UpdateCache<Song>();
                UpdateCache<Album>();
                UpdateCache<AlbumSong>();
                
                _timer.Enabled = true;
                 
                //обновление данных на котролах-подписантах
                SendCacheCanged<Group>();
                SendCacheCanged<Song>();
                SendCacheCanged<Album>();
                SendCacheCanged<AlbumSong>();

            };
        } 
        #endregion

        #region Реализация IDatabase и методов работы с кэшами (CreateConnection,Select,Insert,Update,Delete)
        /// <summary> Получение конекта к базе </summary>
        /// <returns>Конект</returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>  Метод выбора данных </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="useCache">Давать данные из кеша</param>
        /// <returns>Коллекция выбранных записей</returns>
        public ICollection<T> Select<T>(bool useCache = false)
        {
            var type = CheckType<T>(); //проверка типа
            if (!useCache || IsCacheEmpty(type))   //если использование кэша не указано (или он пустой) - обновляем кэш и оттуда возвращаем данные 
                return SelectFromBase<T>();
            return SelectFromCache<T>();
        }

        /// <summary> Проверка не пустой ли кеш </summary>
        /// <param name="type">Проверяемый тип</param>
        /// <returns>Пустой не пустой</returns>
        private bool IsCacheEmpty(Type type)
        {
            if (type == typeof (Group))
                return _groups.Values.Count == 0;
            if (type == typeof (Song))
                return _songs.Values.Count == 0;
            if (type == typeof (Album))
                return _albums.Values.Count == 0;
            return _albumSongs.Count == 0;
        }

        /// <summary>  Метод выбора данных из базы</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Коллекция выбранных записей</returns>
        private ICollection<T> SelectFromBase<T>()
        {
            UpdateCache<T>();
            var ret = SelectFromCache<T>();

            if (_isTimerRunning || _interval<=0) 
                return ret;

            _timer.Start();
            _isTimerRunning = true;

            return ret;
        }

        /// <summary> Обновляет данные кэша </summary>
        /// <typeparam name="T">тип кэша</typeparam>
        private void UpdateCache<T>()
        {
            var type = CheckType<T>();    // Проверка типа

            using (var connection = CreateConnection())    // ресурс коннекта
            {
                using (connection.StateManager())  // ресурс открытия/закрытия коннекта
                {
                    string selectCommand;  //переменная строки запроса
                    if (type == typeof(Group))   //селект для групп      
                    {
                        _groups.Clear();      //очистка кэша
                        selectCommand = "select * from [Groups.Test]";
                        connection.ReadToCollection(_groups, rec =>          //перезагрузка данных в кэш
                        {                                                             
                            var gr = new Group(rec);
                            return new KeyValuePair<int, Group>(gr.GroupId, gr);
                        }, selectCommand);
                    }

                    if (type == typeof(Song))  //селект для групп  (данные загружаются так же как для групп) 
                    {
                        _songs.Clear();       
                        
                        selectCommand = @"select                             
                                             songId=s.songId
                                            ,groupId=s.groupId
                                            ,songName=s.songName
                                            ,groupName=g.groupName 
                                          from 
                                                 [Songs.Test] s 
                                            join [Groups.Test] g on s.groupId = g.groupId";
                        connection.ReadToCollection(_songs, rec =>     
                        {
                            var sn = new Song(rec);
                            return new KeyValuePair<int, Song>(sn.SongId, sn);
                        }, selectCommand);
                    }

                    if (type == typeof(Album))    //селект для альбомов  (данные загружаются так же как для групп) 
                    {
                        _albums.Clear();
                        selectCommand = string.Format("select * from [Albums.Test]");
                        connection.ReadToCollection(_albums, rec =>
                        {
                            var al = new Album(rec);
                            return new KeyValuePair<int, Album>(al.AlbumId, al);
                        }, selectCommand);
                    }

                    if (type == typeof(AlbumSong))  //селект для песен в альбоме   (данные загружаются так же как для групп) 
                    {
                        _albumSongs.Clear();
                        selectCommand =
                            @"SELECT songId=s.songId
                                            ,groupId=s.groupId
                                            ,songName=s.songName
                                            ,groupName=g.groupName
                                            ,albumId=als.albumId
                                            ,albumTitle = a.albumTitle 
                                            from [AlbumSongs.Test] as als 
                                            join [Songs.Test] as s on als.songId = s.songId 
                                            join [Groups.Test] as g on s.groupId = g.groupId
                                            join [Albums.Test] as a on als.albumId = a.albumId";
                        connection.ReadToCollection(_albumSongs, rec => new AlbumSong(rec), selectCommand);
                    }
                }
            }
        }

        /// <summary>  Метод выбора данных из кэша</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Коллекция выбранных записей</returns>
        private ICollection<T> SelectFromCache<T>()
        {
            var type = CheckType<T>();

            if (type == typeof(Group))
                return _groups.Values.Select(g => g.ConvertTo<T>()).ToList();
            if (type == typeof(Song))
                return _songs.Values.Select(g => g.ConvertTo<T>()).ToList();
            if (type == typeof(Album))
                return _albums.Values.Select(g => g.ConvertTo<T>()).ToList();

//            if (type == typeof(AlbumSong))
                return _albumSongs.Select(g => g.ConvertTo<T>()).ToList();
        }

        /// <summary>Удаление записей из таблиц </summary>
        /// <typeparam name="T">тип таблицы</typeparam>
        /// <param name="toDelete"></param>
        public void Delete<T>(ICollection<T> toDelete)
        {
            var type = CheckType<T>();      //проверка типа
            if (toDelete == null || toDelete.Count == 0)   //если нет переданных на удаление записей в коллекцию - выход
                return;

            using (var connection = CreateConnection())
            {
                using (connection.StateManager())
                {
                    string deleteCommand;  // сформированный запрос
                    string whereCondition;  //что и где удалять в бд
                    if (type == typeof(Group))    //удаление групп. Если удаляется группа, то удаляются у песни. Потому стирается запись группы и все связанные с ней песни
                    {
                        whereCondition = string.Format("where g.groupId in({0})", toDelete.Aggregate(string.Empty, (prevWhere, toDeleteElement) => string.Format(prevWhere.Length == 0 ? "{0}{1}" : "{0},{1}", prevWhere, toDeleteElement.ConvertTo<Group>().GroupId)));
                        deleteCommand = string.Format(
                            @"delete als
                              from [AlbumSongs.Test] als 
                               join [Songs.Test] s on s.songId=als.songId
                               join [Groups.Test] g on g.groupId=s.groupId {0};

                              delete s
                              from [Songs.Test] s 
                               join [Groups.Test] g on g.groupId=s.groupId {0};


                              delete g from [Groups.Test] g {0};", whereCondition);

                        connection.ExecuteNonQuery(deleteCommand);
                        // очистка кэшей от записи
                        toDelete.ForEach(toDeleteElement =>                 
                        {
                            var groupId = toDeleteElement.ConvertTo<Group>().GroupId;
                            // Удаление из связки альбомы-песни 
                            for (int i = 0; i < _albumSongs.Count; i++)
                            {
                                if (_albumSongs[i].GroupId == groupId)
                                {
                                    _albumSongs.RemoveAt(i);
                                    i--;
                                }

                            }
                            // Удаление из песен

                            while (true)
                            {
                                var song = _songs.FirstOrDefault(a => a.Value.GroupId == groupId);
                                if (song.Value==null)
                                    break;
                                _songs.Remove(song.Key);
                            } 
                            // Удаление из групп
                            _groups.Remove(groupId);

                        });
                        //обновление данных на котроле-подписанте на событие
                        SendCacheCanged<AlbumSong>();  
                        SendCacheCanged<Song>();
                    }

                    if (type == typeof(Song))
                    {
                        whereCondition = string.Format("where songId in({0})", toDelete.Aggregate(string.Empty, (prevWhere, toDeleteElement) => string.Format(prevWhere.Length == 0 ? "{0}{1}" : "{0},{1}", prevWhere, toDeleteElement.ConvertTo<Song>().SongId)));
                        deleteCommand = string.Format(@"delete from [AlbumSongs.Test] {0}; delete from [Songs.Test] {0} ;", whereCondition);

                        connection.ExecuteNonQuery(deleteCommand);
                        toDelete.ForEach(toDeleteElement =>
                        {
                            var songId = toDeleteElement.ConvertTo<Song>().SongId;
                            _songs.Remove(songId);                        // Удаление из песен
                            
                            for (int i = 0; i < _albumSongs.Count; i++)
                            {
                                if (_albumSongs[i].SongId == songId)       // Удаление из связки альбомы-песни 
                                {
                                    _albumSongs.RemoveAt(i);
                                    i--;
                                }
                                
                            }

                        });
                        SendCacheCanged<AlbumSong>();
                    }

                    if (type == typeof(Album)) //удаление альбомов.
                    {
                        whereCondition = string.Format("where albumId in({0})", toDelete.Aggregate(string.Empty, (prevWhere, toDeleteElement) => string.Format(prevWhere.Length == 0 ? "{0}{1}" : "{0},{1}", prevWhere, toDeleteElement.ConvertTo<Album>().AlbumId)));
                        deleteCommand = string.Format(@"delete from [AlbumSongs.Test] {0}; delete from [Albums.Test] {0} ;", whereCondition);

                        connection.ExecuteNonQuery(deleteCommand);
                        toDelete.ForEach(toDeleteElement =>
                        {
                            var albumId = toDeleteElement.ConvertTo<Album>().AlbumId;
                            _albums.Remove(albumId);

                            for (int i = 0; i < _albumSongs.Count; i++)
                            {
                                if (_albumSongs[i].AlbumId == albumId)
                                {
                                    _albumSongs.RemoveAt(i);
                                    i--;
                                }

                            }

                        });
                        SendCacheCanged<AlbumSong>();
                    }

                    if (type == typeof (AlbumSong))  //удаление песен из альбомов.
                    {
                        whereCondition = string.Format("where albumId = ? and songId in({0})", toDelete.Aggregate(string.Empty, (prevWhere, toDeleteElement) => string.Format(prevWhere.Length == 0 ? "{0}{1}" : "{0},{1}", prevWhere, toDeleteElement.ConvertTo<AlbumSong>().SongId)));
                        deleteCommand = string.Format(@"delete from [AlbumSongs.Test] {0};", whereCondition);
                        var albumSong = toDelete.FirstOrDefault().ConvertTo<AlbumSong>();

                        connection.ExecuteNonQuery(deleteCommand,albumSong.AlbumId);
                        toDelete.ForEach(toDeleteElement =>
                        {
                            _albumSongs.Remove(toDeleteElement.ConvertTo<AlbumSong>());

                        });
                    }
                }
                SendCacheCanged<T>();
            }
        }

        /// <summary>Изменение записи в таблице(ах) </summary>
        /// <typeparam name="T">тип который вызвал запрос</typeparam>
        /// <param name="source">сама запись</param>
        /// <returns></returns>
        public T Update<T>(T source)
        {
            var type = CheckType<T>();    // проверка типа

            using (var connection = CreateConnection())  //ресурс коннекта
            {
                var ret = new Collection<T>();

                using (connection.StateManager())    //ресурс открытия/закрытия коннекта
                {
                    string updateCommand;     //переменная строки запроса
                    if (type == typeof(Group))  // изменение группы
                    {
                        var updateCollection = new Collection<Group>();
                        var grEntry = source as Group;
                        //сформированный запрос
                        updateCommand = string.Format(@"update [Groups.Test] 
                                                        set groupName=?, 
                                                        updateTime=getdate() 
                                                        where groupId=?;
                                                        select * from [Groups.Test] 
                                                        where groupId=?");
                        if (grEntry != null)
                        {
                            connection.ReadToCollection(updateCollection, rec => new Group(rec), updateCommand, grEntry.GroupName, grEntry.GroupId, grEntry.GroupId);
                            var updatedGroup = updateCollection.FirstOrDefault();
                            if (updatedGroup != null)
                            {
                                //обновление данных на котроле-подписанте на событие и записи в кэше
                                _groups[grEntry.GroupId] = updatedGroup;
                                SendCacheCanged<T>();
                                _songs.Where(a => a.Value.GroupId == grEntry.GroupId).ForEach(a => a.Value.UpdateGroup(updatedGroup));
                                SendCacheCanged<Song>();
                                _albumSongs.Where(a => a.GroupId == grEntry.GroupId).ForEach(a => a.Song.UpdateGroup(grEntry));
                                SendCacheCanged<AlbumSong>();

                            }
                            return updatedGroup.ConvertTo<T>();
                        }
                    }

                    if (type == typeof(Song))  // изменение песни
                    {
                        var updateCollection = new Collection<Song>();
                        var grEntry = source as Song;
                        updateCommand =               @"update [Songs.Test]
                                                        set songName=?,
                                                        groupId=? 
                                                        where songId=?;

                                                        select songId=s.songId,
                                                        groupId=s.groupId,
                                                        songName=s.songName,
                                                        groupName=g.groupName 
                                                        from [Songs.Test] as s 
                                                        join [Groups.Test] as g on s.groupId = g.groupId 
                                                        where s.songId=?";
                        if (grEntry != null)
                        {
                            connection.ReadToCollection(updateCollection, rec => new Song(rec), updateCommand, grEntry.SongName, grEntry.GroupId, grEntry.SongId, grEntry.SongId);
                            var updatedSong = updateCollection.FirstOrDefault();
                            if (updatedSong != null)
                            {
                                _songs[grEntry.SongId] = updatedSong;
                                SendCacheCanged<T>();
                                _albumSongs.Where(a=>a.SongId==updatedSong.SongId).ForEach(a=>a.Song=updatedSong);
                                SendCacheCanged<AlbumSong>();

                            }

                            return updatedSong.ConvertTo<T>();
                        }
                    }
                    if (type == typeof(Album))  // изменение альбома
                    {
                        var updateCollection = new Collection<Album>();
                        var grEntry = source as Album;
                        updateCommand = string.Format(@"update [Albums.Test]
                                                        set albumTitle=?
                                                        where albumId=?;                          

                                                        select * from [Albums.Test]
                                                        where albumId=?");
                        if (grEntry != null)
                        {
                            connection.ReadToCollection(updateCollection, rec => new Album(rec), updateCommand, grEntry.AlbumName, grEntry.AlbumId, grEntry.AlbumId);
                            var updatedAlbum = updateCollection.FirstOrDefault();
                            if (updatedAlbum != null)
                            {
                                _albums[grEntry.AlbumId] = updatedAlbum;
                                SendCacheCanged<T>();
                            }

                            return updatedAlbum.ConvertTo<T>();
                        }
                    }

                }

                
                return ret.FirstOrDefault();  //возврат измененной записи
            }
        }
        /// <summary>Добавление записи в таблицу(ы) </summary>
        /// <typeparam name="T">тип который вызвал запрос</typeparam>
        /// <param name="source">запись на добавление</param>
        /// <returns></returns>
        public T Insert<T>(T source)
        {
            var type = CheckType<T>(); //проверка типа
            using (var connection = CreateConnection())   //ресурс коннекта
            {
                using (connection.StateManager())     //ресурс открытия/закрытия коннекта
                {
                    string updateCommand;       //переменная строки запроса
                    if (type == typeof(Group))   //добавление новой группы
                    {
                        var insertCollection = new Collection<Group>();
                        var grEntry = source as Group;
                        updateCommand = @"declare @id int;
                                                    insert [Groups.Test] (groupName)
                                                    values (?);
                                                    select @id=SCOPE_IDENTITY();

                                                    select * from [Groups.Test] 
                                                    where groupId=@id;";
                        if (grEntry != null)
                            connection.ReadToCollection(insertCollection, rec => new Group(rec), updateCommand, grEntry.GroupName);
                        var insertedGroup = insertCollection.FirstOrDefault();
                        if (insertedGroup == null)
                            return insertedGroup.ConvertTo<T>();
                        // добавляем запись в кэш
                        _groups.Add(insertedGroup.GroupId, insertedGroup);
                        //обновление данных на контролах - подписантах
                        SendCacheCanged<T>();

                        return insertedGroup.ConvertTo<T>();
                    }
                    if (type == typeof(Song)) //добавление новой песни
                    {
                        var insertCollection = new Collection<Song>();
                        var grEntry = source as Song;

                        updateCommand = @"declare @id int;
                                                        insert [Songs.Test]
                                                        (songName,groupId) 
                                                        values (?,?); 
                                                        select @id=SCOPE_IDENTITY();

                                                        select 
                                                        songId=s.songId,
                                                        groupId=s.groupId,
                                                        songName=s.songName,
                                                        groupName=g.groupName 
                                                        from [Songs.Test] as s 
                                                        join [Groups.Test] as g
                                                        on s.groupId = g.groupId
                                                        where s.songId=@id;";
                        if (grEntry != null)
                            connection.ReadToCollection(insertCollection, rec => new Song(rec), updateCommand, grEntry.SongName, grEntry.GroupId);
                        var insertedSong = insertCollection.FirstOrDefault();
                        if (insertedSong == null)
                            return insertedSong.ConvertTo<T>();
                        _songs.Add(insertedSong.SongId, insertedSong);   //добавить в кэш
                        SendCacheCanged<T>();        //обновление данных на контролах - подписантах
                        return insertedSong.ConvertTo<T>();
                    }
                    if (type == typeof(Album))     //добавление нового альбома
                    {
                        var insertCollection = new Collection<Album>();
                        var grEntry = source as Album;
                        updateCommand =               @"declare @id int;
                                                        insert [Albums.Test]
                                                        (albumTitle) 
                                                        values (?);                                              
                                                        select @id=SCOPE_IDENTITY();

                                                        select 
                                                        a.albumTitle,
                                                        a.albumId
                                                        from [Albums.Test] as a
                                                        where a.albumId=@id;";
                        if (grEntry != null)
                            connection.ReadToCollection(insertCollection, rec => new Album(rec), updateCommand, grEntry.AlbumName, grEntry.AlbumId);
                        var insertedAlbum = insertCollection.FirstOrDefault();
                        if (insertedAlbum == null) 
                            return insertedAlbum.ConvertTo<T>();
                        _albums.Add(insertedAlbum.AlbumId, insertedAlbum);
                        SendCacheCanged<T>();

                        return insertedAlbum.ConvertTo<T>();
                    }

                    if (type == typeof(AlbumSong))    //добавление новой песни в альбом
                    {
                        var insertCollection = new Collection<AlbumSong>();
                        var grEntry = source as AlbumSong;
                        updateCommand = @"insert [AlbumSongs.Test]
                                            (albumId,songId) 
                                           values (?,?)
  
                                           SELECT songId=s.songId
                                            ,groupId=s.groupId
                                            ,songName=s.songName
                                            ,groupName=g.groupName
                                            ,albumId=als.albumId
                                            ,albumTitle = a.albumTitle 
                                            from [AlbumSongs.Test] as als 
                                            join [Songs.Test] as s on als.songId = s.songId 
                                            join [Groups.Test] as g on s.groupId = g.groupId
                                            join [Albums.Test] as a on als.albumId = a.albumId where als.albumId=? and als.songId=?";

                        if (grEntry != null)
                            connection.ReadToCollection(insertCollection, rec => new AlbumSong(rec), updateCommand, grEntry.AlbumId, grEntry.SongId, grEntry.AlbumId, grEntry.SongId);

                        var insertedAlbumSong = insertCollection.FirstOrDefault();
                        if (insertedAlbumSong == null) 
                            return insertedAlbumSong.ConvertTo<T>();

                        _albumSongs.Add(insertedAlbumSong);
                        SendCacheCanged<T>();
                        return insertedAlbumSong.ConvertTo<T>();
                    }

                }
                                         
            }
            return source;
        }
        #endregion

        #region Событие для обновления кэшей
        /// <summary>
        /// событие рефреша кэша
        /// </summary>
        public Action<Type, ICollection> OnCacheChanged
        {
            get                                                            
            {
                return _cacheChanged;
            }
            set
            {
                _cacheChanged += value;
            }
        }
        /// <summary>
        ///  обработчик события рефреша данных в таблицах
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void SendCacheCanged<T>()
        {
            if (!_timer.Enabled && _isTimerRunning)
                return;

            var t = CheckType<T>();
            var handler = OnCacheChanged;
            if (handler != null)
                handler(t, SelectFromCache<T>().ToList());                      
        }
        #endregion

        #region Проверка типа
        /// <summary>   Проверка типа </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns>Тип</returns>
        private Type CheckType<T>()
        {
            if (!_enabledTypes.Contains(typeof(T)))
                throw new Exception("Незнакомый тип");
            return typeof(T);
        } 
        #endregion
    }                      
}
                                     




                                                       