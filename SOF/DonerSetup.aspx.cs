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


public partial class SOF_DonerSetup : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    SOFManager objSOFMgr = new SOFManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtSOF = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.EntryMode(false);
            //Common.FillDropDownList_Nil(objMasMgr.SelectProjectCode(0), ddlProject);
            //TabContainer1.ActiveTabIndex  = 0; 
            //this.OpenRecord();
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
            this.ClearControls();
        }
    }

    private void OpenRecord()
    {
        //dtSOF = objSOFMgr.SelectSOFList(0);
        //grList.DataSource = dtSOF;
        //grList.DataBind();

        //foreach (GridViewRow gRow in grList.Rows)
        //{
        //    gRow.Cells[1].Text = gRow.Cells[1].Text == "G" ? "Grant" : "Sponsorship";
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            //if (ddlProject.SelectedItem.Text   == "")
            //{
            //    lblMsg.Text = "Please select project.";
            //    ddlProject.Focus();
            //    return false;
            //}
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData(string strDelete)
    {
        try
        {
            //if (hfIsUpdate.Value == "N")
            //    hfID.Value = Common.getMaxId("SalarySourceList", "SalarySourceId");
            //else
            //    hfID.Value = hfID.Value.ToString();

            //clsSOFSetup objSOF = new clsSOFSetup();
            //objSOF.SalarySourceId = hfID.Value.ToString();
            //objSOF.SourceType = radGrant.Checked == true ? "G" : "S";
            //objSOF.SalSourceName = txtSalarySourceName.Text.Trim();
            //objSOF.SalSourceCode = txtSalarySourceCode.Text.Trim();
            //objSOF.ProjectCode = ddlProject.SelectedItem.Text.Trim();  
            //objSOF.SalarySource = txtSalarySource.Text.Trim();
            //objSOF.Salary = txtSalary.Text.Trim();
            //objSOF.Bonus = txtBonus.Text.Trim();
            //objSOF.PF = txtPF.Text.Trim();
            //objSOF.IT = txtIT.Text.Trim();
            //objSOF.PFLoan = txtPFLoan.Text.Trim();
            //objSOF.FringePF = txtFringePF.Text.Trim();
            //objSOF.Medical = txtMedical.Text.Trim();
            //objSOF.Gratuity = txtGratuity.Text.Trim();
            //objSOF.IsActive = chkInActive.Checked == true ? "N" : "Y";
            //objSOF.IsDeleted = "N";
            //objSOF.InsertedBy = Session["USERID"].ToString();
            //objSOF.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());

            //objSOFMgr.InsertSOFList(objSOF, hfIsUpdate.Value, strDelete);

            //lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), strDelete);
            //this.EntryMode(false);
            //this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        this.EntryMode(false);        
    }

    private void ClearControls()
    {
        //radGrant.Checked = false;
        //radSponsorship.Checked = false;
        //txtSalarySourceName.Text = "";
        //txtSalarySourceCode.Text = "";

        ////ddlProject.SelectedItem.Text = ""; 
        //txtSalarySource.Text = "";
        //txtSalary.Text = "";
        //txtBonus.Text = "";
        //txtPF.Text = "";
        //txtIT.Text = "";
        //txtPFLoan.Text = "";
        //txtFringePF.Text = "";
        //txtMedical.Text = "";
        //txtGratuity.Text = "";
    }

  

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtSalarySourceName.Text = Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim());
                txtSalarySourceCode.Text = Common.CheckNullString(grList.SelectedRow.Cells[3].Text.Trim());                
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkInActive.Checked = Common.CheckNullString(grList.SelectedRow.Cells[12].Text.Trim()) == "Y" ? false  : true ;
                this.EntryMode(true);
                break;
        }
    }
   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = "SalaryChargingUpload" + Extension;
            string FilePath = Server.MapPath(FolderPath + "/" + FileName);
            FileUpload1.SaveAs(FilePath);

            string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = "SELECT * FROM [Sheet1$]";

            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            grUpload.DataSource = ds;
            grUpload.DataBind();
        }  
    }
    protected void btnSaveBatch_Click(object sender, EventArgs e)
    {
        objSOFMgr.InsertSOFBatch(grUpload, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Salary Source Uploaded Successfully.";
    }
}
