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

public partial class SOF_SalaryCharging : System.Web.UI.Page
{      
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    SOFManager objSOFMgr = new SOFManager();
    
    DataTable dtEmpInfo = new DataTable();
    DataTable dtSalaryCharge = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            txtEntryDate.Text = DateTime.Now.ToShortDateString();
            ChkIsActive.Checked = true;
            Common.FillDropDownList_Nil(objSOFMgr.SelectProjectList(0), ddlSalarySource);
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
            this.ClearSubControls();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDesig.Text = "";
        lblDept.Text = "";
        lblClinic.Text = "";
        lblBasic.Text = "";
        lblCompany.Text = "";
        //lblDistrict.Text = ""; 
        lblLocation.Text = "";
        //lblSubLocation.Text = ""; 
        txtEntryDate.Text = DateTime.Now.ToShortDateString();
        ChkIsActive.Checked = true;
        grList.DataSource = null;
        grList.DataBind();
    }

    private void ClearSubControls()
    {
        //txtEntryDate.Text = DateTime.Now.ToShortDateString();
        ddlSalarySource.SelectedIndex = -1;
        txtPercentage.Text = "";
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee Id .";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesig.Text = dRow["DesigName"].ToString().Trim();
                lblClinic.Text = dRow["ClinicName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblBasic.Text = dRow["BasicSalary"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                //lblDistrict.Text = dRow["PostingDistName"].ToString().Trim();
                lblLocation.Text = dRow["SalLocName"].ToString().Trim();
                //lblSubLocation.Text = dRow["PostingPlaceName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        decimal dclTotalCharge = 0;
        dtSalaryCharge = objSOFMgr.SelectEmpSalaryCharge(0, txtEmpID.Text.Trim());
        grList.DataSource = dtSalaryCharge;
        grList.DataBind();

        foreach (GridViewRow gRow in grList.Rows)
        {
            dclTotalCharge = dclTotalCharge + Convert.ToDecimal(gRow.Cells[4].Text);             
            gRow.Cells[5].Text = gRow.Cells[5].Text == "Y" ? "true" : "false";

            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[6].Text)) == false)
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);

        }
        txtTotalCharge.Text = dclTotalCharge.ToString(); 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
            this.SaveData("N");
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (ddlSalarySource.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Salary Charging From The List.";
                ddlSalarySource.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPercentage.Text )==true   )
            {
                lblMsg.Text = "Please enter salary source percentage.";
                txtPercentage.Focus();
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.ClearControls();
        this.ClearSubControls();
        lblMsg.Text = "";
    }
    
    private void SaveData(string IsDelete)
    {      
        try
        {
            string strEntryDate = ""; 

            if (hfIsUpdate.Value == "N")
                hfID.Value = Common.getMaxId("EmpSalaryCharge", "SalChargeId");
            else
                hfID.Value = hfID.Value.ToString();
            
            if (string.IsNullOrEmpty(txtEntryDate.Text.Trim()) == false)
                strEntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());

            clsEmpSalaryCharging objclsSalCharge = new clsEmpSalaryCharging();
                objclsSalCharge.SalChargeId = hfID.Value.ToString();
                objclsSalCharge.EmpId = txtEmpID.Text.Trim();
                objclsSalCharge.EntryDate= strEntryDate;
                objclsSalCharge.SalarySourceId= ddlSalarySource.SelectedValue.ToString();
                objclsSalCharge.Percentage = txtPercentage.Text;
                objclsSalCharge.IsActive = ChkIsActive.Checked == true ? "Y" : "N";
                objclsSalCharge.InsertedBy = Session["USERID"].ToString();
                objclsSalCharge.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());

                objSOFMgr.InsertEmpSalaryCharge(objclsSalCharge, hfIsUpdate.Value, IsDelete);

                lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);

            this.OpenRecord();  
            this.EntryMode(false);           
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfIsUpdate.Value = "N";
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtEntryDate.Text = Common.CheckNullString(grList.SelectedRow.Cells[6].Text);
                ddlSalarySource.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtPercentage.Text = Common.CheckNullString(grList.SelectedRow.Cells[4].Text);
                ChkIsActive.Checked = Common.CheckNullString(grList.SelectedRow.Cells[5].Text.Trim ()) == "true" ? true : false;               
                this.EntryMode(true);
                break;
            case ("RowDeleting"):
                hfIsUpdate.Value = "Y";
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.SaveData("Y");
                
                this.EntryMode(false);
                break;
        }
    }
}
