using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YunWen.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static string Request(string url, string data = "", HttpMethod httpMethod = HttpMethod.GET)
        {
            HttpWebRequest httpRequest = null;
            try
            {
                if (httpMethod == HttpMethod.POST)
                {
                    httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = httpMethod.ToString();
                    httpRequest.ContentType = "application/x-www-form-urlencoded";

                    httpRequest.Proxy = null;
                    ServicePointManager.DefaultConnectionLimit = 500;

                    if (!string.IsNullOrEmpty(data))
                    {
                        byte[] bts = Encoding.UTF8.GetBytes(data);
                        httpRequest.ContentLength = bts.Length;
                        using (Stream sw = httpRequest.GetRequestStream())
                        {
                            sw.Write(bts, 0, bts.Length);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        if (url.IndexOf("?", StringComparison.Ordinal) > -1) url = url += "&";
                        else url = url += "?";
                        url += data;
                    }
                    httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = httpMethod.ToString();
                }
                httpRequest.KeepAlive = false;
                WebResponse httpResponse = httpRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseText = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return responseText;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                httpRequest = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> data = null)
        {
            string responseText = Request(url, (data ?? new Dictionary<string, string>()).Aggregate("", (current, kvp) => current + string.Format("{0}={1}&", kvp.Key, HttpUtility.UrlEncode(kvp.Value))));
            return responseText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> data = null)
        {
            return Request(url, data.Aggregate("", (current, kvp) => current + string.Format("{0}={1}&", kvp.Key, HttpUtility.UrlEncode(kvp.Value))), HttpMethod.POST);
        }
    }

    /// <summary>
    /// Http请求方法
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// 
        /// </summary>
        GET = 1,
        /// <summary>
        /// 
        /// </summary>
        POST = 2
    }
}
