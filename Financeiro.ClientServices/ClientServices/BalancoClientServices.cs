using Financeiro.ClientServices.Interfaces;
using Financeiro.Util.Configuracao;
using Financeiro.Util.Helpers;
using Financeiro.Util.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Financeiro.ClientServices.ClientServices
{
    public class BalancoClientServices : IBalancoClientService
    {
        private readonly HttpClient _httpClient = default;
        private readonly IOptions<CustomConfiguration> _customConfiguration;
        public BalancoClientServices(HttpClient httpClient, IOptions<CustomConfiguration> customConfiguration)
        {
            _httpClient = httpClient;
            _customConfiguration = customConfiguration;
        }

        public void GerarBalancoDiario()
        {
            try
            {
                var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string> { });

                using (var response = _httpClient.PostAsync(ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "BalancoDiario"), new StringContent("")))
                {
                    response.Result.Content.ReadAsStringAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BalancoMensalModel> GetBalancoMensal(int? ano, int? mes)
        {
            try
            {
                using (var response = _httpClient.GetAsync(ClientServiceHelpers.ConfigurarUrl(_customConfiguration, "BalancoMensal")))
                {
                    var retornoApi = response.Result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<BalancoMensalModel>>(retornoApi.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
