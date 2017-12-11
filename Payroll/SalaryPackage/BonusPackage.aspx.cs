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

public partial class Payroll_SalaryPackage_BonusPackage : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtBonus = new DataTable();
    DataTable dtSalaryHead = new DataTable();
    DataTable dtCurr = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtBonus.Rows.Clear();
            dtBonus.Dispose();

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
            ddlPercentSalHead.Enabled = false;
        }
    }
    private void OpenRecord()
    {
        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "N");

        dtBonus = objPayrollMgr.SelectBonusPackage(0);

        grBonus.DataSource = dtBonus;
        grBonus.DataBind();

        foreach (GridViewRow gRow in grBonus.Rows)
        {            
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 2));
            gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[6].Text)), 2));
        }

        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "N");
        this.Bind_DdlSalaryHead();

        dtCurr = objPayrollMgr.SelectCurrencyList(0);
        this.Bind_DdlCurrency();
    }

    private void Bind_DdlSalaryHead()
    {
        Common.FillDropDownList(dtSalaryHead, ddlPercentSalHead, true);
    }

    private void Bind_DdlCurrency()
    {
        Common.FillDropDownList(dtCurr, ddlCurrency, true);
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
            if (ddlCurrency.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select the currency.";
                ddlCurrency.Focus();
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
        string strSHEADID = "";
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("BonusPak", "BPId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            if (chkPercent.Checked == true)
                strSHEADID = ddlPercentSalHead.SelectedValue.ToString();
            else
                strSHEADID = null;

            Payroll_BonusPak objBonus = new Payroll_BonusPak(lngID.ToString(), txtHeadTitle.Text.Trim(),  txtDescription.Text.Trim(),
                txtBonusAmount.Text, (chkPercent.Checked == true ? "Y" : "N"), strSHEADID,txtNoOfPayment.Text,ddlCurrency.SelectedValue.ToString(),     
                (chkInActive.Checked == true ? "N" : "Y"), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            objPayrollMgr.InsertBonusPackage(objBonus, hfIsUpdate.Value, IsDelete);

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
    
    protected void grBonus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfID.Value = grBonus.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtHeadTitle.Text = grBonus.SelectedRow.Cells[1].Text;
                txtDescription.Text = grBonus.SelectedRow.Cells[2].Text;
                txtBonusAmount.Text = grBonus.SelectedRow.Cells[3].Text;
                chkPercent.Checked = (grBonus.SelectedRow.Cells[4].Text == "Y" ? true  : false );
                if (chkPercent.Checked == true)
                {
                    ddlPercentSalHead.SelectedValue = grBonus.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                    ddlPercentSalHead.Enabled = true;
                }
                else
                {
                    ddlPercentSalHead.SelectedIndex = 0;
                    ddlPercentSalHead.Enabled = false;
                }
                txtNoOfPayment.Text = grBonus.SelectedRow.Cells[6].Text;
                ddlCurrency.SelectedValue = grBonus.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                chkInActive.Checked = (grBonus.SelectedRow.Cells[8].Text == "Y" ? false : true);                
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
            lblMsg.Text = "Select a Salary Head first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void chkPercent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPercent.Checked == true)
            ddlPercentSalHead.Enabled = true;
        else
        {
            ddlPercentSalHead.SelectedIndex = 0;
            ddlPercentSalHead.Enabled = false;
        }
    }
}
