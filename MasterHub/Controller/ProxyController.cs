using Microsoft.AspNetCore.Mvc;

namespace MasterHub.Controller
{
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProxyController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("proxy/{*url}")]
        public async Task<IActionResult> ProxyToMvcApp(string url)
        {
            var targetUri = "";

            if (url.Contains("https://"))
            {
                targetUri = url;
            }
            else if (url.Contains("RemoteApp2"))
            {
                targetUri = $"https://localhost:7063/{url}";
            }

            var response = await _httpClient.GetAsync(targetUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "text/html");
            }

            return StatusCode((int)response.StatusCode);
        }
    }

}
