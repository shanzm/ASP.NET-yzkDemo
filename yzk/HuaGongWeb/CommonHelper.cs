using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HuaGongWeb
{
    public class CommonHelper
    {
        /// <summary>
        /// 查询产品种类，使用在给Head.html页面提供产品种类
        /// </summary>
        /// <returns></returns>
        public static DataRowCollection GetProductCategories()//注意返回值类型，是行的集合类型DataRowCollection
        {
            DataTable dtProdcutCategory = SqlHelper.GetDataTable("select * from T_ProductCategories", CommandType.Text);

            return dtProdcutCategory.Rows;
        }


        /// <summary>
        /// 在Settings.ashx中调用的
        /// 从数据库读取数据后放在匿名对象中，以便传给html模版
        /// </summary>
        /// <returns>返回一个匿名对象data</returns>
        public static object GetSettings()//注意我们的返回类型是推断类型var，所以定义函数的时候写的返回类型是object，你不能写为var，知道为啥不？定义的var是编译器把var自己推断出来的，和你定义的string ，int是没有区别的
        {
            string siteName = CommonHelper.ReadSetting("SiteName");
            string siteURL = CommonHelper.ReadSetting("SiteURL");
            string address = CommonHelper.ReadSetting("Address");
            string postCode = CommonHelper.ReadSetting("PostCode");
            string contactPerson = CommonHelper.ReadSetting("ContactPerson");
            string telPhone = CommonHelper.ReadSetting("TelPhone");
            string fax = CommonHelper.ReadSetting("Fax");
            string mobile = CommonHelper.ReadSetting("Mobile");
            string email = CommonHelper.ReadSetting("Email");
            string title = "系统配置设置";

            var data = new { SiteName = siteName, SiteURL = siteURL, Address = address, PostCode = postCode, ContactPerson = contactPerson, TelPhone = telPhone, Fax = fax, Mobile = mobile, Email = email,Title=title  };
            return data;
        }


        /// <summary>
        /// 在Settings.ashx中调用的
        /// 从数据库查找配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ReadSetting(string name)
        {
            DataTable dt = SqlHelper.GetDataTable("select value from T_Settings where Name=@Name", CommandType.Text, new SqlParameter("@Name", name));
            if (dt.Rows.Count <= 0)
            {
                //CommonHelper中是封装的方法，我们注意一般处理程序中的方法区分
                //我们不能在这里直接在页面显示异常结果，我们要抛异常，在调用这个方法的时候，使用try...catch处理
                throw new Exception("找不到Name=" + name + "的配置项");
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("找到多条Name=" + name + "的配置项");
            }
            return (string)dt.Rows[0]["Value"];
        }


        /// <summary>
        /// 在Settings.ashx中调用的
        /// 保存用户提交的数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void WriteSetting(string name, string value)
        {
            SqlParameter[] param =
                {
                new SqlParameter("@Name", name),
                new SqlParameter ("@Value",value)
            };
            SqlHelper.ExecuteNonquery("Update T_Settings set Value=@Value where Name=@Name", CommandType.Text, param);

        }
    }
}