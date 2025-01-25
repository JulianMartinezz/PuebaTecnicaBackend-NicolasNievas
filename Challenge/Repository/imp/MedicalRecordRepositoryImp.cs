using Challenge.Data;
using Challenge.Models;

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

        public async Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId)
        {
            return await _context.TMedicalRecords.FindAsync(medicalRecordId);
        }
    }
}
