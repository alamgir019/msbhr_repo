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

public partial class Attendance_OTApproval : System.Web.UI.Page
{
    AttnPolicyTableManager objAttnMgr = new AttnPolicyTableManager();
    AdjustAttendanceTableManager objAdjMgr = new AdjustAttendanceTableManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    AttnPolicyTableManager AttPMgr = new AttnPolicyTableManager();
    dsAttendance ds = new dsAttendance();
    DataTable dtBranchWiseDiv = new DataTable(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["ISADMIN"].ToString() == "Y")
            //{
            //Common.FillDropDownList_All(AttPMgr.GetData("0"), ddlShift);
            //}
            //else
            Common.FillDropDownList_All(AttPMgr.GetData("0"), ddlShift);

            if (Session["ISSHIFTINCHR"].ToString() != "Y")
                Response.Redirect("Attendance.aspx");
            //if (string.IsNullOrEmpty(Session["BRANCHID"].ToString()) == false)
            //{
            //    Common.FillDropDownListWithAll(objMasMgr.SelectBranch("0"), ddlSearchValue, "BranchName", "BranchID");
            //}
            //else
            //{
            //    Common.FillDropDownListWithAll(objMasMgr.SelectBranch("0"), ddlSearchValue, "BranchName", "BranchId");
            //}
            Common.FillDropDownList(objAttnMgr.GetData("0"), ddlShift, "PolicyName", "AttnPolicyId", true);
            this.Bind_DdlDivision();

