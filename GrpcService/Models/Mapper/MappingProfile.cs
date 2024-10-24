using AutoMapper;
using Share;

namespace GrpcService.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Student, StudentGrpc>().ReverseMap();
            CreateMap<Teacher, TeacherGrpc>().ReverseMap();
            CreateMap<Class, ClassGrpc>().ReverseMap();
            //CreateMap<UserDto, User>();
        }
    }
}
