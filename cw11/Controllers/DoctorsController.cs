using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using cw11.Exceptions;
using cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyklad10Sample.Models;

namespace cw11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsDbService _service;

        public DoctorsController(IDoctorsDbService service)
        {
            _service = service;

            _service.GenerateSampleDate();
        }

        [HttpGet]
        public IActionResult ListDoctors()
        {
            return Ok(_service.ListDoctors());
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetDoctor([FromRoute] int id)
        {
            try
            {
                return Ok(_service.GetDoctor(id));
            } catch (DoctorNotFoundException)
            {
                return NotFound();
            }
        }   
        
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] Doctor doctor)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _service.CreateDoctor(doctor));

            } catch (DoctorValidationException)
            {
                // TODO details
                return BadRequest();
            }
        }
                
        [HttpPatch("{id:int}")]
        public IActionResult UpdateDoctor([FromBody] Doctor doctor, [FromRoute] int id)
        {
            try
            {
                return Ok(_service.UpdateDoctor(id, doctor));

            } catch (DoctorValidationException)
            {
                // TODO details
                return BadRequest();
            }
        }
        
        [HttpPatch("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _service.DeleteDoctor(id);
                return NoContent();

            } catch (DoctorNotFoundException)
            {
                return NotFound();
            }
        }        
    }
}