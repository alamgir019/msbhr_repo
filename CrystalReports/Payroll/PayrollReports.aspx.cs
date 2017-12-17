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


public partial class CrystalReports_Payroll_PayrollReports : System.Web.UI.Page
{    
    MasterTablesManager MasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    DataTable dtBranchWiseDiv = new DataTable();
    DataTable dtSalDivision = new DataTable();

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
            PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0");
            Common.FillYearList(5, ddlYear);
            DateTime now = DateTime.Now;
            ddlYear.SelectedValue = Convert.ToInt32(now.Year).ToString();
            Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);
            Common.FillDropDownList_All(MasMgr.SelectDepartmentddl(0), ddlDept);
            Common.FillDropDownList_All(MasMgr.SelectGrade(0), ddlGrade);
            Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
        }
    }           
  
    protected void Bind_ddlDivision()
    {
        Common.FillDropDownList_All(MasMgr.SelectDivision(0), ddlDivision);
    }

    protected void Bind_ddlSBU()
    {
        Common.FillDropDownList_All(MasMgr.SelectClinic(), ddlSUB);
    }

    protected void Bind_ddlDept()
    {
        Common.FillDropDownList_All(MasMgr.SelectDepartment(0), ddlDept);
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_ddlDivision();        
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

    private void PanelVisibilityMst(string sSearchBy, string sBranch, string sDiv, string sDept, string sDate, string sShow, string sPostingDist,
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
        if (sDiv == "1")
            PDiv.Visible = true;
        else
            PDiv.Visible = false;
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
        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
        Common.FillMonthList(ddlMonthFrm);
        DateTime now = DateTime.Now;
        ddlMonthFrm.SelectedValue = Convert.ToInt32(now.Month).ToString();
        
        switch (tvReports.SelectedValue)
        {
            case "ESPS":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);
                    Common.FillDropDownList_All(MasMgr.SelectDesignation(0), this.ddlDesig);
                    break;
                } 
            case "BSFF":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0","0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0");
                    break;
                }

            case "SSS":
            case "SSSum":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);
                    break;
                }
            case "ESI":
            case "SCH":         
                {
                    PanelVisibilityMst("0", "1", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");                   
                    //Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);                   
                    //Common.FillDropDownList_All(MasMgr.SelectDepartmentddl(0), ddlDept);
                    //Common.FillDropDownList_All(MasMgr.SelectGrade(0), ddlGrade);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
                    break;
                }
            case "SEC":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");                   
                    break;
                }
            case "YPFC":
            case "YPFB":
            case "YPFLD":
                {
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    Common.FillDropDownList_All(objPayMgr.SelectClinic(), this.ddlDivision);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SS":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0, "Y"), ddlEmpType);
                    break;
                }
            case "SalSum":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    this.AddAllinDDL(ddlSubLoc);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SSSEW":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();

                    DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                    gvSalSource.DataSource = dtSalSource;
                    gvSalSource.DataBind();
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
           
            case "SSS2":
                {
                    PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    break;
                }
            case "ER":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();

                    DataTable dtPostDiv = objPayMgr.SelectPostDist();
                    grPostDivision.DataSource = dtPostDiv;
                    grPostDivision.DataBind();

                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            case "PBWC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    DataTable dtPostDiv = objPayMgr.SelectPostDist();
                    grPostDivision.DataSource = dtPostDiv;
                    grPostDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            case "SSWSD":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    DataTable dtPostDiv = MasMgr.SelectDistrict(0);
                    grPostDivision.DataSource = dtPostDiv;
                    grPostDivision.DataBind();
                    DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                    gvSalSource.DataSource = dtSalSource;
                    gvSalSource.DataBind();                   
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            case "PRLW":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    DataTable dtPostDiv = objPayMgr.SelectPostDist();
                    grPostDivision.DataSource = dtPostDiv;
                    grPostDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
            case "NSWSD":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    break;
                }
            case "ADR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectSalaryHeadCategoryWise(""), ddlSalHead, "HEADNAME", "SHEADID", true, "Select");
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SRR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    break;
                }
           
            case "SSS01":
                {
                    PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    break;
                }    
            case "SSSOF":
                {
                    PanelVisibilityMst("1", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "1", "1", "1", "1", "1", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    break;
                }
            case "SRDTL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                   // Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    break;
                }
            case "SR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    break;
                }
           
            case "AV":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "BV":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                   // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
          
            case "PRECC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectSalDivision(0);
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    DataTable dtPostDiv = objPayMgr.SelectPostDist();
                    grPostDivision.DataSource = dtPostDiv;
                    grPostDivision.DataBind();
                    break;
                }
            case "ARA":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            #region Medical
            case "MR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                   // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "MBB":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    ddlSubLoc.Items.Clear();
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "MBR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    ddlSubLoc.Items.Clear();
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "MMRR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    ddlSubLoc.Items.Clear();
                    Common.FillMonthList_All(ddlMonthFrm); 
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "MHRR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    ddlSubLoc.Items.Clear();
                    Common.FillMonthList_All(ddlMonthFrm); 
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "M"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            #endregion
            case "FP":
            case "FPL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "FPDL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "BST":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlLocation);
                    //this.AddAllinDDL(ddlSubLoc);
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType,false);
                    Common.FillDropDownList_All(MasMgr.SelectReligionList(0), ddlReligion);
                    Common.FillDropDownList_All(MasMgr.SelectRelagionFestivalList(0), ddlFestival);

                    break;
                }
            case "EBPS":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    ddlLocation.SelectedIndex = 0;
                    this.AddAllinDDL(ddlSubLoc);
                    Common.FillDropDownList(MasMgr.SelectReligionList(0), ddlReligion, true);
                    Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "FBS":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                    break;
                }
            case "FBSW":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "1", "0", "0", "0");
                    DataTable dtSalSource1 = objPayMgr.SelectSalSource(0);
                    gvSalSource.DataSource = dtSalSource1;
                    gvSalSource.DataBind();
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    Common.FillDropDownList_All(objPayMgr.SelectSalDivision(0), this.ddlSubLoc);
                    Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "BSR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    this.AddAllinDDL(ddlSubLoc);
                    Common.FillDropDownList(MasMgr.SelectReligionList(0), ddlReligion, true);
                    Common.FillDropDownList(MasMgr.SelectRelagionFestivalList(0), ddlFestival, true);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "SBSR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "SBR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0");
                    break;
                }
           
            case "OTC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    this.AddAllinDDL(ddlSubLoc);
                    Common.FillDropDownList_All(MasMgr.SelectDesignation(0), this.ddlDesig);
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }    
            #region PF
            case "IPFC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList_All(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            case "MPFC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    //this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }
           
            case "AI":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    //Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
            #endregion
            #region Tax
            case "AITD":
            case "TDR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                   // Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmpType);
                    break;
                }
           
            case "ITC":
            case "ITA":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    dtSalDivision = objPayMgr.SelectClinic();
                    grSalDivision.DataSource = dtSalDivision;
                    grSalDivision.DataBind();
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    break;
                }

            case "TC":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "1");
                    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    this.AddAllinDDL(ddlSubLoc);
                    //Common.FillDropDownList(MasMgr.SelectEmpType(0,"Y"), ddlEmpType, false);
                    this.FillEmpList(radBtnListEmp.SelectedValue.ToString());
                    this.lblCommon.Text = "Assessment Year :";

                    break;
                }
            #endregion
            case "IR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0");
                    Common.FillDropDownList_All(objPayMgr.GetLocationData(), this.ddlLocation);
                    Common.FillMonthList_All(ddlMonthFrm);
                    this.AddAllinDDL(ddlSubLoc);
                    break;
                }
            case "CPIL":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "1", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "0");
                    Common.FillDropDownList_All(MasMgr.SelectLocation(0), ddlPostingDist);
                    break;
                }

            case "AVL": 
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0");
                   
                    break;
                }

            case "MBP":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    break;
                }
            case "NGOBSR":
                {
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "0", "1", "0", "1", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0");

                    DataTable dtSalSource = objPayMgr.SelectSalSource(0);
                    gvSalSource.DataSource = dtSalSource;
                    gvSalSource.DataBind();

                    break;
                }

        }
        this.chkSalarySource.Checked=false;
        this.chkSelectEmp.Checked=false;
        this.chkPostDistAll.Checked=false;
        this.chkSelectAll.Checked = false;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {        
        switch (tvReports.SelectedValue)
        {          
            case "ESPS":
                {
                    Session["SalDiv"] = ddlDivision.SelectedValue.ToString();
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
                    Session["DivisionId"] = ddlDivision.SelectedValue.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["SalType"] = rdbSalaryType.SelectedValue.Trim();
                    break;
                }
            case "SSS":
            case "SSSum":
                {
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();                    
                    Session["Type"] = ddlReportBy.SelectedValue.ToString();                                 
                    Session["SalDiv"] = ddlDivision.SelectedValue.ToString();
                    Session["EmpTypeId"] = ddlEmpType.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "ESI":          
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    Session["EmpStatus"] = strEmpType;
                    Session["DeptId"] = ddlDept.SelectedValue;
                    Session["ClinicId"] = ddlDivision.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    //Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
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
            case "SCH":     
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    Session["EmpStatus"] = strEmpType;
                    Session["DeptId"] = ddlDept.SelectedValue;
                    Session["ClinicId"] = ddlDivision.SelectedValue;
                    Session["GradeId"] = ddlGrade.SelectedValue.ToString();
                    //Session["PostingDivId"] = ddlDivision.SelectedValue;
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
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
            case "SEC":
                {
                    Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
                    Session["VYear"] = ddlYear.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }
            case "YPFC":
            case "YPFB":
            case "YPFLD":
            case "IPFC":
            case "AI":
            case "AITD":
            case "TDR":
                {
                    //this.DivEmpLoad();    
                    Session["SalDiv"] = ddlDivision.SelectedValue.ToString();  
                    Session["RptType"] = tvReports.SelectedValue.ToString();
                    Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
                    Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim() == "" ? "" : txtEmpCode.Text.Trim();
                    Session["EmpTypeId"] = "1";
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
                    Session["rbtTax"] = rbtTax.SelectedValue.ToString();
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
                    int month =Convert.ToInt32(ddlMonthFrm.SelectedValue.ToString());
                    string pmonth = GetPreviousMonth(month.ToString());
                    Session["FisYearP"] = fyer.ToString();
                    Session["VMonthP"] = pmonth;
                    this.DivEmpLoad();                            
                    Session["REPORTID"] = tvReports.SelectedNode.Value;

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
        sb.Append("window.open('PayrollReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",sb.ToString(), false);
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
