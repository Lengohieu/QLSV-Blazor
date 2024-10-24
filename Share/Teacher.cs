using ProtoBuf.Grpc;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Share
{

    [DataContract]
    public class TeacherGrpc
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public DateTime DateOfBirth { get; set; }
    }

    [DataContract]
    public class ListTeacher
    {
        [DataMember(Order = 1)]
        public List<TeacherGrpc> Teachers { get; set; } = new List<TeacherGrpc>();
    }

    [DataContract]
    public class PageViewGrpc1
    {
        [DataMember(Order = 1)]
        public List<TeacherGrpc> Teachers { get; set; } = new List<TeacherGrpc>();

        [DataMember(Order = 2)]
        public TeacherFilterGrpc TeacherFilterGrpc { get; set; }

        [DataMember(Order = 3)]
        public int PageNumber { get; set; }

        [DataMember(Order = 4)]
        public int PageSize { get; set; }

        [DataMember(Order = 5)]
        public int PageCount { get; set; }

        [DataMember(Order = 6)]
        public Empty Empty { get; set; }
    }
    [DataContract]
    public class TeacherFilterGrpc
    {
        [DataMember(Order = 1)]
        public String Name { get; set; }
        [DataMember(Order = 2)]
        public DateTime? StartDate { get; set; }
        [DataMember(Order = 3)]
        public DateTime? EndDate { get; set; }

        public TeacherFilterGrpc()
        {

        }
    }

    [ServiceContract]
    public interface TeacherProto
    {
        [OperationContract]
        ListTeacher GetAllTeachers(Empty request, CallContext context = default);

        [OperationContract]
        BooleanGrpc AddNewTeacher(TeacherGrpc request, CallContext context = default);

        [OperationContract]
        BooleanGrpc DeleteTeacher(TeacherGrpc request, CallContext context = default);

        [OperationContract]
        BooleanGrpc UpdateTeacher(TeacherGrpc request, CallContext context = default);

        [OperationContract]
        TeacherGrpc GetTeacherById(IntGrpc request, CallContext context = default);

        [OperationContract]
        Task<PageViewGrpc1> GetDataPageAsync(PageViewGrpc1 pageViewGrpc, CallContext context = default);


        [OperationContract]
        ListTeacher GetDataBySearchAsync(string pageViewGrpc, CallContext context = default);
    }
}
