using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class TeacherEditModel
    {
        public string id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
