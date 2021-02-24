using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GettingStarted
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(ICalculatorDuplexCallback))]
    public interface ICalculatorDuplex
    {
        [OperationContract(IsOneWay = true)]
        void Clear();
        [OperationContract]
        string AddTo(double n, out string Extra, ref int AddStr);
        [OperationContract(IsOneWay = true)]
        void SubtractFrom(double n);
        [OperationContract(IsOneWay = true)]
        void MultiplyBy(double n);
        [OperationContract(IsOneWay = true)]
        void DivideBy(double n);
        [OperationContract]
        Student ModifyStudent(Student stu);
        [OperationContract]
        GetAirfareResponse GetAirfare(GetAirfareRequest request);
    }
    public interface ICalculatorDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void Equals(double result);
        [OperationContract(IsOneWay = true)]
        void Equation(string eqn);
        [OperationContract(IsOneWay = true)]
        void ShowStu(Student stu);
    }
    [DataContract]
    public class Student
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int age { get; set; }
    }
    [MessageContract]
    public class GetAirfareRequest
    {
        [MessageHeader] public DateTime date;
        [MessageBodyMember] public string Data;
    }
    [MessageContract]
    public class GetAirfareResponse
    {
        [MessageBodyMember(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)] public string Content;
    }
}
