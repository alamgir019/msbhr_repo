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

public partial class EIS_Experience : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtExper = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
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
            if (GetTaskPermission() == false)
            {
                this.RefreshControl();
                lblMsg.Text = "Please mention contractual & intern staff's id.";
                btnSave.Enabled = false;
                return;
            }
            else
            {
                lblMsg.Text = "";
                btnSave.Enabled = true;
            }
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblJobTitle.Text = dRow["DesigName"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                lblProject.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblSubDept.Text = dRow["SubDeptName"].ToString().Trim();
                lblSector.Text = dRow["ClinicName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }
    
    private void OpenRecord()
    {
        dtExper = objEmpMgr.SelectEmpExperience(txtEmpID.Text.Trim());
        grExperience.DataSource = dtExper;
        grExperience.DataBind();

        if (dtExper.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grExperience.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[4].Text)) == false)
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            }
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.RefreshControl();
    }

    protected void RefreshControl()
    {
        lblName.Text = "";
        lblCompany.Text = "";
        lblJobTitle.Text = "";
        lblDept.Text = "";
        lblSubDept.Text = "";
        lblSector.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grExperience.DataSource = null;
        grExperience.DataBind();
    }
    protected void ClearControl()
    {
        txtJobTitle.Text = "";
        txtCompany.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtDuration.Text = "";
        txtResponse.Text = ""; 
        chkIsSC.Checked = false;
        ChkIsEmergency.Checked = false;         
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (txtStartDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter Strat date.";
                txtStartDate.Focus();
                return false;
            }

            if (txtEndDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter End date.";
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
    private void SaveData(string strIsDelete)
    {
        try
        {
            string strStartDate = "";
            string strEndDate = "";

            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()) == false)
                strStartDate = Common.ReturnDate(txtStartDate.Text.Trim());

            if (string.IsNullOrEmpty(txtEndDate.Text.Trim()) == false)
                strEndDate = Common.ReturnDate(txtEndDate.Text.Trim());

            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("EmpDisciplinaryLog", "DisciplinaryId");

            objEmpMgr.InsertExperience(hfId.Value.ToString(), txtEmpID.Text.Trim(), txtJobTitle.Text.Trim(), txtCompany.Text.Trim(), strStartDate, strEndDate, txtDuration.Text.Trim(),
                txtResponse.Text.Trim(), chkIsSC.Checked == true ? "Y" : "N", ChkIsEmergency.Checked == true ? "Y" : "N", Session["USERID"].ToString(),
                Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(), strIsDelete);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            this.OpenRecord();
            this.EntryMode(false);
            this.ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void grExperience_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtJobTitle.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[1].Text.Trim());  
                hfId.Value = grExperience.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtCompany.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[2].Text.Trim());  
                txtStartDate.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[3].Text.Trim());
                txtEndDate.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[4].Text.Trim());
                txtDuration.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[5].Text.Trim());
                txtResponse.Text = Common.CheckNullString(grExperience.SelectedRow.Cells[6].Text.Trim());
                chkIsSC.Checked = Common.CheckNullString(grExperience.SelectedRow.Cells[7].Text.Trim()) == "Y" ? true : false;
                ChkIsEmergency.Checked = Common.CheckNullString(grExperience.SelectedRow.Cells[8].Text.Trim()) == "Y" ? true : false;
                this.EntryMode(true);
                break;

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
            lblMsg.Text = "Record Deleted Successfully";
        }
        else
        {
            lblMsg.Text = "Select a record first then try to delete.";
        }
        this.OpenRecord(); 
        this.EntryMode(false);
    }

    private bool GetTaskPermission()
    {
        string strEmpType = "";
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "306", "T102");
        if (dtConsTaskPermission.Rows.Count > 0)
        {
            strEmpType = objEmpMgr.SelectEmpWiseContractType(txtEmpID.Text.Trim());
            if (strEmpType != "")
                return true;
            else
                return false;
        }
        return true;
    }
}
