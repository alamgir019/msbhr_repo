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

public partial class EIS_FileUpload : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    FileUploadManager objVarMgr = new FileUploadManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtEmp = new DataTable();
    DataTable dtSch = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Common.FillMonthList(ddlMonth);
            //Common.FillYearList(5, ddlYear);
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            //Common.FillDropDownList(objMstMgr.SelectSalaryHeadCategoryWise("V"), ddlSalHead, "HEADNAME", "SHEADID", true, "Select");
            //ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            //ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);   
            Common.FillDropDownList_Nil(objMasMgr.SelectDesignation(0), ddlDesignation);
            Common.FillDropDownList_Nil(objMasMgr.SelectDivisionddl(0), ddlOffice);
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
        //if (grPayroll.Rows.Count == 0)
        //{
        //    lblMsg.Text = "No record found.";
        //    return false;
        //}

        //string strFrom = "";
        //string strTo = "";
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
        string strID = "";
        try
        {
            objVarMgr.InsertEmpInfoFromFile(grPayroll);
            //this.EntryMode(false);
            lblMsg.Text = "Record Saved Successfully";            
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            //throw (ex);
        }
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
            string FileName = ddlDesignation.SelectedValue.Trim() + ddlOffice.SelectedValue.Trim() + Extension;
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
            this.FormatData();
            lblRecord.Text = "Total Records : " + grPayroll.Rows.Count.ToString();
        }        
    }

    private void FormatData()
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            gRow.Cells[1].Text = "iDE" + gRow.Cells[1].Text.Trim();
            gRow.Cells[3].Text = Common.FindInDdlValueData(ddlDesignation, gRow.Cells[3].Text.Trim());
            gRow.Cells[4].Text = gRow.Cells[4].Text.Trim() == "Male" ? "M" : "F";
            gRow.Cells[5].Text = Common.FindInDdlValueData(ddlOffice, gRow.Cells[5].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[6].Text) != "")
            {
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text.Trim());
            }
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
            //hfFileName.Value = FileName;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
        this.EntryMode(false);
    }
}
