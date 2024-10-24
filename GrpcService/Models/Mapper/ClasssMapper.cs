using GrpcService.Models;
using Share;

namespace GrpcService.Models.Mapper
{
    public class ClasssMapper
    {
        public ClassGrpc ClassToClassGrpc(Class _class)
        {
            ClassGrpc classGrpc = new ClassGrpc();
            classGrpc.Id = _class.ID;
            classGrpc.Name = _class.Name;
            classGrpc.Subject = _class.Subject;
            classGrpc.TeacherId = _class.TeacherId;
            return classGrpc;
        }
        public Class ClassGrpcToClass(ClassGrpc classGrpc)
        {
            Class _class = new Class();
            _class.ID = classGrpc.Id;
            _class.Name = classGrpc.Name;
            _class.Subject = classGrpc.Subject;
            return _class;
        }
    }
}
