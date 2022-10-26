using System;
using System.ComponentModel.DataAnnotations;

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

        public string FixRow { get; set; } //修订记录定位索引，配置式QueryMeth对应的值如: 赵六,5

        public string FixCol { get; set; } //修订列，原始表格列号, -1表示不需修订的确认标识

        public string FixNew { get; set; } //修订后内容

        public string FixOld { get; set; } //修订前内容参考
    }
}