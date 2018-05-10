using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.ServiceModel.Description;

namespace TransferServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri localUri = new Uri("http://192.168.1.100:98");
            // 开始运行WCF服务
            using (WebServiceHost host = new WebServiceHost(typeof(Service), localUri))
            {
                // 配置缓冲区的最大值
                WebHttpBinding binding = new WebHttpBinding();
                binding.MaxReceivedMessageSize = 500 * 1024 * 1024;
                host.AddServiceEndpoint(typeof(IService), binding, "");

                host.Opened += (a, b) => Console.WriteLine("服务已启动。\n上传地址：" + localUri + "upload");
                host.Closed += (a, b) => Console.WriteLine("服务已关闭。");
                try
                {
                    // 打开服务
                    host.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract, WebInvoke(UriTemplate = "/upload")]
        void UploadFile(Stream content);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        public void UploadFile(Stream content)
        {
            string fileName = "";
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            // 从标头获取文件名
            fileName = request.Headers["file_name"];
            if (string.IsNullOrEmpty(fileName))
            {
                Guid g = Guid.NewGuid();
                fileName = g.ToString();
            }
            // 开始接收文件
            try
            {
                // 获取用户文档库位置
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string newFilePath = Path.Combine(docPath, fileName);
                // 如果文件存在，将其删除
                if (File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }
                using (FileStream fileStream = File.OpenWrite(newFilePath))
                {
                    // 从客户端上传的流中将数据复制到文件流中
                    content.CopyTo(fileStream);
                }
                Console.WriteLine(string.Format("在{0}成功接收文件。", DateTime.Now.ToLongTimeString()));
                // 向客户端发送回应消息
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // 处理错误
                Console.WriteLine(ex.Message);
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                WebOperationContext.Current.OutgoingResponse.StatusDescription = ex.Message;
            }
        }
    }

}
