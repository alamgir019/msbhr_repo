using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CrystalReports_Training_TrainingReports : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();
    LeaveManager objLeaveMgr = new LeaveManager();
    private Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    TrainingManager objEmpMgr = new TrainingManager();
    EmpInfoManager objEmp = new EmpInfoManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(objMasMgr.SelectTrainingName(0, "A"), ddlTrainingName);
            Common.FillDropDownList_All(objMasMgr.SelectProject(0), ddlFundedBy);
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlProgDept);
            Common.FillDropDownList_All(objMasMgr.SelectClinic("Y"), ddlSalLoc);
            Common.FillIdNameDropDownList(objEmp.SelectEmpNameWithID("A"), ddlEmployeeName, "EMPNAME", "EMPID",true);

            ddlFiscalYr.SelectedIndex = 0;
            this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0","0");
        }
    }

    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        switch (tvReports.SelectedValue)
        {
            case "ETD":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0");
                    break;
                }
            case "OTD":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "TSI":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "DTR":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "1", "0", "0", "0");
                    break;
                }
            case "EER":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "0", "0");
                    break;
                }
            case "TLR":
                {
                    PanelVisibilityMst("0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "TLPALAW":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "MFPIT":
                {
                    PanelVisibilityMst("0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "NTC":
                {
                    PanelVisibilityMst("1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "OTR":
                {
                    PanelVisibilityMst("1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "1":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "1", "0", "0", "0", "0");
                    break;
                }
            case "2":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "1", "0", "0", "0", "0");
                    break;
                }
            case "GWP":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "MWTP":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "IDP":
                {
                    PanelVisibilityMst("1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "LAWP":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "TS":
                {
                    PanelVisibilityMst("1", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0");
                    break;
                }
            case "SWTI":
                {
                    PanelVisibilityMst("1", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0");
                    break;
                }
            case "SWP":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "QWP":
                {
                    PanelVisibilityMst("0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0","0");
                    break;
                }
            case "TBR":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "YPR":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }

            case "TCR":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1", "0", "0");
                    break;
                }
            case "TMR":
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script>");
                    sb.Append("window.open('trainingMatrixRpt.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                             sb.ToString(), false);
                    ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                    //PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "1");
                    tvReports.SelectedNode.Selected = false;
                    break;
                }

            case "TRR":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }

            case "INL":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "1");
                    break;
                }
            case "PRL":
                {
                    PanelVisibilityMst("0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1");
                    break;
                }
        }
        this.PanelVisibilityDet();
        //Response.Redirect(Request.RawUrl);
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
            case "ETD":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    Session["EmpId"] = ddlEmployeeName.SelectedValue.Trim();
                    break;
                }
            case "OTD":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "TSI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "DTR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalLocId"] = ddlSalLoc.SelectedValue.ToString().Trim();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString().Trim();
                    Session["FundedBy"] = ddlFundedBy.SelectedValue.ToString().Trim();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "EER":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalLocId"] = ddlSalLoc.SelectedValue.ToString().Trim();
                    Session["EmployeeName"] = ddlEmployeeName.SelectedItem.ToString();
                    Session["EmpId"]= ddlEmployeeName.SelectedValue.ToString().Trim();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString().Trim();
                    break;
                }
            case "TLR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["DeptId"] = ddlProgDept.SelectedValue.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["LAreaId"] = ddlFundedBy.SelectedValue.ToString();
                    break;
                }
            case "TLPALAW":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["DeptId"] = ddlProgDept.SelectedValue.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["LAreaId"] = ddlFundedBy.SelectedValue.ToString();
                    break;
                }

            case "MFPIT":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "NTC":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }

            case "1":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["OTType"] = radBtnOTType.SelectedValue;
                    break;
                }
            case "2":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["OTType"] = radBtnOTType.SelectedValue;
                    break;
                }

            case "GWP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "MWTP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }

            case "IDP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }

            case "LAWP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["LAreaId"] = ddlFundedBy.SelectedValue.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "TS":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["DeptId"] = ddlProgDept.SelectedValue.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["TrainingType"] = ddlTrainType.SelectedValue.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "SWTI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["DeptId"] = ddlProgDept.SelectedValue.ToString();
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["TrainingType"] = ddlTrainType.SelectedValue.ToString();
                    Session["PHeader"] = ddlTrainType.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "SWP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "QWP":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    break;
                }
            case "TBR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "YPR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["ProjectID"] = ddlFundedBy.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "TCR":
                {
                    if (ddlTrainingName.SelectedIndex>0 && ddlEmployeeName.SelectedIndex>0)
                    {
                        Session["REPORTID"] = tvReports.SelectedNode.Value;
                        Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                        Session["EmpId"] = ddlEmployeeName.SelectedValue.ToString();
                        //Session["FromDate"] = txtFromDate.Text.Trim();
                        //Session["ToDate"] = txtToDate.Text.Trim();
                    }
                    else
                    {                        
                        return;
                    }
                    break;
                }
            case "TRR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["ProjectID"] = ddlFundedBy.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text.Trim();
                    Session["ToDate"] = txtToDate.Text.Trim();
                    break;
                }
            case "INL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["ScheduleID"] = ddlSchedule.SelectedValue.ToString();
                    Session["MemoNO"]=txtMemoNO.Text.ToString();
                    Session["FromTime"]=txtFromTime.Text;
                    Session["ToTime"]=txtToTime.Text;
                    Session["ProvCost"]=txtProvCost.Text;
                    Session["InformDate"]= txtInformDate.Text;
                    Session["AttendDate"]=txtAttendDate.Text;
                    Session["Time"]=txtTime.Text;
                    Session["Dormitory"] = txtDormitory.Text;
                    break;
                }
            case "PRL":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["TrainingID"] = ddlTrainingName.SelectedValue.ToString();
                    Session["ScheduleID"] = ddlSchedule.SelectedValue.ToString();
                    Session["MemoNO"]=txtMemoNO.Text.ToString();
                    break;
                }
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open('frmTrainingReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    private void PanelVisibilityMst(string sPEmp, string sPrbtnLArea, string sPFiscalYr, string sPTrainingName, string sPTraningState, string sPLearningArea,
        string sPProgramDept, string sPrbtnCost, string sPQuarter, string sPrbtnAcive, string sPMonth, string sPYear, string sPDateRange, string sPShow,
        string sPActveInAcBasic, string sPTraingType, string sPOTType, string sSalaryLocation, string sEmployeeName,string sInvitationParam,string sSchedule)
    {
        if (sPEmp == "1")
            PEmp.Visible = true;
        else
            PEmp.Visible = false;

        if (sPrbtnLArea == "1")
            PrbtnLArea.Visible = true;
        else
            PrbtnLArea.Visible = false;

        if (sPFiscalYr == "1")

            PFiscalYr.Visible = true;
        else
            PFiscalYr.Visible = false;

        if (sPTrainingName == "1")
            PTrainingName.Visible = true;
        else
            PTrainingName.Visible = false;
        if (sPTraningState == "1")
            PTraningState.Visible = true;
        else
            PTraningState.Visible = false;

        if (sPLearningArea == "1")
            PFundedBy.Visible = true;
        else
            PFundedBy.Visible = false;

        if (sPProgramDept == "1")
            PProgramDept.Visible = true;
        else
            PProgramDept.Visible = false;

        if (sPrbtnCost == "1")
            PrbtnCost.Visible = true;
        else
            PrbtnCost.Visible = false;

        if (sPQuarter == "1")
            PQuarter.Visible = true;
        else
            PQuarter.Visible = false;

        if (sPrbtnAcive == "1")
            PrbtnAcive.Visible = true;
        else
            PrbtnAcive.Visible = false;

        if (sPMonth == "1")
            PMonth.Visible = true;
        else
            PMonth.Visible = false;

        if (sPDateRange == "1")
            PDateRange.Visible = true;
        else
            PDateRange.Visible = false;

        if (sPYear == "1")
            PYear.Visible = true;
        else
            PYear.Visible = false;

        if (sPShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;

        if (sPActveInAcBasic == "1")
            PActveInAcBasic.Visible = true;
        else
            PActveInAcBasic.Visible = false;

        if (sPTraingType == "1")
            PTraingType.Visible = true;
        else
            PTraingType.Visible = false;

        if (sPOTType == "1")
            pnlOTType.Visible = true;
        else
            pnlOTType.Visible = false;

        if (sSalaryLocation == "1")
            pSalaryLocation.Visible = true;
        else
            pSalaryLocation.Visible = false;

        if (sEmployeeName == "1")
            pEmployeeName.Visible = true;
        else
            pEmployeeName.Visible = false;
        if (sInvitationParam=="1")       
            invitation.Visible = true;
        else
            invitation.Visible=false;
        if (sSchedule == "1")
            pSchedule.Visible = true;
        else
            pSchedule.Visible = false;
    }

    protected void Bind_ddlFiscalYr()
    {
        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0), ddlFiscalYr, true);
    }

    protected void Bind_ddlProgDept()
    {
        Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlProgDept);
    }
    protected void Bind_ddlLearningArea()
    {
        Common.FillDropDownList_All(objMasMgr.SelectLearningArea(0), ddlFundedBy);
    }

    protected void Bind_ddlTrainingName()
    {
        Common.FillDropDownList_All(objMasMgr.SelectTrainingName(0, "A"), ddlTrainingName);
    }
    protected void ddlTrainingName_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (tvReports.SelectedValue)
        {
            case "TCR":
                {
                    int trainId=Convert.ToInt32(ddlTrainingName.SelectedValue);
                    if (trainId>-1)
                    {
                        Common.FillDropDownList_All(objEmpMgr.SelectPassedEmployee(trainId, "A"), ddlEmployeeName);
                    }
                    break;
                }
            case "INL":
            case "PRL":
                {
                    if (ddlTrainingName.SelectedIndex <= 0)
                        return;
                    if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim()) != "")
                    {
                        Common.FillDropDownList(objEmpMgr.SelectScheduleList(ddlTrainingName.SelectedValue.ToString().Trim()), ddlSchedule, "ScheDate", "ScheduleID", true);
                    }
                    break;
                }
        }
    }
}