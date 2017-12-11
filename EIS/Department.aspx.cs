using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DepartmentSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtSBU = new DataTable();
    DataTable dtDepartment = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtSBU.Rows.Clear();
            dtSBU.Dispose();
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
            txtDeptCode.Enabled = true;
                    }
    }

    private void OpenRecord()
    {
        dtDepartment = objMasMgr.SelectDepartment(0);
        grDepartment.DataSource = dtDepartment;
        grDepartment.DataBind();

        foreach (GridViewRow gRow in grDepartment.Rows)
        {
            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
        }
       
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            StringBuilder strSBUID = new StringBuilder();

            MasterTablesManager MasMgr = new MasterTablesManager();

            string strValidFrom = "";
            string strValidTo = "";

            if (string.IsNullOrEmpty(txtValidFrom.Text.Trim()) == false)
                strValidFrom = Common.ReturnDate(txtValidFrom.Text.Trim());


            if (string.IsNullOrEmpty(txtValidTo.Text.Trim()) == false)
                strValidTo = Common.ReturnDate(txtValidTo.Text.Trim());

            //Filling Class Properties with values
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("DepartmentList", "DeptID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Department objDepartment = new Department(lngID.ToString(), txtDept.Text.Trim(), "", "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), 
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N/A", "N", "N", strSBUID.ToString(), (chkIsActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertDepartment(objDepartment, hfIsUpadate.Value, IsDelete, strSBUID.ToString(), (chkIsActive.Checked == true ? "N" : "Y"),
                txtDeptCode.Text.Trim(), strValidFrom, strValidTo);

            Common.GetMessage(hfIsUpadate.Value, IsDelete);
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

    protected void grSBU_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // grSBU.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grDepartment.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtDeptCode.Text = Common.CheckNullString(grDepartment.SelectedRow.Cells[1].Text.Trim());
                txtDept.Text = Common.CheckNullString(grDepartment.SelectedRow.Cells[2].Text.Trim());
                txtValidFrom.Text = Common.CheckNullString(grDepartment.SelectedRow.Cells[3].Text.Trim());
                txtValidTo.Text = Common.CheckNullString(grDepartment.SelectedRow.Cells[4].Text.Trim());
                chkIsActive.Checked = (grDepartment.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                hfID.Value = grDepartment.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtDeptCode.Enabled = false;
                this.EntryMode(true);
                //this.CheckDeptWiseSBU(Convert.ToInt32(hfID.Value));                  
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
            lblMsg.Text = "Record Deleted Successfully";
        }
        else
        {
            lblMsg.Text = "Select a record first then try to delete.";
        }

        this.EntryMode(false);
    }
}
