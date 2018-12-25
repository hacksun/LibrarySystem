using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AdminLogin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)Session["id"]))
        {
            //登录读者后不能同时登录超级管理员
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.ReaderLogined + "');location.href='SearchDemo.aspx';</script>");
        }
        else if (!string.IsNullOrEmpty((string)Session["lid"]))
        {
            if (string.Equals((string)Session["lid"], ConfigurationManager.AppSettings["AdminAccount"].ToString()))
                Response.Redirect("~/Pages/AdminPages/Settings.aspx");
            else
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LibrarianLogined + "');location.href='LibrarianPages/IssueBookDemo.aspx';</script>");
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string AdminAccount = ConfigurationManager.AppSettings["AdminAccount"].ToString();
        string AdminPassword = ConfigurationManager.AppSettings["AdminPassword"].ToString();
        if (string.Equals(Login1.UserName, AdminAccount) && string.Equals(Login1.Password, AdminPassword))
        {
            //使用Session方式保存账户信息,lid为管理员id
            Session["lid"] = Login1.UserName;
            e.Authenticated = true;
        }
        else
            e.Authenticated = false;
    }
}