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

public partial class Pages_LibrarianPages_EditShelf : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //DropDownList1.Items.Clear();
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            string stackid = Session["shelf_stack"].ToString();
            DropDownList1.Items.Add(stackid);
            try
            {
                OLMSDBConnection.Open();
                string select_other_stackid = "select StackId from Stacks where StackId<>'"+stackid+"';";
                MySqlCommand cmdselect_other_stackid = new MySqlCommand(select_other_stackid, OLMSDBConnection);
                MySqlDataReader readerstack = cmdselect_other_stackid.ExecuteReader();
                while (readerstack.Read())
                {
                    DropDownList1.Items.Add(readerstack["StackId"].ToString());
                }
                //DropDownList1.DataSource = readerstack;
                //DropDownList1.DataTextField = "StackId";
                //DropDownList1.DataBind();
                readerstack.Close();
                string selectshelf = "select * from Shelves where ShelfId='" + Session["SHELFID"] + "';";
                MySqlCommand cmdselectshelf = new MySqlCommand(selectshelf, OLMSDBConnection);
                MySqlDataReader readershlef = cmdselectshelf.ExecuteReader();
                if (readershlef.Read())
                {
                    LabelShelfId.Text = readershlef["ShelfId"].ToString();//书架ID不可修改
                    TextBoxSummary.Text = readershlef["Summary"].ToString();
                    LabelShelf_Timestamp.Text = readershlef["Timestamp"].ToString();//建架时间不可以改变
                }
                readershlef.Close();
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

    protected void Alter_ShelfInfo(object sender, EventArgs e)
    {
        string newsummary = "";
        string newstackid = DropDownList1.SelectedItem.Text;
        //string pattern = "^\\d{1,10}$";
        if (TextBoxSummary.Text == "" ||TextBoxSummary.Text.Trim().Length==0)
        {
            Response.Write("<script>alert('Shelf_Summary Is Null!')</script>");
            return;
        }
        else
        {
            newsummary = TextBoxSummary.Text.Trim();
        }
        //数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //string selectshelfid = "select count(*) as num from Shelves where ShelfId='" + newshelfid + "';";
        //string updateshelf = "update Shelves set StackId='" + newstackid + "',Summary='" + newsummary + "' where ShelfId='" + Session["SHELFID"] + "';";
        try
        {
            OLMSDBConnection.Open();
            /* if (newshelfid != Session["ID"].ToString())
             {
                 MySqlCommand cmdselectshelfid = new MySqlCommand(selectshelfid, OLMSDBConnection);
                 MySqlDataReader readerselect = cmdselectshelfid.ExecuteReader();
                 while (readerselect.Read())
                 {
                     if (readerselect.HasRows)
                     {
                         Int64 count = (Int64)readerselect["num"];
                         if (count > 0)
                         {
                             Response.Write("<script>window.alert('ShelfId Is Exist!');</script>");
                             return;
                         }
                         break;
                     }
                 }
                 readerselect.Close();
             }*/
            MySqlCommand cmdupdateshelf = OLMSDBConnection.CreateCommand();
            cmdupdateshelf.CommandText = "update Shelves set StackId=@stack,Summary=@summary where ShelfId=@id";
            MySqlParameter updateparater;
            updateparater = new MySqlParameter("@stack", newstackid);
            cmdupdateshelf.Parameters.Add(updateparater);
            updateparater = new MySqlParameter("@summary", newsummary);
            cmdupdateshelf.Parameters.Add(updateparater);
            updateparater = new MySqlParameter("@id", Session["SHELFID"]);
            cmdupdateshelf.Parameters.Add(updateparater);
            int resultupdate = 0;
            resultupdate = cmdupdateshelf.ExecuteNonQuery();
            if (resultupdate != 0)
            {
                Response.Write("<script>alert('Edited Successfully!');window.location.href = 'ShelfInfo.aspx';</script>");
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

    protected void Cancel(object sender, EventArgs e)
    {
        Response.Redirect("ShelfInfo.aspx");
    }
}