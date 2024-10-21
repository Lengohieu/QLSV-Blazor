using GrpcService.Models;
using GrpcService.Models.Entity;

public interface IStudentRepository
{
    public List<Student> GetListStudents();
    public List<Student> GetStudentBySearch(string searchName);
    public Boolean AddNewStudent(Student student);
    public Student FindStudentById(string id);
    public Boolean RemoveStudent(Student student);
    public Boolean UpdateStudentInfor(Student student);
    public List<Student> SortData();
    Task<PageView<Student>> GetDataPageAsync(int pageNumber, int pageSize, StudentFilter studentFilter);
}