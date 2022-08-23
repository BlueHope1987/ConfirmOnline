using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;
using ConfirmOnline.Models;

namespace ConfirmOnline.Operation
{
    public partial class RecodeLookup : System.Web.UI.Page
    {
        public SiteMaster mstPg;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //页面Page_Load优先于Master的，方法不可行
            List<string> qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理
            foreach (string q in qurMth)
            {

            }
            

            //ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("../App_Data/test.xlsx"), "Sheet1");
            //visiter.Test(DataGrid1);
            DataGrid1.DataSource = ((DataSet)Application["WorkSheet"]).Tables[0].DefaultView;
            DataGrid1.DataBind();

            //Page.Application
        }
    }
}