using DesafioNox.Helper;
using DesafioNoxApi.Repositorios.Interfaces;
using DesafioNoxApi.Servicos.Interfaces;
using DesafioNoxModels;
using System.Threading.Tasks;

namespace DesafioNoxApi.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        protected readonly IUsuarioRepositorio _usuarioRepositorio;
        protected readonly ITransacaoRepositorio _transacaoRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, 
            ITransacaoRepositorio transacaoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _transacaoRepositorio = transacaoRepositorio;
        }

        public async Task<dynamic> GetUsuario()
        {
            
            dynamic usuarioSimulacao = _usuarioRepositorio.GetUsuario();
            var usuario = await _usuarioRepositorio.GetUsuarioId(usuarioSimulacao.IdUsuario);

            var result = new MensagemResposta<Usuario>("OK", usuario);

            return result;            
        }
    }
}