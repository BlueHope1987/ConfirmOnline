using ConfirmOnline.Logic;
using System;
using System.Web;

namespace ConfirmOnline
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create safe error messages.
            string generalErrorMsg = "这个站点发生了某个问题，请重试。 " +
                "如果问题继续，请联系技术支持。";
            string httpErrorMsg = "发生了一个HTTP错误，页面未找到,请重试。";
            string unhandledErrorMsg = "应用代码发生未处理异常。";

            // Display safe error message.
            FriendlyErrorMsg.Text = generalErrorMsg;

            // Determine where error was handled.
            string errorHandler = Request.QueryString["handler"];
            if (errorHandler == null)
            {
                errorHandler = "异常页";
            }

            // Get the last error from the server.
            Exception ex = Server.GetLastError();

            // Get the error number passed as a querystring value.
            string errorMsg = Request.QueryString["msg"];
            if (errorMsg == "404")
            {
                ex = new HttpException(404, httpErrorMsg, ex);
                FriendlyErrorMsg.Text = ex.Message;
            }

            // If the exception no longer exists, create a generic exception.
            if (ex == null)
            {
                ex = new Exception(unhandledErrorMsg);
            }

            // Show error details to only you (developer). LOCAL ACCESS ONLY.
            if (Request.IsLocal)
            {
                // Detailed Error Message.
                ErrorDetailedMsg.Text = ex.Message;

                // Show where the error was handled.
                ErrorHandler.Text = errorHandler;

                // Show local access details.
                DetailedErrorPanel.Visible = true;

                if (ex.InnerException != null)
                {
                    InnerMessage.Text = ex.GetType().ToString() + "<br/>" +
                        ex.InnerException.Message;
                    InnerTrace.Text = ex.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = ex.GetType().ToString();
                    if (ex.StackTrace != null)
                    {
                        InnerTrace.Text = ex.StackTrace.ToString().TrimStart();
                    }
                }
            }

            // Log the exception.
            ExceptionUtility.LogException(ex, errorHandler);

            // Clear the error from the server.
            Server.ClearError();
        }
    }
}