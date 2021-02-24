using GettingStartedClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedClient
{
    public class CallbackHandler : ICalculatorDuplexCallback
    {
        public void Result(double result)
        {
            Console.WriteLine("Result({0})", result);
        }

        public void Equation(string equation)
        {
            Console.WriteLine("Equation({0}", equation);
        }

        public void Equals(double result)
        {
            Console.WriteLine("Equals：{0}", result);
        }

        public void ShowStu(Student stu)
        {
            Console.WriteLine("姓名：{0} 年龄：{1}", stu.name, stu.age);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference2.CalculatorClient client = new ServiceReference2.CalculatorClient();
            using (var stream = File.OpenRead(@"C:\Users\c0375.LINDAPATENT1009\Desktop\my.ini"))
            {
                var MysqlConfigFile = client.EchoStream(stream);
                int Len = 0;
                byte[] filebf = new byte[0];
                do
                {
                    var bf = new byte[1024 * 1024 * 5];
                    Len = MysqlConfigFile.Read(bf, 0, bf.Length);
                    if (Len > 0)
                    {
                        filebf = filebf.Concat(bf.Take(Len)).ToArray();
                    }
                    bf = new byte[4096];
                } while (Len > 0);
                Console.WriteLine("读取完成！");
                var MysqlFile = File.Create(@"C:\Users\c0375.LINDAPATENT1009\Desktop\mysql.ini");
                MysqlFile.Write(filebf, 0, filebf.Length);
                MysqlFile.Close();
            }
            Console.Read();
            //// 构造InstanceContext以处理回调接口上的消息。
            //InstanceContext instanceContext = new InstanceContext(new CallbackHandler());
            //// 创建一个客户端。
            //CalculatorDuplexClient client = new CalculatorDuplexClient(instanceContext);

            //Console.WriteLine("Press <ENTER> to terminate client once the output is displayed.");
            //Console.WriteLine();
            // Call the AddTo service operation.
            //double value = 100.00D;
            //string Extra;
            //int AddStr = 0;
            //client.AddTo(value, ref AddStr, out Extra);
            //Console.WriteLine("Ref：" + AddStr);
            //Console.WriteLine("Out：" + Extra);

            //client.ModifyStudent(new Student() { name = "张三", age = 16 });

            //Console.WriteLine(client.GetAirfare(DateTime.Now, "巴拉巴拉"));

            //// Call the SubtractFrom service operation.
            //value = 50.00D;
            //client.SubtractFrom(value);

            //// Call the MultiplyBy service operation.
            //value = 17.65D;
            //client.MultiplyBy(value);

            //// Call the DivideBy service operation.
            //value = 2.00D;
            //client.DivideBy(value);

            //// Complete equation.
            //client.Clear();

            //Console.ReadLine();

            ////Closing the client gracefully closes the connection and cleans up resources.
            //client.Close();
        }
    }
}
