using Challenge.Models;

namespace Challenge.Repository
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad <see cref="MedicalRecordType"/>.
    /// </summary>
    public interface IMedicalRecordTypeRepository
    {
        /// <summary>
        /// Obtiene un objeto <see cref="MedicalRecordType"/> por su ID.
        /// </summary>
        /// <param name="medicalRecordTypeId">ID del tipo de registro médico a buscar.</param>
        /// <returns>Un objeto <see cref="MedicalRecordType"/> si se encuentra, o null si no existe.</returns>
        Task<MedicalRecordType?> GetById(int? medicalRecordTypeId);
    }
}
