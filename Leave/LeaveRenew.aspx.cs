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

public partial class Leave_LeaveRenew : System.Web.UI.Page
{
    LeaveRenewManger objLvRenewMgr = new LeaveRenewManger();
    DataTable dtLv = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    LeaveManager objLvPakMgr = new LeaveManager(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            this.Bind_ddlLeavePackage();         
        }
    }

    protected void Bind_ddlLeavePackage()
    {
        Common.FillDropDownList(objLvPakMgr.SelectLeavePakMst(0), ddlLeavePak,true);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        long sl = 0;
        if (ddlLeavePak.SelectedValue.ToString() == "-1")
        {
            lblMsg.Text = "Please select leave package.";
            grEmployee.DataSource = null;
            grEmployee.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else
        {
            DataTable dtEmp = objLvRenewMgr.SelectLvPackageWsEmp(ddlLeavePak.SelectedValue.ToString());
            grEmployee.DataSource = dtEmp;
            grEmployee.DataBind();
            dtEmp.Rows.Clear();
            dtEmp.Dispose();
            lblRecordCount.Text = grEmployee.Rows.Count.ToString();

            foreach (GridViewRow grow in grEmployee.Rows)
            {
                sl = sl + 1;
                grow.Cells[1].Text = sl.ToString();

                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)grow.Cells[0].FindControl("chkBox");
                chBox.Checked = true;
                grow.Cells[0].Enabled = false;
            }

            dtLv = objLvRenewMgr.SelectOldLeavePeriod(ddlLeavePak.SelectedValue.ToString());
            GridView1.DataSource = dtLv;
            GridView1.DataBind();
            this.formatGridview();

            GridView2.DataSource = dtLv;
            GridView2.DataBind();
            this.formatGridview2();

            if (dtLv.Rows.Count > 0)
            {
                foreach (DataRow row in dtLv.Rows)
                {
                    DateTime strCurrStYr = Convert.ToDateTime(row[0].ToString());
                    DateTime strCurrEndYr = Convert.ToDateTime(row[1].ToString());

                    if ((Convert.ToInt32(strCurrStYr.Year.ToString()) + 1) != Convert.ToInt32(DateTime.Now.Year.ToString()))
                    {
                        lblMsg.Text = "Leave renew has been already completed. So leave renew can be done on next year, 1st " + ConvertToMonthName(strCurrStYr.Month.ToString());
                        btnStart.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblMsg.Text = "Please press on start button for leave renewing.";
                        btnStart.Enabled = true;
                    }

                    if ((Convert.ToInt32(strCurrStYr.Month.ToString())) != Convert.ToInt32(DateTime.Now.Month.ToString()))
                    {
                        lblMsg.Text = "Leave renew has been already completed. So leave renew can be done on next year, 1st " + ConvertToMonthName(strCurrStYr.Month.ToString());
                        btnStart.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblMsg.Text = "Please press on start button for leave renewing.";
                        btnStart.Enabled = true;
                    }

                    //if ((Convert.ToInt32(strCurrStYr.Day.ToString())) != Convert.ToInt32(DateTime.Now.Day.ToString()))
                    //{
                    //    lblMsg.Text = "Leave renew has been already completed. So leave renew can be done next of " + ConvertToMonthName(strCurrStYr.Month.ToString());
                    //    btnStart.Enabled = false;
                    //}
                    //else
                }
            }
        }
    }

    private void formatGridview()
    {
        foreach (GridViewRow gRow in GridView1.Rows)
        {
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
        }
    }

    private void formatGridview2()
    {
        foreach (GridViewRow gRow in GridView2.Rows)
        {
            gRow.Cells[1].Text = this.DisplayDate1(gRow.Cells[1].Text);
            gRow.Cells[2].Text = this.DisplayDate1(gRow.Cells[2].Text);
        }
    }

    public string DisplayDate1(string str)
    {
        //return str;
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate;
        DateTime dtDate;
        char[] splitter = { ' ' };
        arInfo = Common.str_split(str, splitter);
        dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = dtDate.Day.ToString();
        strMon = dtDate.Month.ToString();
        strYear = Convert.ToString(dtDate.Year + 1);
        if (strDay.Length < 2)
        {
            strDay = "0" + strDay;
        }
        if (strMon.Length < 2)
        {
            strMon = "0" + strMon;
        }
        strDate = strDay + "/" + strMon + "/" + strYear;
        return strDate;
    }

    protected void cmdStart_Click(object sender, EventArgs e)
    {
        try
        {
            //Leave Renew
            objLvRenewMgr.UpdateLeaveProfile(grEmployee, ddlLeavePak.SelectedValue.ToString());
            objLvRenewMgr.UpdateLeaveEntitlement(Session["USERID"].ToString());

            objLvRenewMgr.InsertLeaveTypeHistory();

            dtLv = objLvRenewMgr.SelectOldLeavePeriod(ddlLeavePak.SelectedValue.ToString());
            btnStart.Enabled = false;
            GridView1.DataSource = dtLv;
            GridView1.DataBind();
            this.formatGridview();

            GridView2.DataSource = dtLv;
            GridView2.DataBind();
            this.formatGridview2();

            //Medical Renew
            objLvRenewMgr.UpdateMedicalProfile(grEmployee, ddlLeavePak.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        lblMsg.Text = "Leave renew process has been completed successfully";
    }

    private string ConvertToMonthName(string sMonth)
    {
       
        //switch (ddlMonth.SelectedValue.ToString())
        switch (sMonth)
        {
            case "1":
                return "January";                
            case "2":
                return "February";                
            case "3":
                return "March";                
            case "4":
                return "April";
            case "5":
                return "May";
            case "6":
                return "June";
            case "7":
                return "July";
            case "8":
                return "August";
            case "9":
                return "September";
            case "10":
                return "October";
            case "11":
                return "November";
            case "12":
                return "December";
        }
        return "";
    }
}
