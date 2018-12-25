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

public partial class Pages_SearchPN : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Search(object sender, EventArgs e)
    {
        string search;
        search = TextSearch.Text;

        //链接数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        System.Diagnostics.Debug.WriteLine("database is ok");
        //string booknamesql = exp + ";";
        string Periodicalsnamesql = "select * from Periodicals where Title like " + "\"%" + search + "%\"" 
                                           + " or " + "ISSN like " + "\"%" + search + "%\""
                                           + " or " + "Country like " + "\"%" + search + "%\"";

        System.Diagnostics.Debug.WriteLine(Periodicalsnamesql);

        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmd1 = new MySqlCommand(Periodicalsnamesql, OLMSDBConnection);
            ArrayList books_list = new ArrayList();
            MySqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Periodicals p = new Periodicals();
                    p.Title = (string)reader["Title"];
                    books_list.Add(p);
                    //book.href = "/Pages/bookMessage.aspx?book_id=" + reader["BookId"].ToString();
                }
            }
            Repeater1.DataSource = books_list;
            Repeater1.DataBind();
            reader.Close();
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
}
public class Periodicals
{
    public string Title { get; set; }
    //public string href { get; set; }
}

