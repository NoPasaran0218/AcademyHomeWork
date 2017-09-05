using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatOrderingService.Service
{
    public class CodeGeneratorService:ICodeGeneratorService
    {
        private HttpClient _httpClient;

        private readonly CodeGeneratorOptions _options;
        public CodeGeneratorService()
        {
            _httpClient = new HttpClient();
        }
        public CodeGeneratorService(IOptions<CodeGeneratorOptions> options)
        {
            _options = options.Value;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetCode(int id)
        {
            var Url = new Uri(_options.CodeGeneratorUrl+id.ToString());
            var response = await _httpClient.GetAsync(Url);
            var responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }
    }

    public interface ICodeGeneratorService
    {
        Task<string> GetCode(int id);
    }
}


