using Newtonsoft.Json;

namespace BlogDemo.Api
{
    /// <summary>
    ///序列化和反序列化帮助类 
    /// </summary>
    public class JsonNetHelper
    {
        //序列化
        public static string SerializerToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T SerializerToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
