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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (! IsPostBack)
        {

            string account = Request["ReaderId"];
            if (account == null)
            {
                //exception-handler
                return;
            }
            //Database connection test
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            string selectReaderSql = "select * from Readers where ReaderId = ?account;";
            
            try
            {
                OLMSDBConnection.Open();
                MySqlCommand cmd = new MySqlCommand(selectReaderSql, OLMSDBConnection);
                cmd.Parameters.AddWithValue("?account", account);
                MySqlDataReader reader = cmd.ExecuteReader();
                string id="" ;
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TextBoxEmail.Text = reader["Email"].ToString();
                    
                        TextBoxName.Text = reader["Name"].ToString();
                        TextBoxPhone.Text = reader["Phone"].ToString();
                        // string idNumber = reader["idNumber"].ToString();
                        //idNumber = "XXXXXXXXXXXXXX" + idNumber.Substring(idNumber.Length - 4);
                        //TextBoxIDNumber.Text = idNumber;
                        TextBoxIDNumber.Text = reader["idNumber"].ToString();
                        TextBoxPassword.Text = reader["Password"].ToString();
                        id = reader["ReaderId"].ToString();
                        break;
                    }
                }
                reader.Close();
                string selectBookSql = "select IssueTime, ReturnTime,Title, Fine " +
                "from  IssueRecords, BookBarcodes, Books " +
                "where  BookBarcodes.BookBarcode =   IssueRecords.BookBarcode and  BookBarcodes.BookId = Books.BookId " +
                "and IssueRecords.ReaderId = ?reader_id;";
                string selectRevervationSql = "select Books.Title, A.ReservingTime, A.ShelfId, C.StackId, A.BookBarcode from BookBarcodes as A, Books, Shelves  as C" +
                " where A.status = 2 and A.ReservingReaderId = ?readerid and Books.BookId = A.BookId and A.ShelfId = C.ShelfId;";
                int totalOverdueDays = 0;
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
                            flag = 0; //未归还
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
                        r.time = String.Format("{0:F}", ReversationTime2 * 60 - d);
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

    protected void Submit(object sender, EventArgs e)
    {
        string name, readerId, password, idNumber, phone, email;
        readerId = Request["ReaderId"];
        if (TextBoxName.Text == "")
        {
            Response.Write("<script>window.alert('User name can not be empty!');</script>");
            return;
        }
        else
        {
            name = TextBoxName.Text;
        }
        if (TextBoxPhone.Text == "")
        {
            Response.Write("<script>window.alert('Phone can not be empty!');</script>");
            return;
        }
        else
        {
            phone = TextBoxPhone.Text;
           //Response.Write(phone);
            //这里应该有正则匹配去除非法输入防止篡改数据库
        }
        if (TextBoxIDNumber.Text == "")
        {
            Response.Write("<script>window.alert('IDNumber can not be empty!');</script>");
            return;
        }
        else
        {
            //后期补上正则匹配
            idNumber = TextBoxIDNumber.Text;
            if (idNumber.Length != 18)
            {
                Response.Write("<script>window.alert('The ID number is not in the right format!');</script>");
                return;
            }
        }
        if (TextBoxPassword.Text == "")
        {
            Response.Write("<script>window.alert('Password can not be empty!');</script>");
            return;
        }
        else
        {
            password = TextBoxPassword.Text;
         
        }

        email = TextBoxEmail.Text;
        
        //链接数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //检测同账号是否注册过
        string selectReaderSql = "select count(*) as num from Readers where ReaderId !=?readerId and (Phone = ?phone or IdNumber =?idNumber);";
       // string selectReaderSql = "select count(*) as num from Readers where ReaderId !="+readerId+"and (Phone = "+phone +"or IdNumber = "+idNumber+");";
        string insertReaderSql = "UPDATE  Readers SET Phone=?phone,Name=?name,IdNumber=?idNumber,Email=?email,Password=?password where ReaderId=?readerId ";
       // string insertReaderSql = "UPDATE  Readers SET Phone=" + phone + ",Name=" + name + ",IdNumber=" + idNumber + ",Email=" + email + ",Password=" + password + " where ReaderId=" + readerId;
        
        try
        {
           OLMSDBConnection.Open();
            MySqlCommand cmd2 = new MySqlCommand(selectReaderSql, OLMSDBConnection);
            cmd2.Parameters.AddWithValue("?readerId",readerId );
            cmd2.Parameters.AddWithValue("?phone", phone);
            cmd2.Parameters.AddWithValue("?idNumber", idNumber);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if (count > 0)
                    {
                        Response.Write("<script>window.alert('Account already exists or IDNumber is wrong!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();

            MySqlCommand cmd = new MySqlCommand(insertReaderSql, OLMSDBConnection);
            cmd.Parameters.AddWithValue("?readerId", readerId);
            cmd.Parameters.AddWithValue("?phone", phone);
            cmd.Parameters.AddWithValue("?name", name);
            cmd.Parameters.AddWithValue("?password", password);
            cmd.Parameters.AddWithValue("?idNumber", idNumber);
            cmd.Parameters.AddWithValue("?email", email);
           // Response.Write(insertReaderSql);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write("<script>window.alert('Modification is successful!');</script>");
                return;
            }
            else
            {
                Response.Write("<script>window.alert('Failed');</script>");
                //Response.Redirect()
                return;
            }

        }
        catch (MySqlException ex)
        {
            //Console.WriteLine(ex.Message);
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            OLMSDBConnection.Close();
        }

    }

    protected void Delete(object sender, EventArgs e)
    {

        string readerId = Request["ReaderId"];
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        string selectReaderSql = "select count(*) as num from IssueRecords where ReaderId =?readerId and Status=3;";
        string selectReaderSql1 = "select Status  from Readers where ReaderId =?readerId ";
        string deleteReaderSql = "DELETE  FROM Readers  where ReaderId=?readerId ";
        try
        {
            OLMSDBConnection.Open(); 
            MySqlCommand cmd2 = new MySqlCommand(selectReaderSql, OLMSDBConnection);
            cmd2.Parameters.AddWithValue("?readerId", readerId);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if (count > 0)
                    {
                        Response.Write("<script>window.alert('Please return the book first!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();
            MySqlCommand cmd3 = new MySqlCommand(selectReaderSql1, OLMSDBConnection);
            cmd3.Parameters.AddWithValue("?readerId", readerId);
            MySqlDataReader reader1 = cmd3.ExecuteReader();
            while (reader1.Read())
            {
                if (reader1.HasRows)
                {
                    
                    if (reader1["Status"].ToString() == "1")
                    {
                        Response.Write("<script>window.alert('Please pay the fine first!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader1.Close();

            MySqlCommand cmd = new MySqlCommand(deleteReaderSql, OLMSDBConnection);
            cmd.Parameters.AddWithValue("?readerId", readerId);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write("<script>window.alert(' The removal completed successfully!');</script>");
                Session["delete"] = "true";
                Response.Redirect("SearchReader.aspx");
                return;
            }
            else
            {
                Response.Write("<script>window.alert('Failed');</script>");
                //Response.Redirect()
                return;
            }

        }
        catch (MySqlException ex)
        {
            //Console.WriteLine(ex.Message);
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            OLMSDBConnection.Close();
        }


    }

    protected void Cancel(object sender, EventArgs e)
    {
        
        Response.Redirect("SearchReader.aspx");
        return;
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void Repeater1_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void Repeater1_ItemCommand2(object source, RepeaterCommandEventArgs e)
    {

    }
 

    protected void TextBoxPhone_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxName_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxIDNumber_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxEmail_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxPassword_TextChanged(object sender, EventArgs e)
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