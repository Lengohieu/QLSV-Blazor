using Microsoft.AspNetCore.Components;
using QLSV.Data;
namespace QLSV.Pages
{
    public partial class EditStudent : ComponentBase
    {
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<Student> ValueChange { get; set; }
        StudentEditModel EditModel { get; set; } = new StudentEditModel();
        List<Sex> sexs;

        public class Sex
        {
            public int value { get; set; }
            public string Name { get; set; }
        }

        protected override void OnInitialized()
        {
            sexs = new List<Sex>
            {
                new Sex{value=0, Name = "Nam"},
                new Sex{value=1,Name = "Nữ"}
            };
        }

        public void LoadData(Student student)
        {
            EditModel.id = student.ID;
            EditModel.Name = student.Name;
            EditModel.DateOfBirth = student.DateOfBirth;
            EditModel.Address = student.Address;
            EditModel.ClassId = student.ClassId;

            StateHasChanged();
        }

        public void UpdateStudent()
        {
            Student student = new Student();
            student.ID = EditModel.id;
            student.Name = EditModel.Name;
            student.DateOfBirth = EditModel.DateOfBirth;
            student.Address = EditModel.Address;
            student.ClassId = EditModel.ClassId;

            //Thực hiện update sinh viên
            ValueChange.InvokeAsync(student);
        }

    }
}
