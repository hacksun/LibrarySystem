using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_AddNotice : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Add_Click(object sender, EventArgs e)
    {
        if (!rfvDetails.IsValid || !rfvTitle.IsValid) 
        {
            return;
        }
        string details = txtdetails.Text;
        string title = txttitle.Text;

        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter param;
        cmd.CommandText = "insert into Notices(Details,Title) values(@d,@t);";
        param = new MySqlParameter("@d", details);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@t", title);
        cmd.Parameters.Add(param);
        int result = cmd.ExecuteNonQuery();
        if (result == 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Successful + "');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Failure + "');", true);
        }
    }
}