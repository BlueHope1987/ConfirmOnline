using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;
using ConfirmOnline.Models;

namespace ConfirmOnline.Operation
{
    public partial class dataviewtest : System.Web.UI.Page
    {

        public SiteMaster mstPg;
        private List<string> souCol, qurMth;


        protected void Page_Load(object sender, EventArgs e)
        {
            souCol = new List<string>(((SiteSetting)Application["SystemSet"]).SouColReDef.Split(','));
            qurMth = new List<string>(((SiteSetting)Application["SystemSet"]).QueryMeth.Split(',')); //TODO:空值处理错误处理

            prossPrePage(sender,e);

            //Request.Form;
            ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("../App_Data/") + ((SiteSetting)Application["SystemSet"]).DataSource,((SiteSetting)Application["SystemSet"]).DataTable);
  
            DataGrid1.DataSource = visiter.getDataSet().Tables[0].DefaultView;
            DataGrid1.DataBind();

            //Page.Application
        }

        protected void prossPrePage(object sender, EventArgs e)
        {
            Dictionary<string, string> textlist = new Dictionary<string, string>();
            TextBox txt;

            if (!IsPostBack)
            {
                if (Page.PreviousPage != null)// 页面本身也是一个类
                {
                    RecodeLookup PrePage = (RecodeLookup)Page.PreviousPage;
                    textlist = PrePage.queryList;

                    //if (PrePage != null)
                    //{
                    //    foreach (string s in souCol)
                    //    {
                    //        foreach (string q in qurMth)
                    //        {
                    //            if (s.IndexOf(q) == 0)
                    //            {
                    //                txt = PrePage.FindControl("txt" + s.Split(':')[0]) as TextBox;
                    //                textlist.Add(q, txt.Text);
                    //            }
                    //        }
                    //    }
                    //    return;
                    //}
                }
            }

        }


    }
}
