using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ConfirmOnline.Models
{
    //在首次使用上下文时的非用户数据库初始化
    public class SiteDbInitializer : DropCreateDatabaseIfModelChanges<SiteContext>
    {
        //种子数据写入
        protected override void Seed(SiteContext context)
        {
            GetSiteSetting().ForEach(s => context.SiteSetting.Add(s));
            GetEditFlow().ForEach(s => context.EditFlow.Add(s));
        }

        private static List<SiteSetting> GetSiteSetting()
        {
            var SiteSettings = new List<SiteSetting>
            {
                new SiteSetting
                {
                    AllowFixTimes=-1,
                    CfgCreator="system",
                    CfgCrtTime=DateTime.Now,
                    CfgID=1,
                    CfgName="默认配置",
                    DataSource="test.xlsx",
                    DataTable="Sheet1",
                    FixEntNum=0,
                    QueryMeth="1,2",
                    QueryMethRef="1:string:6|2,2:nub:8,3:select:男|女,4:date:yyyy-mm-dd",
                    SiteContactStr="",
                    SiteCopyRightStr="BlueHope",
                    SiteEnabTimEd=new DateTime(1980,1,1,1,1,1),
                    SiteEnabTimSt=new DateTime(1980,1,1,1,1,1),
                    CfgIsEnable=true,
                    SiteName="信息在线核实程序",
                    SiteWelcomeWord="在此开始检查，确认，修改您的个人信息。",
                    SouColReDef="2:工号,1:姓名,3:性别,4:生日,5:电话,6:部门,7:备注",
                    SouEntNum=0,
                    SouRowRangeEnd=0,
                    SouRowRangeStart=3,
                    UserRegEnab=false,
                }
            };
            return SiteSettings;
        }

        private static List<EditFlow> GetEditFlow()
        {
            var EditFlows = new List<EditFlow>
            {
                new EditFlow
                {
                    Id=1,
                    CfgID=1,
                    FixRow="张三,29",
                    FixerDate=new DateTime(2018,8,28,16,43,27),
                    FixerID="null",
                    FixerDetal="",
                    FixCol="4,5",
                    FixNew="1991-5-23,1300101123",
                    FixOld="1990-5-23,13001000001",
                },

                new EditFlow
                {
                    Id=2,
                    CfgID=1,
                    FixRow="张三,29",
                    FixerDate=new DateTime(2018,8,28,18,16,50),
                    FixerID="null",
                    FixerDetal="",
                    FixCol="4,5",
                    FixNew="1991-4-23,1300111123",
                    FixOld="1991-5-23,1300101123",
                }
            };
            return EditFlows;
        }
    }
}