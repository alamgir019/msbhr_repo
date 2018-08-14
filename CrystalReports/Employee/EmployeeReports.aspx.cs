using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CrystalReports_Employee_EmployeeReports : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();

    LeaveManager objLeaveMgr = new LeaveManager();
    private Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(objMasMgr.SelectGrade(0), ddlGrade);
            Common.FillDropDownList_All(objMasMgr.SelectDivision(0), ddlComponentUnit);
            Common.FillDropDownList_All(objMasMgr.SelectClinic(), ddlSector);
            Common.FillDropDownList_All(objMasMgr.SelectEmpType(0, "Y"), ddlEmpType);
            Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlDepartment);
            Common.FillDropDownList_All(objMasMgr.SelectDivision(0), ddlDivision);
            Common.FillDropDownList_All(objMasMgr.SelectDistrict(0), ddlDistrict);
            Common.FillDropDownList_All(objMasMgr.SelectHomeDistrict(0), ddlPerDistrict);
            Common.FillDropDownList_All(objMasMgr.SelectProject(0), ddlPosByFunc);
            Common.FillDropDownList_All(objMasMgr.SelectReligionList(0), ddlReligion);
            Common.FillDropDownList_All(objMasMgr.SelectDesignation(0), ddlDesignation);
            Common.FillDropDownList_All(objMasMgr.SelectLocationCategory(0), ddlSalaryLoc);
            //Common.FillDropDownList_All(objMasMgr.SelectSalarySubLocation(0), ddlSalarySubLoc);
            Common.FillDropDownList_All(objMasMgr.SelectAction(0, "S"), ddlSeparationType);
            Common.FillDropDownList(objEmpInfoMgr.SelectBankList(), ddlBank, "BankName", "BankCode", true, "All");
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_All(objMasMgr.SelectBloodGroupList(0), ddlBloodGroup);
            Common.FillMonthList(ddlMonthFrm);
            Common.FillYearList(5, ddlYear);
            Common.FillDropDownList_All(objEmpInfoMgr.SelectNatureWiseAction("D"), ddlAction);
            Common.FillDropDownList_All(objMasMgr.SelectReason(0), ddlReasonList);
            DateTime now = DateTime.Now;
            ddlMonthFrm.SelectedValue = Convert.ToInt32(now.Month).ToString();
            ddlYear.SelectedValue = Convert.ToInt32(now.Year).ToString();

            Common.FillDropDownList_All(objMasMgr.SelectBloodGroupList(0), ddlBloodGroup);
            this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");


        }

    }


    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)

    //{
    //    //PGroupWise,PEmpId,PEmpName,PGrade,PDesig,PSector,PProgDept,PCompUnit,PPosByFunc,PSalarySubLoc,PSalaryLoc,PDivision,PDistrict,PPlaceOfPosting,PGender,PReligion,PBank,PBloodGroup,PDateRange,PrbtnTypeEmp,PActveInAcBasic,PShow

    //    this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");

    //this.FillddlEmplStatus();

    {
        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");

        switch (tvReports.SelectedValue)
        {
            case "EL":
                {
                    PanelVisibilityMst("0", "1", "1", "1", "1", "0", "1", "0", "1", "0", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    LabelEmpName.Text = "Employee Name:";
                    LabelFromDate.Text = "Joining Date:";
                    LabelToDate.Text = "To:";
                    LabeEmpId.Text = "Employee Id :";
                    radBtnListEmp.SelectedValue = "A";
                    break;
                }
            case "ELNB":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabelPostingDistrict.Text = "Posting District:";
                    radBtnListEmp.SelectedValue = "A";
                    break;
                }
            case "GWEL":
                {
                    PanelVisibilityMst("1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    ddlGroupWise.SelectedValue = "1";
                    break;
                }
            case "BAI":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    radBtnListEmp.SelectedValue = "A";
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    DataTable dtEmp = objMasMgr.SelectEmpList(strEmpType);
                    gvEmp.DataSource = dtEmp;
                    gvEmp.DataBind();
                    break;
                }
            case "SSL":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "1", "0", "1", "1", "0", "1", "0", "0");
                    radBtnListEmp.SelectedValue = "I";
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }
            case "EECL":
                {
                    PanelVisibilityMst("0", "1", "1", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "EEI":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabelSalaryLocation.Text = "Location";
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }

            case "EI":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    radBtnListEmp.SelectedValue = "A";
                    break;
                }
            case "EWI":
                {
                    PanelVisibilityMst("0", "1", "1", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabelEmpName.Text = "Employee Name:";
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "ELWS":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabelEmpName.Text = "Supervisor Name:";

                    break;
                }

            case "ELWA":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0");
                    radBtnListEmp.SelectedValue = "A";
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    // DataTable dtEmp = objMasMgr.SelectEmployee("", strEmpType);
                    DataTable dtEmp = objMasMgr.SelectEmpList(strEmpType);
                    gvEmp.DataSource = dtEmp;
                    gvEmp.DataBind();
                    break;
                }
            case "EDA":
                {

                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "1");
                    LabelSalaryLocation.Text = "Location";
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "CL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabelFromDate.Text = "Confirmation Date From:";

                    break;
                }
            case "EEID":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "EEIIB":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "ENI":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "EJF":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");

                    break;
                }


            case "ETDYI":
                {

                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");



                    // PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "1");                    
                    radBtnListEmp.SelectedValue = "A";
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    //DataTable dtEmp = objMasMgr.SelectEmployee("", strEmpType);
                    DataTable dtEmp = objMasMgr.SelectEmpList(strEmpType);
                    gvEmp.DataSource = dtEmp;
                    gvEmp.DataBind();
                    break;

                }
            case "1"://Service Length Of Separated Employee
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");//All Separated emps r Inactive
                    LabeEmpId.Text = "Employee Id :";

                    break;
                }
            case "2":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }
            case "3":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }

            case "ER":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }
            //case "DP":
            //    {

            //        PanelVisibilityMst("0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
            //        radBtnListEmp.SelectedValue = "A";
            //        string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
            //        DataTable dtEmp = objMasMgr.SelectEmployee("", strEmpType);
            //        gvEmp.DataSource = dtEmp;
            //        gvEmp.DataBind();
            //        break;

            //    }
            case "ESI":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    // radBtnListEmp.SelectedValue = "A";
                    // string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    //// DataTable dtEmp = objMasMgr.SelectEmployee("", strEmpType);
                    // DataTable dtEmp = objMasMgr.SelectEmpList(strEmpType);
                    // gvEmp.DataSource = dtEmp;
                    // gvEmp.DataBind();
                    //LabelServiceLngthFrom.Text = "Basic From";
                    break;
                }
            case "LSAE":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    radBtnListEmp.SelectedValue = "A";
                    break;
                }

            case "IR":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }

            case "OTC":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Employee Id :";
                    break;
                }

            #region Consultant Report
            case "CIWP":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Consultant Id :";

                    break;
                }

            case "COI":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    LabeEmpId.Text = "Consultant Id :";

                    break;
                }
            #endregion


            #region System Report
            case "UHR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }


            //-----------------------------------------
            case "EPHR":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillMonthList_All(ddlMonthFrm);
                    break;
                }
            case "ETR":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillMonthList_All(ddlMonthFrm);
                    break;
                }
            case "ECSR":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "ESCHR":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }

                #endregion

        }
        this.PanelVisibilityDet();
    }


    protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.PanelVisibilityDet();
    }
    private void PanelVisibilityDet()
    {

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        switch (tvReports.SelectedValue)
        {
            case "EL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["GradeId"] = ddlGrade.SelectedValue;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FullName"] = txtEmpName.Text.Trim();
                    Session["Gender"] = ddlGender.SelectedValue;
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["UnitId"] = ddlComponentUnit.SelectedValue;
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["PostingDistId"] = ddlDistrict.SelectedValue;
                    Session["DesigId"] = ddlDesignation.SelectedValue;
                    Session["PosByFuncId"] = ddlPosByFunc.SelectedValue;
                    Session["ReligionId"] = ddlReligion.SelectedValue;
                    Session["TNTPosition"] = ddlTNTPosition.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpType"] = ddlEmpType.SelectedValue;
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["Basic"] = radBtnBasic.SelectedValue;
                    break;
                }
            case "ELNB":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["PostingDistId"] = ddlDistrict.SelectedValue;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpType"] = ddlEmpType.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "GWEL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["RPRTID"] = ddlGroupWise.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;

                    if (ddlGroupWise.SelectedValue == "1")
                        Session["GROUPID"] = ddlGrade.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "2")
                        Session["GROUPID"] = ddlSector.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "3")
                        Session["GROUPID"] = ddlComponentUnit.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "4")
                        Session["GROUPID"] = ddlPosByFunc.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "5")
                        Session["GROUPID"] = ddlTNTPosition.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "6")
                        Session["GROUPID"] = ddlSalaryLoc.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "7")
                        Session["GROUPID"] = ddlDivision.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "8")
                        Session["GROUPID"] = ddlTNTPosition.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "9")
                        Session["GROUPID"] = ddlDesignation.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "10")
                        Session["GROUPID"] = ddlReligion.SelectedValue;
                    else if (ddlGroupWise.SelectedValue == "11")
                        Session["GROUPID"] = ddlDepartment.SelectedValue;
                    break;
                }
            case "BAI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpId = "";
                    int i = 1;
                    foreach (GridViewRow gRow in gvEmp.Rows)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
                        if (checkBox.Checked)
                        {
                            if (i == 1)
                                strEmpId = gRow.Cells[1].Text.Trim();
                            else
                                strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["EmpID"] = strEmpId;// txtEmpCode.Text.Trim(); //
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["BankCode"] = ddlBank.SelectedValue;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpType"] = ddlEmpType.SelectedValue;

                    //DataTable dtLeaveType = MasMgr.SelectEmployee("");
                    // gvEmp.DataSource = dtLeaveType;
                    // gvEmp.DataBind(); 
                    break;
                }
            case "SSL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["GradeId"] = ddlGrade.SelectedValue;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["SeparationType"] = ddlSeparationType.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    Session["Rehireable"] = radBtnRehireable.SelectedValue.ToString();// chkRehireable.Checked == true ? "N" : "Y";
                    if (string.IsNullOrEmpty(txtServiceLength.Text.Trim()) == false)
                        Session["ServiceLengthFrom"] = txtServiceLength.Text.Trim();
                    else
                        Session["ServiceLengthFrom"] = "0";
                    if (string.IsNullOrEmpty(txtTo.Text.Trim()) == false)
                        Session["ServiceLengthTo"] = txtTo.Text.Trim();
                    else
                        Session["ServiceLengthTo"] = "0";

                    break;
                }
            case "EECL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FullName"] = txtEmpName.Text.Trim();
                    Session["DesigId"] = ddlDesignation.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EEI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["PostingDistId"] = ddlDistrict.SelectedValue;
                    Session["BloodGroupId"] = ddlBloodGroup.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpType"] = ddlEmpType.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EWI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FullName"] = txtEmpName.Text.Trim();
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DesigId"] = ddlDesignation.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "ELWS":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FullName"] = txtEmpName.Text.Trim();
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "ELWA":
                {

                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpId = "";
                    int i = 1;
                    foreach (GridViewRow gRow in gvEmp.Rows)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
                        if (checkBox.Checked)
                        {
                            if (i == 1)
                                strEmpId = gRow.Cells[1].Text.Trim();
                            else
                                strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["EmpId"] = strEmpId;
                    Session["HomeDistId"] = ddlPerDistrict.SelectedValue;
                    Session["BloodGroupId"] = ddlBloodGroup.SelectedValue;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpType"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EDA":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    // Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    // Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["ReasonOfAction"] = ddlReasonList.SelectedValue.ToString();
                    Session["ActionType"] = ddlAction.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;

                    break;
                }
            case "CL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EEID":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim(); //
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "EEIIB":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim(); //
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "ENI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["SalLocId"] = ddlSalaryLoc.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }

            case "EJF":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "ETDYI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpId = "";
                    int i = 1;
                    foreach (GridViewRow gRow in gvEmp.Rows)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
                        if (checkBox.Checked)
                        {
                            if (i == 1)
                                strEmpId = gRow.Cells[1].Text.Trim();
                            else
                                strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["EmpID"] = strEmpId;// txtEmpCode.Text.Trim(); //
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "1":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    //Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }
            case "2":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["IsActive"] = radBtnListEmp.SelectedValue.ToString();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;

                    break;
                }
            case "3":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;

                    if (string.IsNullOrEmpty(txtServiceLength.Text.Trim()) == false)
                        Session["ServiceLengthFrom"] = txtServiceLength.Text.Trim();
                    else
                        Session["ServiceLengthFrom"] = "0";
                    if (string.IsNullOrEmpty(txtTo.Text.Trim()) == false)
                        Session["ServiceLengthTo"] = txtTo.Text.Trim();
                    else
                        Session["ServiceLengthTo"] = "0";

                    break;
                }
            case "LSAE":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VMonthName"] = ddlMonthFrm.SelectedItem.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["VYearName"] = ddlYear.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    break;
                }

            case "ER":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    Session["EmpID"] = txtEmpCode.Text.Trim();

                    break;
                }
            //case "DP":
            //    {
            //        Session["REPORTID"] = tvReports.SelectedNode.Value;
            //        string strEmpId = "";
            //        int i = 1;
            //        foreach (GridViewRow gRow in gvEmp.Rows)
            //        {
            //            CheckBox checkBox = new CheckBox();
            //            checkBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
            //            if (checkBox.Checked)
            //            {
            //                if (i == 1)
            //                    strEmpId = gRow.Cells[1].Text.Trim();
            //                else
            //                    strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
            //                i++;
            //            }
            //        }
            //        Session["EmpID"] = strEmpId;// txtEmpCode.Text.Trim(); //
            //        Session["DeptId"] = ddlDepartment.SelectedValue;
            //        Session["LAreaId"] = ddlLearningArea.SelectedValue.ToString();
            //        Session["GradeId"] = ddlGrade.SelectedValue.ToString();
            //        Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
            //        Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();

            //        break;
            //    }
            case "ESI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["EmpStatus"] = strEmpType;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["ClinicId"] = ddlSector.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    //Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    //Session["PostingDistId"] = ddlDistrict.SelectedValue;
                    //if (string.IsNullOrEmpty(txtServiceLength.Text.Trim()) == false)
                    //    Session["BasicFrom"] = txtServiceLength.Text.Trim();
                    //else
                    //    Session["BasicFrom"] = "0";
                    //if (string.IsNullOrEmpty(txtTo.Text.Trim()) == false)
                    //    Session["BasicTo"] = txtTo.Text.Trim();
                    //else
                    //Session["BasicTo"] = "0";
                    break;
                }
            //----------------- New  --------

            case "EPHR":
                {
                    Session["SalDiv"] = ddlSalaryLoc.SelectedValue.ToString() == "-1" ? "" : ddlSalaryLoc.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesignation.SelectedValue.ToString() == "99999" ? "" : ddlDesignation.SelectedValue.ToString();
                    if (txtFromDate.Text.ToString() != "" && txtToDate.Text.ToString() != "")
                    {
                        DateTime Fdt = DateTime.Parse(Common.ReturnDate(txtFromDate.Text.ToString()));// DateTime.Parse(txtFromDate.Text.ToString());
                        DateTime Tdt = DateTime.Parse(Common.ReturnDate(txtToDate.Text.ToString()));
                        if (Fdt.Date <= Tdt.Date)
                        {
                            Session["FDate"] = txtFromDate.Text.ToString();
                            Session["TDate"] = txtToDate.Text.ToString();
                        }
                    }
                    else
                    {
                        Session["FDate"] = "";
                        Session["TDate"] = "";
                    }

                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ETR":
                {
                    Session["SalDiv"] = ddlSalaryLoc.SelectedValue.ToString() == "-1" ? "" : ddlSalaryLoc.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesignation.SelectedValue.ToString() == "99999" ? "" : ddlDesignation.SelectedValue.ToString();
                    if (txtFromDate.Text.ToString() != "" && txtToDate.Text.ToString() != "")
                    {
                        DateTime Fdt = DateTime.Parse(Common.ReturnDate(txtFromDate.Text.ToString()));
                        DateTime Tdt = DateTime.Parse(Common.ReturnDate(txtToDate.Text.ToString()));

                        if (Fdt.Date <= Tdt.Date)
                        {
                            Session["FDate"] = txtFromDate.Text.ToString();
                            Session["TDate"] = txtToDate.Text.ToString();
                        }
                    }
                    else
                    {
                        Session["FDate"] = "";
                        Session["TDate"] = "";
                    }

                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ECSR":
                {
                    Session["SalDiv"] = ddlSalaryLoc.SelectedValue.ToString() == "-1" ? "" : ddlSalaryLoc.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesignation.SelectedValue.ToString() == "99999" ? "" : ddlDesignation.SelectedValue.ToString();
                    if (txtFromDate.Text.ToString() != "" && txtToDate.Text.ToString() != "")
                    {
                        DateTime Fdt = DateTime.Parse(Common.ReturnDate(txtFromDate.Text.ToString()));
                        DateTime Tdt = DateTime.Parse(Common.ReturnDate(txtToDate.Text.ToString()));

                        if (Fdt.Date <= Tdt.Date)
                        {
                            Session["FDate"] = txtFromDate.Text.ToString();
                            Session["TDate"] = txtToDate.Text.ToString();
                        }
                    }
                    else
                    {
                        Session["FDate"] = "";
                        Session["TDate"] = "";
                    }

                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();


                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ESCHR":
                {

                    if (txtFromDate.Text.ToString() != "" && txtToDate.Text.ToString() != "")
                    {
                        DateTime Fdt = DateTime.Parse(Common.ReturnDate(txtFromDate.Text.ToString()));
                        DateTime Tdt = DateTime.Parse(Common.ReturnDate(txtToDate.Text.ToString()));
                        if (Fdt.Date <= Tdt.Date)
                        {
                            Session["FDate"] = txtFromDate.Text.ToString();
                            Session["TDate"] = txtToDate.Text.ToString();
                        }
                    }
                    else
                    {
                        Session["FDate"] = "01/01/1900";
                        Session["TDate"] = "01/01/1900";
                    }

                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["Sector"] = ddlSector.SelectedValue.ToString();
                    Session["Dept"] = ddlDepartment.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }

            #region Consultant Info
            case "CIWP":
                Session["REPORTID"] = tvReports.SelectedNode.Value;
                Session["EmpID"] = txtEmpCode.Text.Trim();

                break;

            case "COI":
                Session["REPORTID"] = tvReports.SelectedNode.Value;
                Session["EmpID"] = txtEmpCode.Text.Trim();

                break;
            #endregion
            #region System Report
            case "UHR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    break;
                }
                #endregion
        }


        //Open New Window
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open('frmEmployeeReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    //PGroupWise,PEmpId,PEmpName,PGrade,PDesig,PSector,PProgDept,PCompUnit,PPosByFunc,PSalarySubLoc,PSalaryLoc,PDivision,PDistrict,PPlaceOfPosting,PGender,PReligion,PBank,PDateRange,PrbtnTypeEmp,PActveInAcBasic,P_Emp,PShow
    private void PanelVisibilityMst(string sPGroupWise, string sPEmpId, string sPEmpName, string sPGrade, string sPDesig, string sPSector,
        string sPProgDept, string sPCompUnit, string sPPosByFunc, string sSalarySubLoc, string sSalaryLoc, string sPDivision, string sPDist,
        string sPPlaceOfPosting, string sPGender, string sPReligion, string sPBank, string sPBloodGroup, string sPLearningArea, string sPFiscalYr,
        string sPDateRange, string sPEmpType, string sPActveInAcBasic, string sP_Emp, string PMonthFrom, string PYear, string sPShow,
        string sPradBtnBasic, string sPSeparationType, string sPRehireableChkBx, string sHomeDist, string sServiceLength, string sReasonOfAction,
        string sActionType)
    {
        //ddlReportBy.SelectedIndex = 0;
        if (sPGroupWise == "1")
            PGroupWise.Visible = true;
        else
            PGroupWise.Visible = false;

        if (sPEmpId == "1")
            PEmpId.Visible = true;
        else
            PEmpId.Visible = false;


        if (sPEmpName == "1")
            PEmpName.Visible = true;
        else
            PEmpName.Visible = false;


        if (sPGrade == "1")
            PGrade.Visible = true;
        else
            PGrade.Visible = false;


        if (sPDesig == "1")
            PDesig.Visible = true;
        else
            PDesig.Visible = false;


        if (sPSector == "1")
            PSector.Visible = true;
        else
            PSector.Visible = false;


        if (sPProgDept == "1")
            PProgDept.Visible = true;
        else
            PProgDept.Visible = false;


        if (sPCompUnit == "1")
            PCompUnit.Visible = true;
        else
            PCompUnit.Visible = false;


        if (sPPosByFunc == "1")
            PPosByFunc.Visible = true;
        else
            PPosByFunc.Visible = false;

        if (sSalarySubLoc == "1")
            PSalarySubLoc.Visible = true;
        else
            PSalarySubLoc.Visible = false;

        if (sSalaryLoc == "1")
            PSalaryLoc.Visible = true;
        else
            PSalaryLoc.Visible = false;


        if (sPDivision == "1")
            PDivision.Visible = true;
        else
            PDivision.Visible = false;


        if (sPDist == "1")
            PDistrict.Visible = true;
        else
            PDistrict.Visible = false;


        if (sPPlaceOfPosting == "1")
            PPlaceOfPosting.Visible = true;
        else
            PPlaceOfPosting.Visible = false;


        if (sPDateRange == "1")
            PDateRange.Visible = true;
        else
            PDateRange.Visible = false;


        if (sPGender == "1")
            PGender.Visible = true;
        else
            PGender.Visible = false;


        if (sPReligion == "1")
            PReligion.Visible = true;
        else
            PReligion.Visible = false;

        if (sPBank == "1")
            PBank.Visible = true;
        else
            PBank.Visible = false;

        if (sPBloodGroup == "1")
            PBloodGroup.Visible = true;
        else
            PBloodGroup.Visible = false;
        if (sPLearningArea == "1")
            PLearningArea.Visible = true;
        else
            PLearningArea.Visible = false;

        if (sPFiscalYr == "1")

            PFiscalYr.Visible = true;
        else
            PFiscalYr.Visible = false;



        if (sPEmpType == "1")
            PEmpType.Visible = true;
        else
            PEmpType.Visible = false;


        if (sPActveInAcBasic == "1")
            PActveInAcBasic.Visible = true;
        else
            PActveInAcBasic.Visible = false;

        if (sP_Emp == "1")
            P_Emp.Visible = true;
        else P_Emp.Visible = false;


        if (PMonthFrom == "1")
            this.PMonthFrom.Visible = true;
        else
            this.PMonthFrom.Visible = false;

        if (PYear == "1")
            this.PYear.Visible = true;
        else
            this.PYear.Visible = false;

        if (sPShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;
        if (sPradBtnBasic == "1")
            PradBtnBasic.Visible = true;
        else
            PradBtnBasic.Visible = false;

        if (sPSeparationType == "1")
            PSeparationType.Visible = true;
        else
            PSeparationType.Visible = false;
        if (sPRehireableChkBx == "1")
            PRehireableChkBx.Visible = true;
        else
            PRehireableChkBx.Visible = false;

        if (sHomeDist == "1")
            PhomeDist.Visible = true;
        else
            PhomeDist.Visible = false;
        if (sServiceLength == "1")
            PServiceLength.Visible = true;
        else
            PServiceLength.Visible = false;

        if (sReasonOfAction == "1")
            PReasonOfAction.Visible = true;
        else
            PReasonOfAction.Visible = false;

        if (sActionType == "1")
            PActionType.Visible = true;
        else
            PActionType.Visible = false;

    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList_All(objMasMgr.SelectDivisionWiseDistrict2(Convert.ToInt32(ddlDivision.SelectedValue)), ddlDistrict);

    }
    protected void ddlGroupWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        // PGroupWise.Visible = true;



        if (ddlGroupWise.SelectedValue == "1")
            PGrade.Visible = true;
        else
            PGrade.Visible = false;
        if (ddlGroupWise.SelectedValue == "2")
            PSector.Visible = true;
        else
            PSector.Visible = false;
        if (ddlGroupWise.SelectedValue == "3")
            PCompUnit.Visible = true;
        else
            PCompUnit.Visible = false;

        if (ddlGroupWise.SelectedValue == "4")
            PPosByFunc.Visible = true;
        else
            PPosByFunc.Visible = false;
        if (ddlGroupWise.SelectedValue == "5")
            PPlaceOfPosting.Visible = true;
        else
            PPlaceOfPosting.Visible = false;
        if (ddlGroupWise.SelectedValue == "6")
            PSalaryLoc.Visible = true;
        else
            PSalaryLoc.Visible = false;
        if (ddlGroupWise.SelectedValue == "7")
            PDivision.Visible = true;
        else
            PDivision.Visible = false;

        if (ddlGroupWise.SelectedValue == "8")
            PPlaceOfPosting.Visible = true;
        else
            PPlaceOfPosting.Visible = false;
        if (ddlGroupWise.SelectedValue == "9")
            PDesig.Visible = true;
        else
            PDesig.Visible = false;
        if (ddlGroupWise.SelectedValue == "10")
            PReligion.Visible = true;
        else
            PReligion.Visible = false;
        if (ddlGroupWise.SelectedValue == "11")
            PProgDept.Visible = true;
        else
            PProgDept.Visible = false;



    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {

        if (chkSelectAll.Checked == true)
        {
            foreach (GridViewRow row in gvEmp.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkBoxEmp");
                checkBox.Checked = true;

            }
        }
        else
        {
            foreach (GridViewRow row in gvEmp.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkBoxEmp");
                checkBox.Checked = false;
            }
        }

    }
    protected void radBtnListEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

        switch (tvReports.SelectedValue)
        {
            case "BAI":
            case "ESI":
            case "ELWA":
            case "ETDYI":
                string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                DataTable dtEmp = objMasMgr.SelectEmployee("", strEmpType);
                gvEmp.DataSource = dtEmp;
                gvEmp.DataBind();
                break;
        }



    }
    //protected void btnShowEmp_Click(object sender, EventArgs e)
    //{


    //}
}