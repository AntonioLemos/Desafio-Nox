using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioNoxApi.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<dynamic> GetUsuarioId(int idUsuario);

        dynamic GetUsuario();
    }
}
