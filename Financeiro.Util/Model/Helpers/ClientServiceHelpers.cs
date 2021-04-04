using Financeiro.Util.Configuracao;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Financeiro.Util.Helpers
{
    public static class ClientServiceHelpers
    {
        public static string ConfigurarUrl(IOptions<CustomConfiguration> customConfiguration, string serviceName)
        {
            return string.Concat(customConfiguration.Value.UrlBaseAPI, customConfiguration.Value.EndPoints.Where(_ => _.ApiName.Equals(serviceName)).FirstOrDefault().ApiPath);
        }
    }
}
