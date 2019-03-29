using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auditor.Core.Helpers
{
    public static class InterfaceHelper
    {
        public static List<TInterface> GetImplementingClassesInstances<TInterface>()
        {
            return typeof(TInterface)
                .Assembly
                .GetTypes()
                .Where(type => typeof(TInterface).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type))
                .Cast<TInterface>()
                .ToList();
        }
    }
}