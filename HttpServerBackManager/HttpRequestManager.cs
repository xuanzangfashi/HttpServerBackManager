using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServerBackManager
{
    public delegate void HttpRequestCallback(string retString);

    public enum POSTDATATYPE
    {
        PDT_STRING,PDT_BYTEARR,
    }

    class RequestThreadParams
    {

        public string url;

        public string requestMethod;

        public HttpRequestCallback callback;

        public string postData;

        public byte[] postData_byte;

        public POSTDATATYPE postDataType;
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
            tmpParams.postDataType = POSTDATATYPE.PDT_STRING;
            Thread tmpthread = new Thread(RequestThreadAdress);
            tmpthread.Start(tmpParams);
        }

        public void SendHttpRequest(string url, string requestMethod, HttpRequestCallback callback, byte[] postData)
        {
            RequestThreadParams tmpParams = new RequestThreadParams();
            tmpParams.url = url;
            tmpParams.requestMethod = requestMethod;
            tmpParams.callback = callback;
            tmpParams.postData_byte = postData;
            tmpParams.postDataType = POSTDATATYPE.PDT_BYTEARR;
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

        private string HttpRequest(string url, string requestMethod, byte[] postDataByteArr)
        {
            string retString = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = requestMethod;
                request.Timeout = 6000000;
                request.ContentType = "application/x-www-form-urlencoded";

                if (postDataByteArr != null)
                {

                    request.ContentLength = postDataByteArr.Length;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        using (BinaryWriter swrite = new BinaryWriter(requestStream))
                        {
                            swrite.Write(postDataByteArr, 0, postDataByteArr.Length);
                        }
                    }
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
            string re = "";
            switch(real_p.postDataType)
            {
                case POSTDATATYPE.PDT_BYTEARR:
                    re = HttpRequest(real_p.url, real_p.requestMethod, real_p.postData_byte);
                    break;
                case POSTDATATYPE.PDT_STRING:
                    re = HttpRequest(real_p.url, real_p.requestMethod, real_p.postData);
                    break;
            }
            
            real_p.callback(re);
        }

    }
}
