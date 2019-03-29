using Auditor.WebApi.Models;
using System;

namespace Auditor.UI.Models
{
    public class ObjectDisplayConfiguration
    {
        public Type Type { get; set; }
        public bool UseObjectNameInsteadOfType { get; set; } = false;

        public string ObjectNameAccessor { get; set; }
        public string SecondObjectNameAccessor { get; set; }
    }
}
