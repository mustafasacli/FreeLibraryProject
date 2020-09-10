using FreeLibrary.Core;
using FreeLibrary.Core.Parameter;

namespace FreeLibrary.QueryBuilding
{
    /// <summary>
    /// Description of IQuery.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets Hashmap contains parameter(s).
        /// </summary>
        Hashmap Parameters { get; set; }

        /// <summary>
        /// Gets Sql Query.
        /// </summary>
        string Text { get; set; }
    }
}