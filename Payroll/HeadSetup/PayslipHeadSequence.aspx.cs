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

public partial class Payroll_HeadSetup_PayslipHeadSequence : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtHeadSeq = new DataTable();
    DataTable dtSalaryHead = new DataTable();
    DataTable dtGrossHead = new DataTable();
    DataTable dtOtherHead = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtHeadSeq.Rows.Clear();
            dtHeadSeq.Dispose();

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
            ddlSalaryHead.Enabled = false;
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            ddlSalaryHead.Enabled =true ;
            lblMsg.Text = "";
        }
    }

    private void OpenRecord()
    {
        dtHeadSeq = objPayrollMgr.SelectPaySlipSalHeadSeq(0, 0);
        grHeadSeq.DataSource = dtHeadSeq;
        grHeadSeq.DataBind();

        foreach (GridViewRow gRow in grHeadSeq.Rows)
        {            
            gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(gRow.Cells[1].Text), 0));
            switch (gRow.Cells[3].Text.Trim())
            {
                case "S":
                    gRow.Cells[3].Text = "Salary";
                    gRow.Cells[3].BackColor = System.Drawing.Color.FromArgb(204, 204, 204);
                    break;
                case "B":
                    gRow.Cells[3].Text = "Benefit";
                    gRow.Cells[3].BackColor = System.Drawing.Color.FromArgb(153, 255, 102);
                    break;
                case "D":
                    gRow.Cells[3].Text = "Deduction";
                    gRow.Cells[3].BackColor = System.Drawing.Color.FromArgb(255, 153, 051);
                    break;
            }
        }

        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "");
        this.Bind_DdlSalaryHead();
    }

    private void Bind_DdlSalaryHead()
    {
        Common.FillDropDownList(dtSalaryHead, ddlSalaryHead,true );
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
            if (ddlSalaryHead.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select the salary head.";
                ddlSalaryHead.Focus();
                return false;
            }
            if (hfIsUpdate.Value.ToString() == "N")
            {
                dtHeadSeq = objPayrollMgr.SelectPaySlipSeqAndHead(Convert.ToDecimal(txtSeqNo.Text.Trim()), 
                    0,"S", hfIsUpdate.Value.ToString());
                if (dtHeadSeq.Rows.Count > 0)
                {
                    lblMsg.Text = txtSeqNo.Text.Trim() + " Sequence No. has already existed in the database. Please change the sequence no.";
                    txtSeqNo.Focus();
                    return false;
                }
                dtHeadSeq = objPayrollMgr.SelectPaySlipSeqAndHead(0,
                    Convert.ToInt32(ddlSalaryHead.SelectedValue.ToString()), "H", hfIsUpdate.Value.ToString());
                if (dtHeadSeq.Rows.Count > 0)
                {
                    lblMsg.Text = ddlSalaryHead.SelectedItem  + " head has already existed in the database. Please change the salary head.";
                    ddlSalaryHead.Focus();
                    return false;
                }  
            }
            else
            {
                dtHeadSeq = objPayrollMgr.SelectPaySlipSeqAndHead(Convert.ToDecimal(txtSeqNo.Text.Trim()), 
                    Convert.ToInt32(ddlSalaryHead.SelectedValue.ToString()),"S", hfIsUpdate.Value.ToString());
                if (dtHeadSeq.Rows.Count > 0)
                {
                    lblMsg.Text = txtSeqNo.Text.Trim() + " Sequence No. has already existed in the database. Please change the sequence no.";
                    txtSeqNo.Focus();
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }    

    private void SaveData(string IsDelete)
    {       
        try
        {
            objPayrollMgr.InsertPaySlipSalHeadSeq(txtSeqNo.Text.Trim(),ddlSalaryHead.SelectedValue.ToString(),       hfIsUpdate.Value, IsDelete,ddlDisplayType.SelectedValue.ToString());

            if (hfIsUpdate.Value == "N")
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }
    protected void grHeadSeq_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtSeqNo.Text = grHeadSeq.SelectedRow.Cells[1].Text;
                ddlSalaryHead.SelectedValue = grHeadSeq.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                this.GetDisplayType();
                this.EntryMode(true);
                break;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSeqNo.Text.Trim()   ) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Salary Head first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void GetDisplayType()
    {
        dtGrossHead = objPayrollMgr.SelectGrossSalHeadWithNature(Convert.ToInt32(ddlSalaryHead.SelectedValue));
        if (dtGrossHead.Rows.Count > 0)
        {
            if (dtGrossHead.Rows[0]["HEADNATURE"].ToString().Trim() == "1")
                ddlDisplayType.SelectedValue = "S";
            else
                ddlDisplayType.SelectedValue = "D";
        }
        else
        {
            dtOtherHead = objPayrollMgr.SelectSalaryHeadwithoutGross(Convert.ToInt32(ddlSalaryHead.SelectedValue));
            if (dtOtherHead.Rows.Count > 0)
            {
                 if (dtOtherHead.Rows[0]["HEADNATURE"].ToString().Trim() == "1")
                     ddlDisplayType.SelectedValue = "B";
                 else
                     ddlDisplayType.SelectedValue = "D";
            }
           
        }
        dtGrossHead.Rows.Clear();
        dtGrossHead.Dispose();
        dtOtherHead.Rows.Clear();
        dtOtherHead.Dispose();
    }

    protected void ddlSalaryHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDisplayType();
    }
}
