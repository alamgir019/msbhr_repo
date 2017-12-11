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

public partial class EIS_HRAction_ContractExtension : System.Web.UI.Page
{
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtContractEx = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objEmpInfoMgr.SelectNatureWiseAction("E"), ddlAction);
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
            //this.ClearControl();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));   
        }
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                lblMsg.Text = "";
                foreach (DataRow dRow in dtEmpInfo.Rows)
                {
                    lblName.Text = dRow["FullName"].ToString();
                    lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                    lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                    lblDept.Text = dRow["DeptName"].ToString().Trim();
                    lblSubDept.Text = dRow["SubDeptName"].ToString().Trim();
                    lblSuncode.Text = dRow["ClinicName"].ToString().Trim();
                }
                this.OpenRecord();
            }
        }
    }

    private void OpenRecord()
    {
        grContExt.Dispose();
        dtContractEx = objEmpInfoMgr.SelectEmpContractExtLog(0, txtEmpID.Text.Trim());
        grContExt.DataSource = dtContractEx;
        grContExt.DataBind();
        if (grContExt.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grContExt.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[2].Text)) == false)
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        this.ClearControl();
        grContExt.DataSource = null;
        grContExt.DataBind();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = ""; 
        lblCompany.Text = "";
        txtEntryDate.Text = "";
        ddlAction.SelectedIndex = -1;
        txtEffDate.Text = "";
        txtContExpDate.Text = "";
    }
   
    protected void grContExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfContExtId.Value = grContExt.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                ddlAction.SelectedValue = grContExt.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtEffDate.Text = Common.CheckNullString(grContExt.SelectedRow.Cells[2].Text.Trim());
                txtContExpDate.Text = Common.CheckNullString(grContExt.SelectedRow.Cells[3].Text.Trim());
                this.EntryMode(true);
                break;
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
        if (ddlAction.SelectedIndex == 0)
        {
            lblMsg.Text = "Please Select an Action.";
            ddlAction.Focus();
            return false;
        }

        if (txtEffDate.Text == "")
        {
            lblMsg.Text = "Please input a valid date.";
            txtEffDate.Focus();
            return false;
        }
        else
            lblMsg.Text = "";

        return true;
    }

    private void SaveData()
    {
        if (hfIsUpdate.Value == "Y")
            hfContExtId.Value = hfContExtId.Value;
        else
            hfContExtId.Value = Common.getMaxId("EmpContractExtLog", "ContractExtId");

        string strEffDate = "";
        string strContExtDate = "";

        if (string.IsNullOrEmpty(txtEffDate.Text.Trim()) == false)
            strEffDate = Common.ReturnDate(txtEffDate.Text.Trim());

        if (string.IsNullOrEmpty(txtContExpDate.Text.Trim()) == false)
            strContExtDate = Common.ReturnDate(txtContExpDate.Text.Trim());

        clsContractExt objContExt = new clsContractExt(txtEmpID.Text.Trim(), hfContExtId.Value.ToString(), ddlAction.SelectedValue.ToString(), strEffDate, strContExtDate,
            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        EmpInfoManager objEmp = new EmpInfoManager();
        objEmp.InsertContractExtension(objContExt, ddlAction.SelectedItem.ToString(), hfIsUpdate.Value);

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";

        else if (hfIsUpdate.Value == "Y")
            lblMsg.Text = "Record Updated Successfully";

        this.OpenRecord();        
        this.EntryMode(false);
        this.ClearControl();
    }
}