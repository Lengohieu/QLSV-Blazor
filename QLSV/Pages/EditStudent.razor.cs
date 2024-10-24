using Microsoft.AspNetCore.Components;
using QLSV.Data;
namespace QLSV.Pages
{
    public partial class EditStudent : ComponentBase
    {
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<Student> ValueChange { get; set; }
        [Parameter] public List<ClassViewModel> ClassViewModels { get; set; }
        StudentEditModel EditModel { get; set; } = new StudentEditModel();

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
