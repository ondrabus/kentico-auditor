using Auditor.Core.Actions;
using System;
using System.Collections.Generic;

namespace Auditor.Core.Models
{
    public class AuditData
    {
        [ExcludeColumn]
        public Guid LogGuid { get; set; }
        public Guid SiteGuid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ObjectGuid { get; set; }
        public Guid SecondObjectGuid { get; set; }
        public DateTime DateCreated { get; set; }
        public string Action { get; set; }
        
        public string ObjectName { get; set; }
        public string SecondObjectName { get; set; }

        [SerializeField]
        public List<DataField> Data { get; set; }
    }
}