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

public partial class Pages_ReaderPages_ViewNotice : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["id"]))
            {
                //未登录时提示登录
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../ReaderLogin.aspx';</script>");
            }
            bind();
        }
    }

    protected void search_Click(object sender, EventArgs e)
    {
        String Da = Request["date"].ToString();
        String[] range = Regex.Split(Da, " – ", RegexOptions.IgnoreCase);
        String start = range[0];
        String end = range[1];
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
}