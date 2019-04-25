using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HuaGongWeb
{
    /// <summary>
    /// ProductList 的摘要说明
    /// </summary>
    public class ProductList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            int pageNum = 1;
            if (context.Request["pageNum"] != null)
            {
                pageNum = Convert.ToInt32(context.Request["pageNum"]);
            }

            string sql = @"select * ,num from
                           (select *,row_number() over(order by p.Id)as num from T_Products as p)as V
                           where V.num between @start and @end ";

            SqlParameter[] param =
            {
              new  SqlParameter ("@start",(pageNum-1)*9),//每页9个数据
              new SqlParameter("@end",pageNum*9)
            };

            DataTable dtProduct = SqlHelper.GetDataTable(sql, CommandType.Text, param);


            int totalCount = (int)SqlHelper.ExecuteScalar("select count(*) from T_Products", CommandType.Text);
            int pageCount = Convert.ToInt32(Math.Ceiling(totalCount / 9.0));
            object[] pageData = new object[pageCount];
            for (int i = 0; i < pageCount; i++)
            {
                pageData[i] = new { Href = "ProductList.ashx?pageNum=" + (i + 1), pageName = i + 1 };
            }





            var date = new { Products = dtProduct.Rows, PageData = pageData, PageNum = pageNum,PageCount=pageCount, TotalCount = totalCount };

            string html = NVelocityHelper.RenderHtml("Front/ProductList.html", date);
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