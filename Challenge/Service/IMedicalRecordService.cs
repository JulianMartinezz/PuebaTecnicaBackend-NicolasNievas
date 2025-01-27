using Challenge.DTO;

namespace Challenge.Service
{
    /// <summary>
    /// Interfaz que define los métodos para gestionar los registros médicos.
    /// </summary>
    public interface IMedicalRecordService
    {
        /// <summary>
        /// Agrega un nuevo registro médico.
        /// </summary>
        /// <param name="request">DTO que contiene la información del registro médico a agregar.</param>
        /// <returns>Respuesta con el DTO del registro médico agregado.</returns>
        Task<BaseResponse<TMedicalRecordDTO>> AddMedicalRecord(TMedicalRecordDTO request);

        /// <summary>
        /// Recupera registros médicos filtrados según el filtro proporcionado.
        /// </summary>
        /// <param name="filter">Filtro con criterios de búsqueda.</param>
        /// <returns>Respuesta con la lista de registros médicos filtrados.</returns>
        Task<BaseResponse<IEnumerable<TMedicalRecordDTO>>> GetFilterMedicalRecords(MedicalRecordFilterDTO filter);

        /// <summary>
        /// Obtiene un registro médico por su identificador.
        /// </summary>
        /// <param name="medicalRecordId">Identificador del registro médico.</param>
        /// <returns>Respuesta con el DTO del registro médico.</returns>
        Task<BaseResponse<TMedicalRecordDTO>> GetMedicalRecordById(int medicalRecordId);

        /// <summary>
        /// Elimina un registro médico de acuerdo con los datos de eliminación proporcionados.
        /// </summary>
        /// <param name="deleteDto">DTO que contiene los datos para la eliminación del registro médico.</param>
        /// <returns>Respuesta con el DTO del registro médico eliminado.</returns>
        Task<BaseResponse<TMedicalRecordDTO>> DeleteMedicalRecord(DeleteMedicalRecordDTO deleteDto);

        /// <summary>
        /// Actualiza un registro médico con los nuevos datos proporcionados.
        /// </summary>
        /// <param name="dto">DTO que contiene los nuevos datos para actualizar el registro médico.</param>
        /// <returns>Respuesta con el DTO del registro médico actualizado.</returns>
        Task<BaseResponse<TMedicalRecordDTO>> UpdateMedicalRecord(TMedicalRecordDTO dto);
    }
}
