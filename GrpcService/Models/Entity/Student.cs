namespace GrpcService.Models
{
    public class Student
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        //public virtual Class Class { get; set; }
        public virtual string ClassId { get; set; }

        public Student(string iD, string name, DateTime dateOfBirth, string address, Class roomClass)
        {
            this.ID = iD;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            //this.Class = roomClass;
        }
        public Student(string name, DateTime dateOfBirth, string address, Class roomClass)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            //this.Class = roomClass;
        }
        public Student()
        {
        }
        public override string? ToString()
        {
            return string.Empty;
            //return $"{ID} - {Name} - {DateOfBirth.ToString("yyyy/mm/đ")} - {Address} - Class: {Class.Subject}";
        }
    }
}
