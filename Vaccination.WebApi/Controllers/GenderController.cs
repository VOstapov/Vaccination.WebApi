using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IService<GenderDto> genderService;

        public GenderController(IService<GenderDto> genderService)
        {
            this.genderService = genderService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var res = await genderService.GetAllAsync();
            return Ok(res);
        }
    }
}