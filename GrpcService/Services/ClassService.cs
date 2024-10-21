using GrpcService.Models;
using GrpcService.Models;
using GrpcService.Models.Mapper;
using NHibernate.Mapping.ByCode.Impl;
using ProtoBuf.Grpc;
using Share;

namespace GrpcService.Services
{
    public class ClassService : ClassProto
    {
        private readonly ILogger<ClassService> _logger;
        private readonly IClassRepository _classRepository;
        ClasssMapper classMapper = new ClasssMapper();
        public ClassService(ILogger<ClassService> logger, IClassRepository classRepository)
        {
            _logger = logger;
            _classRepository = classRepository;
        }

        public ListClass GetAllClasses(Empty request, CallContext context = default)
        {
            ListClass listClass = new ListClass();
            List<Class> classes = _classRepository.GetListClass();
            foreach (var item in classes)
            {
                ClassGrpc classGrpc = classMapper.ClassToClassGrpc(item);
                listClass.List.Add(classGrpc);
            }
            return listClass;
        }

        public ClassGrpc GetClassById(IntGrpc request, CallContext context = default)
        {
            try
            {
                ClassGrpc classGrpc = new ClassGrpc();
                Class @class = _classRepository.GetClassById(request.Id);
                classGrpc = classMapper.ClassToClassGrpc(@class);
                return classGrpc;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