            this.EntryMode();
        }
    }

    private void Bind_DdlDivision()
    {
        Common.FillDropDownList_All(dtBranchWiseDiv, ddlDivision);
    }
    protected void EntryMode()
    {
        if (Session["ISDEPTHEAD"].ToString() == "Y")
        {
            btnSave.Text = "Approve";
            btnDelete.Visible = true;
        }
        txtTotalOTHr.Text = "";
        txtTotalOTMin.Text = "";      
    }

    protected void OpenRecord()
    {
        string strFromDate = Common.ReturnDate(txtAttnFromDate.Text.Trim());
        string strToDate = "";
        string strSearValue = "";
        if (txtEmpId.Visible == false)
            strSearValue = ddlSearchValue.SelectedValue.ToString();
        else
            strSearValue = txtEmpId.Text.Trim();
        if (chkTo.Checked == true)
            if (string.IsNullOrEmpty(txtAttnDateTo.Text) == false)
                strToDate = Common.ReturnDate(txtAttnDateTo.Text.Trim());
            else
                strToDate = strFromDate;
        //strToDate = Common.ReturnDate(txtAttnDateTo.Text.Trim());
        DataTable dtOT = new DataTable();
        if (Session["ISDEPTHEAD"].ToString() == "Y")
        {
            if (ddlSearchBy.SelectedValue != "4")
            {
                if (ddlShift.SelectedItem.Text == "All")
                {
                    dtOT = objAdjMgr.GetOTData(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), ddlSearchValue.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString(),   ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(),  "Y", ddlShift.SelectedValue.ToString());
                }
                else
                    dtOT = objAdjMgr.GetOTDataShiftWise(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), ddlSearchValue.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString(), ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(), "Y", ddlShift.SelectedValue.ToString());

            }
            else
                if (ddlShift.SelectedItem.Text == "All")
                {
                    dtOT = objAdjMgr.GetOTData(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), strSearValue, ddlDivision.SelectedValue.ToString(), ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(),  "Y", ddlShift.SelectedValue.ToString());
                }
                else
                    dtOT = objAdjMgr.GetOTDataShiftWise(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), strSearValue, ddlDivision.SelectedValue.ToString(), ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(),  "Y", ddlShift.SelectedValue.ToString());
        }
        //if (ddlShift.SelectedItem.Text == "All")
        //{
        //    dtOT = objAdjMgr.GetOTData(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), ddlSearchValue.SelectedValue.ToString(), ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(), ddlLocation.SelectedValue.ToString(), "Y", ddlShift.SelectedValue.ToString());
        //}
        //else
        //    dtOT = objAdjMgr.GetOTDataShiftWise(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), strSearValue, ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(), ddlLocation.SelectedValue.ToString(), "Y", ddlShift.SelectedValue.ToString());



        //else if (Session["ISSHIFTINCHR"].ToString() == "Y")
        //     dtOT = objAdjMgr.GetOTData(strFromDate, strToDate, ddlSearchBy.SelectedValue.ToString(), ddlSearchValue.SelectedValue.ToString(), ddlAttnStatus.SelectedValue.ToString(), Session["USERID"].ToString(), ddlLocation.SelectedValue.ToString(),"N");

        grOT.DataSource = dtOT;
        grOT.DataBind();
        string strH = "";
        string strM = "";
        string strRH = "";
        string strRM = "";
        
        int SlNo = 0;
        decimal dblTotalOTHr = 0;
        decimal dblTotalOTMin = 0;
        foreach (GridViewRow gRow in grOT.Rows)
        {
            SlNo = SlNo + 1;
            gRow.Cells[0].Text = SlNo.ToString();
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            gRow.Cells[7].Text = Common.DisplayTime(gRow.Cells[7].Text);
            gRow.Cells[8].Text = Common.DisplayTime(gRow.Cells[8].Text);            

            strH = ConvertMinDataToHourMin(gRow.Cells[12].Text, "H");
            strM = ConvertMinDataToHourMin(gRow.Cells[12].Text, "M");

            gRow.Cells[12].Text = strH + "H " + strM + "Min";

            if ((Common.CheckNullString(gRow.Cells[13].Text) != "") && (gRow.Cells[12].Text != "0"))
            {
                strRH = ConvertMinDataToHourMin(gRow.Cells[13].Text, "H");
                strRM = ConvertMinDataToHourMin(gRow.Cells[13].Text, "M");
                gRow.Cells[13].Text = strRH + "H " + strRM + "Min";
            }
            else
                gRow.Cells[13].Text = "0";

            HiddenField hfAppSt = new HiddenField();
            hfAppSt = (HiddenField)gRow.Cells[13].FindControl("hfAppStatus");
            if (hfAppSt.Value == "R")
            {
                //gRow.BackColor = System.Drawing.Color.CadetBlue;
                gRow.Enabled = true;
            }
            else if (hfAppSt.Value == "A")
            {
                gRow.BackColor = System.Drawing.Color.Green;
                gRow.Enabled = false;
            }


            // For Summary Toatl Hr & Minute
            if (gRow.BackColor == System.Drawing.Color.Green)
            {
                TextBox txtOTHr = (TextBox)gRow.Cells[14].FindControl("txtOTH");
                dblTotalOTHr = dblTotalOTHr + Convert.ToDecimal(Common.ReturnZeroForNull(txtOTHr.Text));

                TextBox txtOTMin = (TextBox)gRow.Cells[15].FindControl("txtOTM");
                dblTotalOTMin = dblTotalOTMin + Convert.ToDecimal(Common.ReturnZeroForNull(txtOTMin.Text));

                if (Common.CheckNullString(gRow.Cells[17].Text) != "")
                    gRow.Cells[17].Text = Common.DisplayDate(gRow.Cells[17].Text);
                if (Common.CheckNullString(gRow.Cells[19].Text) != "")
                    gRow.Cells[19].Text = Common.DisplayDate(gRow.Cells[19].Text);

                txtTotalOTHr.Text = dblTotalOTHr.ToString();
                //txtTotalOTMin.Text = dblTotalOTMin.ToString();
            }

            decimal dblMod = 0;
            decimal dblDiv = 0;
            decimal dblResult = 0;
            dblMod = dblTotalOTMin % 60;
            dblDiv = dblTotalOTMin / 60;
            dblResult = Math.Truncate(dblDiv);
            if (string.IsNullOrEmpty(txtTotalOTHr.Text) == false)                
                txtTotalOTHr.Text = Convert.ToString(Convert.ToDecimal(txtTotalOTHr.Text) + dblResult);
            
            txtTotalOTMin.Text = dblMod.ToString();  
        }
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
    }

    protected int GetTimeDiff(string InTime, string PcyInTime)
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
            //timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

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
        timeDiff = GetTimeDiff(InTime, PcyInTime);
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
    protected void ddlSearchValue_SelectedIndexChanged(object sender, EventArgs e)
    {
       // dtBranchWiseDiv = objMasMgr.SelectBranchWiseDivision(ddlSearchValue.SelectedValue.ToString());
        this.Bind_DdlDivision();
        if (ddlSearchValue.SelectedValue.ToString().Trim() == "100")
            lblDivision.Text = "Division";
        else
            lblDivision.Text = "Department";
        
        //lblMsg.Text = ddlSearchValue.SelectedValue.ToString();
        //this.Bind_ddlSection(); 
    }

    //protected void Bind_ddlSection()
    //{
    //    Common.FillDropDownList_All(objMasMgr.SelectDeptWiseSectionddl(Convert.ToInt32(ddlSearchValue.SelectedValue.ToString())), ddlSection);
    //}

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable  dtResult=new DataTable();
        switch (ddlSearchBy.SelectedValue.ToString())
        {
            case "1":
                dtResult = objMasMgr.SelectDivision(0);
                txtEmpId.Visible = false;
                ddlSearchValue.Visible = true;
                break;
            case "2":
                //dtResult = objMasMgr.SelectSBU(0);
                //txtEmpId.Visible = false;
                //ddlSearchValue.Visible = true;
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
        if(txtEmpId.Visible == false)
            Common.FillDropDownList(dtResult, ddlSearchValue,1,0, false);
    }

    private string ConvertMinDataToHourMin(string strMinData,string strRetType)
    {
        long lngMin = Convert.ToInt64(strMinData);
        long lngHour = 0;
        lngHour = lngMin / 60;
        lngMin = lngMin - (lngHour * 60);
        if (strRetType == "H")
            return lngHour.ToString();
        else
            return lngMin.ToString();
    }

    private string ConvertHourMinDataToMin(string strH,string strM)
    {
        long lngMin = Convert.ToInt64(strM);
        long lngHour = Convert.ToInt64(strH);
        string strRet = "";
        strRet = Convert.ToString(lngHour * 60 + lngMin);
        return strRet;
    }

    public string SetDefaultOTData(string strRecOT, string strOT, string strForcedOT, string strIsForced, string strType)
    {
        string strRetValue = "";

        if ((strForcedOT != "0") || Common.CheckNullString(strForcedOT) != "")
        {
            if (strIsForced == "Y")
            {
                strRetValue = ConvertMinDataToHourMin(strForcedOT, strType);
                return strRetValue;
            }

        }  

        if ((strRecOT != "0") || Common.CheckNullString(strRecOT) != "")
        {
            strRetValue = ConvertMinDataToHourMin(strRecOT, strType);
            return strRetValue;
        }
        if ((strRecOT == "0") || Common.CheckNullString(strRecOT) == "")
        {
            if (strIsForced == "N")
                strRetValue = ConvertMinDataToHourMin(strOT, strType);
        }

       
        return strRetValue;
    }
   
   
    protected string CheckNullString(string str)
    {
        if ((string.IsNullOrEmpty(str) == false) && str!="&nbsp;")
            return str;
        else
            return  "";
    }
    protected void SaveData(string strAppStatus)
    {
        int CkeckedDataCount = 0;
        try
        {
            foreach (GridViewRow gRow in grOT.Rows)
            {
                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                if (chBox.Checked == true)
                {
                    CkeckedDataCount++;
                }
            }
            OTManager objOTMgr = new OTManager();
            string strInsBy = Session["USERID"].ToString();  
            string strInsDate = Common.SetDateTime(DateTime.Now.ToString());
            if (Session["ISDEPTHEAD"].ToString() == "Y")
            {
                objOTMgr.InsertOTApproval(grOT, CkeckedDataCount, strInsBy, strInsDate, "Y", strAppStatus );
                if (strAppStatus == "D")
                {
                    lblMsg.Text = "OT Denied Successfully";
                }
                else
                {
                    lblMsg.Text = "OT Approved Successfully";
                }
            }
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["ISDEPTHEAD"].ToString() == "Y")
                SaveData("A");
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        grOT.DataSource = dt;
        grOT.DataBind();
        this.EntryMode();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SaveData("D");
    }

    
}
