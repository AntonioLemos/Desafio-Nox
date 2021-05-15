using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DesafioNox.Helper;
using DesafioNoxApi.Servicos.Interfaces;
using DesafioNoxModels;

namespace DesafioNoxApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuariosController : ApiController
    {
        protected readonly IUsuarioServico _usuarioServico;
        public UsuariosController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        [Route("usuario")]
        public async Task<HttpResponseMessage> GetUsuario()
        {
            try
            {
                MensagemResposta<Usuario> result = await _usuarioServico.GetUsuario();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                var user = new Usuario();
                var result = new MensagemResposta<Usuario>(string.Concat("Erro ao recuperar usuario!", ex.ToString()), user);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

    }
}
