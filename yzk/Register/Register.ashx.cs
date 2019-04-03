using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Register
{
    /// <summary>
    /// Register 的摘要说明
    /// </summary>
    public class Register : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
         
            string html = NVelocityHelper.RenderHtml("Register.html", null);
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