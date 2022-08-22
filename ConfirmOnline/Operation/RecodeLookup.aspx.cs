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
        public SiteMaster mstPg;
        protected void Page_Load(object sender, EventArgs e)
        {
            mstPg = (SiteMaster)this.Master;
            /*
            List<string> qurMth = new List<string>(mstPg.SystemSet.QueryMeth.Split(','));
            foreach (string q in qurMth)
            {

            }
            */
            
            ExcelVisiter visiter=new ExcelVisiter(Server.MapPath("../App_Data/test.xlsx"));
            visiter.Test(DataGrid1);
            
            //Page.Application
        }
    }
}