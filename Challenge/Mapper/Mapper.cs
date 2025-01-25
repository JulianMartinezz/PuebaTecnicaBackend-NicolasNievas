using AutoMapper;
using Challenge.DTO;
using Challenge.Models;

namespace Challenge.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<MedicalRecordType, MedicalRecordTypeDTO>().ReverseMap();
            CreateMap<TMedicalRecord, TMedicalRecordDTO>().ReverseMap();
        }
    }
}
