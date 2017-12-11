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

public partial class EIS_HRAction_EmpSeparation : System.Web.UI.Page
{
    DataTable dtEmpInfo = new DataTable();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    DataTable dtEmpSep = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            Common.FillDropDownList_Nil(objMasMgr.SelectAction(0, "S"), ddlAction);
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            btnCalculate.Enabled = false; 
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            ddlAction.SelectedIndex = -1;
            btnCalculate.Enabled = true;
            this.txtSepDate.Text = "";
            this.txtReHire.Text = "";
            this.txtReHCause.Text = "";
            txtPWD.Text = "";

            hfSLYear.Value = "";
            hfSLMonth.Value = "";
            hfSLDay.Value = "";
        }
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDeg_Project.Text = "";
        lblOffice_Loc.Text = "";
        lblDept.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = ""; 
        lblJoiningDate.Text = "";

        grEmpSep.DataSource = null;
        grEmpSep.DataBind();
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
        }
        else
        {
            this.EntryMode(false);
        }
    }

    private void FillEmpInfo(string EmpId)
    {
        if (Session["USERID"].ToString().Trim().ToUpper() == "ADMIN")
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfoWithAwardLength(txtEmpID.Text.Trim());
        else if (Session["USERID"].ToString().Trim().ToUpper() != "ADMIN")
        {
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfoWithAwardLength(txtEmpID.Text.Trim());
        }

        if (dtEmpInfo.Rows.Count > 0)
        {
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                foreach (DataRow row in dtEmpInfo.Rows)
                {
                    lblName.Text = row["FullName"].ToString().Trim();
                    lblOffice_Loc.Text = row["DivisionName"].ToString().Trim();
                    lblDeg_Project.Text = row["DesigName"].ToString().Trim();
                    lblDept.Text = row["DeptName"].ToString().Trim();
                    lblSubDept.Text = row["SubDeptName"].ToString().Trim();
                    lblSuncode.Text = row["ClinicName"].ToString().Trim();
                    lblJoiningDate.Text = Common.DisplayDate(row["JoiningDate"].ToString().Trim());
                    hfLPakId.Value = row["LeavePakId"].ToString().Trim();
                    hfSLYear.Value = row["SLYear"].ToString().Trim();
                    hfSLMonth.Value = row["SLMonth"].ToString().Trim();
                    hfSLDay.Value = row["SLDay"].ToString().Trim();
                    hfTotDays.Value = row["TotDays"].ToString().Trim();
                }
            }
        }
        else
        {
            lblMsg.Text = "Employee code is not valid Or not under your office.";
            txtEmpID.Text = "";
            lblName.Text = "";
            lblDeg_Project.Text = "";
            lblOffice_Loc.Text = "";
            return;
        }
    }

    protected bool ValidateAndSave()
    {
        if (txtEmpID.Text == "")
        {
            lblMsg.Text = "Please select Emp code.";
            txtEmpID.Focus();
            return false;
        }

        if (ddlAction.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select an action from the list.";
            ddlAction.Focus();
            return false;
        }

        if (txtSepDate.Text == "")
        {
            lblMsg.Text = "Please select a valid separation date.";
            txtSepDate.Focus();
            return false;
        }

        return true;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }

    }
    
    public void CalculateJobLength(string strJoiningDate, string strSepDate)
    {
        // compute the difference of two dates, years, months & days
        // d1 should be the larger (newest) of the two dates
       
        string dt1 = Common.ReturnDate(strSepDate);
        string dt2 = Common.ReturnDate(strJoiningDate);
        DateTime d1 = Convert.ToDateTime(dt1);
        DateTime d2 = Convert.ToDateTime(dt2);

        int intYears = 0;
        int intMonths = 0;
        int intDays = 0;
        //d1 = d1.AddDays(-1);  
        if (d1 < d2)
        {
            DateTime d3 = d2;
            d2 = d1;
            d1 = d3;
        }

        // compute difference in total months
        intMonths = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

        // based upon the 'days', adjust months & compute actual days difference
        if (d1.Day < d2.Day)
        {
            intMonths--;
            intDays = GetDaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
        }
        else
        {
            intDays = d1.Day - d2.Day;
        }

        // compute years & actual months
        intYears = intMonths / 12;
        intMonths -= intYears * 12;

        //2nd Calculation
        int int2ndYears = 0;
        int int2ndMonths = 0;
        int int2ndDays = 0;
        if (hfSLYear.Value != "")
            int2ndYears = Convert.ToInt32(hfSLYear.Value) + intYears;
        else
            int2ndYears = intYears;
        if (hfSLMonth.Value != "")
            int2ndMonths = Convert.ToInt32(hfSLMonth.Value) + intMonths;
        else
            int2ndMonths = intMonths;
        if (hfSLDay.Value != "")
            int2ndDays = Convert.ToInt32(hfSLDay.Value) + intDays;
        else
            int2ndDays = intDays;

        int intGrDays = 0;
        int intGrMonths = 0;
        if (int2ndDays > 30)
        {
            intGrDays = int2ndDays - 30;
            int2ndDays = intGrDays;
            intMonths = intMonths + 1;
        }
        if (int2ndMonths > 11)
        {
            intGrMonths = int2ndMonths - 11;
            int2ndMonths = intGrMonths;
            int2ndYears = int2ndYears + 1;
        }

        hfSLYear.Value = int2ndYears.ToString();
        hfSLMonth.Value = int2ndMonths.ToString();
        hfSLDay.Value = int2ndDays.ToString();
    }

    private static int GetDaysInMonth(int year, int month)
    {
        // this is also available from Calendar class, but just as easy to do ourselves

        if (month < 1 || month > 12)
        {
            throw new ArgumentException("month value must be from 1-12");
        }

        // 1 2 3 4 5 6 7 8 9 10 11 12
        int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (((year / 400 * 400) == year) ||
        (((year / 4 * 4) == year) && (year % 100 != 0)))
        {
            days[2] = 29;
        }

        return days[month];
    }

    //Calculate Total Service Days
    private void CalculateTotalDays(string strJoiningDate, string strSepDate)
    {
        int iTotDays;
        iTotDays = GetDaysBetweenDates(strJoiningDate, strSepDate);
        iTotDays = iTotDays + Convert.ToInt32(hfTotDays.Value != "" ? hfTotDays.Value : "0");
        hfTotDays.Value = iTotDays.ToString();
    }

    private int GetDaysBetweenDates(string firstDate, string secondDate)
    {
        DateTime dtfirstDate = Convert.ToDateTime(Common.ReturnDate(firstDate));
        DateTime dtsecondDate = Convert.ToDateTime(Common.ReturnDate(secondDate));
        return dtsecondDate.Subtract(dtfirstDate).Days;
    }

    protected void SaveData()
    {
        string IsDelete = "N";
        if ((hfIsUpdate.Value == "Y") || (IsDelete == "Y"))
            hfSepId.Value = hfSepId.Value;
        else
            hfSepId.Value = Common.getMaxId("EmpSeparationLog", "SeparationId");

        string strSepDate = "";

        if (string.IsNullOrEmpty(txtSepDate.Text.Trim()) == false)
            strSepDate = Common.ReturnDate(txtSepDate.Text.Trim());

        ClsEmpSeparation objEmpSep = new ClsEmpSeparation(txtEmpID.Text.Trim(), hfSepId.Value.ToString(), ddlAction.SelectedValue.ToString(),
            strSepDate, txtPWD.Text.Trim(), txtReHire.Text.Trim(), txtReHCause.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        EmpInfoManager objEmp = new EmpInfoManager();
        objEmp.InsertEmpSeparation(objEmpSep, ddlAction.SelectedItem.ToString(), hfIsUpdate.Value, IsDelete,
            (hfSLYear.Value != "" ? hfSLYear.Value.ToString() : "0"),
            (hfSLMonth.Value != "" ? hfSLMonth.Value.ToString() : "0"),
            (hfSLDay.Value != "" ? hfSLDay.Value.ToString() : "0"),
            (hfTotDays.Value != "" ? hfTotDays.Value.ToString() : "0"), hfLPakId.Value.ToString());

        if ((hfIsUpdate.Value == "N") && (IsDelete == "N"))
            lblMsg.Text = "Record Saved Successfully";
        else if ((hfIsUpdate.Value == "Y") && (IsDelete == "N"))
            lblMsg.Text = "Record Updated Successfully";

        //this.OpenRecord();
        this.EntryMode(false);
        this.ClearControls();
    }

    private void OpenRecord()
    {
        grEmpSep.Dispose();
        dtEmpSep = objEmpInfoMgr.SelectEmpSeparation(0, txtEmpID.Text.Trim());
        grEmpSep.DataSource = dtEmpSep;
        grEmpSep.DataBind();
        if (grEmpSep.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grEmpSep.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[2].Text)) == false)
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            }
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.ClearControls();
        // this.OpenRecord();
    }
    protected void grEmpSep_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                hfIsUpdate.Value = "N";
                hfSepId.Value = grEmpSep.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                ddlAction.SelectedValue = grEmpSep.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                txtSepDate.Text = Common.CheckNullString(grEmpSep.SelectedRow.Cells[2].Text.Trim());
                txtPWD.Text = Common.CheckNullString(grEmpSep.SelectedRow.Cells[3].Text.Trim());
                txtReHire.Text = Common.CheckNullString(grEmpSep.SelectedRow.Cells[4].Text.Trim());
                txtReHCause.Text = Common.CheckNullString(grEmpSep.SelectedRow.Cells[5].Text.Trim());
                this.EntryMode(true);
                break;
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSepDate.Text.Trim()) == false)
        {
            txtPWD.Text = txtPWD.Text.Trim() + "(" + lblJoiningDate.Text.Trim() + "-" + txtSepDate.Text.Trim() + ")";
            this.CalculateJobLength(lblJoiningDate.Text.Trim(), txtSepDate.Text.Trim());
            this.CalculateTotalDays(lblJoiningDate.Text.Trim(), txtSepDate.Text.Trim());
        }
    }
    protected void grEmpSep_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
