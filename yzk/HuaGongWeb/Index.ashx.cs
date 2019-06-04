using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HuaGongWeb
{
    /// <summary>
    /// Index 的摘要说明
    /// </summary>
    public class Index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //为了foot.html获取配置数据
            var settings = CommonHelper.GetSettings();

            //为了head.html获取产品分类数据
            DataRowCollection dtProductCategories = CommonHelper.GetProductCategories();

            //获取推荐产品的数据
            string sql = "select * from T_Products where IsRecommend=1";

            DataTable dtRecommendProduct = SqlHelper.GetDataTable(sql, CommandType.Text);

            var data = new {RecommendProducts=dtRecommendProduct .Rows, ProductCategories= dtProductCategories, Settings=settings  };
            string html = NVelocityHelper.RenderHtml("Front/Index.html", data);
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