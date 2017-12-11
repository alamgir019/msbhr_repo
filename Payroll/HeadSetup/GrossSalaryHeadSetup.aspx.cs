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

public partial class Payroll_HeadSetup_GrossSalaryHeadSetup : System.Web.UI.Page
{

    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtGrossSalHead = new DataTable();
    DataTable dtSalaryHead = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtGrossSalHead.Rows.Clear();
            dtGrossSalHead.Dispose();

            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";            
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtSalaryHead = objPayrollMgr.SelectSalaryHead(0, "N");

        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);

        grGrossSalary.DataSource = dtSalaryHead;
        grGrossSalary.DataBind();
        DataRow[] foundRows;
        string strExpr = "";
        for (int i = 0; i < grGrossSalary.Rows.Count; i++)
        {
            strExpr = "SHEADID=" + grGrossSalary.DataKeys[i].Values[0].ToString().Trim() ;
            foundRows = dtGrossSalHead.Select(strExpr);
            if (foundRows.Length > 0 )
            {
                CheckBox chBox = (CheckBox)grGrossSalary.Rows[i].FindControl("chkBox");
                chBox.Checked = true;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int TotCount = 0;        
        try
        {
            foreach (GridViewRow gRow in grGrossSalary.Rows)
            {
                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                if (chBox.Checked == true)
                {
                    TotCount = TotCount+1 ;
                }
            }

            objPayrollMgr.InsertGrossSalHead(grGrossSalary, TotCount);

           
            Common.EmptyTextBoxValues(this);
            //this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
}
