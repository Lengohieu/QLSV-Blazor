namespace QLSV.Data
{
    public interface IClassService
    {
        Task<List<Class>> GetAllClasss();
        Task<bool> AddOrUpdateClass(Class class1);
        Task<bool> DeleteClassAsync(Class class1);
        Task<Class> GetClassByIdAsync(string id);
        Task<List<Class>> GetListClasssBySearchAsync(string searchName);

        public Boolean AddNewClass(Class class1);

        /// <summary>
        /// Hàm cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="classUpdate"></param>
        public Boolean UpdateClass(Class class1);

        /// <summary>
        /// Hàm xóa 1 sinh viên
        /// </summary>
        /// <param name="class"></param>
        public Boolean DeleteClass(Class class1);
    }
}
