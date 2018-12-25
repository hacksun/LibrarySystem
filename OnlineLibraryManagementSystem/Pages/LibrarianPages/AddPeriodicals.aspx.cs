using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

public partial class Pages_LibrarianPages_AddPeriodicals : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            InitDDLShelf();
        }
    }

    protected void InitDDLShelf()
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        string getShelves_sql = "select " +
                                "ShelfId,StackId " +
                                "from " +
                                "Shelves;";
        MySqlCommand getShelves_cmd = new MySqlCommand(getShelves_sql, OLMSDBConnection);

        OLMSDBConnection.Open();
        MySqlDataReader reader = getShelves_cmd.ExecuteReader();
        while (reader.Read())
        {
            ddlShelf.Items.Add(new ListItem
            {
                Text = reader["ShelfId"].ToString() + "," + reader["StackId"].ToString(),
                Value = reader["ShelfId"].ToString()
            });
        }
        OLMSDBConnection.Close();
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        
        string title = "";
        string country = "";
        string issn = "";
        string price = "";
        string integerparttern = "^[1-9]\\d*$";
        string issnparttern = "^[0-9A-Z]{8}$";
        if (tbTitle.Text.Trim() != "")
        {
            title = tbTitle.Text.Trim();
        }
        else
        {
            Response.Write("<script>alert('Title Is Null!')</script>");
            return;
        }
        if (tbCountry.Text.Trim() != "")
        {
            country = tbCountry.Text.Trim();
        }
        else
        {
            Response.Write("<script>alert('Country Is Null!')</script>");
            return;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(tbISSN.Text.Trim(), issnparttern))
        {
            issn = tbISSN.Text.Trim();
        }
        else
        {
            Response.Write("<script>alert('Error ISSN Format!')</script>");
            return;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(tbPrice.Text, integerparttern))
        {
            price = tbPrice.Text;
        }
        else
        {
            Response.Write("<script>alert('Price Must Bigger Than Zero!')</script>");
            return;
        }
        string type = ddlType.SelectedValue.ToString();
        string shelf = ddlShelf.SelectedValue.ToString();
        string imageURL = imCover.ImageUrl;

        string selectissn = "select count(*) as num from Periodicals where ISSN='" + issn + "';";
        string submit_sql = "insert into " +
                            "Periodicals(ISSN,ImageURL,Title,Country,Type,ShelfId,Price) " +
                            "value(@ISSN,@ImageURL,@Title,@Country,@Type,@ShelfId,@Price);";
        try
        { 
            OLMSDBConnection.Open();
            MySqlCommand cmdselect = new MySqlCommand(selectissn, OLMSDBConnection);
            MySqlDataReader reader = cmdselect.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if (count > 0)
                    {
                        Response.Write("<script>alert('Periodical Is Exist!')</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();
            var submit_cmd = new MySqlCommand(submit_sql, OLMSDBConnection);
            submit_cmd.Parameters.AddWithValue("@ISSN", issn);
            submit_cmd.Parameters.AddWithValue("@ImageURL", imageURL);
            submit_cmd.Parameters.AddWithValue("@Title", title);
            submit_cmd.Parameters.AddWithValue("@Country", country);
            submit_cmd.Parameters.AddWithValue("@Type", type);
            submit_cmd.Parameters.AddWithValue("@ShelfId", shelf);
            submit_cmd.Parameters.AddWithValue("@Price", price);
                
            if (submit_cmd.ExecuteNonQuery() == 1)
            {
                Response.Write("<script>alert('Add Periodical Successfully!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Add Periodical Failure!')</script>");
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

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        bool filesValid = false;
        HttpPostedFile req = Request.Files["fileupload"];
        if (req == null || req.ContentLength < 0)
        {

            Response.Write("<script>alert('Not Found!')</script>");
        }
        else
        {

            string fileExtension = System.IO.Path.GetExtension(req.FileName.ToString()).ToLower();
            string[] restricyExtension = { ".gif", ".jpg", ".bmp", ".png" };
            string src = req.FileName;
            for (int i = 0; i < restricyExtension.Length; i++)
            {
                if (fileExtension == restricyExtension[i])
                {
                    filesValid = true;

                }

            }
            if (filesValid == true)
            {
                //判断是否有该路径  
                string wantPath = Server.MapPath("~/Images/Cover/");
                if (!Directory.Exists(wantPath))
                {   //如果不存在就创建
                    Directory.CreateDirectory(wantPath);
                    req.SaveAs(wantPath + src);
                    imCover.ImageUrl = "~/Images/Cover/" + src;
                }
                else
                {
                    req.SaveAs(wantPath + src);
                    imCover.ImageUrl = "~/Images/Cover/" + src;

                }

            }
            else
            {
                Response.Write("<script>alert('Error Format!')</script>");
                return;
            }

        }
    }
 }