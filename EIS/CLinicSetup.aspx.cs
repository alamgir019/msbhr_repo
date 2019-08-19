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

//using Microsoft.Office.Interop.Word;

public partial class EIS_CLinicSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    DataTable dtCompany = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtCompany.Rows.Clear();
            dtCompany.Dispose();
            //Common.FillDropDownList_Nil(objMasMgr.GetClinicTypeList(), ddlClinicType);
            grClinic.DataSource = null;
            grClinic.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            Common.FillDropDownList(objEmpInfoMgr.SelectBankList(), ddlBank, "BankName", "BankCode", true, "Nil");
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
        }
    }

    private void OpenRecord()
    {
        dtCompany = objMasMgr.SelectClinic("A");
        grClinic.DataSource = dtCompany;
        grClinic.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            //Filling Class Properties with values
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("ClinicList", "ClinicId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Clinic objCnt = new Clinic(lngID.ToString(), txtClinic.Text.Trim(), txtShortName.Text.Trim(), ddlClinicType.SelectedValue.ToString(),txtSunCode.Text.Trim(),
                txtBankAccNo.Text.Trim(),ddlBank.SelectedValue.ToString().Trim (),ddlBranch.SelectedValue.ToString().Trim(),       
                (ChkIsActive.Checked == true ? "N" : "Y"), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(),
                Common.SetDateTime(DateTime.Now.ToString()), "MSB");

            MasMgr.InsertUpClinicList(objCnt,    hfIsUpadate.Value, IsDelete);

            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {


        //Application application = new Application();
        //Document document = application.Documents.Open("E:\\Notes.docx");

        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = "";
    }

    protected void grClinic_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grClinic.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grClinic_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtClinic.Text = Common.CheckNullString(grClinic.SelectedRow.Cells[1].Text.Trim());
                txtShortName.Text = Common.CheckNullString(grClinic.SelectedRow.Cells[2].Text.Trim ());

                ddlClinicType.SelectedValue = grClinic.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                txtSunCode.Text = Common.CheckNullString(grClinic.SelectedRow.Cells[3].Text.Trim());
                txtBankAccNo.Text = Common.CheckNullString(grClinic.SelectedRow.Cells[4].Text.Trim());
                if ((string.IsNullOrEmpty(grClinic.DataKeys[_gridView.SelectedIndex].Values[3].ToString()) == false) &&
                        (Common.CheckNullString(grClinic.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim() ) != "-1"))
                    ddlBank.SelectedValue = grClinic.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                Common.FillDropDownList(objEmpInfoMgr.SelectBranchList(ddlBank.SelectedValue.ToString()), ddlBranch, "BranchName", "Routingno", true, "Nil");
                ddlBranch.SelectedValue = Common.RetrieveddL(ddlBranch, grClinic.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim() , "-1");
                
                ChkIsActive.Checked = Common.CheckNullString(grClinic.SelectedRow.Cells[7].Text.Trim() )== "N" ? true : false;
                hfID.Value = grClinic.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.EntryMode(true);
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a clinic first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void grClinic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList(objEmpInfoMgr.SelectBranchList(ddlBank.SelectedValue.ToString()), ddlBranch, "BranchName", "Routingno", true, "Nil");
    }
}
