using System;
using System.Web.UI;

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