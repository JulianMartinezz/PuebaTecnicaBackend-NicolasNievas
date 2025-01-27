using Challenge.Data;
using Challenge.Models;

namespace Challenge.Repository.imp
{
    /// <summary>
    /// Implementación del repositorio para la entidad <see cref="MedicalRecordType"/>.
    /// </summary>
    public class MedicalRecordTypeRepositoryImp : IMedicalRecordTypeRepository
    {
        /// <summary>
        /// Contexto de base de datos utilizado para acceder a los datos.
        /// </summary>
        private readonly MedicalContext _context;

        /// <summary>
        /// Constructor de la clase <see cref="MedicalRecordTypeRepositoryImp"/>.
        /// </summary>
        /// <param name="medicalContext">Contexto de base de datos inyectado.</param>
        public MedicalRecordTypeRepositoryImp(MedicalContext medicalContext) {
            _context = medicalContext;
        }

        /// <summary>
        /// Obtiene un objeto <see cref="MedicalRecordType"/> por su ID.
        /// </summary>
        /// <param name="medicalRecordTypeId">ID del tipo de registro médico a buscar.</param>
        /// <returns>Un objeto <see cref="MedicalRecordType"/> si se encuentra, o null si no existe.</returns>
        public async Task<MedicalRecordType?> GetById(int? medicalRecordTypeId){
            return await _context.MedicalRecordTypes.FindAsync(medicalRecordTypeId);
        }
    }
}
