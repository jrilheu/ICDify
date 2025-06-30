using ICDify.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ICDify.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly ExtractAndMapIndicationsUseCase _useCase;

        public DrugController(ExtractAndMapIndicationsUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("{drugName}/extract")]
        public async Task<IActionResult> ExtractMappedIndications(string drugName)
        {
            var indications = await _useCase.ExecuteAsync(drugName);
            return Ok(indications);
        }
    }
}
