using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void InitializeCulture()
    {
        if (Session["PreferredCulture"] == null)
        {
            Session["PreferredCulture"] = "zh-CN";
        }
        string userCulture = Session["PreferredCulture"].ToString();
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(userCulture);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(userCulture);
    }
}