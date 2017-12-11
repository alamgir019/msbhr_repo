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

public partial class Payroll_HeadSetup_TotalSalaryHeadSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtTotalSalHead = new DataTable();
    DataTable dtSalaryHead = new DataTable();
    DataTable dtGrossSalHead = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtTotalSalHead.Rows.Clear();
            dtTotalSalHead.Dispose();

            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "");
        dtGrossSalHead=objPayrollMgr.SelectGrossSalHead(0);
        dtTotalSalHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);

        grTotalSalary.DataSource = dtSalaryHead;
        grTotalSalary.DataBind();
        DataRow[] foundRows;
        DataRow[] foundGrRows;
        DataRow[] foundRptRows;
        string strExpr = "";
        string strExprGross = "";
        for (int i = 0; i < grTotalSalary.Rows.Count; i++)
        {
            strExpr = "SHEADID=" + grTotalSalary.DataKeys[i].Values[0].ToString().Trim();
            strExprGross = "SHEADID=" + grTotalSalary.DataKeys[i].Values[0].ToString().Trim();
            foundRows = dtTotalSalHead.Select(strExpr);
            foundGrRows = dtGrossSalHead.Select(strExprGross);            

            HiddenField hfDT = (HiddenField)grTotalSalary.Rows[i].FindControl("hfDispType");
           
            
            if (foundRows.Length > 0)
            {
                CheckBox chBox = (CheckBox)grTotalSalary.Rows[i].FindControl("chkBox");
                chBox.Checked = true;

                TextBox txtSN = (TextBox)grTotalSalary.Rows[i].FindControl("txtSeqNo");
                txtSN.Text = foundRows[0]["SEQNO"].ToString().Trim();
                
            }
            if (foundGrRows.Length > 0)
            {
                hfDT.Value = "S";
                grTotalSalary.Rows[i].Cells[2].Text = "Salary";
            }
            else
            {
                if (grTotalSalary.DataKeys[i].Values[1].ToString().Trim() == "1")
                {
                    hfDT.Value = "B";
                    grTotalSalary.Rows[i].Cells[2].Text = "Benefit";
                }
                else if (grTotalSalary.DataKeys[i].Values[1].ToString().Trim() == "-1")
                {
                    hfDT.Value = "D";
                    grTotalSalary.Rows[i].Cells[2].Text = "Deduction";
                }
            }
            foundGrRows = null;
            foundRows = null;

        }
    }

    protected bool ValidateAndSave()
    {
        int i = 0;
        int j = 0;
        foreach (GridViewRow gRow1 in grTotalSalary.Rows)
        {
            
            TextBox txtSN1 = (TextBox)gRow1.Cells[3].FindControl("txtSeqNo");
           //txtSN1.Text = foundRows[0]["SEQNO"].ToString().Trim();
            if (string.IsNullOrEmpty(txtSN1.Text.Trim()) == false)
            {
                j = 0;
                foreach (GridViewRow gRow2 in grTotalSalary.Rows)
                {
                    if (i != j)
                    {
                        TextBox txtSN2 = (TextBox)gRow2.Cells[3].FindControl("txtSeqNo");
                        if (txtSN1.Text.Trim() == txtSN2.Text.Trim())
                        {
                            lblMsg.Text = "Duplicate sequence found at item [" + gRow1.Cells[1].Text.Trim() + "] and [" + gRow2.Cells[1].Text.Trim() + "]";
                            return false;
                        }
                    }
                    j++;
                }
            }
            i++;
        
        }
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int TotCount = 0;
        try
        {
            if (this.ValidateAndSave() == true)
            {
                foreach (GridViewRow gRow in grTotalSalary.Rows)
                {
                    CheckBox chBox = new CheckBox();
                    chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                    if (chBox.Checked == true)
                    {
                        TextBox txtBox = new TextBox();
                        txtBox = (TextBox)gRow.Cells[3].FindControl("txtSeqNo");
                        if (txtBox.Text == "")
                        {
                            lblMsg.Text = "Please enter seq. no for the checked salary head.";
                            return;
                        }                        
                        TotCount = TotCount + 1;
                    }
                }
                objPayrollMgr.InsertTotalSalHead(grTotalSalary, TotCount);
                lblMsg.Text = "Payroll Salary Items has been set";
                Common.EmptyTextBoxValues(this);
                //this.EntryMode(false);
                this.OpenRecord();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
}
