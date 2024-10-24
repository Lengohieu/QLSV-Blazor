using QLSV.Data;
using QLSV.Dto;
using Share;

namespace QLSV.Mapper
{
    public class TeacherMapper
    {
        //private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        public TeacherMapper(ITeacherService teacherService)
        {
            //_classService = classService;
            _teacherService = teacherService;
        }

        public TeacherMapper()
        {
        }

        public TeacherGrpc MapEntityToGrpc(Teacher teacher)
        {
            TeacherGrpc teacherGrpc = new TeacherGrpc();
            teacherGrpc.Id = teacher.ID;
            teacherGrpc.Name = teacher.Name;
            teacherGrpc.DateOfBirth = teacher.DateOfBirth;
            return teacherGrpc;
        }

        public Teacher MapGrpcToEntity(TeacherGrpc teacherGrpc)
        {
            Teacher teacher = new Teacher();
            teacher.ID = teacherGrpc.Id.ToString();
            teacher.Name = teacherGrpc.Name;
            teacher.DateOfBirth = teacherGrpc.DateOfBirth;
            //if (teacher.Class == null)
            //{
            //    teacher.Class = new Class();
            //}
            //teacher.Class.ID = teacherGrpc.ClassId;
            return teacher;
        }

        public TeacherViewDto MapEntityToViewDto(Teacher teacher)
        {
            try
            {
                TeacherViewDto TeacherViewDto = new TeacherViewDto();
                TeacherViewDto.Name = teacher.Name;
                //TeacherViewDto.ClassName = teacher.Class.Subject;
                TeacherViewDto.DateOfBirth = teacher.DateOfBirth.ToString("dd/MM/yyyy");
                return TeacherViewDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}