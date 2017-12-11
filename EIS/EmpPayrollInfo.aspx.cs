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

public partial class Back_Office_EmpPayrollInfo : System.Web.UI.Page
{  
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    Payroll_MasterMgr objSalaryManager = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objPayOptMgr = new Payroll_PaySlipOptionMgr();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtEmpAction = new DataTable();
    DataTable dtTrainService = new DataTable();
    DataTable dtTaskPermission = new DataTable();

    decimal minSal = 0;
    decimal maxSal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlCompany);
            Common.FillDropDownList_Nil(objMasMgr.SelectProject(), ddlProject);
            Common.FillDropDownList_Nil(objMasMgr.SelectProjectOffice(0), ddlProjectOffice);
            Common.FillDropDownList_Nil(objMasMgr.SelectClinic(), ddlClinic);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartment(0), ddlDept);
            Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDept.SelectedValue)), ddlSubDept);            
            Common.FillDropDownList_Nil(objMasMgr.SelectGrade(0), ddlGrade);
            Common.FillDropDownList_Nil(objMasMgr.SelectGradeWsDesignation(Convert.ToInt32(ddlGrade.SelectedValue)), ddlDesignation);

            Common.FillDropDownList_Nil(objMasMgr.SelectSalaryLocation(0), ddlSalaryLoc);
            Common.FillDropDownList_Nil(objMasMgr.SelectLocationCategory(0), ddlLocCategory);
            Common.FillDropDownList_Nil(objMasMgr.SelectLeavePakMst(0), ddlLeavePackage);
            Common.FillDropDownList_Nil(objMasMgr.SelectWeekendPolicy(0), ddlWeekend);
            Common.FillDropDownList_Nil(objMasMgr.SelectAttendancePolicy(0), ddlAttndPolicy);
            Common.FillDropDownList_Nil(objMasMgr.SelectAction(0, "S"), ddlSepType);
            Common.FillDropDownList_Nil(objSalaryManager.SelectSalaryPackage(0), ddlSalaryPak);
            Common.FillDropDownList_Nil(objPayOptMgr.GetMonthlyPayrollCycleData(), ddlMPC);

            Common.FillDropDownList(objEmpInfoMgr.SelectSupervisor(), ddlSupervisor, "EMPNAME", "EMPID", true, "Nil");
            Common.FillDropDownList_Nil(objMasMgr.SelectRegion(), ddlRegion);
            Common.FillDropDownList(objEmpInfoMgr.SelectBankList(), ddlBankName, "BankName", "BankCode", true, "Nil");
            //this.GetTaskPermissionContract();
            DataTable dtTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "302", "T102");
            Common.FillDropDownList_Nil(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType);

            Common.FillDropDownList_Nil(objMasMgr.SelectTaxRegion(0), ddlTaxRegion);

            if (string.IsNullOrEmpty(Session["HREMPID"].ToString()) == false)
            {
                txtEmpID.Text = Session["HREMPID"].ToString().Trim();
                this.FillEmpInfo(Session["HREMPID"].ToString().Trim());
            }
        }
        //this.GetViewPermission();
    }

    protected void cmdFind_Click(object sender, EventArgs e)
    {
        this.FillEmpInfo("");
      
    }

    private void FillEmpInfo(string strEmpId)
    {
        if (txtEmpID.Text.Trim() != "")
        {
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHR(txtEmpID.Text.Trim());
            if (dtEmpInfo.Rows.Count == 0)
            {
                this.RefreshControl();               
                lblMsg.Text = "Invalid Employee Id .";               
            }
            else
            {
                lblMsg.Text = "";
                chkIsNew.Checked = false;
                foreach (DataRow dRow in dtEmpInfo.Rows)
                {
                    txtEmpFullName.Text = dRow["FullName"].ToString().Trim();
                    txtEmpFullName.ToolTip = Common.DisplayDate(dRow["DOB"].ToString().Trim());
                    txtBankAccNo.Text = dRow["BankAccNo"].ToString().Trim();
                    txtBasicSalary.Text = dRow["BasicSalary"].ToString().Trim();
                    txtGrossSalary.Text = dRow["GrossSalary"].ToString().Trim();
                    txtConfirmDate.Text = string.IsNullOrEmpty(dRow["ConfirmationDate"].ToString()) == false ? Common.DisplayDate(dRow["ConfirmationDate"].ToString()) : "";
                    txtContractExpDate.Text = string.IsNullOrEmpty(dRow["ContractEndDate"].ToString()) == false ? Common.DisplayDate(dRow["ContractEndDate"].ToString()) : "";
                    txtContractInterval.Text = dRow["ContractInterval"].ToString().Trim();
                    txtContractPurpose.Text = dRow["ContractPurpose"].ToString().Trim();
                    txtDateInGrade.Text = string.IsNullOrEmpty(dRow["DateInGrade"].ToString()) == false ? Common.DisplayDate(dRow["DateInGrade"].ToString()) : "";
                    txtDateInPosition.Text = string.IsNullOrEmpty(dRow["DateInPosition"].ToString()) == false ? Common.DisplayDate(dRow["DateInPosition"].ToString()) : "";
                    txtJoiningDate.Text = string.IsNullOrEmpty(dRow["JoiningDate"].ToString()) == false ? Common.DisplayDate(dRow["JoiningDate"].ToString()) : "";
                    txtOtherBenefit.Text = dRow["OtherBenefit"].ToString().Trim();
                    txtPostingDate.Text = string.IsNullOrEmpty(dRow["PostingDate"].ToString()) == false ? Common.DisplayDate(dRow["PostingDate"].ToString()) : "";
                    txtProbationPeriod.Text = dRow["ProbationPeriod"].ToString().Trim();
                    txtRemarks.Text = dRow["Remarks"].ToString().Trim();
                    txtRetirementDate.Text = string.IsNullOrEmpty(dRow["RetirementDate"].ToString()) == false ? Common.DisplayDate(dRow["RetirementDate"].ToString()) : "";
                    if (string.IsNullOrEmpty(txtRetirementDate.Text) == true )
                    {
                        char[] splitter = { '/' };
                        string[] arinfo = Common.str_split(txtEmpFullName.ToolTip.ToString(), splitter);
                        int iBirthYear = 0;
                        string strRetirementDate = "";
                        if (arinfo.Length == 3)
                        {
                            iBirthYear = Convert.ToInt16(arinfo[2]);
                            iBirthYear = iBirthYear + 60;

                            strRetirementDate = iBirthYear + "/" + arinfo[1] + "/" + arinfo[0];
                            arinfo = null;
                        }
                        txtRetirementDate.Text = Common.DisplayDate(strRetirementDate);  
                    }
                    txtSeparationDate.Text = string.IsNullOrEmpty(dRow["SeparateDate"].ToString()) == false ? Common.DisplayDate(dRow["SeparateDate"].ToString()) : "";
                    txtSeparationReason.Text = dRow["SeparateReason"].ToString().Trim();
                    txtSeveranceId.Text = dRow["SeveranceId"].ToString().Trim();
                    txtSeveranceReason.Text = dRow["SeveranceReason"].ToString().Trim();
                    txtWorkArea.Text = dRow["WorkArea"].ToString().Trim();
                    txtActionDate.Text = string.IsNullOrEmpty(dRow["ActionDate"].ToString()) == false ? Common.DisplayDate(dRow["ActionDate"].ToString()) : "";
                    txtActionName.Text = dRow["ActionName"].ToString().Trim();
                    txtWorkingDays.Text = dRow["WorkingDays"].ToString().Trim();

                    lnkEmpCV.Text = dRow["UploadCV"].ToString().Trim();
                    lnkEmpSignature.Text = dRow["EmpSignature"].ToString().Trim();
                    lnkEmpDocument.Text = dRow["UploadDocument"].ToString().Trim();

                    //ddlBonusPak.SelectedValue = Common.RetrieveddL(ddlBonusPak, dRow["BonusPakId"].ToString(), "-1");
                    ddlBankName.SelectedValue = Common.RetrieveddL(ddlBankName, dRow["BankCode"].ToString(), "-1");
                    Common.FillDropDownList(objEmpInfoMgr.SelectBranchList(ddlBankName.SelectedValue.ToString()), ddlBranchCode, "BranchName", "Routingno", true, "Nil");
                    ddlBranchCode.SelectedValue = Common.RetrieveddL(ddlBranchCode, dRow["RoutingNo"].ToString(), "-1");
                    lblRoutingNo.Text = dRow["RoutingNo"].ToString().Trim();
                    ddlCompany.SelectedValue = Common.RetrieveddL(ddlCompany, dRow["DivisionId"].ToString(), "99999");
                    ddlClinic.SelectedValue = Common.RetrieveddL(ddlClinic, dRow["ClinicId"].ToString(), "99999");
                    ddlProject.SelectedValue = Common.RetrieveddL(ddlProject, dRow["ProjectId"].ToString(), "99999");
                    ddlProjectOffice.SelectedValue = Common.RetrieveddL(ddlProjectOffice, dRow["ProjOfficeId"].ToString(), "99999");                 

                    ddlDept.SelectedValue = Common.RetrieveddL(ddlDept, dRow["DeptId"].ToString(), "99999");
                    Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDept.SelectedValue)), ddlSubDept);
                    ddlSubDept.SelectedValue = Common.RetrieveddL(ddlSubDept, dRow["SubDeptId"].ToString(), "99999");
                    
                    ddlGrade.SelectedValue = Common.RetrieveddL(ddlGrade, dRow["GradeId"].ToString(), "99999");
                    Common.FillDropDownList_Nil(objMasMgr.SelectGradeWsDesignation(Convert.ToInt32(ddlGrade.SelectedValue)), ddlDesignation);
                    ddlDesignation.SelectedValue = Common.RetrieveddL(ddlDesignation, dRow["DesigId"].ToString(), "99999");
                    
                    //Common.FillDropDownList_Nil(objMasMgr.SelectDesigWiseJobTitle2(Convert.ToInt32(ddlDesignation.SelectedValue)), ddlJobTitle);
                    //ddlJobTitle.SelectedValue = Common.RetrieveddL(ddlJobTitle, dRow["JobTitleId"].ToString(), "99999");

                    ddlEmpType.SelectedValue = Common.RetrieveddL(ddlEmpType, dRow["EmpTypeID"].ToString(), "99999");
                    this.ConfirmValidation();
                    //ddlEmpNature.SelectedValue = Common.RetrieveddL(ddlEmpNature, dRow["EmpNatureID"].ToString(), "99999");
                    
                   
                    ddlLeavePackage.SelectedValue = Common.RetrieveddL(ddlLeavePackage, dRow["LeavePakId"].ToString(), "99999");
                    hfLPakId.Value = ddlLeavePackage.SelectedValue.ToString();
                    //hfJoiningDate.Value = Common.DisplayDate(txtJoiningDate.Text);
                    ddlMPC.SelectedValue = Common.RetrieveddL(ddlMPC, dRow["MPCId"].ToString(), "99999");
                    //ddlPosByFunction.SelectedValue = Common.RetrieveddL(ddlPosByFunction, dRow["PosFuncId"].ToString(), "99999");

                    //ddlPostDivision.SelectedValue = Common.RetrieveddL(ddlPostDivision, dRow["PostingDivId"].ToString(), "99999");
                    //Common.FillDropDownList_Nil(objMasMgr.SelectDivisionWiseDistrict2(Convert.ToInt32(ddlPostDivision.SelectedValue)), ddlPostDistrict);
                    //ddlPostDistrict.SelectedValue = Common.RetrieveddL(ddlPostDistrict, dRow["PostingDistId"].ToString(), "99999");

                    //ddlPostingPlace.SelectedValue = Common.RetrieveddL(ddlPostingPlace, dRow["PostingPlaceId"].ToString(), "99999");
                    ddlSalaryLoc.SelectedValue = Common.RetrieveddL(ddlSalaryLoc, dRow["SalLocId"].ToString(), "99999");
                    ddlLocCategory.SelectedValue = Common.RetrieveddL(ddlSalaryLoc, dRow["LocCatId"].ToString(), "99999");
                    ddlSalaryPak.SelectedValue = Common.RetrieveddL(ddlSalaryPak, dRow["SalPakId"].ToString(), "99999");
                    //ddlSalarySubLoc.SelectedValue = Common.RetrieveddL(ddlSalarySubLoc, dRow["SalSubLocId"].ToString(), "99999");

                    ddlSepType.SelectedValue = Common.RetrieveddL(ddlSepType, dRow["SeparateTypeId"].ToString(), "99999");
                    ddlStatus.SelectedValue = Common.RetrieveddL(ddlStatus, dRow["EmpStatus"].ToString(), "99999");
                    chkIsNotRehire.Checked = dRow["IsNotRehirable"].ToString() == "Y" ? true : false;
                    txtNotRehireReason.Text = dRow["NotRehireReason"].ToString().Trim();

                    ddlSupervisor.SelectedValue = Common.RetrieveddL(ddlSupervisor, dRow["SupervisorId"].ToString(), "-1");
                    ddlRegion.SelectedValue = Common.RetrieveddL(ddlRegion, dRow["RegionId"].ToString(), "99999");
                    //ddlUnit.SelectedValue = Common.RetrieveddL(ddlUnit, dRow["UnitId"].ToString(), "99999");
                    ddlWeekend.SelectedValue = Common.RetrieveddL(ddlWeekend, dRow["WeekendId"].ToString(), "99999");
                    ddlAttndPolicy.SelectedValue = Common.RetrieveddL(ddlWeekend, dRow["AttnPolicyID"].ToString(), "99999");

                    chkIsChildEdu.Checked = dRow["IsChildEduAllow"].ToString() == "Y" ? true : false;
                    chkIsMedicalEntitle.Checked = dRow["IsMedicalEntmnt"].ToString() == "Y" ? true : false;
                    chkIsOTEntitle.Checked = dRow["IsOTEntmnt"].ToString() == "Y" ? true : false;
                    chkIsPayrollStaff.Checked = dRow["IsPayrollStaff"].ToString() == "Y" ? true : false;
                    
                    chkIsSeveranceBenefit.Checked = dRow["IsSeveranceBenefit"].ToString() == "Y" ? true : false;
                    chkWorkArea.Checked = dRow["WorkAreaType"].ToString() == "Y" ? true : false;
                    ddlTaxRegion.SelectedValue = Common.RetrieveddL(ddlTaxRegion, dRow["TaxRegionId"].ToString(), "99999");

                    dtEmpAction = objEmpInfoMgr.SelectEmpActionLog(txtEmpID.Text.Trim());
                    if (dtEmpAction.Rows.Count > 0)
                    {
                        grEmpAction.DataSource = dtEmpAction;
                        grEmpAction.DataBind();

                        foreach (GridViewRow gRow in grEmpAction.Rows)
                        {
                            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
                        }
                    }
                    if (Common.CheckNullString(dRow["EmpStatus"].ToString()) == "I")
                    {
                        lblMsg.Text = "This Staff Has Been Separated.";
                        txtSeparationReason.ForeColor = System.Drawing.Color.Red;
                        btnSave.Enabled = false;
                    }
                    //else if (Session["ISPAYADMIN"].ToString() == "N")
                    //{
                    //    lblMsg.Text = "";
                    //    btnSave.Enabled = true;
                    //}
                    //else
                    //{
                    //    lblMsg.Text = "";
                    //    btnSave.Enabled = false;
                    //}

                    if ((Common.CheckNullString(dRow["EmpStatus"].ToString()) == "I") && (chkIsNotRehire.Checked == true))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        txtNotRehireReason.ForeColor = System.Drawing.Color.Red;
                    }

                    txtAsset.Text = dRow["Asset"].ToString().Trim();
                }
            }
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        this.SaveData();
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
        try
        {
            if (txtEmpFullName.Text == "")
            {
                lblMsg.Text = "You have to press find button with this EmpID";
                cmdFind.Focus();
                return false;
            }
            if (ddlEmpType.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select employee type.";
                ddlEmpType.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtWorkingDays.Text.Trim()) == true)
            {
                lblMsg.Text = "Please Enter Working Day.";
                txtWorkingDays.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtGrossSalary.Text.Trim()) == true)
            {
                lblMsg.Text = "Please Enter Gross Salary.";
                txtGrossSalary.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBasicSalary.Text.Trim()) == true)
            {
                lblMsg.Text = "Please press on Calculate Basic button to get basic salary.";
                cmdCalBasic.Focus(); 
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            lblMsg.Text = ex.Message;
            throw (ex);
        }
    }

    public void GetBasicSalaryRange(Int32 grdId)
    {
        DataTable dtGradeLevel = new DataTable();
        dtGradeLevel = objMasMgr.Select_GradeLevel_MinMaxSal(grdId);
        if (dtGradeLevel.Rows.Count > 0)
        {
            foreach (DataRow dr in dtGradeLevel.Rows)
            {
                minSal = Convert.ToDecimal(dr["Min"].ToString().Trim());
                maxSal = Convert.ToDecimal(dr["Max"].ToString().Trim());              
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.RefreshControl();
    }

    protected void RefreshControl()
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        grEmpAction.DataSource = null;
        grEmpAction.DataBind();
        lblMsg.Text = "";
        lblRoutingNo.Text = "";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            if (string.IsNullOrEmpty(txtEmpID.Text) == false)
            {
                this.SaveData();
            }
            else
            {
                lblMsg.Text = "Insert EmpID to Delete.";
            }
        }
        this.EntryMode(false);
    }

    private clsEmpInfoHr BindObject()
    {
        string strJoinDate = "";
        string strConfirmDate = "";
        string strContractExpDate = "";
        string strDateInGrade = "";
        string strDateInPosition = "";
        string strPostingDate = "";
        string strSeparationDate = "";
        string strRetirementDate = "";
        string strServiceStart = "";
        string strServiceEnd = "";

        if (string.IsNullOrEmpty(txtJoiningDate.Text.Trim()) == false)
            strJoinDate = Common.ReturnDate(txtJoiningDate.Text.Trim());

        if (string.IsNullOrEmpty(txtConfirmDate.Text.Trim()) == false)
            strConfirmDate = Common.ReturnDate(txtConfirmDate.Text.Trim());

        if (string.IsNullOrEmpty(txtContractExpDate.Text.Trim()) == false)
            strContractExpDate = Common.ReturnDate(txtContractExpDate.Text.Trim());

        if (string.IsNullOrEmpty(txtDateInGrade.Text.Trim()) == false)
            strDateInGrade = Common.ReturnDate(txtDateInGrade.Text.Trim());

        if (string.IsNullOrEmpty(txtDateInPosition.Text.Trim()) == false)
            strDateInPosition = Common.ReturnDate(txtDateInPosition.Text.Trim());

        if (string.IsNullOrEmpty(txtPostingDate.Text.Trim()) == false)
            strPostingDate = Common.ReturnDate(txtPostingDate.Text.Trim());

        if (string.IsNullOrEmpty(txtSeparationDate.Text.Trim()) == false)
            strSeparationDate = Common.ReturnDate(txtSeparationDate.Text.Trim());

        if (string.IsNullOrEmpty(txtRetirementDate.Text.Trim()) == false)
            strRetirementDate = Common.ReturnDate(txtRetirementDate.Text.Trim());

        clsEmpInfoHr obj = new clsEmpInfoHr();
        obj.ActionDate = txtActionDate.Text.Trim();
        obj.ActionName = txtActionName.Text.Trim();
        obj.BankAccNo = txtBankAccNo.Text.Trim();
        obj.BankCode = ddlBankName.SelectedValue.ToString();
        obj.BasicSalary = txtBasicSalary.Text.Trim();
        obj.GrossSalary = txtGrossSalary.Text.Trim();
        obj.ConfirmationDate = strConfirmDate;
        obj.ContractEndDate = strContractExpDate;
        obj.ContractInterval = txtContractInterval.Text.Trim();
        obj.ContractPurpose = txtContractPurpose.Text.Trim();
        obj.DateInGrade = strDateInGrade;
        obj.DateInPosition = strDateInPosition;
        obj.DeptId = ddlDept.SelectedValue.ToString();
        obj.SubDeptId = ddlSubDept.SelectedValue.ToString();
        obj.DesigId = ddlDesignation.SelectedValue.ToString();
        obj.EmpId = txtEmpID.Text.Trim();

        if (ddlSepType.SelectedIndex == 0)
            obj.EmpStatus = ddlStatus.SelectedValue.ToString();
        else
            obj.EmpStatus = "I";

        obj.EmpTypeID = ddlEmpType.SelectedValue.ToString();
        //obj.EmpNatureID = ddlEmpNature.SelectedValue.ToString();
        obj.GradeId = ddlGrade.SelectedValue.ToString();
      
        obj.IsChildEduAllow = chkIsChildEdu.Checked == true ? "Y" : "N";
        obj.IsMedicalEntmnt = chkIsMedicalEntitle.Checked == true ? "Y" : "N";
        obj.IsOTEntmnt = chkIsOTEntitle.Checked == true ? "Y" : "N";
        obj.IsPayrollStaff = chkIsPayrollStaff.Checked == true ? "Y" : "N";
        obj.IsServiceAgrmnt = "N";
        obj.IsSeveranceBenefit = chkIsSeveranceBenefit.Checked == true ? "Y" : "N";
        //obj.JobTitleId = ddlJobTitle.SelectedValue.ToString();
        obj.JoiningDate = strJoinDate;
        obj.MPCId = ddlMPC.SelectedValue.ToString();
        obj.OtherBenefit = txtOtherBenefit.Text.Trim();
        //obj.PosFuncId = ddlPosByFunction.SelectedValue.ToString();
        obj.PostingDate = strPostingDate;
        //obj.PostingDistId = ddlPostDistrict.SelectedValue.ToString();
        //obj.PostingDivId = ddlPostDivision.SelectedValue.ToString();
        //obj.PostingPlaceId = ddlPostingPlace.SelectedValue.ToString();
        obj.ProbationPeriod = txtProbationPeriod.Text.Trim();
        obj.Remarks = txtRemarks.Text.Trim();
        obj.RetirementDate = strRetirementDate;
        obj.RoutingNo = ddlBranchCode.SelectedValue.ToString();
        obj.SalLocId = ddlSalaryLoc.SelectedValue.ToString();
        obj.LocCatId= ddlLocCategory.SelectedValue.ToString();
        obj.SalPakId = ddlSalaryPak.SelectedValue.ToString();
        obj.BonusPakId = "1";
        //obj.SalSubLocId = ddlSalarySubLoc.SelectedValue.ToString();
        obj.CompanyId = ddlCompany.SelectedValue.ToString();
        obj.ProjectId = ddlProject.SelectedValue.ToString();
        obj.ProjectOfficeId = ddlProjectOffice.SelectedValue.ToString();
        obj.ClinicId = ddlClinic.SelectedValue.ToString();
        obj.SeparateDate = strSeparationDate;
        obj.SeparateReason = txtSeparationReason.Text.Trim();
        obj.SeparateTypeId = ddlSepType.SelectedValue.ToString();

        obj.ServiceEndDate = strServiceStart;
        obj.ServiceStartDate = strServiceEnd;

        obj.SeveranceId = txtSeveranceId.Text.Trim();
        obj.SeveranceReason = txtSeveranceReason.Text.Trim();
        //obj.SubDesigId = ddlJobTitle.SelectedValue.ToString();
        obj.SupervisorId = ddlSupervisor.SelectedValue.ToString();
        obj.RegionId = ddlRegion.SelectedValue.ToString();

        if (string.IsNullOrEmpty(fileEmpCV.PostedFile.FileName) == false)
            obj.EmpCV = txtEmpID.Text.Trim() + "-" + fileEmpCV.PostedFile.FileName;
        else
            obj.EmpCV = lnkEmpCV.Text.Trim();

        if (string.IsNullOrEmpty(fileEmpSignature.PostedFile.FileName) == false)
            obj.EmpSignature = txtEmpID.Text.Trim() + "-" + fileEmpSignature.PostedFile.FileName;
        else
            obj.EmpSignature = lnkEmpSignature.Text.Trim();

        if (string.IsNullOrEmpty(fileEmpDocument.PostedFile.FileName) == false)
            obj.EmpDocument = txtEmpID.Text.Trim() + "-" + fileEmpDocument.PostedFile.FileName;
        else
            obj.EmpDocument = lnkEmpDocument.Text.Trim();

        obj.WorkArea = txtWorkArea.Text.Trim();
        obj.WorkAreaType = chkWorkArea.Checked == true ? "Y" : "N";
        // obj.BonusPakId = ddlBonusPak.SelectedValue.ToString();
        obj.LeavePakId = ddlLeavePackage.SelectedValue.ToString();
        obj.WeekendId = ddlWeekend.SelectedValue.ToString();
        obj.AttnPolicyID = ddlAttndPolicy.SelectedValue.ToString();

        string strCardNo = txtEmpID.Text.Substring(1);
        obj.CardNo = Convert.ToInt32(strCardNo);

        obj.WorkingDays = txtWorkingDays.Text.Trim();
        obj.IsNotRehirable = chkIsNotRehire.Checked == true ? "Y" : "N";
        obj.NotRehireReason = txtNotRehireReason.Text.Trim();
        obj.Asset = txtAsset.Text.Trim();
        obj.InsertedBy = Session["USERID"].ToString();
        obj.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());
        obj.TaxRegionId = ddlTaxRegion.SelectedValue.ToString();
        return obj;
    }

    private void SaveData()
    {
        try
        {
            string strLeaveUpdate = "";
            this.UploadCV();
            this.UploadDocument();
            this.UploadSignature();

            if (hfLPakId.Value.ToString()  == ddlLeavePackage.SelectedValue.ToString())// || (hfJoiningDate.Value == Common.DisplayDate(txtJoiningDate.Text))) //mn
                strLeaveUpdate = "N";
            else
                strLeaveUpdate = "Y";

            objEmpInfoMgr.InsertEmpInfoTabHR(this.BindObject(), strLeaveUpdate, chkIsNew.Checked == true ? "Y" : "N", Session["FISCALYRID"].ToString());

            lblMsg.Text = "Record Updated Successfully";

            this.EntryMode(false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
            //btnSave.Enabled = true;
            Common.EmptyTextBoxValues(this);
            txtGrossSalary.Text = "0";
            txtBasicSalary.Text = "0";
            txtWorkingDays.Text = "30"; 
            txtContractInterval.Text = "0";
            txtProbationPeriod.Text = "0";
            txtOtherBenefit.Text = "N/A"; 
            chkIsNew.Checked = false;
            lnkEmpCV.Text = "";
            lnkEmpSignature.Text = "";
            lnkEmpDocument.Text = "";
            grEmpAction.DataSource = null;
            grEmpAction.DataBind();
            ddlMPC.SelectedIndex = 1;
        }        
    }

    //private void GetViewPermission()
    //{
    //    if (Session["ISPAYADMIN"].ToString() == "Y")
    //        btnSave.Enabled = false;
    //    else
    //        btnSave.Enabled = true;
    //}

    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList(objEmpInfoMgr.SelectBranchList(ddlBankName.SelectedValue.ToString()), ddlBranchCode, "BranchName", "Routingno", true, "Nil");
    }

    protected void txtContractInterval_TextChanged(object sender, EventArgs e)
    {
        if ((string.IsNullOrEmpty(txtContractInterval.Text.Trim()) == false) && (string.IsNullOrEmpty(txtJoiningDate.Text.Trim()) == false))
        {
            DateTime dateRetirement = Convert.ToDateTime(Common.ReturnDate(txtJoiningDate.Text.Trim()));
            dateRetirement = dateRetirement.AddMonths(Convert.ToInt32(txtContractInterval.Text.Trim()));
            txtContractExpDate.Text = Common.DisplayDate(dateRetirement.ToString());
        }
    }
  
    private void UploadCV()
    {
        if (fileEmpCV.HasFile)
        {
            string flName = txtEmpID.Text.Trim() + "-" + fileEmpCV.PostedFile.FileName;
            string FilePath = Server.MapPath("../EmpCV" + "/" + flName);

            //Delete Existing File
            FileInfo File = new FileInfo(FilePath);
            File.Delete();

            fileEmpCV.SaveAs(FilePath);
        }
    }

    private void UploadSignature()
    {
        if (fileEmpSignature.HasFile)
        {
            string flName = txtEmpID.Text.Trim() + "-" + fileEmpSignature.PostedFile.FileName;
            string FilePath = Server.MapPath("../EmpSignature" + "/" + flName);

            //Delete Existing File
            FileInfo File = new FileInfo(FilePath);
            File.Delete();

            fileEmpSignature.SaveAs(FilePath);
        }
    }

    private void UploadDocument()
    {
        if (fileEmpDocument.HasFile)
        {
            string flName = txtEmpID.Text.Trim() + "-" + fileEmpDocument.PostedFile.FileName;
            string FilePath = Server.MapPath("../EmpDocument" + "/" + flName);

            //Delete Existing File
            FileInfo File = new FileInfo(FilePath);
            File.Delete();

            fileEmpDocument.SaveAs(FilePath);
        }
    }

    protected void lnkEmpCV_Click(object sender, EventArgs e)
    {
        string FilePath = Server.MapPath("../EmpCV" + "/" + lnkEmpCV.Text);
        System.Diagnostics.Process.Start(FilePath);
    }

    protected void lnkEmpSignature_Click(object sender, EventArgs e)
    {
        string FilePath = Server.MapPath("../EmpSignature" + "/" + lnkEmpSignature.Text);
        System.Diagnostics.Process.Start(FilePath);
    }

    protected void lnkEmpDocument_Click(object sender, EventArgs e)
    {
        string FilePath = Server.MapPath("../EmpDocument" + "/" + lnkEmpDocument.Text);
        System.Diagnostics.Process.Start(FilePath);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void txtSupervisorId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSupervisorId.Text) == false)
            {
                ddlSupervisor.SelectedValue = txtSupervisorId.Text.Trim();
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Invalid Supervisor Id.";
        }
    }   

   
    protected void ddlSalaryLoc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList_Nil(objMasMgr.SelectDeptWsSubDept(Convert.ToInt32(ddlDept.SelectedValue)), ddlSubDept);
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void txtGrossSalary_TextChanged(object sender, EventArgs e)
    {
        
    }
    private void CalculateBasicSalary()
    {
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
        DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("1", ddlEmpType.SelectedValue.ToString());
        if (dtBfPlc.Rows.Count > 0)
        {
            decimal dclBasicAmt = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtGrossSalary.Text, 0), Common.RoundDecimal(dtBfPlc.Rows[0]["Value"].ToString(), 2));
            txtBasicSalary.Text = dclBasicAmt.ToString();
        }
    }
    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList_Nil(objMasMgr.SelectGradeWsDesignation(Convert.ToInt32(ddlGrade.SelectedValue)), ddlDesignation );

        string strIsOTEntitle="";
        if (ddlGrade.SelectedValue.ToString() == "99999")
        {
            chkIsOTEntitle.Checked = false;
            return;
        }
        strIsOTEntitle = objMasMgr.GetGradeWsOTEntitlement(Convert.ToInt16(ddlGrade.SelectedValue.ToString()));
        if (strIsOTEntitle == "Y")
            chkIsOTEntitle.Checked = true;
        else
            chkIsOTEntitle.Checked = false;
    }
    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtProbationPeriod_TextChanged(object sender, EventArgs e)
    {
        DateTime dtJoinDate = new DateTime();
        DateTime dtConfirmDueDate = new DateTime();
        if ((string.IsNullOrEmpty(txtProbationPeriod.Text) == false) && (string.IsNullOrEmpty(txtJoiningDate.Text) == false))
        {
            dtJoinDate = Convert.ToDateTime(Common.ReturnDate(txtJoiningDate.Text.Trim()));
            dtConfirmDueDate = dtJoinDate.AddMonths(Convert.ToInt32(txtProbationPeriod.Text.Trim()));
            txtConfirmDate.Text = Common.DisplayDate(dtConfirmDueDate.ToString());
        }
    }

    private void ConfirmValidation()
    {
        if (ddlEmpType.SelectedValue.ToString() == "1")
        {
            txtProbationPeriod.Enabled = true;
            txtContractInterval.Enabled = false;
            txtContractInterval.Text = "0";
        }
        else
        {
            txtProbationPeriod.Enabled = false ;
            txtContractInterval.Enabled = true ;
            txtProbationPeriod.Text = "0";
        }
    }

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ConfirmValidation();
    }

    protected void cmdCalBasic_Click(object sender, EventArgs e)
    {
        this.CalculateBasicSalary();
    }
    protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
