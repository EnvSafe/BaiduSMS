using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// 发送操作的结果
    /// </summary>
    public class SMSResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// 服务生成的流水号
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestID { get; set; }
    }
}
