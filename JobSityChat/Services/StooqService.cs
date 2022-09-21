using JobSityChat.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Services
{
    public interface IStooqService {
        Task<string> GetStoqQuote(string stockCode);
    }
    public class StooqService : Service, IStooqService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public StooqService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.StoqUrl);
        }
        public async Task<string> GetStoqQuote(string stockCode)
        {
                var response = await _httpClient.GetAsync(requestUri: $"?s={stockCode}&f=sd2t2ohlcv&h&e=csv", HttpCompletionOption.ResponseHeadersRead);
                using var resultStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(resultStream, Encoding.UTF8);
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (line.ToUpper().Contains(stockCode.ToUpper()))
                    {
                        string[] quoteValue = line.Split(",");
                        return $"{stockCode.ToUpper()} quote is ${quoteValue[6]} per share" ;
                    }
                }
                return string.Empty;
        }
    }
}
