using AutoMapper;
using Contas.Web.Api.Data.Dto;
using Contas.Web.Api.Data.Entities;
using Contas.Web.Api.Infrastructure.Persistence.DataModule.Interfaces;
using Contas.Web.Api.Service.PlanoContasService.Interfaces;

namespace Contas.Web.Api.Service.PlanoContasService
{
    public class PlanoContasService : BaseService.BaseService, IPlanoContasService
    {
        public PlanoContasService(
            IDataModule dataModule,
            IMapper mapper) 
        : base(dataModule, mapper) {}

        public List<PlanoContasDto> GetAll()
        {
            var _list = this.DataModule.PlanoContasRepository.List();

            var result = this.Mapper.Map<List<PlanoContas>, List<PlanoContasDto>>(_list?.ToList() ?? new List<PlanoContas>());

            return result;
        }

        public List<PlanoContasDto> GetByName(string Name)
        {
            var _list = this.DataModule.PlanoContasRepository.List(x => x.Nome.Contains(Name));

            var result = this.Mapper.Map<List<PlanoContas>, List<PlanoContasDto>>(_list?.ToList() ?? new List<PlanoContas>());

            return result;
        }

        public async Task<PlanoContasDto> GetById(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                    throw new ArgumentNullException("Código da conta não informado.");

                var _row = await this.DataModule.PlanoContasRepository.GetAsync(x => x.Codigo == Id) 
                    ?? throw new ArgumentNullException("Conta não localizada.");

                var result = this.Mapper.Map<PlanoContas, PlanoContasDto>(_row);

                return result;
            }
            catch (Exception ex)
            {
                return await Task.FromException<PlanoContasDto>(new ArgumentException(ex?.InnerException?.Message ?? ex?.Message));
            }
        }

        public List<PlanoContasDto> GetParents(string Type)
        {
            var _list = DataModule.PlanoContasRepository.List(x => x.AceitaLancamentos == false && x.Tipo == Type);

            var result = this.Mapper.Map<List<PlanoContas>, List<PlanoContasDto>>(_list?.ToList() ?? new List<PlanoContas>());

            return result;
        }

        private string CalculateNextId(string Code)
        {
            string JoinArray(string[] value)
            {
                string result = string.Empty;

                foreach (var item in value)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        result += item + ".";
                    }
                }

                return result;
            }

            string[] ArrTrim(string[] value, int pos)
            {
                for (int x = pos; x < value.Length; x++)
                {

                    value[x] = string.Empty;
                }
                return value;
            }

            var spCode = Code.Split('.');

            for (int x = spCode.Length - 1; x >= 0; x++)
            {
                int posCode = Convert.ToInt32(spCode[x]);

                if (posCode < 999)
                {
                    spCode = ArrTrim(spCode, x);
                    string ncode = JoinArray(spCode);
                    posCode++;
                    return $"{ncode}{posCode}";
                }
                else
                {
                    if (spCode.Length > 1)
                    {
                        posCode = Convert.ToInt32(spCode[x - 1]);

                        spCode = ArrTrim(spCode, x-1);

                        string ncode = JoinArray(spCode);

                        return CalculateNextId($"{ncode}{posCode}");
                    }

                }
            }

            return string.Empty;
        }


        public async Task<string> GetNextId(string ParentId)
        {
            string result = $"{ParentId}.1";

            var _pconta = DataModule.PlanoContasRepository.List(x => x.ContaPai == ParentId).OrderBy(o => o.Codigo).LastOrDefault();

            if (_pconta != null)
            {
                result = CalculateNextId(_pconta?.Codigo ?? string.Empty);

                var _pcontaVerif = await DataModule.PlanoContasRepository?.GetAsync(x => x.Codigo == result) ?? new PlanoContas();

                while (_pcontaVerif != null)
                {
                    result = CalculateNextId(result);
                    _pcontaVerif = await DataModule.PlanoContasRepository.GetAsync(x => x.Codigo == result);
                }
            }

            return result;
        }

        public async Task<bool> AddAsync(PlanoContasDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException("Código da conta não informado.");

                if (!string.IsNullOrEmpty(dto.ContaPai))
                {
                    await IsSameType(dto, new PlanoContasDto { Codigo = dto.ContaPai });
                }

                var pcontas = this.Mapper.Map<PlanoContasDto, PlanoContas>(dto);

                var result = await DataModule.PlanoContasRepository.AddAsync(pcontas);

                this.DataModule.CommitData();

                return true;
            }
            catch (Exception ex)
            {
                string error_msg = ex?.InnerException?.Message ?? ex?.Message ?? string.Empty;

                if (error_msg.ToLower().Contains("pk_tb_planocontas"))
                {
                    error_msg = "Plano de contas cadastrado em duplicidade.";
                }

                return await Task.FromException<bool>(new ArgumentException(error_msg));
            }
        }

        public async Task<bool> UpdateAsync(PlanoContasDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException("Código da conta não informado.");

                if (IsParent(dto) && dto.AceitaLancamentos)
                    throw new ArgumentException("Conta definida como conta pai de outras contas. Não é possível aceitar lançamentos.");

                if (!string.IsNullOrEmpty(dto.ContaPai))
                {
                    await IsSameType(dto, new PlanoContasDto { Codigo = dto.ContaPai });
                }

                var pContas = await DataModule.PlanoContasRepository.FinAsync(dto.Codigo) 
                    ?? throw new ArgumentException("Plano de contas não localizado.");

                pContas.Nome = dto.Nome;
                pContas.Tipo = dto.Tipo;
                pContas.AceitaLancamentos = dto.AceitaLancamentos;
                pContas.ContaPai = dto.ContaPai;

                this.DataModule.CommitData();

                return true;
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(new ArgumentException(ex?.InnerException?.Message ?? ex?.Message ?? string.Empty));
            }
        }

        public async Task<bool> DeleteAsync(PlanoContasDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException("Código da conta não informado.");

                if (IsParent(dto))
                    throw new ArgumentException("Conta definida como conta pai de outras contas. Não é possível excluí-la.");

                var pContas = await DataModule.PlanoContasRepository.FinAsync(dto.Codigo)
                    ?? throw new ArgumentException("Plano de contas não localizado.");

                DataModule.PlanoContasRepository.Delete(pContas);

                this.DataModule.CommitData();

                return true;
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(new ArgumentException(ex?.InnerException?.Message ?? ex?.Message ?? string.Empty));
            }
        }

        private bool IsParent(PlanoContasDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException("Conta não informada.");

                var _list = this.DataModule.PlanoContasRepository.List(x => x.ContaPai == dto.Codigo);

                return (_list?.Count() ?? 0) > 0;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsSameType(PlanoContasDto dto, PlanoContasDto parent)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.ContaPai))
                {
                    var _row = await DataModule.PlanoContasRepository.GetAsync(x => x.Codigo == parent.Codigo)
                        ?? throw new ArgumentException("Conta pai não localizada.");

                    if (_row.Tipo != dto.Tipo)
                    {
                        throw new ArgumentException("O tipo de conta definida não pode ser diferente do tipo da conta pai.");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(new ArgumentException(ex?.InnerException?.Message ?? ex?.Message ?? string.Empty));
            }
        }



    }
}
