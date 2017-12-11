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

public partial class Payroll_LoanTypeSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtLoanType = new DataTable();
    DataTable dtSalHead = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtLoanType.Rows.Clear();
            dtLoanType.Dispose();

            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();

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
            txtMinServiceLife.Text = "0";
        }
    }

    private void OpenRecord()
    {
        dtLoanType = objPayrollMgr.SelectLoanType (0);
        grLoanType.DataSource = dtLoanType;
        grLoanType.DataBind();

        foreach (GridViewRow gRow in grLoanType.Rows)
        {        
            //gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 2));
        }
        dtSalHead = objPayrollMgr.SelectSalaryHead(0, "");
        this.Bind_DdlSalaryHead();
    }

    private void Bind_DdlSalaryHead()
    {
        Common.FillDropDownList(dtSalHead, ddlSalaryHead, true);
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
            if (ddlSalaryHead.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select salary head.";
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


    private void SaveData(string IsDelete)
    {
        long lngID = 0;        
        try
        {     
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("LOANTYPE", "LOANTYPEID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Payroll_LoanType objLoanType = new Payroll_LoanType(lngID.ToString(), txtName.Text.Trim(),txtDescription.Text.Trim(),   
                ddlSalaryHead.SelectedValue.ToString(),(chkInActive.Checked == true ? "N" : "Y"),(chkIsPFLoan.Checked == true ? "Y" : "N"),
                Common.ReturnZeroForNull(txtMinServiceLife.Text), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            objPayrollMgr.InsertLoanType(objLoanType, hfIsUpdate.Value, IsDelete);

            if (hfIsUpdate.Value == "N")
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }
    protected void grLoanType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtName.Text = grLoanType.SelectedRow.Cells[1].Text.Trim();
                txtDescription.Text = Common.CheckNullString(grLoanType.SelectedRow.Cells[2].Text.Trim());

                ddlSalaryHead.SelectedValue = grLoanType.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                chkInActive.Checked = (grLoanType.SelectedRow.Cells[4].Text == "Y" ? false : true);
                chkIsPFLoan.Checked = (grLoanType.SelectedRow.Cells[5].Text == "Y" ? true : false);
                txtMinServiceLife.Text = Convert.ToString(Convert.ToDouble(Common.ReturnZeroForNull(grLoanType.SelectedRow.Cells[6].Text)));// grOtherHead.SelectedRow.Cells[3].Text;

                hfID.Value = grLoanType.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.EntryMode(true);
                break;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Loan Type first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
