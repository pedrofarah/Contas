using AutoMapper;
using Contas.Web.Api.Infrastructure.Persistence.DataModule.Interfaces;
using Contas.Web.Api.Service.BaseService.Interface;

namespace Contas.Web.Api.Service.BaseService
{
    public class BaseService : IBaseService
    {
        public BaseService(
            IDataModule dataModule,
            IMapper mapper)
        {
            this.DataModule = dataModule;
            this.Mapper = mapper;
        }

        public readonly IDataModule DataModule;

        public readonly IMapper Mapper;

    }
}
