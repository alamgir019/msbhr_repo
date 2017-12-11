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

public partial class EIS_MedicalBenefit : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtMBenefit = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_Nil(objMasMgr.SelectMedNominee(0, txtEmpID.Text.ToString().Trim()), ddlMedNominee);              
            this.EntryMode(false); 
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
        }
        else
        {
            this.ClearControl();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtMedicalDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));    
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                lblProject.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblSubDept.Text = dRow["SubDeptName"].ToString().Trim();
                lblSuncode.Text = dRow["ClinicName"].ToString().Trim();
            }
            if (ddlFiscalYr.SelectedValue.ToString() != "")
            {
                lblMedicalBalance.Text = objEmpMgr.SelectMedicalHospitalBalance(txtEmpID.Text.Trim(), ddlFiscalYr.SelectedValue.ToString(), "M");
                lblHospitalBalance.Text = objEmpMgr.SelectMedicalHospitalBalance(txtEmpID.Text.Trim(), ddlFiscalYr.SelectedValue.ToString(), "H");
                this.OpenRecord();
            }           
        }
    }

    private void OpenRecord()
    {
        dtMBenefit = objEmpMgr.SelectMedicalBenefit(0, txtEmpID.Text.Trim(),ddlFiscalYr.SelectedValue.ToString()   );
        grMBenefit.DataSource = dtMBenefit;
        grMBenefit.DataBind();

        foreach (GridViewRow gRow in grMBenefit.Rows)
        {
            if (gRow.Cells[1].Text == "M")
                gRow.Cells[1].Text = "Medicine";
            else if (gRow.Cells[1].Text == "H")
                gRow.Cells[1].Text = "Hospitalization";
            else if (gRow.Cells[1].Text == "S")
                gRow.Cells[1].Text = "Special Hospitalization";

            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);  
        }

        Common.FillDropDownList_Nil(objMasMgr.SelectMedNominee(0, txtEmpID.Text.ToString().Trim()), ddlMedNominee);
        //txtLimit.Text = dtMBenefit.Rows[0]["Limit"].ToString(); 
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblCompany.Text = "";
        lblProject.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grMBenefit.DataSource = null;
        grMBenefit.DataBind();       
    }

    protected void ClearControl()
    {
        radMedicine.Checked = false;
        radHospital.Checked = false;
        chkIsSpHospital.Checked = false;       
        txtLimit.Text = "";
        txtReqAmount.Text = "";
        txtApproveAmount.Text = "";       
        txtRemarks.Text = "";
    } 

    protected bool ValidateAndSave()
    {
        try
        {
            if ((radMedicine.Checked == false) && (radHospital.Checked == false) && (radHospital.Checked == false))
            {
                lblMsg.Text = "Please select any one type.";
                radMedicine.Focus();
                return false;
            }
            if (txtReqAmount.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter requested amount.";
                txtReqAmount.Focus();
                return false;
            }

            if (txtApproveAmount.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter approve amount.";
                txtApproveAmount.Focus();
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
    private void SaveData(string strIsDelete)
    {
        try
        {
            string strBenefitType = "";
            string strMedicalDate = "";

            if (radMedicine.Checked == true)
                strBenefitType = "M";
            else if (radHospital.Checked == true)
                strBenefitType = "H";

            if (string.IsNullOrEmpty(txtMedicalDate.Text.Trim()) == false)
                strMedicalDate = Common.ReturnDate(txtMedicalDate.Text.Trim());

            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("MedicalBenefit", "BenefitId");

            objEmpMgr.InsertMedicalBenefit(hfId.Value.ToString(), txtEmpID.Text.Trim(), ddlFiscalYr.SelectedValue.ToString(), strBenefitType, chkIsSpHospital.Checked == true ? "Y" : "N",
                 strMedicalDate,txtLimit.Text.Trim(), txtReqAmount.Text.Trim(), txtApproveAmount.Text.Trim(), txtRemarks.Text.Trim(), ddlMedNominee.SelectedValue.ToString().Trim(),
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(), strIsDelete);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            this.OpenRecord();
            this.EntryMode(false);
            this.ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void grMBenefit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfId.Value = grMBenefit.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                if (grMBenefit.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "M")
                    radMedicine.Checked = true;
                else if (grMBenefit.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "H")
                    radHospital.Checked = true;

                if (Common.CheckNullString(grMBenefit.SelectedRow.Cells[2].Text.Trim())== "Y")
                    chkIsSpHospital.Checked = true;
                else
                    chkIsSpHospital.Checked = false ;

                txtMedicalDate.Text = Common.CheckNullString(grMBenefit.SelectedRow.Cells[3].Text.Trim());
                txtLimit.Text = Common.CheckNullString(grMBenefit.SelectedRow.Cells[4].Text.Trim());
                txtReqAmount.Text = Common.CheckNullString(grMBenefit.SelectedRow.Cells[5].Text.Trim());
                txtApproveAmount.Text = Common.CheckNullString(grMBenefit.SelectedRow.Cells[6].Text.Trim());
                txtRemarks.Text = Common.CheckNullString(grMBenefit.SelectedRow.Cells[7].Text.Trim());
                if (grMBenefit.DataKeys[_gridView.SelectedIndex].Values[2].ToString() != "")
                    ddlMedNominee.SelectedValue = grMBenefit.DataKeys[_gridView.SelectedIndex].Values[2].ToString();             
                this.EntryMode(true);
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }
    protected void radMedicine_CheckedChanged(object sender, EventArgs e)
    {
        if (radMedicine.Checked == true)
            txtLimit.Text = objEmpMgr.SelectMedicalCost(txtEmpID.Text.Trim(), ddlFiscalYr.SelectedValue.ToString(),"M");
        else
            txtLimit.Text = ""; 
    }
    protected void radHospital_CheckedChanged(object sender, EventArgs e)
    {
        if (radHospital.Checked == true)
            txtLimit.Text = objEmpMgr.SelectMedicalCost(txtEmpID.Text.Trim(), ddlFiscalYr.SelectedValue.ToString(),"H");
        else
            txtLimit.Text = ""; 
    }
}
