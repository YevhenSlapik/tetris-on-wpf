using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace MusicCollection.Database
{
    /// <summary>Интерфейс который предоставляет функционал для подключения к БД</summary>
    public interface IDatabase
    {
        /// <summary> Получение конекта к базе </summary>
        /// <returns>Конект</returns>
        IDbConnection CreateConnection();
        
        /// <summary>  Метод выбора данных </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="useCache">Давать данные из кеша</param>
        /// <returns>Коллекция выбранных записей</returns>
        ICollection<T> Select<T>(bool useCache=false);

        /// <summary> Метод удаления записей </summary>
        /// <typeparam name="T">тип группы, песни итд</typeparam>
        void Delete<T>(ICollection<T> toDelete);

        /// <summary>
        ///   Метод изменения записей в таблицах
        /// </summary>
        /// <typeparam name="T">тип группы, песни итд.</typeparam>
        /// <param name="source"></param>
        T Update<T>(T source);

        /// <summary>
        ///   Метод добаления записей в таблицы
        /// </summary>
        /// <typeparam name="T">тип группы, песни итд.</typeparam>
        /// <param name="source">изменяемый обьект в БД</param>
        /// <returns>измененный обьект</returns>
        T Insert<T>(T source);

        Action<Type, ICollection> OnCacheChanged { get; set; }

    }   
}
