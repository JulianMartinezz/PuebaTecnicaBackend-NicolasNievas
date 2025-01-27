using Challenge.Data;
using Challenge.DTO;
using Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repository.imp
{
    public class MedicalRecordRepositoryImp : IMedicalRecordRepository
    {
        private readonly MedicalContext _context;

        public MedicalRecordRepositoryImp(MedicalContext context)
        {
            _context = context;
        }

        public async Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord)
        {
            _context.TMedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }

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

        public async Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId)
        {
            return await _context.TMedicalRecords.FindAsync(medicalRecordId);
        }

        public async Task<TMedicalRecord?> UpdateMedicalRecord(TMedicalRecord medicalRecord)
        {
             _context.TMedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }
    }
}
