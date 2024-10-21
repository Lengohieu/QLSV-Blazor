using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;
using Share;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using QLSV.Mapper;
using Microsoft.IdentityModel.Tokens;

namespace QLSV.Data
{


    public class StudentService : IStudentService
    {
        StudentMapper studentMapper = new StudentMapper();

        public StudentProto GetService()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress($"http://localhost:5142", new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<StudentProto>();
        }
        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                List<Student> students = new List<Student>();
                var client = GetService();
                Empty empty = new Empty();
                var listStudent = client.GetAllStudents(empty);
                foreach (var student in listStudent.Students)
                {
                    Student student1 = studentMapper.MapGrpcToEntity(student);

                    students.Add(student1);
                }
                return students;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            // goi db binh thuong
            //Student student;
            //using (var session = FluentNHibernateHelper.OpenSession())
            //{
            //    using (ITransaction transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            student = await session.GetAsync<Student>(id);
            //            return student;
            //        }
            //        catch (Exception ex)
            //        {
            //            //await transaction.RollbackAsync();
            //            throw ex;
            //        }
            //    }
            //}

            // dung grpc
            try
            {
                IntGrpc intGrpc = new IntGrpc();
                intGrpc.Id = id;
                var client = GetService();
                var studentGrpc = client.GetStudentById(intGrpc);
                Student student = studentMapper.MapGrpcToEntity(studentGrpc);
                return student;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddOrUpdateStudent(Student student)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(student.ID))
                {
                    student.ID = Guid.NewGuid().ToString();
                    AddNewStudent(student);
                    //await session.SaveAsync(student);
                }
                else
                {
                    UpdateStudent(student);
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<bool> DeleteStudentAsync(Student student)
        {
            bool result = false;
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        await session.DeleteAsync(student);
                        await transaction.CommitAsync();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }
                }
            }
            return result;
        }

        public async Task<List<Student>> GetListStudentsBySearchAsync(string searchName)
        {
            // goi binh thuong
            //using (var session = FluentNHibernateHelper.OpenSession())
            //{
            //    using (ITransaction transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            List<Student> students;
            //            students = session.Query<Student>().Where(c => c.Name.Like('%' + searchName + '%')).ToList();
            //            //await transaction.CommitAsync();
            //            return students;
            //        }
            //        catch (Exception ex)
            //        {
            //            await transaction.RollbackAsync();
            //            throw ex;
            //        }
            //    }
            //}

            // su dung grpc
            try
            {
                List<Student> students = new List<Student>();
                var client = GetService();
                Empty empty = new Empty();
                if (!searchName.IsNullOrEmpty())
                {
                    var listStudent = client.GetDataBySearchAsync(searchName);
                    foreach (var student in listStudent.Students)
                    {
                        Student student1 = studentMapper.MapGrpcToEntity(student);

                        students.Add(student1);
                    }
                }
                return students;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AddNewStudent(Student student)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                StudentGrpc studentGrpc = studentMapper.MapEntityToGrpc(student);
                var client = GetService();
                booleanGrpc = client.AddNewStudent(studentGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateStudent(Student student)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                StudentGrpc studentGrpc = studentMapper.MapEntityToGrpc(student);
                var client = GetService();
                booleanGrpc = client.UpdateStudent(studentGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteStudent(Student student)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                StudentGrpc studentGrpc = studentMapper.MapEntityToGrpc(student);
                var client = GetService();
                booleanGrpc = client.DeleteStudent(studentGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}