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

public partial class EIS_ProjectSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtCompany = new DataTable();

    DataTable dtBenefitType = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtCompany.Rows.Clear();
            dtCompany.Dispose();
            Common.FillDropDownList_Nil(objMasMgr.SelectWeekendPolicy(0), ddlWeekend);

            grProject.DataSource = null;
            grProject.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
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
        dtBenefitType = objMasMgr.SelectProjectWsBenefit(0);
        grBenefits.DataSource = dtBenefitType;
        grBenefits.DataBind();

        dtCompany = objMasMgr.SelectProject(0);
        grProject.DataSource = dtCompany;
        grProject.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();            
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("ProjectList", "ProjectId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsProject objPrj = new clsProject(lngID.ToString(),
                                                txtProject.Text.Trim(),
                                                txtProjectCode.Text.Trim(),

                                                Common.ReturnDate(txtStartDate.Text.ToString()),
                                                Common.ReturnDate(txtEndDate.Text.ToString()),                                                

                                                ddlWeekend.SelectedItem.Value.ToString(),
                                                ddlIncrement.SelectedItem.Value.ToString(),
                                                ddlIncrMonth.SelectedItem.Value.ToString(),
                                                txtIncrementYear.Text.ToString() == "" ? "0" : txtIncrementYear.Text.ToString(),
                                                (ChkIsActive.Checked == true ? "N" : "Y"),
                                                "N",
                                                Session["USERID"].ToString(),
                                                Common.SetDateTime(DateTime.Now.ToString()));

            MasMgr.InsertUpProject(grBenefits,  objPrj, hfIsUpadate.Value, IsDelete);

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
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = "";
    }

    protected void grProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grProject.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtProject.Text = grProject.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtProjectCode.Text = grProject.DataKeys[_gridView.SelectedIndex].Values[2].ToString();

                txtStartDate.Text = Common.DisplayDate(grProject.DataKeys[_gridView.SelectedIndex].Values[3].ToString());
                txtEndDate.Text = Common.DisplayDate(grProject.DataKeys[_gridView.SelectedIndex].Values[4].ToString());

                ddlWeekend.SelectedValue = grProject.DataKeys[_gridView.SelectedIndex].Values[5].ToString();
                ddlIncrement.SelectedValue = grProject.DataKeys[_gridView.SelectedIndex].Values[6].ToString();
                ddlIncrMonth.SelectedValue = grProject.DataKeys[_gridView.SelectedIndex].Values[7].ToString();
                txtIncrementYear.Text = grProject.DataKeys[_gridView.SelectedIndex].Values[8].ToString();
                ChkIsActive.Checked = grProject.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                hfID.Value = grProject.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.CheckProjectWsBenefit(Convert.ToInt32(hfID.Value));
                this.EntryMode(true);
                break;
        }
    }

    protected void CheckProjectWsBenefit(Int32 strProjId)
    {
        string strEmpTypeId = "";
        int i = 0;
        this.UnCheckAllDesig();
        DataTable dtGradeWsDesig = objMasMgr.SelectProjectWsBenefit(strProjId);
        foreach (GridViewRow gRow in grBenefits.Rows)
        {
            strEmpTypeId = grBenefits.DataKeys[i].Values[0].ToString();
            foreach (DataRow dRow in dtGradeWsDesig.Rows)
            {
                if (string.Compare(strEmpTypeId, dRow["EmpTypeId"].ToString()) == 0)
                {
                    CheckBox chkSel = new CheckBox();
                    if (Common.CheckNullString(dRow["IsPF"].ToString())=="Y")
                    {                        
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxPF");
                        chkSel.Checked = true;                       
                    }
                    if (Common.CheckNullString(dRow["IsGratuity"].ToString()) == "Y")
                    {                       
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxGratuity");
                        chkSel.Checked = true;
                    }
                    if (Common.CheckNullString(dRow["IsEOC"].ToString()) == "Y")
                    {
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxEOC");
                        chkSel.Checked = true;
                    }
                    if (Common.CheckNullString(dRow["IsEL"].ToString()) == "Y")
                    {
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxEL");
                        chkSel.Checked = true;
                    }
                    if (Common.CheckNullString(dRow["IsInsurance"].ToString()) == "Y")
                    {
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxInsurance");
                        chkSel.Checked = true;
                    }
                    if (Common.CheckNullString(dRow["IsMedical"].ToString()) == "Y")
                    {
                        chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBoxMedical");
                        chkSel.Checked = true;
                    }
                }
            }
            i++;
        }
    }

    protected void UnCheckAllDesig()
    {
        foreach (GridViewRow gRow in grBenefits.Rows)
        {
            CheckBox chkPF = new CheckBox();
            CheckBox chkGratuity = new CheckBox();
            CheckBox chkEOC = new CheckBox();
            CheckBox chkEL = new CheckBox();
            CheckBox chkInsurance = new CheckBox();
            CheckBox chkMedical = new CheckBox();

            chkPF = (CheckBox)gRow.Cells[0].FindControl("chkBoxPF");
            chkPF.Checked = false;
            chkGratuity = (CheckBox)gRow.Cells[0].FindControl("chkBoxGratuity");
            chkGratuity.Checked = false;
            chkEOC = (CheckBox)gRow.Cells[0].FindControl("chkBoxEOC");
            chkEOC.Checked = false;
            chkEL = (CheckBox)gRow.Cells[0].FindControl("chkBoxEL");
            chkEL.Checked = false;
            chkInsurance = (CheckBox)gRow.Cells[0].FindControl("chkBoxInsurance");
            chkInsurance.Checked = false;
            chkMedical = (CheckBox)gRow.Cells[0].FindControl("chkBoxMedical");
            chkMedical.Checked = false;
            continue;
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
            lblMsg.Text = "Select a project first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
