using Contas.Web.Api.Data.Entities;
using Contas.Web.Api.Infrastructure.Persistence.Repositories.Interfaces;

namespace Contas.Web.Api.Infrastructure.Persistence.DataModule.Interfaces
{
    public interface IDataModule
    {
        IRepository<PlanoContas> PlanoContasRepository { get; }
        void CommitData();
    }
}
