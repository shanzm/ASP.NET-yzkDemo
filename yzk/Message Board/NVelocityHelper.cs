using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Message_Board
{
    public class NVelocityHelper
    {
        
        /// <summary>
        /// 用数据date填充模版template中的Date，生成html
        /// </summary>
        /// <param name="templatesName">模板文件</param>
        /// <param name="data">传递给模版的数据</param>
        /// <returns>html</returns>
        public  static string RenderHtml(string templateName, object data)
        {
            VelocityEngine vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, System.Web.Hosting.HostingEnvironment.MapPath("~/templates"));//模板文件所在的文件夹
            vltEngine.Init();

            VelocityContext vltContext = new VelocityContext();

            vltContext.Put("Data", data);//设置参数，在模板中可以通过$data来引用
         
            Template vltTemplate = vltEngine.GetTemplate(templateName);
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            string html = vltWriter.GetStringBuilder().ToString();
            
            return html;
        }
    }
}