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

public partial class Payroll_FiscalYearSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtFiscalYr = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtFiscalYr.Rows.Clear();
            dtFiscalYr.Dispose();

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
        }
    }

    private void OpenRecord()
    {
        dtFiscalYr = objPayrollMgr.SelectFiscalYear(0);
        grFiscalYear.DataSource = dtFiscalYr;
        grFiscalYear.DataBind();

        foreach (GridViewRow gRow in grFiscalYear.Rows)
        {
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
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
            double TotDay = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            if (string.IsNullOrEmpty(txtEndDate.Text) == false && string.IsNullOrEmpty(txtStartDate.Text) == false)
            {
                char[] splitter ={ '/' };
                string[] arinfo = Common.str_split(txtStartDate.Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                }
                arinfo = Common.str_split(txtEndDate.Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                }

                TimeSpan Dur = dtTo.Subtract(dtFrom);

                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
                if (TotDay < 0)
                {
                    lblMsg.Text = "Start Date can not be greater than end date.";
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
        long lngID = 0;
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("FiscalYearList", "FiscalYrId");
            else
                lngID = Convert.ToInt32(hfID.Value);


            Payroll_FiscalYear objFiscalYr = new Payroll_FiscalYear(lngID.ToString(), "", txtTitle.Text.Trim(),   txtDescription.Text.Trim(),
                 txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), (chkIsClosed.Checked == true ? "Y" : "N"), (chkCurrFiscalYr.Checked == true ? "Y" : "N"),
                 "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            objPayrollMgr.InsertFiscalYear(objFiscalYr, hfIsUpdate.Value, IsDelete, (chkIsTax.Checked == true ? "Y" : "N"),
                (chkIsPF.Checked == true ? "Y" : "N"), (chkIsMed.Checked == true ? "Y" : "N"));

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

    protected void grFiscalYear_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtTitle.Text = Common.CheckNullString(grFiscalYear.SelectedRow.Cells[1].Text);
                chkIsTax.Checked = (grFiscalYear.SelectedRow.Cells[2].Text == "Y" ? true : false);
                chkIsPF.Checked = (grFiscalYear.SelectedRow.Cells[3].Text == "Y" ? true : false);
                chkIsMed.Checked = (grFiscalYear.SelectedRow.Cells[4].Text == "Y" ? true : false);
                txtStartDate.Text =  Common.CheckNullString(grFiscalYear.SelectedRow.Cells[5].Text.Trim());
                txtEndDate.Text = Common.CheckNullString(grFiscalYear.SelectedRow.Cells[6].Text.Trim()); 
                chkIsClosed.Checked = (grFiscalYear.SelectedRow.Cells[7].Text == "Y" ? true  : false );
                chkCurrFiscalYr.Checked = (grFiscalYear.SelectedRow.Cells[8].Text == "Y" ? true  : false );
                hfID.Value = grFiscalYear.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtDescription.Text = grFiscalYear.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
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
            lblMsg.Text = "Select a Project Title first from the list then try to delete.";
        }
        this.EntryMode(false);
    }
}
