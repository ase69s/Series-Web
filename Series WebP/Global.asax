<%@ Application Language="C#" %>

<%-- ReSharper disable EmptyGeneralCatchClause --%>
<script RunAt="server">

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception err = Server.GetLastError().GetBaseException();
        //Page pagex=(Page) sender;
        //Funciones.Mensaje(pagex, pagex.UniqueID, err.Message + " " + err.StackTrace);
        //Server.Transfer("PaginaError.aspx");
        Funciones.EnviarMailError(err);
        Response.Redirect("Series.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started 
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends.
    }
    protected string BuildMessage()
    {
        StringBuilder strMessage = new StringBuilder();
        try
        {
            Exception err = Server.GetLastError().GetBaseException();

            string stackTrace = err.StackTrace;

            stackTrace = stackTrace.Replace(" at ", "<br />");





            HttpContext context = HttpContext.Current;

            string sForm = context.Request.Form.ToString();

            string[] arrFormVars = sForm.Split(new[] { '&' });

            strMessage.Append("<style type='text/css'>");

            strMessage.Append("<!--");

            strMessage.Append(".basix {");

            strMessage.Append("font-family: Verdana, Arial, Helvetica, sans-serif;");

            strMessage.Append("font-size: 12px;");

            strMessage.Append("}");

            strMessage.Append(".header1 {");

            strMessage.Append("font-family: Verdana, Arial, Helvetica, sans-serif;");

            strMessage.Append("font-size: 12px;");

            strMessage.Append("font-weight: bold;");

            strMessage.Append("color: #000099;");

            strMessage.Append("}");

            strMessage.Append(".tlbbkground1 {");

            strMessage.Append("background-color: #000099;");

            strMessage.Append("}");

            strMessage.Append("-->");

            strMessage.Append("</style>");

            strMessage.Append("<table width='85%' border='0' align='center' cellpadding='5' cellspacing='1' class='tlbbkground1'>");

            strMessage.Append("<tr bgcolor='#eeeeee'>");

            strMessage.Append("<td colspan='2' class='header1'>Page Error</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>IP Address</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + Request.ServerVariables["REMOTE_ADDR"] + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>User Agent</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + Request.ServerVariables["HTTP_USER_AGENT"] + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Page</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + Request.Url.AbsoluteUri + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Time</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + DateTime.Now + " EST</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Message</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + err.Message + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Source</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + err.Source + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Stack Trace</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + stackTrace + "</td>");

            strMessage.Append("</tr>");

            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Method</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix'>" + err.TargetSite + "</td>");

            strMessage.Append("</tr>");



            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Form Variables</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix' style='padding-top: 15px; padding-left: 25px'>");

            strMessage.Append("<table cellpadding='5'>");



            foreach (string t in arrFormVars)
            {
                string[] arr = t.Split(new[] { '=' });

                if (arr[0] == "__VIEWSTATE") continue;
                arr[0] = arr[0].Replace("%3a", " - ");

                arr[1] = arr[1].Replace("+", " ");

                strMessage.Append("<tr>");

                strMessage.Append("<td width='300' style='font-weight: bold; font-size: 9pt; border-bottom: 1px solid #00000;' nowrap>" + arr[0] + ":</td>");

                strMessage.Append("<td class='basix' style='border-bottom: 1px solid #00000'>" + arr[1] + "</td>");

                strMessage.Append("</tr>");
            }

            strMessage.Append("</table>");

            strMessage.Append("</td>");

            strMessage.Append("</tr>");



            strMessage.Append("<tr>");

            strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>Cookie Variables</td>");

            strMessage.Append("<td bgcolor='#FFFFFF' class='basix' style='padding-top: 15px; padding-left: 25px'>");

            strMessage.Append("<table cellpadding='5'>");



            for (int i = 0; i < context.Request.Cookies.Count; i++)
            {

                HttpCookie cookie = context.Request.Cookies[i];

                strMessage.Append("<tr>");

                strMessage.Append("<td width='300' style='font-weight: bold; font-size: 9pt; border-bottom: 1px solid #00000;' nowrap>" + cookie.Name + ":</td>");

                strMessage.Append("<td class='basix' style='border-bottom: 1px solid #00000'>" + cookie.Value + "</td>");

                strMessage.Append("</tr>");

            }

            strMessage.Append("</table>");

            strMessage.Append("</td>");

            strMessage.Append("</tr>");



            strMessage.Append("</table>");
        }
        catch { }
        return (strMessage.ToString());

    }
</script>
<%-- ReSharper restore EmptyGeneralCatchClause --%>

