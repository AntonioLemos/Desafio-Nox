using DesafioNoxApi.Repositorios.Interfaces;
using DesafioNoxData.DataContexts;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioNoxApi.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private DesafioNoxDataContext db = new DesafioNoxDataContext();

        public async Task<dynamic> GetUsuarioId(int idUsuario)
        {
            try
            {
                var result = db.Usuario.SqlQuery(
                        @"SELECT
                            usuario.IdUsuario,
                            usuario.EmailUsuario,
                            SaldoUsuarioReal = usuario.SaldoUsuarioReal, 
                            SaldoUsuario = (usuario.SaldoUsuarioReal - (SELECT ISNULL(SUM(ValorTransacao), 0) 
                                                                        FROM [tbTransacao] Transacao 
                                                                    WHERE Transacao.IdUsuario = @idUsuario
                                                                    AND Transacao.IdStatusTransacao = (SELECT IdStatusTransacao 
                                                                                                         FROM [tbStatusTransacao] 
                                                                                                       WHERE StatusTransacaoDescricao = 'ABERTA')))                          
                        FROM 
                            [tbUsuario] usuario
                        WHERE
                            usuario.IdUsuario = @idUsuario", new SqlParameter("@idUsuario", idUsuario)).FirstOrDefault();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public dynamic GetUsuario()
        {
            try
            {
                var result = db.Usuario.FirstOrDefault();
                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}