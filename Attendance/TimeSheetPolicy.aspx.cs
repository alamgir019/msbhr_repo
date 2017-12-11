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

public partial class Attendance_TimeSheetPolicy : System.Web.UI.Page
{
    TimeSheetManager timeSheetMgr = new TimeSheetManager();
    DataTable dtTimeSheetPolicy = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            hfIsUpdate.Value = "N";
            this.OpenRecord();
        }
    }
    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnAdd.Text = "Update";
            hfIsUpdate.Value = "Y";
        }
        else
        {
            btnAdd.Text = "Save";
            hfIsUpdate.Value = "N";
            EntryText();
        }
    }
    protected bool ValidateAndSave()
    {
        if (txtHour.Text == "")
        {
            lblMsg.Text = "Please input Hour";
            txtHour.Focus();
            return false;
        }

        return true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }
    protected void SaveData(string IsDelete)
    {
        if ((hfIsUpdate.Value == "Y") || (IsDelete == "Y"))
            hfID.Value = hfID.Value;
        else
            hfID.Value = "";


        timeSheetMgr.Insert_TimeSheetPolicy(hfID.Value, ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), txtHour.Text.Trim(),
            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value, IsDelete);

        if ((hfIsUpdate.Value == "N") && (IsDelete == "N"))
            lblMsg.Text = "Record Saved Successfully";
        else if ((hfIsUpdate.Value == "Y") && (IsDelete == "N"))
            lblMsg.Text = "Record Updated Successfully";

        this.OpenRecord();
        this.EntryText();
        this.EntryMode(false);
    }
    protected void EntryText()
    {
        txtHour.Text = "";
    }
    private void OpenRecord()
    {
        string mon = "";
        //string year = ddlYear.SelectedValue.ToString();
        //string month = ddlMonth.SelectedValue.ToString();

        dtTimeSheetPolicy = timeSheetMgr.GET_TimeSheetPolicy("","");
        grTimeSheetPolicy.DataSource = dtTimeSheetPolicy;
        grTimeSheetPolicy.DataBind();

        foreach (GridViewRow gRow in grTimeSheetPolicy.Rows)
        {
            mon = gRow.Cells[2].Text.Trim();
            switch (mon)
            {
                case "1":
                    gRow.Cells[2].Text = "January";
                    break;

                case "2":
                    gRow.Cells[2].Text = "February";
                    break;

                case "3":
                    gRow.Cells[2].Text = "March";
                    break;

                case "4":
                    gRow.Cells[2].Text = "April";
                    break;

                case "5":
                    gRow.Cells[2].Text = "May";
                    break;

                case "6":
                    gRow.Cells[2].Text = "June";
                    break;

                case "7":
                    gRow.Cells[2].Text = "July";
                    break;

                case "8":
                    gRow.Cells[2].Text = "August";
                    break;

                case "9":
                    gRow.Cells[2].Text = "September";
                    break;

                case "10":
                    gRow.Cells[2].Text = "October";
                    break;

                case "11":
                    gRow.Cells[2].Text = "November";
                    break;

                case "12":
                    gRow.Cells[2].Text = "December";
                    break;
            }
        }
    }
    protected void grTimeSheetPolicy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("EditClick"):

                hfID.Value = grTimeSheetPolicy.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ddlYear.SelectedValue = grTimeSheetPolicy.SelectedRow.Cells[1].Text;
                ddlMonth.SelectedValue = grTimeSheetPolicy.DataKeys[_gridView.SelectedIndex].Values[1].ToString(); //grTimeSheetPolicy.SelectedRow.Cells[2].Text;
                txtHour.Text = grTimeSheetPolicy.SelectedRow.Cells[3].Text;

                this.EntryMode(true);
                break;
        }
    }
}
