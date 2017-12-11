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

public partial class Attendance_AdjustAttendance : System.Web.UI.Page
{
    AttnPolicyTableManager objAttnMgr = new AttnPolicyTableManager();
    AdjustAttendanceTableManager objAdjMgr = new AdjustAttendanceTableManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    
    dsAttendance ds = new dsAttendance();
    DataTable dtBranchWiseDiv = new DataTable();
    DataTable dtDivision = new DataTable(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objAttnMgr.GetData("0"), ddlShift, "PolicyName", "AttnPolicyId", true);
           // Common.FillDropDownListWithAll(objMasMgr.SelectDepartment(0), ddlSearchValue, "DeptName", "DeptId");
            Common.FillDropDownListWithAll(objMasMgr.SelectSalaryLocation(0), ddlLocation, "SalLocName", "SalLocId");
           // Common.FillDropDownListWithAll(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, "TypeName", "EmpTypeID");  
            string alertScript = "javascript: SearchByChanged();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", alertScript, true);
            ClientScript.RegisterStartupScript(this.GetType(), "alertScript", alertScript);

        }
    }


    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string strFDate = Common.ReturnDate(txtAttnFromDate.Text.Trim());
        string strFromDate = Common.ReturnDateFormat(txtAttnFromDate.Text.Trim(), false);
        string strToDate = "";
        string strSearValue = "";

        if (chkTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtAttnDateTo.Text) == false)
                strToDate = Common.ReturnDateFormat(txtAttnDateTo.Text.Trim(), true);
            else
                strToDate = strFDate;
        }
        else
        {
            strToDate = strFDate;
        }
        if (ddlSearchBy.SelectedValue == "3")
            strSearValue = ddlSearchValue.SelectedValue.ToString();
        else
            strSearValue = txtEmpId.Text.Trim();

        DataTable dtAttnAdj = objAdjMgr.GetData(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), strSearValue,
            ddlAttnStatus.SelectedValue.ToString(), ddlLocation.SelectedValue.ToString(),
            "0");
        grAttnAdj.DataSource = dtAttnAdj;
        grAttnAdj.DataBind();
        foreach (GridViewRow gRow in grAttnAdj.Rows)
        {
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            if (Common.CheckNullString(gRow.Cells[6].Text) != "")
            {
                gRow.Cells[6].Text = Common.DisplayDateTime(gRow.Cells[6].Text).ToString();
            }
            if (Common.CheckNullString(gRow.Cells[8].Text) != "")
            {
                gRow.Cells[8].Text = Common.DisplayDateTime(gRow.Cells[8].Text).ToString();
            }
        }
        string alertScript = "javascript: SearchByChanged();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", alertScript, true);
        ClientScript.RegisterStartupScript(this.GetType(), "alertScript", alertScript);
    }

    protected int GetTimeDiff(string InTime, string PcyInTime, string IsNextDay)
    {
        int timeDiff = 0;
        DateTime dFrom;
        DateTime dTo;

        if (DateTime.TryParse(InTime, out dFrom) && DateTime.TryParse(PcyInTime, out dTo))
        {
            TimeSpan TS = dTo - dFrom;
            int hour = TS.Hours;
            int mins = TS.Minutes;
            int secs = TS.Seconds;
            timeDiff = hour * 60 + mins;
            if (IsNextDay == "Y")
                timeDiff = 1440 + timeDiff;
        }
        return timeDiff;
    }

    public string CalculateDelay(string strInTime,string strPolicyInTime,string strArrGraceTime)
    {
        string strRetValue = "";
        if (string.IsNullOrEmpty(strInTime) == true)
            return "0";
        if (string.IsNullOrEmpty(strPolicyInTime) == true)
            return "0";
        int GraceTime = Convert.ToInt32(strArrGraceTime);
        int timeDiff = 0;
        int delay = 0;
        string InTime = Common.DisplayTime(strInTime);
        string PcyInTime = Common.DisplayTime(strPolicyInTime);
        timeDiff = GetTimeDiff(InTime, PcyInTime,"N");
        delay = timeDiff + GraceTime;
        if (timeDiff >= 0)
            strRetValue = "0";
        else
        {
            if (delay >= 0)
                strRetValue = "0";
            else 
                strRetValue = Convert.ToString(Math.Abs(timeDiff));           
        } 
        return strRetValue;
    }    

    //private void Bind_DdlDivision()
    //{
    //    Common.FillDropDownList_All(dtBranchWiseDiv, ddlDivision);
    //}

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtResult = new DataTable();
        switch (ddlSearchBy.SelectedValue.ToString())
        {
            case "1":
                dtResult = objMasMgr.SelectDivision(0);
                txtEmpId.Visible = false;
                ddlSearchValue.Visible = true;
                break;
            case "2":
                dtResult = objMasMgr.SelectSBU(0);
                txtEmpId.Visible = false;
                ddlSearchValue.Visible = true;
                break;
            case "3":
                dtResult = objMasMgr.SelectDepartment(0);
                txtEmpId.Visible = false;
                ddlSearchValue.Visible = true;
                break;
            case "4":
                txtEmpId.Visible = true;
                ddlSearchValue.Visible = false;
                break;
        }
        if (txtEmpId.Visible == false)
            Common.FillDropDownList(dtResult, ddlSearchValue, 1, 0, false);
    }

    protected void CheckedSelectedGrid()
    {
        int i=0;
        foreach (GridViewRow gRow in grAttnAdj.Rows)
        {
            DataRow row = ds.dtAttendance.NewRow();
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            string strIsUpdated = CheckNullString(grAttnAdj.DataKeys[i].Values[7].ToString());
            if (strIsUpdated == "Y")
            {
                chBox.Checked = true;
                grAttnAdj.Rows[i].BackColor = System.Drawing.Color.LightPink;
            }
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
            i++;
        }
    }

    protected void AddToDataTableFromGrid ()
    {
        ds.dtAttendance.Rows.Clear();
        int i = 1;
        foreach (GridViewRow gRow in grAttnAdj.Rows)
        {
            DataRow row = ds.dtAttendance.NewRow();
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == false)
            {
                row["SL"] = i;
                row["EmpId"] = CheckNullString(gRow.Cells[1].Text);
                row["FullName"] = CheckNullString(gRow.Cells[2].Text);
                row["JobTitle"] = this.CheckAmpercent(this.CheckNullString(gRow.Cells[3].Text));
                row["DeptName"] = this.CheckAmpercent(this.CheckNullString(gRow.Cells[4].Text));
                row["AttndDate"] = Common.ReturnDateFormat_ddmmyyyy(gRow.Cells[5].Text, false);          
                row["SignInTime"] = CheckNullString(gRow.Cells[6].Text);
                row["InLocation"] = CheckNullString(gRow.Cells[7].Text);
                row["SignOutTime"] = CheckNullString(gRow.Cells[8].Text);
                row["OutLocation"] = CheckNullString(gRow.Cells[9].Text);
                row["Status"] = CheckNullString(gRow.Cells[10].Text);
                row["Delay"] = null;
                row["Remarks"] = CheckNullString(gRow.Cells[12].Text);
                row["PolicyName"] = CheckNullString(gRow.Cells[13].Text);
                row["ChangedShift"] = CheckNullString(gRow.Cells[14].Text);
                row["CardNo"] = CheckNullString(gRow.Cells[15].Text);
                row["isUpdatedManually"] = CheckNullString(gRow.Cells[16].Text);
                row["ExtraTimeWorked"] = CheckNullString(gRow.Cells[17].Text);
                row["SunIn"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[1].ToString());
                row["SunOut"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[2].ToString());
                row["ArvlGrace"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[3].ToString());
                row["AttnPolicyId"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[4].ToString());
                row["LunchBreak"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[5].ToString());
                row["OTStartGrace"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[6].ToString());
                row["IsUpdated"] = CheckNullString(grAttnAdj.DataKeys[i - 1].Values[7].ToString());
            }
            else
            {
                row = SetAdjustedData(row,gRow,i);
            }
            ds.dtAttendance.Rows.Add(row);
            ds.dtAttendance.AcceptChanges();
            i++;
        }
        grAttnAdj.DataSource = ds.dtAttendance;
        grAttnAdj.DataBind();
        this.CheckedSelectedGrid();
        chkIn.Checked = false;
        chkOut.Checked = false;
        chkShift.Checked = false;
        chkStatus.Checked = false;

        string alertScript = "javascript: SearchByChanged();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", alertScript, true);
        ClientScript.RegisterStartupScript(this.GetType(), "alertScript", alertScript);
    }

    protected DataRow SetAdjustedData(DataRow row, GridViewRow gRow,int intSL)
    {
        DateTime dtAttndDate=DateTime.Now ;
        string strInTime="";
        string strAtDate = "";
        string strInDate="";
        string strInDateTime = "";
        string strOutTime = "";
        string strOutDate = "";
        string strOutDateTime = "";
        //Used For Shift Changed
        int ArvlGrace = 0;
        int OTGrace = 0;
        string SignIn = "";
        string SignOut = "";
        int Lunch = 0;
        bool IsOTCalculated = false;
        bool IsDelayCalculated = false;
        string strIsNextDay = chkIsNextDay.Checked == true ? "Y" : "N";
        
        row["SL"] = intSL;
        row["EmpId"] = CheckNullString(gRow.Cells[1].Text);
        row["FullName"] = CheckNullString(gRow.Cells[2].Text);
        row["JobTitle"] =this.CheckAmpercent(CheckNullString(gRow.Cells[3].Text));
        row["DeptName"] =this.CheckAmpercent(CheckNullString(gRow.Cells[4].Text));
        //dtAttndDate = Convert.ToDateTime ( CheckNullString(gRow.Cells[5].Text),);
        row["AttndDate"] = Common.ReturnDateFormat_ddmmyyyy(gRow.Cells[5].Text,false ); 
        if (chkIn.Checked == true)
        {
            if ((string.IsNullOrEmpty(gRow.Cells[5].Text) == false)&&(gRow.Cells[5].Text!="&nbsp;"))
            {
                strInDate = gRow.Cells[5].Text;                
                strInTime = ddlInHour.SelectedValue.ToString() + ":" + ddlInMin.SelectedValue.ToString();
                strInDateTime = Common.ReturnDate(strInDate) + " " + strInTime;
                DateTime SignInDateTime = Convert.ToDateTime(strInDateTime); 
                row["SignInTime"] = SignInDateTime.ToString("dd/MM/yyyy hh:mm tt");
            }
            else
            {
                row["SignInTime"] = "";
                strInDateTime = "";
            }           
        }
        else
        {
            strInDateTime = CheckNullString(gRow.Cells[6].Text);
            string[] arinfo = new string[4];
            char[] spl={ ' ' };
            arinfo = Common.str_split(strInDateTime, spl);
            if (strInDateTime != "")
            {
                strInDateTime = Common.ReturnDate(arinfo[0]) + " " + arinfo[1] + " " + arinfo[2];
                row["SignInTime"] = Common.DisplayDateTime(strInDateTime);
            }
        }
        row["InLocation"] = CheckNullString(gRow.Cells[7].Text);

        if (chkOut.Checked == true)
        {
            if ((string.IsNullOrEmpty(gRow.Cells[5].Text) == false)&&(gRow.Cells[5].Text!="&nbsp;"))
            {
                strOutDate = gRow.Cells[5].Text;
                strOutTime = ddlOutHour.SelectedValue.ToString() + ":" + ddlOutMin.SelectedValue.ToString();
                strOutDateTime = Common.ReturnDate(strOutDate) + " " + strOutTime;
                DateTime SignOutDateTime = Convert.ToDateTime(strOutDateTime);
                if (chkIsNextDay.Checked == true)
                {
                    SignOutDateTime = SignOutDateTime.AddDays(1);
                    strOutDateTime = SignOutDateTime.ToString();
                    strIsNextDay = "Y";
                    row["SignOutTime"] = Common.DisplayDateTime(SignOutDateTime.ToString());
                }

                else
                {
                    row["SignOutTime"] = Common.DisplayDateTime(SignOutDateTime.ToString());
                    strIsNextDay = "N";
                }
            }
            else
            {
                row["SignOutTime"] = "";
                strOutDateTime = "";
            }            
        }
        else
        {
            char[] strSpliter ={ ' ' };
            string[] arinfo=new string[4] ;

            arinfo = gRow.Cells[8].Text.Split(strSpliter); 
            strOutDate = arinfo[0];
            if (Common.CheckNullString(strOutDate) != "")
            {
                strOutDate = Common.ReturnDate(arinfo[0]);
                strOutTime = arinfo[1] + " " + arinfo[2];
                strOutDateTime = strOutDate + " " + strOutTime;
                arinfo = null;

                if ((strOutDateTime != "") && (strInDateTime != ""))
                {
                    //TimeSpan tsDayDiff = Convert.ToDateTime(Common.DisplayDate(strOutDateTime)) - Convert.ToDateTime(Common.DisplayDate(strInDateTime));
                    TimeSpan tsDayDiff = new TimeSpan();
                    //if (chkOut.Checked = false)
                    //{

                    //tsDayDiff = Convert.ToDateTime(strOutDateTime) - DateTime.ParseExact(strInDateTime, "dd/MM/yyyy h:mm:ss tt", new System.Globalization.CultureInfo("en-GB", true));
                    tsDayDiff = Convert.ToDateTime(strOutDateTime) - Convert.ToDateTime(strInDateTime);
                    //txtRemarks.Text = strOutDateTime + "---------" + strInDateTime;
                    
                    //}
                    //else
                    //    tsDayDiff = DateTime.ParseExact(strOutDateTime, "MM/dd/yyyy h:mm:ss tt", new System.Globalization.CultureInfo("en-GB", true)) - DateTime.ParseExact(strInDateTime, "dd/MM/yyyy h:mm:ss tt", new System.Globalization.CultureInfo("en-GB", true));
                    if (tsDayDiff.Days != 0)
                        strIsNextDay = "Y";
                    else
                        strIsNextDay = "N";
                }
            }
            if (strOutDateTime != "")
                row["SignOutTime"] = Common.DisplayDateTime(strOutDateTime);
        }
        
        row["OutLocation"] = CheckNullString(gRow.Cells[9].Text);
        if (chkShift.Checked == true)
        {
            DataTable dtShift = objAttnMgr.GetData(ddlShift.SelectedValue.ToString());
            if (dtShift.Rows.Count > 0)
            {
                ArvlGrace = Convert.ToInt32(dtShift.Rows[0]["ArvlGrace"]);
                OTGrace = Convert.ToInt32(dtShift.Rows[0]["OTStartGrace"]);
                SignIn = dtShift.Rows[0]["InTime"].ToString();
                SignOut = dtShift.Rows[0]["OutTime"].ToString();
                Lunch = Convert.ToInt32(dtShift.Rows[0]["LunchBreak"].ToString());

                row["SunIn"] = SignIn;
                row["SunOut"] = SignOut;
                row["ArvlGrace"] = ArvlGrace.ToString();
                row["AttnPolicyId"] = ddlShift.SelectedValue.ToString();
                row["LunchBreak"] = Lunch.ToString();
                row["OTStartGrace"] = OTGrace.ToString();
                int intDiff;
                if(strOutDateTime!="")
                    intDiff = GetTimeDiff(Common.DisplayTime(SignOut), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()),"N");
                else
                    intDiff=0;
                //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                int OTTime=0;
                int Diff = 0;
                if (intDiff > OTGrace)
                {
                    if ((strInDateTime != "") && (strOutDateTime != ""))
                    {
                        Diff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(strInDateTime).ToString()), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()),strIsNextDay);
                        OTTime = intDiff - Lunch;
                    }
                    else
                    {
                        OTTime = 0;
                    }
                }
                else
                    OTTime=0;

                row["ExtraTimeWorked"] = OTTime;
                row["ChangedShift"] = ddlShift.SelectedItem.ToString();
                IsOTCalculated = true;
            }
        }
        else
        {
            Lunch = string.IsNullOrEmpty(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString()) == false ? Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString()) : 0;
            ArvlGrace = string.IsNullOrEmpty(grAttnAdj.DataKeys[intSL - 1].Values[3].ToString()) == false ? Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[3].ToString()) : 0;
            OTGrace = string.IsNullOrEmpty(grAttnAdj.DataKeys[intSL - 1].Values[6].ToString())==false? Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[6].ToString()):0;
            SignIn = CheckNullString(grAttnAdj.DataKeys[intSL - 1].Values[1].ToString());
            SignOut = CheckNullString(grAttnAdj.DataKeys[intSL - 1].Values[2].ToString());

            row["SunIn"] = SignIn;
            row["SunOut"] = SignOut;
            row["ArvlGrace"] = ArvlGrace.ToString();
            row["AttnPolicyId"] = CheckNullString(grAttnAdj.DataKeys[intSL - 1].Values[4].ToString());
            row["LunchBreak"] = Lunch.ToString();
            row["OTStartGrace"] = OTGrace.ToString();
            row["ExtraTimeWorked"] = CheckNullString(gRow.Cells[17].Text);
            row["ChangedShift"] = CheckNullString(gRow.Cells[14].Text);
            IsOTCalculated = false;
        }

        lblMsg.Text = "Processed";

        if (chkStatus.Checked == true)
        {
            switch (ddlStatus.SelectedValue.ToString())
            {
                case "A":
                    row["SignInTime"] = null;
                    row["SignOutTime"] = null;
                    row["Status"] = "A";
                    row["ExtraTimeWorked"] = "0";
                    break;
                case "W":
                    if (strInDateTime != "")
                        row["Status"] = "WP";
                    else
                        row["Status"] = "W";

                    if ((strOutDateTime != "") && (strInDateTime != ""))
                    {
                        int InOutDiff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(strInDateTime).ToString()), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()),strIsNextDay);
                        //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                        int OT = InOutDiff - Lunch;
                        if (OT >= 0)
                            row["ExtraTimeWorked"] = Convert.ToString(OT);
                        else
                            row["ExtraTimeWorked"] = "0";
                    }
                    else
                        row["ExtraTimeWorked"] = "0";
                    break;
                case "H":
                    if(strInDateTime != "")
                        row["Status"] = "HP";
                    else
                        row["Status"] = "H";

                    if ((strOutDateTime != "") && (strInDateTime != ""))
                    {
                        int InOutDiff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(strInDateTime).ToString()), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()),strIsNextDay);
                        //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                        int OT = InOutDiff - Lunch;
                        if (OT >= 0)
                            row["ExtraTimeWorked"] = Convert.ToString(OT);
                        else
                            row["ExtraTimeWorked"] = "0";
                    }
                    else
                        row["ExtraTimeWorked"] = "0";
                    break;
                case "WD":

                    if ((strOutDateTime != "") && (strInDateTime != "") && SignIn != "")
                    {
                        int intDelay = Convert.ToInt32(CalculateDelay(Convert.ToString(Convert.ToDateTime(strInDateTime)), SignIn, ArvlGrace.ToString()));
                        if (intDelay <= 0)
                            row["Status"] = "P";
                        else
                            row["Status"] = "L";

                        int InOutDiff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(SignOut).ToString()), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()), strIsNextDay);
                        //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                        int OT = InOutDiff - Lunch;
                        if (OT >= 0)
                            row["ExtraTimeWorked"] = Convert.ToString(OT);
                        else
                            row["ExtraTimeWorked"] = "0";
                    }
                    else
                    {
                        row["ExtraTimeWorked"] = "0";

                    }
                    break;
            }
            IsOTCalculated = true;
            IsDelayCalculated = true;
        }
        else
        {
            row["Status"] = CheckNullString(gRow.Cells[10].Text);
            IsOTCalculated = false;
            IsDelayCalculated = false;
        }

        if ((chkIn.Checked == true) && (IsDelayCalculated == false))
        {
            if ((strOutDateTime != "") && (strInDateTime != "") && SignIn != "")
            {
                int intInDelay = Convert.ToInt32(CalculateDelay(Convert.ToString(Convert.ToDateTime(strInDateTime)), SignIn, ArvlGrace.ToString()));
                if (intInDelay <= 0)
                    row["Status"] = "P";
                else
                    row["Status"] = "L";
            }
        }
        if ((chkOut.Checked == true)&&(IsDelayCalculated==false))
        {
            if ((strOutDateTime != "") && (strInDateTime != "") && SignIn != "")
            {
                int intOutDiff = GetTimeDiff(Common.DisplayTime(SignOut), Common.DisplayTime(Convert.ToDateTime(strOutDateTime).ToString()),strIsNextDay);
                //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                int OTTime2 = 0;
                int Diff2 = 0;
                if (intOutDiff > OTGrace)
                {                    
                    //Diff2 = GetTimeDiff(Common.DisplayTime(strInDateTime), Common.DisplayTime(strOutDateTime), strIsNextDay);
                    //Edit at 29.11.10
                    //Diff2 = GetTimeDiff(Common.DisplayTime(SignOut), Common.DisplayTime(strOutDateTime), strIsNextDay);
                    OTTime2 = intOutDiff - Lunch;
                }
                else
                    OTTime2 = 0;

                row["ExtraTimeWorked"] = OTTime2;
            }
        }

        //strInDateTime="16/03/2010 9:00:00 AM";
        //Convert.ToDateTime(strInDateTime).ToString();

        //row["Status"] = CheckNullString(gRow.Cells[10].Text);
        row["Delay"] = "";
        row["Remarks"] = CheckNullString(txtRemarks.Text.Trim());
        row["PolicyName"] = CheckNullString(gRow.Cells[13].Text);
        row["CardNo"] = CheckNullString(gRow.Cells[15].Text);
        row["isUpdatedManually"] = "Y";
       // 
        row["IsUpdated"] = "Y";
        return row;
    }
    //protected string CalculateOT(string strInTime, string strOutTime,string strPlcInTime,string strPlcOutTime, string strArrGrace, string strOTGrace)
    //{

    //}


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddToDataTableFromGrid();
    }

    protected string CheckNullString(string str)
    {
        if ((string.IsNullOrEmpty(str) == false) && str!="&nbsp;")
            return str;
        else
            return  "";
    }

    protected string CheckAmpercent(string str)
    {
        string[] strText = str.Split(' ');
        string strRetText = "";
        if (strText.Length > 0)
        {
            for (int i = 0; i < strText.Length; i++)
            {
                if (strText[i] == "&amp;")
                    strText[i] = "&";
                strRetText = strRetText + " " + strText[i];
            }
        }
        else
        {
            strRetText = str;
        }
        return strRetText;

    }

    protected void SaveData()
    {
        int CkeckedDataCount = 0;
        try
        {
            foreach (GridViewRow gRow in grAttnAdj.Rows)
            {
                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                if (chBox.Checked == true)
                {
                    CkeckedDataCount++;
                }
            }
            
            string strInsBy = Session["USERID"].ToString()  ;
            string strInsDate = Common.SetDateTime(DateTime.Now.ToString());
            objAdjMgr.InsertAdjustAttendance(grAttnAdj, CkeckedDataCount, strInsBy, strInsDate);
            lblMsg.Text = "Record Adjusted Successfully";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        grAttnAdj.DataSource = dt;
        grAttnAdj.DataBind();
        chkIn.Checked = false;
        chkOut.Checked = false;
        chkIsNextDay.Checked = false;
        chkShift.Checked = false;
        chkStatus.Checked = false;
        txtRemarks.Text = "";
    }

    
    protected void chkShift_CheckedChanged(object sender, EventArgs e)
    {

    }
   
    protected void ddlOutHour_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
}

