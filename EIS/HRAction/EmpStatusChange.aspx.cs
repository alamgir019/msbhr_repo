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

public partial class EIS_EmpStatusChange : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    DBConnector objDB = new DBConnector();

    DataTable dtEmpSep = new DataTable();
    DataTable dtEmpInfo = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
        }
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
        }
        else
        {
            //this.EntryMode(false);
        }
    }

    private void FillEmpInfo(string EmpId)
    {
        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoWithAwardLength(txtEmpID.Text.Trim());


        if (dtEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmpInfo.Rows)
            {
                lblName.Text = row["FullName"].ToString().Trim();
                lblOffice_Loc.Text = row["DivisionName"].ToString().Trim();
                lblDeg_Project.Text = row["DesigName"].ToString().Trim();
                lblDept.Text = row["DeptName"].ToString().Trim();
                lblSubDept.Text = row["SubDeptName"].ToString().Trim();
                lblSuncode.Text = row["ClinicName"].ToString().Trim();
                lblJoiningDate.Text = Common.DisplayDate(row["JoiningDate"].ToString().Trim());
                lblSeparateDate.Text = Common.DisplayDate(row["SeparateDate"].ToString().Trim());
                if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
                {
                    lblMsg.Text = "This Staff Has Been Separated.";
                    btnSave.Enabled = true;
                }
                else
                    btnSave.Enabled = false;
            }          
        }
        else
        {
            lblMsg.Text = "Employee code is not valid Or not under your office.";
            txtEmpID.Text = "";
            lblName.Text = "";
            lblDeg_Project.Text = "";
            lblOffice_Loc.Text = "";

            return;
        }
    }

    protected void grReHired_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfReHiredId.Value = grReHired.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();               
                txtEffDate.Text = Common.CheckNullString(grReHired.SelectedRow.Cells[3].Text.Trim());                
                this.EntryMode(true);
                break;
        }
    }



    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        //this.OpenRecord();
        grReHired.DataSource = null;
        grReHired.DataBind();

        lblName.Text = "";
        lblDeg_Project.Text = "";
        lblOffice_Loc.Text = "";
        lblSeparateDate.Text = "";  
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
            //ddlAction.SelectedIndex = 0;
        }
    }

    protected bool ValidateAndSave()
    {       
        if (txtEffDate.Text == "")
        {
            lblMsg.Text = "Please input a valid date.";
            txtEffDate.Focus();
            return false;
        }
      
        return true;
    }

    private void SaveData()
    {
        long lnId = 0;
        if (hfIsUpdate.Value == "Y")
            lnId = Convert.ToInt64(hfReHiredId.Value);
        else 
            lnId = objDB.GerMaxIDNumber("EmpReHiredLog", "ReHiredId");

        string strEffDate = "";

        if (string.IsNullOrEmpty(txtEffDate.Text.Trim()) == false)
            strEffDate = Common.ReturnDate(txtEffDate.Text.Trim());               

        clsContractExt objReHired = new clsContractExt(txtEmpID.Text.Trim(), lnId.ToString(),"" , strEffDate,
            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        EmpInfoManager objEmp = new EmpInfoManager();
        objEmp.InsertReHiredLog(objReHired, "", "", hfIsUpdate.Value);            

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";

        else if (hfIsUpdate.Value == "Y")
            lblMsg.Text = "Record Updated Successfully";

        else
            lblMsg.Text = "";



        this.OpenRecord();
        this.EntryText();
        this.EntryMode(false);
    }

    protected void EntryText()
    {
        txtEmpID.Text = ""; 
        lblName.Text ="";
        lblDeg_Project.Text = "";
        lblOffice_Loc.Text = "";
        lblSeparateDate.Text = ""; 

       
        txtEffDate.Text = "";

        grReHired.DataSource = null;
        grReHired.DataBind();  
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
            EntryText();
        }
    }

    private void OpenRecord()
    {
        grReHired.Dispose();
        dtEmpSep = objEmpInfoMgr.SelectEmpReHiredlog(0, txtEmpID.Text.Trim());
        grReHired.DataSource = dtEmpSep;
        grReHired.DataBind();
        if (grReHired.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grReHired.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[0].Text)) == false)
                    gRow.Cells[0].Text = Common.DisplayDate(gRow.Cells[0].Text);
            }
        }
    }

}
