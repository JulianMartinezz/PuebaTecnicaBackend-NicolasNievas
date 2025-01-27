using Challenge.Data;
using Challenge.DTO;
using Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repository.imp
{
    /// <summary>
    /// Implementación del repositorio para la gestión de registros médicos.
    /// </summary>
    public class MedicalRecordRepositoryImp : IMedicalRecordRepository
    {
        /// <summary>
        /// Contexto de base de datos utilizado para acceder a los registros médicos.
        /// </summary>
        private readonly MedicalContext _context;

        /// <summary>
        /// Constructor de la clase <see cref="MedicalRecordRepositoryImp"/>.
        /// </summary>
        /// <param name="context">El contexto de base de datos inyectado.</param>
        public MedicalRecordRepositoryImp(MedicalContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega un nuevo registro médico a la base de datos.
        /// </summary>
        /// <param name="medicalRecord">El objeto <see cref="TMedicalRecord"/> que se agregará.</param>
        /// <returns>El registro médico agregado.</returns>
        public async Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord)
        {
            _context.TMedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }

        /// <summary>
        /// Obtiene una lista de registros médicos filtrados según los criterios especificados.
        /// </summary>
        /// <param name="filter">Objeto <see cref="MedicalRecordFilterDTO"/> con los filtros aplicados.</param>
        /// <returns>Una tupla que contiene la lista de registros médicos filtrados y el conteo total.</returns>
        public async Task<(List<TMedicalRecord> Medical, int TotalCount)> GetFilterMedicalRecords(MedicalRecordFilterDTO filter)
        {
            var query = _context.TMedicalRecords.AsQueryable();

            if (filter.StatusId.HasValue)
                query = query.Where(x => x.StatusId == filter.StatusId);

            if (filter.MedicalRecordTypeId.HasValue)
                query = query.Where(x => x.MedicalRecordTypeId == filter.MedicalRecordTypeId);

            if (filter.StartDateFrom.HasValue)
                query = query.Where(x => x.StartDate >= filter.StartDateFrom);

            if (filter.EndDateFrom.HasValue)
                query = query.Where(x => x.EndDate <= filter.EndDateFrom);

            var totalCount = await query.CountAsync();

            var medicalR = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (medicalR, totalCount);
        }

        /// <summary>
        /// Obtiene un registro médico por su ID.
        /// </summary>
        /// <param name="medicalRecordId">El ID del registro médico a buscar.</param>
        /// <returns>El registro médico encontrado o null si no existe.</returns>
        public async Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId)
        {
            return await _context.TMedicalRecords.FindAsync(medicalRecordId);
        }

        /// <summary>
        /// Actualiza un registro médico existente en la base de datos.
        /// </summary>
        /// <param name="medicalRecord">El objeto <see cref="TMedicalRecord"/> con los datos actualizados.</param>
        /// <returns>El registro médico actualizado o null si no se pudo actualizar.</returns>
        public async Task<TMedicalRecord?> UpdateMedicalRecord(TMedicalRecord medicalRecord)
        {
             _context.TMedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }
    }
}
