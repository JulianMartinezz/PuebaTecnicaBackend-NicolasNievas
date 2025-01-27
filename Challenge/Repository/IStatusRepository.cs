using Challenge.Models;

namespace Challenge.Repository
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad <see cref="Status"/>.
    /// </summary>
    public interface IStatusRepository
    {
        /// <summary>
        /// Obtiene un objeto <see cref="Status"/> por su ID.
        /// </summary>
        /// <param name="statusId">ID del estado a buscar.</param>
        /// <returns>Un objeto <see cref="Status"/> si se encuentra, o null si no existe.</returns>
        Task<Status?> GetById(int? statusId);
    }
}
