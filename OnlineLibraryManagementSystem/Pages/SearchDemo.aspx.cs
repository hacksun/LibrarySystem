using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

public partial class Pages_SearchDemo : BasePage
{
    public static SortInfo siForGv = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        var lbLogin = (Master.FindControl("LoginView") as LoginView).FindControl("lbLogin") as LinkButton;
        if (lbLogin != null)
        {
            lbLogin.PostBackUrl = "~/Pages/ReaderLogin.aspx";
        }
        myshow();
    }

    protected void brSearch_Click(object sender, EventArgs e)
    {
        this.notice.Style.Add("display", "none");
        // 输入过滤，未完成
        if (tbSearch.Text.ToString().Length == 0)
        {
            Response.Redirect("~/Pages/SearchDemo.aspx");
            return;
        }

        // 查询
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getResult_sql;
        string keyword = tbSearch.Text.ToString();
        if (ddlClass.SelectedValue.ToString().Equals("Books"))
        {
            gvBookResult.Enabled = true;
            getResult_sql = new MySqlCommand("select BookId,Title,ImageURL,Author,Publisher " +
                                                     "from Books " +
                                                     "where " + (ddlField.SelectedValue.ToString().Equals("ISBN") ? "ISBN13 like '%" + keyword + "%' or ISBN10 like '%" + keyword + "%';" : ddlField.Text.ToString() + " like '%" + keyword + "%';"), OLMSDBConnection);
            //下面的语句有问题
            /*var rules_para = new MySqlParameter
            {
                ParameterName = "@rules",
                IsNullable = false,
                Value = ddlField.SelectedValue.ToString().Equals("ISBN") ? "ISBN13 like '%" + keyword + "%' or ISBN10 like '%" + keyword + "%';" : ddlField.Text.ToString() + " like '%" + keyword + "%';"
            };
            getResult_sql.Parameters.Add(rules_para);*/
        }
        else
        {
            gvPeriodicalResult.Enabled = true;
            getResult_sql = new MySqlCommand("select * " +
                                             "from Periodicals " +
                                             "where " + (ddlField.SelectedValue.ToString() + " like '%" + keyword + "%';"), OLMSDBConnection);
            // 同上
            /*var rules_para = new MySqlParameter
            {
                ParameterName = "rules",
                IsNullable = false,
                Value = ddlField.SelectedValue.ToString() + " like '%" + keyword + "%';"
            };
            getResult_sql.Parameters.Add(rules_para);*/
        }

        var resultAdapter = new MySqlDataAdapter(getResult_sql);
        var resultSet = new DataSet();

        OLMSDBConnection.Open();
        resultAdapter.Fill(resultSet);
        OLMSDBConnection.Close();


        if (ddlClass.SelectedValue.ToString().Equals("Books"))
        {
            DataTable searchResult = resultSet.Tables[0];
            gvBookResult.DataSource = searchResult;
            gvBookResult.DataBind();
            if (gvBookResult.HeaderRow != null)
            {
                gvBookResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        else
        {
            DataTable searchResult = resultSet.Tables[0];
            searchResult.Columns.Add("NewType");
            foreach (DataRow row in searchResult.Rows)
            {
                if (Session["PreferredCulture"].ToString() == "zh-CN")
                {
                    if (row["Type"].ToString() == "0")
                    {
                        row["NewType"] = "杂志";
                    }
                    if (row["Type"].ToString() == "1")
                    {
                        row["NewType"] = "报纸";
                    }
                }
                else
                {
                    if (row["Type"].ToString() == "0")
                    {
                        row["NewType"] = "Magazine";
                    }
                    if (row["Type"].ToString() == "1")
                    {
                        row["NewType"] = "Newspaper";
                    }
                }
            }
            gvPeriodicalResult.DataSource = searchResult;
            gvPeriodicalResult.DataBind();
            if (gvPeriodicalResult.HeaderRow != null)
            {
                gvPeriodicalResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedValue.ToString().Equals("Books"))
        {
            ddlField.Items.FindByValue("Author").Enabled = true;
            ddlField.Items.FindByValue("ISBN").Enabled = true;
            ddlField.Items.FindByValue("ISSN").Enabled = false;
            gvPeriodicalResult.Enabled = false;
            gvPeriodicalResult.DataSource = null;
            gvPeriodicalResult.DataBind();
        }
        else
        {
            ddlField.Items.FindByValue("Author").Enabled = false;
            ddlField.Items.FindByValue("ISBN").Enabled = false;
            ddlField.Items.FindByValue("ISSN").Enabled = true;
            gvBookResult.Enabled = false;
            gvBookResult.DataSource = null;
            gvBookResult.DataBind();
        }
    }

    protected void tbSearch_TextChanged(object sender, EventArgs e)
    {
        if (tbSearch.Text == "") this.notice.Style.Add("display", "block");
        else this.notice.Style.Add("display", "none");
    }
    public void myshow()
    {

        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand getDeposit_sql = new MySqlCommand("select * from Notices ORDER BY NoticeId DESC LIMIT 3");
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
        String[] notices = { "No announcement", "No announcement", "No announcement", "", "" };
        String[] titles = { "", "", "", "", "" };
        String[] dates = { "", "", "", "", "" };
        int i = 0;
        int j = 0;
        int m = 0;
        foreach (DataRow dr in dtResult.Rows)
        {
            titles[i++] = dr["Title"].ToString();
            notices[j++] = dr["Details"].ToString();
            dates[m++]= Convert.ToDateTime(dr["Timestamp"]).ToString("yyyy-MM-dd");
        }
        title1.Text = titles[0].ToString();
        title2.Text = titles[1].ToString();
        title3.Text = titles[2].ToString();
        notice1.Text = notices[0].ToString();
        notice2.Text = notices[1].ToString();
        notice3.Text = notices[2].ToString();
        date1.Text ="Date:" + dates[0].ToString();
        date2.Text ="Date:" + dates[1].ToString();
        date3.Text ="Date:" + dates[2].ToString();

    }

    protected void tbSearch_TextChanged1(object sender, EventArgs e)
    {

    }

    protected void gvBookResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string bookId = gvBookResult.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvCopy = e.Row.FindControl("gvCopy") as GridView;
            Label copy = e.Row.FindControl("copy") as Label;
            Label copy0 = e.Row.FindControl("copy0") as Label;
            string num = "";
            string num0 = "";

            string book_allnum = "select count(*) as num from BookBarcodes where BookId=" + bookId + " limit 0,2";
            string book_num0 = "select count(*) as num from BookBarcodes where BookId=" + bookId + " and Status=0";
            string sqlstr = "select BookBarcode,BookId,ShelfId,Status from BookBarcodes where BookId =" + bookId;
            string strCon = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection sqlcon = new MySqlConnection(strCon);
            sqlcon.Open();
            MySqlCommand cmd_num = new MySqlCommand(book_allnum, sqlcon);
            MySqlDataReader reader = cmd_num.ExecuteReader();
            while(reader.Read())
            {
                if(reader.HasRows)
                {
                    num = reader["num"].ToString();
                }
            }
            reader.Close();
            cmd_num.CommandText = book_num0;
            reader = cmd_num.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    num0 = reader["num"].ToString();
                }
            }
            reader.Close();

            copy.Text = Resources.Resource.Numberofcollections + ":" + num;
            copy0.Text = Resources.Resource.Numberofborrowables + ":" + num0;

            MySqlDataAdapter myda = new MySqlDataAdapter(sqlstr, sqlcon);
            DataSet myds = new DataSet();     
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
                        row["newStatus"] = "在馆无预约";
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
            gvCopy.DataSource = searchResult;
            gvCopy.DataBind();
            foreach (GridViewRow row in gvCopy.Rows)
            {
                Button btreserve = (Button)row.FindControl("ButtonReserve");
                btreserve.Enabled = false;
                if (row.Cells[2].Text == "在馆无预约" || row.Cells[2].Text == "No Reservation")
                {
                    btreserve.Enabled = true;
                }
            }
        }
    }

    protected void gvCopy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().Equals("Btn_reserve"))
        {
            if (string.IsNullOrEmpty((string)Session["id"]))
            {
                //未登录时提示登录
                Response.Write("<script type='text/javascript'>alert('" + Resources.Resource.LogInNotice + "');location.href='ReaderLogin.aspx';</script>");
                return;
            }

            Button btn = (Button)e.CommandSource;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            GridView gv = gvr.NamingContainer as GridView;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string bookBarcode = gv.DataKeys[rowIndex].Value.ToString();
            string readerId = Session["id"].ToString();
            string book_sql1 = "select * from BookBarcodes where BookBarcode=" + bookBarcode;
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            OLMSDBConnection.Open();
            MySqlCommand cmd1 = new MySqlCommand(book_sql1, OLMSDBConnection);
            MySqlDataReader bookReader = cmd1.ExecuteReader();
            string bookId = "";
            while (bookReader.Read())
            {
                if (bookReader.HasRows)
                {
                    bookId = bookReader["BookId"].ToString();
                    if ((int)bookReader["Status"] != 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Reservation_Fail + "');", true);
                        return;
                    }
                }

            }
            bookReader.Close();
            string book_sql3 = "select * from BookBarcodes where BookId=" + bookId;
            MySqlCommand cmd3 = new MySqlCommand(book_sql3, OLMSDBConnection);
            MySqlDataReader bookReader3 = cmd3.ExecuteReader();
            while (bookReader3.Read())
            {
                if (bookReader3.HasRows)
                {
                    if (bookReader3["ReservingReaderId"].ToString().Equals(readerId))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.ReservationAlready + "');", true);
                        return;
                    }
                }
            }
            bookReader3.Close();

            string reservingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int reservingReaderId = int.Parse(readerId);
            string reserve_sql = "update BookBarcodes set Status=2, ReservingTime=?reservingtime,ReservingReaderId=?reservingreaderid where BookBarcode=?barcode";
            MySqlCommand cmd2 = new MySqlCommand(reserve_sql, OLMSDBConnection);
            cmd2.Parameters.AddWithValue("?reservingtime", reservingTime);
            cmd2.Parameters.AddWithValue("?reservingreaderid", reservingReaderId);
            cmd2.Parameters.AddWithValue("?barcode", bookBarcode);
            int result = cmd2.ExecuteNonQuery();
            if (result == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Reservation_Success + "');", true);
                string sqlstr = "select BookBarcode,BookId,ShelfId,Status from BookBarcodes where BookId =" + bookId;
                string strCon = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
                MySqlConnection sqlcon = new MySqlConnection(strCon);
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
                            row["newStatus"] = "在馆无预约";
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
                gv.DataSource = searchResult;
                gv.DataBind();
                foreach (GridViewRow row in gv.Rows)
                {
                    Button btreserve = (Button)row.FindControl("ButtonReserve");
                    btreserve.Enabled = false;
                    if (row.Cells[2].Text == "在馆无预约" || row.Cells[2].Text == "No Reservation")
                    {
                        btreserve.Enabled = true;
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Reservation_Fail + "');", true);
                return;
            }

        }


    }
}