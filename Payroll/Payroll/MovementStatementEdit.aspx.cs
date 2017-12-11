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
using System.IO;

public partial class Payroll_Payroll_MovementStatementEdit : System.Web.UI.Page
{
    DataTable dtMovement = new DataTable();
    DataTable dtMovementDetls = new DataTable();
    DataTable dtValidation;
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');
            lblMonthBetween.Text = "";
            if (strVal[2] == "O")
                lblPaymentFor.Text = "Office: ";
            else if (strVal[2] == "B")
                lblPaymentFor.Text = "Bank: ";


            lblBank.Text = strVal[4];
            dtMovement = objPayRptMgr.GetSalaryMovementData(strVal[0], strVal[1], strVal[2], strVal[3], "Y");
            grMovement.DataSource = dtMovement;
            grMovement.DataBind();
            if (grMovement.Rows.Count > 0)
            {
                grMovement.FooterRow.Cells[0].Text = "Salary Increased/(Decreased) in " + Common.ReturnFullMonthName(strVal[0]) + strVal[1];
                grMovement.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                this.GetFooterDifference(grMovement, 1);
            }
            if (dtMovement.Rows.Count > 0)
            {
                lblMonthBetween.Text = dtMovement.Rows[0]["MONTHBETWEEN"].ToString().Trim();
                lblPreparedBy.Text = objEmpMgr.GetEmpName(dtMovement.Rows[0]["INSERTEDBY"].ToString().Trim());
            }


            dtMovementDetls = objPayRptMgr.GetSalaryMovementData(strVal[0], strVal[1], strVal[2], strVal[3], "N");
            grMovementDetls.DataSource = dtMovementDetls;
            grMovementDetls.DataBind();
            if (grMovementDetls.Rows.Count > 0)
            {
                this.GetFooterSummary(grMovementDetls, 3);
                grMovementDetls.FooterRow.Cells[2].Text = "Salary Increased/(Decreased) in " + Common.ReturnFullMonthName(strVal[0]) + strVal[1];
                grMovementDetls.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                btnUpdate.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
            }

            this.IniValidationDataTable(9);
            this.GetValidationResult();
        }
    }

    

    protected void GetFooterDifference(GridView grv, int inIndx)
    {
        decimal decSummary = 0;        
        for (int i = inIndx; i < grv.Columns.Count; i++)
        {
            decSummary = 0;
            decSummary = Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) - Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0);
            if (decSummary == 0)
                grv.FooterRow.Cells[i].Text = "-";
            else if (decSummary < 0)
                grv.FooterRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(decSummary)) + ")";
            else
                grv.FooterRow.Cells[i].Text = decSummary.ToString();
            
            if (Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) == 0)
                grv.Rows[1].Cells[i].Text = "-";
            else if (Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) < 0)
                grv.Rows[1].Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(grv.Rows[1].Cells[i].Text.Trim()))) + ")";

            if (Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0) == 0)
                 grv.Rows[0].Cells[i].Text = "-";
            else if (Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0) < 0)
                 grv.Rows[1].Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(grv.Rows[0].Cells[i].Text.Trim()))) + ")";
        }
    }

    protected void GetFooterSummary(GridView grv, int inIndx)
    {
        decimal decSummary = 0;
        for (int i = inIndx; i < grv.Columns.Count; i++)
        {
            decSummary = 0;
            foreach (GridViewRow gRow in grv.Rows)
            {
                decSummary = decSummary + Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0);
                if (Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0) == 0)
                    gRow.Cells[i].Text = "-";
                else if (Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0) < 0)
                    gRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(gRow.Cells[i].Text.Trim()))) + ")";
            }
            if (decSummary == 0)
                grv.FooterRow.Cells[i].Text = "-";
            else if (decSummary < 0)
                grv.FooterRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(decSummary)) + ")";
            else
                grv.FooterRow.Cells[i].Text = decSummary.ToString();

        }
    }

    

    protected void IniValidationDataTable(int inCol)
    {
        dtValidation = new DataTable();
        for (int i = 0; i < inCol; i++)
        {
            dtValidation.Columns.Add(i.ToString());
        }
        dtValidation.AcceptChanges();

        DataRow nRow = dtValidation.NewRow();
        dtValidation.Rows.Add(nRow);
        dtValidation.AcceptChanges();

        grValidation.DataSource = dtValidation;
        grValidation.DataBind();
    }

    protected void GetValidationResult()
    {
        grValidation.Rows[0].Cells[0].Text = "Validation Check  ";
        for (int i = 1; i < grValidation.Columns.Count; i++)
        {
            if (grMovementDetls.Rows.Count > 0)
            {
                if (grMovement.FooterRow.Cells[i].Text.Trim() == grMovementDetls.FooterRow.Cells[i + 2].Text.Trim())
                {
                    grValidation.Rows[0].Cells[i].Text = "TRUE";
                }
                else
                {
                    grValidation.Rows[0].Cells[i].Text = "FALSE";
                    //grValidation.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                grValidation.Rows[0].Cells[i].Text = "TRUE";
            }
            grValidation.Rows[0].Cells[i].HorizontalAlign = HorizontalAlign.Center;
            grValidation.Rows[0].Cells[i].Font.Bold = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Payroll_MovementStatement objMovMgr = new Payroll_MovementStatement();
        objMovMgr.UpdateMoveStatement(grMovementDetls, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Updated Successfully";
 
    }
   
}
