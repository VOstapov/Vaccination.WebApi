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
    public class MedicationController : ControllerBase
    {
        private readonly IService<MedicationDto> medicationService;

        public MedicationController(IService<MedicationDto> medicationService)
        {
            this.medicationService = medicationService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var res = await medicationService.GetAllAsync();
            return Ok(res);
        }
    }
}