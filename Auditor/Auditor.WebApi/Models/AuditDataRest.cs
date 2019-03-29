using System.Collections.Generic;

namespace Auditor.WebApi.Models
{
    public class AuditDataRest : PagingData
    {
        public List<AuditDataRestItem> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
