using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class MessageEntity
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MsgType
        {
            /// <summary>
            /// 接收到的消息
            /// </summary>
            From,
            /// <summary>
            /// 发送的消息
            /// </summary>
            To
        }


        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MessageType { get; set; }
    }
}
