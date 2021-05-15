using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioNoxApi.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task<dynamic> GetUsuario();
    }
}
