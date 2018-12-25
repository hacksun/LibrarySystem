using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ShowReaderInfo : BasePage
{
    private static DateTime GetDateTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long time = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(time);
        return dtStart.Add(toNow);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["id"]))
            {
                //未登录时提示登录
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../ReaderLogin.aspx';</script>");
            }
            //注意这里改成了通过Session获取
            string id = (string)Session["id"];
            //string id = "111";
            if (id == null)
            {
                //exception-handler
                return;
            }
            //Database connection test
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            string selectReaderSql = "select * from Readers where ReaderId = ?id;";
            string selectBookSql = "select IssueTime, ReturnTime,Title, Fine " +
                "from  IssueRecords, BookBarcodes, Books " +
                "where  BookBarcodes.BookBarcode =   IssueRecords.BookBarcode and  BookBarcodes.BookId = Books.BookId " +
                "and IssueRecords.ReaderId = ?reader_id;";
            string selectRevervationSql = "select Books.Title, A.ReservingTime, A.ShelfId, C.StackId, A.BookBarcode from BookBarcodes as A, Books, Shelves  as C" +
                " where A.status = 2 and A.ReservingReaderId = ?readerid and Books.BookId = A.BookId and A.ShelfId = C.ShelfId;";
            int totalOverdueDays = 0;
            try
            {
                OLMSDBConnection.Open();
                MySqlCommand cmd = new MySqlCommand(selectReaderSql, OLMSDBConnection);
                cmd.Parameters.AddWithValue("?id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TextBoxEmail.Text = (string)reader["Email"];
                        TextBoxName.Text = (string)reader["Name"];
                        LabelName.Text = "welcome " + (string)reader["Name"] + "!";
                        TextBoxTelephone.Text = (string)reader["Phone"];
                        string idNumber = (string)reader["IdNumber"];
                        idNumber = "**************" + idNumber.Substring(idNumber.Length - 4);
                        TextBoxIDNumber.Text = idNumber;
                        break;
                    }
                }
                reader.Close();
                
                MySqlCommand cmd2 = new MySqlCommand(selectBookSql, OLMSDBConnection);
                cmd2.Parameters.AddWithValue("?reader_id", id);
                ArrayList issueRecords = new ArrayList();
                ArrayList history = new ArrayList();
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                int flag = 1;
                while (reader2.Read())
                {
                    if (reader2.HasRows)
                    {
                        Record r = new Record();
                        r.title = (string)reader2["Title"];
                        DateTime issueTime = (DateTime)reader2["IssueTime"];
                        DateTime returnTime;
                        TimeSpan d;

                        if (reader2["ReturnTime"] is System.DBNull)
                        {
                            r.returnTime = "";
                            //获取当前时间
                            flag = 0; //为归还
                            DateTime Now = DateTime.Now;
                            d = Now.Subtract(issueTime);
                        }
                        else
                        {
                            try
                            {
                                returnTime = (DateTime)reader2["ReturnTime"];
                                r.returnTime = returnTime.ToString();
                            }
                            catch (Exception ex)
                            {
                                returnTime = DateTime.Now;
                                flag = 0;
                                r.returnTime = "";
                            }
                            d = returnTime.Subtract(issueTime);
                        }
                        int delta = d.Days - 30;
                        if (delta < 0)
                        {
                            r.overdueTime = "0";
                        }
                        else
                        {
                            r.overdueTime = delta.ToString();
                            totalOverdueDays = totalOverdueDays + delta;
                        }
                        r.issueTime = issueTime.ToString();
                        if (reader2["Fine"] is System.DBNull)
                        {
                            r.fine = "";
                        }
                        else
                        {
                            r.fine = reader2["Fine"].ToString();
                        }
                        if (flag == 1)
                        {
                            history.Add(r);
                        }
                        else
                        {
                            issueRecords.Add(r);
                        }
                        

                    }
                }
                int finePerDay = int.Parse(ConfigurationManager.AppSettings.Get("OverdueFinePerDay"));
                double totalFine = totalOverdueDays * finePerDay;
                TextBoxFine.Text = totalFine.ToString();
                reader2.Close();
                MySqlCommand cmd3 = new MySqlCommand(selectRevervationSql, OLMSDBConnection);
                cmd3.Parameters.AddWithValue("?readerid", id);
                ArrayList reversationRecords = new ArrayList();
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string ReversationTime = "";
                try
                {
                     ReversationTime = ConfigurationManager.AppSettings.Get("OverdueReservationDuration");
                }
                catch
                {
                    ReversationTime = "";
                }
                double ReversationTime2 = double.Parse(ReversationTime);
                while (reader3.Read())
                {
                    if (reader3.HasRows)
                    {
                        Record2 r = new Record2();
                        r.barcode = (string)reader3["BookBarcode"];
                        r.shelf = reader3.GetString("ShelfId");
                        r.stack = reader3.GetString("StackId");
                        r.title = reader3.GetString("Title");
                        DateTime time = (DateTime)reader3["ReservingTime"];
                        DateTime nowTime = DateTime.Now;
                        TimeSpan delta = nowTime.Subtract(time);
                        double d = (double)delta.TotalMinutes;
                        r.time = String.Format("{0:F}", ReversationTime2*60 - d);
                        reversationRecords.Add(r);
                    }
                }

                GridView1.DataSource = issueRecords;
                GridView1.DataBind();
                GridView2.DataSource = reversationRecords;
                GridView2.DataBind();
                GridView3.DataSource = history;
                GridView3.DataBind();
            }
            catch (MySqlException ex)
            {
                //exception-handler
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OLMSDBConnection.Close();
            }
        }
    }

    protected void Cancel(object sender, EventArgs e)
    {
        //返回上一个页面  Response.Redirect()
        return;
    }
    protected void Repeater1_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void Repeater1_ItemCommand2(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxIDNumber_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxName_TextChanged(object sender, EventArgs e)
    {

    }
}
public class Record
{
    public string title { get; set; }
    public string issueTime { get; set; }
    public string returnTime { get; set; }
    public string overdueTime { get; set; }
    public string fine { get; set; }
}
public class Record2
{
    public string title { get; set; }
    public string time { get; set; }
    public string shelf { get; set; }
    public string stack { get; set; }
    public string barcode { get; set; }
}