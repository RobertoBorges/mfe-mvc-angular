using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MasterHub.Pages
{
    public class RemoteApp2Model : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;

        public string HtmlContent { get; private set; }

        public RemoteApp2Model(HttpClient httpClient, ILogger<IndexModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            HtmlContent = string.Empty;
        }

        public void OnGet()
        {
            var targetUri = $"https://localhost:7065/";

            var response = _httpClient.GetAsync(targetUri);

            if (response.Result.IsSuccessStatusCode)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                HtmlContent = Content(content.Result, "text/html").Content;
            }
        }
    }
}
