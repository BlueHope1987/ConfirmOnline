using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;

namespace ConfirmOnline.Logic
{
    public class ExcelVisiter
    {
        readonly string filepath;

        public ExcelVisiter(string fp)
        {
            filepath = fp;
        }

        public bool Test(System.Web.UI.WebControls.GridView dg)
        {
            string sConnectionString;

            if (System.IO.Path.GetExtension(filepath).Equals(".xls"))
            {
                // xls Microsoft.Jet.OLEDB.4.0 不好用
                //HDR=YES/NO 第一行是否有字段名默认YES
                //IMEX=1 解决数字字符混合不正常 0汇出模式仅写入 1汇入模式仅读取 2连接模式可读写
                sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    "Data Source=" + filepath + ";" +
                    "Extended Properties='Excel 8.0; HDR=NO; IMEX=1'";
            }
            else if (System.IO.Path.GetExtension(filepath).Equals(".xlsx"))
            {
                //xlsx Microsoft.Ace.OleDb.12.0 缺组件
                sConnectionString = "Provider=Microsoft.Ace.OleDb.12.0;" +
                "data source=" + filepath + ";" +
                "Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
                //filepath=Server.MapPath("../App_Data/test.xlsx")
            }
            else
            {
                return false;
            }

            // Create connection object by using the preceding connection string.
            OleDbConnection objConn = new OleDbConnection(sConnectionString);

            // Open connection with the database.
            objConn.Open();

            // The code to follow uses a SQL SELECT command to display the data from the worksheet.
            // Create new OleDbCommand to return data from worksheet.
            OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
            //需要在Excel 插入菜单-名称-定义 将数据选区定义为myRange1 若工作表为 [Sheet1$] 若不指特定第一个[$A1:R65536]

            // Create new OleDbDataAdapter that is used to build a DataSet
            // based on the preceding SQL SELECT statement.
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

            // Pass the Select command to the adapter.
            objAdapter1.SelectCommand = objCmdSelect;

            // Create new DataSet to hold information from the worksheet.
            DataSet objDataset1 = new DataSet();

            // Fill the DataSet with the information from the worksheet.
            objAdapter1.Fill(objDataset1, "XLData");

            // Bind data to DataGrid control.
            dg.DataSource = objDataset1.Tables[0].DefaultView;
            dg.DataBind();

            // Clean up objects.
            objConn.Close();

            return true;
        }

        //待研究返回工作表清单
        public List<string> ListSheetInExcel()
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filepath;
            if (System.IO.Path.GetExtension(filepath).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (System.IO.Path.GetExtension(filepath).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);
            List<string> listSheet = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        listSheet.Add(drSheet["TABLE_NAME"].ToString());
                    }
                }
            }
            return listSheet;
        }

        //待研究返回工作表清单
        public static List<string> ToExcelsSheetList(string filepath)
        {
            List<string> sheets = new List<string>();
            using (OleDbConnection connection =
                    new OleDbConnection((filepath.TrimEnd().ToLower().EndsWith("x"))
                    ? "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filepath + "';" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'"
                    : "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filepath + "';Extended Properties=Excel 8.0;"))
            {
                connection.Open();
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dt.Rows)
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                    {
                        string s = drSheet["TABLE_NAME"].ToString();
                        sheets.Add(s.StartsWith("'") ? s.Substring(1, s.Length - 3) : s.Substring(0, s.Length - 1));
                    }
                connection.Close();
            }
            return sheets;
        }

        //待研究连接方法
        protected void testcon1()
        {
            //HttpPostedFile jvFile = FileUpload1.PostedFile;
            string jvPath = filepath;
            //jvFile.SaveAs(jvPath);

            string[] begins = {
                 "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=",
                 "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=",
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=",
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
        };
            string[] ends = {
                ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"",
                ";Extended Properties=\"Excel 8.0;HDR=Yes;\"",
               ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"",
               ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"",
               ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"",
               ";Extended Properties=\"Excel 12.0;HDR=YES\"",
               ";Extended Properties=\"Excel 12.0 Macro;HDR=YES\"",
               ";Extended Properties=\"text;HDR=Yes;FMT=Delimited\"",
               ";Extended Properties=\"text;HDR=Yes;FMT=Fixed\";"
       };

            for (int i = 0; i < begins.Length; i++)
            {
                System.Text.StringBuilder sbExcelFileConnStr = new System.Text.StringBuilder();
                sbExcelFileConnStr.Append(begins[i]);
                sbExcelFileConnStr.Append(jvPath);
                sbExcelFileConnStr.Append(ends[i]);

                OleDbConnection dbConn = new OleDbConnection(sbExcelFileConnStr.ToString());
                string[] excelSheets = { };
                try
                {
                    dbConn.Open();
                }
                catch (Exception ex)
                {
                    // fails here with "System.Data.OleDb.OleDbException: 
                    // External table is not in the expected format." 
                    // 
                    // 
                }
            }
        }
    }
}