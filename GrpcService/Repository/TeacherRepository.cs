using GrpcService.Models;
using GrpcService.Models.Entity;
using GrpcService.Repository.Interface;
using NHibernate;
using NHibernate.Linq;

namespace GrpcService.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ISessionFactory _session;

        public TeacherRepository(ISessionFactory session)
        {
            _session = session;
        }
        public bool AddNewTeacher(Teacher teacher)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        //teacher.ClassId = Guid.NewGuid().ToString();
                        session.Save(teacher);
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

        public Teacher FindTeacherById(string id)
        {
            using (var session = _session.OpenSession())
            {
                var teacher = session.Query<Teacher>()
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
                return teacher;
            }
        }

        public async Task<PageView<Teacher>> GetDataPageAsync(int pageNumber, int pageSize, TeacherFilter teacherFilter)
        {
            using (var session = _session.OpenSession())
            {
                var query = session.Query<Teacher>(); // Implement NHibernate query here
                query = Filter(query, teacherFilter);
                var pagedData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var total = query.Count();
                return new PageView<Teacher>
                {
                    Data = pagedData,
                    PageCount = total // Đây là tổng số mục
                };
            }
        }

        private IQueryable<Teacher> Filter(IQueryable<Teacher> query, TeacherFilter teacherFilter)
        {
            if (teacherFilter.Name != null && !teacherFilter.Name.Equals(""))
            {
                query = query.Where(c => c.Name.Contains(teacherFilter.Name));
            }
            if (teacherFilter.StartDate != null)
            {
                query = query.Where(teacher => teacher.DateOfBirth >= teacherFilter.StartDate);
            }
            if (teacherFilter.EndDate != null)
            {
                query = query.Where(teacher => teacher.DateOfBirth <= teacherFilter.EndDate);
            }
            return query;
        }

        public List<Teacher> GetListTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (var session = _session.OpenSession())
            {
                return session.Query<Teacher>()
                    .ToList();
            }
        }

        public List<Teacher> GetTeacherBySearch(string searchName)
        {
            List<Teacher> teachers = new List<Teacher>();
            using (var session = _session.OpenSession())
            {
                return session.Query<Teacher>()
                    .Where(c => c.Name.Like('%' + searchName + '%'))
                    .ToList();
            }
        }

        public bool RemoveTeacher(Teacher teacher)
        {
            using (var session = _session.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    // Lấy sinh viên cần xóa từ cơ sở dữ liệu
                    var teacherToRemove = session.Get<Teacher>(teacher.ID);
                    if (teacherToRemove != null)
                    {
                        // Xóa sinh viên khỏi cơ sở dữ liệu
                        session.Delete(teacherToRemove);

                        // Commit giao dịch
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Teacher with ID {teacher.ID} not found.");
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

        public List<Teacher> SortData()
        {
            using (var session = _session.OpenSession())
            {
                return session.Query<Teacher>()
                    .OrderBy(x => x.Name)
                    .ThenByDescending(x => x.DateOfBirth)
                    .ToList();
            }
        }

        public bool UpdateTeacherInfor(Teacher teacher)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        //teacher.ClassId = Guid.NewGuid().ToString();
                        session.Update(teacher);
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
    }
}
