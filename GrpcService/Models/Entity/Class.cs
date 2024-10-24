namespace GrpcService.Models
{
    public class Class
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Subject { get; set; }
        public virtual string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public Class()
        {

        }
        public Class(string ID, string Name, string Subject, Teacher teacher)
        {
            this.ID = ID;
            this.Name = Name;
            this.Subject = Subject;
            this.TeacherId = teacher.ID;
        }
    }
}
