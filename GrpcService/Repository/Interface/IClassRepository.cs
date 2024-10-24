using GrpcService.Models;
using GrpcService.Models.Entity;

public interface IClassRepository
{
    public List<Class> GetListClasss();
    public List<Class> GetClassBySearch(string searchName);
    public Boolean AddNewClass(Class class1);
    public Class FindClassById(string id);
    public Boolean RemoveClass(Class class1);
    public Boolean UpdateClassInfor(Class class1);
    public List<Class> SortData();
    Task<PageView<Class>> GetDataPageAsync(int pageNumber, int pageSize, ClassFilter classFilter);
}