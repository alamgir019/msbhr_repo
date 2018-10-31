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

public partial class EIS_NomineeDeath : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtMNominee = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectRelationList(0), ddlRelation);
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
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                lblProject.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblSubDept.Text = dRow["SubDeptName"].ToString().Trim();
                lblSuncode.Text = dRow["ClinicName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtMNominee = objEmpMgr.SelectNominee( txtEmpID.Text.Trim(),"Y");
        grNominee.DataSource = dtMNominee;
        grNominee.DataBind();

        if (dtMNominee.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grNominee.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);

                if (Common.CheckNullString(gRow.Cells[5].Text) == "M")
                    gRow.Cells[5].Text = "Male";
                else if (Common.CheckNullString(gRow.Cells[5].Text) == "F")
                    gRow.Cells[5].Text = "Female";
                else
                    gRow.Cells[5].Text = "Nil";
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
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblCompany.Text = "";
        lblProject.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grNominee.DataSource = null;
        grNominee.DataBind();
    }

    protected void ClearControl()
    {
        txtNominee.Text = "";
        ddlRelation.SelectedIndex = -1; 
        txtDOB.Text = "";
        txtBenefit.Text = "";
        ddlGender.SelectedIndex = -1;
        txtBeneficiary.Text = "";
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
            if (txtNominee.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter nominee name.";
                txtNominee.Focus();
                return false;
            }
           
            if (ddlRelation.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select relation with nominee.";
                ddlRelation.Focus();
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
            string strDOB = "";
           
            if (string.IsNullOrEmpty(txtDOB.Text.Trim()) == false)
                strDOB = Common.ReturnDate(txtDOB.Text.Trim());

            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("Nominee", "NomineeId");

            objEmpMgr.InsertNominee(hfId.Value.ToString(), txtEmpID.Text.Trim(), txtNominee.Text.Trim(), ddlRelation.SelectedValue.ToString(), strDOB, txtBenefit.Text,
                ddlGender.SelectedValue.ToString(), txtBeneficiary.Text.Trim(), "D", Session["USERID"].ToString(),
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

    protected void grNominee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtNominee.Text = Common.CheckNullString(grNominee.SelectedRow.Cells[1].Text.Trim());
                hfId.Value = grNominee.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ddlRelation.SelectedValue = grNominee.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                txtDOB.Text = Common.CheckNullString(grNominee.SelectedRow.Cells[3].Text.Trim());
                txtBenefit.Text = Common.CheckNullString(grNominee.SelectedRow.Cells[4].Text.Trim());
                ddlGender.Text = grNominee.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();  
                txtBeneficiary.Text = Common.CheckNullString(grNominee.SelectedRow.Cells[6].Text.Trim());                
                this.EntryMode(true);
                break;
        }
    }

    private bool GetTaskPermission()
    {
        string strEmpType = "";
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "30802", "T102");
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
