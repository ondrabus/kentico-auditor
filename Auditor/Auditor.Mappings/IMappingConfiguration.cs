using AutoMapper;

namespace Auditor.Mappings
{
    public interface IMappingConfiguration
    {
        IMapperConfigurationExpression RegisterMappings(IMapperConfigurationExpression configurationExpression);
    }
}
