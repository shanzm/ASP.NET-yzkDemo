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
            context.Response.ContentType = "text/plain";

            string action = context.Request["Action"];

            if (action=="PostComment")//用户提交评论
            {
                int proudctId = Convert.ToInt32(context.Request["ProductId"]);
                string title = context.Request["Title"];
                string msg = context.Request["Msg"];
                
                //undone:对用户的评论进行审查



            }

            else if (action=="Load")//展示评论
            {

            }

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