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

public partial class Payroll_IncrementPolicy : System.Web.UI.Page
{
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    DataTable dtIncPolicy = new DataTable();

    dsPayroll objPay = new dsPayroll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            hfIsUpdate.Value = "N";
            Common.FillMonthList(ddlStartMonth);
            Common.FillMonthList(ddlEndMonth);
            dtIncPolicy.Rows.Clear();
            dtIncPolicy.Dispose();
            
            grList.DataSource = null;
            grList.DataBind();
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
            hfIsUpdate.Value = "Y";
        }
        else
        {
            Common.EmptyTextBoxValues(this);
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";            
        }
    }

    private void OpenRecord()
    {
        dtIncPolicy = objOptMgr.SelectIncPolicy(0);
        grList.DataSource = dtIncPolicy;
        grList.DataBind();

        if (dtIncPolicy.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grList.Rows)
            {
                gRow.Cells[2].Text = Common.retMonthName(gRow.Cells[2].Text);
                gRow.Cells[3].Text = Common.retMonthName(gRow.Cells[3].Text);
            }
        }
    }

    private void SaveData(string IsDelete)
    {
        decimal decID = 0;
        try
        {            
            if (hfIsUpdate.Value == "N")
                decID = Convert.ToDecimal(Common.getMaxId("IncPolicyList", "IncPolicyId"));
            else
                decID = Convert.ToDecimal(hfID.Value);

            DataTable dtData = objPay.Tables["IncPolicyList"];
            DataRow nRow = dtData.NewRow();
            nRow["IncPolicyId"] = decID;
            nRow["IncPolicyName"] = txtIncPolicy.Text.Trim();
            nRow["StartMonth"] = ddlStartMonth.SelectedValue.ToString();
            nRow["EndMonth"] = ddlEndMonth.SelectedValue.ToString();
            nRow["IncPercent"] = txtIncPercent.Text.Trim();

            nRow["IsActive"] = chkInActive.Checked == true ? 'N' : 'Y';
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

            objOptMgr.SaveData(dtData, hfIsUpdate.Value == "N" ? "I" : "U");
            //lblMsg.Text = Common.GetMessage(hfIsUpdate.Value == "N" ? "I" : "U");
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
        
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = "";
    }

    protected void grList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grList.PageIndex = e.NewPageIndex;
        this.OpenRecord();
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

                txtIncPolicy.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim());
                ddlStartMonth.SelectedValue  =grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                ddlEndMonth.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                txtIncPercent.Text = Common.CheckNullString(grList.SelectedRow.Cells[4].Text.Trim());

                chkInActive.Checked = Common.CheckNullString(grList.SelectedRow.Cells[5].Text.Trim()) == "N" ? true : false;
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
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
            lblMsg.Text = "Select a policy first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
