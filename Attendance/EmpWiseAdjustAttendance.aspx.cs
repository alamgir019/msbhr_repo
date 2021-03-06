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

public partial class Attendance_EmpWiseAdjustAttendance : System.Web.UI.Page
{
    AttnRemindManager objRemMgr = new AttnRemindManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    DataTable dtGridAbsentList = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtEmpInfo = objEmpInfoMgr.SelectEmpInfoOfficeWiseForLeave(Session["EMPID"].ToString().Trim(), "-1");
            if (dtEmpInfo.Rows.Count > 0)
            {
                lblEmpID.Text = Session["EMPID"].ToString().Trim();

                lblName.Text = dtEmpInfo.Rows[0]["FullName"].ToString().Trim() + ", " + dtEmpInfo.Rows[0]["JobTitle"].ToString().Trim()
                             + ", " + dtEmpInfo.Rows[0]["DivisionName"].ToString().Trim();
                hfSupervisor.Value=dtEmpInfo.Rows[0]["REPORTINGTO"].ToString().Trim();
                DataTable dtSuper = objEmpInfoMgr.SelectEmpInfoOfficeWiseForLeaveSPV(hfSupervisor.Value.Trim(), "-1");
                if (dtSuper.Rows.Count > 0)
                {
                    lblSupervisor.Text = dtSuper.Rows[0]["FullName"].ToString().Trim() + ", " + dtSuper.Rows[0]["DivisionName"].ToString().Trim()
                         + ", " + dtSuper.Rows[0]["JobTitle"].ToString().Trim();
                    
                    hfSupervisorEmail.Value = dtSuper.Rows[0]["OfficeEmail"].ToString().Trim();
                }
            }
            lblMsg.Text = "Your request is sent to your supervisor's module";
        }

    }
    public void IniGridDataTable()
    {
        dtGridAbsentList.Columns.Add("ATTNDDATE");
        dtGridAbsentList.Columns.Add("REMARKS");
        dtGridAbsentList.AcceptChanges();
    }

    protected void btnGetAbsentRecord_Click(object sender, EventArgs e)
    {
        this.IniGridDataTable();
        DataTable dtAbsent = objRemMgr.GetAbsentRecord("'PIB00004'", Common.ReturnDate(txtDateFrom.Text.Trim()), Common.ReturnDate(txtDateTo.Text.Trim()));
        foreach (DataRow dRow in dtAbsent.Rows)
        {
            DataRow nRow = dtGridAbsentList.NewRow();
            nRow["ATTNDDATE"] =Common.DisplayDate(dRow["ATTNDDATE"].ToString().Trim());
            nRow["REMARKS"] = "";
            dtGridAbsentList.Rows.Add(nRow);
            dtGridAbsentList.AcceptChanges();
        }
        grDateList.DataSource = dtGridAbsentList;
        grDateList.DataBind();
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}
