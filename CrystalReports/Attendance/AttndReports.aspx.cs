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

public partial class CrystalReports_Attendance_AttndReports : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(objMasMgr.SelectDivision(0), ddlDivision);
            Common.FillDropDownList_All(objMasMgr.SelectClinic(), ddlSUB);
            Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlDept);
            Common.FillDropDownList_All(AttPMgr.GetData("0"), ddlShift);
            
            this.PanelVisibilityMst("0", "0", "0", "0", "0","0", "0", "0");                      
        }
    }
   
    protected void cbEnableEmp_CheckedChanged(object sender, EventArgs e)
    {
        
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
            if (tvReports.SelectedValue == "DA" || tvReports.SelectedValue == "AE" || tvReports.SelectedValue == "LR" ||
                tvReports.SelectedValue == "AR" || tvReports.SelectedValue == "IR" || tvReports.SelectedValue == "ED" ||
                tvReports.SelectedValue == "DailyA" || tvReports.SelectedValue == "MonthlyA" || tvReports.SelectedValue == "InvOT" ||
                tvReports.SelectedValue == "EWOS" || tvReports.SelectedValue == "EmpLVBalance" || tvReports.SelectedValue == "TSL")
            {
                PBranch.Visible = true;
                PDiv.Visible = true;
                PDept.Visible = true;
                PShift.Visible = true;
            }
        }
        else
        {
            PBranch.Visible = false;
            PDiv.Visible = false;
            PDept.Visible = false;
            PShift.Visible = false;           
        }
        txtEmpCode.Text = "";
        Session["EmpId"] = "";
    }

    private void PanelVisibilityMst(string sSearchBy, string sBranch, string sDiv,
        string sDept, string sDate, string sShow, string sShift, string sClosed)
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
        if (sShift == "1")
            PShift.Visible = true;
        else
            PShift.Visible = false;

        if (sDate == "1")
            pDate.Visible = true;
        else
        {
            pDate.Visible = false;
            txtFromDate.Text = Common.DisplayDate(DateTime.Now.ToString());
            txtToDate.Text = Common.DisplayDate(DateTime.Now.ToString());
        }
        if (sShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;
        if (sClosed == "1")
            PClosed.Visible = true;
        else
            PClosed.Visible = false;        
    }

    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)
    {
        PSearchBy.Enabled = true;

        this.PanelVisibilityMst("0", "0", "0", "0", "0", "0", "0", "0");
        //this.FillddlEmplStatus();
        switch(tvReports.SelectedValue) 
        {            
            case "DA":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "AE":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "SumAttnd":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "LR":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "AR":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "IR":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "ED":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "DailyA":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");                    
                    break;
                }
            case "MonthlyA":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    int TotalDay = 0;
                    txtFromDate.Text = "01/" + Common.DisplayMothYear(txtFromDate.Text);
                    TotalDay = GetMonthDay(Convert.ToDateTime(Common.ReturnDate(txtToDate.Text)));
                    txtToDate.Text = TotalDay + "/" + Common.DisplayMothYear(txtToDate.Text);
                    break;
                }
            case "InvOT":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
            case "EWOS":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }                      
            case "EmpLVBalance":
                {
                    PanelVisibilityMst("1", "1", "1", "1","1", "0", "1", "1" );
                    break;
                }
            case "TSL":
                {
                    PanelVisibilityMst("1", "1", "1", "1", "1", "1", "1", "1");
                    break;
                }
        }
        this.PanelVisibilityDet();
    }

    protected void hfId_ValueChanged(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //Go To Report Page With Session Variables ......
        switch (tvReports.SelectedValue)
        {
            case "DA":
            case "AE":
            case "SumAttnd":
            case "LR":
            case "AR":
            case "IR":
            case "ED":
            case "DailyA":
            case "MonthlyA":
            case "InvOT":
            case "EWOS":
            case "EmpLVBalance":
                {
                    Session["Flag"] = ddlReportBy.SelectedValue.ToString();
                    Session["FromDate"] = txtFromDate.Text;
                    Session["ToDate"] = txtToDate.Text;
                    Session["DivisionId"] = ddlDivision.SelectedValue.ToString();
                    Session["SbuId"] = ddlSUB.SelectedValue.ToString();
                    Session["DeptId"] = ddlDept.SelectedValue.ToString();
                    Session["EmpId"] = txtEmpCode.Text.Trim();
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["Division"] = ddlDivision.SelectedItem.ToString();
                    Session["SBU"] = ddlSUB.SelectedItem.ToString();
                    Session["Dep"] = ddlDept.SelectedItem.ToString();
                    Session["ShiftID"] = ddlShift.SelectedValue.ToString();
                    Session["ShiftName"] = ddlShift.SelectedItem.Text.ToString();
                    Session["IsClosed"] = ddlIsClosed.SelectedValue.ToString();
                    break;
                }
            case "TSL":
                {
                    Session["EmpId"] = "E000333";
                    Session["VMonth"] = "11";
                    Session["VYear"] = "2017";
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    break;
                }               

        }
        //Session["MONTH"] = ddlMonth.SelectedValue.ToString();    

        //Response.Redirect("../CrystalReports/frmReportViewer.aspx");
        //Open New Window
        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        sb.Append("window.open('frmAttndReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    
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
    //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //this.Bind_ddlSection(); 
    //    //this.Bind_ddlLocation(); 
    //    txtEmpCode.Enabled = true;
    //}
    //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}
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
}
