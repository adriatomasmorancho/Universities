using Microsoft.AspNetCore.Mvc;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Universities.DistributedServices.WebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationsController : ControllerBase
    {

        private readonly IUniversitiesService _universitiesService;

        public MigrationsController(IUniversitiesService universitiesService)
        {
            _universitiesService = universitiesService;
        }

        [HttpGet("Universities")]
        public async Task<IActionResult> MigrateUniversities()
        {
            MigrateAllRsDto result = await _universitiesService.MigrateAllAsync();
            if (result.errors != null)
            {
                return BadRequest(
                    ErrorMessageMapperHelper.MapListAllErrorsEnumToStringMessages(
                        result.errors
                    )
                );
            }

            return Ok();
        }


        



    }
}
