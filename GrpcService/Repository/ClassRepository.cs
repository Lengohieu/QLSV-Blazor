using GrpcService.Models;
using GrpcService.Models.Entity;
using NHibernate;
using NHibernate.Linq;

internal class ClassRepository : IClassRepository
{
    private readonly ISessionFactory _session;

    public ClassRepository(ISessionFactory session)
    {
        _session = session;
    }
    public bool AddNewClass(Class class1)
    {
        using (var session = _session.OpenSession())
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    //class1.TeacherId = Guid.NewGuid().ToString();
                    session.Save(class1);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }
        return false;
    }

    public Class FindClassById(string id)
    {
        using (var session = _session.OpenSession())
        {
            var class1 = session.Query<Class>()
                .Where(s => s.ID == id)
                .FirstOrDefault();
            return class1;
        }
    }

    public List<Class> GetClassBySearch(string searchName)
    {
        List<Class> classs = new List<Class>();
        using (var session = _session.OpenSession())
        {
            return session.Query<Class>()
                .Where(c => c.Name.Like('%' + searchName + '%'))
                .ToList();
        }
    }

    public async Task<PageView<Class>> GetDataPageAsync(int pageNumber, int pageSize, ClassFilter classFilter)
    {
        using (var session = _session.OpenSession())
        {
            var query = session.Query<Class>(); // Implement NHibernate query here
            query = Filter(query, classFilter);
            var pagedData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var total = query.Count();
            return new PageView<Class>
            {
                Data = pagedData,
                PageCount = total // Đây là tổng số mục
            };
        }
    }

    public List<Class> GetListClasss()
    {
        List<Class> classs = new List<Class>();
        using (var session = _session.OpenSession())
        {
            return session.Query<Class>()
                .ToList();
        }
    }

    public bool RemoveClass(Class class1)
    {
        using (var session = _session.OpenSession())
        using (var transaction = session.BeginTransaction())
        {
            try
            {
                // Lấy sinh viên cần xóa từ cơ sở dữ liệu
                var classToRemove = session.Get<Class>(class1.ID);
                if (classToRemove != null)
                {
                    // Xóa sinh viên khỏi cơ sở dữ liệu
                    session.Delete(classToRemove);

                    // Commit giao dịch
                    transaction.Commit();
                    return true;
                }
                else
                {
                    Console.WriteLine($"Class with ID {class1.ID} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                transaction.Rollback();
            }
            return false;
        }
    }
    public List<Class> SortData()
    {
        using (var session = _session.OpenSession())
        {
            return session.Query<Class>()
                .OrderBy(x => x.Name)
                //.ThenByDescending(x => x.DateOfBirth)
                .ToList();
        }
    }

    public bool UpdateClassInfor(Class class1)
    {
        using (var session = _session.OpenSession())
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    //class1.TeacherId = Guid.NewGuid().ToString();
                    session.Update(class1);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occurred: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }
        return false;
    }
    private IQueryable<Class>? Filter(IQueryable<Class> query, ClassFilter classFilter)
    {
        if (classFilter.Name != null && !classFilter.Name.Equals(""))
        {
            query = query.Where(c => c.Name.Contains(classFilter.Name));
        }
        if (classFilter.Subject != null && !classFilter.Subject.Equals(""))
        {
            query = query.Where(c => c.Subject.Contains(classFilter.Subject));
        }
        return query;
    }
}