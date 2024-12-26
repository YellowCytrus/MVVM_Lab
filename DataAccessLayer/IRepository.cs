using DomainObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Возвращает всех студентов.
        /// </summary>
        /// <returns>Коллекция всех студентов.</returns>
        IEnumerable<Student> GetAll();

        /// <summary>
        /// Возвращает студента по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор студента.</param>
        /// <returns>Студент с заданным идентификатором или null, если студент не найден.</returns>
        Student GetById(int id);

        /// <summary>
        /// Обновляет данные студента.
        /// </summary>
        /// <param name="fromId">Идентификатор студента, подлежащего обновлению.</param>
        /// <param name="to">Обновленные данные студента.</param>
        void Update(int fromId, Student to);

        /// <summary>
        /// Создает нового студента.
        /// </summary>
        /// <param name="item">Данные нового студента.</param>
        void Create(Student item);

        /// <summary>
        /// Удаляет студента по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор студента, подлежащего удалению.</param>
        void Delete(int id);
    }
}
