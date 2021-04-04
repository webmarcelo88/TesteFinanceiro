using Financeiro.Util.Enum;
using System;

namespace Financeiro.Util.Model
{
    public class LancamentoFinanceiroApiModel
    {

        public double Valor { get; set; }
        public TipoLancamentoEnum TipoLancamento { get; set; }
    }
}
