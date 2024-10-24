
namespace QLSV.Dto
{
    public class TeacherViewDto
    {
        public int Stt { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }

        public TeacherViewDto(int stt, int iD, string name, string dateOfBirth, string address, string ClassName)
        {
            this.Stt = stt;
            this.ID = iD;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public TeacherViewDto()
        {
        }

        public override string? ToString()
        {
            return $"{Stt} - {Name} - {DateOfBirth}";
        }
    }
}