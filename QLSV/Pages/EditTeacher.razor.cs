using Microsoft.AspNetCore.Components;
using QLSV.Data;
namespace QLSV.Pages
{
    public partial class EditTeacher : ComponentBase
    {
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<Teacher> ValueChange { get; set; }
        TeacherEditModel EditModel { get; set; } = new TeacherEditModel();

        public void LoadData(Teacher teacher)
        {
            EditModel.id = teacher.ID;
            EditModel.Name = teacher.Name;
            EditModel.DateOfBirth = teacher.DateOfBirth;

            StateHasChanged();
        }

        public void UpdateTeacher()
        {
            Teacher teacher = new Teacher();
            teacher.ID = EditModel.id;
            teacher.Name = EditModel.Name;
            teacher.DateOfBirth = EditModel.DateOfBirth;

            ValueChange.InvokeAsync(teacher);
        }

    }
}
