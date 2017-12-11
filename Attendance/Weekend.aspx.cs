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

public partial class Attendance_Weekend : System.Web.UI.Page
{
    WeekendTableManager objWeekMgr = new WeekendTableManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            this.OpenRecord();
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
        grWeekend.DataSource = objWeekMgr.GetData();
        grWeekend.DataBind();
    }


    protected void CheckWeekendDay(string strDay,string strDBValue)
    {
        if (strDBValue == "Y")
        {
            foreach (ListItem lst in chkDayList.Items)
            {
                if (string.Compare(strDay, lst.Value) == 0)
                {
                    lst.Selected = true;
                    break;
                }
            }
        }
    }

    protected void UnCheckWeekendDay()
    {
        foreach (ListItem lst in chkDayList.Items)
        {
            lst.Selected = false;
        }
    }


    private void SaveData(string IsDelete)
    {

        try
        {
            string strWeekendId = "";
            int i = 0;
            string IsUpdate = hfIsUpdate.Value;
            string[] strListDays=new string[7];
            // Initialize the Default Value to the Day String Array
            for (i = 0; i < 7;i++)
            {
                strListDays[i] = "N";
            }
            i=0;
            // Set the value of the Checked days to "Y"
            foreach (ListItem li in chkDayList.Items)
            {
                if(li.Selected==true)
                {
                switch (li.Value)
                {
                    case "SUN":
                        strListDays[0] = "Y";
                         i++;
                        break;
                    case "MON":
                        strListDays[1] = "Y";
                         i++;
                        break;
                    case "TUE":
                        strListDays[2] = "Y";
                         i++;
                        break;
                    case "WED":
                        strListDays[3] = "Y";
                         i++;
                        break;
                    case "THU":
                        strListDays[4] = "Y";
                         i++;
                        break;
                    case "FRI":
                        strListDays[5] = "Y";
                         i++;
                        break;
                    case "SAT":
                        strListDays[6] = "Y";
                         i++;
                        break;
                }
                }
               
            }
            // Get/set the Weekend Id
            if ((IsUpdate == "N") && (IsDelete == "N"))
            {
                strWeekendId = Common.getMaxId("WeekEndPackmst", "WeekEndID");
            }
            else
            {
                strWeekendId = hfWeekendId.Value;
            }
            string strIsActive = chkInActive.Checked == true ? "N" : "Y";
            string strDaysCount=i.ToString();

            Weekend objWeek = new Weekend(strWeekendId, txtPrifleName.Text.Trim(),strListDays[0],strListDays[1],strListDays[2],strListDays[3],strListDays[4],strListDays[5],strListDays[6],strDaysCount, strIsActive,Session["USERID"].ToString(), Common.SetDate(DateTime.Today.ToString()));

            objWeekMgr.InsertWeekend(IsUpdate, IsDelete, objWeek);

            if ((IsUpdate == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "Y"))
                lblMsg.Text = "Record Deleted Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
            this.UnCheckWeekendDay();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }


    protected void grWeekend_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        string[] arInfo = new string[3];
        switch (_commandName)
        {
            case ("DoubleClick"):
                
                hfWeekendId.Value = grWeekend.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtPrifleName.Text = grWeekend.SelectedRow.Cells[1].Text;
                chkInActive.Checked = grWeekend.SelectedRow.Cells[3].Text == "Y" ? false : true;

                this.UnCheckWeekendDay();

                HiddenField hfWESun = new HiddenField();
                hfWESun = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWESun");
                CheckWeekendDay("SUN", hfWESun.Value);

                HiddenField hfWEMon = new HiddenField();
                hfWEMon = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWEMon");
                CheckWeekendDay("MON", hfWEMon.Value);

                HiddenField hfWETues = new HiddenField();
                hfWETues = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWETues");
                CheckWeekendDay("TUE", hfWETues.Value);

                HiddenField hfWEWed = new HiddenField();
                hfWEWed = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWEWed");
                CheckWeekendDay("WED", hfWEWed.Value);

                HiddenField hfWETue = new HiddenField();
                hfWETue = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWETue");
                CheckWeekendDay("THU", hfWETue.Value);

                HiddenField hfWEFri = new HiddenField();
                hfWEFri = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWEFri");
                CheckWeekendDay("FRI", hfWEFri.Value);

                HiddenField hfWESat = new HiddenField();
                hfWESat = (HiddenField)grWeekend.SelectedRow.Cells[4].FindControl("hfWESat");
                CheckWeekendDay("SAT", hfWESat.Value);
              
                this.EntryMode(true);
                break;
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
        this.UnCheckWeekendDay();
        this.OpenRecord();
    }
}
