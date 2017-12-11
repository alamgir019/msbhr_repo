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

public partial class EIS_EmpWiseWeekend : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpWiseWeekendTableManager objEmp = new EmpWiseWeekendTableManager();
    AttnPolicyTableManager objAttnPcyMgr = new AttnPolicyTableManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Bind_ddlDivision();            
            if (string.IsNullOrEmpty(Session["Division"].ToString()) == false)
            {
                ddlDivision.SelectedValue = Session["DivisionId"].ToString();
                ddlDivision.Enabled = false;
                //Common.FillDropDownList_All(objMasMgr.SelectSBUWiseDivision(Convert.ToInt32(ddlDivision.SelectedValue)), ddlSBU);
                //ddlSBU.SelectedValue = Session["SBUId"].ToString();
                //ddlSBU.Enabled = false;
                //Common.FillDropDownList_All(objMasMgr.SelectSBUWiseDept(Convert.ToInt32(ddlSBU.SelectedValue)), ddlDept);
            }
            else
            {
                ddlDivision.Enabled = true;
                ddlSBU.Enabled = true;
            }
            this.Bind_ddlGrade();
            this.Bind_ddlDesignation();
            txtAttndDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());
            hfAttndDate.Value = "";
        }

        string alertScript = "javascript: SearchByChanged();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", alertScript, true);
        //btnShow.Attributes.Add("onClick", "SearchByChanged();");
        //Page.RegisterStartupScript("myScript", "<script language=JavaScript>SearchByChanged();</script>");
    }

    //Start data binding
    protected void Bind_ddlDivision()
    {
        Common.FillDropDownList_All(objMasMgr.GetDivision(), ddlDivision);
    }
    protected void Bind_ddlSBU()
    {
        //Common.FillDropDownList_All(objMasMgr.SelectSBUWiseDivision(Convert.ToInt32(ddlDivision.SelectedValue.ToString())), ddlSBU);
    }
    protected void Bind_ddlDept()
    {
        //Common.FillDropDownList_All(objMasMgr.SelectSBUWiseDept(Convert.ToInt32(ddlSBU.SelectedValue.ToString())), ddlDept);
    }
    protected void Bind_ddlGrade()
    {
        Common.FillDropDownList_All(objMasMgr.SelectGrade(0), ddlGrade);
    }
    protected void Bind_ddlDesignation()
    {
        Common.FillDropDownList_All(objMasMgr.SelectDesignation(0), ddlDesig );
    }

    protected void OpenRecord()
    {
        DataTable dtEmp = objEmp.GetEmployee(txtSearchValue.Text.Trim(), ddlDivision.SelectedValue.ToString(), ddlSBU.SelectedValue.ToString(), ddlDept.SelectedValue.ToString(),
            ddlGrade.SelectedValue.ToString(), ddlDesig.SelectedValue.ToString(), txtSearchValue.Text.Trim(), txtSearchValue.Text.Trim(), txtSearchValue.Text.Trim(),
            ddlSearchBy.SelectedValue.ToString(), Session["USERID"].ToString(), Common.ReturnDate(txtAttndDate.Text.Trim()));
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();
        dtEmp.Rows.Clear();
        dtEmp.Dispose();
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();
        SetCurrentStatusAndOT(grEmployee);
        hfAttndDate.Value = txtAttndDate.Text.Trim();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
    }
    protected void SetCurrentStatusAndOT(GridView grv)
    {
        foreach (GridViewRow gRow in grv.Rows)
        {
            if (Common.CheckNullString(gRow.Cells[5].Text) == "")
            {
                gRow.Cells[5].Text = "A";
                gRow.Cells[7].Text = "0";
            }
            else
            {
                gRow.BackColor = System.Drawing.Color.Salmon;
            }
        }
    }

    protected void RemoveNewtStatusAndOT(GridView grv)
    {
        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chkSelect = new CheckBox();
            chkSelect = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkSelect.Checked == true)
            {
                if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                {
                    gRow.Cells[6].Text = "";
                }

                HiddenField hfExtraTime = new HiddenField();
                hfExtraTime = (HiddenField)gRow.Cells[8].FindControl("hfExtraTimeWorked");
                if (hfExtraTime.Value == "")
                {
                    gRow.Cells[7].Text = "0";
                    gRow.BackColor = System.Drawing.Color.Honeydew;
                }
                else
                {
                    gRow.Cells[7].Text = hfExtraTime.Value;
                    gRow.BackColor = System.Drawing.Color.Salmon;
                }
            }
        }
    }

    protected void SetNewStatusAndOT(GridView grv,DataTable dtAttnPcy)
    {
        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chkSelect = new CheckBox();
            chkSelect = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkSelect.Checked == true)
            {
                gRow.Cells[6].Text = "W";
                HiddenField hfSInTime = new HiddenField();
                hfSInTime = (HiddenField)gRow.Cells[8].FindControl("hfSignInTime");
                
                HiddenField hfSOTime = new HiddenField();
                hfSOTime = (HiddenField)gRow.Cells[8].FindControl("hfSignOutTime");

                HiddenField hfAttnPId = new HiddenField();
                hfAttnPId = (HiddenField)gRow.Cells[8].FindControl("AttnPolicyId");
                if (hfAttnPId.Value != "")
                    gRow.Cells[7].Text = CalCulateOT(dtAttnPcy, Convert.ToInt32(hfAttnPId.Value), hfSInTime.Value, hfSOTime.Value);
                // gRow.Cells[7].Text = "0";
                gRow.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
    protected string CalCulateOT(DataTable dtAttnPcy,int strPolicyId,string strInTime,string strOutTime)
    {
        string strPcyOutTime = "";
        int lunch = 0;
        int InOutDiff=0;
        string strExpr = "AttnPolicyId=" + strPolicyId;
        DataRow[] foundRows = dtAttnPcy.Select(strExpr);
        if (foundRows.Length > 0)
        {
            strPcyOutTime = foundRows[0]["OutTime"].ToString();
            lunch = Convert.ToInt32(foundRows[0]["LunchBreak"].ToString());
            if ((strInTime != "") && (strOutTime != ""))
            {
                DateTime dtinTime = Convert.ToDateTime(strInTime);
                DateTime dtoutTime = Convert.ToDateTime(strOutTime);
                TimeSpan tp = dtinTime-dtoutTime;
                if(tp.Days==0)
                    InOutDiff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(strInTime).ToString()),Common.DisplayTime(strOutTime),"N");
                else
                    InOutDiff = GetTimeDiff(Common.DisplayTime(Convert.ToDateTime(strInTime).ToString()),Common.DisplayTime(strOutTime),"Y");
                //int LunchBreak = Convert.ToInt32(grAttnAdj.DataKeys[intSL - 1].Values[5].ToString());
                int OT = InOutDiff - lunch;
                if (OT >= 0)
                    return Convert.ToString(OT);
                else
                    return "0";
            }
            else
                return "0";
        }
        else
            return "0";
    }
    protected int GetTimeDiff(string InTime, string OutTime,string IsNextDay)
    {
        int timeDiff = 0;
        DateTime dFrom;
        DateTime dTo;
        if (DateTime.TryParse(InTime, out dFrom) && DateTime.TryParse(OutTime, out dTo))
        {
           
                TimeSpan TS = dTo - dFrom;
                int hour = TS.Hours;
                int mins = TS.Minutes;
                int secs = TS.Seconds;
                timeDiff = hour * 60 + mins;
                if (IsNextDay == "Y")
                    timeDiff = 1440 + timeDiff;
            
        }
            //timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
        return timeDiff;
    }


    protected void ddlSBU_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_ddlDept();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_ddlSBU();
    }
    protected void btnSetWeekend_Click(object sender, EventArgs e)
    {
        DataTable dtAttnPcy = objAttnPcyMgr.GetData("0");
        SetNewStatusAndOT(grEmployee, dtAttnPcy);
        dtAttnPcy.Rows.Clear();
        dtAttnPcy.Dispose();
    }
    protected void btnRemoveWeekend_Click(object sender, EventArgs e)
    {
        this.RemoveNewtStatusAndOT(grEmployee);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        grEmployee.DataSource = null;
        grEmployee.DataBind();
        txtAttndDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());
        lblRecordCount.Text = "0";
        hfAttndDate.Value = "";
    }

    protected void SaveData()
    {
        if (grEmployee.Rows.Count > 0)
        {
            if (hfAttndDate.Value != "")
            {
                objEmp.InsertEmpWiseWeekendData(grEmployee, Common.ReturnDate(hfAttndDate.Value), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
                lblMsg.Text = "Record Saved Successfully";
                this.OpenRecord();
            }
        }
        else
        {
            lblMsg.Text = "There is no data to update...";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData();
    }
}
