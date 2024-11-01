using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using QLSV.Data;

namespace QLSV.Pages
{
    public partial class TeacherData : ComponentBase
    {
        [Inject] ITeacherService TeacherService { get; set; }
        List<Teacher> teachers { get; set; }
        List<TeacherViewModel> teachersViewModels { get; set; }
        EditTeacher editTeacher = new EditTeacher();
        TaskSearchTeacher taskSearchTeachers = new TaskSearchTeacher();
        bool visible = false;
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            teachersViewModels = new();
            teachers = new();
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            loading = true;
            teachersViewModels.Clear();
            teachers = await TeacherService.GetAllTeachers();
            teachersViewModels = GetViewModels(teachers);
            teachersViewModels = GetViewModels(teachers).OrderBy(c => c.Name).ToList();
            int stt = 1;
            foreach(var i in teachersViewModels) 
            {
                
                i.stt = stt++;
            }

            loading = false;
            StateHasChanged();
        }

        List<TeacherViewModel> GetViewModels(List<Teacher> datas)
        {
            var models = new List<TeacherViewModel>();
            TeacherViewModel model;
            int stt = 1;
            datas.ForEach(c =>
            {
                model = new TeacherViewModel();
                model.ID = c.ID;
                model.Name = c.Name;
                model.DateOfBirth = c.DateOfBirth;

                models.Add(model);
            });
            return models;
        }

        void AddTeacher()
        {
            var teacherData = new Teacher();
            ShowTeacherDetail(teacherData);
        }

        void ShowTeacherDetail(Teacher data)
        {
            editTeacher.LoadData(data);
            visible = true;
        }

        async Task Save(Teacher data)
        {
            var resultAdd = await TeacherService.AddOrUpdateTeacher(data);
            await LoadAsync();
            visible = false;
        }

        async Task Edit(TeacherViewModel teacherViewModel)
        {
            //var teacherData = teachers.FirstOrDefault(c => c.Id == teacherViewModel.id);
            Teacher teacher = await TeacherService.GetTeacherByIdAsync(teacherViewModel.ID);
            ShowTeacherDetail(teacher);
        }

        async Task DeleteTeacher(TeacherViewModel teacherViewModel)
        {
            Teacher teacher = await TeacherService.GetTeacherByIdAsync(teacherViewModel.ID);
            TeacherService.DeleteTeacher(teacher);
            await LoadAsync();
        }

        public class TaskSearchTeacher
        {
            public string? Name { get; set; }
        }

        async Task Search()
        {
            var name = taskSearchTeachers.Name;
            if (name.IsNullOrEmpty())
            {
                teachers = await TeacherService.GetAllTeachers();
                teachersViewModels = GetViewModels(teachers);
                return;
            }
            teachersViewModels.Clear();
            teachers = await TeacherService.GetListTeachersBySearchAsync(name);
            teachersViewModels = GetViewModels(teachers);
            StateHasChanged();
        }

    }

}
