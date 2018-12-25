using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Help 的摘要说明
/// </summary>
public static class Help
{
    public static void LibrarianMenuInit(Page page, object sender, EventArgs e)
    {
        var loginView = page.Master.FindControl("LoginView") as LoginView;
        var hlMyAccount = loginView.FindControl("hlMyAccount") as HyperLink;
        if (hlMyAccount != null)
        {
            hlMyAccount.Visible = false;
        }
        var lbLogout = loginView.FindControl("lbLogout") as LinkButton;
        if (lbLogout != null)
        {
            lbLogout.PostBackUrl = "~/Pages/LibrarianLogin.aspx";
        }

        var menu = page.Master.FindControl("LibrarianMenu") as Menu;
        menu.Enabled = true;
        menu.Visible = true;
    }
}