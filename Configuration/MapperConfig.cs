using AutoMapper;
using CSharpWebAppRazorDB.DTO;
using CSharpWebAppRazorDB.Models;

namespace CSharpWebAppRazorDB.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<StudentInsertDTO, Student>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();
            CreateMap<StudentReadOnlyDTO, Student>().ReverseMap();
        }
    }
}
