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

public partial class Payroll_Payroll_ChildEducationAllowance : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    Payroll_VariableAllowanceManager objVarMgr = new Payroll_VariableAllowanceManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtEduAllow = new DataTable();

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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.ClearControls();
        lblMsg.Text = "";
        ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
        ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";

        txtChildName.Text = "";
        txtDOB.Text = "";
        ddlGender.SelectedIndex = -1;
        txtAge.Text = "";
        txtAmount.Text = "";
        

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
            }
        }
    }

    private void OpenRecord()
    {
        dtEduAllow = objVarMgr.SelectChildEduAllowance("0", txtEmpID.Text.Trim());
        grList.DataSource = dtEduAllow;
        grList.DataBind();
        foreach (GridViewRow gRow in grList.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[2].Text)) == false)
                gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            if ((string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false) && (Common.CheckNullString(gRow.Cells[3].Text) != "0"))
                gRow.Cells[3].Text = ((gRow.Cells[3].Text) == "M" ? "Male" : "Female");

             if ((string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[6].Text)) == false) && (Common.CheckNullString(gRow.Cells[6].Text) != "0"))
                
                  if ((gRow.Cells[6].Text) == "1")
                 gRow.Cells[6].Text = "January";
             else if ((gRow.Cells[6].Text) == "2")
                 gRow.Cells[6].Text = "February";
             else if ((gRow.Cells[6].Text) == "3")
                 gRow.Cells[6].Text = "March";
             else if ((gRow.Cells[6].Text) == "4")
                 gRow.Cells[6].Text = "April";
             else if ((gRow.Cells[6].Text) == "5")
                 gRow.Cells[6].Text = "May" ;
             else if((gRow.Cells[6].Text) == "6")
                 gRow.Cells[6].Text = "June";
             else if ((gRow.Cells[6].Text) == "7")
                 gRow.Cells[6].Text = "July";
             else if ((gRow.Cells[6].Text) == "8")
                 gRow.Cells[6].Text = "August";
             else if ((gRow.Cells[6].Text) == "9")
                 gRow.Cells[6].Text = "September";
             else if ((gRow.Cells[6].Text) == "10")
                 gRow.Cells[6].Text = "October";
             else if ((gRow.Cells[6].Text) == "11")
                 gRow.Cells[6].Text = "November";
             else if ((gRow.Cells[6].Text) == "12")
                 gRow.Cells[6].Text = "December";



                 
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

    private void SaveData()
    {
        string strAllowId = "";
        try
        {
            if (hfIsUpdate.Value == "N")
                strAllowId = Common.getMaxId("ChildEduAllowance", "AllowanceId");
            else
                strAllowId = hfID.Value.ToString();

            string strDOB = "";

            if (string.IsNullOrEmpty(txtDOB.Text.Trim()) == false)
                strDOB = Common.ReturnDate(txtDOB.Text.Trim());

            Payroll_ChildEduAllowannce objclsEduAllow = new Payroll_ChildEduAllowannce();

            objclsEduAllow.AllowanceID = strAllowId;
            objclsEduAllow.EmpId = txtEmpID.Text.Trim();
            objclsEduAllow.ChildName = txtChildName.Text.Trim();
            objclsEduAllow.ChildDOB = strDOB;
            objclsEduAllow.Gender = ddlGender.SelectedValue.ToString();
            objclsEduAllow.Age = txtAge.Text;
            objclsEduAllow.Amount = txtAmount.Text;
            objclsEduAllow.VMONTH = ddlMonth.SelectedValue.Trim();
            objclsEduAllow.VYEAR = ddlYear.SelectedValue.Trim();
            objclsEduAllow.InsertedBy = Session["USERID"].ToString();
            objclsEduAllow.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());

            objVarMgr.InsertChildEduAllowance(objclsEduAllow, hfIsUpdate.Value, "N");

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



    protected void grList_SelectedIndexChanged(object sender, EventArgs e)
    {

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
                txtChildName.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text);
                txtDOB.Text = Common.CheckNullString(grList.SelectedRow.Cells[2].Text);
                ddlGender.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtAge.Text = Common.CheckNullString(grList.SelectedRow.Cells[4].Text);
                txtAmount.Text = Common.CheckNullString(grList.SelectedRow.Cells[5].Text);
                ddlMonth.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                ddlYear.SelectedItem.Text =Common.CheckNullString(grList.SelectedRow.Cells[7].Text);

                this.EntryMode(true);
                break;
            case ("RowDeleting"):
                hfIsUpdate.Value = "N";
                SaveData();
                break;
        }
    }
}
