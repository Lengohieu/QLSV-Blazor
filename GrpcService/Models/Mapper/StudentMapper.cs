using GrpcService.Models;
using Share;

namespace GrpcService.Models.Mapper
{
    internal class StudentMapper
    {
        public StudentGrpc ClassToClassGrpc(Student student)
        {
            StudentGrpc studentGrpc = new StudentGrpc();
            studentGrpc.Id = student.ID;
            studentGrpc.Name = student.Name;
            studentGrpc.Address = student.Address;
            studentGrpc.DateOfBirth = student.DateOfBirth;
            //if (student.Class == null)
            //{
            //    student.Class = new Class();
            //}
            //studentGrpc.ClassId = student.Class.ID;
            return studentGrpc;
        }
        public Student ClassGrpcToClass(StudentGrpc studentGrpc)
        {
            Student student = new Student();
            student.ID = studentGrpc.Id;
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
    }
}