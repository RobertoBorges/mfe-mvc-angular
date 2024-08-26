using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MasterHub.Pages
{
    public class RemoteApp1Model(HttpClient httpClient, ILogger<IndexModel> logger) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;

        private readonly ILogger<IndexModel> _logger;

        public string HtmlContent { get; private set; }

        public void OnGet()
        {
            var targetUri = $"https://localhost:7063/";

            var response = _httpClient.GetAsync(targetUri);

            if (response.Result.IsSuccessStatusCode)
            {
                var content = response.Result.Content.ReadAsStringAsync();
                HtmlContent = Content(content.Result, "text/html").Content;
            }
        }
    }
}
