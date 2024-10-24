namespace GrpcService.Models
{
    public class Teacher
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual ICollection<Class> RoomClasses { get; set; }
        public Teacher(string ID, string Name, DateTime DateOfBirth)
        {
            this.ID = ID;
            this.Name = Name;
            this.DateOfBirth = DateOfBirth;
        }
        public Teacher()
        {

        }

    }
}
