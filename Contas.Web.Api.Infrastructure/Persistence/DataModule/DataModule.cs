using Contas.Web.Api.Data.Entities;
using Contas.Web.Api.Infrastructure.Persistence.DataModule.Interfaces;
using Contas.Web.Api.Infrastructure.Persistence.Repositories;
using Contas.Web.Api.Infrastructure.Persistence.Repositories.Interfaces;

namespace Contas.Web.Api.Infrastructure.Persistence.DataModule
{
    public class DataModule : IDataModule, IDisposable
    {

        private readonly ApplicationDbContext appDbContext;

        public DataModule(ApplicationDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        public void CommitData()
        {
            appDbContext.SaveChanges();
        }

        private IRepository<PlanoContas> planoContasRepository;
        public IRepository<PlanoContas> PlanoContasRepository
        {
            get
            {
                planoContasRepository ??= new Repository<PlanoContas>(appDbContext);
                return planoContasRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                appDbContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
