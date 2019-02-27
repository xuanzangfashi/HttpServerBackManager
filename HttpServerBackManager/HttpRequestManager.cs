using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServerBackManager
{
    public delegate void HttpRequestCallback(string retString);
    class RequestThreadParams
    {
        public string url;
        public string requestMethod;
        public HttpRequestCallback callback;
        public string postData;
    }
    class HttpRequestManager
    {
        private static HttpRequestManager Instance = null;
        private HttpRequestManager()
        {

        }

        public static HttpRequestManager GetInstance()
        {
            if (Instance == null)
                Instance = new HttpRequestManager();
            return Instance;
        }

        public void SendHttpRequest(string url, string requestMethod, HttpRequestCallback callback, string postData = null)
        {
            RequestThreadParams tmpParams = new RequestThreadParams();
            tmpParams.url = url;
            tmpParams.requestMethod = requestMethod;
            tmpParams.callback = callback;
            tmpParams.postData = postData;
            Thread tmpthread = new Thread(RequestThreadAdress);
            tmpthread.Start(tmpParams);
        }
        private string HttpRequest(string url, string requestMethod,string postDataStr)
        {
            string retString = null;
            try
            {
                 HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = requestMethod;
                request.Timeout = 6000000;
                request.ContentType = "application/x-www-form-urlencoded";
                
                if (postDataStr != null)
                {

                    request.ContentLength = postDataStr.Length;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        using (StreamWriter swrite = new StreamWriter(requestStream))
                        {
                            swrite.Write(postDataStr.ToCharArray(),0,postDataStr.Length);
                        }
                    }


                    //byte[] postdata = null;
                    //postdata = Encoding.UTF8.GetBytes(postDataStr);
                    //request.ContentLength = postdata.Length;
                    //Stream myRequestStream;
                    //StreamWriter myStreamWriter;
                    
                    //myRequestStream = request.GetRequestStream();
                    //myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                    //myStreamWriter.Write(postdata);
                    //myStreamWriter.Flush();
                    //myStreamWriter.Close();
                }
                else
                {
                    request.ContentLength = 0;
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                retString = ex.Message;


            }
            return retString;
        }

        private void RequestThreadAdress(object p)
        {
            RequestThreadParams real_p = p as RequestThreadParams;
            var re = HttpRequest(real_p.url, real_p.requestMethod, real_p.postData);
            real_p.callback(re);
        }

    }
}
