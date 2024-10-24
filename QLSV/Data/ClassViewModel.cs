using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class ClassViewModel
    {
        public string ID { get; set; }
        [Display(Name = "STT")]
        public int stt { get; set; }

        [Display(Name = "Tên lớp học")]
        public string Name { get; set; }

        [Display(Name = "Môn học")]
        public string Subject { get; set; }

        [Display(Name = "Giáo viên")]
        public string TeacherId { get; set; }

        
        [Display(Name = "Giáo viên")]
        public string TeacherName { get; set; }

    }
}
