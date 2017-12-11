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
public partial class LeaveTypeSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    LeaveManager objLeaveMgr = new LeaveManager();
    static DataTable dtLeaveType = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtLeaveType.Rows.Clear();
            dtLeaveType.Dispose();
            grLeaveType.DataSource = null;
            grLeaveType.DataBind();
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
            hfIsUpadate.Value = "Y";
            TabContainer1.ActiveTabIndex = 0;
            lblMsg.Text = ""; 
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
            txtMaximumLeave.Text = "0";
            txtCancarryOver.Text = "0";
            txtCanCashable.Text = "0";
            txtMaxLimtCarry.Text = "0";
            txtCarryOverAndCashable.Text = "0";
            txtNextLevInt.Text = "0";
            txtNoOfTimes.Text = "0";
            txtEligibilityTime.Text = "0";
            txtCalcInterval.Text = "0";
            this.TabContainer1.ActiveTabIndex = 0;            
        }
    }

    private void OpenRecord()
    {
        dtLeaveType = objLeaveMgr.SelectLeaveType(0);
        grLeaveType.DataSource = dtLeaveType;
        grLeaveType.DataBind();
    }
    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("LeaveTypeList", "LTypeID");
            else
                lngID = Convert.ToInt32(hfID.Value);
            decimal LngCarryOverCash = 0;
            if (Convert.ToInt16(ddlLNature.SelectedValue) == 1)
                LngCarryOverCash = Convert.ToDecimal(txtCancarryOver.Text);
            else if (Convert.ToInt16(ddlLNature.SelectedValue) == 2)
                LngCarryOverCash = Convert.ToDecimal(txtCanCashable.Text);
            else if (Convert.ToInt16(ddlLNature.SelectedValue) == 3)
                LngCarryOverCash = Convert.ToDecimal(txtCarryOverAndCashable.Text);

            LeaveType objLeaveType = new LeaveType(lngID.ToString(),txtMaxLimtCarry.Text , txtLeaveType.Text.Trim(),txtAbbrName.Text.Trim(),   
            txtDescription.Text.ToString().Trim() , ddlLMUnit.SelectedValue, (chkCalculate1UnitPer.Checked == true ? "1" : "0"),
            (txtCalcInterval.Text==""?"0": txtCalcInterval.Text), ddlCalBase.SelectedValue, ddlLNature.SelectedValue, (txtMaximumLeave.Text==""?"0":txtMaximumLeave.Text),
            LngCarryOverCash.ToString(),( txtEligibilityTime.Text==""? "0":txtEligibilityTime.Text),  
            (txtNextLevInt.Text==""?"0":txtNextLevInt.Text),(txtNoOfTimes.Text==""?"0":txtNoOfTimes.Text), 
            Session["USERID"].ToString() , Common.SetDateTime(DateTime.Now.ToString()),
            (chkIsActive.Checked == true ? "N" : "Y"), IsDelete, "32",(chkIsOffdayCounted.Checked == true ? "Y" : "N"));

            objLeaveMgr.InsertLeaveType(objLeaveType, hfIsUpadate.Value, IsDelete, (chkIsActive.Checked == true ? "N" : "Y"));

            if (hfIsUpadate.Value == "N")
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

    protected string GetLNature(string strLNatureId)
    {
        string strText = "";
        strText = Common.FindInDdlTextData(ddlLNature, strLNatureId);
        return strText;
    }

    protected string DayHour(string strD)
    {
        if (strD == "D")
            return "Day";
        else
            return "Hour";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);        
        this.OpenRecord();
        lblMsg.Text = "";
    }
    protected void grLeaveType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grLeaveType.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }
    protected void grLeaveType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtLeaveType.Text = grLeaveType.SelectedRow.Cells[1].Text;
                hfID.Value = grLeaveType.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkIsActive.Checked = (grLeaveType.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false);
                txtDescription.Text =Common.CheckNullString(grLeaveType.SelectedRow.Cells[2].Text.Trim()) ;

                HiddenField hfLNature = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfLNature");
                ddlLNature.SelectedValue = hfLNature.Value;

                HiddenField hfLeaveTTL = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfLeaveTTL");
                txtMaximumLeave.Text = string.IsNullOrEmpty(hfLeaveTTL.Value.Trim()) == true ? "0" : hfLeaveTTL.Value.Trim();
                HiddenField hfEligibility = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfEligibility");
                txtEligibilityTime.Text = string.IsNullOrEmpty(hfEligibility.Value.Trim()) == true ? "0" : hfEligibility.Value.Trim();
                HiddenField hfNextLevInterval = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfNextLevInterval");
                txtNextLevInt.Text = string.IsNullOrEmpty(hfEligibility.Value.Trim()) == true ? "0" : hfEligibility.Value.Trim();
                HiddenField hfTotalMatLev = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfTotalMatLev");
                HiddenField hfMaxCarryCashDay = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfMaxCarryCashDay");
                HiddenField hfmaxCarryLimit = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfmaxCarryLimit");
                txtMaxLimtCarry.Text = string.IsNullOrEmpty(hfmaxCarryLimit.Value.Trim())==true ?"0": hfmaxCarryLimit.Value.Trim();
                if (hfLNature.Value == "1")
                    txtCancarryOver.Text = hfMaxCarryCashDay.Value;
                else if (hfLNature.Value == "2")
                    txtCanCashable.Text = hfMaxCarryCashDay.Value;
                else if (hfLNature.Value == "3")
                    txtCarryOverAndCashable.Text = hfMaxCarryCashDay.Value;
                else if ((hfLNature.Value == "4")|| (hfLNature.Value == "5"))
                {
                    txtNextLevInt.Text = hfNextLevInterval.Value;
                    txtNoOfTimes.Text = hfTotalMatLev.Value;
                }
                
                txtAbbrName.Text = Common.CheckNullString(grLeaveType.SelectedRow.Cells[5].Text.Trim());  
                HiddenField hfIsOffDC = (HiddenField)grLeaveType.SelectedRow.Cells[4].FindControl("hfIsOffdayCounted");
                chkIsOffdayCounted.Checked = (hfIsOffDC.Value == "Y" ? true : false);
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
            lblMsg.Text = "Select a LeaveType first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void ddlLNature_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void grLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
