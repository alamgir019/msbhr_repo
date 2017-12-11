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

public partial class Payroll_SalaryPackage_BonusPolicyPackage : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, "TYPENAME", "EMPTYPEID", true, "Select");
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
        grBonus.DataSource = objPayrollMgr.SelectBonusPolicyData();
        grBonus.DataBind();
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData();
    }

     
    private void SaveData()
    {
        string strBPID = "";
        try
        {
            if (hfIsUpdate.Value == "N")
                strBPID = Common.getMaxId("BonusPolicy", "BPID");
            else
                strBPID = hfID.Value;

            if (chkProrata.Checked == true)
                txtPercent.Text = "0";

            objPayrollMgr.InsertBonusPolicyData(strBPID, ddlEmpType.SelectedValue.Trim(), txtPercent.Text.Trim(), hfIsUpdate.Value, 
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()),chkProrata.Checked==true?"Y":"N");

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
                ddlEmpType.SelectedValue = grBonus.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtPercent.Text = grBonus.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                chkProrata.Checked = grBonus.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim()=="Y"?true:false;
                this.EntryMode(true);
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            objPayrollMgr.DeleteBonusPolicyData(hfID.Value.Trim());
            Common.EmptyTextBoxValues(this);
            this.OpenRecord();
            lblMsg.Text = "Record Deleted Successfully";
        }
        else
        {
            lblMsg.Text = "Select a item from the list then try to delete.";
        }

        this.EntryMode(false);
    }
   
}
