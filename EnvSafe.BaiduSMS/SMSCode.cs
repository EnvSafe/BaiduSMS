namespace EnvSafe.Baidu.SMS
{
    /// <summary>
    /// 保留的SMS发送结果代码
    /// <para>注意，服务返回的代码可能不在此枚举中</para>
    /// </summary>
    public enum SMSCode
    {
        调用成功 = 0,
        接口调用失败 = 1,
        请求的接口不存在 = 2,
        请求参数错误 = 3,
        系统未知错误 = 100
    }
}
