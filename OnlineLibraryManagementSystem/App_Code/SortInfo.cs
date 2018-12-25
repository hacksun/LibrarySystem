using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 表格排序类
/// </summary>
public class SortInfo : System.Web.UI.Page   //这里继承了System.Web.UI.Page，因为只有这样才能用ViewState.
{
    public string SortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }

    public string SortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }


    /// <summary>
    /// 排序的表格的数库源,在执行DataBind()方法前,构造些类,
    /// 构造之前用Session或页面的静态成员 保存此类,在表格的Sorting事件中重新读出,并调用SortDataBind()方法
    /// SortInfo命名规则:si+页面名+控件名
    /// </summary>
    /// <param name="dt"></param>
    public SortInfo(DataTable dt)
    {
        ViewState["dt"] = dt;
    }

    private string GetSortDirection()
    {
        switch (SortDirection)
        {
            case "ASC":
                SortDirection = "DESC";
                break;
            case "DESC":
                SortDirection = "ASC";
                break;
        }
        return SortDirection;

    }

    private DataView SortDataTable(System.Data.DataTable dt, bool IsPageIndexChanging)
    {
        if (dt != null)
        {
            DataView dv = new DataView(dt);
            if (SortExpression != string.Empty)
            {
                if (IsPageIndexChanging)
                {
                    dv.Sort = SortExpression+" "+SortDirection;
                }
                else
                {
                    dv.Sort = SortExpression+" "+GetSortDirection();
                }
            }
            return dv;
        }
        else
        {
            return new DataView();
        }
    }

    public void SortDataBind(GridView gv, int PageIndex, bool GetSort)
    {
        DataTable dt = (DataTable)ViewState["dt"];
        gv.PageIndex = PageIndex;
        gv.DataSource = SortDataTable(dt, GetSort);
        gv.DataBind();
    }


    /// <summary>
    /// 重新帮定超级链接列
    /// </summary>
    /// <param name="gv">表格</param>
    /// <param name="ColumnIndex">列索引</param>
    /// <param name="Address">目标地址,此地址除了值不包括其它都要包括.如SvcCtr_Detail.aspx?id=</param>
    /// <param name="Target">打开方式</param>
    public void BindLinkButton(GridView gv, int ColumnIndex, string Address, string Target)
    {
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            gv.Rows[i].Cells[ColumnIndex].Text = "<a href=" + Address + gv.Rows[i].Cells[ColumnIndex].Text.Trim() +
            " target=" + Target + ">" + gv.Rows[i].Cells[ColumnIndex].Text + "</a>";
        }
    }


    /// <summary>
    /// 重新帮定超级链接列,参数是由前缀+列内容组成的.
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="ColumnIndex">必须是一个数字列否则用报错</param>
    /// <param name="Address"></param>
    /// <param name="Prefix">前缀</param>
    /// <param name="Target"></param>
    /// <param name="FormatString">格式化列串的格式化串</param>
    public void BindLinkButton(GridView gv, int ColumnIndex, string Address, string Prefix, string Target, string FormatString)
    {
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            gv.Rows[i].Cells[ColumnIndex].Text = "<a href=" + Address + Prefix + Convert.ToInt32(gv.Rows[i].Cells[ColumnIndex].Text).ToString(FormatString) +
            " target=" + Target + ">" + gv.Rows[i].Cells[ColumnIndex].Text + "</a>";
        }
    }



}