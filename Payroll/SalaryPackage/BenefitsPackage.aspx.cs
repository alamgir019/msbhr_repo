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

public partial class Payroll_SalaryPackage_BenefitsPackage : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();

    Payroll_MasterMgr objSalaryHead = new Payroll_MasterMgr();

    //Payroll_BenefitPakMst objSalaryBenefitMst = new Payroll_BenefitPakMst();

    Payroll_MasterMgr objBenefitManager = new Payroll_MasterMgr();

    static DataTable dt = new DataTable();
    DataRow dr;
    dsPayroll_BenefitPackage objDS = new dsPayroll_BenefitPackage();
    DataTable dtBenefitsPack = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            this.EntryMode(false);
            Common.FillDropDownList(objSalaryHead.SelectSalaryHead(0, "Y"), ddlSalHead, true);
            

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
            Common.EmptyTextBoxValues(this);
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtPayAmount.Text = "0";
            grBenefitHeads.DataSource = null;
            grBenefitHeads.DataBind();
            this.OpenRecord();
        }
    }

    protected void OpenRecord()
    {
        grPackageList.DataSource = null;
        grPackageList.DataBind();

        dtBenefitsPack = objBenefitManager.SelectBenefitPackage(0);
        if (dtBenefitsPack.Rows.Count > 0)
        {
            grPackageList.DataSource = dtBenefitsPack;
            grPackageList.DataBind();
            //this.FormatSalaryPackageGrid();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.AddToBenfitPakDetailsDataSet();
    }



    protected void AddToBenfitPakDetailsDataSet()
    {

        if (grBenefitHeads.Rows.Count == 0)
        {
            DataRow nRow = objDS.dtBenefitPakDtls.NewRow();
            nRow["SHEADID"] = ddlSalHead.SelectedValue.ToString();
            nRow["HEADNAME"] = ddlSalHead.SelectedItem.Text.ToString();
            nRow["PACKAGEID"] = "1";
            nRow["PAYAMT"] = txtPayAmount.Text.Trim();
            nRow["ISINPERCENT"] = chkPercent.Checked == true ? "Y" : "N";
            nRow["PERCENTSALHEAD"] = chkPercent.Checked == true ? ddlPercentSalHead.SelectedValue.ToString() : "";
            nRow["PAYMENTTYPE"] = ddlPaymentType.SelectedValue.ToString();
            nRow["CALRULES"] = ddlCalRules.SelectedValue.ToString();
            objDS.dtBenefitPakDtls.Rows.Add(nRow);
        }
        else
        {
            int i = 0;
            foreach (GridViewRow gRow in grBenefitHeads.Rows)
            {
                DataRow nRow = objDS.dtBenefitPakDtls.NewRow();
                if (grBenefitHeads.DataKeys[i].Values[1].ToString().Trim() != ddlSalHead.SelectedValue.ToString().Trim())
                {
                    nRow["SHEADID"] = grBenefitHeads.DataKeys[i].Values[1].ToString();
                    nRow["HEADNAME"] = gRow.Cells[1].Text.Trim();
                    nRow["PACKAGEID"] = grBenefitHeads.DataKeys[i].Values[0].ToString();
                    nRow["PAYAMT"] = gRow.Cells[2].Text.Trim();
                    nRow["ISINPERCENT"] = gRow.Cells[3].Text.Trim();
                    nRow["PERCENTSALHEAD"] = grBenefitHeads.DataKeys[i].Values[2].ToString();
                    nRow["PAYMENTTYPE"] = grBenefitHeads.DataKeys[i].Values[3].ToString();
                    nRow["CALRULES"] = grBenefitHeads.DataKeys[i].Values[4].ToString();
                    objDS.dtBenefitPakDtls.Rows.Add(nRow);
                }

                i++;
            }
            // New Row
            DataRow nRow2 = objDS.dtBenefitPakDtls.NewRow();
            nRow2["SHEADID"] = ddlSalHead.SelectedValue.ToString();
            nRow2["HEADNAME"] = ddlSalHead.SelectedItem.Text.ToString();
            nRow2["PACKAGEID"] = "1";
            nRow2["PAYAMT"] = txtPayAmount.Text.Trim();
            nRow2["ISINPERCENT"] = chkPercent.Checked == true ? "Y" : "N";
            nRow2["PERCENTSALHEAD"] = chkPercent.Checked == true ? ddlPercentSalHead.SelectedValue.ToString() : "";
            nRow2["PAYMENTTYPE"] = ddlPaymentType.SelectedValue.ToString();
            nRow2["CALRULES"] = ddlCalRules.SelectedValue.ToString();
            objDS.dtBenefitPakDtls.Rows.Add(nRow2);
        }
        objDS.dtBenefitPakDtls.AcceptChanges();
        grBenefitHeads.DataSource = objDS.dtBenefitPakDtls;
        grBenefitHeads.DataBind();
        foreach (GridViewRow gRow in grBenefitHeads.Rows)
        {
            gRow.Cells[5].Text = gRow.Cells[3].Text.Trim() == "0" ? "Payslip" : "Daily Payment";
            gRow.Cells[6].Text = Common.FindInDdlTextData(ddlCalRules, gRow.Cells[6].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[4].Text) != "")
                gRow.Cells[4].Text = gRow.Cells[4].Text.Trim() == "B" ? "Basic" : "Gross Salary";
        }
    }

    protected void chkPercent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPercent.Checked == true)
        {
            ddlPercentSalHead.Enabled = true;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.ValidateAndSave("N");
    }
    protected void ValidateAndSave(string IsDelete)
    {
        try
        {
            this.SaveData(IsDelete);
            this.EntryMode(false);
            //this.FillPackageInfo();
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
                lngID = objDB.GerMaxIDNumber("SalaryPakFacilityMST", "PackageID");
            else
                lngID = Convert.ToInt64(hfID.Value);

          //  string StrPaymentType = "";

            Payroll_BenefitPakMst objSalaryBenefitMst = new Payroll_BenefitPakMst(
                 lngID.ToString(),
                 txtHeadTitle.Text.Trim(),
                 txtDescription.Text.Trim(),
                 (chkInActive.Checked == true ? "N" : "Y"),
                  Session["USERID"].ToString(),
                 Common.SetDateTime(DateTime.Now.ToString()));
            objBenefitManager.InsertBenefitPakMst(objSalaryBenefitMst, hfIsUpdate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"), grBenefitHeads);
            if ((hfIsUpdate.Value == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((hfIsUpdate.Value == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if (IsDelete == "Y")
                lblMsg.Text = "Record Deleted Successfully";

            //this.EntryMode(false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void grBenefitHeads_RowCommand(object sender, GridViewCommandEventArgs e)
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
                foreach (GridViewRow gRow in grBenefitHeads.Rows)
                {
                    DataRow nRow = objDS.dtBenefitPakDtls.NewRow();
                    if (grBenefitHeads.DataKeys[i].Values[1].ToString() != grBenefitHeads.DataKeys[_gridView.SelectedIndex].Values[1].ToString())
                    {
                        nRow["SHEADID"] = grBenefitHeads.DataKeys[i].Values[1].ToString();
                        nRow["HEADNAME"] = gRow.Cells[1].Text.Trim();
                        nRow["PACKAGEID"] = grBenefitHeads.DataKeys[i].Values[0].ToString();
                        nRow["PAYAMT"] = gRow.Cells[2].Text.Trim();
                        nRow["ISINPERCENT"] = gRow.Cells[3].Text.Trim();
                        nRow["PERCENTSALHEAD"] = grBenefitHeads.DataKeys[i].Values[2].ToString();
                        nRow["PAYMENTTYPE"] = grBenefitHeads.DataKeys[i].Values[3].ToString();
                        nRow["CALRULES"] = grBenefitHeads.DataKeys[i].Values[4].ToString();
                        objDS.dtBenefitPakDtls.Rows.Add(nRow);
                    }
                    i++;
                }
                objDS.dtBenefitPakDtls.AcceptChanges();
                grBenefitHeads.DataSource = objDS.dtBenefitPakDtls;
                grBenefitHeads.DataBind();
                foreach (GridViewRow gRow in grBenefitHeads.Rows)
                {
                    gRow.Cells[5].Text = gRow.Cells[5].Text.Trim() == "0" ? "Payslip" : "Daily Payment";
                    gRow.Cells[6].Text = Common.FindInDdlTextData(ddlCalRules, gRow.Cells[6].Text.Trim());
                    if (Common.CheckNullString(gRow.Cells[4].Text) != "")
                        gRow.Cells[4].Text = gRow.Cells[4].Text.Trim() == "B" ? "Basic" : "Gross Salary";
                }
               // EntryMode(true);
               // this.GetGrossNetPayableAmnt();
                break;
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
                //Payroll_MasterMgr objSalaryManager2 = new Payroll_MasterMgr();
                hfID.Value=grPackageList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtHeadTitle.Text=grPackageList.SelectedRow.Cells[1].Text.Trim();
                txtDescription.Text=grPackageList.SelectedRow.Cells[2].Text.Trim();
                chkInActive.Checked=grPackageList.SelectedRow.Cells[3].Text.Trim()=="Y"?false:true;

                // Display the Details Value

                    DataTable dtBenfHeadDetails = objSalaryHead.SelectBenefitPakDetls(Convert.ToInt32(hfID.Value));
                    grBenefitHeads.DataSource = dtBenfHeadDetails;
                    grBenefitHeads.DataBind();
                    foreach (GridViewRow gRow in grBenefitHeads.Rows)
                    {
                        gRow.Cells[5].Text = gRow.Cells[5].Text.Trim() == "0" ? "Payslip" : "Daily Payment";
                        gRow.Cells[6].Text = Common.FindInDdlTextData(ddlCalRules, gRow.Cells[6].Text.Trim());
                        if (Common.CheckNullString(gRow.Cells[4].Text) !="")
                            gRow.Cells[4].Text = gRow.Cells[4].Text.Trim() == "B" ? "Basic" : "Gross Salary";


                    }
                 this.EntryMode(true);
                TabContainer1.ActiveTabIndex = 0;
                break;
        }
    }
}
