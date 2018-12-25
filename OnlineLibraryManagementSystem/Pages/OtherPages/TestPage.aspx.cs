using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;
using System.Drawing;

public partial class Pages_TestPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Console.WriteLine("a");
        if (!IsPostBack)
        {
            //Database connection test
            string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
            MySqlConnection OLMSDBConnection = new MySqlConnection(OLMSDBConnectionString);

            OLMSDBConnection.Open();
            OLMSDBConnection.Close();

            //Book information query test
            Book book = BookInfoQuery.GetByISBN("9787111128069");
            if (book != null)
            {
                lbBookInfo.Text = book.isbn10 + " " + book.image + " " + book.author[0]+book.tags[0].title;
            }
            else
            {
                lbBookInfo.Text = "Book Not Found";
            }

            ViewState["book"] = book;
        }
    }

    protected void btShowBarcode_Click(object sender, EventArgs e)
    {
        Book book = (Book)ViewState["book"];
        string path = HttpRuntime.AppDomainAppPath.ToString() + "Images\\Barcode\\";
        //Barcode generation test
        if (book != null)
        {
           // var barcodeImage = MyBarcodeGenerator.Generate(book.isbn10) as System.Drawing.Image;
       
            //MyBarcodeGenerator.ShowBarcode(book.isbn13, this.Response);
        }
        else
        {
            Response.Write("Book Not Found");
        }
    }
}