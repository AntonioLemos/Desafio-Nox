using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioNox.Helper
{
    public class MensagemResposta<T> where T : class
    {
        public string Mensagem { get; set; }
        public T Resposta { get; set; }
    }
}