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
using System.Text;
public partial class Payroll_Payroll_StaffSalaryAndIT : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    string strHeadID = "";
    string strHeadName = "";
    string strHeadSL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlMonth.Items.Add("Nil");
            Common.FillDropDownList(objMastMg.SelectSalaryLocation(0), ddlLocation, "SalLocName", "SalLocId", true, "Nil");
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "T"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        DataTable dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "");
        DataTable dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);
        DataTable dtTotalSalHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);
        DataTable dtReportSalHead = objPayRptMgr.SelectSalHeadWithSeqForReport(1);

        DataRow nRow = dtSalaryHead.NewRow();
        nRow["SHEADID"] = "99999";
        nRow["HEADNAME"] = "Gross Salary";
        nRow["HEADNATURE"] = "2";
        nRow["DEFALTAMNT"] = "0.00";
        dtSalaryHead.Rows.Add(nRow);
        dtSalaryHead.AcceptChanges();
        grSalItem.DataSource = dtSalaryHead;
        grSalItem.DataBind();
        DataRow[] foundRows;
        DataRow[] foundGrRows;
        DataRow[] foundRptRows;
        string strExpr = "";
        string strExprGross = "";
        for (int i = 0; i < grSalItem.Rows.Count; i++)
        {            
            strExpr = "SHEADID=" + grSalItem.DataKeys[i].Values[0].ToString().Trim();
            strExprGross = "SHEADID=" + grSalItem.DataKeys[i].Values[0].ToString().Trim();
            foundRows = dtTotalSalHead.Select(strExpr);
            foundGrRows = dtGrossSalHead.Select(strExprGross);
            foundRptRows = dtReportSalHead.Select(strExpr);

            if (grSalItem.DataKeys[i].Values[0].ToString().Trim() == "99999")
            {
                if (foundRptRows.Length > 0)
                {
                    CheckBox chBox1 = (CheckBox)grSalItem.Rows[i].FindControl("chkBox");
                    chBox1.Checked = true;
                    TextBox txtSN1 = (TextBox)grSalItem.Rows[i].FindControl("txtSeqNo");
                    txtSN1.Text = foundRptRows[0]["SEQNO"].ToString().Trim();
                }
                continue;
            }

            HiddenField hfDT = (HiddenField)grSalItem.Rows[i].FindControl("hfDispType");
            if (foundRptRows.Length > 0)
            {
                CheckBox chBox = (CheckBox)grSalItem.Rows[i].FindControl("chkBox");
                chBox.Checked = true;
                TextBox txtSN = (TextBox)grSalItem.Rows[i].FindControl("txtSeqNo");
                txtSN.Text = foundRptRows[0]["SEQNO"].ToString().Trim();
            }
            
            if (foundRows.Length == 0)
            {
                if (foundGrRows.Length == 0)
                {
                    grSalItem.Rows[i].Visible = false;
                    foundGrRows = null;
                    foundRows = null;
                    continue;
                }
            }
            if (foundGrRows.Length > 0)
            {
                hfDT.Value = "S";
                grSalItem.Rows[i].Cells[2].Text = "Salary";
            }
            else
            {
                if (grSalItem.DataKeys[i].Values[1].ToString().Trim() == "1")
                {
                    hfDT.Value = "B";
                    grSalItem.Rows[i].Cells[2].Text = "Benefit";
                }
                else if (grSalItem.DataKeys[i].Values[1].ToString().Trim() == "-1")
                {
                    hfDT.Value = "D";
                    grSalItem.Rows[i].Cells[2].Text = "Deduction";
                }
            }
            foundGrRows = null;
            foundRows = null;
        }
    }
   
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        // Empty Seq Cell
        foreach (GridViewRow gRow in grSalItem.Rows)
        {
            CheckBox chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            TextBox txtSN = (TextBox)gRow.Cells[0].FindControl("txtSeqNo");
            if (chBox.Checked == true)
            {
                if (string.IsNullOrEmpty(txtSN.Text.Trim()) == true)
                {
                    lblMsg.Text = "Please enter a sequence no for salary item " + gRow.Cells[1].Text;
                    return;
                }
            }
        }
        // Duplicate Seq No
        foreach (GridViewRow gRow in grSalItem.Rows)
        {
            CheckBox chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            TextBox txtSN = (TextBox)gRow.Cells[0].FindControl("txtSeqNo");
            if (chBox.Checked == true)
            {
                foreach (GridViewRow gRow2 in grSalItem.Rows)
                {
                    CheckBox chBox2 = (CheckBox)gRow2.Cells[0].FindControl("chkBox");
                    TextBox txtSN2 = (TextBox)gRow2.Cells[0].FindControl("txtSeqNo");
                    if (chBox2.Checked == true)
                    {
                        if (gRow.DataItemIndex != gRow2.DataItemIndex)
                        {
                            if (txtSN.Text.Trim() == txtSN2.Text.Trim())
                            {
                                lblMsg.Text = "Duplicate sequence no exist between salary item " + gRow.Cells[1].Text + " and " + gRow2.Cells[1].Text;
                                return;
                            }
                        }
                    }
                }
            }
        }

        objPayRptMgr.InsertSalHeadWithSeqForReport(grSalItem, 1);

        StringBuilder sb = new StringBuilder();
        string strURL = "StaffSalaryAndITReport.aspx?params=" + ddlFiscalYear.SelectedValue.ToString().Trim() + ","
                                                                     + ddlLocation.SelectedValue.Trim() + ","
                                                                     + ddlFiscalYear.SelectedItem.Text.Trim() + ","
                                                                     + ddlMonth.SelectedValue.Trim();                   
                                                                                                                                            
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
