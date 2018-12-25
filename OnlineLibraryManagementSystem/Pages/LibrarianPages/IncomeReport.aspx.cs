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

public partial class Pages_LibrarianPages_IncomeReport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getDeposit_sql = new MySqlCommand("select ReaderId,Timestamp from Readers");
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
        dtResult.Columns["Timestamp"].ColumnName = "Time";
        dtResult.Columns.Add("Amount");
        dtResult.Columns.Add("Type");

        Double totalDeposit = 0;
        Double totalFine = 0;
        Double total = 0;

        foreach (DataRow dr in dtResult.Rows)
        {
            double Deposit = Convert.ToDouble(ConfigurationManager.AppSettings["Deposit"].ToString());
            totalDeposit += Deposit;
            if (Session["PreferredCulture"].ToString() == "zh-CN")
            {
                dr["Amount"] = Deposit.ToString() + "元";
                dr["Type"] = "押金";
            }
            else
            {
                dr["Amount"] = Deposit.ToString() + " Yuan";
                dr["Type"] = "Deposit";
            }
        }
        MySqlCommand getFine_sql = new MySqlCommand("select ReaderId,ReturnTime,Fine from IssueRecords where Fine is not null and Status != 3");
        var result2Adapter = new MySqlDataAdapter();
        result2Adapter.SelectCommand = getFine_sql;
        result2Adapter.SelectCommand.Connection = OLMSDBConnection;
        var result2Set = new DataSet();

        OLMSDBConnection.Open();
        result2Adapter.Fill(result2Set);
        OLMSDBConnection.Close();
        DataTable search2Result = result2Set.Tables[0];

        foreach (DataRow dr in search2Result.Rows)
        {
            DataRow newdr = dtResult.NewRow();
            newdr["ReaderId"] = dr["ReaderId"];
            newdr["Time"] = dr["ReturnTime"];
            totalFine += Convert.ToDouble(dr["Fine"].ToString());
            if (Session["PreferredCulture"].ToString() == "zh-CN")
            {
                newdr["Amount"] = dr["Fine"].ToString() + "元";
                newdr["Type"] = "罚款";
            }
            else
            {
                newdr["Amount"] = dr["Fine"].ToString() + " Yuan";
                newdr["Type"] = "Fine";
            }
            dtResult.Rows.Add(newdr);
        }
        ViewState["normal"] = dtResult;
        Income.DataSource = dtResult;
        Income.DataBind();
        Income.HeaderRow.TableSection = TableRowSection.TableHeader;

        total = totalDeposit + totalFine;
        Total_Deposit_Text.Text = totalDeposit.ToString();
        Total_Fine_Text.Text = totalFine.ToString();
        Total_Text.Text = total.ToString();
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

        Double totalDeposit = 0;
        Double totalFine = 0;
        Double total = 0;

        foreach (DataRow dr in dtResult.Rows)
        {
            DateTime selectTime = (DateTime)dr["time"];
            System.Diagnostics.Debug.WriteLine(selectTime);

            if (DateTime.Compare(startDate, selectTime) < 0 && DateTime.Compare(endDate, selectTime) > 0)
            {
                if (dr["Type"].ToString() == "罚款")
                {
                    String Money = Regex.Replace(dr["Amount"].ToString(),"元","");
                    totalFine += Convert.ToDouble(Money);
                }
                if (dr["Type"].ToString() == "Fine")
                {
                    String Money = Regex.Replace(dr["Amount"].ToString(), " Yuan", "");
                    totalFine += Convert.ToDouble(Money);
                }
                if (dr["Type"].ToString() == "押金")
                {
                    String Money = Regex.Replace(dr["Amount"].ToString(), "元", "");
                    totalDeposit += Convert.ToDouble(Money);
                }
                if (dr["Type"].ToString() == "Deposit")
                {
                    String Money = Regex.Replace(dr["Amount"].ToString(), " Yuan", "");
                    totalDeposit += Convert.ToDouble(Money);
                }
                rangeResult.ImportRow(dr);
                System.Diagnostics.Debug.WriteLine("push");
            }
        }
        if (rangeResult.Rows.Count == 0)
        {
            DataRow blankRow = rangeResult.NewRow();
            rangeResult.Rows.Add(blankRow);
        }
        Income.DataSource = rangeResult;
        Income.DataBind();
        Income.HeaderRow.TableSection = TableRowSection.TableHeader;
        total = totalDeposit + totalFine;
        Total_Deposit_Text.Text = totalDeposit.ToString();
        Total_Fine_Text.Text = totalFine.ToString();
        Total_Text.Text = total.ToString();
    }

    protected void reset_Click(object sender, EventArgs e)
    {
        DataTable dtResult = ViewState["normal"] as DataTable;
        Income.DataSource = dtResult;
        Income.DataBind();
        Income.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}