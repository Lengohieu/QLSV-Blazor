using GrpcService.Models;
using GrpcService.Models.Entity;

namespace GrpcService.Repository.Interface
{
    public interface ITeacherRepository
    {
        public List<Teacher> GetListTeachers();
        public List<Teacher> GetTeacherBySearch(string searchName);
        public Boolean AddNewTeacher(Teacher teacher);
        public Teacher FindTeacherById(string id);
        public Boolean RemoveTeacher(Teacher teacher);
        public Boolean UpdateTeacherInfor(Teacher teacher);
        public List<Teacher> SortData();
        Task<PageView<Teacher>> GetDataPageAsync(int pageNumber, int pageSize, TeacherFilter teacherFilter);
    }
}
