using Challenge.DTO;
using Challenge.Models;

namespace Challenge.Repository
{
    /// <summary>
    /// Interfaz que define las operaciones para la gestión de registros médicos en la base de datos.
    /// </summary>
    public interface IMedicalRecordRepository
    {
        /// <summary>
        /// Agrega un nuevo registro médico a la base de datos.
        /// </summary>
        /// <param name="medicalRecord">El objeto <see cref="TMedicalRecord"/> que se agregará.</param>
        /// <returns>El registro médico agregado.</returns>
        Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord);

        /// <summary>
        /// Obtiene un registro médico por su ID.
        /// </summary>
        /// <param name="medicalRecordId">El ID del registro médico a buscar.</param>
        /// <returns>El registro médico encontrado o null si no existe.</returns>
        Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId);

        /// <summary>
        /// Obtiene una lista de registros médicos filtrados según los criterios especificados.
        /// </summary>
        /// <param name="filter">Objeto <see cref="MedicalRecordFilterDTO"/> con los filtros aplicados.</param>
        /// <returns>Una tupla que contiene la lista de registros médicos filtrados y el conteo total.</returns>
        Task<(List<TMedicalRecord> Medical, int TotalCount)> GetFilterMedicalRecords(MedicalRecordFilterDTO filter);

        /// <summary>
        /// Actualiza un registro médico existente en la base de datos.
        /// </summary>
        /// <param name="medicalRecord">El objeto <see cref="TMedicalRecord"/> con los datos actualizados.</param>
        /// <returns>El registro médico actualizado o null si no se pudo actualizar.</returns>
        Task<TMedicalRecord?> UpdateMedicalRecord(TMedicalRecord medicalRecord);
    }
}
