using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ChangePassword : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && string.IsNullOrEmpty((string)Session["id"]))
        {
            //未登录时提示登录
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../ReaderLogin.aspx';</script>");
        }
    }
    protected void btChangePassword_Click(object sender, EventArgs e)
    {
        //确保已经通过了所有验证控件
        if (!rfvOldPassword.IsValid || !rfvNewPassword.IsValid || !rfvConfirmNewPassword.IsValid || !cvNewPassword.IsValid)
            return;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select * from Readers where ReaderId = @u and Password = @p";
        MySqlParameter param;
        param = new MySqlParameter("@u", Session["id"]);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@p", tbOldPassword.Text);
        cmd.Parameters.Add(param);
        object res = cmd.ExecuteScalar();
        conn.Close();
        if (res != null)
        {
            //更改密码
            conn.Open();
            MySqlCommand ChangePasswordCmd = conn.CreateCommand();
            ChangePasswordCmd.CommandText = "UPDATE Readers SET Password = @np WHERE ReaderId = @u;";
            param = new MySqlParameter("@u", Session["id"]);
            ChangePasswordCmd.Parameters.Add(param);
            param = new MySqlParameter("@np", tbNewPassword.Text);
            ChangePasswordCmd.Parameters.Add(param);
            ChangePasswordCmd.ExecuteNonQuery();
            conn.Close();
            Session["id"] = null;
            Response.Write("<script type='text/javascript'>alert('OK');location.href='../ReaderLogin.aspx';</script>");
        }
        else
            Response.Write("<script>alert('" + Resources.Resource.WrongPassword + "')</script>");
    }
}