using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_CategoryManagement : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridviewBind();
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
            string select = "select Title from Books";
            OLMSDBConnection.Open();
            MySqlCommand cmdselec = new MySqlCommand(select, OLMSDBConnection);
            DropDownList1.Items.Add("Add");
            MySqlDataReader readertitle = cmdselec.ExecuteReader();
            while (readertitle.Read())
            {
                DropDownList1.Items.Add(readertitle["Title"].ToString());
            }
            readertitle.Close();
            OLMSDBConnection.Close();
        }
    }
    public void GridviewBind()
    {
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        var OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

        MySqlCommand get_sql = new MySqlCommand("select CategoryId,Name from BookCategories");
        var resultAdapter = new MySqlDataAdapter();
        resultAdapter.SelectCommand = get_sql;
        resultAdapter.SelectCommand.Connection = OLMSDBConnection;
        var resultSet = new DataSet();

        OLMSDBConnection.Open();
        resultAdapter.Fill(resultSet);
        OLMSDBConnection.Close();

        DataTable searchResult = resultSet.Tables[0];

        Category.DataSource = searchResult;
        Category.DataKeyNames = new string[] { "CategoryId" };
        Category.DataBind();
        Category.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Category_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Category.EditIndex = e.NewEditIndex;
        GridviewBind();
    }

    protected void Category_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int categoryId = int.Parse(Category.DataKeys[e.RowIndex].Values[0].ToString());
        string name = ((TextBox)Category.Rows[e.RowIndex].FindControl("txtName")).Text;
        int updateflag = 1;//1--更新，0--不更新
        if (name.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('Name can not be none!');", true);
            GridviewBind();
            return;
        }
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
            conn.Open();
            MySqlCommand cmdselectnewname = conn.CreateCommand();
            cmdselectnewname.CommandText= "select count(*) as num from BookCategories where Name=@name  && CategoryId<>@id;";
            MySqlParameter cmdparam;
            cmdparam = new MySqlParameter("@name", name);
            cmdselectnewname.Parameters.Add(cmdparam);
            cmdparam = new MySqlParameter("@id", categoryId);
            cmdselectnewname.Parameters.Add(cmdparam);
            MySqlDataReader readernum = cmdselectnewname.ExecuteReader();
            while (readernum.Read())
            {
                if (readernum.HasRows)
                {
                    Int64 count = (Int64)readernum["num"];
                    if (count > 0)
                    {
                        updateflag = 0;
                        ClientScript.RegisterStartupScript(GetType(), "", "window.alert('This category has existed!');", true);
                        GridviewBind();
                        return;
                    }
                    break;
                }
            }
            readernum.Close();
            if (updateflag != 0)
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update BookCategories set Name=@n where CategoryId=@i";
                MySqlParameter param;
                param = new MySqlParameter("@n", name);
                cmd.Parameters.Add(param);
                param = new MySqlParameter("@i", categoryId);
                cmd.Parameters.Add(param);
                int result = cmd.ExecuteNonQuery();

                if (result == 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditSuccess + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.EditFail + "');", true);
                }
                Category.EditIndex = -1;
                GridviewBind();
            }
    }

    protected void Category_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Category.EditIndex = -1;
        GridviewBind();
    }

    protected void Category_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Category.PageIndex = e.NewPageIndex;
        GridviewBind();
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        if (newName.Text.Equals(""))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.CategoryNameRequire + "');", true);
            GridviewBind();
            return;
        }
        string name = newName.Text;
        int flag = 0;//0表示已有该类别
        string addid = "";
        string[] categoryid = { };
        int resultinsert = 0;
        int resultupdate = 0;
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        try
        {
            OLMSDBConnection.Open();
            //先找有没有
            string selecttags = "select count(*) as num,CategoryId from BookCategories where Name='" + name + "';";
            MySqlCommand cmdselecttags = new MySqlCommand(selecttags, OLMSDBConnection);
            MySqlDataReader readertags = cmdselecttags.ExecuteReader();
            while (readertags.Read())
            {
                if (readertags.HasRows)
                {
                    Int64 count = (Int64)readertags["num"];
                    if (count == 0)
                    {
                        flag = 1;//没有该类别
                    }
                    else
                    {
                       addid= readertags["CategoryId"].ToString();             
                    }
                }
            }
            readertags.Close();
            //如果没有该类别插入类别表
            if (flag == 1)
            {
                MySqlCommand cmd = OLMSDBConnection.CreateCommand();
                MySqlParameter param;
                cmd.CommandText = "insert into BookCategories(Name) values(@n);";
                param = new MySqlParameter("@n", name);
                cmd.Parameters.Add(param);
                resultinsert = cmd.ExecuteNonQuery();
                string selectid = "select CategoryId from BookCategories where Name='" + name + "';";
                MySqlCommand cmdselecid = new MySqlCommand(selectid, OLMSDBConnection);
                MySqlDataReader readerselectid = cmdselecid.ExecuteReader();
                if (readerselectid.Read())
                {
                    addid = readerselectid["CategoryId"].ToString();
                }
                readerselectid.Close();
            }
            if (DropDownList1.Text != "Add")
            {
                //书本category
                string selectcategory = "select Category from Books where Title='" + DropDownList1.SelectedItem.Text.Replace("\'", "\\\'") + "';";
                MySqlCommand cmdselect = new MySqlCommand(selectcategory, OLMSDBConnection);
                MySqlDataReader readercategory = cmdselect.ExecuteReader();
                if (readercategory.Read())
                {
                    categoryid = readercategory["Category"].ToString().Split(',');
                }
                readercategory.Close();
                List<string> categorylist = new List<string>(categoryid);


                if (categorylist.Contains(addid) != true)
                {
                    categorylist.Add(addid);
                    string updatebook = "update Books set Category='" + string.Join(",", categorylist.ToArray()) + "' where Title='" + DropDownList1.SelectedItem.Text.Replace("\'", "\\\'") + "';";
                    MySqlCommand cmdupdatebook = new MySqlCommand(updatebook, OLMSDBConnection);
                    resultupdate = cmdupdatebook.ExecuteNonQuery();
                }
                if (resultupdate != 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Successful + "');", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "window.alert('Faild,Book contains this category!');", true);
                    return;
                }
                Category.EditIndex = -1;
                GridviewBind();
            }
            if (resultinsert != 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('" + Resources.Resource.Successful + "');", true);
                GridviewBind();
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('This category has existed!');", true);
                return;
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
    }

}