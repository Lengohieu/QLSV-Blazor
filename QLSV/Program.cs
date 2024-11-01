using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QLSV.Data;
using QLSV.Mapper;
using Syncfusion.Blazor;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAntDesign();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IClassService, ClassService>();
builder.Services.AddSingleton<ITeacherService, TeacherService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
