using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvSafe.Baidu
{
    /// <summary>
    /// 鉴权认证配置
    /// </summary>
    public class BCESettings
    {
        /// <summary>
        /// 用户AK
        /// </summary>
        public string AccessKeyId { get; set; }
        /// <summary>
        /// 用户SK
        /// </summary>
        public string SecretAccessKey { get; set; }

        /// <summary>
        /// 认证访问超时，单位：秒
        /// </summary>
        public int ExpirationPeriodInSeconds { get; set; } = 1800;
    }
}
