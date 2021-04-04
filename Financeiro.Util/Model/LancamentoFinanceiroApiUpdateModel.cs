using Financeiro.Util.Enum;

namespace Financeiro.Util.Model
{
    public class LancamentoFinanceiroApiUpdateModel
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public TipoLancamentoEnum TipoLancamento { get; set; }
    }
}
