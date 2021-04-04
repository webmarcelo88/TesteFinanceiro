using System;

namespace Financeiro.Util.Model
{
    public class LancamentoFinanceiroFiltro
    {
        public DateTime? DataLancamento { get; set; }
        public int? TipoLancamento { get; set; }
        public bool? Conciliado { get; set; }
    }
}
