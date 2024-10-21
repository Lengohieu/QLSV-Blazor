using GrpcService.Models;
using GrpcService.Models.Entity;
using NHibernate;
using NHibernate.Linq;

public class StudentRepository : IStudentRepository
{
    private readonly ISessionFactory _session;

    public StudentRepository(ISessionFactory session)
    {
        _session = session;
    }
    public List<Student> GetListStudents()
    {
        List<Student> students = new List<Student>();
        using (var session = _session.OpenSession())
        {
            return session.Query<Student>()
                .ToList();
        }
    }
    public Boolean AddNewStudent(Student student)
    {
        using (var session = _session.OpenSession())
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    student.ClassId = Guid.NewGuid().ToString();
                    session.Save(student);
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
    public Student FindStudentById(string id)
    {
        using (var session = _session.OpenSession())
        {
            var student = session.Query<Student>()
                .Where(s => s.ID == id)
                .FirstOrDefault();
            return student;
        }
    }
    public Boolean RemoveStudent(Student student)
    {
        using (var session = _session.OpenSession())
        using (var transaction = session.BeginTransaction())
        {
            try
            {
                // Lấy sinh viên cần xóa từ cơ sở dữ liệu
                var studentToRemove = session.Get<Student>(student.ID);
                if (studentToRemove != null)
                {
                    // Xóa sinh viên khỏi cơ sở dữ liệu
                    session.Delete(studentToRemove);

                    // Commit giao dịch
                    transaction.Commit();
                    return true;
                }
                else
                {
                    Console.WriteLine($"Student with ID {student.ID} not found.");
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
    public Boolean UpdateStudentInfor(Student student)
    {
        using (var session = _session.OpenSession())
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    student.ClassId = Guid.NewGuid().ToString();
                    session.Update(student);
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
    public List<Student> SortData()
    {
        using (var session = _session.OpenSession())
        {
            return session.Query<Student>()
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.DateOfBirth)
                .ToList();
        }
    }

    public async Task<PageView<Student>> GetDataPageAsync(int pageNumber, int pageSize, StudentFilter studentFilter)
    {
        using (var session = _session.OpenSession())
        {
            var query = session.Query<Student>(); // Implement NHibernate query here
            query = Filter(query, studentFilter);
            var pagedData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var total = query.Count();
            return new PageView<Student>
            {
                Data = pagedData,
                PageCount = total // Đây là tổng số mục
            };
        }
    }
    private IQueryable<Student>? Filter(IQueryable<Student> query, StudentFilter studentFilter)
    {
        if (studentFilter.Name != null && !studentFilter.Name.Equals(""))
        {
            query = query.Where(c => c.Name.Contains(studentFilter.Name));
        }
        if (studentFilter.Address != null && !studentFilter.Address.Equals(""))
        {
            query = query.Where(c => c.Address.Contains(studentFilter.Address));
        }
        if (studentFilter.StartDate != null)
        {
            query = query.Where(student => student.DateOfBirth >= studentFilter.StartDate);
        }
        if (studentFilter.EndDate != null)
        {
            query = query.Where(student => student.DateOfBirth <= studentFilter.EndDate);
        }
        //if (studentFilter.ClassId != -1)
        //{
        //    query = query.Where(student => student.Class.ID == studentFilter.ClassId);
        //}
        //if (studentFilter.Id != -1)
        //{
        //    query = query.Where(student => student.ID == studentFilter.Id);
        //}
        return query;
    }

    public List<Student> GetStudentBySearch(string searchName)
    {
        List<Student> students = new List<Student>();
        using (var session = _session.OpenSession())
        {
            return session.Query<Student>()
                .Where(c => c.Name.Like('%' + searchName + '%'))
                .ToList();
        }
    }
}