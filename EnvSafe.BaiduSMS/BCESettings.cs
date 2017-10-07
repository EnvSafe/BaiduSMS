using Heroius.Extension;

namespace EnvSafe.Baidu
{
    /// <summary>
    /// 鉴权认证配置
    /// </summary>
    public class BCESettings: ObservableEntity
    {
        /// <summary>
        /// 用户AK
        /// </summary>
        public string AccessKeyId { get { return _AccessKeyId; } set { _AccessKeyId = value; RaisePropertyChangedEvent("AccessKeyId"); } } string _AccessKeyId = "";
        /// <summary>
        /// 用户SK
        /// </summary>
        public string SecretAccessKey { get { return _SecretAccessKey; } set { _SecretAccessKey = value; RaisePropertyChangedEvent("SecretAccessKey"); } } string _SecretAccessKey = "";

        /// <summary>
        /// 认证访问超时，单位：秒
        /// </summary>
        public int ExpirationPeriodInSeconds { get { return _ExpirationPeriodInSeconds; } set { _ExpirationPeriodInSeconds = value; RaisePropertyChangedEvent("ExpirationPeriodInSeconds"); } } int _ExpirationPeriodInSeconds = 1800;
    }
}
