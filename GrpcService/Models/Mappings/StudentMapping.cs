using NHibernate.Mapping.ByCode.Conformist;

namespace GrpcService.Models.Mappings
{
    class StudentMapping : ClassMapping<Student>
    {
        public StudentMapping()
        {

            Table("Student");
            Id(p => p.ID, map => map.Column("ID"));
            Property(p => p.Name);
            Property(p => p.DateOfBirth);
            Property(p => p.Address);
            Property(p => p.ClassId);
            //ManyToOne(p => p.Class, m => m.Column("RoomClass"));
        }
    }
}