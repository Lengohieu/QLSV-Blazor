using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace GrpcService.Models.Mappings
{
    class TeacherMapping : ClassMapping<Teacher>
    {
        public TeacherMapping()
        {
            Table("Teacher");

            Id(x => x.ID, map => map.Column("ID"));

            Property(x => x.Name);

            Property(x => x.DateOfBirth);

            Bag(x => x.RoomClasses, c =>
            {
                c.Key(k => k.Column("Teacher"));
                c.Cascade(Cascade.All | Cascade.DeleteOrphans);
                c.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}