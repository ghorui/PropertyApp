using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Properties.Model;
using Properties.Service;
using System.Threading.Tasks;

namespace SamplePropertiesApp.Controllers
{
    [Route("api/Properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IInteractionGetService _interactionGetService;
        private readonly IInteractionPostService _interactionPostService;

        public PropertiesController(IInteractionGetService interactionGetService, IInteractionPostService interactionPostService)
        {
            _interactionGetService = interactionGetService;
            _interactionPostService = interactionPostService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _interactionGetService.GetResponseAsync();
            if (result == null)
            {
                return StatusCode(500, "[Error] GET Properties - Error while fetching the record.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PropertyInfo input)
        {
            var result = await _interactionPostService.GetResponseAsync(input);
            if (!result)
            {
                return StatusCode(500, "[Error] POST Property - Error while saving the record.");
            }
            return Ok(result);
        }
    }
}
