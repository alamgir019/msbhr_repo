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
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            //Common.FillDropDownList(objMstMgr.SelectSalaryHeadCategoryWise("V"), ddlSalHead, "HEADNAME", "SHEADID", true, "Select");
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_Nil(objMstMgr.SelectClinic(), ddlClinic);
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
                   txtActionDate.Text.Trim(), "Increment Process",txtCOLA.Text.Trim(),txtGrpPer.Text.Trim(),txtInvPer.Text.Trim());
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
        //grIncrementList.DataSource = dtEmpList;
        //grIncrementList.DataBind();
        foreach (DataRow dRow in dtEmpList.Rows)
        {
            decimal dclTotalScore=0;

            DataRow nRow = objDS.dtIncrement.NewRow();
            //DataRow[] foundRows = dtEmpList.Select("EMPID='" + gRow.Cells[1].Text.Trim() + "'");

            if (string.IsNullOrEmpty(dRow["EmpTypeId"].ToString()))
            {
                dRow["EmpTypeId"] = "-1";
            }

            DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0", dRow["EmpTypeId"].ToString().Trim());
            DataRow[] foundPlcRow;
            foundPlcRow = null;

            nRow["EmpId"] = dRow["EmpId"].ToString().Trim()  ;
            nRow["FullName"] = dRow["FullName"].ToString().Trim();
            nRow["DesigName"] = dRow["DesigName"].ToString().Trim();
            nRow["ClinicName"] = dRow["ClinicName"].ToString().Trim();
            nRow["BasicSalary"] = dRow["BasicSalary"].ToString().Trim();
            nRow["GrossSalary"] = dRow["GrossSalary"].ToString().Trim();

            nRow["COLA"] = Common.ReturnZeroForNull(txtCOLA.Text.Trim());
            nRow["GrpPer"] = Common.ReturnZeroForNull(txtGrpPer.Text.Trim());
            nRow["InvPer"] = Common.ReturnZeroForNull(txtInvPer.Text.Trim());

            dclTotalScore = Convert.ToDecimal(nRow["COLA"]) + Convert.ToDecimal(nRow["GrpPer"]) + Convert.ToDecimal(nRow["InvPer"]);

            nRow["NewBasicSalary"] = Convert.ToDecimal(nRow["BasicSalary"]) + ((Convert.ToDecimal(nRow["BasicSalary"]) * dclTotalScore) / 100);
            nRow["NewGrossSalary"] = Convert.ToDecimal(nRow["GrossSalary"]) + ((Convert.ToDecimal(nRow["GrossSalary"]) * dclTotalScore) / 100);
            
            if (dtBfPlc.Rows.Count>0)
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
        objDS.dtIncrement.AcceptChanges();
        grIncrementList.DataSource = objDS.Tables["dtIncrement"];
        grIncrementList.DataBind();
    }
}
