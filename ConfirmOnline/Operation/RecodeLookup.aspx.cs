using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;


namespace ConfirmOnline.Operation
{
    public partial class RecodeLookup : System.Web.UI.Page
    {
        public SiteMaster masterPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            masterPage = (SiteMaster)this.Master;
            ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("../App_Data/test.xlsx"));
            visiter.Test(DataGrid1);
            //Page.Application
        }
    }
}