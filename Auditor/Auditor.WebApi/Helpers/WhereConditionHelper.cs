using Auditor.WebApi.Models;
using CMS.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.WebApi.Helpers
{
    internal class WhereConditionHelper
    {
        public static string GetWhereCondition(string columnName, FilterableType filterableType, string parameterName)
        {
            var format = "{0} = @{1}";

            switch (filterableType)
            {
                case FilterableType.GreaterOrEqual:
                    format = "{0} >= @{1}";
                    break;

                case FilterableType.LessOrEqual:
                    format = "{0} <= @{1}";
                    break;

                case FilterableType.Contained:
                case FilterableType.StartsWith:
                case FilterableType.IsInMiddle:
                    format = "{0} LIKE @{1}";
                    break;
            }

            return string.Format(format, columnName, parameterName);
        }

        public static object GetParameterValue(object value, FilterableType filterableType)
        {
            switch (filterableType)
            {
                case FilterableType.Contained:
                    return $"%{value}%";

                case FilterableType.StartsWith:
                    return $"{value}%";

                case FilterableType.IsInMiddle:
                    return $"%_{value}_%";

                default:
                    return value;
            }
        }

        public static Tuple<List<string>, QueryDataParameters> GetWhereConditionsAndParameters(AuditDataFilter filterObject)
        {
            if (filterObject == null)
                throw new ArgumentNullException("filterObject");

            var whereConditions = new List<string>();
            var parameters = new QueryDataParameters();
            var filterProperties = FilterHelper.GetFilters(typeof(AuditDataFilter));

            foreach (var filterProperty in filterProperties)
            {
                var property = filterProperty.Key;
                var filter = filterProperty.Value;

                var value = property.GetValue(filterObject);
                if (TypeHelper.IsNullOrDefault(property.PropertyType, value))
                    continue;

                if (filter.Columns == null)
                {
                    whereConditions.Add(GetWhereCondition(property.Name, filter.Type, property.Name));
                }
                else
                {
                    var orConditions = new List<string>();

                    foreach (var column in filter.Columns)
                    {
                        orConditions.Add(GetWhereCondition(column, filter.Type, property.Name));
                    }

                    // handle DataSearch
                    if (property.Name == nameof(AuditDataFilter.ObjectGuid) && filterObject.DataSearch.Any())
                    {
                        var index = 0;
                        foreach (var key in filterObject.DataSearch.Keys)
                        {
                            var paramName = "DataSearch_" + index++;
                            orConditions.Add(GetWhereCondition("Data", FilterableType.Contained, paramName));
                            parameters.Add(paramName, $"%\"n\":\"{key}\",\"v\":\"{filterObject.DataSearch[key]}\"%");
                        }
                    }

                    whereConditions.Add($"({string.Join(" OR ", orConditions)})");
                }

                parameters.Add(property.Name, GetParameterValue(value, filter.Type));
            }

            return new Tuple<List<string>, QueryDataParameters>(whereConditions, parameters);
        }
    }
}
