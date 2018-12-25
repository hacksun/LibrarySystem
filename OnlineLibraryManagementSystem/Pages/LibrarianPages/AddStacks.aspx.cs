using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;

public partial class Pages_AddStacks : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddStacks(object sender, EventArgs e)
    {
        String stackid = "";
        String position = "";
        String stack_summary = "";
        String nowdate = "";
        String pattern = "^[A-Z]-\\d{3}$";
        if (TextBoxStackId.Text == "")
        {
            Response.Write("<script>alert('StackId is null!')</script>");
            return;
        }
        else if (TextBoxStackId.Text.Trim().Length==5&&System.Text.RegularExpressions.Regex.IsMatch(TextBoxStackId.Text.Trim(), pattern))
        {
            stackid = TextBoxStackId.Text.Trim();
        }
        else
        {
            Response.Write("<script>alert('Error StackId!\\nStackId Example:A-101')</script>");
            return;
        }
        if (TextBoxPosition.Text == ""||TextBoxPosition.Text.Trim().Length==0)
        {
            Response.Write("<script>alert('Stack Position Is Null!')</script>");
            return;
        }
        else
        {
            position = TextBoxPosition.Text.Trim();
        }
        if (TextBoxSummary.Text == "" || TextBoxSummary.Text.Trim().Length == 0)
        {
            Response.Write("<script>alert('Stack_Summary Is Null!')</script>");
            return;
        }
        else
        {
            stack_summary = TextBoxSummary.Text.Trim();
        }
        //数据库
        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);
        //检查同ID书库是否存在
       // string selectStack = "select count(*) as num from Stacks where StackId='" + stackid + "';";
        //创建书库
        //string insertStack = "insert into Stacks(StackId,Position,Summary) " + "values('" + stackid +"','"+position+ "','" + stack_summary  + "');";
        //打开数据库
        try
        {
            OLMSDBConnection.Open();
            MySqlCommand cmdselect = OLMSDBConnection.CreateCommand();
            cmdselect.CommandText = "select count(*) as num from Stacks where StackId =@s";
            MySqlParameter selectparaters;
            selectparaters = new MySqlParameter("@s", stackid);
            cmdselect.Parameters.Add(selectparaters);
            MySqlDataReader reader = cmdselect.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Int64 count = (Int64)reader["num"];
                    if (count > 0)
                    {
                        Response.Write("<script>window.alert('StackId Is Exist!');</script>");
                        return;
                    }
                    break;
                }
            }
            reader.Close();
            MySqlCommand cmdinsert = OLMSDBConnection.CreateCommand();
            cmdinsert.CommandText = "insert into Stacks(StackId,Position,Summary) values(@stackid,@position,@summary);";
            MySqlParameter insertparater;
            insertparater = new MySqlParameter("@stackid", stackid);
            cmdinsert.Parameters.Add(insertparater);
            insertparater = new MySqlParameter("@position", position);
            cmdinsert.Parameters.Add(insertparater);
            insertparater = new MySqlParameter("@summary", stack_summary);
            cmdinsert.Parameters.Add(insertparater);
            int result = 0;
            result = cmdinsert.ExecuteNonQuery();
            if (result != 0)
            {
                Response.Write("<script>alert('Created Successfully!')</script>");
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