namespace QLSV.Data
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<bool> AddOrUpdateTeacher(Teacher teacher);
        Task<bool> DeleteTeacherAsync(Teacher teacher);
        Task<Teacher> GetTeacherByIdAsync(string id);
        Task<List<Teacher>> GetListTeachersBySearchAsync(string searchName);

        public Boolean AddNewTeacher(Teacher teacher);

        /// <summary>
        /// Hàm cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="teacherUpdate"></param>
        public Boolean UpdateTeacher(Teacher teacher);

        /// <summary>
        /// Hàm xóa 1 sinh viên
        /// </summary>
        /// <param name="teacher"></param>
        public Boolean DeleteTeacher(Teacher teacher);
    }
}
