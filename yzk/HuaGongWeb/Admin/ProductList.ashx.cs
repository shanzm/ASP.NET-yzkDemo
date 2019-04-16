using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HuaGongWeb.Admin
{
    /// <summary>
    /// ProductList 的摘要说明
    /// </summary>
    public class ProductList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string sql = "select p.Id as Id, p.Name as Name,p.Msg as Msg,pc.Name as CategoryName from T_Products as p left  join T_ProductCategories as pc on p.CategoryId=pc.Id";
            DataTable dtProduct = SqlHelper.GetDataTable(sql, CommandType.Text);
            var data = new { Products = dtProduct.Rows, Title = "产品展示" };

            string html = NVelocityHelper.RenderHtml("Admin/ProductList.html", data);

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