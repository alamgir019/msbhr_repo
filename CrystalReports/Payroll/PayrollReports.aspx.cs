using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class CrystalReports_Payroll_PayrollReports : System.Web.UI.Page
{    
    MasterTablesManager MasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    DataTable dtBranchWiseDiv = new DataTable();
    DataTable dtSalDivision = new DataTable();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    ReportManager rptManager = new ReportManager();

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PanelVisibilityMst("0", "0","0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0");
            Common.FillYearList(5, ddlYear);
            DateTime now = DateTime.Now;
            ddlYear.SelectedValue = Convert.ToInt32(now.Year).ToString();
            Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
            Common.FillDropDownList_All(MasMgr.SelectDepartmentddl(0), ddlDept);
            Common.FillDropDownList_All(MasMgr.SelectGrade(0), ddlGrade);
            Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
    }
    //protected void Bind_ddlDivision()
    //{
    //    Common.FillDropDownList_All(MasMgr.SelectDivision(0), ddlDivision);
    //}

    protected void Bind_ddlDivision()
    {
        Common.FillDropDownList_All(MasMgr.SelectDivision(0), ddlCompany);
    }

    protected void Bind_ddlDept()
    {
        Common.FillDropDownList_All(MasMgr.SelectDepartment(0), ddlDept);
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.Bind_ddlDivision();        
    }
    
    protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tv = tvReports.SelectedValue.ToString();
        if (tv == "SSS")
        {
            if (ddlReportBy.SelectedValue.ToString().Equals("E"))
            {
                PEmpId.Visible = true;
                this.PGridSalSubLoc.Visible = false;
            }
            if (ddlReportBy.SelectedValue.ToString().Equals("D"))
            {
                PEmpId.Visible = false;
                this.PGridSalSubLoc.Visible = true;
            }
        }
        else if (tv == "YPFLD" || tv == "AI" || tv == "AITD" || tv == "TDR")
        {
            if (ddlReportBy.SelectedValue.ToString().Equals("E"))
            {
                PGridEmpList.Visible = true;
                this.PGridSalSubLoc.Visible = false;
            }
            if (ddlReportBy.SelectedValue.ToString().Equals("D"))
            {
                PGridEmpList.Visible = false;
                this.PGridSalSubLoc.Visible = true;
            }
        }
    }

    private void PanelVisibilityMst(string sSearchBy, string sBranch, string sCompany, string sDiv, string sDept, string sDate, string sShow, string sPostingDist,
        string sClosed, string PFisY, string PMonthFrom, string PMonTo, string PYear, string P_GridSalSubLoc, string PSalaryLocation, string PSalarySubLocation, string gvPost,
        string P_GridEmpList, string P_SCEl, string P_AV, string P_GridPostDist, string P_PayType, string P_EmpId, string PGrade, string PDesig,
        string PSalHead, string PSector, string Religion, string Fastival, string SalSource, string sQuarter, string sRptType, string sPTax,string sEmpType,
        string sP_Date, string sP_LT, string sP_ComText)
    {
        ddlReportBy.SelectedIndex = 0;
        if (sSearchBy == "1")
            PSearchBy.Visible = true;            
        else
            PSearchBy.Visible = false;
        if (sBranch == "1")
            PBranch.Visible = true;
        else
            PBranch.Visible = false;
        if (sCompany == "1")
            PCompany.Visible = true;
        else
            PCompany.Visible = false;
       
        if (sDept == "1")
            PDept.Visible = true;
        else
            PDept.Visible = false;
        if (sPostingDist == "1")
            PPostingDist.Visible = true;
        else
            PPostingDist.Visible = false;

        if (sDate == "1")
            pDate.Visible = true;
        else        
            pDate.Visible = false;            
        
        if (sShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;
        if (sClosed == "1")
            PClosed.Visible = true;
        else
            PClosed.Visible = false;
        if (PFisY == "1")
            this.PFisY.Visible = true;
        else
            this.PFisY.Visible = false;
        if (PMonthFrom == "1")
            this.PMonthFrom.Visible = true;
        else
            this.PMonthFrom.Visible = false;       
        if (PYear == "1")
            this.PYear.Visible = true;
        else
            this.PYear.Visible = false;
        if (P_GridSalSubLoc == "1")
            this.PGridSalSubLoc.Visible = true;
        else
            this.PGridSalSubLoc.Visible = false;
        if (PSalaryLocation == "1")
            this.P_SalaryLocation.Visible = true;
        else
            this.P_SalaryLocation.Visible = false;
        if (PSalarySubLocation == "1")
            this.P_SalarySubLocation.Visible = true;
        else
            this.P_SalarySubLocation.Visible = false;
        if (gvPost == "1")
            this.grPostDivision.Visible = true;
        else
            this.grPostDivision.Visible = false;
        if (P_GridEmpList == "1")
            this.PGridEmpList.Visible = true;
        else
            this.PGridEmpList.Visible = false;

        if(P_SCEl=="1")
            this.PSCEl.Visible = true;
            else
            this.PSCEl.Visible = false;
        if(P_AV=="1")
            this.P_AV.Visible = true;
        else
            this.P_AV.Visible = false;
        if (P_GridPostDist == "1")
            this.PGridPostDist.Visible = true;
        else
            this.PGridPostDist.Visible = false;

        if (P_PayType == "1")
            this.P_PayType.Visible = true;
        else
            this.P_PayType.Visible = false;

         if (P_EmpId == "1")
             this.PEmpId.Visible = true;
        else
             this.PEmpId.Visible = false;

        if (PGrade=="1")
            this.PGrade.Visible=true;
        else
            this.PGrade.Visible=false;

        if (PDesig == "1")
            this.PDesig.Visible = true;
        else
            this.PDesig.Visible = false;
        if (PSalHead == "1")
            this.P_SalHead.Visible = true;
        else
            this.P_SalHead.Visible = false;
        if (PSector == "1")
            this.P_Sector.Visible = true;
        else
            this.P_Sector.Visible = false;

        if(Religion =="1")
            this.P_Religion.Visible = true;
        else
            this.P_Religion.Visible = false;

         if(Fastival =="1")
             this.P_Festival.Visible = true;
        else
             this.P_Festival.Visible = false;

        if(SalSource =="1")
            this.P_SalSource.Visible = true;
        else
            this.P_SalSource.Visible = false;

        if(sQuarter =="1")
            this.P_Quarter.Visible = true;
        else
            this.P_Quarter.Visible = false;
        if (sRptType == "1")
            this.P_RptType.Visible = true;
        else
            this.P_RptType.Visible = false;
        if (sPTax == "1")
            this.PanelTax.Visible = true;
        else
            this.PanelTax.Visible = false;
        if (sEmpType == "1")
            this.pnlEmpType.Visible = true;
        else
            this.pnlEmpType.Visible = false;

        if (sP_Date == "1")
            this.P_Date.Visible = true;
        else
            this.P_Date.Visible = false;

        if (sP_LT == "1")
            this.P_LT.Visible = true;
        else
            this.P_LT.Visible = false;

        if (sP_ComText == "1")
            this.P_ComText.Visible = true;
        else
            this.P_ComText.Visible = false;
    }

    private void AddAllinDDL(DropDownList ddl)
    {
        ListItem lst;
        lst = new ListItem();
        lst.Text = "All";
        lst.Value = "0";

        ddl.Items.Add(lst);
    }

    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)
    {
        txtEmpCode.Text = ""; 
        PSearchBy.Enabled = true;
        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
        Common.FillMonthList(ddlMonthFrm);
        DateTime now = DateTime.Now;
        ddlMonthFrm.SelectedValue = Convert.ToInt32(now.Month).ToString();

        switch (tvReports.SelectedValue)
        {
            #region Salary
            case "ESPS":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    Common.FillDropDownList_All(MasMgr.SelectDesignation(0), this.ddlDesig);
                    break;
                }
            case "BSFF":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0");
                    break;
                }

            case "SSS":
            case "SSSum":
            case "SSL":
                {
                    PanelVisibilityMst("0", "1", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    Common.FillDropDownList_All(MasMgr.SelectDivision(), this.ddlCompany);
                    break;
                }
            case "PRLW":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    break;
                }
            case "CWSC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }

            #endregion
            #region PF
            case "MPFC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    //this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            case "YPFC":
            case "YPFB":
            case "YPFLD":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "PFLL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);

                    //this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            #endregion
            #region Final Payment
            case "FP":
            case "FPL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    break;
                }
            #endregion
            #region Employee Information
            case "ESI":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    break;
                }
            case "SEC":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    break;
                }
            case "SCH":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    //Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);                   
                    //Common.FillDropDownList_All(MasMgr.SelectDepartmentddl(0), ddlDept);
                    //Common.FillDropDownList_All(MasMgr.SelectGrade(0), ddlGrade);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
                    break;
                }

            #endregion
            #region Bonus
            case "BST":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlLocation);
                    //this.AddAllinDDL(ddlSubLoc);
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType,false);
                    Common.FillDropDownList_All(MasMgr.SelectReligionList(0), ddlReligion);
                    Common.FillDropDownList_All(MasMgr.SelectRelagionFestivalList(0), ddlFestival);

                    break;
                }
            case "BSTSum":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    break;
                }
            #endregion
            #region Tax
            case "AITD":            
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "AITMD":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "TC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "1");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    this.AddAllinDDL(ddlSubLoc);
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    this.lblCommon.Text = "Assessment Year :";

                    break;
                }
            case "STD":
                {
                    PanelVisibilityMst("0", "1", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlClinic);
                    Common.FillDropDownList_All(MasMgr.SelectDivision(), this.ddlCompany);
                    break;
                }

            #endregion
            #region Salary Reconcil
            case "SRR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    break;
                }
            #endregion
            #region Gratuity
            case "GBR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    break;
                }
                #endregion 
                #region Commit
                //case "ITC":
                //case "ITA":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        break;
                //    }
                //case "SS":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
                //        break;
                //    }
                //case "SalSum":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        this.AddAllinDDL(ddlSubLoc);
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "SSSEW":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();

                //        DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                //        gvSalSource.DataSource = dtSalSource;
                //        gvSalSource.DataBind();
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }

                //case "SSS2":
                //    {
                //        PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        break;
                //    }
                //case "ER":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();

                //        DataTable dtPostDiv = objPayMgr.SelectPostDist();
                //        grPostDivision.DataSource = dtPostDiv;
                //        grPostDivision.DataBind();

                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        break;
                //    }
                //case "TDR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "PBWC":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        DataTable dtPostDiv = objPayMgr.SelectPostDist();
                //        grPostDivision.DataSource = dtPostDiv;
                //        grPostDivision.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        break;
                //    }
                //case "SSWSD":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        DataTable dtPostDiv = MasMgr.SelectDistrict(0);
                //        grPostDivision.DataSource = dtPostDiv;
                //        grPostDivision.DataBind();
                //        DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                //        gvSalSource.DataSource = dtSalSource;
                //        gvSalSource.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        break;
                //    }

                //case "NSWSD":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        break;
                //    }
                //case "ADR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        Common.FillDropDownList(objPayMgr.SelectSalaryHeadCategoryWise(""), ddlSalHead, "HEADNAME", "SHEADID", true, "Select");
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }


                //case "SSS01":
                //    {
                //        PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        break;
                //    }
                //case "SSSOF":
                //    {
                //        PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "1", "1", "1", "1", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        break;
                //    }
                //case "SRDTL":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        // Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                //        break;
                //    }
                //case "SR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                //        break;
                //    }

                //case "AV":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "BV":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }

                //case "PRECC":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectSalDivision(0);
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        DataTable dtPostDiv = objPayMgr.SelectPostDist();
                //        grPostDivision.DataSource = dtPostDiv;
                //        grPostDivision.DataBind();
                //        break;
                //    }
                //case "ARA":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        break;
                //    }
                //#region Medical
                //case "MR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "MBB":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        ddlSubLoc.Items.Clear();
                //        Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "MBR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        ddlSubLoc.Items.Clear();
                //        Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "MMRR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        ddlSubLoc.Items.Clear();
                //        Common.FillMonthList_All(ddlMonthFrm);
                //        Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "MHRR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        ddlSubLoc.Items.Clear();
                //        Common.FillMonthList_All(ddlMonthFrm);
                //        Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                //        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //#endregion

                //case "FPDL":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        break;
                //    }

                //case "EBPS":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        ddlLocation.SelectedIndex = 0;
                //        this.AddAllinDDL(ddlSubLoc);
                //        Common.FillDropDownList(MasMgr.SelectReligionList(0), ddlReligion, true);
                //        Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "FBS":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0");
                //        Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                //        break;
                //    }
                //case "FBSW":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "1", "0", "0", "0");
                //        DataTable dtSalSource1 = objPayMgr.SelectSalSource(0);
                //        gvSalSource.DataSource = dtSalSource1;
                //        gvSalSource.DataBind();
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                //        Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "BSR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        this.AddAllinDDL(ddlSubLoc);
                //        Common.FillDropDownList(MasMgr.SelectReligionList(0), ddlReligion, true);
                //        Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //case "SBSR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0");
                //        break;
                //    }
                //case "GBR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0");
                //        break;
                //    }

                //case "OTC":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        this.AddAllinDDL(ddlSubLoc);
                //        Common.FillDropDownList_All(MasMgr.SelectDesignation(0), this.ddlDesig);
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }
                //#region PF
                //case "IPFC":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        Common.FillDropDownList_All(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear);
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }


                //case "AI":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                //        dtSalDivision = objPayMgr.SelectClinic();
                //        grSalDivision.DataSource = dtSalDivision;
                //        grSalDivision.DataBind();
                //        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                //        //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                //        break;
                //    }

                //#endregion

                //case "IR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0");
                //        Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                //        Common.FillMonthList_All(ddlMonthFrm);
                //        this.AddAllinDDL(ddlSubLoc);
                //        break;
                //    }
                //case "CPIL":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "1", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0");
                //        Common.FillDropDownList_All(MasMgr.SelectLocation(0), ddlPostingDist);
                //        break;
                //    }

                //case "AVL": 
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");

                //        break;
                //    }

                //case "MBP":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                //        break;
                //    }
                //case "NGOBSR":
                //    {
                //        PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");

                //        DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                //        gvSalSource.DataSource = dtSalSource;
                //        gvSalSource.DataBind();

                //        break;
                //    }
                #endregion
        }
        this.chkSalarySource.Checked=false;
        this.chkSelectEmp.Checked=false;
        this.chkPostDistAll.Checked=false;
        this.chkSelectAll.Checked = false;
    }

    private void ExPortReport(ReportDocument ReportDoc, string rptPath)
    {

        CrystalDecisions.Shared.ExportOptions CrExportOptions;
        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/CrystalReports/Payroll/VirtualReport/" + rptPath);
        CrExportOptions = ReportDoc.ExportOptions;
        {
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
        }
        ReportDoc.Export();


        ReportDoc.Close();
        ReportDoc.Dispose();
    }
    private void ExPortExcell(ReportDocument ReportDoc, string rptPath)
    {

        CrystalDecisions.Shared.ExportOptions CrExportOptions;
        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/CrystalReports/Payroll/VirtualReport/" + rptPath);
        CrExportOptions = ReportDoc.ExportOptions;
        {
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
        }
        ReportDoc.Export();

        ReportDoc.Close();
        ReportDoc.Dispose();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string fileName = "";
        string ReportPath = "";
        string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        DataTable MyDataTable = new DataTable();
        ReportDocument ReportDoc = new ReportDocument();
        switch (tvReports.SelectedValue)
        {          
            case "ESPS":
                {
                    //Session["ReportFormat"] = "pdf";
                    string SalDiv = ddlClinic.SelectedValue.ToString();
                    string FisYearText = ddlFisYear.SelectedItem.Text.ToString();
                    string FisYear = ddlFisYear.SelectedValue.ToString();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string Desig = ddlDesig.SelectedValue.ToString() == "99999" ? "0" : ddlDesig.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    string EmpId = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    string EmpTypeId = ddlEmpType.SelectedValue.ToString();

                    fileName = Session["USERID"].ToString() + "_" + "SalPaySlipAll" + ".pdf";

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalPaySlipAll.rpt");
                    MyDataTable = objPayRptMgr.Get_PayslipMonthlyAll(FisYear, VMonth, VYear, EmpId, Desig, SalDiv);
                    string pHeader = "Salary/Wages for the month of-- " + Common.ReturnFullMonthName(VMonth) + ", " + VYear;

                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("P_Header", pHeader);
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "BSFF":
                {
                    string DivisionId = ddlClinic.SelectedValue.ToString();
                    string FisYear = ddlFisYear.SelectedValue.ToString();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    string EmpTypeId = ddlEmpType.SelectedValue.ToString();
                    string SalType = rdbSalaryType.SelectedValue.Trim();

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBankStatement.rpt");
                    MyDataTable = objPayRptMgr.Get_Rpt_BankStatement(FisYear, VMonth, VYear, DivisionId, EmpTypeId,SalType);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("p_Month", Common.ReturnFullMonthName(VMonth));
                    ReportDoc.SetParameterValue("p_Year", VYear);
                    ReportDoc.SetParameterValue("p_SalaryType", SalType);
                    fileName = Session["USERID"].ToString() + "_" + "BankStatement" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SC":
                {
                    DateTime now = DateTime.Now;
                    string VMonth = Convert.ToInt32(now.Month).ToString();
                    string VYear = Convert.ToInt32(now.Year).ToString();
                    if (string.IsNullOrEmpty(txtEmpCode.Text.Trim()) == true)
                    {
                        lblMsg.Text = "Please Enter Employee Id. ";
                        return;
                    }
                    string EmpID = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    string rptType = rbtTax.SelectedValue.ToString();

                    
                    string strGender = "";
                    string strHeShe = "";
                    decimal dclFestivalBonus = 0;
                    decimal dclGratuity = 0;
                    decimal dclGrandTotal = 0;
                    decimal dclNetPay = 0;
                    decimal dclPF = 0;
                    if (rptType == "1")
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithTax.rpt");
                        ReportDoc.Load(ReportPath);
                    }
                    else
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithoutTax.rpt");
                        ReportDoc.Load(ReportPath);
                    }
                    MyDataTable = objPayRptMgr.Get_PaySleepWithTax(VMonth,VYear, EmpID);//, Session["SectorId"].ToString(), Session["PostingDistId"].ToString()
                    DataTable dt1 = MyDataTable;
                    if (dt1.Rows.Count > 0)
                    {
                        string EmpTypeID = (dt1.Rows[0]["EmpTypeID"]).ToString();
                        ReportDoc.SetParameterValue("P_Name", dt1.Rows[0]["FullName"]);
                        ReportDoc.SetParameterValue("P_Desig", dt1.Rows[0]["DesigName"]);
                        ReportDoc.SetParameterValue("P_Basic", String.Format("{0:0,0}", dt1.Rows[0]["P_Basic"]));
                        ReportDoc.SetParameterValue("P_HouseRent", String.Format("{0:0,0}", dt1.Rows[0]["P_HouseRent"]));
                        ReportDoc.SetParameterValue("P_Medical", String.Format("{0:0,0}", dt1.Rows[0]["P_Medical"]));
                        ReportDoc.SetParameterValue("P_Other", String.Format("{0:0,0}", dt1.Rows[0]["P_Other"]));
                        ReportDoc.SetParameterValue("P_Gross", String.Format("{0:0,0}", dt1.Rows[0]["P_Gross"]));
                        ReportDoc.SetParameterValue("P_PF", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_PF"])));
                        ReportDoc.SetParameterValue("P_TotalLoan", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_TotalLoan"])));
                        
                        dclPF = Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_PF"])));
                        ReportDoc.SetParameterValue("P_FBonus", String.Format("{0:0,0}", dclFestivalBonus));

                        //Joining Date to Current Date Calculation
                        DateTime dtJoiningDate = Convert.ToDateTime(dt1.Rows[0]["JoiningDate"]);
                        DateTime dtCurrDate = Convert.ToDateTime(DateTime.Now);

                        DateTime dtFrom = new DateTime();
                        DateTime dtTo = new DateTime();
                        double iTotDay = 0;
                        char[] splitter = { '/' };
                        string[] arinfo = Common.str_split(Common.DisplayDate(dtJoiningDate.ToString()), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }
                        arinfo = Common.str_split(Common.DisplayDate(dtCurrDate.ToString()), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }

                        TimeSpan Dur = dtTo.Subtract(dtFrom);

                        iTotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;

                        ReportDoc.SetParameterValue("P_Gratuaty", String.Format("{0:0,0}", dclGratuity));

                        dclGrandTotal = Convert.ToDecimal(dt1.Rows[0]["P_Gross"]) + dclFestivalBonus + dclGratuity;
                        ReportDoc.SetParameterValue("P_GrandTotal", String.Format("{0:0,0}", dclGrandTotal));

                        if (rptType == "1")
                        {
                            ReportDoc.SetParameterValue("P_IT", String.Format("{0:0,0}", dt1.Rows[0]["P_IT"]));
                            dclNetPay = dclGrandTotal - Convert.ToDecimal(dt1.Rows[0]["P_IT"]) - (dclPF * 2) - Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_TotalLoan"])));
                            ReportDoc.SetParameterValue("P_NetPay", String.Format("{0:0,0}", dclNetPay));
                        }
                        strGender = dt1.Rows[0]["Gender"].ToString();
                        if (strGender == "M")
                        {
                            strGender = "Mr. ";
                            strHeShe = " He";
                        }
                        else
                        {
                            strGender = "Ms. ";
                            strHeShe = " She";
                        }
                        ReportDoc.SetParameterValue("P_Body", "This is to certify that " + strGender + dt1.Rows[0]["FullName"] + ", " + dt1.Rows[0]["JobTitleName"] + " of " +
                            dt1.Rows[0]["DivisionName"].ToString() + ", " + dt1.Rows[0]["SectorName"].ToString() + " has been working in this organization since " +
                            dtJoiningDate.ToString("dd") + " " + dtJoiningDate.ToString("MMMM") + " " + dtJoiningDate.ToString("yyyy") + ". " +
                            strHeShe + " is a " + dt1.Rows[0]["TypeName"].ToString() + " employee of the organization. As per our service rule/terms of employment his date of retirement in N/A. " +
                            strHeShe + " is working in our clinic division/department as a " + dt1.Rows[0]["JobTitleName"] + ".");
                        if (dt1.Rows[0]["Gender"].ToString() == "M")
                            strGender = "His ";
                        else
                            strGender = "Her ";

                        ReportDoc.SetParameterValue("P_SalaryTitle", strGender + "current salary (monthly) statement is as follows:");

                        ReportDoc.SetParameterValue("P_date", now.ToString("MMMM") + " " + now.ToString("dd") + ", " + now.ToString("yyyy"));
                        ReportDoc.SetParameterValue("p_He_She", strHeShe);
                    }
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummeryEmpWise" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSS":
                {                 
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["Company"] = ddlCompany.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummeryEmpWise.rpt");
                    MyDataTable = objPayRptMgr.Get_Salary_SheetEmpWise(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["Company"].ToString() );
                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"] + "/" + Session["VYear"]));
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummeryEmpWise" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSSum":
                {

                    string FisYear = ddlFisYear.SelectedValue.ToString();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    string SalDiv = ddlClinic.SelectedValue.ToString();
                    string EmpTypeId = ddlEmpType.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_SheetSummary(VMonth, FisYear, SalDiv);

                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet Summary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSL":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["Company"] = ddlCompany.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalaryList.rpt");
                    MyDataTable = objPayRptMgr.Get_Salary_SheetEmpWise(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["Company"].ToString());
                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString()  + "/" + Session["VYear"].ToString() ));
                    ReportDoc.SetParameterValue("P_Header", "Staff List for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("P_CompName", ddlCompany.SelectedItem.Text.Trim());  
                    ReportDoc.SetParameterValue("P_CompAddr", "Address : House no 6/2, Flock-F, Lalmatia Housing Estate, Dhaka- 1207");
                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "StaffSalaryList" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "ESI":          
                {
                    //string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    //string EmpStatus = strEmpType;
                    //string DeptId = ddlDept.SelectedValue;
                    //string ClinicId = ddlDivision.SelectedValue;
                    //string GradeId = ddlGrade.SelectedValue.ToString();
                    //string EmpId = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    //string EmpTypeID = ddlEmpType.SelectedValue;

                    Session["DeptId"] = ddlDept.SelectedValue;
                    Session["ClinicId"] = ddlClinic.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;

                    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpSalaryInfo.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetEmpSalaryInfo(Session["EmpId"].ToString(), Session["GradeId"].ToString(), Session["ClinicId"].ToString(), Session["DeptId"].ToString(), Session["EmpTypeID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SCH":     
                {
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    string EmpStatus = strEmpType;
                    string DeptId = ddlDept.SelectedValue;
                    string ClinicId = ddlClinic.SelectedValue;
                    string GradeId = ddlGrade.SelectedValue.ToString();
                    string EmpId = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    string EmpTypeID = ddlEmpType.SelectedValue;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryHistory.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetEmpSalaryHistoryInfo(EmpId, GradeId, ClinicId, DeptId, EmpTypeID);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("pHeader", "Employee Salary History");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SEC":
                {
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryExceptionCase.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetEmpSalaryExceptionCase(VMonth, VYear);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "YPFC":
                Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                Session["RptType"] = tvReports.SelectedValue.ToString();
                Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                Session["EmpTypeId"] = "1";
                Session["REPORTID"] = tvReports.SelectedNode.Value;

                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFContribution.rpt");
                MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFC", Session["EmpTypeId"].ToString());
               // DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["FisYear"].ToString()));
                ReportDoc.Load(ReportPath);
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("P_Header", "Yearly PF Contribution For The Fiscal Year " + Session["FisYearText"].ToString());
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                fileName = Session["USERID"].ToString() + "_" + "Yearly PF Contribution" + ".pdf";

                this.ExPortReport(ReportDoc, fileName);
                break;

            case "YPFB":
                //this.DivEmpLoad();    
                Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                Session["RptType"] = tvReports.SelectedValue.ToString();
                Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                Session["EmpTypeId"] = "1";
                Session["REPORTID"] = tvReports.SelectedNode.Value;
               
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFBalance.rpt");
                MyDataTable = objPayRptMgr.Get_YearlyPFBalance(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFB", Session["EmpTypeId"].ToString());               
                ReportDoc.Load(ReportPath);
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("P_Header", "Yearly PF Balance For The Fiscal Year " + Session["FisYearText"].ToString());
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                fileName = Session["USERID"].ToString() + "_" + "Yearly PF Balance" + ".pdf";
                this.ExPortReport(ReportDoc, fileName);
                break;
            case "PFLL":
                Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();              
                Session["REPORTID"] = tvReports.SelectedNode.Value;
                Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPFLoanLedger.rpt");
                MyDataTable = objPayRptMgr.GetPFLoanLedgerData(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["EmpId"].ToString().Trim()  );
                ReportDoc.Load(ReportPath);
                ReportDoc.SetDataSource(MyDataTable);

                ReportDoc.SetParameterValue("pMonthName", Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                fileName = Session["USERID"].ToString() + "_" + "PF Loan Ledger" + ".pdf";
                this.ExPortReport(ReportDoc, fileName);
                break;
            case "YPFLD":
                Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                Session["RptType"] = tvReports.SelectedValue.ToString();
                Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                Session["EmpTypeId"] = "1";
                Session["REPORTID"] = tvReports.SelectedNode.Value;

                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFLoanDeduct.rpt");
                MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFLD", Session["EmpTypeId"].ToString());
                ReportDoc.Load(ReportPath);
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("P_Header", "PF Loan Deduction For The Fiscal Year " + Session["FisYearText"].ToString());
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                fileName = Session["USERID"].ToString() + "_" + "PF Loan Deduction" + ".pdf";
                this.ExPortReport(ReportDoc, fileName);

                break;
            case "IPFC":
            case "AI":
            case "AITD":
                {
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();   
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyITDeduct.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "AITD", Session["EmpTypeId"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Staff Salary Tax Deduction for The Fiscal Year -" + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "Staff Salary Tax Deduction" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "AITMD":
                {
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = "1";
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthWsITDeduct.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_TaxDedMonthWise(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), Session["EmpTypeId"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Staff Salary Month Wise Tax Deduction for The Fiscal Year -" + Session["FisYearText"].ToString() + " Upto Month " + Common.retMonthName(Session["VMonth"].ToString()));
                    ReportDoc.SetParameterValue("P_Month", Session["VMonth"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "Staff Salary Month Wise Tax Deduction" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "TC":
                {
                    Session["SalLocId"] = ddlLocation.SelectedValue;
                    Session["SalSubLocId"] = ddlSubLoc.SelectedValue;
                    this.EmpLoad();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["AssessYear"] = txtCommon.Text.Trim();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FisYearTxt"] = ddlFisYear.SelectedItem.Text.Trim();

                    dsITStatement ds01 = new dsITStatement();
                    ds01.Tables.Remove("dtEMPINFO");
                    ds01.Tables.Remove("dtITDEPOSITRECORDS");

                    DataSet ItStatementds = new DataSet();

                    ItStatementds = objPayRptMgr.Get_Rpt_ITStatement(Session["FisYear"].ToString(), Session["EmpTypeId"].ToString(), Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["SalSubLocId"].ToString());

                    DataTable destinationTable = CopyDataTable(ItStatementds.Tables[0], ItStatementds.Tables[0].Rows.Count);
                    destinationTable.TableName = "dtEMPINFO";
                    ds01.Tables.Add(destinationTable.Copy());

                    DataTable destinationTable1 = CopyDataTable(ItStatementds.Tables[1], ItStatementds.Tables[1].Rows.Count);
                    destinationTable1.TableName = "dtITDEPOSITRECORDS";
                    ds01.Tables.Add(destinationTable1.Copy());


                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptITStatement.rpt");
                    ReportDoc.Load(ReportPath);

                    ReportDoc.SetDataSource(ds01);

                    ReportDoc.SetParameterValue("P_Header", "Annual salary certificate and advance tax deduction for the financial year" + Session["FisYearTxt"].ToString());
                    ReportDoc.SetParameterValue("P_FiscalYear", Session["FisYearTxt"].ToString());
                    ReportDoc.SetParameterValue("P_AssessYear", Session["AssessYear"].ToString());                   
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);

                    fileName = Session["USERID"].ToString() + "_" + "Annual Salary Certificate" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "TDR":
                {
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();  
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = "1";
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                   
                    break;
                }
            case "STD":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["Company"] = ddlCompany.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptITDedStatementList.rpt");
                    MyDataTable = objPayRptMgr.Get_ITDedStatement(Session["VMonth"].ToString() , Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["Company"].ToString());
                                       
                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"]));
                    ReportDoc.SetParameterValue("P_Header", "Statement of Tax Deduction at Source From Salary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "StatementofTaxDeductionList" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    string usdRate = "0";// objPayMgr.SelectUSDRate(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    //usdRate = usdRate == "" ? Session["USDRATE"].ToString() : usdRate;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalReconcilation.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_Reconcilation(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), usdRate, Session["EmpTypeId"].ToString());
                    DataTable dt1 = objPayRptMgr.Get_Salary_Reconcilation_Param(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), Session["EmpTypeId"].ToString());
                    //PassParamValue(dt1);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));// Convert.ToDateTime(Session["FromDate"].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        ReportDoc.SetParameterValue("NewJoindeEmpID", Common.ReturnZeroForNull(dt1.Rows[0]["NJEmpID"].ToString()));
                        ReportDoc.SetParameterValue("SeparatedEmpID", Common.ReturnZeroForNull(dt1.Rows[0]["SeprmpID"].ToString()));
                        ReportDoc.SetParameterValue("TNewJoin", Common.ReturnZeroForNull(dt1.Rows[0]["TNJ"].ToString()));
                        ReportDoc.SetParameterValue("LMTSN", Common.ReturnZeroForNull(dt1.Rows[0]["LMTSN"].ToString()));
                        ReportDoc.SetParameterValue("TNOS", Common.ReturnZeroForNull(dt1.Rows[0]["TNS"].ToString()));
                        ReportDoc.SetParameterValue("SS", Common.ReturnZeroForNull(dt1.Rows[0]["SS"].ToString()));
                        ReportDoc.SetParameterValue("PSN", Common.ReturnZeroForNull(dt1.Rows[0]["PSN"].ToString()));
                        ReportDoc.SetParameterValue("P_Header", "Salary Reconciliation Report " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    }
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "Salary Reconciliation" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }

            case "GBR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["Company"] = ddlCompany.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptGratuityBenefitsSummery.rpt");
                    MyDataTable = objPayRptMgr.Get_ITDedStatement(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["Company"].ToString());

                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"]));
                    ReportDoc.SetParameterValue("P_Header", "Statement of MSB Staffs Gratuity Balance" );

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "StatementofGratuityBalance" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "EBPS":
                {                   
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();                    
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();  
                    break;
                }
            
            case "SSSEW":
                {
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    this.DivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();   
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    this.SalSourceID();
                    break;
                }
            case "SSS01":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session.Remove("EmpId");
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();
                    if (ddlReportBy.SelectedValue.ToString().Equals("E"))
                    {
                        Session["EmpId"] = txtEmpCode.Text.Trim();
                    }
                    else
                    {
                        Session["EmpId"] = "";
                    }
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SS":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                          if(i==1)
                              strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                              strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                          i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();  
                    break;
                }
            case "SalSum":
                {                   
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();  
                    break;
                }
            case "SSSOF":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();

                            i++;
                        }
                    }
                    Session.Remove("EmpId");
                    if (ddlReportBy.SelectedValue.ToString().Equals("E"))                    
                        Session["EmpId"] = txtEmpCode.Text.Trim();                    
                    else                    
                        Session["EmpId"] = "";                    
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    break;
                }
            case "SRDTL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();   
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ARA":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                } 
            case "AV":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["BankAccNo"] = "";
                    Session["SalDiv"] = strSalDivision;
                    Session["VoucherType"] = ddlVType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "BV":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["BankAccNo"] = "";
                    Session["SalDiv"] = strSalDivision;
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
           
            case "PBWC":
                {  
                    this.SalLocDivEmpLoad();                   
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ER":
                {                   
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SSWSD":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }
            case "PRLW":
                {
                    this.SalLocDivEmpLoad();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    string SalDiv = Session["SalDiv"].ToString();
                    string PostDist = Session["PostDist"].ToString();
                    string EmpID = Session["EmpID"].ToString();
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPayrollRprLocWise.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_PayrollReportLocWise(VMonth, VYear, SalDiv, PostDist, EmpID);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Payroll for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "CWSC":
                {
                    this.SalLocDivEmpLoad();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    string SalDiv = Session["SalDiv"].ToString();
                    string PostDist = Session["PostDist"].ToString();
                    string EmpID = Session["EmpID"].ToString();
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptComWsSalaryCharging.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_CompWsSalaryCharging(VMonth, VYear, SalDiv, PostDist, EmpID);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Salary Charging for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "CompWsSalaryCharging" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "NSWSD":
                {
                    this.SalDivisionLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "PRECC":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();                    
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
                
            case "MR":
                {                   
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }          
            case "ETR":
                {
                    Session["SalDiv"] = ddlLocation.SelectedValue.ToString() == "-1" ? "" : ddlLocation.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesig.SelectedValue.ToString() == "99999" ? "" : ddlDesig.SelectedValue.ToString();
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
                    Session["SalDiv"] = ddlLocation.SelectedValue.ToString() == "-1" ? "" : ddlLocation.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesig.SelectedValue.ToString() == "99999" ? "" : ddlDesig.SelectedValue.ToString();
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

            case "ADR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalHead"] = ddlSalHead.SelectedValue.ToString();
                    Session["SalHeadText"] = ddlSalHead.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
           
            case "MBB":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();                   
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    this.SalLocDivEmpLoad();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MBR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    this.SalLocDivEmpLoad();  
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MMRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    Session["BenefitType"] = "M";
                    this.SalLocDivEmpLoad();  
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MHRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    Session["BenefitType"] = "H";
                    this.SalLocDivEmpLoad();  
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FP":
                Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                Session["VYear"] = ddlYear.SelectedValue.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                Session["REPORTID"] = tvReports.SelectedNode.Value;

                DataSet ds = new DataSet();
                ds = objPayRptMgr.Get_Rpt_FaynalPaymentList(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["FisYear"].ToString(),  Session["EmpID"].ToString(), ds);

                DataTable tableA = ds.Tables[0].Copy();
                DataTable tableB = ds.Tables[1].Copy();
                DataSet dso2 = new DataSet();

                tableA.TableName = "dtFaynalPaymentList";
                dso2.Tables.Add(tableA);
                if (tableB.Rows.Count > 0)
                {
                    tableB.TableName = "dtYearlyPFContribution";
                    dso2.Tables.Add(tableB);
                }
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptFinalPayment.rpt");
                ReportDoc.Load(ReportPath);

                ReportDoc.SetDataSource(dso2);
                                             
                fileName = Session["USERID"].ToString() + "_" + "FinalPaymentList" + ".pdf";

                this.ExPortReport(ReportDoc, fileName);
                break;
            case "FPL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FPDL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "BST":
                {
                    string FisYear = ddlFisYear.SelectedValue.ToString();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string Division = ddlClinic.SelectedValue.ToString();
                    string Religion = ddlReligion.SelectedValue.ToString();
                    string Festival = ddlFestival.SelectedValue.Trim();
                    string FestivalName = ddlFestival.SelectedValue.Trim() == "-1" ? " " : this.ddlFestival.SelectedItem.Text.ToString();
                    string EmpTypeId = ddlEmpType.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;
                    
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptdtBonusStatFastival.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_BonusStatementFastival(FisYear, VMonth, Division, Religion,Festival,EmpTypeId);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Bonus Sheet for the Month of - " + Common.ReturnFullMonthName(VMonth) + ", " + VYear);
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "BonusStatFastival" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "BSTSum":
                {
                    string FisYear = ddlFisYear.SelectedValue.ToString();
                    string VMonth = ddlMonthFrm.SelectedValue.ToString();
                    string VYear = ddlYear.SelectedValue.ToString();
                    string Type = ddlReportBy.SelectedValue.ToString();
                    string SalDiv = ddlClinic.SelectedValue.ToString();
                    string EmpTypeId = ddlEmpType.SelectedValue.ToString();
                    string REPORTID = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBonusSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_BonusSummary(VMonth, FisYear, SalDiv);

                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    ReportDoc.SetParameterValue("P_Header", "Bonus Statement Summary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    fileName = Session["USERID"].ToString() + "_" + "BonusSummery" + ".pdf";

                    this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "FBS":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();                   
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FBSW":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "" : this.ddlFestival.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["EmpID"] = this.txtEmpCode.Text.ToString() == "" ? "" : this.txtEmpCode.Text.ToString();
                    Session["Year"] = ddlFisYear.SelectedItem.ToString();
                    Session["FestivalName"] = ddlFestival.SelectedValue.Trim() == "-1" ? "" : ddlFestival.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }
            case "BSR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["Religion"] = ddlReligion.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();
                    Session["FestivalName"] = ddlFestival.SelectedValue.Trim() == "-1" ? " " : this.ddlFestival.SelectedItem.Text.ToString();
                    //Session["rbtEType"] = "1";// ddlEmpType.SelectedValue.Trim().ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SBSR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Quarter"] = ddlQuarter.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SBR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Quarter"] = ddlQuarter.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MPFC":
                {
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    int fyer = Convert.ToInt32(ddlFisYear.SelectedValue.ToString());
                    int month =Convert.ToInt32(ddlMonthFrm.SelectedValue.ToString());
                    string pmonth = GetPreviousMonth(month.ToString());
                    Session["FisYearP"] = fyer.ToString();
                    Session["VMonthP"] = pmonth;
                    this.DivEmpLoad();                            
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthlyPFContribution.rpt");
                    MyDataTable = objPayRptMgr.Get_MonthlyPFContribution(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["FisYearP"].ToString(), Session["VMonthP"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["FisYear"].ToString()));
                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("P_Header", "Monthly PF Contribution For The Fiscal Year " + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("P_Month", Common.ReturnFullMonthName(Session["VMonthP"].ToString()));
                    ReportDoc.SetParameterValue("C_Month", Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    fileName = Session["USERID"].ToString() + "_" + "Monthly PF Contribution" + ".pdf";
                    this.ExPortReport(ReportDoc, fileName);

                    break;
                }
            case "OTC":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalLocId"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLocId"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["DesigId"] = ddlDesig.SelectedValue;
                    Session["MonthName"] = ddlMonthFrm.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "ITC":
            case "ITA":
                {
                    this.DivEmpLoad();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "IR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["SalLocId"] = ddlLocation.SelectedValue;
                    Session["SalSubLocId"] = ddlSubLoc.SelectedValue;
                    Session["LetterType"] = ddlTypeOfletter.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    break;
                }
            case "CPIL":
                {
                    Session["LetterType"] = ddlTypeOfletter.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["PostingDist"] = this.ddlPostingDist.SelectedValue.ToString() == "-1" ? "0" : this.ddlPostingDist.SelectedValue.ToString();
                    Session["PrintDate"] = txtPrintDate.Text.ToString() == "" ? System.DateTime.Now.ToString() : Common.ReturnDate(txtPrintDate.Text.ToString());
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    break;
                }
            case "MBP":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "AVL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "NGOBSR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }
            
        }
        //Open New Window
        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        sb.Append("window.open('VirtualReport/" + fileName + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
        //sb.Append("window.open('PayrollReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());    
    }
    public DataTable CopyDataTable(DataTable dtSource, int iRowsNeeded)
    {

        if (dtSource.Rows.Count >= iRowsNeeded)
        {
            // cloned to get the structure of source
            DataTable dtDestination = dtSource.Clone();
            for (int i = 0; i < iRowsNeeded; i++)
            {
                dtDestination.ImportRow(dtSource.Rows[i]);
            }
            return dtDestination;
        }
        else
            return dtSource;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string fileName = "";
        string ReportPath = "";
        string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        DataTable MyDataTable = new DataTable();
        ReportDocument ReportDoc = new ReportDocument();
        switch (tvReports.SelectedValue)
        {
            case "ESPS":
                {
                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalPaySlipAll.rpt");
                    //ReportDoc.Load(ReportPath);

                    //Session["ReportFormat"] = "excel";


                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Desig"] = ddlDesig.SelectedValue.ToString() == "99999" ? "0" : ddlDesig.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "BSFF":
                {
                    Session["DivisionId"] = ddlClinic.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    break;
                }
            case "SC":
                {
                    DateTime now = DateTime.Now;
                    Session["VMonth"] = Convert.ToInt32(now.Month).ToString();
                    Session["VYear"] = Convert.ToInt32(now.Year).ToString();
                    if (string.IsNullOrEmpty(txtEmpCode.Text.Trim()) == true)
                    {
                        lblMsg.Text = "Please Enter Employee Id. ";
                        return;
                    }
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["rptType"] = rbtTax.SelectedValue.ToString();


                    //string strGender = "";
                    //string strHeShe = "";
                    //decimal dclFestivalBonus = 0;
                    //decimal dclGratuity = 0;
                    //decimal dclGrandTotal = 0;
                    //decimal dclNetPay = 0;
                    //decimal dclPF = 0;
                    //if (rptType == "1")
                    //{
                    //    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithTax.rpt");
                    //    ReportDoc.Load(ReportPath);
                    //}
                    //else
                    //{
                    //    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithoutTax.rpt");
                    //    ReportDoc.Load(ReportPath);
                    //}
                    //MyDataTable = objPayRptMgr.Get_PaySleepWithTax(VMonth, VYear, EmpID);//, Session["SectorId"].ToString(), Session["PostingDistId"].ToString()
                    //DataTable dt1 = MyDataTable;
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    string EmpTypeID = (dt1.Rows[0]["EmpTypeID"]).ToString();
                    //    ReportDoc.SetParameterValue("P_Name", dt1.Rows[0]["FullName"]);
                    //    ReportDoc.SetParameterValue("P_Desig", dt1.Rows[0]["DesigName"]);
                    //    ReportDoc.SetParameterValue("P_Basic", String.Format("{0:0,0}", dt1.Rows[0]["P_Basic"]));
                    //    ReportDoc.SetParameterValue("P_HouseRent", String.Format("{0:0,0}", dt1.Rows[0]["P_HouseRent"]));
                    //    ReportDoc.SetParameterValue("P_Medical", String.Format("{0:0,0}", dt1.Rows[0]["P_Medical"]));
                    //    ReportDoc.SetParameterValue("P_Other", String.Format("{0:0,0}", dt1.Rows[0]["P_Other"]));
                    //    ReportDoc.SetParameterValue("P_Gross", String.Format("{0:0,0}", dt1.Rows[0]["P_Gross"]));
                    //    ReportDoc.SetParameterValue("P_PF", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_PF"])));
                    //    ReportDoc.SetParameterValue("P_TotalLoan", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_TotalLoan"])));

                    //    dclPF = Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_PF"])));
                    //    ReportDoc.SetParameterValue("P_FBonus", String.Format("{0:0,0}", dclFestivalBonus));

                    //    //Joining Date to Current Date Calculation
                    //    DateTime dtJoiningDate = Convert.ToDateTime(dt1.Rows[0]["JoiningDate"]);
                    //    DateTime dtCurrDate = Convert.ToDateTime(DateTime.Now);

                    //    DateTime dtFrom = new DateTime();
                    //    DateTime dtTo = new DateTime();
                    //    double iTotDay = 0;
                    //    char[] splitter = { '/' };
                    //    string[] arinfo = Common.str_split(Common.DisplayDate(dtJoiningDate.ToString()), splitter);
                    //    if (arinfo.Length == 3)
                    //    {
                    //        dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    //        arinfo = null;
                    //    }
                    //    arinfo = Common.str_split(Common.DisplayDate(dtCurrDate.ToString()), splitter);
                    //    if (arinfo.Length == 3)
                    //    {
                    //        dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    //        arinfo = null;
                    //    }

                    //    TimeSpan Dur = dtTo.Subtract(dtFrom);

                    //    iTotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;

                    //    ReportDoc.SetParameterValue("P_Gratuaty", String.Format("{0:0,0}", dclGratuity));

                    //    dclGrandTotal = Convert.ToDecimal(dt1.Rows[0]["P_Gross"]) + dclFestivalBonus + dclGratuity;
                    //    ReportDoc.SetParameterValue("P_GrandTotal", String.Format("{0:0,0}", dclGrandTotal));

                    //    if (rptType == "1")
                    //    {
                    //        ReportDoc.SetParameterValue("P_IT", String.Format("{0:0,0}", dt1.Rows[0]["P_IT"]));
                    //        dclNetPay = dclGrandTotal - Convert.ToDecimal(dt1.Rows[0]["P_IT"]) - (dclPF * 2) - Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_TotalLoan"])));
                    //        ReportDoc.SetParameterValue("P_NetPay", String.Format("{0:0,0}", dclNetPay));
                    //    }
                    //    strGender = dt1.Rows[0]["Gender"].ToString();
                    //    if (strGender == "M")
                    //    {
                    //        strGender = "Mr. ";
                    //        strHeShe = " He";
                    //    }
                    //    else
                    //    {
                    //        strGender = "Ms. ";
                    //        strHeShe = " She";
                    //    }
                    //    ReportDoc.SetParameterValue("P_Body", "This is to certify that " + strGender + dt1.Rows[0]["FullName"] + ", " + dt1.Rows[0]["JobTitleName"] + " of " +
                    //        dt1.Rows[0]["DivisionName"].ToString() + "," + dt1.Rows[0]["SectorName"].ToString() + " has been working in this organization since " +
                    //        dtJoiningDate.ToString("dd") + " " + dtJoiningDate.ToString("MMMM") + " " + dtJoiningDate.ToString("yyyy") + "." +
                    //        strHeShe + " is a " + dt1.Rows[0]["TypeName"].ToString() + " employee of the organization. As per our service rule/terms of employment his date of retirement in N/A." +
                    //        strHeShe + " is working in our clinic division/department as a " + dt1.Rows[0]["JobTitleName"] + ".");

                    //    if (dt1.Rows[0]["Gender"].ToString() == "M")
                    //        strGender = "His ";
                    //    else
                    //        strGender = "Her ";

                    //    ReportDoc.SetParameterValue("P_SalaryTitle", strGender + "current salary (monthly) statement is as follows:");

                    //    ReportDoc.SetParameterValue("P_date", now.ToString("MMMM") + " " + now.ToString("dd") + ", " + now.ToString("yyyy"));
                    //    ReportDoc.SetParameterValue("p_He_She", strHeShe);
                    //}
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummeryEmpWise" + ".pdf";
                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSS":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummeryEmpWise.rpt");
                    //MyDataTable = objPayRptMgr.Get_Salary_SheetEmpWise(VMonth, FisYear, SalDiv);
                    //ReportDoc.Load(ReportPath);
                    //ReportDoc.SetDataSource(MyDataTable);
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    //ReportDoc.SetParameterValue("P_Header", "Salary Sheet for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    //ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummeryEmpWise" + ".pdf";

                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSSum":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummery.rpt");
                    //ReportDoc.Load(ReportPath);
                    //MyDataTable = objPayRptMgr.Get_Salary_SheetSummary(VMonth, FisYear, SalDiv);

                    //ReportDoc.SetDataSource(MyDataTable);
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    //ReportDoc.SetParameterValue("P_Header", "Salary Sheet Summary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    //ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SSL":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;                    
                    break;
                }
            case "ESI":
                {
                    Session["DeptId"] = ddlDept.SelectedValue;
                    Session["ClinicId"] = ddlClinic.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    //ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpSalaryInfo.rpt");
                    //ReportDoc.Load(ReportPath);
                    //MyDataTable = rptManager.GetEmpSalaryInfo(EmpId, GradeId, ClinicId, DeptId, EmpTypeID);
                    //ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "CWSC":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalDiv"] = Session["SalDiv"].ToString();

                    Session["PostDist"] = Session["PostDist"].ToString();
                    Session["EmpID"] = Session["EmpID"].ToString();
                    break;
                }
            case "SCH":
                {
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    Session["EmpStatus"] = strEmpType;
                    Session["DeptId"] = ddlDept.SelectedValue;
                    Session["ClinicId"] = ddlClinic.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeID"] = ddlEmpType.SelectedValue;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryHistory.rpt");
                    //ReportDoc.Load(ReportPath);
                    //MyDataTable = rptManager.GetEmpSalaryHistoryInfo(EmpId, GradeId, ClinicId, DeptId, EmpTypeID);
                    //ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("pHeader", "Employee Salary History");
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";
                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "SEC":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryExceptionCase.rpt");
                    //ReportDoc.Load(ReportPath);
                    //MyDataTable = rptManager.GetEmpSalaryExceptionCase(VMonth, VYear);
                    //ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";
                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
         
            case "AITD":            
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                break;
            case "YPFC":
            case "YPFB":
            case "YPFLD":
            case "IPFC":
            case "AI":
            case "TDR":
                {
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlClinic.SelectedValue.ToString();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();   
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }

            case "EBPS":
                {
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }

            case "SSSEW":
                {
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    this.DivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    this.SalSourceID();
                    break;
                }
            case "SSS01":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session.Remove("EmpId");
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();
                    if (ddlReportBy.SelectedValue.ToString().Equals("E"))
                    {
                        Session["EmpId"] = txtEmpCode.Text.Trim();
                    }
                    else
                    {
                        Session["EmpId"] = "";
                    }
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SS":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "SalSum":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "SSSOF":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();

                            i++;
                        }
                    }
                    Session.Remove("EmpId");
                    if (ddlReportBy.SelectedValue.ToString().Equals("E"))
                        Session["EmpId"] = txtEmpCode.Text.Trim();
                    else
                        Session["EmpId"] = "";
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["SalDiv"] = strSalDivision;
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    break;
                }
            case "SRDTL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ARA":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "AV":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["BankAccNo"] = "";
                    Session["SalDiv"] = strSalDivision;
                    Session["VoucherType"] = ddlVType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "BV":
                {
                    string strSalDivision = "";
                    int i = 1;
                    foreach (GridViewRow gRow in grSalDivision.Rows)
                    {
                        CheckBox chBox = new CheckBox();
                        chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                        if (chBox.Checked == true)
                        {
                            if (i == 1)
                                strSalDivision = gRow.Cells[1].Text.Trim();
                            else
                                strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["BankAccNo"] = "";
                    Session["SalDiv"] = strSalDivision;
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "PBWC":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ER":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SSWSD":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }
            case "PRLW":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPayrollRprLocWise.rpt");
                    //ReportDoc.Load(ReportPath);
                    //MyDataTable = objPayRptMgr.Get_Rpt_PayrollReportLocWise(VMonth, VYear, SalDiv, PostDist, EmpID);
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + VMonth + "/" + VYear));
                    //ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("P_Header", "Payroll for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    //ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    //fileName = Session["USERID"].ToString() + "_" + "SalSheetSummery" + ".pdf";

                    //this.ExPortReport(ReportDoc, fileName);
                    break;
                }
            case "NSWSD":
                {
                    this.SalDivisionLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "PRECC":
                {
                    this.SalLocDivEmpLoad();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }

            case "MR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ETR":
                {
                    Session["SalDiv"] = ddlLocation.SelectedValue.ToString() == "-1" ? "" : ddlLocation.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesig.SelectedValue.ToString() == "99999" ? "" : ddlDesig.SelectedValue.ToString();
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
                    Session["SalDiv"] = ddlLocation.SelectedValue.ToString() == "-1" ? "" : ddlLocation.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Grade"] = ddlGrade.SelectedValue.ToString() == "99999" ? "" : ddlGrade.SelectedValue.ToString();
                    Session["Desig"] = ddlDesig.SelectedValue.ToString() == "99999" ? "" : ddlDesig.SelectedValue.ToString();
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

            case "ADR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalHead"] = ddlSalHead.SelectedValue.ToString();
                    Session["SalHeadText"] = ddlSalHead.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }

            case "MBB":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    this.SalLocDivEmpLoad();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MBR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    this.SalLocDivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MMRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    Session["BenefitType"] = "M";
                    this.SalLocDivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MHRR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalSunLocID"] = ddlSubLoc.SelectedValue.ToString();
                    Session["BenefitType"] = "H";
                    this.SalLocDivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FP":
                Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                Session["VYear"] = ddlYear.SelectedValue.ToString();
                Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                Session["REPORTID"] = tvReports.SelectedNode.Value;

                //DataSet ds = new DataSet();
                //ds = objPayRptMgr.Get_Rpt_FaynalPaymentList(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["FisYear"].ToString(), Session["EmpID"].ToString(), ds);

                //DataTable tableA = ds.Tables[0].Copy();
                //DataTable tableB = ds.Tables[1].Copy();
                //DataSet dso2 = new DataSet();

                //tableA.TableName = "dtFaynalPaymentList";
                //dso2.Tables.Add(tableA);
                //tableB.TableName = "dtYearlyPFContribution";
                //dso2.Tables.Add(tableB);

                //ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptFinalPayment.rpt");
                //ReportDoc.Load(ReportPath);

                //ReportDoc.SetDataSource(dso2);

                //fileName = Session["USERID"].ToString() + "_" + "FinalPaymentList" + ".pdf";

                //this.ExPortReport(ReportDoc, fileName);
                break;
            case "FPL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FPDL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.ToString() == "" ? "" : txtEmpCode.Text.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "BST":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString();
                    //Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["Religion"] = ddlReligion.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim();
                    Session["FestivalName"] = ddlFestival.SelectedValue.Trim() == "-1" ? " " : this.ddlFestival.SelectedItem.Text.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FBS":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "FBSW":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "" : this.ddlFestival.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["EmpID"] = this.txtEmpCode.Text.ToString() == "" ? "" : this.txtEmpCode.Text.ToString();
                    Session["Year"] = ddlFisYear.SelectedItem.ToString();
                    Session["FestivalName"] = ddlFestival.SelectedValue.Trim() == "-1" ? "" : ddlFestival.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }
            case "BSR":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["SalLoc"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLoc"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["Religion"] = ddlReligion.SelectedValue.ToString();
                    Session["Festival"] = ddlFestival.SelectedValue.Trim() == "-1" ? "0" : this.ddlFestival.SelectedValue.ToString();
                    Session["FestivalName"] = ddlFestival.SelectedValue.Trim() == "-1" ? " " : this.ddlFestival.SelectedItem.Text.ToString();
                    //Session["rbtEType"] = "1";// ddlEmpType.SelectedValue.Trim().ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SBSR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Quarter"] = ddlQuarter.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "SBR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["Quarter"] = ddlQuarter.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "MPFC":
                {
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    int fyer = Convert.ToInt32(ddlFisYear.SelectedValue.ToString());
                    int month = Convert.ToInt32(ddlMonthFrm.SelectedValue.ToString());
                    string pmonth = GetPreviousMonth(month.ToString());
                    Session["FisYearP"] = fyer.ToString();
                    Session["VMonthP"] = pmonth;
                    this.DivEmpLoad();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;  

                    break;
                }
            case "PFLL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    break;
                }
            case "OTC":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["SalLocId"] = this.ddlLocation.SelectedValue.ToString() == "-1" ? "0" : this.ddlLocation.SelectedValue.ToString();
                    Session["SalSubLocId"] = this.ddlSubLoc.SelectedValue.ToString() == "-1" ? "0" : this.ddlSubLoc.SelectedValue.ToString();
                    Session["DesigId"] = ddlDesig.SelectedValue;
                    Session["MonthName"] = ddlMonthFrm.SelectedItem.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    break;
                }
            case "ITC":
            case "ITA":
                {
                    this.DivEmpLoad();
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "IR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpID"] = txtEmpCode.Text.Trim();
                    Session["SalLocId"] = ddlLocation.SelectedValue;
                    Session["SalSubLocId"] = ddlSubLoc.SelectedValue;
                    Session["LetterType"] = ddlTypeOfletter.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    break;
                }
            case "CPIL":
                {
                    Session["LetterType"] = ddlTypeOfletter.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["PostingDist"] = this.ddlPostingDist.SelectedValue.ToString() == "-1" ? "0" : this.ddlPostingDist.SelectedValue.ToString();
                    Session["PrintDate"] = txtPrintDate.Text.ToString() == "" ? System.DateTime.Now.ToString() : Common.ReturnDate(txtPrintDate.Text.ToString());
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

                    break;
                }
            case "MBP":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "AVL":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "NGOBSR":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    this.SalSourceID();
                    break;
                }

            case "TC":
                {
                    Session["SalLocId"] = ddlLocation.SelectedValue;
                    Session["SalSubLocId"] = ddlSubLoc.SelectedValue;
                    this.EmpLoad();  //Session["EmpID"]
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["AssessYear"] = txtCommon.Text.Trim();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["FisYearTxt"] = ddlFisYear.SelectedItem.Text.Trim();

                    break;
                }
        }
        //Open New Window
        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        //sb.Append("window.open('VirtualReport/" + fileName + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
        sb.Append("window.open('PayrollReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
   
    private void EmpLoad()
    {
        string strEmpId = "";
        int k = 1;
        foreach (GridViewRow gRow in gvEmp.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
            if (chBox.Checked == true)
            {
                if (k == 1)
                    strEmpId = gRow.Cells[1].Text.Trim();
                else
                    strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                k++;
            }
        }

        Session["EmpID"] = strEmpId;
    }

    private void DivEmpLoad()
    {
        string strSalDivision = "";
        int i = 1;
        foreach (GridViewRow gRow in grSalDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                if (i == 1)
                    strSalDivision = gRow.Cells[1].Text.Trim();
                else
                    strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();
                i++;
            }
        }

        string strEmpId = "";
        int k = 1;
        foreach (GridViewRow gRow in gvEmp.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
            if (chBox.Checked == true)
            {
                if (k == 1)
                    strEmpId = gRow.Cells[1].Text.Trim();
                else
                    strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                k++;
            }
        }
        Session["SalDiv"] = strSalDivision;
        Session["EmpID"] = strEmpId;
    }

    private string GetPreviousMonth(string month)
    {
        string pmonth = "";
        switch (month)
        {
            case "1":
                {
                    pmonth = "12";
                    break;
                }
            case "2":
                {
                    pmonth = "1";
                    break;
                }
            case "3":
                {
                    pmonth = "2";
                    break;
                }
            case "4":
                {
                    pmonth = "3";
                    break;
                }
            case "5":
                {
                    pmonth = "4";
                    break;
                }
            case "6":
                {
                    pmonth = "5";
                    break;
                }
            case "7":
                {
                    pmonth = "6";
                    break;
                }
            case "8":
                {
                    pmonth = "7";
                    break;
                }
            case "9":
                {
                    pmonth = "8";
                    break;
                }
            case "10":
                {
                    pmonth = "9";
                    break;
                }
            case "11":
                {
                    pmonth = "10";
                    break;
                }
            case "12":
                {
                    pmonth = "11";
                    break;
                }
        }
        return pmonth;
    }
    private void SalSourceID()
    {
        string strSalarySourceId = "";
        int i = 1;
        int j = 0;
        foreach (GridViewRow gRow in gvSalSource.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkSalarySourceId");
            if (chBox.Checked == true)
            {
                if (i == 1)
                    strSalarySourceId = gvSalSource.DataKeys[j].Values[0].ToString().Trim();
                else
                    strSalarySourceId = strSalarySourceId + "," + gvSalSource.DataKeys[j].Values[0].ToString().Trim();
                i++;
            }
            j++;
        }
        Session["SalSourceID"] = strSalarySourceId;
    }
    private void SalLocDivEmpLoad()
    {
        string strSalSubLock = "";
        int i = 1;
        foreach (GridViewRow gRow in grSalDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                if (i == 1)
                    strSalSubLock = gRow.Cells[1].Text.Trim();
                else
                    strSalSubLock = strSalSubLock + "," + gRow.Cells[1].Text.Trim();
                i++;
            }
        }

        string strPostDist = "";
        int j = 1;
        foreach (GridViewRow gRow in grPostDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxPost");
            if (chBox.Checked == true)
            {
                if (j == 1)
                    strPostDist = gRow.Cells[1].Text.Trim();
                else
                    strPostDist = strPostDist + "," + gRow.Cells[1].Text.Trim();
                j++;
            }
        }

        string strEmpId = "";
        int k = 1;
        foreach (GridViewRow gRow in gvEmp.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
            if (chBox.Checked == true)
            {
                if (k == 1)
                    strEmpId = gRow.Cells[1].Text.Trim();
                else
                    strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                k++;
            }
        }
        Session["SalDiv"] = strSalSubLock;
        Session["PostDist"] = strPostDist;
        Session["EmpID"] = strEmpId;
    }

    private void SalDivisionLoad()
    {
        string strSalSubLock = "";
        int i = 1;
        foreach (GridViewRow gRow in grSalDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                if (i == 1)
                    strSalSubLock = gRow.Cells[1].Text.Trim();
                else
                    strSalSubLock = strSalSubLock + "," + gRow.Cells[1].Text.Trim();
                i++;
            }
        }
        Session["SalDiv"] = strSalSubLock;
    }

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (tvReports.SelectedValue)
        {
            case "DA":
            case "AE":
            case "LR":
            case "AR":
            case "IR":
            case "ED":
            case "DailyA":
            case "MonthlyA":
            case "InvOT":
            case "SummOT":
            case "EmpInfo":
            case "EmpStr":
            case "SalDeduct":
            case "OTCon":
            case "MA":
            case "Shf":
            case "EWOS":
            case "MSR":
            case "MSR0":
            case "Prm":
            case "EAI":
            case "SumAttnd":
                break;
        }
    }

    public int GetMonthDay(DateTime dtDate)
    {
        int intDay = 0;
        switch (dtDate.Month.ToString())
        {
            case "1":
            case "3":
            case "5":
            case "7":
            case "8":
            case "10":
            case "12":
                intDay = 31;
                break;
            case "4":
            case "6":
            case "9":
            case "11":
                intDay = 30;
                break;
            case "2":
                decimal a = Convert.ToDecimal(dtDate.Year);
                decimal b = 4;
                decimal Rem;
                Rem = decimal.Remainder(a, b);
                if (Rem == 0)
                {
                    intDay = 29;
                }
                else
                {
                    intDay = 28;
                }
                break;
        }
        return intDay;

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectAll.Checked == true)
        {
            foreach (GridViewRow row in grSalDivision.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBox");
                chkcheck.Checked = false ;
            }
        }

        else
        {
            foreach (GridViewRow row in grSalDivision.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBox");
                chkcheck.Checked = false;
            }
        }
    }
    
    protected void chkPostDistAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPostDistAll.Checked == true)
        {
            foreach (GridViewRow row in grPostDivision.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBoxPost");
                chkcheck.Checked = false ;
            }
        }
        else
        {
            foreach (GridViewRow row in grPostDivision.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBoxPost");
                chkcheck.Checked = false;
            }
        }
    }

    protected void chkSelectAllEmp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectAll.Checked == true)
        {
            foreach (GridViewRow row in gvEmp.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBoxEmp");
                chkcheck.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow row in gvEmp.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkBoxEmp");
                chkcheck.Checked = false;
            }
        }
    }   
    
    protected void chkSalarySourceId_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSalarySource.Checked == true)
        {
            foreach (GridViewRow row in gvSalSource.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkSalarySourceId");
                chkcheck.Checked = false ;
            }
        }
        else
        {
            foreach (GridViewRow row in gvSalSource.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkSalarySourceId");
                chkcheck.Checked = false;
            }
        }
    }
      
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubLoc.Items.Clear();
        ddlSubLoc.Dispose();

        Common.FillDropDownList_All(MasMgr.SelectSalaryLocWiseSubLoc(Convert.ToInt32(ddlLocation.SelectedValue)), ddlSubLoc);
    }
    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        string strSalDivision = "";
        int i = 1;
        foreach (GridViewRow gRow in grSalDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                if (i == 1)
                    strSalDivision = gRow.Cells[1].Text.Trim();
                else
                    strSalDivision = strSalDivision + "," + gRow.Cells[1].Text.Trim();

                i++;
            }
        }


        string strPostDivision = "";
        int j = 1;
        foreach (GridViewRow gRow in grPostDivision.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxPost");
            if (chBox.Checked == true)
            {
                if (j == 1)
                    strPostDivision = gRow.Cells[1].Text.Trim();
                else
                    strPostDivision = strPostDivision + "," + gRow.Cells[1].Text.Trim();
                j++;
            }
        }

        if (strPostDivision == "" && strSalDivision=="")
        {
            lblMsg.Text = "Please select Posting Division!";
            return;
        }

       string strEmpType=radBtnListEmp.SelectedValue.Trim().ToString();
       DataTable dtEmployeList = objPayMgr.SelectEmployeeList(strSalDivision, strPostDivision, strEmpType);
       gvEmp.DataSource = dtEmployeList;
       gvEmp.DataBind();
    }

    protected void rbtnEmptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type=this.rbtnEmptype.SelectedValue.ToString();
        loadEmpDDL(type);
    }

    private void loadEmpDDL(string type)
    {
        Common.FillDropDownValue(objPayMgr.GetEmpListID(type), this.ddlEmp);
    }

    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList_Nil(MasMgr.SelectSectorWiseDepartment2(Convert.ToInt32(ddlSector.SelectedValue)), ddlDept);
    }

    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string religion = this.ddlReligion.SelectedValue.ToString();
        if (religion != "99999")
            Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(Convert.ToInt32(religion)), ddlFestival,true );
    }

    protected void radBtnListEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
    }

    private void FillEmpList(string strEmpStatus)
    {    
        DataTable dtEmp = MasMgr.SelectEmpList(strEmpStatus);
        gvEmp.DataSource = dtEmp;
        gvEmp.DataBind();
    }

    protected void rdbSalaryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
        DataTable dtEmployeList = objPayMgr.SelectEmployeeListPF(strEmpType);
        gvEmp.DataSource = dtEmployeList;
        gvEmp.DataBind();
    }
}
