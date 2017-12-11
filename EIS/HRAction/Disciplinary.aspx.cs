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

public partial class EIS_HRAction_Disciplinary : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMst = new MasterTablesManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtDisc = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objEmpMgr.SelectNatureWiseAction("D"), ddlAction);
            //Common.FillDropDownList_Nil(objEmpMgr.SelectAction(0), ddlAction);
            Common.FillDropDownList_Nil(objMst.SelectReason(0), ddlReasonList);

            this.EntryMode(false);          
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            lblMsg.Text = "";
        }
        else
        {
            this.ClearControl();            
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));   
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
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                lblMsg.Text = "";
                foreach (DataRow dRow in dtEmpInfo.Rows)
                {
                    lblName.Text = dRow["FullName"].ToString();
                    lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                    lblSector.Text = dRow["ProjectName"].ToString().Trim();
                    lblDept.Text = dRow["DeptName"].ToString().Trim();
                }
                this.OpenRecord();
            }
        }
    }
    private void OpenRecord()
    {
        dtDisc = objEmpMgr.SelectDisciplinary(txtEmpID.Text.Trim());
        grDisciplinary.DataSource = dtDisc;
        grDisciplinary.DataBind();

        if (dtDisc.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grDisciplinary.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text)) == false)
                    gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[4].Text)) == false)
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
                              
            }
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (ddlAction.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Action From The List.";
                ddlAction.Focus();
                return false;
            }

            if (txtActionDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter action date.";
                txtActionDate.Focus();
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

    protected void SaveData()
    {
        string strEntryDate = "";
        string strActionDate = "";
        string strReviewDate = "";

        if (string.IsNullOrEmpty(txtEntryDate.Text.Trim()) == false)
            strEntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());

        if (string.IsNullOrEmpty(txtActionDate.Text.Trim()) == false)
            strActionDate = Common.ReturnDate(txtActionDate.Text.Trim());

        if (string.IsNullOrEmpty(txtReviewDate.Text.Trim()) == false)
            strReviewDate = Common.ReturnDate(txtReviewDate.Text.Trim());

        if (hfIsUpdate.Value == "Y")
            hfId.Value = hfId.Value;
        else
            hfId.Value = Common.getMaxId("EmpDisciplinaryLog", "DisciplinaryId");

        objEmpMgr.InsertDisciplinary(hfId.Value.ToString(), txtEmpID.Text.Trim(), strEntryDate, ddlAction.SelectedValue.ToString(), ddlReasonList.SelectedValue.ToString(), strActionDate, strReviewDate, chkIsReviewed.Checked == true ? "Y" : "N", 
            chkIsSuspendInc.Checked ==true ? "Y":"N",txtRemarks.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString());

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
        else
            lblMsg.Text = "Record Updated Successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }    
  
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";        
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grDisciplinary.DataSource = null;
        grDisciplinary.DataBind();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {
        ddlAction.SelectedIndex = -1;
        ddlReasonList.SelectedIndex = -1;
        txtActionDate.Text = "";
        txtReviewDate.Text = "";
        chkIsReviewed.Checked = false;
        chkIsSuspendInc.Checked = false;
        txtRemarks.Text = "";
    }

    protected void grDisciplinary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                ddlAction.SelectedValue = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                hfId.Value = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtEntryDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[1].Text.Trim());
                ddlReasonList.SelectedValue = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                txtActionDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[3].Text.Trim());
                txtReviewDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[4].Text.Trim());
                chkIsReviewed.Checked = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[5].Text.Trim())=="Y"?true:false ;
                chkIsSuspendInc.Checked = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[6].Text.Trim()) == "Y" ? true : false;
                txtRemarks.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[7].Text.Trim());
                this.EntryMode(true);
                lblMsg.Text = "";
                break;
        }
    }
    
}
