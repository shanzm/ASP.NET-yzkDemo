using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HuaGongWeb
{
    /// <summary>
    /// ProductView 的摘要说明
    /// </summary>
    public class ProductView : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int Id =Convert.ToInt32( context.Request["Id"]);

            string sql = "select * from T_Products where Id=@Id";
            SqlParameter param = new SqlParameter("Id", Id);

            DataTable dtProduct = SqlHelper.GetDataTable(sql, CommandType.Text, param);

            var data = new { Product = dtProduct.Rows[0]};

            string html = NVelocityHelper.RenderHtml("Front/ProductView.html", data);
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