using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register
{
    /// <summary>
    /// CheckUserName 的摘要说明
    /// </summary>
    public class CheckUserName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string username = context.Request["UserName"];
            int UserNameCount = (int)SqlHelper.ExecuteScalar("select count(*) from T_Person where Name=@Name;", CommandType.Text, new SqlParameter("@Name", username));
            if (UserNameCount <=0)
            {
                context.Response.Write("ok");//用户名不重复
            }
            else if (UserNameCount>=1)//用户名已存在
            {
                context.Response.Write("error");
            }
            else if (username.Contains("毛泽东")||username.Contains("管理员"))//含有禁用词
            {
                context.Response.Write("forbid");
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