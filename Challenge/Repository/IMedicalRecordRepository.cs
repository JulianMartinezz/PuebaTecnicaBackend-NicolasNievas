using Challenge.DTO;
using Challenge.Models;

namespace Challenge.Repository
{
    public interface IMedicalRecordRepository
    {
        Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord);
        Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId);
        Task<(List<TMedicalRecord> Medical, int TotalCount)> GetFilterMedicalRecords(MedicalRecordFilterDTO filter);
        Task<TMedicalRecord?> UpdateMedicalRecord(TMedicalRecord medicalRecord);
    }
}
