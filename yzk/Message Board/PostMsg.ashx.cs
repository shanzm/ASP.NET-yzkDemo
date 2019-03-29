using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Message_Board
{
    /// <summary>
    /// PostMsg 的摘要说明
    /// </summary>
    public class PostMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string save = context.Request["Save"];
            if (string.IsNullOrEmpty(save)) //展示留言页面
            {

                var data = new { Title = "发表留言" };
                context.Response.Write(NVelocityHelper.RenderHtml("PostMsg.html", data));
            }

            else//发表留言（保存）
            {
                string title = context.Request["Title"];
                string msg = context.Request["Msg"];
                string nickName = context.Request["NickName"];
                //注意checkbox被选中的返回值是“on”否则是null
                //bool isAnonymous = context.Request["IsAnonymous"] == "on" ? true : false;
                bool isAnonymous = (context.Request["IsAnonymous"] == "on");
                string ipAddress = context.Request.UserHostAddress;

                SqlParameter[] param =
                {
                    new SqlParameter ("@Title",title),
                    new SqlParameter ("@Msg",msg),
                    new SqlParameter ("@NickName",nickName),
                    new SqlParameter ("@IPAddress",ipAddress),
                    new SqlParameter ("@IsAnonymous",Convert.ToByte (isAnonymous))
                };

                string sql = "insert into T_Msg (Title,Msg,NickName,IPAddress,IsAnonymous,PostDate) values(@Title,@Msg,@NickName,@IPAddress,@IsAnonymous,GetDate());";
                SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                //TODO:数据校验（判断数据是否为空）
                context.Response.Redirect("ViewMsg.ashx");

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