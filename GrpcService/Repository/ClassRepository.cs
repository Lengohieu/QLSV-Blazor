using GrpcService.Models;
using NHibernate;
using NHibernate.Linq;

internal class ClassRepository : IClassRepository
{
    private readonly ISessionFactory _session;

    public ClassRepository(ISessionFactory session)
    {
        _session = session;
    }
    public List<Class> GetListClass()
    {
        List<Student> students = new List<Student>();
        using (var session = _session.OpenSession())
        {
            return session.Query<Class>()
                .Fetch(s => s.Teacher)
                .ToList();
        }
    }
    public Class GetClassById(int id)
    {
        using (var session = _session.OpenSession())
        {
            return session.Query<Class>()
                .Where(c => c.ID == id)
                .First();
        }
    }

    public Class GetClassById(string id)
    {
        throw new NotImplementedException();
    }
}