﻿using Financeiro.Util.Model;
using Financeiro.Util.Model.ViewModel;
using Financeiro.Dominio;

namespace Financeiro.Util.ConfiguracaoAutoMapper
{
    public class AutoMapperModelToService : AutoMapper.Profile
    {
        public AutoMapperModelToService()
        {
            CreateMap<LancamentoFinanceiroApiModel, LancamentoFinanceiro>()
                .ForPath(_ => _.TipoLancamento.ID, opt => opt.MapFrom(src => (int)src.TipoLancamento));

            CreateMap<LancamentoFinanceiroApiUpdateModel, LancamentoFinanceiro>()
              .ForPath(_ => _.TipoLancamento.ID, opt => opt.MapFrom(src => (int)src.TipoLancamento));

            CreateMap<LancamentoFinanceiroInserirViewModel, LancamentoFinanceiroApiModel>()
                .ForMember(_ => _.TipoLancamento, opt => opt.MapFrom(src => src.TipoLancamento ? Enum.TipoLancamentoEnum.Credito : Enum.TipoLancamentoEnum.Debito));

            CreateMap<LancamentoFinanceiroInserirViewModel, LancamentoFinanceiroApiUpdateModel>()
            .ForPath(_ => _.TipoLancamento, opt => opt.MapFrom(src => src.TipoLancamento ? Enum.TipoLancamentoEnum.Credito : Enum.TipoLancamentoEnum.Debito));

        }
    }
}
