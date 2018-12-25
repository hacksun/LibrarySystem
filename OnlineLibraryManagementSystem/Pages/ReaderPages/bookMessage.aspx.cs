using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Pages_bookMessage : BasePage
{
    string strCon = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
    MySqlConnection sqlcon;
    MySqlCommand sqlcom;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
        string bookId = Request["book_id"];
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            string book_id_sql = "select * from Books where BookId=" + bookId;
            string book_num = "select count(*) as num from BookBarcodes where BookId=" + bookId+" limit 0,1";
            OLMSDBConnection.Open();
            MySqlCommand cmd1 = new MySqlCommand(book_id_sql, OLMSDBConnection);
            ArrayList books_list = new ArrayList();
            MySqlDataReader reader = cmd1.ExecuteReader();
            string categoryid = "";
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Image1.ImageUrl = reader["ImageURL"].ToString();
                    //Label7.Text = " 子标题：";
                    //Label8.Text = "来源：";
                    //Label9.Text = " 装订方式：";
                    //Label10.Text = " 翻译人：";
                    //Label11.Text = " 分类：";
                    title.Text =   reader["Title"].ToString();
                    author.Text =  reader["Author"].ToString();
                    pubDate.Text = Convert.ToDateTime(reader["PubDate"]).ToString("yyyy-MM-dd");
                    price.Text =   reader["Price"].ToString();
                    isbn13.Text = reader["ISBN13"].ToString();
                    isbn10.Text = reader["ISBN10"].ToString();
                    //subtitle.Text = reader["SubTitle"].ToString();
                    //origintitle.Text = reader["OriginTitle"].ToString();
                    //binding.Text = reader["Binding"].ToString();
                    //translator.Text = reader["Translator"].ToString();
                    //catalog.Text = reader["Catalog"].ToString();
                    pages.Text = reader["Pages"].ToString();
                    publisher.Text = reader["Publisher"].ToString();
                    if (reader["Category"].ToString() != "")
                    {
                        categoryid = reader["Category"].ToString();
                    }
                    break;
                }
            }
            reader.Close();
            //显示类别
            LabelCategorytitle.Visible = false;
            LabelCatogoryinfo.Visible = false;          
            
            //馆藏
            MySqlCommand cmd2 = new MySqlCommand(book_num, OLMSDBConnection);
            MySqlDataReader reader1 = cmd2.ExecuteReader();

            while (reader1.Read())
            {
                if (reader1.HasRows)
                {
                    //        Label14.Text = "位置：";
                    numberofcollections.Text = reader1["num"].ToString();
                    //        Label15.Text = reader1["Shelfid"].ToString();
                    //        break;
                }
            }
            reader1.Close();
            if (categoryid != "")
            {
                LabelCategorytitle.Visible = true;
                LabelCatogoryinfo.Visible = true;
                string[] categoryid_array = categoryid.Split(',');
                string category = "";
                List<string> categorytotalinfo = new List<string>();
                foreach (string id in categoryid_array)
                {
                    string selectcategory = "select Name from BookCategories where CategoryId='" + id + "';";
                    MySqlCommand cmdselect = new MySqlCommand(selectcategory, OLMSDBConnection);
                    MySqlDataReader readerinfo = cmdselect.ExecuteReader();
                    if (readerinfo.Read())
                    {
                        categorytotalinfo.Add(readerinfo["Name"].ToString());
                    }
                    readerinfo.Close();
                }
                category = string.Join(",", categorytotalinfo.ToArray());
                LabelCatogoryinfo.Text = category;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            OLMSDBConnection.Close();
        }


        ////////////////////////////////////////////////图书位置信息/////////////////////////////////////////
        /*string OLMSDBConnectionString1 = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection1 = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            
            string book_num = "select * from BookBarcodes where BookId=" + bookId + " limit 0,1";
            OLMSDBConnection.Open();

            MySqlCommand cmd2 = new MySqlCommand(book_num, OLMSDBConnection);
            MySqlDataReader reader1 = cmd2.ExecuteReader();

            while (reader1.Read())
            {
                if (reader1.HasRows)
                {
                    Label15.Text = reader1["Shelfid"].ToString();
                    break;
                }
            }
            reader1.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            OLMSDBConnection.Close();
        }*/
    }

    protected void reserve_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty((string)Session["id"]))
        {
            //未登录时提示登录
            Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='../ReaderLogin.aspx';</script>");
            return;
        }
        string bookId = Request["book_id"];
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        string readerId = Session["id"].ToString();
        string barcode = "";
        GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);
        barcode = row.Cells[0].Text;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            string book_sql1 = "select * from BookBarcodes where BookId=" + bookId;
            OLMSDBConnection.Open();
            MySqlCommand cmd1 = new MySqlCommand(book_sql1, OLMSDBConnection);
            ArrayList bookList = new ArrayList();
            MySqlDataReader bookReader = cmd1.ExecuteReader();
            //Boolean bookAva = false;
            //string reserveBarcode = null;
            while(bookReader.Read())
            {
                if(bookReader.HasRows)
               {
                    //已预约
                    if (bookReader["ReservingReaderId"].ToString().Equals(readerId))
                    {
                        Response.Write("<script>alert('" + Resources.Resource.ReservationAlready + "')</script>");
                        return;
                    }
                    //有可预约图书
                  //  if((int)bookReader["Status"]==0)
                 //   {
                //        bookAva = true;
                //        reserveBarcode = bookReader["BookBarcode"].ToString();
                //    }

                }

            }
            bookReader.Close();
            //库存不足
           // if(!bookAva)
           // {
              //  Response.Write("<script>alert('" + Resources.Resource.Reservation_Fail + "')</script>");
          //      return;
          //  }
           // else
           //{
                string reservingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int reservingReaderId = 0;
                reservingReaderId = int.Parse(readerId);

                System.Diagnostics.Debug.Write(reservingReaderId);
                string reserve_sql = "update BookBarcodes set Status=2, ReservingTime=?reservingtime,ReservingReaderId=?reservingreaderid where BookBarcode=?bookbarcode";
                MySqlCommand cmd2 = new MySqlCommand(reserve_sql, OLMSDBConnection);
                cmd2.Parameters.AddWithValue("?reservingtime", reservingTime);
                cmd2.Parameters.AddWithValue("?reservingreaderid", reservingReaderId);
                cmd2.Parameters.AddWithValue("?bookbarcode", barcode);
                int result = cmd2.ExecuteNonQuery();
                if (result == 1)
                {
                    Response.Write("<script>alert('" + Resources.Resource.Reservation_Success + "')</script>");
                    bind();
                }
                else
                {
                    Response.Write("<script>alert('" + Resources.Resource.Reservation_Fail + "')</script>");
                }
            //}
        }
        catch(MySqlException ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
            throw;
        }
        finally
        {
            OLMSDBConnection.Close();
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string bookId = Request["book_id"];
        sqlcon = new MySqlConnection(strCon);
        string sqlstr = "update Librarians set BookBarcode='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "',BookId='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() + "',ShelfId='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() + "',Status='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() + "' where BookId='"
            + bookId + "'";
        sqlcom = new MySqlCommand(sqlstr, sqlcon);
        sqlcon.Open();
        sqlcom.ExecuteNonQuery();
        sqlcon.Close();
        GridView1.EditIndex = -1;
        bind();
    }

    public void bind()
    {
        string bookId = Request["book_id"];
        sqlcon = new MySqlConnection(strCon);
        string sqlborrow = "select count(*) as num from BookBarcodes where BookId='" + bookId + "' && Status='0';";
        sqlcon.Open();
        MySqlCommand cmdsqlborrow = new MySqlCommand(sqlborrow,sqlcon);
        MySqlDataReader readerborrow = cmdsqlborrow.ExecuteReader();
        if (readerborrow.Read())
        {
            numberofborrowables.Text = readerborrow["num"].ToString();
        }
        readerborrow.Close();
        sqlcon.Close();
        string sqlstr = "select BookBarcode,BookId,ShelfId,Status from BookBarcodes where BookId =" + bookId;
        MySqlDataAdapter myda = new MySqlDataAdapter(sqlstr, sqlcon);
        DataSet myds = new DataSet();
        sqlcon.Open();
        myda.Fill(myds, "BookBarcodes");
        DataTable searchResult = myds.Tables[0];
        searchResult.Columns.Add("newStatus");
        searchResult.Columns.Add("Position");
        foreach (DataRow row in searchResult.Rows)
        {
            string status = row["Status"].ToString();
            if (Session["PreferredCulture"].ToString() == "zh-CN")
            {
                if (status == "0")
                {
                    row["newStatus"] = "在馆无预约";                   
                }
                if (status == "1")
                    row["newStatus"] = "已借出";
                if (status == "2")
                    row["newStatus"] = "已预约";
            }
            else
            {
                if (status == "0")
                    row["newStatus"] = "No Reservation";
                if (status == "1")
                    row["newStatus"] = "On Loan";
                if (status == "2")
                    row["newStatus"] = "Aleardy Reserved";
            }
            string selectstackid = "select StackId from Shelves where ShelfId='" + row["ShelfId"].ToString() + "';";
            MySqlCommand cmdselectstackid = new MySqlCommand(selectstackid, sqlcon);
            MySqlDataReader readerstackid = cmdselectstackid.ExecuteReader();
            if (readerstackid.Read())
            {
                row["Position"] = row["ShelfId"] + "," + readerstackid["StackId"].ToString();
            }
            readerstackid.Close();
        }
        GridView1.Enabled = true;
        GridView1.DataSource = searchResult;
        GridView1.DataKeyNames = new string[] { "BookBarcode" };//主键
        GridView1.DataBind();
        if (GridView1.HeaderRow != null)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            Button btreserve = (Button)row.FindControl("ButtonReserve");
            btreserve.Enabled = false;
            if (row.Cells[3].Text== "在馆无预约"||row.Cells[3].Text== "No Reservation")
            {
                btreserve.Enabled = true;
            }
        }
        sqlcon.Close();
    }
}
