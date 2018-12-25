using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ChangeReaderInfomation : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && string.IsNullOrEmpty((string)Session["id"]))
        {
            //未登录时提示登录
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../ReaderLogin.aspx';</script>");
        }
    }
    protected void btNewInfomation_Click(object sender, EventArgs e)
    {
        //确保已经通过了所有验证控件
        if (!rfvNewEmail.IsValid || !rfvNewPhone.IsValid)
            return;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Readers SET Phone = @phone, Email = @email WHERE(ReaderId = @u);";
        MySqlParameter param;
        param = new MySqlParameter("@u", Session["id"]);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@email", tbNewEmail.Text);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@phone", tbNewPhone.Text);
        cmd.Parameters.Add(param);
        cmd.ExecuteNonQuery();
        conn.Close();
        Response.Write("<script type='text/javascript'>alert('OK');location.href='../SearchDemo.aspx';</script>");
    }
}