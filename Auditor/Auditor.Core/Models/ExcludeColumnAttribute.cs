using System;

namespace Auditor.Core.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ExcludeColumnAttribute : Attribute
    {
    }
}
