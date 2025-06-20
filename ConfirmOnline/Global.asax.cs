﻿using ConfirmOnline.Logic;
using ConfirmOnline.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;


namespace ConfirmOnline
{

    public class Global : HttpApplication
    {
        public SiteSetting SystemSet;

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //初始化业务数据库
            Database.SetInitializer(new SiteDbInitializer());

            //添加规则与用户
            RoleActions roleActions = new RoleActions();
            roleActions.AddUserAndRole();

            //添加路由
            RegisterCustomRoutes(RouteTable.Routes);

            //读取配置
            Application["SystemSet"] = GetSiteSetting().First();

            //将数据表读取全局 不适用
            //ExcelVisiter visiter = new ExcelVisiter(Server.MapPath("App_Data/UploadExcels/")+((SiteSetting)Application["SystemSet"]).DataSource, Server.MapPath("App_Data/UploadExcels/") + ((SiteSetting)Application["SystemSet"]).DataTable);
            //Application["WorkSheet"] = visiter.getDataSet();
        }

        public IQueryable<SiteSetting> GetSiteSetting()
        {
            var _db = new ConfirmOnline.Models.SiteContext();
            IQueryable<SiteSetting> query = _db.SiteSetting.Where(s => s.CfgIsEnable == true);
            return query;
        }

        void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.Ignore("Operation/{resource}");
            routes.Ignore("Operation/{*pathInfo}");
            routes.Ignore("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });

            //routes.MapPageRoute(
            //    "ProductsByCategoryRoute",
            //    "Category/{categoryName}",
            //    "~/ProductList.aspx"
            //);
            //routes.MapPageRoute(
            //    "ProductByNameRoute",
            //    "Product/{productName}",
            //    "~/ProductDetails.aspx"
            //);
        }

        //应用程序级错误处理

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs.

            // Get last error from the server
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                    Server.Transfer("~/ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
                }
            }
        }
    }
}