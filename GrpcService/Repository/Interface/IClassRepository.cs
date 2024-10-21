using GrpcService.Models;

public interface IClassRepository
{
    List<Class> GetListClass();
    Class GetClassById(string id);
}