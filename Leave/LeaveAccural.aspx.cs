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

public partial class Leave_LeaveAccural : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    LeaveManager objLeaveMgr = new LeaveManager();

    DataTable dtELLeave=new DataTable ();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, "TypeName", "EmpTypeId", false);
            Common.FillDropDownList(objLeaveMgr.SelectLeaveType(0), ddlLeaveType, "LTypeTitle", "LTypeID", false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ClearFields();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        this.FillEmpLeaveProfile();
    }

    private void FillEmpLeaveProfile()
    {
        dtELLeave = objLeaveMgr.SelectLeaveType(1);
        grEmpList.DataSource = objLeaveMgr.SelectEmpTypeWSLeaveProfile(ddlEmpType.SelectedValue.ToString(), ddlLeaveType.SelectedValue.ToString());
        grEmpList.DataBind();

        dtELLeave.Rows.Clear();
        dtELLeave.Dispose();
        this.FormatLeaveStatusGridNumber(ddlEmpType.SelectedValue.ToString(), ddlLeaveType.SelectedValue.ToString());
    }

    protected void FormatLeaveStatusGridNumber(string strEmpTypeId, string strLeaveTypeId)
    {
        int i = 1;
        DateTime dtJoinDate;
        DateTime dtCurrDate = DateTime.Now.Date;

        foreach (GridViewRow gRow in grEmpList.Rows)
        {
            gRow.Cells[1].Text = i.ToString();

            if (Common.CheckNullString(gRow.Cells[5].Text) != "")
            {
                dtJoinDate = Convert.ToDateTime(gRow.Cells[5].Text);
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
                TimeSpan DateDiff = dtCurrDate - dtJoinDate;
                int iJoinDay = dtJoinDate.Day;
                string strTotDay = Common.ReturnTotalDay(DateDiff.ToString());
                if (strTotDay == "00:00:00")
                    strTotDay = "0";

                if ((ddlLeaveType.SelectedValue.ToString() == "1") || (ddlLeaveType.SelectedValue.ToString() == "3"))
                {
                    if (Convert.ToInt16(strTotDay) >= 20)
                        gRow.Cells[7].Text = "1";
                    else
                        gRow.Cells[7].Text = "0";

                    gRow.Cells[8].Text = Convert.ToString(Convert.ToDecimal(Common.ReturnZeroForNull(gRow.Cells[6].Text)) + Convert.ToDecimal(gRow.Cells[7].Text));
                }
                i++;
            }
        }
    }
       
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    private bool ValidateAndSave()
    {
        try
        {
            if (objLeaveMgr.CheckForMultipleEntry(ddlLeaveType.SelectedValue.ToString()   ,ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString()) == true)
            {
                lblMsg.Text = "Leave already accrued of the month "+ ddlMonth.SelectedItem.Text.Trim() + ", " + ddlYear.SelectedItem.Text.Trim();
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

    private void SaveData()
    {
        string strLogID = "";

        strLogID = Common.getMaxId("EmpLeaveAccrual", "LogId");
        objLeaveMgr.InsertEmpLeaveAccrueLog(strLogID, grEmpList, ddlLeaveType.SelectedValue.ToString(), ddlMonth.SelectedValue,
            ddlYear.SelectedValue, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Record has been saved successfully.";
        this.ClearFields();
    }
    private void ClearFields()
    {
        grEmpList.DataSource = null;
        grEmpList.DataBind();
    }
}