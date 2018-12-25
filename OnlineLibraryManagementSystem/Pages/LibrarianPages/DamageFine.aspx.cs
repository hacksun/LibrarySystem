using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_DamageFine : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void fineReparation_Click(object sender, EventArgs e)
    {
        //检查登陆
        if (string.IsNullOrEmpty((string)Session["lid"]))
        {
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='/Pages/LibrarianLogin.aspx';</script>");
        }
        string account = "";
        if (string.IsNullOrEmpty((string)Session["lid"]))
        {
            Response.Write("<script>alert('Account Is Null!')</script>");
            return;
        }
        else
        {
            account = Session["lid"].ToString();
        }
        string BarcodeID = tbBarcode.Text;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            OLMSDBConnection.Open();
            string getRecordIdSql = "SELECT * FROM IssueRecords WHERE BookBarcode = " + BarcodeID;
            MySqlCommand cmd2 = new MySqlCommand(getRecordIdSql, OLMSDBConnection);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            int bookId = 0;
            double originalFine = 0;
            double price = 0;
            double newFine = 0;
            double rate = 0;
            int amount = 0;
            int newamount = 0;
            //如果没有符合条件的图书说明输入书号错误
            if (!reader2.HasRows)
            {
                Response.Write("<script>alert('" + Resources.Resource.InputError + "')</script>");
                return;
            }
            while (reader2.Read())
            {
                if(reader2.HasRows)
                {
                    if (reader2["Fine"].ToString().Equals(""))
                        originalFine = 0;
                    else
                        originalFine = Convert.ToDouble(reader2["Fine"].ToString());
                    break;
                }
            }
            reader2.Close();
            string getIDSql = "select * from BookBarcodes where BookBarcode = " + BarcodeID;
            MySqlCommand cmd = new MySqlCommand(getIDSql, OLMSDBConnection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                if (reader.HasRows)
                {
                    bookId = int.Parse(reader["BookId"].ToString());
                    break;
                }
            }
            reader.Close();
            string getPriceSql = "SELECT * FROM Books WHERE BookId = " + bookId;
            MySqlCommand cmd3 = new MySqlCommand(getPriceSql, OLMSDBConnection);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while(reader3.Read())
            {
                if(reader3.HasRows)
                {
                    price = Convert.ToDouble(reader3["Price"].ToString());
                    amount = int.Parse(reader3["Amount"].ToString());
                    break;
                }
            }
            reader3.Close();
            System.Diagnostics.Debug.WriteLine(price);

            if (TypeField.SelectedValue.ToString().Equals("Damage"))
            {
                rate = Convert.ToDouble(ConfigurationManager.AppSettings["DamageFineRate"].ToString());
                newFine = originalFine + rate * price;
            }
            else
            {
                rate = Convert.ToDouble(ConfigurationManager.AppSettings["LostFineRate"].ToString());
                newFine = originalFine + rate * price;
            }
            System.Diagnostics.Debug.WriteLine(newFine);
            DateTime time_now = DateTime.Now;
            string now_time = time_now.ToString("yyyy-MM-dd HH:mm:ss");
            string updateFineSql = "update IssueRecords set Status=2,Fine=?fine,ReturnTime=?time where BookBarcode=?bookbarcode";
            MySqlCommand cmd4 = new MySqlCommand(updateFineSql, OLMSDBConnection);
            cmd4.Parameters.AddWithValue("?fine", newFine);
            cmd4.Parameters.AddWithValue("?bookbarcode", BarcodeID);
            cmd4.Parameters.AddWithValue("time", now_time);
            cmd4.ExecuteNonQuery();

            //删除该书
            string deleteSql = "delete from BookBarcodes where BookBarcode=" + BarcodeID;
            MySqlCommand cmd5 = new MySqlCommand(deleteSql, OLMSDBConnection);
            cmd5.ExecuteNonQuery();

            //更新书本数量
            newamount = amount - 1;
            string updateAmountSql = "update Books set Amount=?a where BookId=?b";
            MySqlCommand cmd6 = new MySqlCommand(updateAmountSql, OLMSDBConnection);
            cmd6.Parameters.AddWithValue("?a", newamount);
            cmd6.Parameters.AddWithValue("?b", bookId);
            cmd6.ExecuteNonQuery();

            //插入删除记录
            string getLibrarianSql = "select LibrarianId from Librarians where Account=" + account;
            int librarianId = 0;
            MySqlCommand cmd7 = new MySqlCommand(getLibrarianSql, OLMSDBConnection);
            MySqlDataReader readerlid = cmd7.ExecuteReader();
            if (readerlid.Read())
            {
                librarianId = int.Parse(readerlid["LibrarianId"].ToString());
            }
            readerlid.Close();
            string insertbookmanagement = "insert BookManagementRecords(Operation,BookId,LibrarianId,Amount) Values('Delete','" + bookId + "','" + librarianId + "','" + 1 + "');";
            MySqlCommand cmdinsertbookmanagement = new MySqlCommand(insertbookmanagement, OLMSDBConnection);
            cmdinsertbookmanagement.ExecuteNonQuery();
            Response.Write("<script>alert('" + Resources.Resource.DamageAlert + " " + newFine + " " + Resources.Resource.Yuan + "')</script>");
        }
        catch(MySqlException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        finally
        {
            OLMSDBConnection.Close();
        }
    }
}