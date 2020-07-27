using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using IRAnonymized.Assignment.WebApi.Services.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;
        
        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        /// <summary>
        /// Import a file.
        /// </summary>
        /// <param name="file">File to be imported.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([Required] IFormFile file)
        {
            var importResponse = await _importService.ImportDataFromFileAsync(file);

            if(importResponse.Status == ImportFileStatus.SentToBeProcessed)
            {
                return Accepted();
            }

            return BadRequest();
        }
    }
}