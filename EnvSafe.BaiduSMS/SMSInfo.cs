using System.Collections.Generic;
using System.Linq;
using Heroius.Extension;

namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// 发送的消息
    /// </summary>
    public class SMSInfo
    {
        /// <summary>
        /// 调用ID
        /// </summary>
        public string InvokeID { get; set; }
        /// <summary>
        /// 采用的模板
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 模板变量
        /// </summary>
        public Dictionary<string, string> ContentVar { get; set; }

        /// <summary>
        /// 获取 json 格式文本，作为请求主体
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            return $"{{\"invokeId\":\"{InvokeID}\",\"phoneNumber\":\"{PhoneNumber}\",\"templateCode\":\"{TemplateCode}\"{(ContentVar==null?"": $",\"contentVar\":{{{ContentVar.Select(kvp=>$"\"{kvp.Key}\":\"{kvp.Value}\"").Merge(",")}}}")}}}";
        }
    }
}
