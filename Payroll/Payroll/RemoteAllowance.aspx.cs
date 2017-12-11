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

public partial class Payroll_Payroll_RemoteAllowance : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    Payroll_VariableAllowanceManager objVarMgr = new Payroll_VariableAllowanceManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtReAllow = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);            
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlPostingDiv);
            Common.FillDropDownList_Nil(objMasMgr.SelectSalaryLocation(0), ddlSalaryLoc);
            Common.FillDropDownList_Nil(objMasMgr.SelectLocation(0), ddlPlaceofPosting);
        }
    }   

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.ClearControls();
    }

    private void ClearControls()
    {
        txtEmpID.Text = ""; 
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        lblBasic.Text = "";

        ddlPostingDiv.SelectedIndex = -1;
        ddlSalaryLoc.SelectedIndex = -1;
        ddlPlaceofPosting.SelectedIndex = -1;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtPercentage.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = ""; 

        grList.DataSource = null;
        grList.DataBind();
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
        }
    }

    private void FillEmpInfo(string EmpId)
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
                lblSector.Text = dRow["SectorName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblBasic.Text = dRow["BasicSalary"].ToString().Trim();
            }            
        }
    }

    private void OpenRecord()
    {
        dtReAllow = objVarMgr.SelectRemoteAllowance("0", txtEmpID.Text.Trim());
        grList.DataSource = dtReAllow;
        grList.DataBind();
        foreach (GridViewRow gRow in grList.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[5].Text)) == false)
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[6].Text)) == false)
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
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
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";                      
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
            SaveData();
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

    private void SaveData()
    {
        string strAllowId = "";
        try
        {
            if (hfIsUpdate.Value == "N")
                strAllowId = Common.getMaxId("RemoteAllowanceAdd", "AllowanceId");
            else
                strAllowId = hfID.Value.ToString(); 

            string strDateFrom = "";
            string strDateTo = "";
            
            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()) == false)
                strDateFrom = Common.ReturnDate(txtStartDate.Text.Trim());

            if (string.IsNullOrEmpty(txtEndDate.Text.Trim()) == false)
                strDateTo = Common.ReturnDate(txtEndDate.Text.Trim());

            Payroll_RemoteAllowannce objclsReAllow = new Payroll_RemoteAllowannce();

            objclsReAllow.AllowanceID = strAllowId;
                objclsReAllow.EmpId =txtEmpID.Text.Trim();
                objclsReAllow.PostingDivID=ddlPostingDiv.SelectedValue.ToString();
                objclsReAllow.SalLocId = ddlSalaryLoc.SelectedValue.ToString();
                objclsReAllow.PostingPlaceId =ddlPlaceofPosting.SelectedValue.ToString();
                objclsReAllow.DateFrom = strDateFrom;
                objclsReAllow.DateTo = strDateTo;
                objclsReAllow.Basic=lblBasic.Text.Trim ();
                objclsReAllow.Percentage=txtPercentage.Text.Trim();
                objclsReAllow.Amount =txtAmount.Text.Trim();
                objclsReAllow.Remarks = txtRemarks.Text.Trim();          
                objclsReAllow.InsertedBy =  Session["USERID"].ToString();
                objclsReAllow.InsertedDate =Common.SetDateTime(DateTime.Now.ToString());
        
            objVarMgr.InsertRemoteAllowance(objclsReAllow, hfIsUpdate.Value,  "N");

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else if (hfIsUpdate.Value == "Y")
                lblMsg.Text = "Record Updated Successfully";
            else
                lblMsg.Text = "Record Deleted Successfully";

            this.EntryMode(false);
            this.OpenRecord();
            this.ClearControls();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfIsUpdate.Value = "N";
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                if (string.IsNullOrEmpty(grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString()) == false)
                    ddlPostingDiv.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                if (string.IsNullOrEmpty(grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString()) == false)
                    ddlSalaryLoc.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                if (string.IsNullOrEmpty(grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString()) == false)
                    ddlPlaceofPosting.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                txtStartDate.Text = Common.CheckNullString(grList.SelectedRow.Cells[5].Text);
                txtEndDate.Text = Common.CheckNullString(grList.SelectedRow.Cells[6].Text);
                //lblBasic.Text = Common.CheckNullString(grList.SelectedRow.Cells[7].Text);
                txtPercentage.Text = Common.CheckNullString(grList.SelectedRow.Cells[8].Text.Trim());
                txtAmount.Text = Common.CheckNullString(grList.SelectedRow.Cells[9].Text.Trim());

                this.EntryMode(true);
                break;
            case ("RowDeleting"):
                hfIsUpdate.Value = "N";
                SaveData();
                break;
        }
    }
}
