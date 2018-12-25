using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ReaderPages_ForgotPassword : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Verify_Click(object sender, EventArgs e)
    {
        if (!rfvId.IsValid || !rfvPhone.IsValid)
        {
            return;
        }
        string phone = txtPhone.Text;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select * from Readers where Phone=" + phone;
        object res = cmd.ExecuteScalar();
        if (res == null) 
        {
            Response.Write("<script>alert('" + Resources.Resource.PhoneNotRegister + "')</script>");
            return;
        }

        string idNumberText = TextBoxId.Text;
        string idNumber = null;
        string email = null;
        string password = null;
        if (idNumberText.Length != 18)
        {
            Response.Write("<script>alert('" + Resources.Resource.InvalidId + "')</script>");
            return;
        }
       
        cmd.CommandText = "select IdNumber,Email,Password from Readers WHERE Phone="+phone;
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader.HasRows)
            {
                idNumber = reader["IdNumber"].ToString();
                email = reader["Email"].ToString();
                password = reader["Password"].ToString();
                break;
            }
        }
        reader.Close();
        conn.Close();

        if (idNumber.Equals(idNumberText))
        {
            SendEmail.Send(email, "OnlineLibraryManagement Get Back Password",
                            "Dear User, your password is " + password);
            Response.Write("<script>alert('" + Resources.Resource.VerificationSuccess + Resources.Resource.PasswordSent + "')</script>");
        }
        else
        {
            Response.Write("<script>alert('" + Resources.Resource.VerificationFail + "')</script>");
        }
    }
}