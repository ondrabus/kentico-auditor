using System;
using System.Linq;

namespace Auditor.WebApi.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class FilterableAttribute : Attribute
    {
        public FilterableType Type { get; set; }
        public string[] Columns { get; set; }

        public FilterableAttribute(FilterableType type, string columns = null)
        {
            Type = type;

            if (columns != null)
                Columns = columns.Split(',').Select(s => s.Trim()).ToArray();
        }
    }
}