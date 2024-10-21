using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using QLSV.Data;

namespace QLSV.Pages
{
    public partial class StudentData : ComponentBase
    {
        [Inject] IStudentService StudentService { get; set; }
        List<Student> students { get; set; }
        List<StudentViewModel> studentsViewModels { get; set; }
        EditStudent editStudent = new EditStudent();
        TaskSearchStudent taskSearchStudents = new TaskSearchStudent();
        bool visible = false;
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            studentsViewModels = new();
            students = new();
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            loading = true;
            studentsViewModels.Clear();
            //await Task.Delay(3000);
            students = await StudentService.GetAllStudents();
            studentsViewModels = GetViewModels(students);
            loading = false;
            StateHasChanged();
        }

        List<StudentViewModel> GetViewModels(List<Student> datas)
        {
            var models = new List<StudentViewModel>();
            StudentViewModel model;
            int stt = 1;
            datas.ForEach(c =>
            {
                model = new StudentViewModel();
                model.stt = stt;
                model.ID = c.ID;
                model.Name = c.Name;
                model.DateOfBirth = c.DateOfBirth;
                model.Address = c.Address;

                models.Add(model);
                stt++;
            });
            return models;
        }

        void AddStudent()
        {
            var studentData = new Student();
            ShowStudentDetail(studentData);
        }

        void ShowStudentDetail(Student data)
        {
            editStudent.LoadData(data);
            visible = true;
        }

        async Task Save(Student data)
        {
            var resultAdd = await StudentService.AddOrUpdateStudent(data);
            await LoadAsync();
            visible = false;
        }

        async Task Edit(StudentViewModel studentViewModel)
        {
            //var studentData = students.FirstOrDefault(c => c.Id == studentViewModel.id);
            Student student = await StudentService.GetStudentByIdAsync(studentViewModel.ID);
            ShowStudentDetail(student);
        }

        async Task DeleteStudent(StudentViewModel studentViewModel)
        {
            Student studen = await StudentService.GetStudentByIdAsync(studentViewModel.ID);
            //await StudentService.DeleteStudentAsync(studen);
            StudentService.DeleteStudent(studen);
            await LoadAsync();
        }

        public class TaskSearchStudent
        {
            public string? Name { get; set; }
        }

        async Task Search()
        {
            var name = taskSearchStudents.Name;
            if (name.IsNullOrEmpty())
            {
                students = await StudentService.GetAllStudents();
                studentsViewModels = GetViewModels(students);
                return;
            }
            studentsViewModels.Clear();
            students = await StudentService.GetListStudentsBySearchAsync(name);
            studentsViewModels = GetViewModels(students);
            StateHasChanged();
        }

    }

}
