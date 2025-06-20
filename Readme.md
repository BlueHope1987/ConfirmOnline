信息在线核实程序
ver 0.8.0.0

======简介：

管理员登录上传Excel数据表文件，设置并选取数列查询列；用户访问主页并通过查询项找到自己的条目，完成信息确认及修改；管理员查询并下载修订结果。
可供局域网用户简单的信息查询、修订及更新用例。

基于“ASP.Net Web 应用程序”项目模板及用户管理逻辑、数据库；
附加功能数据库，Entity Framework代码先行（Code First）、Microsoft.Ace.OleDb.12.0、WebForm 等实现。
	复位网站可删除App_Data目录下的数据库相关mdf、ldf文件，EF Code First会自动重建。

该项目由2022年某时，鄙人单位里偶然一个临时全局需要，看起来简单却尚无简便理想的方案，
——甚至各种在线问卷系统也未能实现该场景。于是撸起袖子自己攒出了这个小程序且当演练，达到可用状态，虽然后来并未被采纳。
2025年上半年软考后，鄙人重整状态为恢复实践驱动，整理旧仓库时给clone来github，这个小玩具且算是灰色发布了。
功能实现还很简陋有待打磨，但有需要是可以拿来配置下就用了。

======项目Release服务器端依赖：

IIS 或 IIS Express 服务器
	https://www.microsoft.com/zh-CN/download/details.aspx?id=48264
.Net Framework 4.5
	https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/net45
Microsoft.Ace.OleDb.12.0
	即 Microsoft Access 2016 数据库引擎可再发行程序包(x86优先)
	https://www.microsoft.com/zh-cn/download/details.aspx?id=54920

======项目部署

ConfirmOnline文件夹是项目解决方案目录，可用VS2015及更高版本打开、调试、部署。
条件不具备情况下，亦可不剔除工程文件，并直接放在IIS空白网站根目录运行。

======默认管理用户名及密码：
Admin123@ConfirmOnline.com
	由 Logic\RoleActions.cs 定义
