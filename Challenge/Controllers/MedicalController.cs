using Challenge.DTO;
using Challenge.Service;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalController : Controller
    {
        //Inyectar servicio 
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalRecord([FromBody] TMedicalRecordDTO medicalRecordDto)
        {
            var response = await _medicalRecordService.AddMedicalRecord(medicalRecordDto);
            if (response.Success == true)
            {
                return CreatedAtAction(nameof(GetMedicalRecord), new { medicalRecordId = response.Data!.StatusId }, response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterMedicalRecords([FromQuery] MedicalRecordFilterDTO filter)
        {
            var response = await _medicalRecordService.GetFilterMedicalRecords(filter);
            if (response.Success == true)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{medicalRecordId}")]
        public async Task<IActionResult> GetMedicalRecord(int medicalRecordId)
        {
            var response = await _medicalRecordService.GetMedicalRecordById(medicalRecordId);
            if (response.Success == true)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteMedicalRecord([FromBody] DeleteMedicalRecordDTO deleteDto)
        {
            var response = await _medicalRecordService.DeleteMedicalRecord(deleteDto);
            if (response.Success == true)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedicalRecord([FromBody] TMedicalRecordDTO medicalRecordDto)
        {
            var response = await _medicalRecordService.UpdateMedicalRecord(medicalRecordDto);
            if (response.Success == true)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
