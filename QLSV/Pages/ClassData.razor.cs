﻿using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using QLSV.Data;

namespace QLSV.Pages
{
    public partial class ClassData : ComponentBase
    {
        [Inject] IClassService ClassService { get; set; }
        [Inject] ITeacherService TeacherService { get; set; }
        List<Class> classs { get; set; }
        List<ClassViewModel> classsViewModels { get; set; }
        EditClass editClass = new EditClass();
        TaskSearchClass taskSearchClasss = new TaskSearchClass();
        bool visible = false;
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            classsViewModels = new();
            classs = new();
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            loading = true;
            classsViewModels.Clear();
            //await Task.Delay(3000);
            classs = await ClassService.GetAllClasss();
            classsViewModels = GetViewModels(classs);
            loading = false;
            StateHasChanged();
        }

        List<ClassViewModel> GetViewModels(List<Class> datas)
        {
            var models = new List<ClassViewModel>();
            ClassViewModel model;
            int stt = 1;
            datas.ForEach(c =>
            {
                model = new ClassViewModel();
                model.stt = stt;
                model.ID = c.ID;
                model.Name = c.Name;
                model.Subject = c.Subject;

                models.Add(model);
                stt++;
            });
            return models;
        }

        void AddClass()
        {
            var classData = new Class();
            ShowClassDetail(classData);
        }

        void ShowClassDetail(Class data)
        {
            editClass.LoadData(data);
            visible = true;
        }

        async Task Save(Class data)
        {
            var resultAdd = await ClassService.AddOrUpdateClass(data);
            await LoadAsync();
            visible = false;
        }

        async Task Edit(ClassViewModel classViewModel)
        {
            //var classData = classs.FirstOrDefault(c => c.Id == classViewModel.id);
            Class class1 = await ClassService.GetClassByIdAsync(classViewModel.ID);
            ShowClassDetail(class1);
        }

        async Task DeleteClass(ClassViewModel classViewModel)
        {
            Class class1 = await ClassService.GetClassByIdAsync(classViewModel.ID);
            //await ClassService.DeleteClassAsync(studen);
            ClassService.DeleteClass(class1);
            await LoadAsync();
        }

        public class TaskSearchClass
        {
            public string? Name { get; set; }
        }

        async Task Search()
        {
            var name = taskSearchClasss.Name;
            if (name.IsNullOrEmpty())
            {
                classs = await ClassService.GetAllClasss();
                classsViewModels = GetViewModels(classs);
                return;
            }
            classsViewModels.Clear();
            classs = await ClassService.GetListClasssBySearchAsync(name);
            classsViewModels = GetViewModels(classs);
            StateHasChanged();
        }

    }

}