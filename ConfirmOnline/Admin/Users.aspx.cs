using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ConfirmOnline.Models;
using System.Data;

namespace ConfirmOnline.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NewButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Account/Register");
            return;
        }

        protected void SetAdmin_Click(object sender, EventArgs e)
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdUserResult; var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = userMgr.FindById((String)UserMgrForm.DataKey.Value);
            if (!userMgr.IsInRole(appUser.Id, "Admin"))
            {
                IdUserResult = userMgr.AddToRole(appUser.Id, "Admin");
            }
            else
            {
                IdUserResult = userMgr.RemoveFromRole(appUser.Id, "Admin");
            }
            UserMgrLst.DataBind();
            return;
        }
    }
}