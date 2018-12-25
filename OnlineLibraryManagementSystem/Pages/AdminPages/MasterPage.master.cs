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
            ddlLanguages.SelectedValue = Session["PreferredCulture"].ToString();
            if (!string.IsNullOrEmpty((string)Session["id"]))
            {
                //登录读者后不能同时访问超级管理员页面
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.ReaderLogined + "');location.href='../SearchDemo.aspx';</script>");
            }
            else if (!string.IsNullOrEmpty((string)Session["lid"]))
            {
                //登录管理员后不能同时访问超级管理员页面
                if (!string.Equals((string)Session["lid"], ConfigurationManager.AppSettings["AdminAccount"].ToString()))
                    Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LibrarianLogined + "');location.href='../LibrarianPages/IssueBookDemo.aspx';</script>");
            }
            else
            {
                //未登录时提示登录
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../AdminLogin.aspx';</script>");
            }
        }
    }

    protected void lbSignOut_Click(object sender, EventArgs e)
    {
        Session["lid"] = null;
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
