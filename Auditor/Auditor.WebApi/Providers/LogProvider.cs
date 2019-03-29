using Auditor.Core.Helpers;
using Auditor.Core.Models;
using Auditor.WebApi.Helpers;
using Auditor.WebApi.Models;
using AutoMapper;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace Auditor.WebApi.Providers
{
    public static class LogProvider
    {
        public static AuditDataRest GetLogs(AuditDataFilter filter)
        {
            if (filter.PageSize == 0)
                throw new ArgumentNullException("filter.Size");

            var columns = $"{nameof(AuditData.LogGuid)}, {nameof(AuditData.SiteGuid)}, {nameof(AuditData.UserGuid)}, {nameof(AuditData.DateCreated)}, {nameof(AuditData.Action)}, {nameof(AuditData.ObjectName)}, {nameof(AuditData.SecondObjectName)}, {nameof(AuditData.ObjectGuid)}, {nameof(AuditData.Data)}";
            var query = $"SELECT @columns@ FROM {DatabaseHelper.AuditorDatabaseTable}";

            var filterData = WhereConditionHelper.GetWhereConditionsAndParameters(filter);

            var whereConditions = filterData.Item1;
            var parameters = filterData.Item2;

            if (whereConditions.Count > 0)
                query += $"  WHERE {string.Join(" AND ", whereConditions)}";

            var countQuery = query.Replace("@columns@", "COUNT(*)");
            query = query.Replace("@columns@", columns);

            PagingHelper.ApplyPaging(ref query, filter);

            var result = new AuditDataRest
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                OrderBy = filter.OrderBy
            };

            result.TotalCount = DatabaseHelper.ExecuteCountQuery(new CMS.DataEngine.QueryParameters(countQuery, parameters, CMS.DataEngine.QueryTypeEnum.SQLQuery));
            if (result.TotalCount == 0)
                return result;

            var data = DatabaseHelper.ExecuteQuery(new CMS.DataEngine.QueryParameters(query, parameters, CMS.DataEngine.QueryTypeEnum.SQLQuery));
            result.Items = new List<AuditDataRestItem>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                result.Items.Add(Mapper.Map<AuditDataRestItem>(DataTableHelper.FillClass<AuditData>(row)));
            }
                        
            return result;
        }

        public static AuditDataRestItem GetLog(Guid logGuid)
        {
            var query = $"SELECT * FROM {DatabaseHelper.AuditorDatabaseTable} WHERE {nameof(AuditData.LogGuid)} = @{nameof(AuditData.LogGuid)}";
            var parameters = new CMS.DataEngine.QueryDataParameters();
            parameters.Add(nameof(AuditData.LogGuid), logGuid);

            var data = DatabaseHelper.ExecuteQuery(new CMS.DataEngine.QueryParameters(query, parameters, CMS.DataEngine.QueryTypeEnum.SQLQuery));
            if (DataHelper.DataSourceIsEmpty(data))
                return null;

            var logItem = Mapper.Map<AuditDataRestItem>(DataTableHelper.FillClass<AuditData>(data.Tables[0].Rows[0]));
            
            return logItem;
        }
    }
}
