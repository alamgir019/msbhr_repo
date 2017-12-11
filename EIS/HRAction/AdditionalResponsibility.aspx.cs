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

public partial class EIS_HRAction_AdditionalResponsibility : System.Web.UI.Page
{   
    EmpInfoManager objEmpMgr = new EmpInfoManager();    
    DataTable dtEmpInfo = new DataTable();
    DataTable dtAddResponse = new DataTable();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objEmpMgr.SelectNatureWiseAction("R"), ddlAction);     
            this.EntryMode(false);
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
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));   
        }
        ddlAction.SelectedIndex = 1; 
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No .";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblSector.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblBasicSalary.Text = dRow["GrossSalary"].ToString().Trim();    
            }
            this.OpenRecord();
        }
    }
    private void OpenRecord()
    {
        dtAddResponse = objEmpMgr.SelectAddResponsibility(0, txtEmpID.Text.Trim());
        grAddResponsibility.DataSource = dtAddResponse;
        grAddResponsibility.DataBind();

        if (dtAddResponse.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grAddResponsibility.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text)) == false)
                    gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
                 if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[2].Text)) == false)
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
                 if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
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
            if (ddlAction.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Action From The List.";
                ddlAction.Focus();
                return false;
            }

            if (txtStartDate.Text.Trim()   == "")
            {
                lblMsg.Text = "Please enter start date.";
                txtStartDate.Focus();  
                return false;
            }

            if (txtEndDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter end date.";
                txtEndDate.Focus();
                return false;
            }
           
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
        string strEntryDate = "";
        string strStartDate = "";
        string strEndDate = "";

        if (string.IsNullOrEmpty(txtEntryDate.Text.Trim()) == false)
            strEntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());

        if (string.IsNullOrEmpty(txtStartDate.Text.Trim()) == false)
            strStartDate = Common.ReturnDate(txtStartDate.Text.Trim());

        if (string.IsNullOrEmpty(txtEndDate.Text.Trim()) == false)
            strEndDate = Common.ReturnDate(txtEndDate.Text.Trim());

        if (hfIsUpdate.Value == "Y") 
            hfId.Value = hfId.Value;
        else
            hfId.Value = Common.getMaxId("EmpAddResponsibilityLog", "AddResponseId");

        objEmpMgr.InsertAddResponsibility(hfId.Value.ToString(), txtEmpID.Text.Trim(), ddlAction.SelectedValue.ToString(), strEntryDate, strStartDate, strEndDate, txtAmount.Text.Trim(),
            txtPercent.Text.Trim(), txtResponsibility.Text.Trim(), (chkIsResponseAllowance.Checked == true ? "Y" : "N"), (chkIsRepeat.Checked == true ? "Y" : "N"),
            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString());

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
        else
            lblMsg.Text = "Record Updated Successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }

    protected void ClearControl()
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        lblBasicSalary.Text = ""; 
        Common.EmptyTextBoxValues(this);

        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtAmount.Text = "";
        txtPercent.Text = "";
        txtResponsibility.Text = "";
        chkIsResponseAllowance.Checked = false;

        grAddResponsibility.DataSource = null;
        grAddResponsibility.DataBind();  
    }
    
    protected void grAddResponsibility_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfId.Value = grAddResponsibility.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                txtEntryDate.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[1].Text.Trim());
                txtStartDate.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[2].Text.Trim());
                txtEndDate.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[3].Text.Trim());               
                txtPercent.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[4].Text.Trim());
                txtAmount.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[5].Text.Trim());
                txtResponsibility.Text = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[6].Text.Trim());
                chkIsResponseAllowance.Checked = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[7].Text.Trim()) == "Y" ? true : false;
                chkIsRepeat.Checked = Common.CheckNullString(grAddResponsibility.SelectedRow.Cells[8].Text.Trim()) == "Y" ? true : false;
                this.EntryMode(true);
                lblMsg.Text = "";
                break;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.OpenRecord();
        grAddResponsibility.DataSource = null;
        grAddResponsibility.DataBind();
        lblMsg.Text = "";
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStartDate.Text) == true)
        {
            lblMsg.Text = "Please enter start date";
            txtStartDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPercent.Text) == true)
        {
            lblMsg.Text = "Please enter percent value";
            txtPercent.Focus();
            return;
        }
        DateTime dtStartManth = Convert.ToDateTime(Common.ReturnDate(txtStartDate.Text.Trim()));
        DataTable dtBenefits = objPreMgr.SelectVaribaleAllowanceDataEmpWise(dtStartManth.Month.ToString(), dtStartManth.Year.ToString(),txtEmpID.Text.Trim(),"5");
        if ((string.IsNullOrEmpty(txtPercent.Text) == false) && string.IsNullOrEmpty(lblBasicSalary.Text) == false)
        {
            txtAmount.Text = Math.Round(Convert.ToDecimal(lblBasicSalary.Text) * Convert.ToDecimal(txtPercent.Text) / 100).ToString();
            if (dtBenefits.Rows.Count > 0)
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) + Convert.ToDecimal(dtBenefits.Rows[0]["PayAmt"].ToString()));  
        }
    }
}
