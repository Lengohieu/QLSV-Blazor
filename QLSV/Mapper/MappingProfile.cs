using AutoMapper;
using QLSV.Data;
using Share;

namespace QLSV.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherGrpc>().ReverseMap();
            CreateMap<Class, ClassGrpc>().ReverseMap();
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
            CreateMap<Class, ClassViewModel>().ReverseMap();
        }
    }
}
