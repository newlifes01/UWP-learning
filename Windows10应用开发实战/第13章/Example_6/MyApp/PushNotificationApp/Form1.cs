using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net.Http;

namespace PushNotificationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void btnGetToken_Click(object sender, EventArgs e)
        {
            if (txtSID.Text.Length == 0 || txtPK.Text.Length == 0)
            {
                return;
            }

            btnGetToken.Enabled = false;
            AccessTokenData tkdata = await RequestTokenAsync(txtSID.Text.Trim(), txtPK.Text.Trim());
            if (tkdata != null)
            {
                txtToken.Text = tkdata.AccessToken;
            }
            this.Tag = tkdata;
            btnGetToken.Enabled = true;
        }


        async Task<AccessTokenData> RequestTokenAsync(string sid, string pk)
        {
            // 验证身份并获取令牌的URI
            Uri reqUri = new Uri("https://login.live.com/accesstoken.srf");
            AccessTokenData tokendt = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // POST的内容必须为application/x-www-form-urlencoded格式
                    Dictionary<string, string> formdata = new Dictionary<string, string>
                    {
                        { "grant_type", "client_credentials" },
                        { "client_id", sid },
                        { "client_secret", pk },
                        { "scope", "notify.windows.com" }
                    };
                    FormUrlEncodedContent content = new FormUrlEncodedContent(formdata);
                    // 发送请求
                    HttpResponseMessage response = null;
                    response = await client.PostAsync(reqUri, content);
                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        // 反序列化服务器回应的数据
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            DataContractJsonSerializer sz = new DataContractJsonSerializer(typeof(AccessTokenData));
                            tokendt = (AccessTokenData)sz.ReadObject(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return tokendt;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Length==0)
            {
                MessageBox.Show("请输入通知内容。"); return;
            }
            if (txtChannelUri.Text.Length == 0)
            {
                MessageBox.Show("请输入通道URI。"); return;
            }

            AccessTokenData tokendata = this.Tag as AccessTokenData;
            if (tokendata == null)
            {
                MessageBox.Show("请先获取访问令牌。"); return;
            }

            btnSend.Enabled = false;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(txtChannelUri.Text.Trim());
                req.Method = "POST";
                // 添加验证头
                req.Headers.Add("Authorization", string.Format("Bearer {0}", tokendata.AccessToken));
                req.ContentType = "text/xml";
                // 该HTTP标头表示通知类型为Toast
                req.Headers.Add("X-WNS-Type", "wns/toast");

                // 写入通知内容
                byte[] data = Encoding.UTF8.GetBytes(txtContent.Text);
                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                // 发送请求
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                // 输出响应头
                StringBuilder strbd = new StringBuilder();
                foreach (var hd in response.Headers.AllKeys)
                {
                    strbd.AppendLine(hd + ":" + req.Headers.Get(hd));
                }
                txtResponse.Text = strbd.ToString();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("发送成功。");
                }
                else
                {
                    MessageBox.Show("发送失败。");
                }
            }
            catch (Exception ex)
            {
                txtResponse.Text = ex.Message;
            }

            btnSend.Enabled = true;
        }

    }


    [DataContract]
    public class AccessTokenData
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}
