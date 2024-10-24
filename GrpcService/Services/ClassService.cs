using AutoMapper;
using GrpcService.Models;
using GrpcService.Models;
using GrpcService.Models.Entity;
using GrpcService.Models.Mapper;
using NHibernate.Mapping.ByCode.Impl;
using ProtoBuf.Grpc;
using Share;

namespace GrpcService.Services
{
    public class ClassService : ClassProto
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        ClasssMapper classMapper = new ClasssMapper();
        public ClassService(IMapper mapper, ILogger<GreeterService> logger, IClassRepository classRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _classRepository = classRepository;
        }
        public BooleanGrpc AddNewClass(ClassGrpc request, CallContext context = default)
        {
            Class class1 = _mapper.Map<Class>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _classRepository.AddNewClass(class1);
            return booleanGrpc;
        }

        public BooleanGrpc DeleteClass(ClassGrpc request, CallContext context = default)
        {
            Class class1 = _mapper.Map<Class>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _classRepository.RemoveClass(class1);
            return booleanGrpc;
        }

        public ListClass GetAllClasss(Empty request, CallContext context = default)
        {
            ListClass listClass = new ListClass();
            List<Class> classs = _classRepository.GetListClasss();
            foreach (Class class1 in classs)
            {
                ClassGrpc classGrpc = classMapper.ClassToClassGrpc(class1);
/*                ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(class1);
*/                listClass.Classs.Add(classGrpc);
            }
            return listClass;
        }

        public ClassGrpc GetClassById(IntGrpc request, CallContext context = default)
        {
            try
            {
                ClassGrpc classGrpc = new ClassGrpc();
                Class class1 = _classRepository.FindClassById(request.Id);
                classGrpc = _mapper.Map<ClassGrpc>(class1);
                return classGrpc;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ListClass GetDataBySearchAsync(string pageViewGrpc, CallContext context = default)
        {
            ListClass listClass = new ListClass();
            List<Class> classs = _classRepository.GetClassBySearch(pageViewGrpc);
            foreach (Class class1 in classs)
            {
                ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(class1);
                listClass.Classs.Add(classGrpc);
            }
            return listClass;
        }

        

        private ClassFilter MapGrpcToClassFilter(ClassFilterGrpc classFilterGrpc)
        {
            var filter = new ClassFilter();
            filter.StartDate = classFilterGrpc.StartDate;
            filter.EndDate = classFilterGrpc.EndDate;
            filter.Name = classFilterGrpc.Name;
            filter.TeacherId = classFilterGrpc.TeacherId;
            return filter;
        }

        public BooleanGrpc UpdateClass(ClassGrpc request, CallContext context = default)
        {
            Class class1 = _mapper.Map<Class>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _classRepository.UpdateClassInfor(class1);
            return booleanGrpc;
        }

        public async Task<PageViewGrpc2> GetDataPageAsync(PageViewGrpc2 pageViewGrpc, CallContext context = default)
        {
            try
            {
                PageView<Class> pageView = new PageView<Class>();
                pageView.PageNumber = pageViewGrpc.PageNumber;
                pageView.PageSize = pageViewGrpc.PageSize;
                ClassFilter classFilter = new ClassFilter();
                classFilter = MapGrpcToClassFilter(pageViewGrpc.ClassFilterGrpc);
                var result = await _classRepository.GetDataPageAsync(pageView.PageNumber, pageView.PageSize, classFilter);
                var resultGrpc = new PageViewGrpc2();
                resultGrpc.PageCount = result.PageCount;
                resultGrpc.PageNumber = result.PageNumber;
                resultGrpc.PageSize = resultGrpc.PageSize;
                foreach (var item in result.Data)
                {
                    ClassGrpc classGrpc = _mapper.Map<ClassGrpc>(item);
                    resultGrpc.Classs.Add(classGrpc);
                }
                return resultGrpc;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
