using System;

namespace DesafioNox.Models.ModelView
{
    public class TransacaoModelView
    {
        public TransacaoModelView()
        {
            this.DataHoraTransacao = DateTime.Now.ToLocalTime();
        }
        public int IdUsuario { get; set; }

        public string ValorTransacao { get; set; }

        public string IdOrdemTransacao { get; set; }

        public DateTime DataHoraTransacao { get; set; }
    }
}