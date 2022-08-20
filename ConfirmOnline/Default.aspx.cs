using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfirmOnline.Logic;
using ConfirmOnline.Models;

namespace ConfirmOnline
{
    public partial class _Default : Page
    {
        public SiteMaster masterPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            masterPage = (SiteMaster)this.Master;
        }
    }
}