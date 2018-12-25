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
using System.Data;

public partial class Pages_LibrarianPages_EditStack : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //数据库
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

            OLMSDBConnection.Open();
            //搜索建库时间，建库时间不可变
            string selecttimestamp = "select * from Stacks where StackId='"+Session["STACKID"] +"';";
            MySqlCommand cmdselecttimestamp = new MySqlCommand(selecttimestamp, OLMSDBConnection);
            MySqlDataReader reader = cmdselecttimestamp.ExecuteReader();
            if (reader.Read())
            {
                TextBoxStackId.Text = reader["StackId"].ToString();
                TextBoxPosition.Text = reader["Position"].ToString();
                TextBoxSummary.Text = reader["Summary"].ToString();
                LabelStack_Timestamp.Text = reader["Timestamp"].ToString();
            }
            reader.Close();
            OLMSDBConnection.Close();
            GridviewBind();
        }
    }

    protected void Alter_StackInfo(object sender, EventArgs e)
    {
        string newstackid = "";
        string newsummary = "";
        string newposition = "";
        String pattern = "^[A-Z]-\\d{3}$";
        if (TextBoxStackId.Text == ""||TextBoxStackId.Text.Trim().Length==0)
        {
            Response.Write("<script>alert('StackId Is Null!')</script>");
            return;
        }
        if (TextBoxStackId.Text.Trim().Length==5&&System.Text.RegularExpressions.Regex.IsMatch(TextBoxStackId.Text.Trim(), pattern))
        {
            newstackid = TextBoxStackId.Text.Trim();
        }
        else
        {
            Response.Write("<script>alert('Error StackId!\\nStackId Example:A-101')</script>");
            return;
        }
        if (TextBoxPosition.Text == ""||TextBoxPosition.Text.Trim().Length==0)
        {
            Response.Write("<script>alert('Stack Position Is Null')</script>");
            return;
        }
        else
        {
            newposition = TextBoxPosition.Text.Replace(" ","");
        }
        if (TextBoxSummary.Text == "" || TextBoxSummary.Text.Trim().Length == 0)
        {
            Response.Write("<script>alert('Stack_Summary Is Null')</script>");
            return;
        }
        else
        {
            newsummary = TextBoxSummary.Text.Trim();
        }
        //数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //string updatestack = "update Stacks set StackId='" + newstackid + "',Position='" + newposition + "',Summary='" + newsummary + "' where StackId='" + Session["STACKID"] + "';";
       // string selectnewstackid = "select count(*) as num from Stacks where StackId='" + newstackid + "';";
       try
        {
            OLMSDBConnection.Open();
            if (newstackid != Session["STACKID"].ToString())
            {
                MySqlCommand cmdselectstackid = OLMSDBConnection.CreateCommand();
                cmdselectstackid.CommandText = "select count(*) as num from Stacks where StackId=@s";
                MySqlParameter selectparaters;
                selectparaters = new MySqlParameter("@s", newstackid);
                cmdselectstackid.Parameters.Add(selectparaters);
                MySqlDataReader readerselect = cmdselectstackid.ExecuteReader();
                while (readerselect.Read())
                {
                    if (readerselect.HasRows)
                    {
                        Int64 count = (Int64)readerselect["num"];
                        if (count > 0)
                        {
                            Response.Write("<script>window.alert('StackId Is Exist!');</script>");
                            return;
                        }
                        break;
                    }
                }
                readerselect.Close();
            }
            MySqlCommand cmdupdatestack = OLMSDBConnection.CreateCommand();
            cmdupdatestack.CommandText = "update Stacks set StackId=@stackid,Position=@position,Summary=@summary where Stackid=@old;";
            MySqlParameter updateparater;
            updateparater = new MySqlParameter("@stackid", newstackid);
            cmdupdatestack.Parameters.Add(updateparater);
            updateparater = new MySqlParameter("@position", newposition);
            cmdupdatestack.Parameters.Add(updateparater);
            updateparater = new MySqlParameter("@summary", newsummary);
            cmdupdatestack.Parameters.Add(updateparater);
            updateparater = new MySqlParameter("@old", Session["STACKID"]);
            cmdupdatestack.Parameters.Add(updateparater);
            int result = 0;
            result = cmdupdatestack.ExecuteNonQuery();
            if (result != 0)
            {
                Session["STACKID"] = newstackid;
                Response.Write("<script>alert('Edited Successfully!');window.location.href = 'StackInfo.aspx';</script>");
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
    protected void GridviewBind()
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        string stackid = Session["STACKID"].ToString();
        MySqlCommand get_sql = new MySqlCommand("select ShelfId,Summary,Timestamp from Shelves where StackId='" + stackid + "';");
        var resultAdapter = new MySqlDataAdapter();
        resultAdapter.SelectCommand = get_sql;
        resultAdapter.SelectCommand.Connection = OLMSDBConnection;
        var resultSet = new DataSet();

        OLMSDBConnection.Open();
        resultAdapter.Fill(resultSet);
        OLMSDBConnection.Close();

        DataTable searchResult = resultSet.Tables[0];

        Shelves.DataSource = searchResult;
        Shelves.DataKeyNames = new string[] { "ShelfId" };
        Shelves.DataBind();
        if (Shelves.HeaderRow != null)
        {
            Shelves.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        }
    protected void Shelves_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Shelves.EditIndex = e.NewEditIndex;
        GridviewBind();
    }
    protected void Shelves_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ShelfId = int.Parse(Shelves.DataKeys[e.RowIndex].Values[0].ToString());
        string summary = ((TextBox)Shelves.Rows[e.RowIndex].FindControl("txtSummary")).Text;
        if (summary.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('Name can not be none!');", true);
            GridviewBind();
            return;
        }
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update Shelves set Summary=@s where ShelfId=@i";
            MySqlParameter param;
            param = new MySqlParameter("@s", summary);
            cmd.Parameters.Add(param);
            param = new MySqlParameter("@i", ShelfId);
            cmd.Parameters.Add(param);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditSuccess + "');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditFail + "');", true);
            }
            Shelves.EditIndex = -1;
            GridviewBind();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            GridviewBind();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void Shelves_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Shelves.EditIndex = -1;
        GridviewBind();
    }
    protected void Shelves_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Shelves.PageIndex = e.NewPageIndex;
        GridviewBind();
    }
    protected void Cancel(object sender, EventArgs e)
    {
        Response.Redirect("StackInfo.aspx");
    }
}