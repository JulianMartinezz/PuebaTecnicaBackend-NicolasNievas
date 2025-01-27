using Challenge.DTO;
using Challenge.Service;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Controllers
{
    /// <summary>
    /// Controlador para gestionar los registros médicos.
    /// </summary>
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

        /// <summary>
        /// Agregar un nuevo registro médico
        /// </summary>
        /// <param name="medicalRecordDto">DTO con la información del registro médico.</param>
        /// <returns>
        /// - Si la operación es exitosa, devuelve un objeto con los detalles del registro médico creado y un código HTTP 201 (Created).
        /// - Si la operación falla, devuelve un mensaje de error y un código HTTP 400 (Bad Request).
        /// </returns>
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

        /// <summary>
        /// Obtiene los registros médicos filtrados según criterios específicos.
        /// </summary>
        /// <param name="filter">DTO con los criterios de filtrado.</param>
        /// <returns>Lista de registros médicos que cumplen con el filtro.</returns>
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

        /// <summary>
        /// Obtiene un registro médico específico por su ID.
        /// </summary>
        /// <param name="medicalRecordId">ID del registro médico.</param>
        /// <returns>El registro médico solicitado.</returns>
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

        /// <summary>
        /// Elimina un registro médico de manera lógica.
        /// </summary>
        /// <param name="deleteDto">DTO con la información necesaria para eliminar el registro.</param>
        /// <returns>
        /// - Si la operación es exitosa, devuelve una confirmación de la eliminación lógica y un código HTTP 200 (OK)
        /// - Si el registro no se encuentra, devuelve un mensaje de error y un código HTTP 404 (Not Found).
        /// </returns>
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

        /// <summary>
        /// Actualiza un registro médico existente.
        /// </summary>
        /// <param name="medicalRecordDto">DTO con la información actualizada del registro médico.</param>
        /// <returns>
        /// - Si la operación es exitosa, devuelve el registro médico actualizado y un código HTTP 200 (OK).
        /// - Si el registro no se encuentra, devuelve un mensaje de error y un código HTTP 404 (Not Found).
        /// </returns>
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
