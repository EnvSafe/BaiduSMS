using System.Collections.Generic;
using System.Linq;
using Heroius.Extension;
using System.ComponentModel;

namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// 发送的消息
    /// </summary>
    public class SMSInfo : ObservableEntity
    {
        /// <summary>
        /// 调用ID
        /// </summary>
        [Description("调用ID")]
        public string InvokeID { get { return _InvokeID; } set { _InvokeID = value; RaisePropertyChangedEvent("InvokeID"); } }
        string _InvokeID;
        /// <summary>
        /// 采用的模板
        /// </summary>
        [Description("采用的模板编号")]
        public string TemplateCode { get { return _TemplateCode; } set { _TemplateCode = value; RaisePropertyChangedEvent("TemplateCode"); } }
        string _TemplateCode;
        /// <summary>
        /// 接受者号码
        /// </summary>
        [Description("接受者号码")]
        public string PhoneNumber { get { return _PhoneNumber; } set { _PhoneNumber = value; RaisePropertyChangedEvent("PhoneNumber"); } }
        string _PhoneNumber;
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
            return $"{{\"invokeId\":\"{InvokeID}\",\"phoneNumber\":\"{PhoneNumber}\",\"templateCode\":\"{TemplateCode}\"{((ContentVar == null || ContentVar.Count == 0) ? "" : $",\"contentVar\":{{{ContentVar.Select(kvp => $"\"{kvp.Key}\":\"{kvp.Value}\"").Merge(",")}}}")}}}";
        }
    }
}
