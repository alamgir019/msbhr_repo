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

public partial class Payroll_Payroll_BonusApprove : System.Web.UI.Page
{   
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    BonusAllowanceManager objBonMgr = new BonusAllowanceManager();   

    DataTable dtEmp = new DataTable();  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //hfIsUpdate.Value = "Y";
            // Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "T"), ddlFiscalYearTax, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_All(objMasMgr.SelectReligionList(0), ddlReligion);
            Common.FillDropDownList_Nil(objMasMgr.SelectFestivalList(0), ddlFestival);

            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
           
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        
    }

    protected void GenerateRecord()
    {
        dtEmp = objBonMgr.GetBonusAllowanceData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "14",
            ddlReligion.SelectedValue.Trim(), ddlFestival.SelectedValue.Trim());
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();

        //this.GetBonusData();
        int i = 1;
        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            i++;
        }
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count == 0)
        {
            lblMsg.Text = "No Record to Save";
            return;
        }

        objBonMgr.UpdateBonusStatus(grEmployee, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "A", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        
        lblMsg.Text = "Record Saved Successfully";
        grEmployee.DataSource = null;
        grEmployee.DataBind();
    }  

    private bool CheckField()
    {
        lblMsg.Text = "";      
        return true;
    }
   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        grEmployee.DataSource = null;
        grEmployee.DataBind();
    }
    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string religion = this.ddlReligion.SelectedValue.ToString();
        if (religion != "99999")
            Common.FillDropDownList(objMasMgr.SelectRelagionFestivalList(Convert.ToInt32(religion)), ddlFestival, true);
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.GenerateRecord(); 
    }
}
