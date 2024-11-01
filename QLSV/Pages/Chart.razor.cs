using AntDesign.Charts;
using Title = AntDesign.Charts.Title;
using Microsoft.AspNetCore.Components;
using QLSV.Data;
using System.Runtime.CompilerServices;
using AutoMapper;
using AntDesign;
using System.Text.RegularExpressions;
namespace QLSV.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] IStudentService StudentService { get; set; }
        [Inject] IClassService ClassService { get; set; }
        List<Student> students { get; set; }
        List<StudentViewModel> studentsViewModels { get; set; }
        List<ClassViewModel> classViewModels { get; set; }
        bool loading;
        object[] data1;
        int countLop1 = 0;
        int countLop2 = 0;
        int countLop3 = 0;
        int countLop4 = 0;
        int countLop5 = 0;
        int countLop6 = 0;
        int countLop7 = 0;
        int countLop8 = 0;
        int countLop9 = 0;

        protected override async Task OnInitializedAsync()
        {
            studentsViewModels = new();
            students = new();
            await LoadClassAsync();
            await LoadAsync();
            ConfigureData();
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
        public void ConfigureData()
        {
            foreach (var studentViewModel in studentsViewModels)
            {
                var ClassName = studentViewModel.RoomClassName;
                Match match = Regex.Match(ClassName, @"Lớp \d+");

                if (match.Success)
                {
                    string result = match.Value;
                    Console.WriteLine(result);
                    studentViewModel.RoomClassName = result;
                }
            }
            countLop1 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 1");
            countLop2 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 2");
            countLop3 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 3");
            countLop4 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 4");
            countLop5 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 5");
            countLop6 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 6");
            countLop7 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 7");
            countLop8 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 8");
            countLop9 = studentsViewModels.Count(c => c.RoomClassName == "Lớp 9");
            data1 = new object[]
            {
                new { year = "Lớp 1", value = countLop1 },
                new { year = "Lớp 2", value = countLop2 },
                new { year = "Lớp 3", value = countLop3 },
                new { year = "Lớp 4", value = countLop4 },
                new { year = "Lớp 5", value = countLop5 },
                new { year = "Lớp 6", value = countLop6 },
                new { year = "Lớp 7", value = countLop7 },
                new { year = "Lớp 8", value = countLop8 },
                new { year = "Lớp 9", value = countLop9 }
            };
        }
        #region Example 1

        ColumnConfig config1 = new ColumnConfig
        {
            Title = new Title
            {
                Visible = true,
                Text = "Set active state"
            },
            Description = new Description
            {
                Visible = true,
                Text = "Set the single value active state."
            },
            XField = "year",
            YField = "value"
        };

        #endregion Example 1

        private DateTime selectedDate = DateTime.Today;
        private IChartComponent chartRef;

        private bool isResetUsed = false;
        public void ResetData()
        {
            if (isResetUsed) return;

            if (selectedDate.Month < 6)
            {
                ConfigureData();
            }
            else
            {

                //int tempCount = countLop1;
                countLop9 = countLop8;
                countLop8 = countLop7;
                countLop7 = countLop6;
                countLop6 = countLop5;
                countLop5 = countLop4;
                countLop4 = countLop3;
                countLop3 = countLop2;
                countLop2 = countLop1;
                countLop1 = 0;

               data1 = new object[]
               {
                  new { year = "Lớp 1", value = countLop1 },
                  new { year = "Lớp 2", value = countLop2 },
                  new { year = "Lớp 3", value = countLop3 },
                  new { year = "Lớp 4", value = countLop4 },
                  new { year = "Lớp 5", value = countLop5 },
                  new { year = "Lớp 6", value = countLop6 },
                  new { year = "Lớp 7", value = countLop7 },
                  new { year = "Lớp 8", value = countLop8 },
                  new { year = "Lớp 9", value = countLop9 }
               };
                chartRef.ChangeData(data1);
            }

            isResetUsed = true;
        }
    }
}