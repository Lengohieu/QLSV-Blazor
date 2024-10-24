
namespace QLSV.Dto
{
    public class ClassViewDto
    {
        public int Stt { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string TeacherId { get; set; }

        public ClassViewDto(int stt, int iD, string name, string dateOfBirth, string address, string ClassName)
        {
            this.Stt = stt;
            this.ID = iD;
            this.Name = name;
            this.Subject = address;
            this.TeacherId = TeacherId;
        }

        public ClassViewDto()
        {
        }

        public override string? ToString()
        {
            return $"{Stt} - {Name} - {Subject} - {TeacherId}";
        }
    }
}