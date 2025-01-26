using Challenge.Models;

namespace Challenge.Repository
{
    public interface IMedicalRecordRepository
    {
        Task<TMedicalRecord> AddMedicalRecord(TMedicalRecord medicalRecord);
        Task<TMedicalRecord?> GetMedicalRecordById(int medicalRecordId);
        //Task<TMedicalRecord?> DeleteMedicalRecord(int medicalRecordId, string deletedBy, string deletionReason);
        Task<TMedicalRecord?> UpdateMedicalRecord(TMedicalRecord medicalRecord);
    }
}
