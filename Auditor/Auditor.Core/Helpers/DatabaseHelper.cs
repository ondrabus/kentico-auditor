using CMS.DataEngine;
using CMS.Helpers;
using System.Data;
using System.IO;
using System.Reflection;

namespace Auditor.Core.Helpers
{
    public static class DatabaseHelper
    {
        public const string AuditorDatabaseTable = "Auditor_Log";

        public static void BulkInsert<T>(DataTable table)
        {
            GetConnection().BulkInsert(table, AuditorDatabaseTable, new BulkInsertSettings { Mappings = DataTableHelper.GetMapping<T>() });
        }

        public static DataSet ExecuteQuery(QueryParameters query)
        {
            return GetConnection().ExecuteQuery(query);
        }

        public static int ExecuteCountQuery(QueryParameters query)
        {
            return ValidationHelper.GetInteger(GetConnection().ExecuteScalar(query), 0);
        }

        private static GeneralConnection GetConnection()
        {
            // try get alternative connection string
            var alternativeConnectionString = ConnectionHelper.GetConnectionString(PredefinedConstants.AuditorConnectionStringName, true);
            if (!string.IsNullOrEmpty(alternativeConnectionString))
                return ConnectionHelper.GetConnection(alternativeConnectionString);

            return ConnectionHelper.GetConnection();
        }

        public static void Install()
        {
            var query = string.Empty;
            using (Stream stream = typeof(DatabaseHelper).Assembly.GetManifestResourceStream("Auditor.Core.install.sql"))
            using (StreamReader reader = new StreamReader(stream))
            {
                query = reader.ReadToEnd();
            }

            GetConnection().ExecuteNonQuery(new QueryParameters(query, null, QueryTypeEnum.SQLQuery));
        }
    }
}
