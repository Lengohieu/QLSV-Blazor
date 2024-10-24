using QLSV.Data;
using QLSV.Dto;
using Share;

namespace QLSV.Mapper
{
    public class ClassMapper
    {
        private readonly IClassService _classService;
        public ClassMapper(IClassService classService)
        {
            _classService = classService;
        }

        public ClassMapper()
        {
        }

        public ClassGrpc MapEntityToGrpc(Class class1)
        {
            ClassGrpc classGrpc = new ClassGrpc();
            classGrpc.Id = class1.ID;
            classGrpc.Name = class1.Name;
            classGrpc.Subject = class1.Subject;
            classGrpc.TeacherId = class1.TeacherId;
            return classGrpc;
        }

        public Class MapGrpcToEntity(ClassGrpc classGrpc)
        {
            Class class1 = new Class();
            class1.ID = classGrpc.Id.ToString();
            class1.Name = classGrpc.Name;
            class1.Subject = classGrpc.Subject;
            return class1;
        }

        public ClassViewDto MapEntityToViewDto(Class class1)
        {
            try
            {
                ClassViewDto ClassViewDto = new ClassViewDto();
                ClassViewDto.Name = class1.Name;
                ClassViewDto.Subject = class1.Subject;
                //ClassViewDto.ClassName = class.Class.Subject;
                ClassViewDto.ID = int.Parse(class1.ID);
                return ClassViewDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}