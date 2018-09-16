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

public partial class EIS_HRAction_Transition : System.Web.UI.Page
{
    DataTable dtEmpInfo = new DataTable();
    DataTable dtTrans = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectAction(0, "P"), ddlAction);
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlCompany);
            Common.FillDropDownList_Nil(objMasMgr.SelectClinic("Y"), ddlClinic);
            Common.FillDropDownList_Nil(objMasMgr.SelectProject(0), ddlProject);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartment(0), ddlDepartment);
            Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDepartment.SelectedValue)), ddlSubDept);            
            Common.FillDropDownList_Nil(objMasMgr.SelectDesignation(0), ddlDesignation);
            Common.FillDropDownList_Nil(objMasMgr.SelectGrade(0), ddlGrade);
            Common.FillDropDownList_Nil(objMasMgr.SelectSalaryLocation(0), ddlSalaryLoc);
            Common.FillDropDownList_Nil(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType);
            hfIsUpdate.Value = "N";
            this.EntryMode(false);
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
            lblMsg.Text = "";
            this.EntryMode(false);
        }
    }
    private void FillEmpInfo(string EmpId)
    {
        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());

        if (dtEmpInfo.Rows.Count > 0)
        {
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                foreach (DataRow row in dtEmpInfo.Rows)
                {
                    lblName.Text = row["FullName"].ToString().Trim();
                    lblName.ToolTip = Common.DisplayDate(row["DOB"].ToString().Trim());
                    lblDesignation.Text = row["DesigName"].ToString().Trim();
                    lblDesignation.ToolTip = row["EmpTypeId"].ToString().Trim();
                    lblSector.Text = row["ProjectName"].ToString().Trim();
                    lblDept.Text = row["DeptName"].ToString().Trim();
                    lblJoinDate.Text = Common.DisplayDate(row["JoiningDate"].ToString().Trim());
                    ddlCompany.SelectedValue = Common.RetrieveddL(ddlCompany, row["DivisionId"].ToString(), "99999");
                    ddlClinic.SelectedValue = Common.RetrieveddL(ddlClinic, row["ClinicId"].ToString(), "99999");
                    ddlProject.SelectedValue = Common.RetrieveddL(ddlProject, row["ProjectId"].ToString(), "99999");
                    ddlDepartment.SelectedValue = Common.RetrieveddL(ddlDepartment, row["DeptId"].ToString(), "99999");
                    Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDepartment.SelectedValue)), ddlSubDept);
                    ddlSubDept.SelectedValue = Common.RetrieveddL(ddlSubDept, row["SubDeptId"].ToString(), "99999");
                    ddlDesignation.SelectedValue = Common.RetrieveddL(ddlDesignation, row["DesigId"].ToString(), "99999");
                    ddlGrade.SelectedValue = Common.RetrieveddL(ddlGrade, row["GradeId"].ToString(), "99999");
                    ddlSalaryLoc.SelectedValue = Common.RetrieveddL(ddlSalaryLoc, row["SalLocId"].ToString(), "99999");
                    ddlEmpType.SelectedValue = Common.RetrieveddL(ddlEmpType, row["EmpTypeID"].ToString(), "99999");
                    txtNewGrossSalary.Text = string.IsNullOrEmpty(row["GrossSalary"].ToString()) == true ? "" : row["GrossSalary"].ToString();
                    txtNewGrossSalary.ToolTip = row["SalPakId"].ToString().Trim();

                    hfDivision.Value = ddlCompany.SelectedValue.ToString();
                    hfClinic.Value = ddlClinic.SelectedValue.ToString();
                    hfProject.Value = ddlProject.SelectedValue.ToString();
                    hfDept.Value = ddlDepartment.SelectedValue.ToString();
                    hfSubDept.Value = ddlSubDept.SelectedValue.ToString();
                    hfDesig.Value = ddlDesignation.SelectedValue.ToString();
                    hfGrade.Value = ddlGrade.SelectedValue.ToString();
                    hfSalLoc.Value = ddlSalaryLoc.SelectedValue.ToString();
                    hfEmpType.Value = ddlEmpType.SelectedValue.ToString();
                    hfGrossSalary.Value = txtNewGrossSalary.Text.Trim(); 
                }
            }
        }
        else
        {
            lblMsg.Text = "Employee code is not valid.";
            txtEmpID.Text = "";
            lblName.Text = "";
            lblDesignation.Text = "";
            lblDept.Text = "";
            lblJoinDate.Text = "";
            return;
        }
    }

    private void OpenRecord()
    {
        grEmpTransition.Dispose();
        dtTrans = objEmpInfoMgr.SelectEmpTransitionLog(txtEmpID.Text.Trim());
        grEmpTransition.DataSource = dtTrans;
        grEmpTransition.DataBind();
        if (grEmpTransition.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grEmpTransition.Rows)
            {
                if (gRow.Cells[1].Text == "P")
                    gRow.Cells[1].Text = "Promotion";
                else if (gRow.Cells[1].Text == "T")
                    gRow.Cells[1].Text = "Transfer";
                else if (gRow.Cells[1].Text == "C")
                    gRow.Cells[1].Text = "Change In Status";
                else if (gRow.Cells[1].Text == "E")
                    gRow.Cells[1].Text = "Equity Adjustment";

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[10].Text)) == false)
                    gRow.Cells[10].Text = Common.DisplayDate(gRow.Cells[10].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[11].Text)) == false)
                    gRow.Cells[11].Text = Common.DisplayDate(gRow.Cells[11].Text);
            }
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryText();
        this.EntryMode(false);
        this.ClearControls();
        lblMsg.Text = "";
    }

    protected void EntryText()
    {
        Common.EmptyTextBoxValues(this, txtEmpID);
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            lblMsg.Text = "";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
        }
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        lblJoinDate.ToolTip = "";

        grEmpTransition.DataSource = null;
        grEmpTransition.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    protected bool ValidateAndSave()
    {
        if (txtEmpID.Text == "")
        {
            lblMsg.Text = "Please select Emp code.";
            txtEmpID.Focus();
            return false;
        }
        if ((radPromotion.Checked == false) && (radTrans.Checked == false) && (radStatus.Checked == false) )
        {
            lblMsg.Text = "Please select any transition type.";
            radPromotion.Focus();
            return false;
        }
        if (ddlAction.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select an action from the list.";
            ddlAction.Focus();
            return false;
        }
        if (txtEffDate.Text == "")
        {
            lblMsg.Text = "Please select a valid effective date.";
            txtEffDate.Focus();
            return false;
        }
        return true;
    }

    private void SaveData()
    {
        dsPayroll_SalaryPackage objDs = new dsPayroll_SalaryPackage();
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

        string strTranType = "";
        if (hfIsUpdate.Value == "Y")
            hfId.Value = hfId.Value;
        else
            hfId.Value = Common.getMaxId("EmpTransitionLog", "TransId");

        string strEntryDate = "";
        string strEffDate = "";
        string strNextIncDate = "";
        string strGradeChDate = "";
        string strRetirementDate = "";

        if (string.IsNullOrEmpty(txtEntryDate.Text.Trim()) == false)
            strEntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());

        if (string.IsNullOrEmpty(txtEffDate.Text.Trim()) == false)
            strEffDate = Common.ReturnDate(txtEffDate.Text.Trim());

        if (string.IsNullOrEmpty(txtNextIncDate.Text.Trim()) == false)
            strNextIncDate = Common.ReturnDate(txtNextIncDate.Text.Trim());

        if (string.IsNullOrEmpty(txtGradeChangeDate.Text.Trim()) == false)
            strGradeChDate = Common.ReturnDate(txtGradeChangeDate.Text.Trim());

        char[] splitter = { '/' };
        string[] arinfo = Common.str_split(lblName.ToolTip.ToString(), splitter);
        int iBirthYear = 0;
        if (arinfo.Length == 3)
        {
            iBirthYear = Convert.ToInt16(arinfo[2]);
            iBirthYear = iBirthYear + 60;

            strRetirementDate = iBirthYear + "/" + arinfo[1] + "/" + arinfo[0];
            arinfo = null;
        }

        if (radPromotion.Checked == true)
            strTranType = "P";
        else if (radTrans.Checked == true)
            strTranType = "T";
        else if (radStatus.Checked == true)
            strTranType = "C";

        DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0", ddlEmpType.SelectedValue.ToString()   );
        DataRow[] foundPlcRow;
        foundPlcRow = null;
        decimal dclNewBasicSal = 0;
        //Basic
        foundPlcRow = dtBfPlc.Select("SHEADID=1");
        if (foundPlcRow.Length > 0)
        {
            DataRow nRow1 = objDs.dtSalPackUpdate.NewRow();
            nRow1["SHEADID"] = 1;
            nRow1["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
            dclNewBasicSal = Convert.ToDecimal(nRow1["PAYAMT"].ToString());
            objDs.dtSalPackUpdate.Rows.Add(nRow1);
        }
        //House Rent
        foundPlcRow = dtBfPlc.Select("SHEADID=2");
        if (foundPlcRow.Length > 0)
        {
            DataRow nRow2 = objDs.dtSalPackUpdate.NewRow();
            nRow2["SHEADID"] = 2;
            nRow2["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
            objDs.dtSalPackUpdate.Rows.Add(nRow2);
        }

        //Medical
        foundPlcRow = dtBfPlc.Select("SHEADID=3");
        if (foundPlcRow.Length > 0)
        {
            DataRow nRow3 = objDs.dtSalPackUpdate.NewRow();
            nRow3["SHEADID"] = 3;
            nRow3["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
            objDs.dtSalPackUpdate.Rows.Add(nRow3);
        }
        if (ddlEmpType.SelectedValue.ToString() == "1")
        {
            //PF Allowance 
            foundPlcRow = dtBfPlc.Select("SHEADID=8");
            if (dtBfPlc.Rows.Count > 0)
            {
                DataRow nRow4 = objDs.dtSalPackUpdate.NewRow();
                nRow4["SHEADID"] = 8;
                nRow4["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(dclNewBasicSal.ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                objDs.dtSalPackUpdate.Rows.Add(nRow4);
            }

            objDs.dtSalPackUpdate.AcceptChanges();
        }

        clsEmpTransition objEmpTrans = new clsEmpTransition(hfId.Value.ToString(), txtEmpID.Text.Trim(), strEntryDate, strTranType, ddlAction.SelectedValue.ToString(),
            ddlCompany.SelectedValue.ToString(), ddlClinic.SelectedValue.ToString(), ddlProject.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(),
            ddlSubDept.SelectedValue.ToString(), ddlDesignation.SelectedValue.ToString(), ddlGrade.SelectedValue.ToString(), ddlSalaryLoc.SelectedValue.ToString(),
            ddlEmpType.SelectedValue.ToString(), txtNewGrossSalary.Text.Trim(),
            strEffDate, strNextIncDate, strGradeChDate,txtRemarks.Text.Trim(), (chkIsTAApplicable.Checked == true ? "Y" : "N"),
            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        objEmpInfoMgr.InsertEmpTransitionLog(objEmpTrans, hfDivision.Value.ToString(), hfClinic.Value.ToString(), hfProject.Value.ToString(), hfDept.Value.ToString(), hfSubDept.Value.ToString(), hfDesig.Value.ToString(),
              hfGrade.Value.ToString(), hfSalLoc.Value.ToString(),hfEmpType.Value.ToString(),   txtNewGrossSalary.Text.Trim(), strEffDate, strNextIncDate, strGradeChDate, strRetirementDate, txtRemarks.Text.Trim(), hfIsUpdate.Value,
             txtNewGrossSalary.ToolTip.ToString(), hfGrossSalary.Value.ToString(),  objDs.dtSalPackUpdate);
        lblMsg.Text = "Record Saved Successfully";

        this.EntryText();
        this.EntryMode(false);
        this.ClearControls();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDepartment.SelectedValue)), ddlSubDept);
    }
}
