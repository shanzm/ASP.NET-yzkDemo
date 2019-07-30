using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HuaGongWeb.Admin
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            //undone:用户注册和管理SysUserEdit.ashx;SysUserList.ashx
            //undone:密码使用了明文，应该使用Md5等加密方式

            if (Convert.ToBoolean(context.Request["IsPostBack"]))//保存
            {
                string userName = context.Request["UserName"];
                string password = context.Request["Password"];

                SqlParameter[] param =
                {
                    new SqlParameter ("@username",userName ),
                    new SqlParameter ("@password",password )
                };
                int count = (int)SqlHelper.ExecuteScalar("select count(*) from T_SysUsers where Name=@username and Password=@password", System.Data.CommandType.Text, param);

                if (count == 1)//密码用户正确
                {
                    //把用户登录的信息记录到session中，并且转向Settings页面
                    //undone:我们应该写一个登录成功后的页面
                    context.Session["UserName"] = userName;
                    context.Response.Redirect("Settings.ashx");


                }
                else if (count > 1)
                {
                    context.Response.Write("用户数据出现重复");
                }
                else
                {
                    context.Response.Write("用户名或密码错误");
                }


            }

            else
            {
                context.Response.Write(NVelocityHelper.RenderHtml("Admin/Login.html", null));
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