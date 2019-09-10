using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Domain.Models;

namespace Vaccination.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public static List<Patient> list = new List<Patient>()
            {
                new Patient()
                {
                    Id =0,
                    Birthday = new DateTime(1990, 1, 1),
                    Gender = "Мужской",
                    Name = "Дмитрий",
                    Patronomic = "Степанович",
                    Soname = "Тереякин",
                    Snils = "1231-12312-123123"
                },
                new Patient()
                {
                    Id =1,
                    Birthday = new DateTime(1985, 2, 2),
                    Gender = "Мужской",
                    Name = "Игорь",
                    Patronomic = "Николаевич",
                    Soname = "Снупдогов",
                    Snils = "6666-77777-459871"
                }
            };

        public static List<Vaccine> vaccines = new List<Vaccine>()
        {
          new Vaccine() { Id = -1, PatientId = -1}
        };

        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return list;
        }

        [HttpGet("{id}")]
        public async Task<Patient> Get(int id)
        {
            return list.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Patient patient)
        {
            patient.Id = list.Max(x => x.Id) + 1;
            list.Add(patient);
            return Created("", patient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] Patient patient)
        {
            list[list.FindIndex(x => x.Id == id)] = patient;
            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            list.RemoveAll(x => x.Id == id);
            return Ok();
        }

        [HttpGet("{patientId}/vaccine")]
        public async Task<ActionResult> GetVaccines(int patientId)
        {
            return Ok(vaccines.Where(x => x.PatientId == patientId));
        }

        [HttpGet("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> GetVaccine(int patientId, int vaccineId)
        {
            return Ok(vaccines.FirstOrDefault(x => x.Id == vaccineId && x.PatientId == patientId));
        }

        [HttpPost("{patientId}/vaccine")]
        public async Task<ActionResult> AddVaccine(int patientId, [FromBody]Vaccine vaccine)
        {
            vaccine.Id = (vaccines?.Max(x => x.Id) ?? 0) + 1;
            vaccine.PatientId = patientId;
            vaccines.Add(vaccine);
            return Ok(vaccine);
        }

        [HttpPut("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> Put(int patientId, int vaccineId, [FromBody] Vaccine vaccine)
        {
            vaccines[vaccines.FindIndex(x => x.Id == vaccineId && x.PatientId == patientId)] = vaccine;
            return Ok(vaccine);
        }

        [HttpDelete("{patientId}/vaccine/{vaccineId}")]
        public async Task<ActionResult> Delete(int patientId, int vaccineId)
        {
            vaccines.RemoveAll(x => x.Id == vaccineId && x.PatientId == patientId);
            return Ok();
        }
    }
}