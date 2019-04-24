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

            int PageNum = 1;//页数默认是1，则star=（1-1）*10，end=1*10
            if (context.Request["PageNum"] != null)//接收的页码按钮超链接中的PageNum
            {
                PageNum = Convert.ToInt32(context.Request["PageNum"]);
            }

            string sql = @"select Id, Name,CategoryName
                          from
                          (select row_number() over(order by p.Id) as num,p.Id, p.Name as Name,pc.Name as CategoryName 
                          from T_Products as p left  join T_ProductCategories as pc 
                          on p.CategoryId=pc.Id ) as T
                          where T.num between @start and @end";


            SqlParameter[] param =
             {
                new SqlParameter  ("@start",(PageNum -1)*10),
                new SqlParameter ("@end",PageNum*10)
            };
            DataTable dtProduct = SqlHelper.GetDataTable(sql, CommandType.Text, param);


            //分页的页码

            //表中数据总行数
            int totalCount = (int)SqlHelper.ExecuteScalar("select count(*) from T_Products", CommandType.Text);
            //分页数
            int pageCount = (int)Math.Ceiling(totalCount / 10.0);//10行一页
                                                                 //定义一个页码数组
            object[] pageDate = new object[pageCount];

            for (int i = 0; i < pageDate.Length; i++)//for (int = 0; i<pageCount; i++)
            {
                pageDate[i] = new { Href = "ProductList.ashx?PageNum=" + (i + 1), PageName = "第" + (i + 1) + "页" };
            }





            var data = new { Products = dtProduct.Rows, Title = "产品展示", Page = pageDate };

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