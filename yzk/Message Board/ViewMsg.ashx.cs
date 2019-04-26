using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MessageBoard
{
    /// <summary>
    /// ViewMsg 的摘要说明
    /// </summary>
    public class ViewMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            DataTable dtMsgs = SqlHelper.GetDataTable("select * from T_Msg;", CommandType.Text);


            context.Response.Write(NVelocityHelper.RenderHtml("ViewMsg.html", new { Title = "展示留言", Msg=dtMsgs.Rows }));
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