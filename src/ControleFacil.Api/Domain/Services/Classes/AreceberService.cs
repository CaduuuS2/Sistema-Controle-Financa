using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class AreceberService : IAreceberService
    {
        private readonly IAreceberRepository _areceberRepository;
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;
        private readonly IMapper _mapper;

         public AreceberService(IAreceberRepository AreceberRepository, INaturezaDeLancamentoRepository naturezaDeLancamentoRepository, IMapper mapper)
        {
            _areceberRepository = AreceberRepository;
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
            _mapper = mapper;
        }
    
        public async Task<AreceberResponseContract> Adicionar(AreceberRequestContract entidade, long idUsuario)
        {
            var Areceber = _mapper.Map<Areceber>(entidade);
            Areceber.DataCadastro = DateTime.Now;
            Areceber = await _areceberRepository.Adicionar(Areceber);
            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(long id, AreceberRequestContract entidade, long idUsuario)
        { 
            var _ = await AreceberExisteEPertenceAUsuario(id, idUsuario);
            var Areceber = _mapper.Map<Areceber>(entidade);
            Areceber.Id = id;
            Areceber = await _areceberRepository.Atualizar(Areceber);
            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var Areceber = await AreceberExisteEPertenceAUsuario(id, idUsuario);
            await _areceberRepository.Deletar(_mapper.Map<Areceber>(Areceber));
        }

        public async Task<IEnumerable<AreceberResponseContract>> Obter(long idUsuario)
        {
            var areceberes = await _areceberRepository.ObterPeloIdUsuario(idUsuario);
            return areceberes.Select(Areceber => 
            {
                return _mapper.Map<AreceberResponseContract>(Areceber);
            });
        }

        public async Task<AreceberResponseContract> Obter(long id, long idUsuario)
        {
            var Areceber = await AreceberExisteEPertenceAUsuario(id, idUsuario);  
            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task<IEnumerable<AreceberResponseContract>> ObterPorNatureza(long idNatureza, long idUsuario)
        {
            var _ = await NaturezaExisteEPertenceAUsuario(idNatureza, idUsuario);
            var areceberes = await Obter(idUsuario);
            return areceberes.Where(a => a.IdNaturezaDeLancamento == idNatureza)
            .Select(a => _mapper.Map<AreceberResponseContract>(a));
        }

        private async Task<Areceber> AreceberExisteEPertenceAUsuario(long id, long idUsuario)
        {
            var areceber = await _areceberRepository.Obter(id);
            if(areceber is null)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma A Pagar pelo id {id}.");
            }
            var natureza = await _naturezaDeLancamentoRepository.Obter(areceber.IdNaturezaDeLancamento);
            if(natureza is null || natureza.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma A Pagar pelo id {id}.");
            }
            return areceber;
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

        public async Task<IEnumerable<AreceberResponseContract>> ObterPorVencimento(long idUsuario, DateTime vencimentoInicial, DateTime vencimentoFinal)
        {
            var areceberes = await _areceberRepository.ObterPeloIdUsuario(idUsuario);
            return areceberes.Where(a => a.DataVencimento.CompareTo(vencimentoInicial) >= 0 
            && a.DataVencimento.CompareTo(vencimentoFinal) <= 0)
            .Select(a => _mapper.Map<AreceberResponseContract>(a));
        }

        public async Task<IEnumerable<AreceberResponseContract>> ObterTituloBaixado(long idUsuario)
        {
             var areceberes = await _areceberRepository.ObterPeloIdUsuario(idUsuario);
            return areceberes.Where(a => a.DataRecebimento != null)
            .Select(a => _mapper.Map<AreceberResponseContract>(a));
        }
    }
}