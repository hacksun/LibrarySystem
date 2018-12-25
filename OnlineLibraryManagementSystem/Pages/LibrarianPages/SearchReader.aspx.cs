using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Pages_SearchReader : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)Session["delete"]))
        {
            if (Session["delete"].ToString() == "true")
            {
                Response.Write("<script>window.alert(' The removal completed successfully!');</script>");
            }
               
            Session["delete"] = "false";
        }
     
        
    }

    protected void Search(object sender, EventArgs e)
    {
        string search;
        search = TextSearch.Text;

        //链接数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        System.Diagnostics.Debug.WriteLine("database is ok");
        String readersql = "" ;
        if (DropDownList1.SelectedValue == "0")
        {
            readersql = "select * from Readers where Phone like " + "\"%" + search + "%\""
                                           + " or " + "Name like " + "\"%" + search + "%\"";
            
        }
        if (DropDownList1.SelectedValue == "1")
        {
            readersql = "select * from Readers where Phone like " + "\"%" + search + "%\"";
        }
        if (DropDownList1.SelectedValue == "2")
        {
            readersql = "select * from Readers where Name like " + "\"%" + search + "%\"";
        }


        try
        {
            OLMSDBConnection.Open();

            MySqlCommand cmd1 = new MySqlCommand(readersql, OLMSDBConnection);
            ArrayList readers_list = new ArrayList();
            MySqlDataReader sqlreader = cmd1.ExecuteReader();
            while (sqlreader.Read())
            {
                if (sqlreader.HasRows)
                {
                    Readers reader = new Readers();
                    reader.ReaderId= sqlreader["ReaderId"].ToString();
                    reader.Name = (string)sqlreader["Name"];
                    reader.Phone = (string)sqlreader["Phone"];
                    if (sqlreader["Email"] == null) { reader.Email = ""; }
                    else { reader.Email = sqlreader["Email"].ToString(); }
                    readers_list.Add(reader);
                    reader.href = "/Pages/LibrarianPages/ShowReaderInfoByLib.aspx?ReaderId=" + sqlreader["ReaderId"].ToString();
                }
            }
            GridView1.DataSource = readers_list;
            GridView1.DataBind();
            sqlreader.Close();
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

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void TextSearch_TextChanged(object sender, EventArgs e)
    {

    }

    public class Readers
    {
        public string ReaderId { get; set; }
        public string Name { get; set; }
        public string href { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}