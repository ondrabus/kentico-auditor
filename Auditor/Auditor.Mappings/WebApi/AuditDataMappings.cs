using AutoMapper;
using Auditor.Core.Models;
using Auditor.WebApi.Models;

namespace Auditor.Mappings.WebApi
{
    public class AuditDataMappings : IMappingConfiguration
    {
        public IMapperConfigurationExpression RegisterMappings(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<AuditData, AuditDataRestItem>();
            configurationExpression.CreateMap<DataField, DataFieldRest>();

            return configurationExpression;
        }
    }
}
