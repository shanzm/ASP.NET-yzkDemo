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
        //注意这个项目只是测试ajax,从注册页面注册的时候，没有写入数据库，也没有返回注册成功的提示
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