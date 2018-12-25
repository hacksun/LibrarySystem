using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Web.Security;
using System.Configuration;

public partial class Pages_MasterPage : BaseMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty((string)Session["lid"]))
            {
                string url = HttpContext.Current.Request.Url.Host;
                //登录管理员或者超级管理员后不能访问读者界面
                if (string.Equals((string)Session["lid"], ConfigurationManager.AppSettings["AdminAccount"].ToString()))
                    Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.AdminLogined + "');top.location.href='AdminPages/Settings.aspx';</script>");
                else
                    Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LibrarianLogined + "');top.location.href='LibrarianPages/IssueBookDemo.aspx';</script>");
            }
            ddlLanguages.SelectedValue = Session["PreferredCulture"].ToString();
        }
        if (!string.IsNullOrEmpty((string)Session["id"])) 
        {
            hlMyAccount.Visible = true;
            LibrarianLogin.Visible = false;
        }
    }

    protected void ddlLanguages_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PreferredCulture"] = ddlLanguages.SelectedValue.ToString();
        Response.Redirect(Request.Url.ToString());
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Remove("id");
        Session.Remove("lid");
        Response.Redirect("~/Pages/SearchDemo.aspx");
    }
}
