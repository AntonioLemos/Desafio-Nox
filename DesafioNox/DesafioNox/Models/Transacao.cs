using System;

namespace DesafioNox.Models
{
    public class Transacao
    {
        public int IdTransacao { get; set; }
        public decimal ValorTransacao { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int IdOrdemTransacao { get; set; }
        public virtual OrdemTransacao OrdemTransacao { get; set; }
        public DateTime DataHoraTransacao { get; set; }
        public int IdStatusTransacao { get; set; }
        public virtual StatusTransacao StatusTransacao { get; set; }

    }
}
