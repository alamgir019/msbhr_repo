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

public partial class SubDepartmentSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtSBU = new DataTable();
    DataTable dtDepartment = new DataTable();

    dsEmpConfig objEDS = new dsEmpConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtSBU.Rows.Clear();
            dtSBU.Dispose();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartment(0), ddlDept);
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
        }
    }

    private void OpenRecord()
    {
        dtDepartment = objMasMgr.SelectSubDepartment(0);
        grDepartment.DataSource = dtDepartment;
        grDepartment.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            decimal decID = 0;
            if (hfIsUpdate.Value == "N")
                decID = Convert.ToDecimal(Common.getMaxId("SubDepartmentList", "SubDeptId"));
            else
                decID = Convert.ToDecimal(hfID.Value);

            DataTable dtData = objEDS.Tables["SubDepartmentList"];
            DataRow nRow = dtData.NewRow();
            nRow["SubDeptId"] = decID;
            nRow["SubDeptName"] = txtDept.Text.Trim();
            nRow["DeptId"] = ddlDept.SelectedValue.ToString();   

            nRow["IsActive"] = chkIsActive.Checked == true ? 'N' : 'Y';
            nRow["IsDeleted"] = "N";

            if (hfIsUpdate.Value == "N")
            {
                nRow["InsertedBy"] = Session["USERID"].ToString();
                nRow["InsertedDate"] = DateTime.Now;
            }
            else
            {
                nRow["UpdatedBy"] = Session["USERID"].ToString();
                nRow["UpdatedDate"] = DateTime.Now;
            }


            dtData.Rows.Add(nRow);
            dtData.AcceptChanges();
            objMasMgr.SaveData(dtData, hfIsUpdate.Value == "N" ? "I" : "U");
            this.ClearControls();
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
      this.ClearControls();
    }

    private void ClearControls()
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
                txtDept.Text = Common.CheckNullString(grDepartment.SelectedRow.Cells[1].Text.Trim());
                ddlDept.SelectedValue = grDepartment.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                chkIsActive.Checked = Common.CheckNullString(grDepartment.SelectedRow.Cells[3].Text.Trim()) == "Y" ? false : true;
                hfID.Value = grDepartment.DataKeys[_gridView.SelectedIndex].Values[0].ToString();                
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
        }
        else
        {
            lblMsg.Text = "Select a record first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
