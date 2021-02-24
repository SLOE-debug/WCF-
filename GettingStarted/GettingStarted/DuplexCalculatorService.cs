using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GettingStarted
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class DuplexCalculatorService : ICalculatorDuplex
    {
        double result;
        string equation;
        ICalculatorDuplexCallback callback = null;

        public DuplexCalculatorService()
        {
            result = 0.0D;
            equation = result.ToString();
            callback = OperationContext.Current.GetCallbackChannel<ICalculatorDuplexCallback>();
        }

        public void Clear()
        {
            callback.Equation(equation + " = " + result.ToString());
            result = 0.0D;
            equation = result.ToString();
        }

        public string AddTo(double n, out string Extra, ref int AddStr)
        {
            result += n;
            equation += " + " + n.ToString();
            callback.Equals(result);
            Extra = "out参数";
            AddStr = 10;
            return "";
        }

        public void SubtractFrom(double n)
        {
            result -= n;
            equation += " - " + n.ToString();
            callback.Equals(result);
        }

        public void MultiplyBy(double n)
        {
            result *= n;
            equation += " * " + n.ToString();
            callback.Equals(result);
        }

        public void DivideBy(double n)
        {
            result /= n;
            equation += " / " + n.ToString();
            callback.Equals(result);
        }

        public Student ModifyStudent(Student stu)
        {
            stu.name = "李四";
            callback.ShowStu(stu);
            return stu;
        }

        public GetAirfareResponse GetAirfare(GetAirfareRequest request)
        {
            return new GetAirfareResponse() { Content = "Success OK！" };
        }
    }
}
