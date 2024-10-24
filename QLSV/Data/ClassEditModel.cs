using System.ComponentModel.DataAnnotations;

namespace QLSV.Data
{
    public class ClassEditModel
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string TeacherId { get; set; }
    }
}
