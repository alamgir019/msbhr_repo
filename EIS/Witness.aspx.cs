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

public partial class EIS_Witness : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtWitness = new DataTable();

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
        dtWitness = objEmpMgr.SelectWitness(txtEmpID.Text.Trim());
        grWitness.DataSource = dtWitness;
        grWitness.DataBind();

        if (dtWitness.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grWitness.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
           }
        }
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
        grWitness.DataSource = null;
        grWitness.DataBind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.RefreshControl(); 
    }

    protected void ClearControl()
    {
        txtName.Text = "";        
        txtAddress.Text = "";
        txtSignatureDate.Text = "";        
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
            if (txtName.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter witness name.";
                txtName.Focus();
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
            string strSignDate = "";

            if (string.IsNullOrEmpty(txtSignatureDate.Text.Trim()) == false)
                strSignDate = Common.ReturnDate(txtSignatureDate.Text.Trim());

            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("WitnessList", "WitnessId");

            objEmpMgr.InsertWitness(hfId.Value.ToString(), txtEmpID.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim(), strSignDate,
                 Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(), strIsDelete);

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
    protected void grWitness_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtName.Text = Common.CheckNullString(grWitness.SelectedRow.Cells[1].Text.Trim());
                hfId.Value = grWitness.DataKeys[_gridView.SelectedIndex].Values[0].ToString();                
                txtAddress.Text = Common.CheckNullString(grWitness.SelectedRow.Cells[2].Text.Trim());
                txtSignatureDate.Text = Common.CheckNullString(grWitness.SelectedRow.Cells[3].Text.Trim());
                this.EntryMode(true);
                break;
        }
    }

    private bool GetTaskPermission()
    {
        string strEmpType = "";
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "310", "T102");
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
