namespace Challenge.DTO
{
    public class DeleteMedicalRecordDTO
    {
        public int MedicalRecordId { get; set; }
        public string DeletedBy { get; set; }
        public string DeletionReason { get; set; }
    }
}
