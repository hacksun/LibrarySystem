using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AdminPages_Settings : BasePage
{
    string DamageKey = "DamageFineRate";
    string depositKey = "Deposit";
    string LostKey = "LostFineRate";
    string MaximumKey = "MaximumIssue";
    string OverdueFinePerDayKey = "OverdueFinePerDay";
    string LimitofreservationKey = "Limitofreservation";
    string LimitofissueKey = "Limitofissue";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Deposit.Text = getValue(depositKey);
            DamageFineRate.Text = getValue(DamageKey);
            LostFineRate.Text = getValue(LostKey);
            MaximunIssue.Text = getValue(MaximumKey);
            OverdueFinePerDay.Text = getValue(OverdueFinePerDayKey);
            Limitofreservation.Text = getValue(LimitofreservationKey);
            Limitofissue.Text = getValue(LimitofissueKey);
        }
    }
    protected void Submit(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        string item = "appSettings";
        Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
        AppSettingsSection appSection = (AppSettingsSection)config.GetSection(item);
        string depositValue = Deposit.Text;
        setValue(appSection, depositKey, depositValue);
        string DamageValue = DamageFineRate.Text;
        setValue(appSection, DamageKey, DamageValue);
        string LostValue = LostFineRate.Text;
        setValue(appSection, LostKey, LostValue);
        string MaximunValue = MaximunIssue.Text;
        setValue(appSection, MaximumKey, MaximunValue);
        string OverdueFinePerDayValue = OverdueFinePerDay.Text;
        setValue(appSection, OverdueFinePerDayKey, OverdueFinePerDayValue);
        string LimitofreservationValue = Limitofreservation.Text;
        setValue(appSection, LimitofreservationKey, LimitofreservationValue);
        string LimitofissueValue = OverdueFinePerDay.Text;
        setValue(appSection, LimitofissueKey, LimitofissueValue);
        config.Save();
        Response.Write("<script>alert('" + Resources.Resource.Successful + "')</script>");
    }
    private string getValue(string key)
    {
        try
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
        catch
        {
            return "";
        }
    }
    private void setValue(AppSettingsSection appSection, string key, string value)
    {
        if (value == null || value == "")
        {
            value = ConfigurationManager.AppSettings.Get(key);
        }
        if (appSection.Settings[key] == null)
        {
            appSection.Settings.Add(key, value);
        }
        else
        {
            appSection.Settings.Remove(key);
            appSection.Settings.Add(key, value);
        }
    }
}