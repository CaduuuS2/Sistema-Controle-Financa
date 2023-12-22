using System.Globalization;
using AutoMapper;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class ApagarService : IApagarService
    {
        private readonly IApagarRepository _apagarRepository;
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;
        private readonly IMapper _mapper;

         public ApagarService(IApagarRepository ApagarRepository, INaturezaDeLancamentoRepository naturezaDeLancamentoRepository, IMapper mapper)
        {
            _apagarRepository = ApagarRepository;
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
            _mapper = mapper;
        }
    
        public async Task<ApagarResponseContract> Adicionar(ApagarRequestContract entidade, long idUsuario)
        {
            var apagar = _mapper.Map<Apagar>(entidade);
            apagar.DataCadastro = DateTime.Now;
            apagar = await _apagarRepository.Adicionar(apagar);
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task<ApagarResponseContract> Atualizar(long id, ApagarRequestContract entidade, long idUsuario)
        { 
            var _ = await ApagarExisteEPertenceAUsuario(id, idUsuario);
            var apagar = _mapper.Map<Apagar>(entidade);
            apagar.Id = id;
            apagar = await _apagarRepository.Atualizar(apagar);
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var Apagar = await ApagarExisteEPertenceAUsuario(id, idUsuario);
            await _apagarRepository.Deletar(_mapper.Map<Apagar>(Apagar));
        }

        public async Task<IEnumerable<ApagarResponseContract>> Obter(long idUsuario)
        {
            var apagar = await _apagarRepository.ObterPeloIdUsuario(idUsuario);
            return apagar.Select(apagar => 
            {
                return _mapper.Map<ApagarResponseContract>(apagar);
            });
        }

        public async Task<ApagarResponseContract> Obter(long id, long idUsuario)
        {
            var apagar = await ApagarExisteEPertenceAUsuario(id, idUsuario);  
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task<IEnumerable<ApagarResponseContract>> ObterPorNatureza(long idNatureza, long idUsuario)
        {
            var _ = await NaturezaExisteEPertenceAUsuario(idNatureza, idUsuario);
            var apagares = await Obter(idUsuario);
            return apagares.Where(a => a.IdNaturezaDeLancamento == idNatureza)
            .Select(a => _mapper.Map<ApagarResponseContract>(a));
        }

        public async Task<IEnumerable<ApagarResponseContract>> ObterPorVencimento(long idUsuario, DateTime vencimentoInicial, DateTime vencimentoFinal)
        {
            var apagares = await _apagarRepository.ObterPeloIdUsuario(idUsuario);
            return apagares.Where(a => a.DataVencimento.CompareTo(vencimentoInicial) >= 0 
            && a.DataVencimento.CompareTo(vencimentoFinal) <= 0)
            .Select(a => _mapper.Map<ApagarResponseContract>(a));
        }

        public async Task<IEnumerable<ApagarResponseContract>> ObterTituloBaixado(long idUsuario)
        {
             var apagares = await _apagarRepository.ObterPeloIdUsuario(idUsuario);
            return apagares.Where(a => a.DataPagamento != null)
            .Select(a => _mapper.Map<ApagarResponseContract>(a));
        }

        private async Task<Apagar> ApagarExisteEPertenceAUsuario(long id, long idUsuario)
        {
            var apagar = await _apagarRepository.Obter(id);
            if(apagar is null)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma A Pagar pelo id {id}.");
            }
            var natureza = await _naturezaDeLancamentoRepository.Obter(apagar.IdNaturezaDeLancamento);
            if(natureza is null || natureza.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma A Pagar pelo id {id}.");
            }
            return apagar;
        }

        private async Task<NaturezaDeLancamento> NaturezaExisteEPertenceAUsuario(long idNatureza, long idUsuario)
        {
            var natureza = await _naturezaDeLancamentoRepository.Obter(idNatureza);
            if(natureza is null || natureza.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma Natureza de lancamento pelo id {idNatureza}.");
            }
            return natureza;
        }
    }
}