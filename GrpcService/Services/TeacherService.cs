using AutoMapper;
using GrpcService.Models;
using GrpcService.Models.Entity;
using GrpcService.Repository.Interface;
using ProtoBuf.Grpc;
using Share;

namespace GrpcService.Services
{
    public class TeacherService : TeacherProto
    {
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
        public TeacherService(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }
        public BooleanGrpc AddNewTeacher(TeacherGrpc request, CallContext context = default)
        {
            Teacher teacher = _mapper.Map<Teacher>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _teacherRepository.AddNewTeacher(teacher);
            return booleanGrpc;
        }

        public BooleanGrpc DeleteTeacher(TeacherGrpc request, CallContext context = default)
        {
            Teacher teacher = _mapper.Map<Teacher>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _teacherRepository.RemoveTeacher(teacher);
            return booleanGrpc;
        }

        public ListTeacher GetAllTeachers(Empty request, CallContext context = default)
        {
            ListTeacher listTeacher = new ListTeacher();
            List<Teacher> teachers = _teacherRepository.GetListTeachers();
            foreach (Teacher teacher in teachers)
            {
                TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                listTeacher.Teachers.Add(teacherGrpc);
            }
            return listTeacher;
        }

        public ListTeacher GetDataBySearchAsync(string pageViewGrpc, CallContext context = default)
        {
            ListTeacher listTeacher = new ListTeacher();
            List<Teacher> teachers = _teacherRepository.GetTeacherBySearch(pageViewGrpc);
            foreach (Teacher teacher in teachers)
            {
                TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                listTeacher.Teachers.Add(teacherGrpc);
            }
            return listTeacher;
        }

        public async Task<PageViewGrpc1> GetDataPageAsync(PageViewGrpc1 pageViewGrpc, CallContext context = default)
        {
            try
            {
                PageView<Teacher> pageView = new PageView<Teacher>();
                pageView.PageNumber = pageViewGrpc.PageNumber;
                pageView.PageSize = pageViewGrpc.PageSize;
                TeacherFilter teacherFilter = new TeacherFilter();
                teacherFilter = MapGrpcToTeacherFilter(pageViewGrpc.TeacherFilterGrpc);
                var result = await _teacherRepository.GetDataPageAsync(pageView.PageNumber, pageView.PageSize, teacherFilter);
                var resultGrpc = new PageViewGrpc1();
                resultGrpc.PageCount = result.PageCount;
                resultGrpc.PageNumber = result.PageNumber;
                resultGrpc.PageSize = resultGrpc.PageSize;
                foreach (var item in result.Data)
                {
                    TeacherGrpc teacherGrpc = _mapper.Map<TeacherGrpc>(item);
                    resultGrpc.Teachers.Add(teacherGrpc);
                }
                return resultGrpc;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public TeacherGrpc GetTeacherById(IntGrpc request, CallContext context = default)
        {
            try
            {
                TeacherGrpc teacherGrpc = new TeacherGrpc();
                Teacher teacher = _teacherRepository.FindTeacherById(request.Id);
                teacherGrpc = _mapper.Map<TeacherGrpc>(teacher);
                return teacherGrpc;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BooleanGrpc UpdateTeacher(TeacherGrpc request, CallContext context = default)
        {
            Teacher teacher = _mapper.Map<Teacher>(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _teacherRepository.UpdateTeacherInfor(teacher);
            return booleanGrpc;
        }
        private TeacherFilter MapGrpcToTeacherFilter(TeacherFilterGrpc teacherFilterGrpc)
        {
            var filter = new TeacherFilter();
            filter.StartDate = teacherFilterGrpc.StartDate;
            filter.EndDate = teacherFilterGrpc.EndDate;
            filter.Name = teacherFilterGrpc.Name;
            return filter;
        }
    }
}
