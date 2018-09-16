using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CrystalReports_Leave_LeaveReports : System.Web.UI.Page


{
    //DBConnector objDC = new DBConnector();
    MasterTablesManager MasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();
   //  DataTable dtBranchWiseDiv = new DataTable(); 
    LeaveManager objLeaveMgr=new LeaveManager();
    private Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(MasMgr.SelectDivision(0), ddlPostDivision);
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);            
            Common.FillDropDownList_All(MasMgr.SelectLeaveType(0), ddlLeaveType);
            Common.FillDropDownList_All(MasMgr.SelectClinic("Y"), ddlSector);
            Common.FillDropDownList_All(MasMgr.SelectDepartment(0), ddlDepartment);
            Common.FillDropDownList_All(MasMgr.SelectEmpType(0,"Y"), ddlEmployeeType);

            ddlFiscalYr.SelectedIndex = 0;

            this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        }

    }
    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)
    {
        PSearchBy.Enabled = true;

        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1");
        //this.FillddlEmplStatus();
        switch (tvReports.SelectedValue)
        {
            case "ELBR":
                {
                    PanelVisibilityMst("1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "1");
                    break;

                  
                }
            case "EWLIR":
                {
                    PanelVisibilityMst("0", "1", "0", "1", "1", "1", "1", "1", "1", "1", "1", "0", "1");//RptBy,Div,Date/FiscalYr/Lvtype/fromTo/P_Emp/EmpId
                    radBtnListEmp.SelectedValue = "A";
                    string strEmpType = radBtnListEmp.SelectedValue.Trim().ToString();
                    DataTable dtLeaveType = MasMgr.SelectEmployee("", strEmpType);
                    gvEmp.DataSource = dtLeaveType;
                    gvEmp.DataBind(); 
                    break; 
                }

            case "EMWLR":
                {
                    PanelVisibilityMst("1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "1", "1");
                    break;
                    
                }
            case "EILR":
                {
                    PanelVisibilityMst("1", "1", "0", "1", "1", "0", "0", "0", "0", "0", "1", "1", "1");
                    break;

                }
        }

        this.PanelVisibilityDet();
    }


    protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.PanelVisibilityDet();

    }
    private void PanelVisibilityDet()
    {


        if (ddlReportBy.SelectedValue.ToString().Equals("E"))
        {
            PEmp.Visible = true;
        }
        else
       {
            PEmp.Visible = false;
        }
        if (ddlReportBy.SelectedValue.ToString().Equals("D"))
        {
            
                
                PDiv.Visible = true;
               
            
        }
        else
        {
          
            PDiv.Visible = false;
           
        }
        txtEmpCode.Text = "";
        Session["EmpId"] = "";
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //Go To Report Page With Session Variables ......
        switch (tvReports.SelectedValue)
        {
            case "ELBR":
           
                {


                    Session["Flag"] = ddlReportBy.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["PostingDivId"] = ddlPostDivision.SelectedValue.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["EmpTypeID"] = ddlEmployeeType.SelectedValue.ToString();
                    break;
                }


            case "EWLIR":
                {
                    //Session["Flag"] = ddlReportBy.SelectedValue.ToString();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    string strEmpId = "";
                    int i = 1;
                    foreach (GridViewRow gRow in gvEmp.Rows)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox = (CheckBox)gRow.Cells[0].FindControl("chkBoxEmp");
                        if (checkBox.Checked )
                        {
                            if (i == 1)
                                strEmpId = gRow.Cells[1].Text.Trim();
                            else
                                strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                            i++;
                        }
                    }
                   
                    Session["EmpId"] = strEmpId;//txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["PostingDivId"] = ddlPostDivision.SelectedValue.ToString();
                    Session["LeaveType"] = ddlLeaveType.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["SectorId"] = ddlSector.SelectedValue;
                    Session["DeptId"] = ddlDepartment.SelectedValue;
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["EmpTypeID"] = ddlEmployeeType.SelectedValue.ToString();
                    break;
                    
                }

            case "EMWLR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["PostingDivId"] = ddlPostDivision.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["LeaveType"] = ddlLeaveType.SelectedValue.ToString();
                    Session["LeaveTypeName"] = ddlLeaveType.SelectedItem.ToString();
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["EmpTypeID"] = ddlEmployeeType.SelectedValue.ToString();
                    break;

                }

            case "EILR":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["PostingDivId"] = ddlPostDivision.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["FiscalYrId"] = ddlFiscalYr.SelectedValue.ToString();
                    Session["FiscalYr"] = ddlFiscalYr.SelectedItem.ToString();
                    Session["ToDate"] = txtToDate.Text;
                    Session["EmpStatus"] = radBtnListEmp.SelectedValue;
                    Session["EmpTypeID"] = ddlEmployeeType.SelectedValue.ToString();
                    break;

                }
        }
          

       
        //Open New Window
        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        sb.Append("window.open('frmLeaveReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());    

    }


    private void PanelVisibilityMst(string sSearchBy, string sDiv,
       string sDate, string sShow, string sFiscalYr, string sLeaveType, string sPSector, string sPProgDept, string sP_Emp, string sPDateRange, string sPEmpStatus, string sPEmpId, string sPEmployeeType)
    {
        ddlReportBy.SelectedIndex = 0;

        if (sSearchBy == "1")
            PSearchBy.Visible = true;
        else
            PSearchBy.Visible = false;
        
        if (sDiv == "1")
            PDiv.Visible = true;
        else
            PDiv.Visible = false;

       

        if (sDate == "1")
            pDate.Visible = true;
        else
        {
            pDate.Visible = false;
            txtDate.Text = Common.DisplayDate(DateTime.Now.ToString());
           
        }
        if (sShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;
        if (sFiscalYr == "1")
            PFiscalYr.Visible = true;
        else
            PFiscalYr.Visible = false;

        if (sLeaveType == "1")
            PLeavetype.Visible = true;
        else
            PLeavetype.Visible = false;

        if (sPSector == "1")
            PSector.Visible = true;
        else
            PSector.Visible = false;


        if (sPProgDept == "1")
            PProgDept.Visible = true;
        else
            PProgDept.Visible = false;

        if (sP_Emp == "1")
            P_Emp.Visible = true;
        else
        
            P_Emp.Visible = false;

        

        if (sPDateRange=="1")
        
            PDateRange.Visible = true;

        
        else
        
            PDateRange.Visible = false;
        if (sPEmpStatus == "1")
            PEmpStatus.Visible = true;
        else
            PEmpStatus.Visible = false;

        if (sPEmpId == "1")

            PEmp.Visible = true;

        else

            PEmp.Visible = false;

        if (sPEmployeeType == "1")

            PEmployeeType.Visible = true;

        else
            PEmployeeType.Visible = false;
        
        }
   
    protected void ddlPostDivision_SelectedIndexChanged(object sender, EventArgs e)
    {

      //  Common.FillDropDownList_Nil(MasMgr.SelectDivision(0), ddlPostDivision);
    }
    //Start data binding    
    protected void Bind_ddlDivision()
    {
        //FillDropDownValue
      //  Common.FillDropDownList_Nil(MasMgr.SelectDivision(0), ddlPostDivision);
    }
    protected void btnLoadEmployee_Click(object sender, EventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        //if(chkSelectAll.Checked==true)
        //{
        //    foreach (GridViewRow row in gvEmp.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)row.FindControl("chkBoxEmp");
        //        checkBox.Checked = true;

        //    }
        //}
        //else
        //{
        //    foreach (GridViewRow row in gvEmp.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox) row.FindControl("chkBoxEmp");
        //        checkBox.Checked = false;
        //    }
        //}

    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Common.FillDropDownList_Nil(MasMgr.SelectLeaveType(0),ddlLeaveType);
    }
    protected void ddlFiscalYr_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}