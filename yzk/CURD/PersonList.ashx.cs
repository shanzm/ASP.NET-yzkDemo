using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CURD
{
    /// <summary>
    /// PersonList 的摘要说明
    /// </summary>
    public class PersonList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string sql = "select * from T_Person;";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);


            //注意不要把一个表（DateTable)直接给前台页面
            //一个表的所有行给html页面，遍历每一行可以输出具体每一行的每一个单元格
            string html = NVelocityHelper.RenderHtml("PersonList.html", dt.Rows);
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