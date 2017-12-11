using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class LeaveApplicationRpt : System.Web.UI.Page 
{
    static string strStartDate = "";
    static string strEndDate = "";
    static string strStartLeavePeriod = "";
    static string strEndLeavePeriod="";
    string[] strVal;

    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            if (string.IsNullOrEmpty(strParams) == false)
            {
                strVal = null;
                char[] splitter ={ ',' };
                strVal = Common.str_split(strParams, splitter);

                strStartDate = DateTime.Now.Year.ToString();
                strEndDate = Convert.ToString(Convert.ToInt32(strStartDate));

                strStartDate = strStartDate + "-" + "01" + "-" + "01";
                strEndDate = strEndDate + "-" + "12" + "-" + "31";

                //Leave Information
                DataTable dtLeaveApp = new DataTable();
                dtLeaveApp = objLeaveMgr.SelectLeaveAppMstRpt(Convert.ToInt32(strVal[1]), strVal[0], strVal[2], strStartDate, strEndDate);
                rptLeavApp.DataSource = dtLeaveApp;
                rptLeavApp.DataBind();

                //Leave Profile
                if (dtLeaveApp.Rows.Count > 0)
                {
                    if (dtLeaveApp.Rows[0]["Gender"].ToString() == "F")
                    {
                        this.FillEmpLeaveProfile(strVal[0], "F");
                    }
                    else
                    {
                        this.FillEmpLeaveProfile(strVal[0], "M");
                    }

                    this.FillEmpLeaveDetails(strVal[0]);
                    this.GetLeavePeriod();

                    //Responsible Person Name & designation

                    //DataTable dtDivLevel = new DataTable();
                    DataTable dtResPerson = new DataTable();
                    //dtSecLevel = objLeaveMgr.SelectDivisionLevel(strVal[0].ToString());
                    //if (dtDivLevel.Rows.Count > 0)
                    //{
                    //    if (dtDivLevel.Rows[0]["SecLevel"].ToString() == "C")

                    dtResPerson = objLeaveMgr.SelectResponsePerson(Convert.ToInt32(strVal[1]), strVal[0].ToString());
                    //else
                    //    dtResPerson = objLeaveMgr.SelectResponsePerson(Convert.ToInt32(strVal[1]), strVal[0].ToString() , "B");

                    //if (dtResPerson.Rows.Count > 0)
                    //{
                    //    rptResponsePerson.DataSource = dtResPerson;
                    //    rptResponsePerson.DataBind();
                    //}
                    //}
                }                
            }
        }
    }

    private void FillEmpLeaveProfile(string EmpId,string Sex)
    {
        DataTable dtEmp = new DataTable();
        dtEmp.Rows.Clear();
        dtEmp.Dispose();

        dtEmp = objLeaveMgr.SelectEmpLeaveProfileEXCPL(EmpId, "0",  Sex);

        if (dtEmp.Rows.Count > 0)
        {
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();

            grLeaveStatus.DataSource = dtEmp;
            grLeaveStatus.DataBind();

            this.FormatLeaveStatusGridNumber();
            strStartLeavePeriod = Common.SetDate(dtEmp.Rows[0]["LeaveStartPeriod"].ToString());
            strEndLeavePeriod = Common.SetDate(dtEmp.Rows[0]["LeaveEndPeriod"].ToString());
        }
    }

    protected void FormatLeaveStatusGridNumber()
    {
        int i = 0;
        foreach (GridViewRow gRow in grLeaveStatus.Rows)
        {
            gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
            gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
            //gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
            //gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[9].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[9].ToString().Trim()));
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1) + (Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)));

            if (Convert.ToDecimal(gRow.Cells[4].Text) < 0)
            {
                gRow.Cells[4].Text = "0";
            }
            i++;
        }        
    }

    public string GetResumeDate(string strLvEndDate)
    {
        DateTime ResumeDate = Convert.ToDateTime(strLvEndDate);
        // Holiday and Weekend Issue Exist. Need to Solve
        return Common.DisplayDate(ResumeDate.ToShortDateString());
    }

    public string GetUpdatedByUserFullName(string strUserID)
    {
        string strUserName="";
        strUserName= objLeaveMgr.SelectUpdatedByUserName(strUserID);
        
        if (strVal[2].Trim() == "R")
            return "";

        if (strVal[2].Trim() == "D")
            return "";

        if (string.IsNullOrEmpty(strUserName) == true)
            return "";
 
        string[] strArray = strUserName.Split(',');
        if (strArray.Length == 2)
        {
           if (strUserID.ToUpper().Trim() == "ADMIN")
               strUserName=strArray[0]+" (Sys.Admin), <br/> "+ strArray[1];
        }

        return strUserName;
    }

    public string GetUpdatedDate(string strDate)
    {
        if (strVal[2].Trim() == "R")
            return "";
        if (strVal[2].Trim() == "D")
            return "";
        if (string.IsNullOrEmpty(strDate) == true)
            return "";
        DateTime UpdatedDate = Convert.ToDateTime(strDate);
        // Holiday and Weekend Issue Exist. Need to Solve
        return Common.DisplayDate(UpdatedDate.ToShortDateString());
    }

    public string GetPrintDate()
    {
        return Common.DisplayDate(DateTime.Today.ToShortDateString());
    }

    public void GetLeavePeriod()
    {
        lblLeavePeriod.Text   = "Leave Records from " + this.GetLeaveStartDate() + " to " + this.GetLeaveEndDate();
    }

    public string GetLeaveStartDate()
    {        
        return Common.DisplayDate(strStartLeavePeriod);
    }

    public string GetLeaveEndDate()
    {
        return Common.DisplayDate(strEndLeavePeriod);
    }

    public string GetCurrentDate()
    {
        return Common.DisplayDate(DateTime.Today.ToShortDateString());
    }

    private void FillEmpLeaveDetails(string EmpId)
    {
        DataTable dtEmpLvDtls = new DataTable();
        dtEmpLvDtls.Rows.Clear();
        dtEmpLvDtls.Dispose();

        dtEmpLvDtls = objLeaveMgr.SelectEmpLeaveDetails(EmpId, strStartLeavePeriod, strEndLeavePeriod);

        if (dtEmpLvDtls.Rows.Count > 0)
        {
            grLeaveDtls.DataSource = null;
            grLeaveDtls.DataBind();

            grLeaveDtls.DataSource = dtEmpLvDtls;
            grLeaveDtls.DataBind();
            this.FormatLeaveDetailsGridNumber();
        }
    }

    protected void FormatLeaveDetailsGridNumber()
    {
        foreach (GridViewRow gRow in grLeaveDtls.Rows)
        {
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
            if (gRow.Cells[5].Text.Trim() == "A")
                gRow.Cells[5].Text = "Availed";
            else if (gRow.Cells[5].Text.Trim() == "R")
                gRow.Cells[5].Text = "Requsted";
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[7].Text)) == false)
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
        }
    }
}
