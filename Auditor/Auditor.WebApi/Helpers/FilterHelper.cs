using Auditor.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Auditor.WebApi.Helpers
{
    internal class FilterHelper
    {
        public static Dictionary<PropertyInfo, FilterableAttribute> GetFilters(Type t)
        {
            var properties = t
                .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(p => Attribute.IsDefined(p, typeof(FilterableAttribute)));

            var filters = new Dictionary<PropertyInfo, FilterableAttribute>();
            foreach (var property in properties)
            {
                filters.Add(property, (FilterableAttribute)property.GetCustomAttributes(typeof(FilterableAttribute), false).Single());
            }

            return filters;
        }
    }
}
