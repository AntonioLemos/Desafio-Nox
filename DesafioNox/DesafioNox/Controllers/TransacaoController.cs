using DesafioNox.Helper;
using DesafioNox.Models;
using DesafioNox.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DesafioNox.Controllers
{
    public class TransacaoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult AbrirTransacao()
        {
            TransacaoModelView viewModel = new TransacaoModelView();
            ViewBag.Title = "Abrir Transação";

            return View(viewModel);
        }

        public ActionResult Transacoes()
        {
            ViewBag.Title = "Transações";

            return View();
        }

        public async Task<JsonResult> PostTransacao(TransacaoModelView viewModel)
        {
            MensagemResposta<TransacaoModelView> responseMessage = 
                await HttpClienteHelper<MensagemResposta<TransacaoModelView>>
                .PostRequest("transacao/novatransacao", viewModel);

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetTransacao(int userId, string dataInicio = "", string dataFinal = "")
        {
            MensagemResposta<IEnumerable<Transacao>> responseMessage =
                await HttpClienteHelper<MensagemResposta<IEnumerable<Transacao>>>
                .PostRequest("transacao/buscatransacoes", string.Concat(userId, ";", dataInicio, ";", dataFinal));

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> PostStatus(int userId, int statusTransacao, string[] listIds)
        {
            var test = string.Concat(userId, ";", statusTransacao, ";", string.Join(";", listIds));
            MensagemResposta<IEnumerable<Transacao>> responseMessage = 
                await HttpClienteHelper<MensagemResposta<IEnumerable<Transacao>>>
                .PostRequest("transacao/alterarstatus", string.Concat(userId, ";", statusTransacao, ";", string.Join(";", listIds)));

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}
