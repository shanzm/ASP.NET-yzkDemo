using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CURD
{
    /// <summary>
    /// PersonDelete 的摘要说明
    /// </summary>
    public class PersonDelete : IHttpHandler
    {
        ///其实你要是把删除数据也放在PersonEdit.ashx页面也是可是的，只不过就是给PersonEdit.html页面的action隐藏域多一个值：Delete
        ///然后在PersonEdit.ashx 判断，当action=delete的时候，进行删除功能

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            long Id = Convert.ToInt64(context.Request["Id"]);

            SqlParameter param = new SqlParameter("@Id", Id);
            string sql = "delete from T_Person where Id=@Id ";

            SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

            context.Response.Redirect("PersonList.ashx");
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