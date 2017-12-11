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

public partial class Payroll_Payroll_PayrollReconcilDetailEntry : System.Web.UI.Page
{
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MovementStatement objPayMoveMgr = new Payroll_MovementStatement();
    DataTable dtEmpInfo = new DataTable();
    DataTable dtRecilation = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            lblMsg.Text = "";
        }
        else
        {
            this.ClearControl();            
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblSector.Text = dRow["SectorName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();      
                lblJoinDate.Text = Common.DisplayDate(dRow["JoiningDate"].ToString());                
                if (string.IsNullOrEmpty(dRow["JoiningDate"].ToString().Trim()) == false)
                    txtSperationDate.Text = Common.DisplayDate(dRow["JoiningDate"].ToString().Trim());
                lblBasicSalary.Text = dRow["BasicSalary"].ToString().Trim();
                lblBasicSalary.ToolTip = dRow["SalPakId"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtRecilation = objPayMoveMgr.SelectPayrollReconcilDetail(txtEmpID.Text.Trim());
        grList.DataSource = dtRecilation;
        grList.DataBind();

        if (dtRecilation.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grList.Rows)
            {
                gRow.Cells[0].Text = Common.ReturnFullMonthName(gRow.Cells[0].Text.Trim());
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[5].Text)) == false)
                    gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);                
            }
        }
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
           
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    
    protected void SaveData()
    {
        string strSeparationDate = "";

        if (string.IsNullOrEmpty(txtSperationDate.Text.Trim()) == false)
            strSeparationDate = Common.ReturnDate(txtSperationDate.Text.Trim());

        if (hfIsUpdate.Value == "Y")
            hfId.Value = hfId.Value;
        else
            hfId.Value = Common.getMaxId("PayrollReconcilDetailLog", "ReconcilId");

        objPayMoveMgr.InsertPayrollReconcilDetail(hfId.Value.ToString(), txtEmpID.Text.Trim(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
            txtBasicSal.Text, txtAllowance.Text, txtReason.Text.Trim(), strSeparationDate, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
        else
            lblMsg.Text = "Record Updated Successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grList.DataSource = null;
        grList.DataBind();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        txtEntryDate.Text = "";
        lblJoinDate.Text = "";
        lblBasicSalary.Text = "";
        txtEntryDate.Text = "";
        txtSperationDate.Text = "";          
    }   
}
