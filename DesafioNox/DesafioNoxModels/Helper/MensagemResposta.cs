using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioNox.Helper
{
    public class MensagemResposta<T> where T : class
    {
        public MensagemResposta(string mensagem, T resposta = null)
        {
            this.Mensagem = mensagem;
            this.Resposta = resposta;       
        }
        public string Mensagem { get; set; }
        public T Resposta { get; set; }
    }
}