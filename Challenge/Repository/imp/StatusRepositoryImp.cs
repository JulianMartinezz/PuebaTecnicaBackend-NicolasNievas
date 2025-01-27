using Challenge.Data;
using Challenge.Models;

namespace Challenge.Repository.imp
{
    /// <summary>
    /// Implementación del repositorio para la entidad <see cref="Status"/>.
    /// </summary>
    public class StatusRepositoryImp : IStatusRepository
    {
        /// <summary>
        /// Contexto de base de datos utilizado para acceder a los datos.
        /// </summary>
        private readonly MedicalContext _context;

        /// <summary>
        /// Constructor de la clase <see cref="StatusRepositoryImp"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public StatusRepositoryImp(MedicalContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un objeto <see cref="Status"/> por su ID.
        /// </summary>
        /// <param name="statusId">ID del estado a buscar.</param>
        /// <returns>Un objeto <see cref="Status"/> si se encuentra, o null si no existe.</returns>
        public async Task<Status?> GetById(int? statusId)
        {
            return await _context.Statuses.FindAsync(statusId);
        }
    }
}
