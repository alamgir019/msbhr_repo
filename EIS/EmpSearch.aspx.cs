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

public partial class EIS_EmpSearch : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(objMasMgr.SelectEmpType(0,"Y"), ddlEmploymentType);
            Common.FillDropDownList_All(objMasMgr.SelectDepartment(0), ddlDept);
            Common.FillDropDownList_All(objMasMgr.SelectDesignation(0), ddlDesig);
            Common.FillDropDownList_All(objMasMgr.SelectGrade(0), ddlGrade);
            
            //Common.FillDropDownList_All(objMasMgr.SelectSector(0), ddlCompany);
            //Common.FillDropDownList_All(objMasMgr.SelectUnit(0), ddlProject);
            Common.FillDropDownList_All(objMasMgr.SelectDivision(0), ddlCompany);
            Common.FillDropDownList_All(objMasMgr.SelectProject(0), ddlProject);
        }

        string alertScript = "javascript: SearchByChanged();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", alertScript, true);
        btnShow.Attributes.Add("onClick", "SearchByChanged();");
        Page.RegisterStartupScript("myScript", "<script language=JavaScript>SearchByChanged();</script>");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        long sl = 0;
        DataTable dtEmp = objEmp.GetSearchEmployee(txtSearchValue.Text.Trim(), ddlProject.SelectedValue.ToString(), ddlCompany.SelectedValue.ToString(), ddlDept.SelectedValue.ToString(),
            ddlGrade.SelectedValue.ToString(), ddlDesig.SelectedValue.ToString(), txtSearchValue.Text.Trim(), txtSearchValue.Text.Trim(), txtSearchValue.Text.Trim(),
            ddlSearchBy.SelectedValue.ToString(), Session["USERID"].ToString(), ddlEmploymentType.SelectedValue.ToString(), ddlEmpStatus.SelectedValue.ToString().Trim());
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();
        dtEmp.Rows.Clear();
        dtEmp.Dispose();
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();

        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            sl = sl + 1;
            gRow.Cells[0].Text = sl.ToString();
            gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text);

            for (int i = 0; i < grEmployee.Columns.Count; i++)
            {
                if (Common.CheckNullString(gRow.Cells[i].Text) == "")
                {
                    gRow.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=EmpInfo.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        //sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grEmployee.RenderControl(htw);
        //sw = this.GetFooterText(sw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }
}
