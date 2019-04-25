using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuaGongWeb
{
    /// <summary>
    /// ProductCommentAJAX 的摘要说明
    /// </summary>
    public class ProductCommentAJAX : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "html/plain";

            int Id = Convert.ToInt32(context.Request["Id"]);

            string html=NVelocityHelper.RenderHtml("ProductView.html",null);

            context.Response.Write(html);
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