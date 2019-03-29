using System;
using System.Collections.Generic;

namespace Auditor.WebApi.Models
{
    public class AuditDataFilter : PagingData
    {
        [Filterable(FilterableType.Equal, columns: "SiteGuid, ObjectGuid, SecondObjectGuid")]
        public Guid? SiteGuid { get; set; }

        [Filterable(FilterableType.Equal, columns: "UserGuid, ObjectGuid, SecondObjectGuid")]
        public Guid? UserGuid { get; set; }

        [Filterable(FilterableType.Equal, columns: "ObjectGuid, SecondObjectGuid")]
        public Guid? ObjectGuid { get; set; }

        [Filterable(FilterableType.Equal)]
        public string Action { get; set; }

        [Filterable(FilterableType.GreaterOrEqual, columns: "DateCreated")]
        public DateTime? DateStart { get; set; }
        
        [Filterable(FilterableType.LessOrEqual, columns: "DateCreated")]
        public DateTime? DateEnd { get; set; }

        [Filterable(FilterableType.Equal)]
        public string ObjectName { get; set; }
        [Filterable(FilterableType.Equal)]
        public string SecondObjectName { get; set; }

        [Filterable(FilterableType.Contained, columns: "ObjectName, SecondObjectName, ObjectGuid, SecondObjectGuid, Data")]
        public string SearchText { get; set; }
        
        public Dictionary<string, string> DataSearch { get; set; }
    }
}
