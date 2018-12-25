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

public partial class Pages_LibrarianPages_ReaderReport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getReader_sql = new MySqlCommand("select ReaderId,sum(Fine) from IssueRecords where Status=3 group by ReaderId");
        var result1Adapter = new MySqlDataAdapter();
        result1Adapter.SelectCommand = getReader_sql;
        result1Adapter.SelectCommand.Connection = OLMSDBConnection;
        var result1Set = new DataSet();

        OLMSDBConnection.Open();
        result1Adapter.Fill(result1Set);
        OLMSDBConnection.Close();
        DataTable search1Result = result1Set.Tables[0];

        DataTable dtResult = new DataTable();
        dtResult = search1Result.Copy();
        dtResult.Columns.Add("Arrears");

        foreach (DataRow dr in dtResult.Rows)
        {
            if (Session["PreferredCulture"].ToString() == "zh-CN")
            {
                dr["Arrears"] = dr[1].ToString() + "元";
            }
            else
            {
                dr["Arrears"] = dr[1].ToString() + " Yuan";
            }
        }

        Reader.DataSource = dtResult;
        Reader.DataBind();
        Reader.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}