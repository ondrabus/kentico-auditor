using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Auditor.Mappings
{
    public static class Config
    {
        public static void Register()
        {
            var type = typeof(IMappingConfiguration);
            var types = Assembly.GetAssembly(type).GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                foreach (var item in types)
                {
                    var instance = Activator.CreateInstance(item) as IMappingConfiguration;
                    if (instance != null)
                        instance.RegisterMappings(cfg);
                }
            });
        }
    }
}
