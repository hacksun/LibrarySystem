using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)Session["id"]))
        {
            Response.Redirect("~/Pages/SearchDemo.aspx");
        }
        else if (!string.IsNullOrEmpty((string)Session["lid"]))
        {
            if (string.Equals((string)Session["lid"], ConfigurationManager.AppSettings["AdminAccount"].ToString()))
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.AdminLogined + "');location.href='AdminPages/Settings.aspx';</script>");
            else
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LibrarianLogined + "');location.href='LibrarianPages/IssueBookDemo.aspx';</script>");
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select * from Readers where Phone = @phone and Password = @password";
        cmd.Parameters.AddWithValue("@phone", Login1.UserName);
        cmd.Parameters.AddWithValue("@password", Login1.Password);
        object res = cmd.ExecuteScalar();
        if (res != null)
        {
            cmd.CommandText = "select ReaderId from Readers where Phone = @phone";
            MySqlDataReader reader = cmd.ExecuteReader();
            //使用Session方式保存账户信息
            reader.Read();
            string str_id = reader["ReaderId"].ToString();
            Session.Add("id", str_id);
            e.Authenticated = true;
        }
        else
            e.Authenticated = false;
        conn.Close();
    }
}