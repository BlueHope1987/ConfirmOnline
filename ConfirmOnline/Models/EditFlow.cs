using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConfirmOnline.Models
{
    public class EditFlow
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int CfgID { get; set; } //隶属配置式

        public string FixerID { get; set; } //修订者ID

        public string FixerDetal { get; set; } //修订者详情 IP 客户端
        public DateTime FixerDate { get; set; } //修订时间

        public decimal FixRow { get; set; } //修订行，0表示不需修订的确认标识

        public decimal FixCol { get; set; } //修订列，重路由后

        public string FixNew { get; set; } //修订后内容

        public string FixOld { get; set; } //修订前内容参考
    }
}