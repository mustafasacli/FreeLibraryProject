using System.Collections.Generic;
using FreeLibrary.Entity.Validation;

namespace FreeLibrary.Entity
{
    /// <summary>
    /// Description of IBaseBO.
    /// </summary>
    public interface IBaseBO
    {
        /// <summary>
        /// Returns Table Name Of IBaseBO object.
        /// </summary>
        /// <returns>Returns Table Name Of IBaseBO object.</returns>
        string GetTableName();

        /// <summary>
        ///  Returns Identity Name Of IBaseBO object.
        /// </summary>
        /// <returns>Returns Identity Column Name Of IBaseBO object.</returns>
        string GetIdColumn();

        /// <summary>
        /// Clears all change columns list.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets Count of Changed Property.
        /// </summary>
        int ChangeSetCount { get; }

        /// <summary>
        /// Returns true if Value of Property with given name changes, 
        /// return true; else returns false.
        /// </summary>
        /// <param name="propName">Property Name</param>
        /// <returns>Returns bool object.</returns>
        bool IsPropertyChanged(string propName);

        /// <summary>
        /// Returns List of Changed Properties.
        /// </summary>
        /// <returns>Returns a List of strings object. </returns>
        List<string> GetColumnChangeList();

        /// <summary>
        /// Make Validation Operation for Insert/Update Object.
        /// </summary>
        /// <param name="isUpdate">Determines Validation is for update operation. 
        /// If true, Validation is for Update operation else Validation Insert Operation.</param>
        /// <returns>Returns IEntityValidationResult object.</returns>
        IEntityValidationResult ValidateObject(bool isUpdate = false);

        /// <summary>
        /// if includeIdColumn is true, Clears all change columns list, else Clear changes except id column.
        /// </summary>
        /// <param name="includeIdColumn">id column include determining parameter.</param>
        void ClearChanges(bool includeIdColumn = false);

        bool IsNullOrDefault();
    }
}