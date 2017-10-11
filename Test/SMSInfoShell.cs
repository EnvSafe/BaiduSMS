using EnvSafe.Baidu.SMS;
using Heroius.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class SMSInfoShell : ObservableEntity
    {
        SMSInfo info;

        public SMSInfoShell(SMSInfo info)
        {
            this.info = info;
        }

        /// <summary>
        /// 调用ID
        /// </summary>
        [Description("调用ID")]
        public string InvokeID { get { return info.InvokeID; } set { info.InvokeID = value; RaisePropertyChangedEvent("InvokeID"); } }
        /// <summary>
        /// 采用的模板
        /// </summary>
        [Description("采用的模板编号")]
        public string TemplateCode { get { return info.TemplateCode; } set { info.TemplateCode = value; RaisePropertyChangedEvent("TemplateCode"); } }
        /// <summary>
        /// 接受者号码
        /// </summary>
        [Description("接受者号码")]
        public string PhoneNumber { get { return info.PhoneNumber; } set { info.PhoneNumber = value; RaisePropertyChangedEvent("PhoneNumber"); } }
        /// <summary>
        /// 模板变量的json表述
        /// </summary>
        [Description("模板变量的json表述")]
        public string ContentJson
        {
            get
            {
                return (info.ContentVar == null || info.ContentVar.Count == 0) ? "" : $"{{{info.ContentVar.Select(kvp => $"\"{kvp.Key}\":\"{kvp.Value}\"").Merge(",")}}}";
            }
            set
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                info.ContentVar = obj;
                RaisePropertyChangedEvent("ContentJson");
            }
        }
    }
}
