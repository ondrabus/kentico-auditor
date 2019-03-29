using System;

namespace Auditor.Core.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializeFieldAttribute : Attribute
    {
    }
}