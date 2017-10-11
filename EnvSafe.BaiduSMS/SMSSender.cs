using Newtonsoft.Json;
using System;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// 短消息服务访问器（client）
    /// </summary>
    public class SMSSender
    {
        /// <summary>
        /// 实例化 短消息服务访问器（client）
        /// </summary>
        /// <param name="auth">鉴权（profile）</param>
        /// <param name="settings">访问设置</param>
        public SMSSender(BCE auth, SMSSettings settings)
        {
            Auth = auth;
            Settings = settings;
        }

        /// <summary>
        /// 当前 SMS 配置
        /// </summary>
        public SMSSettings Settings { get; set; }
        /// <summary>
        /// 认证实体
        /// </summary>
        public BCE Auth { get; set; }

        /// <summary>
        /// 使用 POST 执行短信下发
        /// </summary>
        /// <param name="smsinfo">消息（message）</param>
        /// <returns></returns>
        public SMSResult SendMessage(SMSInfo smsinfo, string url = "bce/v2/message")
        {
            var time = DateTime.UtcNow;

            string CanonicalRequest = $"POST\n/{url}\n\nhost:{Settings.APIServer}";

            string Content = smsinfo.GetJson();
            byte[] ContentByte = Encoding.UTF8.GetBytes(Content);

            string requestUri = new Uri($"http://{Settings.APIServer}/{url}").ToString();
            HttpWebRequest smsRequest = (WebRequest.Create(requestUri) as HttpWebRequest);
            smsRequest.Host = Settings.APIServer;
            smsRequest.Method = "POST";
            smsRequest.KeepAlive = false;
            smsRequest.Timeout = Settings.Timeout;
            smsRequest.ContentType = "application/json";
            smsRequest.ContentLength = ContentByte.Length;
            smsRequest.Headers["x-bce-date"] = time.ToString("YYYY-MM-DD");
            smsRequest.Headers["Authorization"] = Auth.GetAuthString(time, CanonicalRequest, "host");
            smsRequest.Headers["x-bce-content-sha256"] = GetSHA256hash(Content);

            System.IO.Stream StreamData = smsRequest.GetRequestStream();
            StreamData.Write(ContentByte, 0, ContentByte.Length);
            StreamData.Close();

            HttpWebResponse smsResponse = (HttpWebResponse)smsRequest.GetResponse();
            System.IO.Stream receiveStream = null;
            System.IO.StreamReader responseReader = null;
            if (smsResponse.Headers.Get("Content-Encoding") == "gzip")
            {
                System.IO.Stream zipStream = smsResponse.GetResponseStream();
                receiveStream = new GZipStream(zipStream, CompressionMode.Decompress);
            }
            else
            {
                receiveStream = smsResponse.GetResponseStream();
            }
            responseReader = new System.IO.StreamReader(receiveStream);
            string responseString = responseReader.ReadToEnd();
            return JsonConvert.DeserializeObject<SMSResult>(responseString);
        }

        #region Utility

        /// <summary>
        /// SHA256 HASH
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetSHA256hash(string input)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(input);
            SHA256 sha256 = new SHA256Managed();
            sha256.ComputeHash(clearBytes);
            byte[] hashedBytes = sha256.Hash;
            sha256.Clear();
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return output;
        }

        #endregion
    }
}
