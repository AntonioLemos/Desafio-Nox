using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioNox.Helper
{
    public static class TransacaoGlobals
    {
        public const Int32 STATUS_ABERTO = 1;
        public const Int32 STATUS_EXECUTADO = 2;
        public const Int32 STATUS_REJEITADO = 3; 

        public const Int32 ORDEM_COMPRA = 1;
        public const Int32 ORDEM_VENDA = 2;
    }
}