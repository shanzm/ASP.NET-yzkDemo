using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace JSON
{
    /// <summary>
    /// Json 的摘要说明
    /// </summary>
    public class Json : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Person p1 = new Person() { Name = "张三", Age = 15, Email = "vhsj@qq.com" };
            Person p2 = new Person() { Name = "李四", Age = 16, Email = "lisij@qq.com" };
            Person p3 = new Person() { Name = "王五", Age = 17, Email = "whwu@qq.com" };
            context.Response.ContentType = "text/html";
            List<Person> p = new List<Person>();
            p.Add(p1);
            p.Add(p2);
            p.Add(p3);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string JsonPerson = jss.Serialize(p);

            ///JsonPerson=[{"Name":"张三","Age":15,"Email":"vhsj@qq.com"},{"Name":"张三","Age":15,"Email":"vhsj@qq.com"},{"Name":"张三","Age":15,"Email":"vhsj@qq.com"}]
            context.Response.Write(JsonPerson);


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