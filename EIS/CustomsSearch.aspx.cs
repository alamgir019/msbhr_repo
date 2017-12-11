using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class EIS_CustomsSearch : System.Web.UI.Page
{
    CustomSearchManager objMgr = new CustomSearchManager();
    string strColumns = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string strCond = "";
        foreach (GridViewRow gRow in grColumns.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                if (strColumns == "")
                {
                    if (gRow.Cells[2].Text.Trim() == "datetime")
                        strColumns = "SELECT " + "Convert(varchar(11)," + gRow.Cells[1].Text + ",106) AS " + gRow.Cells[1].Text;
                    else
                        strColumns = "SELECT " + gRow.Cells[1].Text;
                }
                else
                {
                    if (gRow.Cells[2].Text.Trim() == "datetime")
                        strColumns = strColumns + "," + "Convert(varchar(11)," + gRow.Cells[1].Text + ",106) AS " + gRow.Cells[1].Text;
                    else
                        strColumns = strColumns + "," + gRow.Cells[1].Text;
                }
            }
        }

        switch (ddlStatus.SelectedValue.Trim())
        {
            case "A":
                strCond = " WHERE EmpStatus='A'";
                break;
            case "I":
                strCond = " WHERE EmpStatus='I'";
                break;
            case "AI":
                strCond = "";
                break;
        }

        txtQuery.Text = strColumns;
        //txtQuery.Text = strColumns + " FROM " + lblSchema.Text.Trim() + strCond + " ORDER BY EMPID";
        txtQuery.Text = strColumns + " FROM View_CustomSearch " + strCond + " ORDER BY EMPID";
        lblSchema.Text = "";

        this.GetData();
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        this.GetData();
    }

    private void GetData()
    {
        try
        {
            lblMsg.Text = "";
            DataTable dtResult = objMgr.GetQueryResult(txtQuery.Text.Trim());
            if (dtResult.Rows.Count > 0)
            {
                grResult.DataSource = dtResult;
                grResult.DataBind();
            }
            else
            {
                grResult.DataSource = null;
                grResult.DataBind();
            }
            lblTotal.Text = "Total : " + grResult.Rows.Count;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnVWEmployeeReport_Click(object sender, EventArgs e)
    {
        ModalPopupTree.Show();
        DataTable dtDesc = objMgr.GetSchemaDescription("View_CustomSearch");
        if (dtDesc.Rows.Count > 0)
        {
            grColumns.DataSource = dtDesc;
            grColumns.DataBind();
            lblSchema.Text = btnVWEmployeeReport.Text.Trim();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblTotal.Text = "";
        lblSchema.Text = "";
        txtQuery.Text = "";
        grColumns.DataSource = null;
        grColumns.DataBind();
        grResult.DataSource = null;
        grResult.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Report.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        //sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grResult.RenderControl(htw);
        //sw = this.GetFooterText(sw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnExportToWord_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Report.doc";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-word";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grResult.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }
}
