using AutoMapper;
using Challenge.DTO;
using Challenge.Models;

namespace Challenge.Mapper
{
    /// <summary>
    /// Clase de configuración de AutoMapper para definir los mapeos entre entidades del dominio y sus respectivos DTOs.
    /// </summary>
    public class Mapper : Profile
    {
        /// <summary>
        /// Constructor de la clase <see cref="Mapper"/> que configura los mapeos entre entidades y DTOs.
        /// </summary>
        public Mapper()
        {
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<MedicalRecordType, MedicalRecordTypeDTO>().ReverseMap();
            CreateMap<TMedicalRecord, TMedicalRecordDTO>().ReverseMap();
        }
    }
}
