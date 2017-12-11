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

public partial class Payroll_Payroll_CostRecovery : System.Web.UI.Page
{
   
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    GADRecoveryManager objGADMgr = new GADRecoveryManager();
    PlanAccLineManager objAccMgr = new PlanAccLineManager();

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
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            ddlFiscalYear.SelectedValue = Session["FISCALYRID"].ToString().Trim();
            Common.FillDropDownList(objEmpMgr.SelectSupervisor(), ddlEmployee, "EMPNAME", "EMPID", true, "Select");
            //Common.FillDropDownList(objMasMgr.SelectGAD("Y"), ddlGAD, "GADTITLE", "GADCODE", true, "Select GAD");
            Common.FillDropDownList(objAccMgr.SelectAccLineData("0", "Y"), ddlAccLine, "ACCLINE", "ACCLINEID", true, "Select Acc. Line");
            //this.OpenRecord("0");
        }
    }


    protected void EntryMode(bool IsUpdate)
    {
        btnSave.Text = "Save";
            //TextBox1.Text = "100";
            //TextBox2.Text = "100";
            //TextBox3.Text = "100";
            //TextBox4.Text = "100";
            //TextBox5.Text = "100";
            //TextBox6.Text = "100";
            //TextBox7.Text = "100";
            //TextBox8.Text = "100";
            //TextBox9.Text = "100";
            //TextBox10.Text = "100";
            //TextBox11.Text = "100";
            //TextBox12.Text = "100";
            //CheckBox1.Checked = false;
            //CheckBox2.Checked = false;
            //CheckBox3.Checked = false;
            //CheckBox4.Checked = false;
            //CheckBox5.Checked = false;
            //CheckBox6.Checked = false;
            //CheckBox7.Checked = false;
            //CheckBox8.Checked = false;
            //CheckBox9.Checked = false;
            //CheckBox10.Checked = false;
            //CheckBox11.Checked = false;
            //CheckBox12.Checked = false;
       // hfIsUpdate.Value = "Y";
    }

    protected void OpenRecord()
    {
        DataTable dtCostRecData = objGADMgr.SelectCostRecoveryPlanData(ddlFiscalYear.SelectedValue.Trim(), ddlEmployee.SelectedValue.Trim());
        grSchedule.DataSource = dtCostRecData;
        grSchedule.DataBind();
        foreach (GridViewRow gRow in grSchedule.Rows)
        {
            gRow.Cells[0].Text = Convert.ToString(gRow.DataItemIndex + 1);
            if (Common.GetGridControlValue(gRow, 3,  "hfJul") == "Y")
                Common.HighLightGridControl(gRow, 3, "txtJul");
            if (Common.GetGridControlValue(gRow, 4, "hfAug") == "Y")
                Common.HighLightGridControl(gRow, 4, "txtAug");
            if (Common.GetGridControlValue(gRow, 5, "hfSep") == "Y")
                Common.HighLightGridControl(gRow, 5, "txtSep");
            if (Common.GetGridControlValue(gRow, 6, "hfOct") == "Y")
                Common.HighLightGridControl(gRow, 6, "txtOct");
            if (Common.GetGridControlValue(gRow, 7, "hfNov") == "Y")
                Common.HighLightGridControl(gRow, 7, "txtNov");
            if (Common.GetGridControlValue(gRow, 8, "hfDec") == "Y")
                Common.HighLightGridControl(gRow, 8, "txtDec");
            if (Common.GetGridControlValue(gRow, 9, "hfJan") == "Y")
                Common.HighLightGridControl(gRow, 9, "txtJan");
            if (Common.GetGridControlValue(gRow, 10, "hfFeb") == "Y")
                Common.HighLightGridControl(gRow, 10, "txtFeb");
            if (Common.GetGridControlValue(gRow, 11, "hfMar") == "Y")
                Common.HighLightGridControl(gRow, 11, "txtMar");
            if (Common.GetGridControlValue(gRow, 12, "hfApr") == "Y")
                Common.HighLightGridControl(gRow, 12, "txtApr");
            if (Common.GetGridControlValue(gRow, 13, "hfMay") == "Y")
                Common.HighLightGridControl(gRow, 13, "txtMay");
            if (Common.GetGridControlValue(gRow, 14, "hfJun") == "Y")
                Common.HighLightGridControl(gRow, 14, "txtJun");

            gRow.Cells[15].Text = Common.FindInDdlTextData(ddlAccLine, grSchedule.DataKeys[gRow.DataItemIndex].Values[3].ToString().Trim());
        }

        this.ShowFooterSummary();
    }

    protected void ShowFooterSummary()
    {
        // Show Footer Summary
        if (grSchedule.Rows.Count > 0)
        {
            for (int col = 3; col < grSchedule.Columns.Count - 2; col++)
            {
                grSchedule.FooterRow.Cells[col].Text = this.GetColSummary(col).ToString();
            }
        }
    }

    protected decimal GetColSummary(int col)
    {
        decimal decTotal = 0;
        for (int row = 0; row < grSchedule.Rows.Count; row++)
        {
            switch (col)
            {
                case 3:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtJul"));
                    break;
                case 4:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtAug"));
                    break;
                case 5:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtSep"));
                    break;
                case 6:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtOct"));
                    break;
                case 7:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtNov"));
                    break;
                case 8:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtDec"));
                    break;
                case 9:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtJan"));
                    break;
                case 10:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtFeb"));
                    break;
                case 11:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtMar"));
                    break;
                case 12:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtApr"));
                    break;
                case 13:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtMay"));
                    break;
                case 14:
                    decTotal = decTotal + Convert.ToDecimal(Common.GetGridControlValue(grSchedule.Rows[row], col, "txtJun"));
                    break;
            }

        }
        return decTotal;
    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue.Trim() == "-1")
        {
            lblMsg.Text = "Please select an employee";
            return;
        }
        if (grSchedule.Rows.Count == 0)
        {
            lblMsg.Text = "No record to update";
            return;
        }
        else
        {
            for (int col = 3; col < grSchedule.Columns.Count - 2; col++)
            {
              decimal decFooter = 0;
              decFooter=this.GetColSummary(col);
              if (decFooter != 100)
                {
                    lblMsg2.Text = "Percent for a month must be <=100. Please check the highlighted column.";
                    grSchedule.FooterRow.Cells[col].Text = decFooter.ToString();
                    grSchedule.Columns[col].FooterStyle.BackColor = System.Drawing.Color.Yellow;
                    return;
                }
                
                else
                {
                    grSchedule.Columns[col].FooterStyle.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
       

        objGADMgr.UpdateCostRecoveryPlanning(grSchedule, ddlFiscalYear.SelectedValue.Trim(), ddlEmployee.SelectedValue.Trim(),
            Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text="Record updated successfully";
        this.OpenRecord();
        lblMsg2.Text = "";

    }

    
    

   
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //this.SaveData("Y");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        lblMsg2.Text = "";
        string strDefPercent = "0";
        if (ddlEmployee.SelectedValue.Trim() == "-1")
        {
            lblMsg2.Text = "Please select an employee";
            return;
        }
        if (ddlGAD.SelectedValue == "-1")
        {
            lblMsg2.Text = "Please select a GAD";
            return;
        }
        // Duplicate GAD
        foreach (GridViewRow gRow in grSchedule.Rows)
        {
            if (gRow.Cells[1].Text.Trim() == ddlGAD.SelectedValue.Trim())
            {
                lblMsg2.Text = "Duplicate GAD selected. Record cannot be add";
                return;
            }
        }

        if (string.IsNullOrEmpty(txtPercent.Text.Trim()) == true)
            strDefPercent = "0";
        else
            strDefPercent = txtPercent.Text.Trim();
        // Adding the value to database
        //objGADMgr.AddGADRecoverPlanning(ddlFiscalYear.SelectedValue.Trim(), ddlEmployee.SelectedValue.Trim(), ddlGAD.SelectedValue.Trim(),
        //    Common.ReturnZeroForNull(TextBox1.Text.Trim()), (CheckBox1.Checked == true ? "Y" : "N"), 
        //    Common.ReturnZeroForNull(TextBox2.Text.Trim()), (CheckBox2.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox3.Text.Trim()), (CheckBox3.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox4.Text.Trim()), (CheckBox4.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox5.Text.Trim()), (CheckBox5.Checked == true ? "Y" : "N"), 
        //    Common.ReturnZeroForNull(TextBox6.Text.Trim()), (CheckBox6.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox7.Text.Trim()), (CheckBox7.Checked == true ? "Y" : "N"), 
        //    Common.ReturnZeroForNull(TextBox8.Text.Trim()), (CheckBox8.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox9.Text.Trim()), (CheckBox9.Checked == true ? "Y" : "N"), 
        //    Common.ReturnZeroForNull(TextBox10.Text.Trim()), (CheckBox10.Checked == true ? "Y" : "N"),
        //    Common.ReturnZeroForNull(TextBox11.Text.Trim()), (CheckBox11.Checked == true ? "Y" : "N"), 
        //    Common.ReturnZeroForNull(TextBox12.Text.Trim()), (CheckBox12.Checked == true ? "Y" : "N"),
        //    Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "N");

        objGADMgr.AddGADRecoverPlanning(ddlFiscalYear.SelectedValue.Trim(), ddlEmployee.SelectedValue.Trim(), ddlGAD.SelectedValue.Trim(),
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            strDefPercent, "N",
            Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "N",ddlAccLine.SelectedValue.Trim());

        lblMsg.Text = "Cost recovery planning data added successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }


    protected void grSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                lblMsg2.Text = "";
                objGADMgr.DeleteCostRecoveryPlanData(grSchedule.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim());
                lblMsg.Text = "Record deleted successfully";
                this.OpenRecord();
                break;
        }


    }
    protected bool ValidatePercent(decimal[] decPercent)
    {
        foreach (decimal dec in decPercent)
        {
            if (dec > 100)
                return false;
        }
        return true;
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
    }
}
