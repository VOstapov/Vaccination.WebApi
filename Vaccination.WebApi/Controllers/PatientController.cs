﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Models;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IService<PatientDto> patientService;
        private readonly IService<VaccineDto> vaccineService;
        private readonly ISearchService searchService;

        public PatientController(
            IService<PatientDto> patientService,
            IService<VaccineDto> vaccineService,
            ISearchService searchService)
        {
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.vaccineService = vaccineService ?? throw new ArgumentNullException(nameof(vaccineService));
            this.searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string searchString = "")
        {
            var res = await searchService.FindPatientsAsync(searchString);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await patientService.GetAsync(x => x.Id == id, x => x.Gender);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PatientDto patient)
        {
            var res = await patientService.AddAsync(patient);
            return Created($"api/patient/{res.Id}", res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PatientDto patient)
        {
            patient.Id = id;
            var res = await patientService.UpdateAsync(patient, x => x.Id == id);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await patientService.DeleteAsync(x => x.Id == id);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        [HttpGet("{patientId}/vaccine")]
        public async Task<ActionResult> GetVaccines(int patientId)
        {
            var res = await vaccineService.GetAllAsync(x => x.PatientId == patientId, x => x.Medication);
            return Ok(res);
        }

        [HttpGet("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> GetVaccine(int patientId, int vaccineId)
        {
            var res = await vaccineService.GetAsync(
                x => x.PatientId == patientId && x.Id == vaccineId,
                x => x.Medication);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        [HttpPost("{patientId}/vaccine")]
        public async Task<ActionResult> AddVaccine(int patientId, [FromBody]VaccineDto vaccine)
        {
            vaccine.PatientId = patientId;
            var res = await vaccineService.AddAsync(vaccine);
            return Created($"/api/patient/{patientId}/vaccine/{res.Id}", res);
        }

        [HttpPut("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> Put(int patientId, int vaccineId, [FromBody] VaccineDto vaccine)
        {
            // Вдруг забудут =)
            vaccine.Id = vaccineId;
            vaccine.PatientId = patientId;

            var res = await vaccineService.UpdateAsync(
                vaccine,
                x => x.Id == vaccine.Id && x.PatientId == vaccine.PatientId);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        [HttpDelete("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> Delete(int patientId, int vaccineId)
        {
            var res = await vaccineService.DeleteAsync(x => x.PatientId == patientId && x.Id == vaccineId);
            return CheckForNullAndReturnOkOrNotFound(res);
        }

        private ActionResult CheckForNullAndReturnOkOrNotFound(object res)
        {
            // По соглашениям rest, если одиночный ресурс был найден
            // и обработан, вернем ОК.
            // Если ресурс не найден, то вернем 404.
            if (res != null)
            {
                return Ok(res);
            }

            return NotFound();
        }
    }
}