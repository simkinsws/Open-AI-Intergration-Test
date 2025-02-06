using KevinApp.Models;
using KevinApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KevinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAiController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;

        public OpenAiController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpPost]
        public IActionResult AnalyzeText(OpenAiRequestModel request)
        {
            var analyzeText = _openAiService.AnalyzeText(request!.Text!);
            return Ok(analyzeText);
        }
    }
}
