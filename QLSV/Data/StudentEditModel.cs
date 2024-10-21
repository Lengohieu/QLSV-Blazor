using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class StudentEditModel
    {
        public string id { get; set; }
        //public int stt { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ClassId { get; set; }
    }
}
