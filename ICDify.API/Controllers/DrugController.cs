using ICDify.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDify.API.Controllers
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

        /// <summary>
        /// Extracts and maps drug indications to ICD-10 codes
        /// </summary>
        /// <param name="drugName">The drug name (e.g., "Dupixent")</param>
        /// <returns>A list of mapped indications</returns>
        [HttpPost("{drugName}/extract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ExtractMappedIndications(string drugName)
        {
            var indications = await _useCase.ExecuteAsync(drugName);
            return Ok(indications);
        }
    }
}
