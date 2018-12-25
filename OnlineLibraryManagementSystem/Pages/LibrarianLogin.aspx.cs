using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianLogin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)Session["id"]))
        {
            //登录读者后不能同时登录管理员
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.ReaderLogined + "');location.href='SearchDemo.aspx';</script>");
        }
        else if (!string.IsNullOrEmpty((string)Session["lid"]))
        {
            if (string.Equals((string)Session["lid"], ConfigurationManager.AppSettings["AdminAccount"].ToString()))
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.AdminLogined + "');location.href='AdminPages/Settings.aspx';</script>");
            else
                Response.Redirect("~/Pages/LibrarianPages/IssueBookDemo.aspx");
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select * from Librarians where Account = @u and Password = @p";
        MySqlParameter param;
        param = new MySqlParameter("@u", Login1.UserName);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@p", Login1.Password);
        cmd.Parameters.Add(param);
        object res = cmd.ExecuteScalar();
        conn.Close();
        if (res != null)
        {
            //使用Session方式保存账户信息,lid为管理员id
            Session["lid"] = Login1.UserName;
            e.Authenticated = true;
        }
        else
            e.Authenticated = false;
    }
}