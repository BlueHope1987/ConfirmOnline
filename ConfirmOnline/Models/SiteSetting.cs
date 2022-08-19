using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConfirmOnline.Models
{
    public class SiteSetting
    {
        [Key]
        [ScaffoldColumn(false)] //隐藏域
        public int CfgID { get; set; } //配置ID

        [Required, StringLength(20), Display(Name = "配置名")]
        public string CfgName { get; set; } //配置名

        public string CfgCreator { get; set; } //配置创建者ID

        public DateTime CfgCrtTime { get; set; } //配置创建时间

        public bool CfgIsEnable { get; set; } //仅有一项配置为活动的，无活动站点关闭

        public DateTime SiteEnabTimSt { get; set; } //启用时间 yyyy-mm-dd-hh-mm-ss,1980-1-1为无限制

        public DateTime SiteEnabTimEd { get; set; } //停用时间 yyyy-mm-dd-hh-mm-ss,1980-1-1为无限制

        [StringLength(20)]
        public string SiteName { get; set; } //站点名称

        public string SiteWelcomeWord { get; set; } //欢迎词

        public string SiteCopyRightStr { get; set; } //版权信息

        public string SiteContactStr { get; set; } //联系信息

        public bool UserRegEnab { get; set; } //开放普通用户注册，仅注册用户访问

        public int AllowFixTimes { get; set; } //允许数据重复修改次数，0为不允许，负为无限制或其他定义

        public string DateSource { get; set; } //数据源 数据驱动:工作簿:工作表

        public string SouColReDef { get; set; } //列名重路由 2:命名,1:命名,3:命名...

        public int SouRowRangeStart { get; set; } //数据起始列(0为无限制)

        public int SouRowRangeEnd { get; set; } //数据终列(0为无限制)

        public string QueryMeth { get; set; } //指派至少两列供查询验证 1,2,4,5...

        public string QueryMethRef { get; set; } //查询方法参考 格式类型、加密... -,hash,

        public decimal SouEntNum { get; set; } //数据条目数统计

        public decimal FixEntNum { get; set; } //修订条目数统计

    }
}