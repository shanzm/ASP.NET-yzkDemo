using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HuaGongWeb.Admin
{
    /// <summary>
    /// ProductEdit 的摘要说明
    /// </summary>
    public class ProductEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string action = context.Request["Action"];

            #region Edit
            if (action == "Edit")
            {

                if (string.IsNullOrEmpty(context.Request["IsPostBack"]))//展示选中的
                {
                    long Id = Convert.ToInt64(context.Request["Id"]);

                    string sql = "select * from T_Products where Id=@Id";
                    SqlParameter param = new SqlParameter("@Id", Id);
                    DataTable dtProduct = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                    //todo:通过dtProduct.Count()来判断接收的Id是否有效
                    DataTable dtCategory = SqlHelper.GetDataTable("select Id,Name from T_ProductCategories", CommandType.Text);


                    var data = new { Action = "Edit", Product = dtProduct.Rows[0], Title = "编辑产品", Categories = dtCategory.Rows };
                    //注意dtProduct虽然只有一行数据，但是他是一个表格啊，我们直接给的html页面只要第一行就可以了
                    //注意我们是从”编辑“ 的连接点击进来的，所以他的展示页面的Action的值设置为”Edit"

                    string html = NVelocityHelper.RenderHtml("Admin/ProductEdit.html", data);

                    context.Response.Write(html);
                }
                else//编辑后点击保存
                {

                    //todo:用户输入的产品名等信息是否为空做一个判断
                    long Id = Convert.ToInt32(context.Request["Id"]);
                    string Name = context.Request["Name"];
                    string Msg = context.Request["Msg"];
                    long CategoryId = Convert.ToInt32(context.Request["CategoryId"]);

                    //获取浏览器上传的图片的信息
                    HttpPostedFile productImg = context.Request.Files["ProductImage"];//对HttpPostFile查看定义可以看到他的几个方法
                                                                                      //图片要保存到项目的文件夹或子文件夹中，这样才可以通过web来访问图片

                    //注意不要这样写似，以后我们部署到其他的地方，那不就报错了吗
                    //productImg.SaveAs(@"F:\ForGit\ASP.NET-yzkDemo\yzk\HuaGongWeb\uploadfile\");

                    //获取当前项目的某个文件的全路径
                    string filePath = context.Server.MapPath("~/uploadfile");//波浪线就代表当前项目的路径


                    string sql = "update T_Products set Name=@Name,CategoryId=@CategoryId,Msg=@Msg where Id=@Id;";
                    SqlParameter[] param =
                    {
                        new SqlParameter ("@Id",Id),
                        new SqlParameter ("@Name",Name),
                        new SqlParameter ("@Msg",Msg),
                        new SqlParameter ("@CategoryId",CategoryId )

                    };
                    SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                    context.Response.Redirect("ProductList.ashx");
                }


            }
            #endregion


            #region AddNew
            if (action == "AddNew")
            {
                if (string.IsNullOrEmpty(context.Request["IsPostBack"]))//添加页面
                {
                    string sql = "select * from T_ProductCategories";
                    DataTable dtCategory = SqlHelper.GetDataTable(sql, CommandType.Text);



                    var data = new { Title = "添加产品", Action = "AddNew", Categories = dtCategory.Rows, Product = new { Name = "", CategoryId = 0, Msg = "" } };
                    string html = NVelocityHelper.RenderHtml("Admin/ProductEdit.html", data);

                    context.Response.Write(html);
                }
                else
                {
                    string Name = context.Request["Name"];
                    string Msg = context.Request["Msg"];
                    long CategoryId = Convert.ToInt32(context.Request["CategoryId"]);

                    string sql = "insert into T_Products (Name,CategoryId,Msg) values(@Name,@CategoryId,@Msg);";
                    SqlParameter[] param =
                    {

                        new SqlParameter ("@Name",Name),
                        new SqlParameter ("@Msg",Msg),
                        new SqlParameter ("@CategoryId",CategoryId )

                    };
                    SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                    context.Response.Redirect("ProductList.ashx");
                }

            }
            #endregion


            #region Delete
            if (action == "Delete")
            {

                //undone:按理说应该在数据库设置一个删除标志字段，实现软删除，注意查询的地方一样要进行修改，此处直接使用的是物理删除
                //todo:删除前做判断

                long Id = Convert.ToInt32(context.Request["Id"]);

                string sql = "Delete from T_Products where Id=@Id ";
                SqlParameter param = new SqlParameter("@Id", Id);

                SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                context.Response.Redirect("ProductList.ashx");

            }
            #endregion

            else//bug:为什么在显示页面点击编辑按钮之后，显示类待编辑的数据，最后还显示了 action错误：Edit
            {
                context.Response.Write("action错误：" + action);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public object SqlParamters { get; private set; }
    }
}