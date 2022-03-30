using Contas.Web.Api.Data.Dto;

namespace Contas.Web.Api.Service.PlanoContasService.Interfaces
{
    public interface IPlanoContasService
    {
        List<PlanoContasDto> GetAll();
        List<PlanoContasDto> GetByName(string Name);
        List<PlanoContasDto> GetParents(string Type);
        Task<PlanoContasDto> GetById(string Id);
        Task<bool> AddAsync(PlanoContasDto dto);
        Task<bool> UpdateAsync(PlanoContasDto dto);
        Task<bool> DeleteAsync(PlanoContasDto dto);
        Task<string> GetNextId(string ParentId);
    }
}
