using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EnvSafe.Baidu.SMS
{
    public class SMSResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("requestId")]
        public string RequestID { get; set; }
    }
}
