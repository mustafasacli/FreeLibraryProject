namespace FreeLibrary.QueryBuilding
{
    public enum QueryTypes : ushort
    {
        Select = 100,
        Insert = 101,
        Update = 102,
        Delete = 103,
        InsertAndGetId = 104,
        SelectWhereId = 105,
        SelectWhereChangeColumns = 106,
        SelectChangeColumns = 107,
        InsertAnyChange = 108
    };
}