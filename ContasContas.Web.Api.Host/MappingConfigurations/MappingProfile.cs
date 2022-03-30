using Contas.Web.Api.Data.Dto;
using Contas.Web.Api.Data.Entities;

namespace Contas.Web.Api.Host.MappingConfigurations
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<PlanoContas, PlanoContasDto>().ReverseMap();
        }
    }
}
