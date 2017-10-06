using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvSafe.BaiduSMS
{
    public class SMSInfo
    {
        /// <summary>
        /// 模板
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string ContentVar { get; set; }
    }
}
