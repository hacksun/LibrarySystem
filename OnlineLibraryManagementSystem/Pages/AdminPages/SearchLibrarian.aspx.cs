using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;            
using MySql.Data.MySqlClient;

public partial class Pages_AdminPages_SearchLibrarian : BasePage
{
    string strCon = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
    MySqlConnection sqlcon;
    MySqlCommand sqlcom;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    //删除
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string sqlstr = "delete from Librarians where LibrarianId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        sqlcon = new MySqlConnection(strCon);
        sqlcom = new MySqlCommand(sqlstr, sqlcon);
        sqlcon.Open();
        sqlcom.ExecuteNonQuery();
        sqlcon.Close();
        Bind();
    }
    //更新
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        sqlcon = new MySqlConnection(strCon);
        string sqlstr = "update Librarians set Account='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "',Password='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() + "',Name='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() + "' where LibrarianId='"
            + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        sqlcom = new MySqlCommand(sqlstr, sqlcon);
        sqlcon.Open();
        sqlcom.ExecuteNonQuery();
        sqlcon.Close();
        GridView1.EditIndex = -1;
        Bind();
    }
    //取消
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    //绑定

    public void Bind()
    {
        string sqlstr = "select * from Librarians";
        sqlcon = new MySqlConnection(strCon);
        MySqlDataAdapter myda = new MySqlDataAdapter(sqlstr, sqlcon);
        DataSet myds = new DataSet();
        sqlcon.Open();
        myda.Fill(myds, "Librarians");
        GridView1.DataSource = myds;
        GridView1.DataKeyNames = new string[] { "LibrarianId" };//主键
        GridView1.DataBind();
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        sqlcon.Close();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!rfvAccount.IsValid || !rfvName.IsValid || !rfvPassword.IsValid)
        {
            Bind();
            return;
        }
            
        string Account = TextBox2.Text.ToString();
        string Password = TextBox3.Text.ToString();
        string Name = TextBox4.Text.ToString();

        sqlcon = new MySqlConnection(strCon);
        sqlcon.Open();
        string exsql = "select count(*) as num from Librarians where Account = ?Account;";
        MySqlCommand exsqlcom = new MySqlCommand(exsql, sqlcon);
        exsqlcom.Parameters.AddWithValue("?Account", Account);
        MySqlDataReader reader = exsqlcom.ExecuteReader();
        while (reader.Read())
        {
            if (reader.HasRows)
            {
                Int64 count = (Int64)reader["num"];
                if (count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.AccountExist + "');", true);
                    Bind();
                    return;
                }
                break;
            }
        }
        reader.Close();

            string sql = "insert into Librarians(Account,Password,Name) values('" + Account + "','" + Password + "','" + Name + "')";
            string sqlstr = "select LibrarianId,Account,Password,Name from Librarians";
            
            MySqlCommand sqlcom = new MySqlCommand(sql, sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        GridView1.EditIndex = -1;
        Bind();

        
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //GridView1.DataBind();
        Bind();
    }



}


