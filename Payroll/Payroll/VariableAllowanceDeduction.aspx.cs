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

public partial class Payroll_Payroll_VariableAllowanceDeduction : System.Web.UI.Page
{
    Payroll_VariableAllowanceManager objVarMgr = new Payroll_VariableAllowanceManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    BonusAllowanceManager objBonMgr = new BonusAllowanceManager();
    DataTable dtEmp = new DataTable();
    DataTable dtSch = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);

            Common.FillDropDownList(objEmpMgr.SelectSupervisor(), ddlEmployee, "EMPNAME", "EMPID", true, "Select");
            Common.FillDropDownList(objEmpMgr.SelectSupervisor(), ddlEmpList, "EMPNAME", "EMPID", true, "Select");
            Common.FillDropDownList(objMstMgr.SelectSalaryHeadCategoryWise("V"),ddlSalHead,"HEADNAME","SHEADID",true,"Select");
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

            this.OpenRecord("0");
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
            grEmpList.DataSource = null;
            grEmpList.DataBind();
            grSchedule.DataSource = null;
            grSchedule.DataBind();
            ddlEmployee.SelectedIndex = 0;
            txtPayAmt.Text = "";
        }
    }

    protected void OpenRecord(string strVID)
    {
        int i = 0;
        //string strEmpIDs="";
        //if (grEmpList.Rows.Count > 0)
        //{
        //    for (i = 0; i < grEmpList.Rows.Count; i++)
        //    {
        //        if (strEmpIDs == "")
        //            strEmpIDs = "'" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'";
        //        else
        //            strEmpIDs = strEmpIDs + ",'" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'";
        //    }
        //}
        //else
        //    strEmpIDs = "'" + ddlEmployee.SelectedValue.ToString().Trim() + "'";

        grVariableList.DataSource = objVarMgr.SelectVariableList("0", "", ddlEmpList.SelectedValue.ToString().Trim(),ddlMonth.SelectedValue.ToString(),ddlYear.SelectedValue.ToString()          );
        grVariableList.DataBind();

        DateTime dtCurrDate = DateTime.Today;
        foreach (GridViewRow gRow in grVariableList.Rows)
        {
            gRow.Cells[1].Text = Convert.ToString(gRow.DataItemIndex + 1);
            if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[9].Text) != "")
                gRow.Cells[9].Text = Common.DisplayDate(gRow.Cells[9].Text.Trim());

            DateTime dtValidFrom = Convert.ToDateTime(Common.ReturnDate(gRow.Cells[8].Text));
            DateTime dtValidTo = Convert.ToDateTime(Common.ReturnDate(gRow.Cells[9].Text));
            //if (dtCurrDate < dtValidFrom)
            //{
            //     gRow.Cells[10].Text = "N";
            //     gRow.Cells[0].ForeColor = System.Drawing.Color.Red;
            //     gRow.Cells[8].ForeColor = System.Drawing.Color.Red;
            //     gRow.Cells[9].ForeColor = System.Drawing.Color.Red;
            //     gRow.Cells[10].ForeColor = System.Drawing.Color.Red;

            //}

            if (dtCurrDate.Month > dtValidTo.Month && dtCurrDate.Year == dtValidTo.Year)
            {

                gRow.Cells[10].Text = "N";
                gRow.Enabled = false;
                if (ddlSelect.SelectedValue.Trim() == "Y")
                    gRow.Visible = false;

            }
            else if (dtCurrDate.Year > dtValidTo.Year)
            {
                gRow.Cells[10].Text = "N";
                gRow.Enabled = false;
                if (ddlSelect.SelectedValue.Trim() == "Y")
                    gRow.Visible = false;
            }
            else
            {
                if (ddlSelect.SelectedValue.Trim() == "Y")
                    gRow.Visible = true;
                else
                    gRow.Visible = false;
            }

            //else
            //    gRow.Cells[10].Text = (gRow.Cells[10].Text.Trim()) == "Y" ? "Y" : "N";            
        }
    }

    protected void InitializeEmpDataTable()
    {
        dtEmp=new DataTable();
        dtEmp.Columns.Add("EMPID");
        dtEmp.Columns.Add("FULLNAME");
    }

    protected void InitializeSchDataTable()
    {
        dtSch = new DataTable();
        dtSch.Columns.Add("VID");
        dtSch.Columns.Add("VMONTH");
        dtSch.Columns.Add("VYEAR");
        dtSch.Columns.Add("VDAYS");
        dtSch.Columns.Add("PAYAMNT");
    }

    protected void ValidateandSave(string IsDelete)
    {
        if (hfIsUpdate.Value == "N")
        {
            // validate with From date
            if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(txtFrom.Text.Trim()),ddlEmployee.SelectedValue.ToString().Trim()) == true)
            {
                lblMsg.Text = "Record cannot save. Duplicate record exist.";
                return;
            }
            // validate with To date
            if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(txtTo.Text.Trim()),ddlEmployee.SelectedValue.ToString().Trim()) == true)
            {
                lblMsg.Text = "Record cannot save. Duplicate record exist.";
                return;
            }
        }       


        //DataTable dtpaySlip = objOptMgr.SelectpaySlipOption("OC03");
        //DateTime dtFrom = Convert.ToDateTime(Common.ReturnDate(txtFrom.Text.Trim()));
        //DateTime dtTo = Convert.ToDateTime(Common.ReturnDate(txtTo.Text.Trim()));
        //if (dtFrom < Convert.ToDateTime(dtpaySlip.Rows[0]["PayrollValidFrom"].ToString()))
        //{
        //    lblMsg.Text = "Record cannot save. Effective from date cannot be less than the Payroll Validity Period.";
        //    return;
        //}
        //if (dtTo > Convert.ToDateTime(dtpaySlip.Rows[0]["PayrollValidTo"].ToString()))
        //{
        //    lblMsg.Text = "Record cannot save. Effective to date cannot be greater than the Payroll Validity Period.";
        //    return;
        //}

        if (grSchedule.Rows.Count == 0)
        {
            lblMsg.Text = "Record cannot save. Please click the generate button.";
            return;
        }
        if (grEmpList.Rows.Count == 0)
        {
            lblMsg.Text = "Record cannot save. Please click the add button.";
            return;
        }


        this.SaveData(IsDelete);
    }

    protected void SaveData(string IsDelete)
    {
        string strID = "";
        try
        {
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "Y")
                strID = hfID.Value;
            if (IsDelete == "N")
            {
                objVarMgr.InsertData(grEmpList, strID, ddlEmployee.SelectedValue.ToString().Trim(), ddlSalHead.SelectedValue.ToString(), txtPayAmt.Text.Trim(),
                   txtFrom.Text.Trim(), txtTo.Text.Trim(), "Y", hfIsUpdate.Value.ToString(), Session["USERID"].ToString(),
                   Common.SetDateTime(DateTime.Now.ToString()), txtRemarks.Text.Trim(), grSchedule);

                //if (ddlSalHead.SelectedValue.ToString() == "19")
                //{
                //    objBonMgr.InsertBonusAllowanceData(grEmpList, "36", "",
                //        "7", "2015", Common.SetDateTime(DateTime.Now.ToString()),
                //        "19", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "");
                //}

                if (hfIsUpdate.Value == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";
            }
            else
            {
                objVarMgr.DeleteData(strID);
                lblMsg.Text = "Record Deleted Successfully";
            }
            //Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord("0");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckFields())
        {
            this.ValidateandSave("N");
        }
    }

    private bool CheckFields()
    {
        if (grEmpList.Rows.Count == 0)
        {
            lblMsg.Text = "Please select Employee first.";
            return false;
        }
        if (ddlSalHead.SelectedValue == "-1")
        {
            lblMsg.Text = "Please select Salary Head.";
            return false;
        }
        if (string.IsNullOrEmpty(txtPayAmt.Text.Trim()) == true)
        {
            lblMsg.Text = "Please enter Amount.";
            return false;
        }
        if (string.IsNullOrEmpty(txtFrom.Text.Trim()) == true)
        {
            lblMsg.Text = "Please select From Date.";
            return false;
        }
        if (string.IsNullOrEmpty(txtTo.Text.Trim()) == true)
        {
            lblMsg.Text = "Please select To Date.";
            return false;
        }        
        return true;
    }

    protected void AddToEmpList()
    {       
        this.InitializeEmpDataTable();
        string strEmpID = ddlEmployee.SelectedValue.ToString().Trim();
        bool EmpDataExist = false;
        foreach (GridViewRow gRow in grEmpList.Rows)
        {
                DataRow nRow = dtEmp.NewRow();
                nRow["EMPID"]= grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();
                nRow["FULLNAME"]=gRow.Cells[2].Text.Trim();
                dtEmp.Rows.Add(nRow);
                dtEmp.AcceptChanges();
                if (strEmpID == grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim())
                    EmpDataExist = true;
        }
        if (EmpDataExist == false)
        {
            DataRow nRow2 = dtEmp.NewRow();
            nRow2["EMPID"] = ddlEmployee.SelectedValue.ToString().Trim();
            nRow2["FULLNAME"] = ddlEmployee.SelectedItem.Text.Trim();
            dtEmp.Rows.Add(nRow2);
            dtEmp.AcceptChanges();
        }        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlEmployee.SelectedValue != "-1")
        {
            this.AddToEmpList();
            int i = 1;
            if (dtEmp.Rows.Count > 0)
            {
                grEmpList.DataSource = dtEmp;
                grEmpList.DataBind();
                foreach (GridViewRow gRow in grEmpList.Rows)
                {
                    gRow.Cells[1].Text = i.ToString();
                    i++;
                    foreach (GridViewRow gVarRow in grVariableList.Rows)
                    {
                        if (grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim() == gVarRow.Cells[2].Text.Trim())
                        {
                            gVarRow.Cells[2].BackColor = System.Drawing.Color.Orange;
                            gVarRow.Cells[3].BackColor = System.Drawing.Color.Orange;
                        }
                    }
                }
            }
        }
        else
        {
            lblMsg.Text = "Please select Employee first.";
        }
    }

    protected void grVariableList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                ddlEmployee.SelectedValue = grVariableList.SelectedRow.Cells[2].Text.Trim();
                ddlSalHead.SelectedValue = grVariableList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                txtPayAmt.Text= Common.RoundDecimal(grVariableList.SelectedRow.Cells[7].Text.Trim(),2).ToString();
                txtFrom.Text=grVariableList.SelectedRow.Cells[8].Text.Trim();
                txtTo.Text = grVariableList.SelectedRow.Cells[9].Text.Trim();
                txtRemarks.Text = grVariableList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                hfID.Value = grVariableList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
               // chkMakeInactive.Checked = grVariableList.SelectedRow.Cells[10].Text.Trim() == "Y" ? false : true;
                
                this.InitializeEmpDataTable();
                DataRow nRow = dtEmp.NewRow();
                nRow["EMPID"] = grVariableList.SelectedRow.Cells[2].Text.Trim();
                nRow["FULLNAME"] = grVariableList.SelectedRow.Cells[3].Text.Trim();
                dtEmp.Rows.Add(nRow);
                dtEmp.AcceptChanges();
                grEmpList.DataSource = dtEmp;
                grEmpList.DataBind();
                int i = 1;
                foreach (GridViewRow gRow in grEmpList.Rows)
                {
                    gRow.Cells[1].Text = i.ToString();
                    i++;
                }
                // Get The Details Record
                DataTable dtSchTmp = objVarMgr.SelectDetailsData(hfID.Value.ToString());
                grSchedule.DataSource = dtSchTmp;
                grSchedule.DataBind();
                this.SetGrScheduleSerial();
                this.EntryMode(true);
                break;
        }
    }

    protected void grEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
               
                this.InitializeEmpDataTable();
                foreach (GridViewRow gRow in grEmpList.Rows)
                {
                    if (grEmpList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() != grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim())
                    {
                        DataRow nRow = dtEmp.NewRow();
                        nRow["EMPID"] = grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();
                        nRow["FULLNAME"] = gRow.Cells[2].Text.Trim();
                        dtEmp.Rows.Add(nRow);
                        dtEmp.AcceptChanges();
                    }
                    else
                    {
                        foreach (GridViewRow gVarRow in grVariableList.Rows)
                        {
                            if (grEmpList.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim() == gVarRow.Cells[2].Text.Trim())
                            {
                                 gVarRow.Cells[2].BackColor = System.Drawing.Color.Empty;
                                 gVarRow.Cells[3].BackColor = System.Drawing.Color.Empty;
                            }
                        }
                    }
                }
                grEmpList.DataSource = dtEmp;
                grEmpList.DataBind();
                int i = 1;
                foreach (GridViewRow gRow in grEmpList.Rows)
                {
                    gRow.Cells[1].Text = i.ToString();
                    i++;
                }
                //this.OpenRecord("0");
               
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.SaveData("Y");
    }

    protected void ddlSalHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gVarRow in grVariableList.Rows)
        {
            if (ddlSalHead.SelectedValue.ToString().Trim() == grVariableList.DataKeys[gVarRow.DataItemIndex].Values[1].ToString().Trim())
            {
                gVarRow.Cells[6].BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                gVarRow.Cells[6].BackColor = System.Drawing.Color.Empty;
            }
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (CheckFields())
        {
            this.InitializeSchDataTable();
            TimeSpan ts;
            int inMonthDays = 0;
            int inFromMonth = 0;
            int inFromYear = 0;
            DateTime dtFrom = Convert.ToDateTime(Common.ReturnDate(txtFrom.Text.Trim()));
            DateTime dtTo = Convert.ToDateTime(Common.ReturnDate(txtTo.Text.Trim()));
            DateTime dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));
            inFromMonth = dtFrom.Month;
            if (inFromMonth == 12)
            {
                inFromMonth = 1;
                inFromYear = dtFrom.Year + 1;
            }
            else
            {
                inFromMonth = dtFrom.Month + 1;
                inFromYear = dtFrom.Year;
            }
            DateTime dtToStartDate = Convert.ToDateTime(inFromYear.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

            if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
            {
                ts = dtTo - dtFrom;
                inMonthDays = Common.GetMonthDay(dtFrom);
                if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
                {
                    this.AddScheduleData(dtFrom.Month.ToString(), txtPayAmt.Text.Trim(), dtFrom.Year.ToString(), Convert.ToString(ts.Days + 1));
                }
                else
                {
                    this.AddScheduleData(dtFrom.Month.ToString(), this.GetPayAmnt(inMonthDays, ts.Days + 1), dtFrom.Year.ToString(), Convert.ToString(ts.Days + 1));
                }
            }

            else if (dtFrom < dtTo)
            {
                ts = dtFromMonthEndDate - dtFrom;
                inMonthDays = Common.GetMonthDay(dtFrom);
                if ((dtFrom.Day == 1) && dtFromMonthEndDate.Day == inMonthDays)
                {
                    this.AddScheduleData(dtFrom.Month.ToString(), txtPayAmt.Text.Trim(), dtFrom.Year.ToString(), inMonthDays.ToString());
                }
                else
                {
                    this.AddScheduleData(dtFrom.Month.ToString(), this.GetPayAmnt(inMonthDays, ts.Days + 1), dtFrom.Year.ToString(), Convert.ToString(ts.Days + 1));
                }


                while (dtToStartDate <= dtTo)
                {
                    inMonthDays = Common.GetMonthDay(dtToStartDate);
                    if ((dtToStartDate.Month == dtTo.Month) && (dtToStartDate.Year == dtTo.Year))
                    {
                        ts = dtTo - dtToStartDate;
                        if ((dtToStartDate.Day == 1) && dtTo.Day == inMonthDays)
                        {
                            this.AddScheduleData(dtToStartDate.Month.ToString(), txtPayAmt.Text.Trim(), dtToStartDate.Year.ToString(), inMonthDays.ToString());
                        }
                        else
                        {
                            this.AddScheduleData(dtToStartDate.Month.ToString(), this.GetPayAmnt(inMonthDays, ts.Days + 1), dtToStartDate.Year.ToString(), Convert.ToString(ts.Days + 1));
                        }
                    }
                    else
                    {
                        this.AddScheduleData(dtToStartDate.Month.ToString(), txtPayAmt.Text.Trim(), dtToStartDate.Year.ToString(), inMonthDays.ToString());
                    }
                    dtToStartDate = dtToStartDate.AddMonths(1);
                }

            }
            grSchedule.DataSource = dtSch;
            grSchedule.DataBind();
            this.SetGrScheduleSerial();
        }
    }

    protected string GetPayAmnt(int inMonthDays,int inDaysDur)
    {
        decimal decMonthlyAmount = Convert.ToDecimal(txtPayAmt.Text.Trim());
        decimal decUnitDayAmnt = decMonthlyAmount / inMonthDays;
        decimal decPayAmnt = decUnitDayAmnt * inDaysDur;
        return Convert.ToString(Math.Round(decPayAmnt));
    }

    protected void AddScheduleData(string strMonth,string  strPayAmnt,string strYear,string strDaysDur)
    {
        DataRow nRow = dtSch.NewRow();
        nRow["VID"] = "1";
        nRow["VMONTH"] = strMonth;
        nRow["VYEAR"] = strYear;
        nRow["VDAYS"] = strDaysDur;
        nRow["PAYAMNT"] = strPayAmnt;
        dtSch.Rows.Add(nRow);
        dtSch.AcceptChanges();
    }

    protected void SetGrScheduleSerial()
    {
        int i = 1;
        foreach (GridViewRow gRow in grSchedule.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            gRow.Cells[1].Text = Common.GetMonthNameShort(gRow.Cells[1].Text.Trim());
            TextBox txtAmt = (TextBox)gRow.Cells[4].FindControl("txtPayAmnt");
            txtAmt.Text = Common.RoundDecimal(txtAmt.Text.Trim(), 2).ToString();
            i++;
        }
    }

    protected void btnSynchronize_Click(object sender, EventArgs e)
    {
        string strComandRows = objVarMgr.SynchronizeSalaryHead(grVariableList, Session["USERID"].ToString(),
                   Common.SetDateTime(DateTime.Now.ToString()));
        if (strComandRows != "0")
        {
            lblMsg.Text = strComandRows + " Employees Variable Items Synchronization Completed Successfully.";
        }
        else
        {
            lblMsg.Text = "Variable Items Synchronization is Already Up-to-date";
        }
    }

    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.OpenRecord("0");
    }
}
