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

public partial class Pages_LibrarianPages_IssueReport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getIssue_sql = new MySqlCommand("select ReaderId,IssueRecords.BookBarcode,IssueTime,ReturnTime,Title,IssueRecords.Status from IssueRecords,BookBarcodes,Books where IssueRecords.BookBarcode=BookBarcodes.BookBarcode and BookBarcodes.BookId=Books.BookId;");
        var result1Adapter = new MySqlDataAdapter();
        result1Adapter.SelectCommand = getIssue_sql;
        result1Adapter.SelectCommand.Connection = OLMSDBConnection;
        var result1Set = new DataSet();

        OLMSDBConnection.Open();
        result1Adapter.Fill(result1Set);
        OLMSDBConnection.Close();
        DataTable dtResult = result1Set.Tables[0];

        dtResult.Columns[1].ColumnName = "Barcode";
        dtResult.Columns[5].ColumnName = "Status";
        dtResult.Columns.Add("Details");

        foreach (DataRow dr in dtResult.Rows)
        {
            string status = dr["Status"].ToString();
            if (Session["PreferredCulture"].ToString() == "zh-CN")
            {
                if (status == "3")
                    dr["Details"] = "逾期未归还";
                if (status == "1")
                    dr["Details"] = "已归还";
                if (status == "2")
                    dr["Details"] = "已损坏或丢失";
                if (status == "0")
                    dr["Details"] = "未归还";
            }
            else
            {
                if (status == "0")
                    dr["Details"] = "Not Returned";
                if (status == "1")
                    dr["Details"] = "Returned";
                if (status == "2")
                    dr["Details"] = "Damaged or Lost";
                if (status == "3")
                    dr["Details"] = "Overdue not returned";
            }
        }

        ViewState["normal"] = dtResult;
        Issue.DataSource = dtResult;
        Issue.DataBind();
        Issue.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        DateTime endDate = Convert.ToDateTime(end, dtFormat).AddDays(1);
        System.Diagnostics.Debug.WriteLine(startDate);
        System.Diagnostics.Debug.WriteLine(endDate);
        DataTable dtResult = ViewState["normal"] as DataTable;
        DataTable rangeResult = dtResult.Clone();

        foreach (DataRow dr in dtResult.Rows)
        {
            DateTime selectTime = (DateTime)dr["IssueTime"];
            System.Diagnostics.Debug.WriteLine(selectTime);

            if (DateTime.Compare(startDate, selectTime) < 0 && DateTime.Compare(endDate, selectTime) > 0)
            {
                rangeResult.ImportRow(dr);
                System.Diagnostics.Debug.WriteLine("push");
            }
        }
        if (rangeResult.Rows.Count == 0)
        {
            DataRow blankRow = rangeResult.NewRow();
            rangeResult.Rows.Add(blankRow);
        }
        Issue.DataSource = rangeResult;
        Issue.DataBind();
        Issue.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void reset_Click(object sender, EventArgs e)
    {
        DataTable dtResult = ViewState["normal"] as DataTable;
        Issue.DataSource = dtResult;
        Issue.DataBind();
        Issue.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}