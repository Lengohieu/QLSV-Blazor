using QLSV.Data;
using QLSV.Dto;
using Share;

namespace QLSV.Mapper
{
    public class StudentMapper
    {
        //private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        public StudentMapper(IStudentService studentService)
        {
            //_classService = classService;
            _studentService = studentService;
        }

        public StudentMapper()
        {
        }

        public StudentGrpc MapEntityToGrpc(Student student)
        {
            StudentGrpc studentGrpc = new StudentGrpc();
            studentGrpc.Id = student.ID;
            studentGrpc.Name = student.Name;
            studentGrpc.Address = student.Address;
            studentGrpc.DateOfBirth = student.DateOfBirth;
            studentGrpc.ClassId = student.ClassId;
            return studentGrpc;
        }

        public Student MapGrpcToEntity(StudentGrpc studentGrpc)
        {
            Student student = new Student();
            student.ID = studentGrpc.Id.ToString();
            student.Name = studentGrpc.Name;
            student.Address = studentGrpc.Address;
            student.DateOfBirth = studentGrpc.DateOfBirth;
            student.ClassId = studentGrpc.ClassId;
            //if (student.Class == null)
            //{
            //    student.Class = new Class();
            //}
            //student.Class.ID = studentGrpc.ClassId;
            return student;
        }

        public StudentViewDto MapEntityToViewDto(Student student)
        {
            try
            {
                StudentViewDto StudentViewDto = new StudentViewDto();
                StudentViewDto.Name = student.Name;
                StudentViewDto.Address = student.Address;
                //StudentViewDto.ClassName = student.Class.Subject;
                StudentViewDto.DateOfBirth = student.DateOfBirth.ToString("dd/MM/yyyy");
                StudentViewDto.ID = int.Parse(student.ID);
                return StudentViewDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}