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

public partial class Payroll_HeadSetup_SalaryHeadSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtSalaryHead = new DataTable();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtSalaryHead.Rows.Clear();
            dtSalaryHead.Dispose();

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
        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0,"N");        
        grSalaryHead.DataSource = dtSalaryHead;
        grSalaryHead.DataBind();

        foreach (GridViewRow gRow in grSalaryHead.Rows)
        {
            if (gRow.Cells[3].Text == "1")
                gRow.Cells[3].Text = "Additive";
            else
                gRow.Cells[3].Text = "Deductive";

            if (Common.CheckNullString(gRow.Cells[6].Text) != "")
            {
                if (gRow.Cells[6].Text == "S")
                    gRow.Cells[6].Text = "Salary";
                else
                    gRow.Cells[6].Text = "Variable";
            }
            else
                gRow.Cells[6].Text = "";
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
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SALARYHEAD", "SHEADID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            if (ddlHeadType.SelectedIndex == 0)
                strHeadNature = "1";
            else
                strHeadNature = "-1";

            Payroll_SalaryHead objSalHead = new Payroll_SalaryHead(lngID.ToString(), txtHeadTitle.Text.Trim(), strHeadNature, txtDescription.Text.Trim(),
                "N", "0", (chkInActive.Checked == true ? "N" : "Y"), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N",
                (chkIsBasic.Checked == true ? "Y" : "N"), (chkPF.Checked == true ? "Y" : "N"), (chkIsHouseRent.Checked == true ? "Y" : "N"), (chkIsMedical.Checked == true ? "Y" : "N"),
                txtShortTitle.Text.Trim(), txtNaturalCode.Text.Trim(),
                ddlItemCategory.SelectedValue.Trim());

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
    protected void grSalaryHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;        
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtHeadTitle.Text = grSalaryHead.SelectedRow.Cells[1].Text;                
                if (grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "1")
                    ddlHeadType.SelectedIndex = 0;
                else
                    ddlHeadType.SelectedIndex = 1;

                txtDescription.Text = Common.CheckNullString(grSalaryHead.SelectedRow.Cells[4].Text.Trim() );
                chkInActive.Checked = (grSalaryHead.SelectedRow.Cells[5].Text == "Y" ? false : true);
                hfID.Value = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkIsBasic.Checked = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "Y" ? true : false;
                chkPF.Checked = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[3].ToString() == "Y" ? true : false;
                chkIsHouseRent.Checked = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[7].ToString() == "Y" ? true : false;
                chkIsMedical.Checked = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[8].ToString() == "Y" ? true : false;
                txtShortTitle.Text = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                txtNaturalCode.Text = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[5].ToString();
                if (Common.CheckNullString(grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[6].ToString()) != "")
                    ddlItemCategory.SelectedValue = grSalaryHead.DataKeys[_gridView.SelectedIndex].Values[6].ToString();
                else
                    ddlItemCategory.SelectedIndex = -1;
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
