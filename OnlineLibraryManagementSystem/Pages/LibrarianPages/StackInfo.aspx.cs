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

public partial class Pages_StackInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string stackid = "";
            if (Request["StackId"] != null)
            {
                stackid = Request["StackId"];
                Session["STACKID"] = Request["StackId"];
            }
            else
            {
                stackid = Session["STACKID"].ToString();
            }
            //数据库
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            string select = "select * from Stacks where StackId='" + stackid + "';";
            try
            {
                OLMSDBConnection.Open();
                MySqlCommand cmdselectstack = new MySqlCommand(select, OLMSDBConnection);
                MySqlDataReader reader = cmdselectstack.ExecuteReader();
                if (reader.Read())
                {
                    //LabelStackId.Text = reader["StackId"].ToString();
                    //LabelPosition.Text = reader["Position"].ToString();
                    //LabelSummary.Text = reader["Summary"].ToString();
                    //LabelStack_Timestamp.Text = reader["Timestamp"].ToString();
                    LabelStackId.Text= reader["StackId"].ToString();
                    LabelPosition.Text = reader["Position"].ToString();
                    LabelSummary.Text = reader["Summary"].ToString();
                    LabelStack_Timestamp.Text = reader["Timestamp"].ToString();
                }
                reader.Close();
                GridviewBind();
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

    public void GridviewBind()
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        string stackid = "";
        if (Request["StackId"] != null)
        {
            stackid = Request["StackId"];
            Session["STACKID"] = Request["StackId"];
        }
        else
        {
            stackid = Session["STACKID"].ToString();
        }
        MySqlCommand get_sql = new MySqlCommand("select ShelfId,Summary,Timestamp from Shelves where StackId='"+stackid+"';");
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
        if (Shelves.HeaderRow!= null)
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
        Response.Redirect("Search_Stacks_Shelves.aspx");
    }


    protected void Edit_StackInfo(object sender, EventArgs e)
    {
        Response.Redirect("EditStack.aspx");
    }
}