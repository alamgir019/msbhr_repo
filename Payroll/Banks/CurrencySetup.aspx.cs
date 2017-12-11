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

public partial class Payroll_CurrencySetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();    
    DataTable dtCurr = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtCurr.Rows.Clear();
            dtCurr.Dispose();

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
            lblMsg.Text = ""; 
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtConvAmt.Text = "0.00";            
        }
    }
    private void OpenRecord()
    {
        dtCurr = objPayrollMgr.SelectCurrencyList(0);

        grCurrency.DataSource = dtCurr;
        grCurrency.DataBind();

        foreach (GridViewRow gRow in grCurrency.Rows)
        {
            gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 2));
        }
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
            long lngID = 0;        
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("CurrencyList", "CURNCID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            hfID.Value = lngID.ToString();

            if (chkDefault.Checked == true)
            {
                if (objPayrollMgr.IsDefaultExists(lngID.ToString()) == true)
                {
                    lblMsg.Text = "There is already a Operational Currency exist in the list. So please change Operational Currency.";
                    if (chkDefault.Enabled == true)
                        chkDefault.Focus();
                    return false;
                }
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
        
        try
        {
            Payroll_Currency objCurr = new Payroll_Currency(hfID.Value.ToString() , txtCurrName.Text.Trim(), txtCurrSymbol.Text.Trim(),
                txtSmallUnitName.Text, (chkDefault.Checked == true ? "Y" : "N"), txtConvAmt.Text,
                (chkInActive.Checked == true ? "N" : "Y"), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            objPayrollMgr.InsertCurrency(objCurr, hfIsUpdate.Value, IsDelete);

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
    protected void grCurrency_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfID.Value = grCurrency.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtCurrName.Text = Common.CheckNullString(  grCurrency.SelectedRow.Cells[1].Text);
                txtCurrSymbol.Text = Common.CheckNullString(grCurrency.SelectedRow.Cells[2].Text.Trim()) ;
                txtSmallUnitName.Text = Common.CheckNullString(grCurrency.SelectedRow.Cells[3].Text.Trim());
                chkDefault.Checked = (grCurrency.SelectedRow.Cells[4].Text == "Y" ? true : false);
                txtConvAmt.Text = grCurrency.SelectedRow.Cells[5].Text;                
                chkInActive.Checked = (grCurrency.SelectedRow.Cells[6].Text == "Y" ? false : true);
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
}
