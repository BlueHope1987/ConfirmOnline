using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConfirmOnline.Operation
{
    public partial class RecodeCorrectFinished : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["Struct"] == "FinishFix")
            {
                Session.Clear();
                return;
            }
            if ((string)Session["Struct"] == "OutFixTimes")
            {
                megdiv.InnerHtml="感谢使用，您已达到最大修订次数。";
                return;
            }

            Response.Redirect("RecodeLookup");
        }
    }
}