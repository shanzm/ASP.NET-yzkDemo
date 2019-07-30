using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HuaGongWeb.Admin
{
    /// <summary>
    /// Settings 的摘要说明
    /// </summary>
    public class Settings : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //判断是否已经登录，若是登录则显示相应页面，未登录则跳转到登录界面
            //注意该项目的所有需要登录显示的页面都是要在相应的ashx页面上添加下面的判断语句
            //可以把下面的判断是否登录的语句封装为一个类：AdminHelper
            //public calss AdminHelper//注意调用这个类的中方法的ashx页面都要实现IRequriesSessionState接口
            //｛
            //    public static void CheckLogin()
            //    {
            //      HttpContext context = HttpContext.Current;//获取当前的context
            //      if (context.Session["UserName"] == null)
            //      {
            //        context.Response.Redirect("Login.ashx");
            //      }
            //    }
            //}


            //注意Session 的返回值是Object类型
            //if (string.IsNullOrEmpty((string)context.Session["UserName"]))这样写是不对的
            if (context.Session["UserName"] == null)
            {
                context.Response.Redirect("Login.ashx");
            }


            bool isSave = !string.IsNullOrEmpty(context.Request["Save"]);

            if (isSave)//用户填写表单，点击提交按钮
            {
                // todo:通过for循环一个name的数组来简化
                string siteName = context.Request["SiteName"];
                string siteURL = context.Request["SiteURL"];
                string address = context.Request["Address"];
                string postCode = context.Request["PostCode"];
                string contactPerson = context.Request["ContactPerson"];
                string telPhone = context.Request["TelPhone"];
                string fax = context.Request["Fax"];
                string mobile = context.Request["Mobile"];
                string email = context.Request["Email"];
                CommonHelper.WriteSetting("SiteName", siteName);
                CommonHelper.WriteSetting("SiteURL", siteURL);
                CommonHelper.WriteSetting("Address", address);
                CommonHelper.WriteSetting("PostCode", postCode);
                CommonHelper.WriteSetting("ContactPerson", contactPerson);
                CommonHelper.WriteSetting("TelPhone", telPhone);
                CommonHelper.WriteSetting("Fax", fax);
                CommonHelper.WriteSetting("Mobile", mobile);
                CommonHelper.WriteSetting("Email", email);
                context.Response.Write("保存成功！");
            }
            else//展示数据
            {
                var data = CommonHelper.GetSettings();

                string html = NVelocityHelper.RenderHtml("Admin/Settings.html", data);
                context.Response.Write(html);
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