using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace HuaGongWeb
{
    /// <summary>
    /// ProductCommentAJAX 的摘要说明
    /// </summary>
    public class ProductCommentAJAX : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["Action"];

            if (action == "PostComment")//用户提交评论
            {
                long productId = Convert.ToInt32(context.Request["ProductId"]);
                string title = context.Request["Title"];
                string msg = context.Request["Msg"];

                //todo:对用户的评论进行审查

                string sql = "insert into T_ProductComments (ProductId,Title,Msg,CreateDateTime) values(@ProductId,@Title,@Msg,getdate()); ";

                SqlParameter[] param =
                {
                    new SqlParameter ("@ProductId",productId),
                    new SqlParameter ("@Title",title),
                    new SqlParameter ("@Msg",msg)
                };

                SqlHelper.ExecuteNonquery(sql, CommandType.Text, param);

                context.Response.Write("ok");

            }

            else if (action == "Load")//展示评论
            {
                long productId = Convert.ToInt32(context.Request["ProductId"]);

                string sql = "select * from T_productComments where ProductId=@ProductId;";
                SqlParameter param = new SqlParameter("@ProductId", productId);

                DataTable dtPC = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                //注意我们一般不把DataTable这种复杂的数据JSon序列化，Json序列化的一般是数组集合之类的

                object[] comments = new object[dtPC.Rows.Count];
                for (int i = 0; i < comments.Length; i++)
                {
                    DataRow dr = dtPC.Rows[i];
                    string createDateTime = ((DateTime)dr["CreateDateTime"]).ToString();//注意你如果直接对SQL中函数getdate()产生的日期Json序列化，则会在html页面解析出错，所以我们强转为DateTime格式之后又给转换为字符串

                    comments[i] = new { Title = dr["Title"], Msg = dr["Msg"], CreateDateTime = createDateTime };
                }

                string json = new JavaScriptSerializer().Serialize(comments);
                context.Response.Write(json);
            }

            else
            {
                context.Response.Write("Action错误" + action);
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