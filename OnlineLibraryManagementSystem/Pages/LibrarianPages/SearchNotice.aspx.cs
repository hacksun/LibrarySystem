using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_SearchNotice : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {
            ViewState["start"] = null;
            ViewState["end"] = null;
            bind();
        }
        


    }
    public void bind()
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getDeposit_sql = new MySqlCommand("select * from Notices");
        var result1Adapter = new MySqlDataAdapter();
        result1Adapter.SelectCommand = getDeposit_sql;
        result1Adapter.SelectCommand.Connection = OLMSDBConnection;
        var result1Set = new DataSet();
        OLMSDBConnection.Open();
        result1Adapter.Fill(result1Set);
        OLMSDBConnection.Close();
        DataTable search1Result = result1Set.Tables[0];

        DataTable dtResult = new DataTable();
        dtResult = search1Result.Copy();
        ViewState["normal"] = dtResult;

        if (dtResult.Rows.Count == 0)
        {

            DataRow blankRow = dtResult.NewRow();
            dtResult.Rows.Add(blankRow);


            History.DataSource = dtResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;
            History.Rows[0].Visible = false;
        }
        else
        {

            History.DataSource = dtResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }
    public void search_Bind()
    {
        if (ViewState["start"] == null)
        {
            String Da = Request["date"].ToString();
            String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
            ViewState["start"] = range[0];
            ViewState["end"] = range[1];
        }


        String start = ViewState["start"].ToString();
        String end = ViewState["end"].ToString();
        //Response.Write(start + "    " + end);


        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "MM/dd/yyyy";
        DateTime startDate = Convert.ToDateTime(start, dtFormat);
        DateTime endDate = Convert.ToDateTime(end, dtFormat);
        System.Diagnostics.Debug.WriteLine(startDate);
        System.Diagnostics.Debug.WriteLine(endDate);

        DataTable dtResult = ViewState["normal"] as DataTable;


        DataTable rangeResult = dtResult.Clone();

        foreach (DataRow dr in dtResult.Rows)
        {
            String temp = "1999-01-01";
            if (dr["Timestamp"].ToString() != "")
            {
                temp = Convert.ToDateTime(dr["Timestamp"]).ToString("yyyy-MM-dd");
            }
            DateTime selectTime = Convert.ToDateTime(temp, dtFormat);
            //DateTime selectTime = (DateTime)dr["Timestamp"];
            System.Diagnostics.Debug.WriteLine(selectTime);
            if (DateTime.Compare(startDate, selectTime) <= 0 && DateTime.Compare(endDate, selectTime) >= 0)
            {

                rangeResult.ImportRow(dr);
                System.Diagnostics.Debug.WriteLine("push");
            }
        }

        if (rangeResult.Rows.Count == 0)
        {
            DataRow blankRow = rangeResult.NewRow();
            rangeResult.Rows.Add(blankRow);

            History.DataSource = rangeResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;
            History.Rows[0].Visible = false;
        }
        else
        {
            History.DataSource = rangeResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }

    protected void search_Click(object sender, EventArgs e)
    {
        String Da = Request["date"].ToString();
        String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
        String start = range[0];
        String end = range[1];
        //Response.Write(start + "    " + end);
        ViewState["start"] = start;
        ViewState["end"] = end;
        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "MM/dd/yyyy";
        DateTime startDate = Convert.ToDateTime(start, dtFormat);
        DateTime endDate = Convert.ToDateTime(end, dtFormat);
        System.Diagnostics.Debug.WriteLine(startDate);
        System.Diagnostics.Debug.WriteLine(endDate);

        DataTable dtResult = ViewState["normal"] as DataTable;


        DataTable rangeResult = dtResult.Clone();

        foreach (DataRow dr in dtResult.Rows)
        {
             String temp = "1999-01-01";
             if (dr["Timestamp"].ToString() != "")
             {
                 temp = Convert.ToDateTime(dr["Timestamp"]).ToString("yyyy-MM-dd");
             }
            DateTime selectTime = Convert.ToDateTime(temp, dtFormat);
            //DateTime selectTime = (DateTime)dr["Timestamp"];
            System.Diagnostics.Debug.WriteLine(selectTime);
            if (DateTime.Compare(startDate, selectTime) <= 0 && DateTime.Compare(endDate, selectTime) >= 0)
            {

                rangeResult.ImportRow(dr);
                System.Diagnostics.Debug.WriteLine("push");
            }
        }

        if (rangeResult.Rows.Count == 0)
        {
            DataRow blankRow = rangeResult.NewRow();
            rangeResult.Rows.Add(blankRow);

            History.DataSource = rangeResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;
            History.Rows[0].Visible = false;
        }
        else
        {
            History.DataSource = rangeResult;
            History.DataBind();
            History.HeaderRow.TableSection = TableRowSection.TableHeader;
        }



    }

    protected void reset_Click(object sender, EventArgs e)
    {
        Response.AddHeader("Refresh", "0");
        String Da = Request["date"].ToString();
        String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
        String start = range[0];
        String end = range[1];
        ViewState["start"] = start;
        ViewState["end"] = end;
        bind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       string s= History.DataKeys[e.RowIndex].Value.ToString();

  
        string sqlstr = "delete from Notices where NoticeId=" + s + ";";
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        System.Diagnostics.Debug.WriteLine("database is ok");
        OLMSDBConnection.Open();
        MySqlCommand cmd = new MySqlCommand(sqlstr, OLMSDBConnection);
        int result = cmd.ExecuteNonQuery();
        if (result == 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditSuccess + "');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditFail + "');", true);
        }
        String Da = Request["date"].ToString();
        String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
        ViewState["start"] = range[0];
        ViewState["end"] = range[1];
        bind();
    }
   

    protected void History_RowEditing(object sender, GridViewEditEventArgs e)
    {
        History.EditIndex = e.NewEditIndex;
        search_Bind();

    }

    protected void History_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void History_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string NoticeId = History.DataKeys[e.RowIndex].Value.ToString();
        string name = ((TextBox)History.Rows[e.RowIndex].FindControl("txtName")).Text;
        string name1 = ((TextBox)History.Rows[e.RowIndex].FindControl("ttName")).Text;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "update Notices set Details=@n , Title=@t where NoticeId=@i";
        MySqlParameter param;
        param = new MySqlParameter("@n", name);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@i", NoticeId);
        cmd.Parameters.Add(param);
        param = new MySqlParameter("@t", name1);
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
        History.EditIndex = -1;
        String Da = Request["date"].ToString();
        String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
        ViewState["start"] = range[0];
        ViewState["end"] = range[1];
        bind();
    }

    protected void History_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        History.EditIndex = -1;
        search_Bind();
    }

    protected void History_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        History.PageIndex = e.NewPageIndex;
        search_Bind();
    }

}