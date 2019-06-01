using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebJWT.Controllers
{
    public class HomeController : Controller
    {
        private readonly string secret = "QWSDA34RTNJLAMV,4IW12qwerjdn@#8*";
        // GET: Home
     
        public ActionResult JwtByToken()
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
            //AES加密
            var payloadDic = new Dictionary<string, object> {
                { "name","byf"},
                { "password","@777"},
                { "content","登录信息"},
            };
            string str = JsonConvert.SerializeObject(payloadDic, Formatting.Indented); 
            var mstr = AesEncrypt(str, "12345678876543211234567887654abc");
            var payload = new Dictionary<string, object> {
                { "name","jwt"},
                { "exp",secondsSinceEpoch+100},
                { "jti",mstr},
            };
           
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            ViewBag.token = encoder.Encode(payload, secret);

            return View();
        }
        
        public ActionResult GetTokenByContent(string token)
        {
            string msg = "";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                msg = decoder.Decode(token, secret, verify: true);//token为之前生成的字符串
                //获取密文
                //{"name":"jwt","exp":1547777697.0,"jti":"9XSKvy9Ml/cRIAsp3PmG/BCmeKkcXVAjY0XZHRE1ari7A"}
                dynamic jd = JsonConvert.DeserializeObject<dynamic>(msg);
                string miwen = jd.jti;
                string mwStr = AesDecrypt(miwen, "12345678876543211234567887654abc");
                //输出明文
                dynamic mingwen = JsonConvert.DeserializeObject<dynamic>(mwStr);
                string name = mingwen.name;
                string password = mingwen.password;
                string content = mingwen.content;
                msg = $"登录名：{name}，密码：{password}，内容：{content}";


            }
            catch (TokenExpiredException)
            {
                msg = "Token 已过期！";
            }
            catch (SignatureVerificationException)
            {
                msg = "Token签名不一致！";
            }
            catch (Exception err)
            {
                msg = err.ToString();
            }

            //return RedirectToAction("JwtByToken",new { byMsg = msg});
            ViewBag.byMsg = msg;
            return View();
        }
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
