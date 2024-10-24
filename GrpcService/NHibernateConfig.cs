using Microsoft.Extensions.Configuration;
using NHibernate;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using Configuration = NHibernate.Cfg.Configuration;
using GrpcService.Models.Mappings;
namespace GrpcService
{
    public class NHibernateConfig
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var cfg = new Configuration();

            string connectionString = "Server=HIEEUS;Database=QuanLySinhVien;Trusted_Connection=True;";
            cfg.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
            });
            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(StudentMapping));
            mapper.AddMapping(typeof(TeacherMapping));
            mapper.AddMapping(typeof(ClassMapping));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(domainMapping);
            return cfg.BuildSessionFactory();
        }
    }
}
