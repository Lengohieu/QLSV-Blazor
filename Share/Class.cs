using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf.Grpc;
namespace Share
{
    [DataContract]
    public class ClassGrpc
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Subject { get; set; }

        [DataMember(Order = 4)]
        public string TeacherId { get; set; }
    }
    [DataContract]
    public class ListClass
    {
        [DataMember(Order = 1)]
        public List<ClassGrpc> Classs { get; set; } = new List<ClassGrpc>();
    }

    [DataContract]
    public class ClassFilterGrpc
    {
        [DataMember(Order = 1)]
        public String Name { get; set; }
        [DataMember(Order = 2)]
        public DateTime? StartDate { get; set; }
        [DataMember(Order = 3)]
        public DateTime? EndDate { get; set; }

        [DataMember(Order = 4)]
        public int TeacherId { get; set; } = -1;
    }
    [DataContract]
    public class IntGrpc
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public Empty Empty { get; set; }

    }

    [DataContract]
    public class PageViewGrpc2
    {
        [DataMember(Order = 1)]
        public List<ClassGrpc> Classs { get; set; } = new List<ClassGrpc>();

        [DataMember(Order = 2)]
        public ClassFilterGrpc ClassFilterGrpc { get; set; }

        [DataMember(Order = 3)]
        public int PageNumber { get; set; }

        [DataMember(Order = 4)]
        public int PageSize { get; set; }

        [DataMember(Order = 5)]
        public int PageCount { get; set; }

        [DataMember(Order = 6)]
        public Empty Empty { get; set; }
    }

        [ServiceContract]
    public interface ClassProto
    {
        [OperationContract]
        ListClass GetAllClasss(Empty request, CallContext context = default);

        [OperationContract]
        BooleanGrpc AddNewClass(ClassGrpc request, CallContext context = default);

        [OperationContract]
        BooleanGrpc DeleteClass(ClassGrpc request, CallContext context = default);

        [OperationContract]
        BooleanGrpc UpdateClass(ClassGrpc request, CallContext context = default);

        [OperationContract]
        ClassGrpc GetClassById(IntGrpc request, CallContext context = default);

        [OperationContract]
        Task<PageViewGrpc2> GetDataPageAsync(PageViewGrpc2 pageViewGrpc, CallContext context = default);


        [OperationContract]
        ListClass GetDataBySearchAsync(string pageViewGrpc, CallContext context = default);
    }
}
