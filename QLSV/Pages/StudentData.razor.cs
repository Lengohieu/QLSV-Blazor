using AutoMapper;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using QLSV.Data;

namespace QLSV.Pages
{
    public partial class StudentData : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] IStudentService StudentService { get; set; }
        [Inject] IClassService ClassService { get; set; }
        List<Student> students { get; set; }
        List<StudentViewModel> studentsViewModels { get; set; }
        List<ClassViewModel> classViewModels { get; set; }
        EditStudent editStudent = new EditStudent();
        TaskSearchStudent taskSearchStudents = new TaskSearchStudent();
        bool visible = false;
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            studentsViewModels = new();
            students = new();
            await LoadClassAsync();
            await LoadAsync();
        }

        public async Task LoadClassAsync()
        {
            try
            {
                var classDatas = await ClassService.GetAllClasss();

                classViewModels = Mapper.Map<List<ClassViewModel>>(classDatas) ?? new List<ClassViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LoadAsync()
        {
            loading = true;
            studentsViewModels.Clear();
            students = await StudentService.GetAllStudents();
            studentsViewModels = GetViewModels(students).OrderBy(c => c.Name).ToList();
            int stt = 1;
            foreach (var i in studentsViewModels)
            {

                i.stt = stt++;
            }
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
                model.RoomClassName = classViewModels?.FirstOrDefault(x => x.ID == c.ClassId)?.Name;
                models.Add(model);
                stt++;
            });
            return models;
        }

        void AddStudent()
        {
            var studentData = new Student();
            studentData.DateOfBirth = DateTime.Now;
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
            Student student = await StudentService.GetStudentByIdAsync(studentViewModel.ID);
            ShowStudentDetail(student);
        }

        async Task DeleteStudent(StudentViewModel studentViewModel)
        {
            Student studen = await StudentService.GetStudentByIdAsync(studentViewModel.ID);
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
