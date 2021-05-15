using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DesafioNox.Helper;
using DesafioNoxApi.Servicos.Interfaces;
using DesafioNoxModels;

namespace DesafioNoxApi.Controllers
{
    [RoutePrefix("api/transacao")]
    public class TransacaoController : ApiController
    {
        protected readonly ITransacaoServico _transacaoServico;
        public TransacaoController(ITransacaoServico transacaoServico)
        {
            _transacaoServico = transacaoServico;
        }

        [HttpPost]
        [Route("novatransacao")]
        public async Task<HttpResponseMessage> PostTransacao([FromBody]Transacao transacao)
        {
            try
            {
                MensagemResposta<Transacao> result = await _transacaoServico.PostTransacao(transacao);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                var transacaoError = new Transacao();
                var result = new MensagemResposta<Transacao>(string.Concat("Erro ao salvar transação!", ex.ToString()), transacaoError);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpPost]
        [Route("buscatransacoes")]
        public async Task<HttpResponseMessage> GetTransacoesPesquisa([FromBody] string dataBusca)
        {
            try
            {
                MensagemResposta<IEnumerable<Transacao>> result = await _transacaoServico.GetTransacoesPesquisa(dataBusca);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                var transacaoErrorList = new List<Transacao>();
                var result = new MensagemResposta<List<Transacao>>(string.Concat("Erro ao buscar transações!", ex.ToString()), transacaoErrorList);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpPost]
        [Route("alterarstatus")]
        public async Task<HttpResponseMessage> PostStatus([FromBody] string dataStatus)
        {
            try
            {
                MensagemResposta<IEnumerable<Transacao>> result = await _transacaoServico.PostStatus(dataStatus);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                var transacaoError = new Transacao();
                var result = new MensagemResposta<Transacao>(string.Concat("Erro ao alterar status transações!", ex.ToString()), transacaoError);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

    }
}