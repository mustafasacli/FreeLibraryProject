
namespace FreeLibrary.CodeGeneration.Source.BO
{
    internal static class Constants
    {
        internal static readonly string USING_Base = "using FreeLibrary.Core;";//"using Net.FreeORM.Base;";
        internal static readonly string USING_BaseBO = "using FreeLibrary.Entity;";//"using Net.FreeORM.Entity.Base;";// "using Net.FreeORM.Framework.Base;";
        internal static readonly string USING_BaseDL = "using FreeLibrary.Logic;";//"using Net.FreeORM.Logic.BaseDal;";

        internal static readonly string BO_NAMESPACE_FORMAT = "{0}.Source.BO";
        internal static readonly string DL_NAMESPACE_FORMAT = "{0}.Source.DL";

        internal static readonly string BaseBO = "BaseBO";
        internal static readonly string BaseDL = "BaseDL";

        /* ------------------------------------------------------------- */

        internal static readonly string GET_ID_COLUMN_METHOD = "GetIdColumn";
        internal static readonly string GET_TABLE_NAME_METHOD = "GetTableName";

    }
}
