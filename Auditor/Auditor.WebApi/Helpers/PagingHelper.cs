using Auditor.Core.Models;
using Auditor.WebApi.Models;
namespace Auditor.WebApi.Helpers
{
    internal static class PagingHelper
    {
        public static void ApplyPaging(ref string query, AuditDataFilter filter)
        {
            if (string.IsNullOrEmpty(filter.OrderBy))
                filter.OrderBy = $"{nameof(AuditData.DateCreated)} DESC";

            query += $" ORDER BY {filter.OrderBy} ";

            // return all items
            if (filter.PageSize < 0)
                return;

            query += $"OFFSET {filter.PageSize * filter.Page} ROWS ";

            query += $"FETCH NEXT {filter.PageSize} ROWS ONLY ";
        }
    }
}
