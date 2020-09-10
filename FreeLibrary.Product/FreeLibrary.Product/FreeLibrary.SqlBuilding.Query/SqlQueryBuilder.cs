using FreeLibrary.Core.Enums;
using FreeLibrary.Core.Parameter;
using FreeLibrary.Entity;
using FreeLibrary.SqlBuilding.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace FreeLibrary.SqlBuilding.Query
{
    public class SqlQueryBuilder : ISqlBuilder
    {
        private SqlQuery query = new SqlQuery();

        private IBaseBO bo;
        private QueryTypes query_type;
        private ConnectionTypes conn_type;

        public SqlQueryBuilder(IBaseBO businessObj, QueryTypes queryType, ConnectionTypes connType)
        {
            bo = businessObj;
            query_type = queryType;
            conn_type = connType;
        }

        public ISql Sql { get { return query; } }

        public void BuildSql()
        {
            IQueryFormat qformat = new QueryFormat(query_type);
            IQueryAdds adds = new QueryAdds(conn_type);
            Build(bo, qformat, adds, query_type, conn_type);
        }

        public DbParameter CreateParameter()
        {
            return new FreeDbParameter();
        }

        internal void Build(IBaseBO bo, IQueryFormat qformat, IQueryAdds adds, QueryTypes query_type, ConnectionTypes conn_type)
        {
            try
            {
                string queryText = string.Empty;
                queryText = qformat.Format;
                queryText = queryText ?? string.Empty;

                string table_name = string.Empty;
                string columns = string.Empty;
                string vals = string.Empty;

                table_name = GetTableName(bo, adds);
                columns = GetColumns(bo, adds, query_type, conn_type);
                vals = GetVals(bo, adds, query_type, conn_type);

                queryText = queryText.Replace("#TABLE_NAME#", table_name);
                queryText = queryText.Replace("#COLUMNS#", columns);
                queryText = queryText.Replace("#VALS#", vals);

                query.Text = queryText;

                List<DbParameter> parameters = GetParameters(bo, adds, query_type);
                query.Parameters = parameters;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal string GetTableName(IBaseBO bo, IQueryAdds adds)
        {
            string tbl_name = string.Empty;
            try
            {
                tbl_name = bo.GetTableName();
                if (string.IsNullOrWhiteSpace(tbl_name))
                {
                    throw new ArgumentException("Table Name can not be empty or white space");
                }

                tbl_name = string.Format("{0}{1}{2}", adds.Prefix, tbl_name, adds.Suffix);
            }
            catch (Exception e)
            {
                throw;
            }

            return tbl_name;
        }

        internal string GetColumns(IBaseBO bo, IQueryAdds adds, QueryTypes query_type, ConnectionTypes conn_type)
        {
            string cols = string.Empty;
            try
            {
                List<string> colList = bo.GetColumnChangeList();

                if (query_type == QueryTypes.Insert || query_type == QueryTypes.InsertAndGetId
                    || query_type == QueryTypes.Update)
                {
                    string id_col = bo.GetIdColumn();
                    colList.Remove(id_col);
                }

                if (query_type == QueryTypes.Insert || query_type == QueryTypes.InsertAndGetId ||
                    query_type == QueryTypes.InsertAnyChange || query_type == QueryTypes.SelectChangeColumns)
                {
                    foreach (var col in colList)
                    {
                        cols = string.Concat(cols, ", ", col);
                    }
                    cols = cols.TrimStart(',').TrimStart();
                    // RETURN
                    return cols;
                }

                if (query_type == QueryTypes.Update)
                {
                    if (conn_type == ConnectionTypes.Odbc)
                    // || conn_type == ConnectionTypes.External)
                    {
                        foreach (var col in colList)
                        {
                            cols = string.Concat(cols, ", ", col, "=", adds.ParameterPrefix);
                        }
                    }
                    else
                    {
                        foreach (var col in colList)
                        {
                            cols = string.Concat(cols, ", ", col, "=", adds.ParameterPrefix, col);
                        }
                    }

                    cols = cols.TrimStart(',').TrimStart();

                    // RETURN
                    return cols;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return cols;
        }

        internal string GetVals(IBaseBO bo, IQueryAdds adds, QueryTypes query_type, ConnectionTypes conn_type)
        {
            string vals = string.Empty;

            try
            {
                List<string> colList = bo.GetColumnChangeList();
                string id_col = string.Empty;

                //IF BLOCK 1
                if (query_type == QueryTypes.Insert || query_type == QueryTypes.InsertAndGetId
                    || query_type == QueryTypes.InsertAnyChange)
                {
                    if (query_type != QueryTypes.InsertAnyChange)
                    {
                        id_col = bo.GetIdColumn();
                        colList.Remove(id_col);
                    }

                    if (conn_type == ConnectionTypes.Odbc)
                    // || conn_type == ConnectionTypes.External)
                    {
                        foreach (var col in colList)
                        {
                            vals = string.Concat(vals, ", ", adds.ParameterPrefix);
                        }
                    }
                    else
                    {
                        foreach (var col in colList)
                        {
                            vals = string.Concat(vals, ", ", adds.ParameterPrefix, col);
                        }
                    }

                    vals = vals.TrimStart(',').TrimStart();

                    //RETURN
                    return vals;
                }

                //IF BLOCK 2
                if (query_type == QueryTypes.Delete || query_type == QueryTypes.SelectWhereId
                    || query_type == QueryTypes.Update)
                {
                    id_col = bo.GetIdColumn();

                    if (conn_type == ConnectionTypes.Odbc)
                    // || conn_type == ConnectionTypes.External)
                    {
                        vals = string.Format("{0}={1}", id_col, adds.ParameterPrefix);
                    }
                    else
                    {
                        vals = string.Format("{0}={1}{0}", id_col, adds.ParameterPrefix);
                    }
                    //RETURN
                    return vals;
                }

                //IF BLOCK 3
                if (query_type == QueryTypes.SelectWhereChangeColumns)
                {
                    if (conn_type == ConnectionTypes.Odbc)
                    // || conn_type == ConnectionTypes.External)
                    {
                        foreach (var col in colList)
                        {
                            vals = string.Concat(vals, "AND ", col, "=", adds.ParameterPrefix);
                        }
                    }
                    else
                    {
                        foreach (var col in colList)
                        {
                            vals = string.Concat(vals, "AND ", col, "=", adds.ParameterPrefix, col);
                        }
                    }

                    if (vals.Length > 4)
                    {
                        vals = vals.Substring(4, vals.Length - 4);
                    }

                    //RETURN
                    return vals;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            //RETURN
            return vals;
        }

        internal List<DbParameter> GetParameters(IBaseBO bo, IQueryAdds adds, QueryTypes query_type)
        {
            List<DbParameter> parameters = null;
            DbParameter prm;
            try
            {
                if (query_type != QueryTypes.Select ||
                    query_type != QueryTypes.SelectChangeColumns)
                {
                    List<string> colList = bo.GetColumnChangeList();
                    string id_col;
                    PropertyInfo propInfo;
                    Type t = bo.GetType();
                    id_col = bo.GetIdColumn();
                    object val;

                    // IF BLOCK 1
                    if (query_type == QueryTypes.Delete || query_type == QueryTypes.SelectWhereId)
                    {
                        if (string.IsNullOrWhiteSpace(id_col))
                        {
                            throw new Exception("Id Column can not be empty or whit space.");
                        }

                        propInfo = t.GetProperty(id_col);
                        if (propInfo == null)
                        {
                            throw new Exception("Id Column Property has been specified as wrong. Please define right.");
                        }

                        val = propInfo.GetValue(bo, null);

                        if (val == null)
                        {
                            throw new Exception(string.Format("Id Column({0}) Value must be specified.", id_col));
                        }
                        parameters = new List<DbParameter>();
                        prm = this.CreateParameter();
                        prm.ParameterName = string.Format("{0}{1}", adds.ParameterPrefix, id_col);
                        prm.Value = val;
                        parameters.Add(prm);

                        return parameters;
                    }

                    // IF BLOCK 2
                    if (query_type == QueryTypes.Insert || query_type == QueryTypes.InsertAndGetId)
                    {
                        colList.Remove(id_col);
                    }

                    parameters = new List<DbParameter>();

                    foreach (var col in colList)
                    {
                        propInfo = null;
                        propInfo = t.GetProperty(col);
                        if (propInfo == null)
                        {
                            throw new Exception(
                                string.Format("{0} Column Property has been specified as wrong. Please define right.", col));
                        }

                        val = propInfo.GetValue(bo, null);

                        prm = this.CreateParameter();
                        prm.ParameterName = string.Format("{0}{1}", adds.ParameterPrefix, col);
                        prm.Value = val;
                        parameters.Add(prm);
                    }

                    //RETURN
                    return parameters;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            //RETURN
            return parameters;
        }
    }
}
