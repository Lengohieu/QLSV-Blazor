namespace GrpcService.Models.Entity
{
    public class TeacherFilter
    {
        public String Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TeacherFilter()
        {

        }
    }
}