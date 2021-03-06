﻿using Heroius.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EnvSafe.Baidu
{
    /// <summary>
    /// 百度鉴权验证
    /// </summary>
    public class BCE
    {
        /// <summary>
        /// 百度鉴权验证
        /// </summary>
        /// <param name="settings">鉴权认证配置</param>
        public BCE(BCESettings settings)
        {
            Settings = settings;
        }
        /// <summary>
        /// 鉴权认证配置
        /// </summary>
        public BCESettings Settings { get; set; }

        /// <summary>
        /// 获取验证字符串
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <param name="canonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <param name="signedHeaders">所有在这一阶段进行了编码的Header名字转换成全小写之后按照字典序排列，然后用分号（;）连接</param>
        /// <returns></returns>
        public string GetAuthString(DateTime date, string canonicalRequest, string signedHeaders)
        {
            string timestamp = GetTimeStamp(date);
            var prefix = $"bce-auth-v1/{Settings.AccessKeyId}/{timestamp}/{Settings.ExpirationPeriodInSeconds}";
            string signingkey = GetSigningKeyByHMACSHA256HEX(Settings.SecretAccessKey, prefix);
            string signature = GetSignatureByHMACSHA256HEX(signingkey, canonicalRequest);
            return $"{prefix}/{signedHeaders}/{signature}";
        }
        /// <summary>
        /// 获取验证字符串
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <param name="canonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <param name="signedHeaders">所有在这一阶段进行了编码的Header名字</param>
        /// <returns></returns>
        public string GetAuthString(DateTime date, string canonicalRequest, IEnumerable<string> signedHeaders)
        {
            return GetAuthString(date, canonicalRequest, signedHeaders.Select(s=>s.ToLower()).OrderBy(s=>s).Merge(";"));
        }

        #region Utility

        /// <summary>
        /// 获取日期时间的时间戳格式
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// 获取 signingKey。百度云不直接使用SK对待签名串生成摘要。相反的，百度云首先使用SK和认证字符串前缀生成signingKey，然后用signingKey对待签名串生成摘要。
        /// </summary>
        /// <param name="SecretAccessKey">用户SK</param>
        /// <param name="authStringPrefix">认证字符串的前缀部分</param>
        /// <returns></returns>
        public static string GetSigningKeyByHMACSHA256HEX(String SecretAccessKey, String authStringPrefix)
        {
            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(SecretAccessKey));
            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(authStringPrefix));
            string SigningKey = HashEncode(LiveHash);
            return SigningKey;
        }

        /// <summary>
        /// 获取 签名摘要。百度云使用signingKey对canonicalRequest使用HAMC算法计算签名。
        /// </summary>
        /// <param name="SigningKey">签名Key。百度云不直接使用SK对待签名串生成摘要。相反的，百度云首先使用SK和认证字符串前缀生成signingKey，然后用signingKey对待签名串生成摘要。</param>
        /// <param name="CanonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <returns></returns>
        public static string GetSignatureByHMACSHA256HEX(String SigningKey, String CanonicalRequest)
        {
            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(SigningKey));
            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(CanonicalRequest));
            string Signature = HashEncode(LiveHash);
            return Signature;
        }
        
        /// <summary>
        /// 将摘要字符串规范化
        /// </summary>
        /// <param name="hash">已有摘要</param>
        /// <returns></returns>
        static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        #endregion
    }
}
