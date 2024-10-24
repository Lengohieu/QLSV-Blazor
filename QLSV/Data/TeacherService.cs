
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using ProtoBuf.Grpc.Client;
using QLSV.Mapper;
using Share;

namespace QLSV.Data
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper _mapper;
        public TeacherService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public TeacherProto GetService()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress($"http://localhost:5142", new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<TeacherProto>();
        }
        public bool AddNewTeacher(Teacher teacher)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                //TeacherGrpc teacherGrpc = teacherMapper.MapEntityToGrpc(teacher);
                TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                var client = GetService();
                booleanGrpc = client.AddNewTeacher(teacherGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddOrUpdateTeacher(Teacher teacher)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(teacher.ID))
                {
                    teacher.ID = Guid.NewGuid().ToString();
                    AddNewTeacher(teacher);
                }
                else
                {
                    UpdateTeacher(teacher);
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                var client = GetService();
                booleanGrpc = client.DeleteTeacher(teacherGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTeacherAsync(Teacher teacher)
        {
            bool result = false;
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        await session.DeleteAsync(teacher);
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

        public async Task<List<Teacher>> GetAllTeachers()
        {
            try
            {
                List<Teacher> teachers = new List<Teacher>();
                var client = GetService();
                Empty empty = new Empty();
                var listTeacher = client.GetAllTeachers(empty);
                foreach (var teacher in listTeacher.Teachers)
                {
                    Teacher teacherGrpc = _mapper.Map<Teacher>(teacher);

                    teachers.Add(teacherGrpc);
                }
                return teachers;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Teacher>> GetListTeachersBySearchAsync(string searchName)
        {
            try
            {
                List<Teacher> teachers = new List<Teacher>();
                var client = GetService();
                Empty empty = new Empty();
                if (!searchName.IsNullOrEmpty())
                {
                    var listTeacher = client.GetDataBySearchAsync(searchName);
                    foreach (var teacher in listTeacher.Teachers)
                    {
                        Teacher teacherGrpc = _mapper.Map<Teacher>(teacher);

                        teachers.Add(teacherGrpc);
                    }
                }
                return teachers;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Teacher> GetTeacherByIdAsync(string id)
        {
            try
            {
                IntGrpc intGrpc = new IntGrpc();
                intGrpc.Id = id;
                var client = GetService();
                var teacherGrpc = client.GetTeacherById(intGrpc);
                Teacher teacher = _mapper.Map<Teacher>(teacherGrpc);
                return teacher;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                var client = GetService();
                booleanGrpc = client.UpdateTeacher(teacherGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
