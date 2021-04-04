using Financeiro.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Financeiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancoController : FinanceiroControllerBase
    {
        private readonly IBalancoServices _balancoServices;

        public BalancoController(IBalancoServices balancoServices)
        {
            _balancoServices = balancoServices;
        }

        [HttpPost]
        [Route("GerarBalancoDiario")]
        public ActionResult GerarBalancoDiario()
        {
            try
            {

                _balancoServices.GerarBalancoDiario();

                return Ok();
            }
            catch (Exception ex)
            {
                return Result(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBalancoMensal")]
        public ActionResult GetBalancoMensal([FromQuery] DateTime? ano, DateTime? mesParametro)
        {
            try
            {
                var retornoBalancoMensal = _balancoServices.BuscarBalancoMensal(ano, mesParametro);

                if (!retornoBalancoMensal.Any())
                    return NoContent();

                return Ok(retornoBalancoMensal);
            }
            catch (Exception ex)
            {
                return Result(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}