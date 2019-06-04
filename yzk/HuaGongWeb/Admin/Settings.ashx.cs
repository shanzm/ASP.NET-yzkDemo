using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuaGongWeb.Admin
{
    /// <summary>
    /// Settings 的摘要说明
    /// </summary>
    public class Settings : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
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