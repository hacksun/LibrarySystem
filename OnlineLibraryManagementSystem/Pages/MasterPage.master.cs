using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Web.Security;

public partial class Pages_MasterPage : BaseMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlLanguages.SelectedValue = Session["PreferredCulture"].ToString();
        }
    }

    protected void ddlLanguages_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PreferredCulture"] = ddlLanguages.SelectedValue.ToString();
        Response.Redirect(Request.Url.ToString());
    }
}
