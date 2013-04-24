using System;
using System.Text;
using System.Web;
using System.Web.UI;

public static class Funciones
{
    /*public static void Mensaje(Page page, string UniqueIDAJAX, string mensaje)
        {
            string Script = "<script language='JavaScript'>"
                            + "alert('" + mensaje + "')" +
                            "</script>";
            ScriptManager.RegisterStartupScript(page, typeof(Page), UniqueIDAJAX, Script, false);
        }*/
    /*public static void AbrirURL(Page page, string UniqueIDAJAX, string url)
    {
        string Script = "<script language='JavaScript'>"
                       + "window.open('" + url + "', '_blank')" +
                    "</script>";
        ScriptManager.RegisterStartupScript(page, typeof(Page), UniqueIDAJAX, Script, false);
    }*/
    public static void RecargarPagina(Page page, string UniqueIDAJAX)
    {
        const string Script = "<script language='JavaScript'>"
                              + "__doPostBack('__Page', 'MyCustomArgument');" +
                              "</script>";
        ScriptManager.RegisterStartupScript(page, typeof(Page), UniqueIDAJAX, Script, false);
    }
    /*public static void Click(Page page, string UniqueIDAJAX,Control control)
    {
        const string Script = "<script language='JavaScript'>"
                              + "__doPostBack('__Page', 'MyCustomArgument');" +
                              "</script>";
        ScriptManager.RegisterStartupScript(page, typeof(Page), UniqueIDAJAX, Script, false);
    }
    */
    public static void EnviarMailError(Exception ex)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add("ase69s@gmail.com");
        mail.From = new System.Net.Mail.MailAddress("soporteseriesweb@sistec.es");
        mail.IsBodyHtml = true;
        mail.Subject = "Error SeriesWeb";
        mail.Body = BuildMessage(ex);

        try
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                                  {
                                                      Host = "194.149.221.24",
                                                      Credentials =
                                                          new System.Net.NetworkCredential("vangelis", "sanchoyerto")
                                                  };

            smtp.Send(mail);
            //mail.To = "softges@lur.es";
            //SmtpMail.Send(mail);
        }
// ReSharper disable EmptyGeneralCatchClause
        catch
// ReSharper restore EmptyGeneralCatchClause
        {
            //ServicioTecnico servi = new ServicioTecnico();
            //servi.EnviarMailError("CapaAsistencias.cs", exc);

        }
        finally
        {
            mail.Dispose();
        }
    }
    private static string BuildMessage(Exception err)
    {
        StringBuilder strMessage = new StringBuilder();
        try
        {
            //Exception err = Server.GetLastError().GetBaseException();

            string stackTrace = err.StackTrace;

            stackTrace = stackTrace.Replace(" at ", "<br />");

            HttpContext context = HttpContext.Current;

            //añadido
            HttpRequest Request = context.Request;


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

            //zona mia
            if (err.Data.Count > 0)
            {
                strMessage.Append("<tr>");
                strMessage.Append("<td width='100' align='right' bgcolor='#eeeeee' class='header1' nowrap>SQL Data</td>");
                strMessage.Append("<td bgcolor='#FFFFFF' class='basix' style='padding-top: 15px; padding-left: 25px'>");
                strMessage.Append("<table cellpadding='5'>");

                strMessage.Append("<tr>");
                strMessage.Append("<td width='300' style='font-weight: bold; font-size: 9pt; border-bottom: 1px solid #00000;' nowrap>" + "SQL" + ":</td>");
                strMessage.Append("<td class='basix' style='border-bottom: 1px solid #00000'>" + err.Data["SQLx"] + "</td>");
                strMessage.Append("</tr>");

                strMessage.Append("</table>");
                strMessage.Append("</td>");
                strMessage.Append("</tr>");
            }
            //zona mia

            strMessage.Append("</table>");
        }
// ReSharper disable EmptyGeneralCatchClause
        catch { }
// ReSharper restore EmptyGeneralCatchClause
        return (strMessage.ToString());
    }
}