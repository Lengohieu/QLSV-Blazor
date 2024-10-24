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

        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Lớp học")]
        public string RoomClass { get; set; }

        [Display(Name = "Lớp học")]
        public string RoomClassName { get; set; }

    }
}
