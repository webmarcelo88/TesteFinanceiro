﻿namespace Financeiro.Util.Model.ViewModel
{
    public class LancamentoFinanceiroViewModel
    {
        public long Id { get; set; }
        public string DataHoraLancamento { get; set; }
        public string Valor { get; set; }
        public string TipoLancamento { get; set; }
        public string Conciliado { get; set; }
    }
}
