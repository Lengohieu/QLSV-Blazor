namespace GrpcService.Models.Entity
{
    public class ClassFilter
    {
        public String Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String Subject { get; set; }
        public int TeacherId { get; set; } = -1;
        public ClassFilter()
        {

        }
    }
}