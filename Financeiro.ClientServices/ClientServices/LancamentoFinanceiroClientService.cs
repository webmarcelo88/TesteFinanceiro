using Financeiro.ClientServices.Interfaces;
using Financeiro.Util.Configuracao;
using Financeiro.Util.Helpers;
using Financeiro.Util.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Financeiro.ClientServices.ClientServices
{
    public class LancamentoFinanceiroClientService : ILancamentoFinanceiroClientServices
    {
        private readonly HttpClient _httpClient = default;
        private readonly IOptions<CustomConfiguration> _customConfiguration;

        public LancamentoFinanceiroClientService(HttpClient httpClient, IOptions<CustomConfiguration> customConfiguration)
        {
            _httpClient = httpClient;
            _customConfiguration = customConfiguration;

        }

        public void AtualizarLancamentoFinanceiro(LancamentoFinanceiroApiUpdateModel model)
        {
            try
            {
                var dados = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using (var response = _httpClient.PutAsync(ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "PutLancamentoFinanceiro"), dados))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExcluirLancamentoFinanceiro(int id)
        {
            try
            {
                var urlComParametros = $"{ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "DeleteLancamentoFinanceiro")}/{id}";

                using (var response = _httpClient.DeleteAsync(urlComParametros))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LancamentoFinanceiroModel> FiltrarLancamentosFinanceiro(LancamentoFinanceiroFiltro filtro)
        {
            try
            {
                var urlComParametros = $"{ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "GetListaLancamentoFinanceiro")}?" +
                                       $"DataLancamento={filtro.DataLancamento}&" +
                                       $"TipoLancamento={filtro.TipoLancamento}&" +
                                       $"Conciliado={filtro.Conciliado}";

                using (var response = _httpClient.GetAsync(urlComParametros))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<LancamentoFinanceiroModel>>(retornoApi.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LancamentoFinanceiroModel GetLancamentoFinanceiro(int id)
        {
            try
            {
                var urlComParametros = $"{ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "GetLancamentoFinanceiro")}/{id}";

                using (var response = _httpClient.GetAsync(urlComParametros))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<LancamentoFinanceiroModel>(retornoApi.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InserirLancamentoFinaneiro(LancamentoFinanceiroApiModel model)
        {
            try
            {
                var dados = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using (var response = _httpClient.PostAsync(ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "PostLancamentoFinanceiro"), dados))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
