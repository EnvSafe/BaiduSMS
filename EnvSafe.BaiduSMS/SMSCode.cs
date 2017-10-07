using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvSafe.Baidu.SMS
{
    public enum SMSCode
    {
        调用成功 = 0,
        接口调用失败 = 1,
        请求的接口不存在 = 2,
        请求参数错误 = 3,
        系统未知错误 = 100
    }
}
