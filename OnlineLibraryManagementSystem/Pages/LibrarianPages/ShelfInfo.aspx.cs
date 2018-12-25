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

public partial class Pages_ShelfInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string shelfid = "";
        if (Request["ShelfId"] != null)
        {
            shelfid = Request["ShelfId"];
            Session["SHELFID"] = Request["ShelfId"];
        }
        else
        {
            shelfid = Session["SHELFID"].ToString();
        }
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        string select = "select * from Shelves where ShelfId='" + shelfid + "';";
        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmdselectstack = new MySqlCommand(select, OLMSDBConnection);
            MySqlDataReader reader = cmdselectstack.ExecuteReader();
            if (reader.Read())
            {
                LabelShelfId.Text = reader["ShelfId"].ToString();
                LabelStackId.Text = reader["StackId"].ToString();
                LabelShelf_Summary.Text= reader["Summary"].ToString();
                //labelShelf_Timestamp.Text = reader["Timestamp"].ToString();
                LabelShelf_Timestamp.Text = reader["Timestamp"].ToString();
            }
            reader.Close();
            Session["shelf_stack"] = LabelStackId.Text;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            OLMSDBConnection.Close();
        }
    }

    protected void Cancel(object sender, EventArgs e)
    {
        Response.Redirect("Search_Stacks_Shelves.aspx");
    }

    protected void Edit_ShelfInfo(object sender, EventArgs e)
    {
        Response.Redirect("EditShelf.aspx");
    }
}