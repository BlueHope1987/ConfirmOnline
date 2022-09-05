using ConfirmOnline.Logic;
using System;

namespace ConfirmOnline.Operation
{
    public partial class WebFormTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("客户端计算机名：" + Request.UserHostName + "<BR />");
            Response.Write("客户端IP：" + Request.UserHostAddress + "<BR />");
            Response.Write("浏览器：" + Request.Browser.Browser + "<BR />");
            Response.Write("浏览器版本：" + Request.Browser.Version + "<BR />");
            Response.Write("浏览器类型：" + Request.Browser.Type + "<BR />");
            Response.Write("客户端操作系统：" + Request.Browser.Platform + "<BR />");
            Response.Write("是否支持Java：" + Request.Browser.JavaApplets + "<BR />");
            Response.Write("是否支持框架网页：" + Request.Browser.Frames + "<BR />");
            Response.Write("是否支持Cookie：" + Request.Browser.Cookies + "<BR />");
            Response.Write("客户端.NET Framework版本：" + Request.Browser.ClrVersion + "<BR />");
            Response.Write("JScript版本：" + Request.Browser.JScriptVersion + "<BR />");



            Response.Write("请求的虚拟路径：" + Request.Path + "<BR />");
            //Response.Write("title：" + Request.He + "<BR />");
            for (int i = 0; i < Request.Headers.Count; i++)
            {
                Response.Write(Request.Headers.Keys[i] + ":" + Request.Headers[Request.Headers.Keys[i]] + "<BR />");
            }
            Response.Write("请求的物理路径：" + Request.PhysicalPath + "<BR />");



            Response.Write("浏览器类型和版本：" + Request.ServerVariables["HTTP_USER_AGENT"] + "<BR />");
            Response.Write("用户的IP地址：" + Request.ServerVariables["REMOTE_ADDR"] + "<BR />");
            Response.Write("请求的方法：" + Request.ServerVariables["REQUEST_METHOD"] + "<BR />");
            Response.Write("服务器的IP地址：" + Request.ServerVariables["LOCAL_ADDR"] + "<BR />");

            Response.Write(string.Join("<br/>", ExcelVisiter.listOLEDBDrv().ToArray()));
        }
    }
}