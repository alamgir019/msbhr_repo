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

public partial class Payroll_SalaryPackage_BaseSalaryPackage : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    dsAttendance ds = new dsAttendance();
    Payroll_MasterMgr objSalaryHeadMgr = new Payroll_MasterMgr();
    Payroll_MasterMgr objSalaryHeadMgr2 = new Payroll_MasterMgr();
    Payroll_MasterMgr objSalaryManager = new Payroll_MasterMgr();
    
    Payroll_MasterMgr objSalaryPakDets = new Payroll_MasterMgr();
    static DataTable dt = new DataTable();

    DataTable dtSalaryPackage = new DataTable();   

    DataRow dr;
    dsPayroll_SalaryPackage objDS = new dsPayroll_SalaryPackage();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            this.EntryMode(false);
            Common.FillIdNameDropDownList2(objSalaryHeadMgr.SelectSalaryHead(0, "N"), ddlOTSalHead, "HEADNAME", "SHEADID", false);
            Common.FillIdNameDropDownList2(objSalaryPakDets.SelectSalaryHead(0, "N"), ddlAttnSalHead, "HEADNAME", "SHEADID", false);
            Common.FillDropDownList(objSalaryHeadMgr.SelectSalaryHeadDeduction(0, ""), ddlDeductHead,true );
            Common.FillDropDownList(objSalaryPakDets.GenerateSalaryPackTitle(), ddlSalPackTitle, "SalPackTitle", "EMPID", true, "Select");
            this.FillPackageInfo();
            Panel1.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            chkShowAll.Checked = true;
         }
    }

    private void FillPackageInfo()
    {
        grPackageList.DataSource = null;
        grPackageList.DataBind();

        dtSalaryPackage = objSalaryManager.SelectSalaryPackage(0);
        if (dtSalaryPackage.Rows.Count > 0)
        {
            grPackageList.DataSource = dtSalaryPackage;
            grPackageList.DataBind();
            this.FormatSalaryPackageGrid();
            lblPkgCount.Text = "Total Records : " + dtSalaryPackage.Rows.Count.ToString();
        }
    }

    private void FormatSalaryPackageGrid()
    { 
        foreach (GridViewRow grow in grPackageList.Rows)
        {
            if (Common.CheckNullString(grow.Cells[1].Text) != "")
                grow.Cells[1].Text = grow.Cells[1].Text;
        }
    }
    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            btnNew.Visible = true;
        }
        else
        {
            Common.EmptyTextBoxValues(this);
            btnSave.Text = "Save";       
            hfIsUpdate.Value = "N";
            txtNetPayableAmt.Text = "0.00";
            txtNetAmountIn.Text = "0.00";
            txtOTAmtPerHour.Text = "0.00";
            txtAttndBonusAmt.Text = "0.00";
            txtDelay.Text = "0";
            txtDeduct.Text = "0";
            grSalHead.DataSource = null;
            grSalHead.DataBind();
            TabContainer1.ActiveTabIndex = 0;
            chkShowAll.Checked = true;
            btnNew.Visible = false;
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
        DataTable dtBenefitsPolicy = objOptMgr.SelectPayrollBenefitsPolicyData("0","0");

        DataTable dtBasic = objSalaryManager.GetEmpBasic(ddlSalPackTitle.SelectedValue.Trim());
      
        DataTable dtSalHead = objSalaryManager.GetSalaryHeadForPackageSetup();
        this.AddToSalaryPakDetailsDataSet(dtSalHead, dtBenefitsPolicy, dtBasic);
    }

    //private void GetGrossNetPayableAmnt()
    //{
    //    decimal dblNetPayableAmnt = 0;

    //    foreach (GridViewRow gRow in grSalHead.Rows)
    //    {
    //        dblNetPayableAmnt = dblNetPayableAmnt + Convert.ToDecimal(gRow.Cells[4].Text);
    //    }
    //    txtNetPayableAmt.Text = dblNetPayableAmnt.ToString();
    //}

    private void GetGrossNetPayableAmnt()
    {
        decimal dblNetPayableAmnt = 0;
        string sHeadAmnt;

        foreach (GridViewRow gRow in grSalHead.Rows)
        {

            TextBox txtNetPay = (TextBox)gRow.Cells[4].FindControl("txtPayAmnt");
            sHeadAmnt = txtNetPay.Text;

            dblNetPayableAmnt = dblNetPayableAmnt + Convert.ToDecimal(sHeadAmnt);
        }
        txtNetPayableAmt.Text = dblNetPayableAmnt.ToString();
    }

    private decimal GetConvertedAmnt(Decimal dblAmt, Decimal dblGrossAmt)
    {
        Decimal dblConvertAmt = (dblGrossAmt * dblAmt) / 100;
        return dblConvertAmt;
    } 
   
    private decimal TotalAmount(Decimal strtotal)
    {
        Decimal txtNetPayableAmt = strtotal;
        return txtNetPayableAmt;
    }

    protected void AddToSalaryPakDetailsDataSet(DataTable dtSalHead, DataTable dtBenefitsPolicy,DataTable dtBasic)
    {
        Decimal dclBasic = 0;
        decimal dclGross = 0;
        Decimal dclTransAmt = 0;
        
        Decimal dclAmount = 0;
        Decimal dclNetAmt = 0;
        if (string.IsNullOrEmpty(dtBasic.Rows[0]["BasicSalary"].ToString().Trim()) == false)
            dclBasic = Common.RoundDecimal(dtBasic.Rows[0]["BasicSalary"].ToString().Trim(), 2);

        if (string.IsNullOrEmpty(dtBasic.Rows[0]["GrossSalary"].ToString().Trim()) == false)
            dclGross = Common.RoundDecimal(dtBasic.Rows[0]["GrossSalary"].ToString().Trim(), 2);

        foreach (DataRow dRow in dtSalHead.Rows)
        {
            DataRow nRow = objDS.dtSalaryPakDtls.NewRow();
            nRow["SHEADID"] = dRow["SHEADID"].ToString().Trim();
            nRow["HEADNAME"] = dRow["HEADNAME"].ToString().Trim();
            nRow["ISBASICSAL"] = dRow["ISBASIC"].ToString().Trim();
            nRow["ISINPERCENT"] = dRow["DISPLAYTYPE"].ToString().Trim();
            nRow["PERCNTFIELD"] = "";
            if (dRow["ISBASIC"].ToString().Trim() == "Y")
            {
                dclNetAmt = dclNetAmt + dclBasic;
                nRow["TOTAMNT"] = dclBasic.ToString();
                nRow["PAYAMT"] = dclBasic.ToString();
            }
            else if (dRow["IsHouseRent"].ToString().Trim() == "Y")
            {
                dclAmount = this.CalculateHeadAmount(dclGross, dtBenefitsPolicy, dtBasic.Rows[0]["EMPTYPEID"].ToString().Trim(), dRow["SHEADID"].ToString().Trim());
                nRow["TOTAMNT"] = dclAmount.ToString();
                nRow["PAYAMT"] = dclAmount.ToString();
                dclNetAmt = dclNetAmt + dclAmount;
            }
            else if (dRow["IsMedical"].ToString().Trim() == "Y")
            {
                dclAmount = this.CalculateHeadAmount(dclGross, dtBenefitsPolicy, dtBasic.Rows[0]["EMPTYPEID"].ToString().Trim(), dRow["SHEADID"].ToString().Trim());
                nRow["TOTAMNT"] = dclAmount.ToString();
                nRow["PAYAMT"] = dclAmount.ToString();
                dclNetAmt = dclNetAmt + dclAmount;
            }
            else if (dRow["ISCONVEYANCE"].ToString().Trim() == "Y")
            {
                dclNetAmt = dclNetAmt + dclTransAmt;
                nRow["TOTAMNT"] = dclTransAmt.ToString();
                nRow["PAYAMT"] = dclTransAmt.ToString();
            }         
            else if (dRow["ISPF"].ToString().Trim() == "Y")
            {
                dclAmount = this.CalculateHeadAmount(dclBasic, dtBenefitsPolicy, dtBasic.Rows[0]["EMPTYPEID"].ToString().Trim(), dRow["SHEADID"].ToString().Trim());
                nRow["TOTAMNT"] = dclAmount.ToString();
                nRow["PAYAMT"] = dclAmount.ToString();
                dclNetAmt = dclNetAmt + dclAmount;
            }
            else if (dRow["SHEADID"].ToString().ToUpper().Trim() == "17")
            {
                dclAmount = this.CalculateHeadAmount(dclBasic, dtBenefitsPolicy, dtBasic.Rows[0]["EMPTYPEID"].ToString().Trim(), dRow["SHEADID"].ToString().Trim());
                nRow["TOTAMNT"] = dclAmount.ToString();
                nRow["PAYAMT"] = dclAmount.ToString();
                dclNetAmt = dclNetAmt + dclAmount;
            }
            else if (dRow["SHEADID"].ToString().ToUpper().Trim() == "7")
            {
                dclAmount = this.CalculateHeadAmount(dclBasic, dtBenefitsPolicy, dtBasic.Rows[0]["EMPTYPEID"].ToString().Trim(), dRow["SHEADID"].ToString().Trim());
                nRow["TOTAMNT"] = dclAmount.ToString();
                nRow["PAYAMT"] = dclAmount.ToString();
                dclNetAmt = dclNetAmt + dclAmount;
            }
            else
            {
                nRow["TOTAMNT"] = dRow["VALUE"].ToString().Trim();
                nRow["PAYAMT"] = dRow["VALUE"].ToString().Trim();
            }
            nRow["CONVTOTALAMNT"] = dRow["VALUE"].ToString().Trim();
            nRow["ISPFUND"] = dRow["ISPF"].ToString().Trim();
            nRow["AMTCOMPAY"] = "0";
            objDS.dtSalaryPakDtls.Rows.Add(nRow);
        }
        objDS.dtSalaryPakDtls.AcceptChanges();
        grSalHead.DataSource = objDS.dtSalaryPakDtls;
        grSalHead.DataBind();
        txtNetPayableAmt.Text = dclNetAmt.ToString();
    }

    protected Decimal CalculateHeadAmount(Decimal dclBasic, DataTable dtBfPolicy,string strEmpType,string strSHeadID)
    {
        if (dtBfPolicy.Rows.Count > 0)
        {
            DataRow[] foundRows = dtBfPolicy.Select("EMPTYPEID =" + strEmpType + " AND SHEADID=" + strSHeadID);
            Decimal dclPercent = 0;
            Decimal dclAmount = 0;
            if (foundRows.Length > 0)
            {
                if (foundRows[0]["ISPERCENT"].ToString().Trim() == "Y")
                {
                    dclPercent = Common.RoundDecimal(foundRows[0]["VALUE"].ToString().Trim(), 2);
                    dclAmount = dclBasic * dclPercent / 100;
                }
                else
                {
                    dclAmount = Common.RoundDecimal(foundRows[0]["VALUE"].ToString().Trim(), 0);
                }
            }
            return Math.Round(dclAmount, 0);
        }
        else
            return 0;
    }

  
    protected void txtDelay_TextChanged(object sender, EventArgs e)
    {
  
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.ValidateAndSave("N");
    }

    protected void ValidateAndSave(string IsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "N")
            {
                if (objSalaryManager.GetDuplicatePackage(ddlSalPackTitle.SelectedValue.ToString().Trim()).Trim() == ddlSalPackTitle.SelectedValue.ToString().Trim())
                {
                    lblMsg.Text = "Salary package already exist.";
                    return;
                }
            }
            this.SaveData(IsDelete);
            this.EntryMode(false);
            this.FillPackageInfo();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
   }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SalaryPakMst", "SalPakId");
            else
                lngID = Convert.ToInt64(hfID.Value);

            this.GetGrossNetPayableAmnt();
            Payroll_SalaryPakMst objSalaryPakMst = new Payroll_SalaryPakMst(
                 lngID.ToString(), 
                 ddlSalPackTitle.SelectedItem.Text.Trim(),
                 txtDescription.Text.Trim(), 
                 "1",
                 "N",
                 "3",
                  txtOTAmtPerHour.Text.Trim(),  
                 (chkOTPercentOf.Checked == true ? "Y" : "N"),
                 (chkOTPercentOf.Checked == true? ddlOTSalHead.SelectedValue.ToString():""),
                 // ,
                 txtAttndBonusAmt.Text.Trim(), 
                 (chkAttnBonusPercentOf.Checked == true ? "Y" : "N"),            
                 (chkAttnBonusPercentOf.Checked == true ? ddlAttnSalHead.SelectedValue.ToString() : ""),
                 txtDelay.Text,
                 txtDeduct.Text,
                 (txtDeduct.Text != "0"? ddlDeductHead.SelectedValue.ToString(): ""),
                 txtNetPayableAmt.Text.Trim(), 
                 "Y",
                 txtNetPayableAmt.Text.Trim(), 
                 (chkInActive.Checked == true ? "N" : "Y"),
                 (chkCompanyFacility.Checked == true ? "Y" : "N"),                
                 "1",
                  Session["USERID"].ToString(),
                 Common.SetDateTime(DateTime.Now.ToString()));
                 objSalaryManager.InsertSalaryPakMst(objSalaryPakMst, hfIsUpdate.Value, IsDelete, grSalHead,ddlSalPackTitle.SelectedValue.ToString());
            if ((hfIsUpdate.Value == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((hfIsUpdate.Value == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if (IsDelete == "Y")
                lblMsg.Text = "Record Deleted Successfully";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void grPackageList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("RowEdit"):
                Payroll_MasterMgr objSalaryManager2 = new Payroll_MasterMgr();
                dtSalaryPackage = objSalaryManager2.SelectSalaryPackage(Convert.ToInt32(grPackageList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim()));
                if (dtSalaryPackage.Rows.Count > 0)
                {
                    hfIsUpdate.Value = "Y";
                    hfID.Value = grPackageList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                    ddlSalPackTitle.SelectedValue = dtSalaryPackage.Rows[0]["EmpId"].ToString().Trim();
                    //txtHeadTitle.Text = dtSalaryPackage.Rows[0]["SPTitle"].ToString().Trim();
                    txtDescription.Text = dtSalaryPackage.Rows[0]["SPDesc"].ToString().Trim();
                    txtNetPayableAmt.Text = dtSalaryPackage.Rows[0]["totalSalary"].ToString().Trim();
                    txtNetAmountIn.Text = "0";

                    // Display the Details Value
                    DataTable dtSalHeadDetails = objSalaryPakDets.SelectSalaryPakDetls(Convert.ToInt32(hfID.Value));
                    grSalHead.DataSource = dtSalHeadDetails;
                    grSalHead.DataBind();
                    this.EntryMode(true);
                }
                TabContainer1.ActiveTabIndex = 0;
                break;
        }
    }

    protected void grSalHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                int i = 0;
                foreach (GridViewRow gRow in grSalHead.Rows)
                {
                    DataRow nRow = objDS.dtSalaryPakDtls.NewRow();
                    if (grSalHead.DataKeys[i].Values[0].ToString() != grSalHead.DataKeys[_gridView.SelectedIndex].Values[0].ToString())
                    {
                        nRow["SHEADID"] = grSalHead.DataKeys[i].Values[0].ToString();
                        nRow["HEADNAME"] = gRow.Cells[1].Text.Trim();
                        nRow["ISBASICSAL"] = gRow.Cells[2].Text.Trim();
                        nRow["PAYAMT"] = gRow.Cells[3].Text.Trim();
                        nRow["ISINPERCENT"] = gRow.Cells[4].Text.Trim();
                        nRow["PERCNTFIELD"] = Common.CheckNullString(gRow.Cells[5].Text.Trim());
                        nRow["TOTAMNT"] = gRow.Cells[6].Text.Trim();
                        nRow["CONVTOTALAMNT"] = gRow.Cells[7].Text.Trim();
                        nRow["ISPFUND"] = grSalHead.DataKeys[i].Values[1].ToString();
                        nRow["AMTCOMPAY"] = grSalHead.DataKeys[i].Values[2].ToString();
                        objDS.dtSalaryPakDtls.Rows.Add(nRow);
                    }
                    i++;
                }
                objDS.dtSalaryPakDtls.AcceptChanges();
                grSalHead.DataSource = objDS.dtSalaryPakDtls;
                grSalHead.DataBind();
                this.GetGrossNetPayableAmnt();
                break;
        }
    }
    protected void chkOTPercentOf_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        hfIsUpdate.Value = "";
        this.ValidateAndSave("Y");        
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        chkShowAll.Checked = false;
        string strText = txtPackageTitleSearch.Text.Trim();
        this.FillPackageInfoBySearch(strText);
    }

    protected void FillPackageInfoBySearch(string strText)
    {
        dtSalaryPackage.Rows.Clear();
        dtSalaryPackage.Dispose();

        grPackageList.DataSource = null;
        grPackageList.DataBind();

        dtSalaryPackage = objSalaryManager.GetPackageListBySearch(strText);
        if (dtSalaryPackage.Rows.Count > 0)
        {
            grPackageList.DataSource = dtSalaryPackage;
            grPackageList.DataBind();
            this.FormatSalaryPackageGrid();
            lblPkgCount.Text = "Total Records : " + dtSalaryPackage.Rows.Count.ToString();
        }
        TabContainer1.ActiveTabIndex = 1;
    }
    protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowAll.Checked == true)
        {
            this.FillPackageInfo();
            txtPackageTitleSearch.Text = "";
            TabContainer1.ActiveTabIndex = 1;
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        hfIsUpdate.Value = "N";
        this.ValidateAndSave("N");
    }
   
    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {

    }
}
