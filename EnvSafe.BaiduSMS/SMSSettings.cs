using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// SMS 配置
    /// </summary>
    public class SMSSettings
    {
        /// <summary>
        /// SMS API的服务域名
        /// <para>非BCC用户和“华北-北京”区域BCC用户可使用北京域名；如您使用“华南-广州”区域BCC服务请访问广州域名</para>
        /// </summary>
        public string APIServer { get; set; } = "sms.bj.baidubce.com";
        /// <summary>
        /// 请求超时，单位：毫秒
        /// </summary>
        public int Timeout { get; set; } = 60000;
    }
}
