namespace QLSV.Data
{
    public class Student
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        public virtual string ClassId { get; set; }
    }
}
