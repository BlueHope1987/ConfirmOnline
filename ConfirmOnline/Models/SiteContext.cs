using System.Data.Entity;

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