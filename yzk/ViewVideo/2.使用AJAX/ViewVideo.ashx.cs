using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


//注意这个例子，调试的时候，你先打开ViewVideo.html页面（而不是先打开ViewVideo.ashx


namespace ViewVideo._2.使用AJAX
{
    /// <summary>
    /// ViewVideo 的摘要说明
    /// </summary>
    public class ViewVideo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string action = context.Request["action"];
            if (action == "Zan")
            {
                SqlHelper.ExecuteNonquery("Update T_ZanCai set ZanCount=ZanCount+1",CommandType.Text);
                int zanCount = (int)SqlHelper.ExecuteScalar("select top 1 ZanCount from T_ZanCai",CommandType.Text);
                context.Response.Write(zanCount);
            }
            else
            {
                SqlHelper.ExecuteNonquery("Update T_ZanCai set CaiCount=CaiCount+1", CommandType.Text);
                int caiCount = (int)SqlHelper.ExecuteScalar("select top 1 CaiCount from T_ZanCai", CommandType.Text);
                context.Response.Write(caiCount);
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}