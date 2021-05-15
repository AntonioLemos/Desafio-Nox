using DesafioNox.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioNox.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public decimal SaldoUsuarioReal { get; set; }
        public decimal SaldoUsuario { get; set; }
    }
}