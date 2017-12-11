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

public partial class SOF_SalaryChargingUpload : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    FileUploadManager objVarMgr = new FileUploadManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objMstMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

    SOFManager objSOFMgr = new SOFManager();

    DataTable dtEmp = new DataTable();
    DataTable dtSch = new DataTable();

    static bool duplicateRow = false;

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
            grList.DataSource = null;
            grList.DataBind();
        }
    }

    protected bool ValidateandSave()
    {
        if (grList.Rows.Count == 0)
        {
            lblMsg.Text = "No record found.";
            return false;
        }

        if (duplicateRow == true)
        {
            lblMsg.Text = "Please correct duplicate employee wise salary source id from yellow marked list.";
            return false;
        }
        return true;
    }

    protected void SaveData(string IsDelete)
    {
        string strID = "";
        try
        {
            objSOFMgr.InsertSalaryChargingUpData(grList, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "Salary Charging Upload Tool");
            this.EntryMode(false);
            lblMsg.Text = "Record Saved Successfully";
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
            this.SaveData("N");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = "SalaryChargingUpload" + Extension;
            string FilePath = Server.MapPath(FolderPath + "//" + FileName);
            FileUpload1.SaveAs(FilePath);

            string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = "SELECT * FROM [Sheet1$]";

            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            grList.DataSource = ds;
            grList.DataBind();

            lblRecord.Text = "Total Records : " + grList.Rows.Count.ToString();

            foreach (GridViewRow gRow in grList.Rows)
            {
                gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
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
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grList.Rows)
        {
            gRow.Cells[2].Text = objSOFMgr.GetProjectId(gRow.Cells[2].Text.ToString().Trim());
            if (gRow.Cells[2].Text.Trim()=="")
                gRow.BackColor = System.Drawing.Color.Red;
        }
        duplicateRow = false;
        HighlightDuplicate(grList);
    }

    public void HighlightDuplicate(GridView gridview)
    {
        string strEmpId = "";

        strEmpId = gridview.Rows[0].Cells[0].Text.ToString().Trim();

        for (int currentRow = 0; currentRow < gridview.Rows.Count - 1; currentRow++)
        {
            GridViewRow rowToCompare = gridview.Rows[currentRow];
            strEmpId = rowToCompare.Cells[0].Text.Trim();

            if (rowToCompare.Cells[0].Text.Trim() == strEmpId)
            {
                for (int otherRow = currentRow + 1; otherRow < gridview.Rows.Count; otherRow++)
                {
                    GridViewRow row = gridview.Rows[otherRow];

                    if ((rowToCompare.Cells[0].Text) != (row.Cells[0].Text))
                        goto Continue_With_Next_Id;

                    //check Duplicate Emp Id & Source Id 
                    if (((rowToCompare.Cells[0].Text) == (row.Cells[0].Text)) && ((rowToCompare.Cells[2].Text) == (row.Cells[2].Text)))
                    {
                        duplicateRow = true;
                        rowToCompare.BackColor = System.Drawing.Color.Yellow;
                        row.BackColor = System.Drawing.Color.Yellow;
                    }
                    //else if (duplicateRow)
                    //{
                    //    duplicateRow = false;
                    //}
                }
            }
        Continue_With_Next_Id:
            currentRow = currentRow;
        }
    }
}

