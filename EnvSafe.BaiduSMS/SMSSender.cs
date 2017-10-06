using System;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EnvSafe.BaiduSMS
{
    public class SMSSender
    {

        public string SendSMSWEBAPI(SMSInfo smsinfo)
        {
            try
            {
                System.IO.Stream receiveStream = null;
                System.IO.StreamReader responseReader = null;
                string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                // string timestamp2 As String = Date.UtcNow.ToString("yyyy-MM-dd")
                string SecretAccessKey = "申请所得";
                string AccessKeyId = "申请所得";
                //   '  Dim authStringPrefix As String = "bce-auth-v1/{accessKeyId}/{timestamp}/{expirationPeriodInSeconds}"
                string authStringPrefix = string.Format("bce-auth-v1/{1}/{0}/1800", timestamp, AccessKeyId);// '这里要改
                //  ' Dim CanonicalRequest As String = "HTTP Method + "\n" + CanonicalURI + "\n" + CanonicalQueryString + "\n" + CanonicalHeaders"
                string CanonicalRequest = string.Format("POST" + "\n" + "/v1/message" + "\n" + "\n" + "host:sms.bj.baidubce.com");
                string SigningKey = GetSigningKeyByHMACSHA256HEX(SecretAccessKey, authStringPrefix);
                string Signature = GetSignatureByHMACSHA256HEX(SigningKey, CanonicalRequest);
                string Content = string.Empty;

                // Content = "{ \"templateId\":" + smsinfo.TemplateId + ",\"receiver\":" + smsinfo.Receiver + ",\"contentVar\":" + smsinfo.ContentVar + "}";
                Content = "{" + "\"" + "templateId" + "\"" + ":" + smsinfo.TemplateId + "," + "\"" + "receiver" + "\"" + ":" + smsinfo.Receiver + "," + "\"" + "contentVar" + "\"" + ":" + smsinfo.ContentVar + "}";
                string temp = Content.ToString();
                byte[] ContentByte = Encoding.UTF8.GetBytes(temp);
                string GetOrderURL = new Uri("http://sms.bj.baidubce.com/v1/message").ToString();
                System.Net.HttpWebRequest HttpWReq = (WebRequest.Create(GetOrderURL) as System.Net.HttpWebRequest);
                //' HttpWReq.Timeout = 600 * 1000 ''一分钟查询
                HttpWReq.ContentLength = ContentByte.Length;
                HttpWReq.ContentType = "application/json";
                HttpWReq.Headers["x-bce-date"] = timestamp;
                HttpWReq.Headers["Authorization"] = string.Format("bce-auth-v1/{2}/{0}/1800/host/{1}", timestamp, Signature, AccessKeyId);
                HttpWReq.Host = "sms.bj.baidubce.com";
                HttpWReq.Method = "POST";
                HttpWReq.KeepAlive = false;
                System.IO.Stream StreamData = HttpWReq.GetRequestStream();
                StreamData.Write(ContentByte, 0, ContentByte.Length);
                StreamData.Close();
                System.Net.HttpWebResponse HttpWRes = (System.Net.HttpWebResponse)HttpWReq.GetResponse();

                if (HttpWRes.Headers.Get("Content-Encoding") == "gzip")
                {
                    System.IO.Stream zipStream = HttpWRes.GetResponseStream();
                    receiveStream = new GZipStream(zipStream, CompressionMode.Decompress);
                }
                else
                {
                    receiveStream = HttpWRes.GetResponseStream();
                }
                responseReader = new System.IO.StreamReader(receiveStream);
                string responseString = responseReader.ReadToEnd();
                return responseString;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取 hash
        /// </summary>
        /// <param name="SecretAccessKey"></param>
        /// <param name="authStringPrefix"></param>
        /// <returns></returns>
        public string GetSigningKeyByHMACSHA256HEX(String SecretAccessKey, String authStringPrefix)
        {
            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(SecretAccessKey));
            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(authStringPrefix));
            string SigningKey = HashEncode(LiveHash);
            return SigningKey;
        }
        /// <summary>
        /// 获取 hash
        /// </summary>
        /// <param name="SigningKey"></param>
        /// <param name="CanonicalRequest"></param>
        /// <returns></returns>
        public string GetSignatureByHMACSHA256HEX(String SigningKey, String CanonicalRequest)
        {
            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(SigningKey));
            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(CanonicalRequest));
            string Signature = HashEncode(LiveHash);
            return Signature;
        }

        // '将字符串全部变成小写。
        public string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// reverse? 获取输入文本的 hash 值（移除了 - ）
        /// </summary>
        /// <param name="input">输入文本</param>
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
    }
}
