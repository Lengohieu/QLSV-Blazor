
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using ProtoBuf.Grpc.Client;
using QLSV.Mapper;
using Share;

namespace QLSV.Data
{
    public class ClassService : IClassService
    {
        private readonly IMapper _mapper;
        public ClassService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ClassProto GetService()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress($"http://localhost:5142", new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<ClassProto>();
        }
        public bool AddNewClass(Class class1)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(class1);
                var client = GetService();
                booleanGrpc = client.AddNewClass(classGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddOrUpdateClass(Class class1)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(class1.ID))
                {
                    class1.ID = Guid.NewGuid().ToString();
                    AddNewClass(class1);
                }
                else
                {
                    UpdateClass(class1);
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeleteClass(Class class1)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(class1);
                var client = GetService();
                booleanGrpc = client.DeleteClass(classGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteClassAsync(Class class1)
        {
            bool result = false;
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        await session.DeleteAsync(class1);
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

        public async Task<List<Class>> GetAllClasss()
        {
            try
            {
                List<Class> classs = new List<Class>();
                var client = GetService();
                Empty empty = new Empty();
                var listClass = client.GetAllClasss(empty);
                foreach (var class1 in listClass.Classs)
                {
                    Class classGrpc = _mapper.Map<Class>(class1);

                    classs.Add(classGrpc);
                }
                return classs;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Class> GetClassByIdAsync(string id)
        {
            try
            {
                IntGrpc intGrpc = new IntGrpc();
                intGrpc.Id = id;
                var client = GetService();
                var classGrpc = client.GetClassById(intGrpc);
                Class class1 = _mapper.Map<Class>(classGrpc);
                return class1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Class>> GetListClasssBySearchAsync(string searchName)
        {
            try
            {
                List<Class> classs = new List<Class>();
                var client = GetService();
                Empty empty = new Empty();
                if (!searchName.IsNullOrEmpty())
                {
                    var listClass = client.GetDataBySearchAsync(searchName);
                    foreach (var class1 in listClass.Classs)
                    {
                        Class classGrpc = _mapper.Map<Class>(class1);

                        classs.Add(classGrpc);
                    }
                }
                return classs;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateClass(Class class1)
        {
            try
            {
                BooleanGrpc booleanGrpc = new BooleanGrpc();
                ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(class1);
                var client = GetService();
                booleanGrpc = client.UpdateClass(classGrpc);
                return booleanGrpc.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
