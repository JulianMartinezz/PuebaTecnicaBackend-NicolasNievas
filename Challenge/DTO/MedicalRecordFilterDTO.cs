namespace Challenge.DTO
{
    public class MedicalRecordFilterDTO
    {
        public int? StatusId { get; set; }
        public DateOnly? StartDateFrom { get; set; }
        public DateOnly? EndDateFrom { get; set; }
        public int? MedicalRecordTypeId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
