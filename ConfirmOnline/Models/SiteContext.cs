using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConfirmOnline.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext() : base("SiteContext")
        {
        }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        public DbSet<EditFlow> EditFlow { get; set; }
    }
}