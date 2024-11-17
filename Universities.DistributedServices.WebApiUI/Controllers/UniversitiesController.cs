using Microsoft.AspNetCore.Mvc;
using Universities.Infrastructure.Contracts.EntitiesDB;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Universities.DistributedServices.WebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {

        private readonly IUniversitiesService _universitiesService;

        public UniversitiesController(IUniversitiesService universitiesService)
        {
            _universitiesService = universitiesService;
        }

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            ListAllRsDto result = _universitiesService.ListAllAsync();
            // the only possible error would be db connection error,
            // but if it exists, it would have been recognized during Server initialization,
            // and in that case execution will throw an Exception and this code won't be reached.
            // so if this code is executed, we can assure that DB Connection will be ok.
            // Therefore, this endpoint has no error to be taken into account
            return Ok(result.data);
        }

        [HttpGet("FilterByName")]
        public IActionResult FilterByName(string name)
        {
            List<UniversityNameAndWebpageListDto> result = _universitiesService.FilterByName(name);
            return Ok(result);
        }

        [HttpGet("FilterByAlphaTwoCode")]
        public IActionResult FilterByAlphaTwoCode(string alphaTwoCode)
        {
            List<UniversityNameDto> result = _universitiesService.FilterByAlphaTwoCode(alphaTwoCode);
            return Ok(result);
        }


    }
}
