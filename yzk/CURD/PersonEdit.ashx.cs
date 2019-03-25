using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CURD
{
    /// <summary>
    /// PersonEdit 的摘要说明
    /// </summary>
    public class PersonEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            ///注意我们把新增一条数据和编辑数据放在一个页面上，所以哦我们就要进行判断
            ///新增一条数据：PersonEdit.ashx?action=AddNew
            ///修改一条数据：PersonEdit.ashx?action=Edit&Id=3
            ///定义一个变量action来接收html页面的action隐藏域的值

            string action = context.Request["Action"];


            if (action == "AddNew")
            {
                context.Response.Write(NVelocityHelper.RenderHtml("PersonEdit.html", new { Email = "@qq.com" }));//定义一个匿名类只有一个Email属性，作为邮箱栏的默认值，你若是不想要默认值，则直接定义一个”" "“为参数

                //判断Save隐藏域的值是不是true,若是则说明是点击保存
                bool save = Convert.ToBoolean(context.Request["Save"]);
                if (save)
                {
                    string Name = context.Request["Name"];
                    int Age = Convert.ToInt32(context.Request["Age"]);
                    string Email = context.Request["Email"];

                    string sql = "insert into T_Person (Name,Age,Email) values(@Name,@Age,@Email); ";
                    SqlParameter[] param =
                    {
                        new SqlParameter ("@Name",Name),
                        new SqlParameter("@Age", Age),
                        new SqlParameter ("@Email",Email )
                    };
                    SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                    //保存成功返回列表页面
                    context.Response.Redirect("PersonList.ashx");
                }
            }
            else if (action == "Edit")
            {

            }
            else
            {
                context.Response.Write("参数错误");
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