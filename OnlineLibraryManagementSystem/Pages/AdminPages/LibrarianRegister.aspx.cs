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

public partial class Pages_OtherPages_LibrarianRegister :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RegisterReader(object sender, EventArgs e)
    {
        string name, Account, password, idNumber, telephone, ppassword;
        if (TextBoxName.Text == "")
        {
            Response.Write("<script>window.alert('User name can not be empty!');</script>");
            return;
        }
        else
        {
            name = TextBoxName.Text;
        }
        if (TextBoxAccount.Text == "")
        {
            Response.Write("<script>window.alert('Account can not be empty!');</script>");
            return;
        }
        else
        {
            Account = TextBoxAccount.Text;
            //这里应该有正则匹配去除非法输入防止篡改数据库
        }

        ppassword = TextBoxPassword.Text;
        //检测Librarian账号不能与Admin账号相同
        if (string.Equals(Account, ConfigurationManager.AppSettings["AdminAccount"].ToString()))
        {
            Response.Write("<script>window.alert('" + Resources.Resource.LibrarianLogined + "');</script>");
            return;
        }
        //链接数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //检测同账号是否注册过
        string selectLibarianSql = "select count(*) as num from Librarians where Account = ?Account;";
        string insertLibrarianSql = "INSERT INTO Librarians(Account, Password, Name) " +
            "VALUES(?Account, ?Password, ?Name);";
        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmd2 = new MySqlCommand(selectLibarianSql, OLMSDBConnection);
            cmd2.Parameters.AddWithValue("?Account", Account);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if (count > 0)
                    {
                        Response.Write("<script>window.alert('The account already exists!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();

            MySqlCommand cmd = new MySqlCommand(insertLibrarianSql, OLMSDBConnection);
            cmd.Parameters.AddWithValue("?Account", Account);
            cmd.Parameters.AddWithValue("?name", name);
            cmd.Parameters.AddWithValue("?password", ppassword);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write("<script>window.alert('Registration is successful!');</script>");
                //Response.Redirect()
                return;
            }
            else
            {
                Response.Write("<script>window.alert('Failed');</script>");
                //Response.Redirect()
                return;
            }

        }
        catch (MySqlException ex)
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
        //返回上一个页面  
        //Response.Redirect("http://localhost:52800/Pages/ShowReaderInfo.aspx?id=111");
        return;
    }
}