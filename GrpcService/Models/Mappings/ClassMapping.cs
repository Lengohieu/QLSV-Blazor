using NHibernate.Mapping.ByCode.Conformist;

namespace GrpcService.Models.Mappings
{
    class ClassMapping : ClassMapping<Class>
    {
        public ClassMapping()
        {
            Table("RoomClass");
            Id(x => x.ID, m => m.Column("ID"));
            Property(x => x.Name);
            Property(x => x.Subject);
            Property(x => x.TeacherId);

            //ManyToOne(x => x.Teacher, m => m.Column("TeacherId"));
            //Bag(x => x.Students, c =>
            //{
            //    c.Key(k => k.Column("Class"));
            //    c.Cascade(Cascade.All
            //              | Cascade.DeleteOrphans);
            //}, r => r.OneToMany());

        }
    }
}