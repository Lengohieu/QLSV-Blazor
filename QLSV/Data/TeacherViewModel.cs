using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class TeacherViewModel
    {
        public string ID { get; set; }
        [Display(Name = "STT")]
        public int stt { get; set; }

        [Display(Name = "Tên giáo viên")]
        public string Name { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

    }
}
