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

            //注意这里：程序的运行顺序是：从PersonList.html--->PersonEdit.ashx?action=Action(而不是PersonEdit.html)

            if (action == "AddNew")//添加新成员
            {
                //判断Save隐藏域的值是不是true,若是则说明是点击保存
                //注意：好像哦我们的PersonEdit.html页面的Save隐藏域的默认值是true,但是但我们从PersonList.html 的编辑超链接（PersonEdit.ashx?action=AddNew)跳到这个页面的时候,save =false？
                //你要知道表单隐藏域的值只有你点击类submit按钮才会提交到表单指向的页面（PersonEdit.ashx),所以一开始save就是C#默认的false
                bool save = Convert.ToBoolean(context.Request["Save"]);
                if (save)
                {
                    string Name = context.Request["Name"];
                    int Age = Convert.ToInt16(context.Request["Age"]);
                    string Email = context.Request["Email"];
                    long ClassId = Convert.ToInt64(context.Request["ClassId"]);

                    string sql = "insert into T_Person (Name,Age,Email,ClassId) values(@Name,@Age,@Email,@ClassId); ";
                    SqlParameter[] param =
                    {
                        new SqlParameter ("@Name",Name),
                        new SqlParameter("@Age", Age),
                        new SqlParameter ("@Email",Email ),
                        new SqlParameter("@ClassId",ClassId )
                    };
                    SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                    //保存成功跳转到列表页面
                    context.Response.Redirect("PersonList.ashx");
                }
                else
                {
                    //context.Response.Write(NVelocityHelper.RenderHtml("PersonEdit.html", new { Action = "AddNew",  Name = "", Age = "", Email = "@qq.com" } ));
                    //按照上面的写法可不可以呢？可以，但是万一我们的表中也有一个叫做“Action”的列，就会混淆。
                    //所以我们把表中的数据都使用一个匿名对象放在一起
                    DataTable dtClasses = SqlHelper.GetDataTable("select * from T_Classes", CommandType.Text);

                    context.Response.Write(NVelocityHelper.RenderHtml("PersonEdit.html", new { Action = "AddNew", Person = new { Name = "", Age = "", Email = "@qq.com" }, Classes = dtClasses.Rows }));
                }
            }



            ///注意当我们从PersonList.html页面点击每一行数据后的“编辑”按钮时，即点击连接“PersonEdit.ashx?action=Edit&Id=相应的id
            ///之后在这样页面运行下面的代码，首先bool类型的save没有赋值，就是默认的false，执行else的代码（即：展示）
            ///当在展示界面（PersonEdit.ashx?action=Edit&Id=相应Id值）修改了文本框的值，点击submit按钮后，此时PersonEdit.ashx页面的bool save接收到true
            ///即：PersonEdit.ashx?action=Edit&Id=相应的Id值&save=true
            ///即执行下面的修订功能的代码

            else if (action == "Edit")
            {
                bool save = Convert.ToBoolean(context.Request["Save"]);
                if (save)//修改
                {
                    string Name = context.Request["Name"];
                    int Age = Convert.ToInt32(context.Request["Age"]);
                    long Id = Convert.ToInt64(context.Request["Id"]);
                    string Email = context.Request["Email"];
                    long ClassId = Convert.ToInt64(context.Request["ClassId"]);

                    SqlParameter[] param =
                        {
                        new SqlParameter("@Name", Name),
                        new SqlParameter("@Age", Age),
                        new SqlParameter("@Email", Email),
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@ClassId",ClassId)
                        };
                    string sql = "update T_Person set Name=@Name,Age=@Age,Email=@Email ,ClassId=@ClassId where Id=@Id";

                    SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                    context.Response.Redirect("PersonList.ashx");
                }
                else//展示
                {
                    long Id = Convert.ToInt32(context.Request["Id"]);//注意在数据库中我们定义的Id的类型是bigint,所以我们这里定义一个长整型来接收

                    string sql = "select * from T_Person where Id=@Id";
                    SqlParameter param = new SqlParameter("@Id", Id);
                    DataTable dtPerson = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                    DataTable dtClasses = SqlHelper.GetDataTable("select * from T_Classes", CommandType.Text);

                    context.Response.Write(NVelocityHelper.RenderHtml("PersonEdit.html", new { Action = "Edit", Person = dtPerson.Rows[0], Classes = dtClasses.Rows }));
                }

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