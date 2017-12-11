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

public partial class Payroll_USDRateSetup : System.Web.UI.Page
{
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    DataTable dtUSD =new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                hfIsUpdate.Value = "N";
                dtUSD.Rows.Clear();
                dtUSD.Dispose();
                grList.DataSource = null;
                grList.DataBind();
                Common.EmptyTextBoxValues(this);
                lblMsg.Text = "";
                this.EntryMode(false);
                this.OpenRecord();
            }
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
        dtUSD = objPayMgr.SelectUSDRate("");
        grList.DataSource = dtUSD;
        grList.DataBind();

        foreach (GridViewRow gRow in grList.Rows)
        {
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strId = "";

        try
        {
            strId = Common.getMaxId("USDRate", "USDId");
            string strUSDDate = "";

            if (string.IsNullOrEmpty(txtRateDate.Text) == false)
                strUSDDate = Common.ReturnDate(txtRateDate.Text);

            objPayMgr.InsertUDSRate(strId, txtUsdRate.Text, strUSDDate, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");
            this.OpenRecord();
            lblMsg.Text = "Record Saved Successfully";
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = "";
    }

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtUsdRate.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text);
                txtRateDate.Text = Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim());                
                this.EntryMode(true);
                break;
        }
    }
}
