using Auditor.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Auditor.Core.Helpers
{
    public static class DataTableHelper
    {
        internal static DataTable GetDataTable<T>()
        {
            var type = typeof(T);
            var properties = type.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(ExcludeColumnAttribute)));

            var table = new DataTable(type.FullName);
            foreach (var info in properties)
            {
                var columnType = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;
                if (Attribute.IsDefined(info, typeof(SerializeFieldAttribute)))
                {
                    columnType = typeof(string);
                }

                table.Columns.Add(new DataColumn(info.Name, columnType));
            }

            return table;
        }

        internal static void FillRow<T>(DataRow row, T data)
        {
            var type = typeof(T);
            var properties = type.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(ExcludeColumnAttribute))).ToList();

            if (type.FullName != row.Table.TableName)
                throw new InvalidOperationException($"Type of provided DataRow '{row.Table.TableName}' is different than provided type '{type.FullName}'.");

            if (properties.Count != row.Table.Columns.Count)
                throw new InvalidOperationException($"Number of properties of type '{type.Name}' is different than number of columns of the provided DataRow.");

            for (var i = 0; i < properties.Count; i++)
            {
                var value = properties[i].GetValue(data);
                if (Attribute.IsDefined(properties[i], typeof(SerializeFieldAttribute)))
                {
                    value = SerializationHelper.Serialize(value);
                }

                row[i] = value;
            }
        }
        
        public static T FillClass<T>(DataRow row) where T : new()
        {
            var obj = new T();

            var type = typeof(T);
            var properties = type.GetProperties();
            
            foreach (DataColumn col in row.Table.Columns)
            {
                var matchingProperty = properties.Single(p => p.Name == col.ColumnName);
                var value = row[col.ColumnName];

                if (value is DBNull)
                    value = string.Empty;

                if (Attribute.IsDefined(matchingProperty, typeof(SerializeFieldAttribute)))
                {
                    value = SerializationHelper.Unserialize(value.ToString(), matchingProperty.PropertyType);
                }

                matchingProperty.SetValue(obj, value);
            }

            return obj;
        }

        internal static Dictionary<string, string> GetMapping<T>()
        {
            var dictionary = new Dictionary<string, string>();

            var type = typeof(T);
            var properties = type.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(ExcludeColumnAttribute))).ToList();
            
            for (var i = 0; i < properties.Count; i++)
            {
                dictionary.Add(properties[i].Name, properties[i].Name);
            }

            return dictionary;
        }
    }
}