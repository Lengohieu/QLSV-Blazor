using FluentNHibernate.Mapping;

namespace QLSV.Data
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.ID).GeneratedBy.Assigned().Column("ID");
            Map(x => x.Name).Column("Name");
            Map(x => x.DateOfBirth).Column("DateOfBirth");
            Map(x => x.Address).Column("Address");
            Map(x => x.ClassId).Column("ClassId");
            Table("Student");
        }
    }
}
