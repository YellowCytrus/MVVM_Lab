using System;
using System.Collections.Generic;
using Shared;

namespace Model
{
    public interface IModel
    {
        /// <summary>
        /// Событие, вызываемое при изменении данных.
        /// </summary>
        /// <param name="args">Элементы данных, которые были изменены.</param>
        event Action<IEnumerable<StudentEventArgs>> DataChanged;

        /// <summary>
        /// Вызывает событие DataChanged.
        /// </summary>
        void CallDataChanged();

        /// <summary>
        /// Добавть студента в базу данных
        /// </summary>
        /// <param name="IdNameSpecGroup"></param>
        void AddStudent(StudentEventArgs e);


        /// <summary>
        /// Редактировать студента.  
        /// </summary>
        /// <param name="from"> студент, подлежащий редактированию </param>
        /// <param name="to"> изменённый студент, который сохранится в БД</param>
        /// <returns>
        /// Если <paramref name="to"> уже существует в БД, true;
        /// иначе false
        /// </returns
        bool EditStudent(StudentEventArgs from, StudentEventArgs to);


        /// <summary>
        /// Удаляет студента из базы данных.
        /// </summary>
        /// <param name="nameSpecGroup"> Массив строк, содержащий имя, специальность и группу студента.
        /// Первый элемент массива - имя студента, второй - специальность, третий - группа. 
        /// </param>
        void DeleteStudent(int Id);


        /// <summary>
        /// Возвращает все данные о студенте из БД по его Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string[] GetById(int Id);


        /// <summary>
        /// Возвращает список массивов строк, где каждый массив содержит идентификатор, имя специальность и группу студента.
        /// </summary>
        /// <returns>Список массивов строк, содержащих информацию о студентах.</returns>
        List<string[]> GetAllIdNameSpecGroup();
    }
}
