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
using System.Data.OleDb;
using System.Text;
using System.Net;
using System.IO;

public partial class Payroll_Payroll_IncrementUpload : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    FileUploadManager objVarMgr = new FileUploadManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
   
    dsPayroll_Increment objDS = new dsPayroll_Increment();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            //Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            //Common.FillDropDownList(objMstMgr.SelectSalaryHeadCategoryWise("V"), ddlSalHead, "HEADNAME", "SHEADID", true, "Select");
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_Nil(objMstMgr.SelectClinic(), ddlClinic);
        }
        ScriptManager _ScriptMan = ScriptManager.GetCurrent(this);
        _ScriptMan.AsyncPostBackTimeout = 1200;
        _ScriptMan.RegisterPostBackControl(this.btnExport);
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
            lblMsg.Text = "";
            lblRecord.Text = "";
          
            grPayroll.DataSource = null;
            grPayroll.DataBind();
        }
    }

    protected bool ValidateandSave()
    {
        if (TabContainer1.ActiveTabIndex == 0)
        {
            if (grPayroll.Rows.Count == 0)
            {
                lblMsg.Text = "No record found.";
                return false;
            }

            foreach (GridViewRow gRow in grPayroll.Rows)
            {
                // validate with From date
                if (objVarMgr.IsDuplicateIncDate(gRow.Cells[1].Text.Trim(), Common.ReturnDate(gRow.Cells[8].Text.Trim())) == true)
                {
                    lblMsg.Text = "Record cannot save because of duplicate record exist. Please remove duplicate record.";
                    gRow.BackColor = System.Drawing.Color.Yellow;
                    return false;
                }
            }
            return true;
        }
        else
        {
            if (grIncrementList.Rows.Count == 0)
            {
                lblMsg.Text = "No record found.";
                return false;
            }

            foreach (GridViewRow gRow in grIncrementList.Rows)
            {
                // validate with From date
                if (objVarMgr.IsDuplicateIncDate(gRow.Cells[1].Text.Trim(), Common.ReturnDate(txtActionDate.Text.Trim())) == true)
                {
                    lblMsg.Text = "Record cannot save because of duplicate record exist. Please remove duplicate record.";
                    gRow.BackColor = System.Drawing.Color.Yellow;
                    return false;
                }
            }
            return true;
        }
    }  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateandSave())
        {
            this.SaveData("N");
        }
    }

    protected void SaveData(string IsDelete)
    {        
        try
        {
            if (TabContainer1.ActiveTabIndex ==0) 
            objVarMgr.InsertIncrementData(grPayroll,   ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "Y", "N", Session["USERID"].ToString(),
                   Common.SetDateTime(DateTime.Now.ToString()), "Increment Upload");
            else
                objVarMgr.InsertIncrementValue(grIncrementList, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "Y", "N", Session["USERID"].ToString(),
                   txtActionDate.Text.Trim(), "Increment Process");
            //else
            //    objVarMgr.InsertBonusAllowanceData(grPayroll, "36", "",
            //        ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), Common.SetDateTime(DateTime.Now.ToString()),
            //        "19", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "");

            this.EntryMode(false);
            lblMsg.Text = "Record Saved Successfully";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = ddlMonth.SelectedValue.Trim() + ddlYear.SelectedValue.Trim() + Extension;
            string FilePath = Server.MapPath(FolderPath + "/" + FileName);
            FileUpload1.SaveAs(FilePath);

            string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = "SELECT * FROM [Sheet1$]";

            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            grPayroll.DataSource = ds;
            grPayroll.DataBind();

            lblRecord.Text = "Total Records : " + grPayroll.Rows.Count.ToString();
        }        
    }

    private void UploadReqFile(decimal ReqId)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = "Req-" + ReqId.ToString() + Extension;

            string FilePath = Server.MapPath(FolderPath + "/" + FileName);

            FileUpload1.SaveAs(FilePath);            
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
        this.EntryMode(false);
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlClinic.SelectedValue == "99999")
        {
            lblMsg.Text = "Please select Cost Center";
            return;
        }
        DataTable dtEmpList = objEmpMgr.SelectEmpForIncrement(ddlClinic.SelectedValue.ToString().Trim());
        this.GenerateIncrementList(dtEmpList);
        //grIncrementList.DataSource = dtEmpList;
        //grIncrementList.DataBind();
    }
    private void GenerateIncrementList(DataTable dtEmpList)
    {
        DateTime dtCurrDate = DateTime.Now;
        decimal dclTotalScore = 0;
        decimal dclCOLA = 0, dclGpPer = 0, dclInvPer = 0;
        foreach (DataRow dRow in dtEmpList.Rows)
        {

            DataRow nRow = objDS.dtIncrement.NewRow();

            //if (string.IsNullOrEmpty(dRow["EmpTypeId"].ToString()))
            //{
            //    dRow["EmpTypeId"] = "-1";
            //}





            string strIncDate = "";
            char[] splitter = { '/' };

            if (string.IsNullOrEmpty(dRow["IncrementDate"].ToString().Trim()) == false)
                strIncDate = dRow["IncrementDate"].ToString();
            else
                strIncDate = dRow["JoiningDate"].ToString();

            string[] arinfo = Common.str_split(Common.DisplayDate(strIncDate), splitter);
            if (arinfo.Length == 3)
            {
                strIncDate = arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0];
                //arinfo = null;
            }
            //Those Inc has already done on this yr 
            if (arinfo[2].ToString() == dtCurrDate.Year.ToString())
            {
                arinfo = null;
                goto AddNextEmpId;
            }
            if (Convert.ToInt16(arinfo[2]) < Convert.ToInt16(dtCurrDate.Year))
            {
                switch (arinfo[1])
                {
                    case "10":
                    case "11":
                    case "12":
                        dclCOLA = 0;
                        dclGpPer = 0;
                        dclInvPer = 0;
                        break;
                    case "07":
                    case "08":
                    case "09":
                        dclCOLA = (Convert.ToDecimal(txtCOLA.Text) * 50) / 100;
                        dclGpPer = (Convert.ToDecimal(txtGrpPer.Text) * 50) / 100;
                        dclInvPer = (Convert.ToDecimal(txtInvPer.Text) * 50) / 100;
                        break;
                    case "04":
                    case "05":
                    case "06":
                        dclCOLA = (Convert.ToDecimal(txtCOLA.Text) * 75) / 100;
                        dclGpPer = (Convert.ToDecimal(txtGrpPer.Text) * 75) / 100;
                        dclInvPer = (Convert.ToDecimal(txtInvPer.Text) * 75) / 100;
                        break;
                    case "01":
                    case "02":
                    case "03":
                        dclCOLA = Convert.ToDecimal(txtCOLA.Text);
                        dclGpPer = Convert.ToDecimal(txtGrpPer.Text);
                        dclInvPer = Convert.ToDecimal(txtInvPer.Text);
                        break;
                }
            }

            if (string.IsNullOrEmpty(dRow["EmpTypeId"].ToString()) == false)
            {
                if (dRow["EmpTypeId"].ToString() == "1")
                {
                }
                else//For Contractual Staff
                {
                    DateTime CurrYear = Convert.ToDateTime(txtActionDate.Text);
                    DateTime JoinYear = Convert.ToDateTime(dRow["JoiningDate"].ToString().Trim());
                    if (JoinYear.AddYears(1) > CurrYear)
                    {
                        TimeSpan DateDiff = JoinYear - CurrYear;
                        string strTotDay = Common.ReturnTotalDay(DateDiff.ToString());
                        if (Convert.ToInt16(strTotDay) < 365)
                        { goto AddNextEmpId; }                       
                    }
                }

                DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0", dRow["EmpTypeId"].ToString().Trim());
                DataRow[] foundPlcRow;
                foundPlcRow = null;

                nRow["EmpId"] = dRow["EmpId"].ToString().Trim();
                nRow["FullName"] = dRow["FullName"].ToString().Trim();
                nRow["TypeName"] = dRow["TypeName"].ToString().Trim();
                nRow["JoiningDate"] = dRow["JoiningDate"].ToString().Trim();
                nRow["DesigName"] = dRow["DesigName"].ToString().Trim();
                nRow["ClinicName"] = dRow["ClinicName"].ToString().Trim();
                if (string.IsNullOrEmpty(dRow["IncrementDate"].ToString().Trim()) == false)
                    nRow["IncrementDate"] = dRow["IncrementDate"].ToString().Trim();
                else
                    nRow["IncrementDate"] = "";                        
                nRow["BasicSalary"] = dRow["BasicSalary"].ToString().Trim();
                nRow["GrossSalary"] = dRow["GrossSalary"].ToString().Trim();

                nRow["COLA"] = dclCOLA;
                nRow["GrpPer"] = dclGpPer;
                nRow["InvPer"] = dclInvPer;

                dclTotalScore = Convert.ToDecimal(nRow["COLA"]) + Convert.ToDecimal(nRow["GrpPer"]) + Convert.ToDecimal(nRow["InvPer"]);

                nRow["NewBasicSalary"] = Math.Round(Convert.ToDecimal(nRow["BasicSalary"]) + ((Convert.ToDecimal(nRow["BasicSalary"]) * dclTotalScore) / 100),0);
                nRow["NewGrossSalary"] = Math.Round(Convert.ToDecimal(nRow["GrossSalary"]) + ((Convert.ToDecimal(nRow["GrossSalary"]) * dclTotalScore) / 100),0);

                if (dtBfPlc.Rows.Count > 0)
                {
                    foundPlcRow = dtBfPlc.Select("SHEADID=2");//House Rent
                    nRow["Housing"] = objEmpMgr.GetHeadAmount(Common.RoundDecimal(nRow["NewGrossSalary"].ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));//Convert.ToDecimal(foundRows[0]["Housing"].ToString()) + Convert.ToDecimal(foundRows[0]["Housing"].ToString()) * dclTotalScore;

                    foundPlcRow = null;
                    foundPlcRow = dtBfPlc.Select("SHEADID=3");//Medical
                    nRow["Medical"] = objEmpMgr.GetHeadAmount(Common.RoundDecimal(nRow["NewGrossSalary"].ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));

                    foundPlcRow = null;
                    foundPlcRow = dtBfPlc.Select("SHEADID=8");//PF
                    if (foundPlcRow.Length > 0)
                        nRow["PF"] = objEmpMgr.GetHeadAmount(Common.RoundDecimal(nRow["NewGrossSalary"].ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));  
                }
                objDS.dtIncrement.Rows.Add(nRow);
            }
            AddNextEmpId:
            { }
        }
        objDS.dtIncrement.AcceptChanges();
        grIncrementList.DataSource = objDS.Tables["dtIncrement"];
        grIncrementList.DataBind();
        lblExistRecordCount.Text = grIncrementList.Rows.Count.ToString();
        if (grIncrementList.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grIncrementList.Rows)
            {
                gRow.Cells[4].Text = gRow.Cells[4].Text != "" ? Common.DisplayDate(gRow.Cells[4].Text) : gRow.Cells[4].Text;
                gRow.Cells[7].Text = gRow.Cells[7].Text != "" ? Common.DisplayDate(gRow.Cells[7].Text) : gRow.Cells[7].Text;
            }
        }
        else
        {
            lblMsg.Text = "Selected Cost centers Increment process for this year has already done or no staff is available.";
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=Increment-Generation.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grIncrementList.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            Response.Write(ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }
}
