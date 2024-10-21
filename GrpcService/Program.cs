using GrpcService.Services;
using GrpcService;
using NHibernate;
using ProtoBuf.Grpc.Server;

namespace GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddGrpc();
            builder.Services.AddCodeFirstGrpc();
            builder.Services.AddSingleton<ISessionFactory>(NHibernateConfig.BuildSessionFactory());
            builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
            builder.Services.AddSingleton<IClassRepository, ClassRepository>();

            var app = builder.Build();

            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<StudentService>();
            app.MapGrpcService<ClassService>();


            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}