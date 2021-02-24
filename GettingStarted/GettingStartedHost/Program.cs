using GettingStarted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: 创建一个URI作为基地地址。
            Uri baseAddress = new Uri("http://localhost:3569/GettingStarted/");

            // Step 2: 创建ServiceHost实例。
            ServiceHost selfHost = new ServiceHost(typeof(CalculatorService), new Uri("http://localhost:5999/GettingStarted/"));
            // Step 2: 创建ServiceHost实例。
            ServiceHost DuplexHost = new ServiceHost(typeof(DuplexCalculatorService), baseAddress);

            try
            {
                // Step 3: 添加服务端点。
                selfHost.AddServiceEndpoint(typeof(ICalculator), new BasicHttpBinding() { TransferMode = TransferMode.Streamed }, "CalculatorService");
                DuplexHost.AddServiceEndpoint(typeof(ICalculatorDuplex), new WSDualHttpBinding(), "DuplexCalculatorService");

                // Step 4: 使元数据交换。
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5: 启动服务。
                selfHost.Open();
                DuplexHost.Open();
                Console.WriteLine("服务已经准备好。");

                // 关闭ServiceHost以停止服务。
                Console.WriteLine("按<Enter>终止服务。");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
                DuplexHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
                DuplexHost.Close();
            }
        }
    }
}
