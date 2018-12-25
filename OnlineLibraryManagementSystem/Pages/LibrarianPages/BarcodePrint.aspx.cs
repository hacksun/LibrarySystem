using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LibrarianPages_BarcodePrint : System.Web.UI.Page
{
    protected int flag = 0;//1--Barcode,2--info_barcode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (Session["Barcode"] != null)
            {
                string[] al = Session["Barcode"] as string[];
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                foreach (string barcode in al)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = barcode + ".jpg";
                    dt.Rows.Add(dr);
                }
                DataListbookbarcode.Enabled = true;
                DataListbookbarcode.DataSource = dt;
                DataListbookbarcode.DataBind();
                Session["Barcode"] = null;
                Session.Remove("Barcode");
                flag = 1;
            }
            if (Session["info_barcode"] != null)
            {
                string barcode = Session["info_barcode"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                DataRow dr = dt.NewRow();
                dr["name"] = barcode + ".jpg";
                dt.Rows.Add(dr);
                DataListbookbarcode.Enabled = true;
                DataListbookbarcode.DataSource = dt;
                DataListbookbarcode.DataBind();
                Session["info_barcode"] = null;
                Session.Remove("info_barcode");
                flag = 2;
            }
            //this.Page.RegisterClientScriptBlock(typeof(this),"","window.onload=doPrint(0");
        }
    }

    protected void Buttoncancel_Click(object sender, EventArgs e)
    {
        if (flag==1)
        {
            Response.Redirect("AddBooks.aspx");
            return;
        }
        if (flag==2)
        {
            Response.Redirect("SearchBooks.aspx");
            return;
        }
        return;
    }
}