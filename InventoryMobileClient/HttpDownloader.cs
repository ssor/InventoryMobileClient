using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Diagnostics;
//using Microsoft.WindowsMobile.Status;
namespace httpHelper
{
    #region RequestState

    public class RequestState
    {
        // This class stores the State of the request.
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;
        public RequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = null;
            streamResponse = null;
        }
    }

    #endregion

    public class HttpException : Exception
    {
        string message;
        new public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }


    /// <summary>
    /// 解析错误信息的接口，包含了两个方法，一个用于检查是否含有错误信息，另一个负责根据错误信息返回对错误的解释
    /// </summary>
    public interface IErrorParser
    {
        bool HasError(string content);
        string GetErrorMessage(string errorInfo);
    }

    public class ErrorParser : IErrorParser
    {
        public string GetErrorMessage(string errorInfo)
        {
            string errorMessage = "";

            return errorMessage;
        }

        public bool HasError(string content)
        {
            bool flag = false;

            return flag;
        }
    }
    public delegate void deleInvokeString(string s);
    public delegate void deleGetRequestObject(object o);
    public delegate void deleRaiseException(HttpException httpException);

    public class HttpWebConnect
    {

        const int BUFFER_SIZE = 1024;
        public IErrorParser myErrorParser;
        #region Events

        public event deleRaiseException AcceptException;
        public event deleGetRequestObject RequestCompleted;

        #endregion

        #region Properties

        int defaultTimeout = 30 * 1000; // 2 minutes timeout

        public int DefaultTimeout
        {
            get { return defaultTimeout; }
            set { defaultTimeout = value; }
        }

        RequestState myRequestState = null;

        string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        bool isCancled = false;

        /// <summary>
        /// 是否已经取消该网络请求
        /// </summary>
        public bool IsCancled
        {
            get { return isCancled; }
            set { isCancled = value; }
        }

        bool isGet = true;

        public bool IsGet
        {
            get { return isGet; }
            set { isGet = value; }
        }
        string postData;
        #endregion

        public void Cancel()
        {
            this.IsCancled = true;
        }

        // Abort the request if the timer fires.
        private void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public HttpWebConnect()
        {
            myRequestState = new RequestState();
        }

        public void TryPostData(string url, string postData)
        {
            this.Url = url;
            this.isGet = false;
            this.postData = postData;
            this.TryPostData();

        }
        void TryPostData()
        {
            try
            {
                if (!this.isGet)
                {
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(this.Url);
                    //myHttpWebRequest.Timeout = this.DefaultTimeout;

                    myHttpWebRequest.Method = "POST";
                    if (this.postData != null)
                    {
                        UTF8Encoding enc = new UTF8Encoding();
                        byte[] bs = enc.GetBytes(this.postData);
                        myHttpWebRequest.ContentType = "text/json";
                        //myHttpWebRequest.ContentType = "text/xml";
                        myHttpWebRequest.ContentLength = bs.Length;

                        myRequestState.request = myHttpWebRequest;
                        myHttpWebRequest.BeginGetRequestStream(new AsyncCallback(RequestStreamCallBack), myRequestState);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(
     string.Format("HttpWebConnect.TryPostData  -> error = {0}"
     , ex.Message));
                //this.RaiseException(ex);
            }
        }
        private void RequestStreamCallBack(IAsyncResult asynchronousResult)
        {
            try
            {
                myRequestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest myrequest = myRequestState.request;
                // End the operation.
                Stream postStream = myrequest.EndGetRequestStream(asynchronousResult);
                UTF8Encoding enc = new UTF8Encoding();
                byte[] byteArray = enc.GetBytes(this.postData);
                //myrequest.ContentLength = byteArray.Length;
                // Write to the request stream.
                postStream.Write(byteArray, 0, postData.Length);
                postStream.Close();
                myrequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);
            }
            catch (System.Exception ex)
            {
                this.RaiseException(ex);
            }

        }

        public void TryRequest(String url)
        {
            this.Url = url;
            this.TryRequest();
        }
        public void TryRequest()
        {
            if (this.IsCancled == true) return;
            try
            {
                // Create a HttpWebrequest object to the desired URL. 
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(this.Url);
                //myHttpWebRequest.Timeout = this.DefaultTimeout;
                // Create an instance of the RequestState and assign the previous myHttpWebRequest
                // object to its request field.  

                myRequestState.request = myHttpWebRequest;

                // Start the asynchronous request.

                IAsyncResult result =
                  (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

            }
            catch (Exception e)
            {
                Debug.WriteLine(
    string.Format("HttpWebConnect.TryRequest  -> error = {0}"
    , e.Message));
                //this.RaiseException(e);
                //if (this.AcceptException  != null)
                //{
                //    HttpException he = new HttpException();
                //    he.Message = e.Message;
                //    this.AcceptException(he);

                //}
            }
        }

        void RaiseEvents()
        {
            if (this.RequestCompleted != null)
            {
                //Deployment.Current.Dispatcher.BeginInvoke(() => RequestCompleted(this.myRequestState.requestData.ToString()));
                this.RequestCompleted(this.myRequestState.requestData.ToString());
            }
        }
        void RaiseException(Exception e)
        {
            if (this.AcceptException != null)
            {
                HttpException he = new HttpException();
                he.Message = e.Message;
                //this.AcceptException(he);
            }
        }
        void ReleaseSource()
        {
            try
            {
                if (myRequestState.response != null)
                {
                    myRequestState.response.Close();
                    myRequestState.response = null;
                }
                if (myRequestState.request != null)
                {
                    myRequestState.request.Abort();
                    myRequestState.request = null;
                }
                if (myRequestState.requestData != null)
                {
                    myRequestState.requestData.Remove(0, myRequestState.requestData.Length);
                }
                //myRequestState.requestData = null;
                if (myRequestState.streamResponse != null)
                {
                    myRequestState.streamResponse.Close();
                    myRequestState.streamResponse = null;
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(
string.Format("HttpWebConnect.ReleaseSource  -> error = {0}"
, ex.Message));
                //RaiseException(ex);
            }
        }


        private void RespCallback(IAsyncResult asynchronousResult)
        {
            if (this.IsCancled)
            {
                ReleaseSource();
                return;
            }
            try
            {
                // State of request is asynchronous.
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest myHttpWebRequest = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

                // Read the response into a Stream object.
                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                // Begin the Reading of the contents of the HTML page and print it to the console.
                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                //return;
            }
            #region 异常处理

            catch (Exception e)
            {
                this.RaiseException(e);
                //if (this.AcceptException != null)
                //{
                //    HttpException he = new HttpException();
                //    he.Message = e.Message;
                //    this.AcceptException(he);

                //}
            }

            #endregion
        }
        private void ReadCallBack(IAsyncResult asyncResult)
        {
            if (this.IsCancled)
            {
                this.ReleaseSource();
                return;
            }

            try
            {

                //RequestState myRequestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                // Read the HTML page and then print it to the console.
                if (read > 0)
                {
                    //string contentType = myRequestState.response.ContentType;
                    //string encodeName = contentType.Substring(contentType.IndexOf("charset") + 8);
                    //Encoding ecd = Encoding.GetEncoding(encodeName);
                    //myRequestState.requestData.Append(ecd.GetString(myRequestState.BufferRead, 0, read));
                    myRequestState.requestData.Append(Encoding.UTF8.GetString(myRequestState.BufferRead, 0, read));
                    IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                    return;
                }
                else
                {
                    // 数据从网络上读取完毕,此时需要完成两个工作：
                    // 1. 检查数据中是否有自定义的异常信息
                    // 2.如果没有异常信息，调用完成事件
                    string str = myRequestState.requestData.ToString();

                    if (myErrorParser != null)
                    {
                        if (myErrorParser.HasError(str))
                        {
                            HttpException he = new HttpException();
                            he.Message = myErrorParser.GetErrorMessage(str);
                            this.RaiseException(he);
                            //if (this.AcceptException != null)
                            //    this.AcceptException(he);
                        }
                        else
                            this.RaiseEvents();

                    }
                    else
                        RaiseEvents();


                }

            }
            #region 异常处理

            catch (Exception e)
            {
                this.RaiseException(e);
                //if (this.AcceptException != null)
                //{
                //    HttpException he = new HttpException();
                //    he.Message = e.Message;
                //    this.AcceptException(he);

                //}
            }

            #endregion

            ReleaseSource();

        }
    }
}