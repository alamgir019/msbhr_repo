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
using System.Data.SqlClient;

public partial class EmpLeaveAdjust : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    LeaveManager objLeaveMngr = new LeaveManager();
    DataTable dtLeaveApp = new DataTable();
    DataTable dtEmp = new DataTable();
    DataTable dtEmpinfo = new DataTable();
    DataTable dtLeaveDet = new DataTable(); 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        {
            dtEmpinfo = objEmpInfoMgr.SelectEmpInfoForLeave(txtEmpId.Text.Trim());

            if (dtEmpinfo.Rows.Count == 0)
            {
                lblMsg2.Text = "Employee Id is not valid.";
                txtEmpId.Focus();
                lblName.Text = ""; 
                lblDesig.Text = "";
                grLeaveStatus.DataSource = null;
                grLeaveStatus.DataBind(); 
                return;
            }
            else
            {
                lblMsg2.Text = "";
                this.FillEmpInfo(txtEmpId.Text.Trim());
            }
        }
    }

    private void FillEmpInfo(string EmpId)
    {
        string strStartDate = DateTime.Now.Year.ToString() ;
        string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate));
        string strSex = "";

        strStartDate = strStartDate + "-" + "01" + "-" + "01";
        strEndDate = strEndDate + "-" + "12" + "-" + "31"; 
        if (dtEmpinfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmpinfo.Rows)
            {
                lblName.Text = row["FullName"].ToString();                
                lblDesig.Text = row["DesigName"].ToString();
                lblEmpType.Text = row["TypeName"].ToString();
                if(string.IsNullOrEmpty(row["JoiningDate"].ToString())==false)
                    lblJoin.Text = Common.DisplayDate(row["JoiningDate"].ToString());
                strSex = row["Gender"].ToString();
                lblLvPack.Text = row["LPackName"].ToString();
            }
        }
        this.FillEmpLeaveProfile(txtEmpId.Text.Trim(), strSex);
        this.FillEmpLeaveDet(txtEmpId.Text.Trim(), strStartDate,strEndDate);
    }

    private void FillEmpLeaveProfile(string EmpId, string Sex)
    {
        grLeaveStatus.DataSource = objLeaveMgr.SelectEmpLeaveProfileEXCPL(EmpId, "0", Sex);
        grLeaveStatus.DataBind();
        this.FormatLeaveStatusGridNumber();
    }

    protected void FormatLeaveStatusGridNumber()
    {
        foreach (GridViewRow gRow in grLeaveStatus.Rows)
        {
            TextBox txtPrevYearCarry = (TextBox)gRow.Cells[1].FindControl("txtPrevYearCarry");
            txtPrevYearCarry.Enabled = true;
            TextBox txtLCarryOverd = (TextBox)gRow.Cells[3].FindControl("txtLCarryOverd");
            TextBox txtLEntitled = (TextBox)gRow.Cells[4].FindControl("txtLEntitled");
            TextBox txtLeaveEnjoyed = (TextBox)gRow.Cells[5].FindControl("txtLeaveEnjoyed");
            TextBox txtOpening = (TextBox)gRow.Cells[6].FindControl("txtOpening");
            //gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 0));//Carry Over
            //gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 0));//Entitled
            // gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 0));//Entitled
            //gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 0));//Avail(Enjoyed)
            //5 for opening leave
            //gRow.Cells[7].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 0) - Convert.ToInt16(txtOpening.Text == "" ? "0" : txtOpening.Text) + Convert.ToInt16(txtPrevYearCarry.Text == "" ? "0" : txtPrevYearCarry.Text));//Balance
            gRow.Cells[7].Text = Convert.ToString(Math.Round(Convert.ToDouble(txtLCarryOverd.Text == "" ? "0" : txtLCarryOverd.Text) +
                Convert.ToDouble(Common.ReturnZeroForNull(txtLEntitled.Text == "" ? "0" : txtLEntitled.Text)) -
                Convert.ToDouble(Common.ReturnZeroForNull(txtLeaveEnjoyed.Text == "" ? "0" : txtLeaveEnjoyed.Text)), 1) - 
                Convert.ToInt16(txtOpening.Text == "" ? "0" : txtOpening.Text) + 
                Convert.ToInt16(txtPrevYearCarry.Text == "" ? "0" : txtPrevYearCarry.Text));//Balance

            if (Convert.ToDecimal(gRow.Cells[7].Text) < 0)
            {
                gRow.Cells[7].Text = "0";
            }
            if (Common.CheckNullString(gRow.Cells[9].Text) != "")
            {
                gRow.Cells[9].Text = Common.DisplayDate(gRow.Cells[9].Text);
            }
            //Common.CheckNullString();
        }
    }   

    private void FillEmpLeaveDet(string EmpId,string LeaveStart,string LeaveEnd)
    {
        dtLeaveDet = objLeaveMgr.SelectEmpLeaveDetails(EmpId, LeaveStart, LeaveEnd);         
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.SaveData();
        this.FillEmpInfo(txtEmpId.Text.Trim());        
    }

    private void SaveData()
    {
        objLeaveMngr.UpdateEmpLeaveProfileM(grLeaveStatus, txtEmpId.Text.Trim(), Session["USERID"].ToString());
        lblMsg.Text = "Leave Adjusted Successfully";
    }

    protected void grLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblDesig.Text = "";
        lblEmpType.Text = "";
        lblJoin.Text = "";
        lblLvPack.Text = "";
        lblMsg.Text = "";
        lblMsg2.Text = "";
        lblName.Text = "";
        txtEmpId.Text = "";
        grLeaveStatus.DataSource = null;
        grLeaveStatus.DataBind();
    }
}
