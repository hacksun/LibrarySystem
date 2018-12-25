using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;

public partial class Pages_ReaderRegistration : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string value = ConfigurationManager.AppSettings.Get("Deposit") + Resources.Resource.IsDeposit;
        IsDeposit.Text = value;
    }

    protected void RegisterReader(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        string name,password, idNumber, telephone, email;
        name = TextBoxName.Text;
        idNumber = TextBoxIDNumber.Text;
        password = ConfigurationManager.AppSettings.Get("DefaultPassword");
        telephone = TextBoxTelephone.Text;
        email = TextBoxEmail.Text;
        if (! IsDeposit.Checked)
        {
            Response.Write("<script>window.alert('" + Resources.Resource.DepositError + "!');</script>");
            return;
        }
        //链接数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //检测同账号是否注册过
        string selectReaderSql = "select count(*) as num from Readers where IdNumber = ?idNumber;";
        string insertReaderSql = "INSERT INTO Readers(Password, Name, IdNumber, Phone, Email) " +
            "VALUES(?password, ?name, ?idNumber, ?phone, ?email);";
        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmd2 = new MySqlCommand(selectReaderSql, OLMSDBConnection);
            cmd2.Parameters.AddWithValue("?idNumber", idNumber);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if(count > 0)
                    {
                        Response.Write("<script>window.alert('" + Resources.Resource.IdOrAccountError + "!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();

            MySqlCommand cmd = new MySqlCommand(insertReaderSql, OLMSDBConnection);
            cmd.Parameters.AddWithValue("?name", name);
            cmd.Parameters.AddWithValue("?password", password);
            cmd.Parameters.AddWithValue("?idNumber", idNumber);
            cmd.Parameters.AddWithValue("?phone", telephone);
            cmd.Parameters.AddWithValue("?email", email);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write("<script>alert('" + Resources.Resource.Successful + "');</script>");
                //Response.Redirect()
            }
            else
            {
                Response.Write("<script>alert('" + Resources.Resource.Failure + "');</script>");
                //Response.Redirect()
            }

        }
        catch(MySqlException ex)
        {
            //Console.WriteLine(ex.Message);
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            OLMSDBConnection.Close();
        }
        
    }

    protected void Cancel(object sender, EventArgs e)
    {
        //Response.Redirect("http://localhost:52800/Pages/ShowReaderInfo.aspx?id=111");
        return;
    }

}