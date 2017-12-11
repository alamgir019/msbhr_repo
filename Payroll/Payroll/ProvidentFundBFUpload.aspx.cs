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

public partial class Payroll_Payroll_ProvidentFundBFUpload : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    Payroll_PFManager objPFMgr = new Payroll_PFManager();
    //EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
   //Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

   //DataTable dtEmp = new DataTable();
   DataTable dtLPFFiscal = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode();
            Common.FillDropDownList(objMstMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);

            dtLPFFiscal=objMstMgr.SelectFiscalYear(0, "LPF");
            lblPFFiscalYearID.Text = dtLPFFiscal.Rows[0]["FiscalYrTitle"].ToString();
            hfId.Value = dtLPFFiscal.Rows[0]["FiscalYrId"].ToString();
        }
    }

    protected void EntryMode()
    {
        lblMsg.Text = "";
        lblRecord.Text = "";
        grPayroll.DataSource = null;
        grPayroll.DataBind();
    }

    protected bool ValidateandSave()
    {
        if (TabContainer2.ActiveTabIndex == 0)
        {
            if (grPayroll.Rows.Count == 0)
            {
                lblMsg.Text = "No record found.";
                return false;
            }
        }
        else {
            if (grPFBF.Rows.Count == 0)
            {
                lblMsg.Text = "No record found.";
                return false;
            }
        }

        //string strFrom = "";
        //string strTo = "";
        ////strFrom = "01/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
        ////strTo = Common.GetMonthDay(Convert.ToInt16(ddlMonth.SelectedValue.Trim()), ddlYear.SelectedValue.Trim()).ToString() + "/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
        ////foreach (GridViewRow gRow in grPayroll.Rows)
        ////{
        ////    // validate with From date
        ////    if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(strFrom), gRow.Cells[0].Text.Trim()) == true)
        ////    {
        ////        lblMsg.Text = "Record cannot save. Duplicate record exist.";
        ////        return false;
        ////    }
        ////    // validate with To date
        ////    if (objVarMgr.IsDuplicateData(ddlSalHead.SelectedValue.ToString().Trim(), Common.ReturnDate(strTo), gRow.Cells[0].Text.Trim()) == true)
        ////    {
        ////        lblMsg.Text = "Record cannot save. Duplicate record exist.";
        ////        return false;
        ////    }
        ////}
        return true;
    }

    protected void SaveData()
    {        
        try
        {
            if (TabContainer2.ActiveTabIndex == 0)
            {
                objPFMgr.InsertPFBalanceUploadData(grPayroll, ddlFiscalYear.SelectedValue.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
                this.EntryMode();
                lblMsg.Text = "Record Saved Successfully";
            }
            else {

                objPFMgr.InsertUploadPFBalanceData(grPFBF, hfId.Value.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
                this.EntryMode();
                lblMsg.Text = "Record Saved Successfully";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateandSave())
        {
            this.SaveData();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = ddlFiscalYear.SelectedValue.Trim() + Extension;
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

            DataTable dtPFAmt = objPFMgr.SelectPaySlipDetlsPFAmount(ddlFiscalYear.SelectedValue.ToString());

            //foreach (GridViewRow gRow in grPayroll.Rows)
            //{
            //    DataRow[] foundRow = dtPFAmt.Select("EmpId='" + gRow.Cells[0].Text.Trim() + "'");
            //    if (foundRow.Length > 0)
            //        gRow.Cells[2].Text = foundRow[0]["TotPayAmt"].ToString();
            //    else
            //        gRow.Cells[2].Text = "0";
            //}
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


    protected void BtnGenerate_Click(object sender, EventArgs e)
    {

        grPFBF.DataSource = null;
        grPFBF.DataBind();

        DataSet ds = new DataSet();
        DataTable dtPFAmt = (DataTable)objPFMgr.GeneratePFBF(hfId.Value.ToString());
        grPFBF.DataSource = dtPFAmt;
        grPFBF.DataBind();

      //  lblRecord.Text = "Total Records : " + grPayroll.Rows.Count.ToString();

       


    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode();
    }
}