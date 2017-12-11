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

public partial class EmpEducation : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtEdu = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objEmpMgr.SelectDegree(0,"Y",""), ddlDegree);
            Common.FillDropDownList_Nil(objEmpMgr.SelectInstitute(0, "Y"), ddlInstitute);
            Common.FillDropDownList_Nil(objEmpMgr.SelectSubject(0, "Y"), ddlSubject);
            Common.FillDropDownList_Nil(objEmpMgr.SelectResult(0, "Y"), ddlResult);
            this.EntryMode(false);            
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
            this.ClearControl();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());

        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            if (GetTaskPermission() == false)
            {
                this.RefreshControl();
                lblMsg.Text = "Please mention contractual & intern staff's id.";
                btnSave.Enabled = false;
                return;
            }
            else
            {
                lblMsg.Text = "";
                btnSave.Enabled = true;
            }
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblJobTitle.Text = dRow["DesigName"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                lblProject.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtEdu = objEmpMgr.SelectEmpEducation(0, txtEmpID.Text.Trim(),"");
        grEmpEdu.DataSource = dtEdu;
        grEmpEdu.DataBind();
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
            if (ddlDegree.SelectedIndex   == -1)
            {
                lblMsg.Text = "Please delect degree.";
                ddlDegree.Focus();
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
    private void SaveData(string strIsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("EmpEducation", "EduId");

            clsEmpEducation objclsEdu = new clsEmpEducation(txtEmpID.Text.Trim(), hfId.Value.ToString(), ddlDegree.SelectedValue.ToString(), ddlInstitute.SelectedValue.ToString(),
                ddlSubject.SelectedValue.ToString(), ddlResult.SelectedValue.ToString(),txtPassingYear.Text.Trim(),txtMarks.Text.Trim(),txtDegreeTitle.Text.Trim() ,chkIsMaxDegree.Checked ==true ?"Y":"N",    
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            objEmpMgr.InsertEmpEducation(objclsEdu, hfIsUpdate.Value.ToString(), strIsDelete);

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), strIsDelete);
            this.OpenRecord();
            this.EntryMode(false);
            this.ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.RefreshControl();
    }

    protected void RefreshControl()
    {
        lblName.Text = "";
        lblJobTitle.Text = "";
        lblDept.Text = "";
        lblCompany.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grEmpEdu.DataSource = null;
        grEmpEdu.DataBind();
        ClearControl();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {        
        ddlDegree.SelectedIndex = -1;
        ddlInstitute.SelectedIndex = -1;
        ddlSubject.SelectedIndex = -1;
        ddlResult.SelectedIndex = -1;
        txtPassingYear.Text = "";
        txtDegreeTitle.Text = "";
        txtMarks.Text = "";
    }

    protected void grEmpEdu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                hfId.Value = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                ddlDegree.SelectedValue = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                ddlInstitute.SelectedValue = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                ddlSubject.SelectedValue = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                ddlResult.SelectedValue = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                
                txtMarks.Text = Common.CheckNullString(grEmpEdu.SelectedRow.Cells[6].Text.Trim());
                txtDegreeTitle.Text = Common.CheckNullString(grEmpEdu.SelectedRow.Cells[7].Text.Trim());
                txtPassingYear.Text = Common.CheckNullString(grEmpEdu.SelectedRow.Cells[8].Text.Trim());
                if (Common.CheckNullString(grEmpEdu.SelectedRow.Cells[9].Text.Trim()) == "Y")
                    chkIsMaxDegree.Checked = true;
                else
                    chkIsMaxDegree.Checked = false ;
                this.EntryMode(true);
                break;

            case ("RowDeleting"):
                hfIsUpdate.Value = "N";
                hfId.Value = grEmpEdu.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                this.EntryMode(true);   
                SaveData("Y");
                break;
        }
    }
    private bool GetTaskPermission()
    {
        string strEmpType = "";
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "305", "T102");
        if (dtConsTaskPermission.Rows.Count > 0)
        {
            strEmpType = objEmpMgr.SelectEmpWiseContractType(txtEmpID.Text.Trim());
            if (strEmpType != "")
                return true;
            else
                return false  ;
        }
        return true;
    }
}
