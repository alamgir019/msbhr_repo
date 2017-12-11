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

public partial class EIS_COLAAdjustment : System.Web.UI.Page
{
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    Payroll_PaySlipOptionMgr objPayOptMgr = new Payroll_PaySlipOptionMgr();
    Payroll_MasterMgr objPayMasMgr = new Payroll_MasterMgr();

    DataTable dtEmpInfo = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, true);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (ddlEmpType.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select employee from the drop down list.";
            ddlEmpType.Focus();
            return;
        }
        if ((txtCOLAPercent.Text.Trim() == "") || (Common.ReturnZeroForNull(txtCOLAPercent.Text.Trim()) == "0") || ((txtCOLAPercent.Text.Trim())=="-"))
        {
            lblMsg.Text = "Please enter COLA percent.";
            txtCOLAPercent.Focus();
            return;
        }
        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoEmpTypeWs("",ddlEmpType.SelectedValue.ToString());
        grEmpList.DataSource = dtEmpInfo;
        grEmpList.DataBind();  
        this.FormateGrid();
        lblRecordCount.Text = grEmpList.Rows.Count.ToString();
        dtEmpInfo.Rows.Clear();
        dtEmpInfo.Dispose(); 
    }

    private void FormateGrid()
    {
        int i = 1;
        decimal dclNewBasic = 0;
        decimal dclHR=0;
        decimal dclPF=0;

        DataTable dtBfPlc = objPayOptMgr.SelectPayrollBenefitsPolicyData("0", ddlEmpType.SelectedValue.ToString());
        DataRow[] foundPlcRow;
        foundPlcRow = null;

        foreach (GridViewRow gRow in grEmpList.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            
            dclNewBasic =Convert.ToDecimal(gRow.Cells[3].Text) + (Convert.ToDecimal(gRow.Cells[3].Text) * Convert.ToDecimal(txtCOLAPercent.Text.Trim())) / 100;
            gRow.Cells[4].Text =  Convert.ToString(Common.RoundDecimal(dclNewBasic.ToString(),0));

            //House Rent
            foundPlcRow = dtBfPlc.Select("SHEADID=2");
            if (foundPlcRow.Length > 0)
            {
                dclHR = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(gRow.Cells[5].Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));
            }
            dclHR = (dclNewBasic * Convert.ToDecimal(dtBfPlc.Rows[0]["VALUE"]) / 100);
            gRow.Cells[5].Text = Convert.ToString(Common.RoundDecimal(dclHR.ToString() ,0) );

            //PF Allowance 
            if (string.IsNullOrEmpty(grEmpList.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim())==false )
            {
                foundPlcRow = null;

                foundPlcRow = dtBfPlc.Select("SHEADID=8");
                if (foundPlcRow.Length > 0)
                {
                    dclPF = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(gRow.Cells[4].Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));
                }
                gRow.Cells[6].Text = Convert.ToString(Common.RoundDecimal(dclPF.ToString(), 0));
            }
            else
                gRow.Cells[6].Text = "0";
            gRow.Cells[7].Text = txtCOLAPercent.Text.Trim();
            i++;
        }
    }

    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }
    private bool ValidateAndSave()
    {
        try
        {
            if (objPayMasMgr.CheckForMultipleCLOAEntry(txtEffDate.Text.Trim()) == true)
            {
                lblMsg.Text = "COLA has already adjusted for '" + txtEffDate.Text.Trim() + "' date's month & year" ;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData()
    {
        objPayMasMgr.InsertCOLAAdjust(grEmpList, Session["FISCALYRID"].ToString(), txtCOLAPercent.Text.Trim(),
            Common.ReturnDate(txtEffDate.Text.Trim()),Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "COLAAdjust");

        lblMsg.Text = "COLA adjustment has been done successfully.";
        this.EntryMode();
    }

    protected void EntryMode()
    {

    }    
}
