using DesafioNox.Helper;
using DesafioNox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DesafioNox.Controllers
{
    public class UsuarioController : Controller
    {
        public async Task<JsonResult> GetUsuario()
        {
            MensagemResposta<Usuario> responseMessage = await HttpClienteHelper<MensagemResposta<Usuario>>.Get("usuario/usuario");

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}