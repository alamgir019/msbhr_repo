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

public partial class Payroll_SalaryHeadBlock : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDivision = new DataTable();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    Payroll_MasterMgr objSalaryHeadMgr2 = new Payroll_MasterMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objEmpInfoMgr.SelectSupervisor(), ddlSupervisor, "EMPNAME", "EMPID", true, "Select");
            Common.FillDropDownList(objSalaryHeadMgr2.SelectSalaryHeadCategoryWise("S"), ddlSalHead, true);
            hfIsUpadate.Value = "N";   
            this.EntryMode(false);
            this.OpenRecord();
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
        }
    }

    private void OpenRecord()
    {
        grSHeadBlock.DataSource = objMasMgr.GetEmpSalaryHeadBlockedData();
        grSHeadBlock.DataBind();

        foreach (GridViewRow gRow in grSHeadBlock.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text.Trim());
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
        }
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SalaryHeadBlock", "TransId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            MasMgr.InsertSalaryHeadBlock(lngID.ToString(), ddlSupervisor.SelectedValue.Trim(), ddlSalHead.SelectedValue.Trim(), txtFromDate.Text.Trim(),
                txtToDate.Text.Trim(), txtBlockAmt.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpadate.Value, IsDelete);

            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckFields())
        {
            this.SaveData("N");
        }
    }

    private bool CheckFields()
    {
        if (ddlSupervisor.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Employee.";
            return false;
        }
        if (ddlSalHead.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Salary Head.";
            return false;
        }
        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg.Text = "Please enter From Date.";
            return false;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg.Text = "Please enter To Date.";
            return false;
        }
        return true;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void grDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grSHeadBlock.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                hfID.Value = grSHeadBlock.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ddlSupervisor.SelectedValue = grSHeadBlock.SelectedRow.Cells[1].Text.Trim();
                ddlSalHead.SelectedValue = grSHeadBlock.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtFromDate.Text = grSHeadBlock.SelectedRow.Cells[4].Text.Trim();
                txtToDate.Text = grSHeadBlock.SelectedRow.Cells[5].Text.Trim();
                txtBlockAmt.Text = grSHeadBlock.SelectedRow.Cells[6].Text.Trim();
                this.EntryMode(true);
                break;
        }
    }

    protected void grDivision_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Division first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
