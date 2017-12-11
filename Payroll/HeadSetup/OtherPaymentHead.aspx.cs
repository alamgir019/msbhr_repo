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

public partial class Payroll_HeadSetup_OtherPaymentHead : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtOtherHead = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtOtherHead.Rows.Clear();
            dtOtherHead.Dispose();

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
            txtDefAmt.Text = "0.00"; 
        }
    }

    private void OpenRecord()
    {
        dtOtherHead = objPayrollMgr.SelectSalaryHead(0, "Y");
        grOtherHead.DataSource = dtOtherHead;
        grOtherHead.DataBind();

        foreach (GridViewRow gRow in grOtherHead.Rows)
        {
            if (gRow.Cells[2].Text == "1")
                gRow.Cells[2].Text = "Additive";
            else
                gRow.Cells[2].Text = "Deductive";

            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 2));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        string strHeadNature = "";
        try
        {     
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SALARYHEAD", "SHEADID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            if (ddlHeadType.SelectedIndex == 0)
                strHeadNature = "1";
            else
                strHeadNature = "-1";

            Payroll_SalaryHead objSalHead = new Payroll_SalaryHead(lngID.ToString(), txtHeadTitle.Text.Trim(), strHeadNature, "",
                "Y", Common.ReturnZeroForNull(txtDefAmt.Text),   (chkInActive.Checked == true ? "N" : "Y"),
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N", "N", "N", "N", "N", "", "", "V");

            objPayrollMgr.InsertSalaryHead(objSalHead, hfIsUpdate.Value, IsDelete);

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

    protected void grOtherHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtHeadTitle.Text = grOtherHead.SelectedRow.Cells[1].Text.Trim();
                if (grOtherHead.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "1")
                    ddlHeadType.SelectedIndex = 0;
                else
                    ddlHeadType.SelectedIndex = 1;

                txtDefAmt.Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(grOtherHead.SelectedRow.Cells[3].Text)), 2));// grOtherHead.SelectedRow.Cells[3].Text;
                chkInActive.Checked = (grOtherHead.SelectedRow.Cells[4].Text == "Y" ? false : true);
                hfID.Value = grOtherHead.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
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
    protected void grOtherHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
