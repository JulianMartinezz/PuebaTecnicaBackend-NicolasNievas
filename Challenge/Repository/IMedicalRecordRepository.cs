using Challenge.Models;

namespace Challenge.Repository
{
    public interface IMedicalRecordRepository
    {
        Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord);
        Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId);
    }
}
