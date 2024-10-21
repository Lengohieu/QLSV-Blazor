using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class StudentViewModel
    {
        public string ID { get; set; }
        [Display(Name = "STT")]
        public int stt { get; set; }

        [Display(Name = "Tên sinh viên")]
        public string Name { get; set; }

        [Display(Name = "ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "DIA CHI")]
        public string Address { get; set; }

        [Display(Name = "lop hoc")]
        public int RoomClass { get; set; }

    }
}
