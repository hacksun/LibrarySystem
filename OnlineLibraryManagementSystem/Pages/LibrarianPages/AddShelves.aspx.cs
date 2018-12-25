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

public partial class Pages_AddShelves : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //DropDownList1.Items.Clear();
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

            OLMSDBConnection.Open();
            string select = "select StackId from Stacks";
            MySqlCommand cmdselectstackid = new MySqlCommand(select, OLMSDBConnection);
            MySqlDataReader reader = cmdselectstackid.ExecuteReader();
            DropDownList1.DataSource = reader;
            DropDownList1.DataTextField = "StackId";
            DropDownList1.DataBind();
            reader.Close();
            OLMSDBConnection.Close();
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }

    protected void AddShelves(object sender, EventArgs e)
    {
        string stackid = "";
        string shelf_summary = "";
        string nowdate = "";
        string shelfid = "";
        stackid = DropDownList1.SelectedItem.Text;
        if (TextBoxShelf_Summary.Text == "" || TextBoxShelf_Summary.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script language='javascript'>alert('Shelf_Summary Is Null!');</script>");
            //Response.Write("<script>alert('Shelf_Summary is null!')</script>");
            return;
        }
        else
        {
            shelf_summary = TextBoxShelf_Summary.Text.Trim();
        }
        //数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //检查同ID书库是否存在
        string selectStack = "select count(*) as num2 from Stacks where StackId='" + stackid + "';";
        //创建书架
       // string insertShelve = "insert into Shelves(StackId,Summary) " + "values('"  + stackid + "','" + shelf_summary  + "');";
        string selectShelveid = "select max(ShelfId) from Shelves";
        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmdselectstack = new MySqlCommand(selectStack, OLMSDBConnection);
            MySqlDataReader reader1 = cmdselectstack.ExecuteReader();
            while (reader1.Read())
            {
                if (reader1.HasRows)
                {
                    Int64 count = (Int64)reader1["num2"];
                    if (count==0)
                    {
                        Response.Write("<script>window.alert('Stack Does Not Exist!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader1.Close();
            MySqlCommand cmdinsert = OLMSDBConnection.CreateCommand();
            cmdinsert.CommandText= "insert into Shelves(StackId,Summary) values(@stackid,@summary);";
            MySqlParameter myparaters;
            myparaters = new MySqlParameter("@stackid", stackid);
            cmdinsert.Parameters.Add(myparaters);
            myparaters = new MySqlParameter("@summary", shelf_summary);
            cmdinsert.Parameters.Add(myparaters);
            int result = 0;
            result = cmdinsert.ExecuteNonQuery();
            MySqlCommand cmdselect = new MySqlCommand(selectShelveid, OLMSDBConnection);
            MySqlDataReader reader2 = cmdselect.ExecuteReader();
            if (reader2.Read())
            {
                shelfid = reader2["max(ShelfId)"].ToString();
            }
            if (result != 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script language='javascript'>alert('Created Successfully!ShelfId:" + shelfid + "');</script>");
                //ClientScript.RegisterStartupScript(GetType(), "", "window.alert('Created Successfully!ShelfId:" + shelfid + "');", true);
                //Response.Write("<script>alert('创建书架成功,书架id为:"+shelfid+"')</script>");
                return;
            }
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
}