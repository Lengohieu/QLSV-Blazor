namespace QLSV.Data
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<bool> AddOrUpdateStudent(Student student);
        Task<bool> DeleteStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(string id);
        Task<List<Student>> GetListStudentsBySearchAsync(string searchName);

        public Boolean AddNewStudent(Student student);

        /// <summary>
        /// Hàm cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="studentUpdate"></param>
        public Boolean UpdateStudent(Student student);

        /// <summary>
        /// Hàm xóa 1 sinh viên
        /// </summary>
        /// <param name="student"></param>
        public Boolean DeleteStudent(Student student);
    }
}
