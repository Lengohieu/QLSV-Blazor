
namespace QLSV.Dto
{
    public class StudentViewDto
    {
        public int Stt { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ClassName { get; set; }

        public StudentViewDto(int stt, int iD, string name, string dateOfBirth, string address, string ClassName)
        {
            this.Stt = stt;
            this.ID = iD;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.ClassName = ClassName;
        }

        public StudentViewDto()
        {
        }

        public override string? ToString()
        {
            return $"{Stt} - {Name} - {DateOfBirth} - {Address} - {ClassName}";
        }
    }
}