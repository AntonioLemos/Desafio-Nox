using DesafioNoxModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioNoxApi.Repositorios.Interfaces
{
    public interface ITransacaoRepositorio
    {
        /// <summary>
        /// Salva nova transacao.
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        Task<dynamic> PostTransacao(Transacao transacao);

        /// <summary>
        /// Lista trasacoes por data de abertura.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="dataInicial"></param>
        /// <param name="dataFinal"></param>
        /// <returns></returns>
        Task<IEnumerable<Transacao>> GetTransacoesPorData(int usuarioId, DateTime dataInicial, DateTime dataFinal);

        /// <summary>
        /// Busca transacao por lista de ids.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="listIds"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, Transacao>> GetTransacoesPorListaIds(int usuarioId, List<int> listIds);

        /// <summary>
        /// Atualiza transações e saldo do usuario
        /// </summary>
        /// <param name="transacaoList"></param>
        /// <param name="usuario"></param>
        void AlterarStatusTransacaoUsuario(Dictionary<DateTime, Transacao> transacaoList, Usuario usuario);

        /// <summary>
        /// Verifica se ordem da transacao existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool TransacaoOrdemExiste(int id);

        /// <summary>
        /// Verifica se status da transacao existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool TransacaoStatusExiste(int id);
    }
}
