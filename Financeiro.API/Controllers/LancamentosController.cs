using AutoMapper;
using Financeiro.Util.Model;
using Financeiro.Dominio;
using Financeiro.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Financeiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : FinanceiroControllerBase
    {
        private readonly ILancamentoServices _lancamentoServices;
        private readonly IMapper _mapper;

        public LancamentosController(ILancamentoServices lancamentoServices, IMapper mapper)
        {
            _lancamentoServices = lancamentoServices;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult InserirLancamentoFinanceiro([FromBody] LancamentoFinanceiroApiModel model)
        {
            try
            {
                _lancamentoServices.InserirLancamento(_mapper.Map<LancamentoFinanceiroApiModel, LancamentoFinanceiro>(model));

                return Ok();
            }
            catch (Exception ex)
            {
                return Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult AtualizarLancamentoFinanceiro([FromBody] LancamentoFinanceiroApiUpdateModel model)
        {
            try
            {
                _lancamentoServices.AtualizarLancamento(_mapper.Map<LancamentoFinanceiroApiUpdateModel, LancamentoFinanceiro>(model));

                return Ok();
            }
            catch (Exception ex)
            {
                return Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("FiltraLista")]
        public ActionResult FiltrarLancamentoFinanceiro([FromQuery] LancamentoFinanceiroFiltro model)
        {
            try
            {
                var listaLancamentos = _lancamentoServices.BuscarLancamentoFinanceiro(model.DataLancamento, model.TipoLancamento, model.Conciliado);

                if (!listaLancamentos.Any())
                    return NoContent();

                return Ok(_mapper.Map<List<LancamentoFinanceiro>, List<LancamentoFinanceiroModel>>(listaLancamentos));
            }
            catch (Exception ex)
            {
                return Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _lancamentoServices.ExcluirLancamentoFinanceiro(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult BuscarLancamentoFinanceiroPorId(int id)
        {
            try
            {
                var lancamento = _lancamentoServices.BuscarLancamentoFinanceiroPorId(id);

                if (lancamento == null)
                    return NotFound();

                return Ok(_mapper.Map<LancamentoFinanceiro, LancamentoFinanceiroModel>(lancamento));
            }
            catch (Exception ex)
            {
                return Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
