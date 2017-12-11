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

public partial class Payroll_Banks_BankSetup : System.Web.UI.Page
{
    PlanAccLineManager objAccMgr = new PlanAccLineManager();
    BankManager objBankMgr = new BankManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.FillBankDropDownList();
            this.EntryMode(false);
            btnDelete.Visible = false;
        }
    }

    protected void FillBankDropDownList()
    {
        DataTable dtBank = objBankMgr.GetDistinctBank();
        Common.FillDropDownList(dtBank, ddlBank, "BANKNAME", "BANKCODE", true, "N/A");
        Common.FillDropDownList(dtBank, ddlBankSearh, "BANKNAME", "BANKCODE", true, "All");
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
            txtBankName.Text = "";
            txtBankCode.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtDistrict.Text = "";
        }
    }

    private void OpenRecord(string strBankCode)
    {
        grBank.DataSource = objBankMgr.GetBankData(strBankCode);
        grBank.DataBind();
    }

    protected void grBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                ddlBank.SelectedValue = grBank.SelectedRow.Cells[1].Text.Trim();
                txtBranchCode.Text = Common.CheckNullString(grBank.SelectedRow.Cells[4].Text.Trim());
                txtBranchName.Text = Common.CheckNullString(grBank.SelectedRow.Cells[3].Text.Trim());
                txtDistrict.Text = Common.CheckNullString(grBank.SelectedRow.Cells[5].Text.Trim());
                if (Common.CheckNullString(grBank.SelectedRow.Cells[6].Text.Trim()) != "")
                    ddlDOS.SelectedValue = grBank.SelectedRow.Cells[6].Text.Trim();
                else
                    ddlDOS.SelectedValue = "-1";
                    
                hfID.Value = grBank.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                hfIsUpdate.Value = "Y";
                this.EntryMode(true);
                lblMsg.Text = "";
                break;
        }
    }

    private void SaveData(string IsDelete)
    {
        string  strID = "";
        string strBankCode="";
        string strBankName="";

        try
        {
            if (hfIsUpdate.Value == "N")
            {
                if (objBankMgr.IsDataExist(txtBranchCode.Text.Trim(), "") == true)
                {
                    lblMsg.Text = "Routing No Already Exist. Cannot Insert Duplicate Routing No";
                    return;
                }
                strID = Common.getMaxId("BANKLIST", "SLID");
            }
            else
            {
                if (objBankMgr.IsDataExist(txtBranchCode.Text.Trim(), hfID.Value.ToString()  ) == true)
                {
                    lblMsg.Text = "Routing No Already Exist. Cannot Insert Duplicate Routing No";
                    return;
                }
                strID = hfID.Value;
            }

            if(ddlBank.SelectedValue.Trim()=="-1")
            {
                strBankCode=txtBankCode.Text.Trim();
                strBankName=txtBankName.Text.Trim();
            }
            else
            {
                strBankCode = ddlBank.SelectedValue.Trim();
                strBankName=ddlBank.SelectedItem.Text.Trim();
            }

            if (IsDelete == "N")
            {
                objBankMgr.InsertBankData(strID, strBankCode, strBankName, txtBranchName.Text.Trim(),
                    txtBranchCode.Text.Trim(), txtDistrict.Text.Trim(), hfIsUpdate.Value.ToString(),ddlDOS.SelectedValue.Trim());

                if (hfIsUpdate.Value == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";
            }
            this.EntryMode(false);
            this.FillBankDropDownList();
            this.OpenRecord(strBankCode);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.SaveData("Y");
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
         this.OpenRecord(ddlBankSearh.SelectedValue.Trim());
    }
}
