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

public partial class Payroll_Payroll_BonusReview : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    BonusAllowanceManager objBonMgr = new BonusAllowanceManager();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

    DataTable dtEmp = new DataTable();
    DataTable dtSch = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //hfIsUpdate.Value = "Y";
            // Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "T"), ddlFiscalYearTax, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_All(objMasMgr.SelectReligionList(0), ddlReligion);
            Common.FillDropDownList_Nil(objMasMgr.SelectFestivalList(0), ddlFestival);

            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            btnDelete.Enabled = false;
            //Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, true);
            //ddlEmpType.SelectedIndex = 1; 
            // this.OpenRecord("0");
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            //hfIsUpdate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            //hfIsUpdate.Value = "N";
        }
    }

    protected void GenerateRecord()
    {
        dtEmp = objBonMgr.GetBonusAllowanceData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "14",
            ddlReligion.SelectedValue.Trim(), ddlFestival.SelectedValue.Trim());
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();

        //this.GetBonusData();
        int i = 1;
        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            i++;
        }
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();

        //this.ValidateWithBenefitsPolicy();
    }
    
    protected decimal GetProrataBfAmount(DateTime dtJoinDate, DateTime dtFestivalDate, decimal dclbfAmount)
    {
        decimal dclJobDuration = 0;
        decimal dclUnitDayAmount = 0;
        decimal dclProrataBfAmount = 0;
        TimeSpan ts = dtFestivalDate - dtJoinDate;
        dclJobDuration = ts.Days + 1;
        dclUnitDayAmount = dclbfAmount / 365;
        dclProrataBfAmount = dclUnitDayAmount * dclJobDuration;
        return dclProrataBfAmount;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count == 0)
        {
            lblMsg.Text = "No Record to Save";
            return;
        }
        //objBonMgr.InsertBonusAllowanceData(grEmployee, ddlFiscalYear.SelectedValue.Trim(), ddlReligion.SelectedValue.Trim(),
        //    ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), Common.ReturnDate(txtFestivalDate.Text.Trim()),
        //    "14", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), ddlFestival.SelectedValue.Trim(), "P",
        //    ddlFiscalYearTax.SelectedValue.Trim());
        
        lblMsg.Text = "Record Saved Successfully";
        grEmployee.DataSource = null;
        grEmployee.DataBind();
    }

   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count == 0)
        {
            lblMsg.Text = "No Record to delete";
            return;
        }
        objBonMgr.DeleteBonusAllowanceData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "14",
            ddlReligion.SelectedValue.Trim(), ddlFestival.SelectedValue.Trim());
        lblMsg.Text = "Records delete successfully";

        grEmployee.DataSource = null;
        grEmployee.DataBind();
    }

    private bool CheckField()
    {
        lblMsg.Text = "";
        //if (ddlReligion.SelectedIndex == 0)
        //{
        //    lblMsg.Text = "Please select Religion";
        //    return false;
        //}

      
        return true;
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (CheckField())
        {
            grEmployee.DataSource = null;
            grEmployee.DataBind();

            DataTable dtBonusRecord = objBonMgr.GetBonusAllowanceData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
            ddlFiscalYear.SelectedValue.Trim(), "14", ddlReligion.SelectedValue.Trim(), ddlFestival.SelectedValue.Trim());

            if (dtBonusRecord.Rows.Count > 0)
            {
                if (dtBonusRecord.Rows[0]["VSTATUS"].ToString().Trim() == "A")
                {
                    lblMsg.Text = "All or some records has been Approved for disbursement. These data cannnot be modify or delete";
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    lblMsg.Text = "All or some records for the this criteria is exist. To replace all the list record please delete the records first. "
                        + " Then regenerate the records. ";
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                }

                grEmployee.DataSource = dtBonusRecord;
                grEmployee.DataBind();

                foreach (GridViewRow gRow in grEmployee.Rows)
                {
                    gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
                }
            }
            else
            {
                this.GenerateRecord();
                btnDelete.Enabled = false;
                btnSave.Enabled = true;                
            }
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        grEmployee.DataSource = null;
        grEmployee.DataBind();
    }
    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string religion = this.ddlReligion.SelectedValue.ToString();
        if (religion != "99999")
            Common.FillDropDownList(objMasMgr.SelectRelagionFestivalList(Convert.ToInt32(religion)), ddlFestival, true);
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.GenerateRecord(); 

    }
}
