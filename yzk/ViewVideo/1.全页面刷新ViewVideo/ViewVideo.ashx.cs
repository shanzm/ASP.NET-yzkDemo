using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ViewVideo
{
    /// <summary>
    /// ViewVideo 的摘要说明
    /// </summary>
    public class ViewVideo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string strIsPostBack = context.Request["IsPostBack"];
      
            if (string.IsNullOrEmpty(strIsPostBack))
            {
                int zanCount = (int)SqlHelper.ExecuteScalar("select top 1 ZanCount from T_ZanCai", CommandType.Text);//数据库就一行数据，我们就不使用视频的Id查询了，直接查第一行
                int caiCount = (int)SqlHelper.ExecuteScalar("select top 1 CaiCount from T_ZanCai", CommandType.Text);
                var data = new { ZanCount = zanCount, CaiCount = caiCount };
                string html = NVelocityHelper.RenderHtml("ViewVideo.html", data);
                context.Response.Write(html);
            }
            else
            {
                //投票
                string zan = context.Request["Zan"];
                if (string.IsNullOrEmpty(zan))
                {
                    SqlHelper.ExecuteNonquery("Update T_ZanCai set CaiCount=CaiCount+1", CommandType.Text);
                    context.Response.Redirect("ViewVideo.ashx");
                }
                else
                {
                    SqlHelper.ExecuteNonquery("Update T_ZanCai set ZanCount=ZanCount+1", CommandType.Text);
                    context.Response.Redirect("ViewVideo.ashx");
                }
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