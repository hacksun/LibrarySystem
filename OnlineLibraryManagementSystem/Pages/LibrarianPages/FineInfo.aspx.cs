using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_FineInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getFine_sql = new MySqlCommand("select ReaderId,BookBarcode,IssueTime,Status,ReturnTime,OverdueLength,Fine from IssueRecords where Fine is not null");
        var resultAdapter = new MySqlDataAdapter();
        resultAdapter.SelectCommand = getFine_sql;
        resultAdapter.SelectCommand.Connection = OLMSDBConnection;
        var resultSet = new DataSet();

        OLMSDBConnection.Open();
        resultAdapter.Fill(resultSet);
        OLMSDBConnection.Close();

        DataTable searchResult = resultSet.Tables[0];
        searchResult.Columns.Add("newStatus");

        foreach (DataRow dr in searchResult.Rows)
        {
            string status = dr["Status"].ToString();
            if(Session["PreferredCulture"].ToString()=="zh-CN")
            {
                if (status == "3")
                    dr["newStatus"] = "未归还";
                if (status == "1")
                    dr["newStatus"] = "已归还";
                if (status == "2")
                    dr["newStatus"] = "已损坏或丢失";
            }
            else
            {
                if (status == "3")
                    dr["newStatus"] = "Not Returned";
                if (status == "1")
                    dr["newStatus"] = "Returned";
                if (status == "2")
                    dr["newStatus"] = "Damaged or Lost";
            }
        }
        FineOverdue.DataSource = searchResult;
        FineOverdue.DataBind();
        FineOverdue.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}