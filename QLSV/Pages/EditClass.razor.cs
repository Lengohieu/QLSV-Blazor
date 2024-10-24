using Microsoft.AspNetCore.Components;
using QLSV.Data;
namespace QLSV.Pages
{
    public partial class EditClass : ComponentBase
    {
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<Class> ValueChange { get; set; }
        [Parameter] public List<TeacherViewModel> teacherViewModels { get; set; }
        ClassEditModel EditModel { get; set; } = new ClassEditModel();

        public void LoadData(Class class1)
        {
            EditModel.id = class1.ID;
            EditModel.Name = class1.Name;
            EditModel.Subject = class1.Subject;
            EditModel.TeacherId = class1.TeacherId;

            StateHasChanged();
        }

        public void UpdateClass()
        {
            Class class1 = new Class();

            class1.ID = EditModel.id;
            class1.Name = EditModel.Name;
            class1.Subject = EditModel.Subject;
            class1.TeacherId = EditModel.TeacherId;

            //Thực hiện update sinh viên
            ValueChange.InvokeAsync(class1);
        }

    }
}
