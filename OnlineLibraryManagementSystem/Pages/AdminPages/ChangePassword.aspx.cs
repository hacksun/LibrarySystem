using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AdminPages_ChangePassword : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btChangePassword_Click(object sender, EventArgs e)
    {
        //确保已经通过了所有验证控件
        if (!rfvOldPassword.IsValid || !rfvNewPassword.IsValid || !rfvConfirmNewPassword.IsValid || !cvNewPassword.IsValid)
            return;
        string adminPassword = ConfigurationManager.AppSettings.Get("AdminPassword").ToString();
        if (!tbOldPassword.Text.Equals(adminPassword))
        {
            Response.Write("<script>alert('" + Resources.Resource.WrongPassword + "')</script>");
            return;
        }
        string newPassword = tbNewPassword.Text;
        Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
        AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
        appSection.Settings.Remove("AdminPassword");
        appSection.Settings.Add("AdminPassword", newPassword);
        config.Save();
        Session["lid"] = null;
        Response.Write("<script type='text/javascript'>alert('OK');location.href='../AdminLogin.aspx';</script>");
    }
}