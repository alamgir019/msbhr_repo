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

public partial class Payroll_Payroll_SalaryPackageUpTool : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    FileUploadManager objVarMgr = new FileUploadManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

    DataTable dtEmp = new DataTable();
    DataTable dtSch = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
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
            //ddlSalHead.SelectedIndex = 0;
            grPayroll.DataSource = null;
            grPayroll.DataBind();
        }
    }

    protected bool ValidateandSave()
    {
        if (grPayroll.Rows.Count == 0)
        {
            lblMsg.Text = "No record found.";
            return false;
        }

        string strFrom = "";
        string strTo = "";
        //strFrom = "01/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
        //strTo = Common.GetMonthDay(Convert.ToInt16(ddlMonth.SelectedValue.Trim()), ddlYear.SelectedValue.Trim()).ToString() + "/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    // validate with From date
        //    if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(strFrom), gRow.Cells[0].Text.Trim()) == true)
        //    {
        //        lblMsg.Text = "Record cannot save. Duplicate record exist.";
        //        return false;
        //    }
        //    // validate with To date
        //    if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(strTo), gRow.Cells[0].Text.Trim()) == true)
        //    {
        //        lblMsg.Text = "Record cannot save. Duplicate record exist.";
        //        return false;
        //    }
        //}
        return true;
    }

    protected void SaveData(string IsDelete)
    {
        objVarMgr.UpdateSalaryPackage(grPayroll, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "Salary Package Upload Tool");

        lblMsg.Text = "Salary Package Updated Successfully.";     
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateandSave())
        {
            this.SaveData("N");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = "SalaryPackage" + Common.SetDate(DateTime.Now.ToString()) + Extension;
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
        this.FormateGrid();
    }

    private void FormateGrid()
    {
        int i = 1;
        decimal dclNewBasic = 0;
        decimal dclAllowance = 0;
        decimal dclPF = 0;

        DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0","0" );
        DataRow[] foundPlcRow;
        foundPlcRow = null;

        foreach (GridViewRow gRow in grPayroll.Rows)
        {           
            dclNewBasic = Convert.ToDecimal(gRow.Cells[1].Text);
            gRow.Cells[1].Text = Convert.ToString(Common.RoundDecimal(dclNewBasic.ToString(), 0));

            //Allowance
            foundPlcRow = dtBfPlc.Select("SHEADID=2");
            if (foundPlcRow.Length > 0)
            {
                dclAllowance = objEmpMgr.GetHeadAmount(Common.RoundDecimal(gRow.Cells[1].Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));
            }
            dclAllowance = (dclNewBasic * Convert.ToDecimal(dtBfPlc.Rows[0]["VALUE"]) / 100);
            gRow.Cells[2].Text = Convert.ToString(Common.RoundDecimal(dclAllowance.ToString(), 0));

            //PF Allowance 
            foundPlcRow = null;

            foundPlcRow = dtBfPlc.Select("SHEADID=13");
            if (foundPlcRow.Length > 0)
            {
                dclPF = objEmpMgr.GetHeadAmount(Common.RoundDecimal(gRow.Cells[1].Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));
            }
            gRow.Cells[3].Text = Convert.ToString(Common.RoundDecimal(dclPF.ToString(), 0));
            i++;
        }
    }
}
