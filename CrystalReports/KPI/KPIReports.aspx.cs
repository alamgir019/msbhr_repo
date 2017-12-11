using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class CrystalReports_KPI_KPIReports : System.Web.UI.Page
{

    private Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    private KPIManager objKpi = new KPIManager();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objKpi.SelectQuarter(0), ddlQuarter, "QuarterName", "QuarterId", false);
            Common.FillDropDownList(objKpi.SelectGroup(0), ddlGroup, "GroupName", "GroupId", false);
            this.PanelVisibilityMst("0","0", "0", "0", "0");
        }
    }

    protected void tvReports_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.PanelVisibilityMst("0","0", "0", "0", "0");
        switch (tvReports.SelectedValue)
        {
            case "KPI":
                {
                    PanelVisibilityMst("1","1", "1", "1", "1");
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

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        switch (tvReports.SelectedValue)
        {
            case "KPI":
                {
                    Session["REPORTID"] = tvReports.SelectedNode.Value;
                    Session["EmpId"] = txtEmpID.Text.Trim();
                    Session["VYear"] = txtYear.Text.ToString();
                    Session["QuarterN"] = ddlQuarter.SelectedItem.ToString();
                    Session["Quarter"] = ddlQuarter.SelectedValue.ToString();
                    Session["Group"] = ddlGroup.SelectedValue.ToString();
                    break;
                }
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open('KPIReportsViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    private void PanelVisibilityMst(string sPShow, string sPEmp, string sPYear, string sPQuarter, string sPGroup)
    {

        if (sPShow == "1")
            PShow.Visible = true;
        else
            PShow.Visible = false;          
        if (sPEmp == "1")
            PEmp.Visible = true;
        else
            PEmp.Visible = false;

        if (sPYear == "1")
            PYear.Visible = true;
        else
            PYear.Visible = false;

        if (sPQuarter == "1")

            PQuarter.Visible = true;
        else
            PQuarter.Visible = false;

        if (sPGroup == "1")
            PGroup.Visible = true;
        else
            PGroup.Visible = false;
       

    }

    //protected void Bind_ddlFiscalYr()
    //{
    //    Common.FillDropDownList(objPayMgr.SelectFiscalYear(0), ddlFiscalYr, true);
    //}

    //protected void Bind_ddlProgDept()
    //{
    //    Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlProgDept);
    //}
    //protected void Bind_ddlLearningArea()
    //{
    //    Common.FillDropDownList_All(objMasMgr.SelectLearningArea(0), ddlLearningArea);
    //}

    //protected void Bind_ddlTrainingName()
    //{
    //    Common.FillDropDownList_All(objMasMgr.SelectTrainingName(0, "A"), ddlTrainingName);
    //}
}