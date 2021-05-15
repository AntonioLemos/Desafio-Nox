using DesafioNox.Helper;
using DesafioNoxModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioNoxApi.Servicos.Interfaces
{
    public interface ITransacaoServico
    {
        /// <summary>
        /// Valida e Adiciona a transacao no banco.
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        Task<dynamic> PostTransacao(Transacao transacao);

        /// <summary>
        /// Busca transacoes. params =  int idUsuario;datetime datainicial;datetime datafinal
        /// </summary>
        /// <param name="dataBusca">string concatenada int idUsuario;datetime datainicial;datetime datafinal</param>
        /// <returns></returns>
        Task<MensagemResposta<IEnumerable<Transacao>>> GetTransacoesPesquisa(string dataBusca);

        /// <summary>
        /// Valida e Altera status da transacao. Retorna lista atualizada. params = int idUsuario;int novo status;idtransacao;idtransacao...
        /// </summary>
        /// <param name="dataStatus">string concatenada int idUsuario;int novo status;idtransacao;idtransacao...</param>
        /// <returns></returns>
        Task<MensagemResposta<IEnumerable<Transacao>>> PostStatus(string dataStatus);
    }
}
