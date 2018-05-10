using System;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace BgServiceTasks
{
    public sealed class ServiceTask : IBackgroundTask
    {
        BackgroundTaskDeferral taskdef = null;
        string serviceName = null;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            taskdef = taskInstance.GetDeferral();
            taskInstance.Canceled += OnCancel;

            // 获取App Service连接相关的对象
            AppServiceTriggerDetails details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (details != null)
            {
                // 获取服务名
                serviceName = details.Name;
                // 获取连接对象
                AppServiceConnection connection = details.AppServiceConnection;
                // 处理相关事件
                connection.RequestReceived += Connection_RequestReceived;
            }
        }

        private void OnCancel(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            taskdef.Complete();
        }

        private async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var msgdef = args.GetDeferral();
            // 获取参数
            if (args.Request.Message.ContainsKey("num1") == false || args.Request.Message.ContainsKey("num2") == false)
            {
                msgdef.Complete();
                taskdef.Complete();
                return;
            }

            int a = Convert.ToInt32(args.Request.Message["num1"]);
            int b = Convert.ToInt32(args.Request.Message["num2"]);

            int result = default(int);
            // 判断计算类型
            switch (serviceName)
            {
                case "Add": //加法
                    result = a + b;
                    break;
                case "Sub": //减法
                    result = a - b;
                    break;
                case "Mul": //乘法
                    result = a * b;
                    break;
                case "Div": //除法
                    result = a / b;
                    break;
                default:
                    result = 0;
                    break;
            }
            // 将计算结果发回给客户端
            ValueSet msg = new ValueSet();
            msg.Add("res", result);
            await args.Request.SendResponseAsync(msg);
            msgdef.Complete();
            taskdef.Complete();
        }
    }
}
