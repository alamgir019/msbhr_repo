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

public partial class Leave_LeaveBalanceRpt : System.Web.UI.Page
{
    EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strDivId = "";
        string strDeptId = "";
        string strParams = Request.QueryString["params"];
        string[] strVal = new string[5];
        if (string.IsNullOrEmpty(strParams) == false)
        {
            char[] splitter ={ ',' };
            strVal = Common.str_split(strParams, splitter);
        }
        lblFrom.Text = Common.DisplayDate(Common.SetDate(DateTime.Now.ToString()));

        strDivId = strVal[1] == "99999" ? "0" : strVal[1];
        strDeptId = strVal[2] == "99999" ? "0" : strVal[2];

        if (strVal.Length == 4)
        {
            DataTable dtLvProfile = objLevProMgr.SelectEmpLeaveBalanceRpt(strVal[3], strDivId, strDeptId);
            grLeaveProfile.DataSource = dtLvProfile;
            grLeaveProfile.DataBind();

            int i = 1;
            string strEmpId = "";
            Decimal dclCountALBal = 0;
            Decimal dclCountCLBal = 0;
            Decimal dclCountSLBal = 0;
            // FORMAT THE DATE FIELD
            dsEmployeeLeave objDS = new dsEmployeeLeave();
            foreach (DataRow dRow in dtLvProfile.Rows)
            {
                if ((dRow["LAbbrName"].ToString().Trim() == "EL") || (dRow["LAbbrName"].ToString().Trim() == "CL") ||
                    (dRow["LAbbrName"].ToString().Trim() == "SL"))
                {
                    if (strEmpId != dRow["EMPID"].ToString().Trim())
                    {
                        DataRow nRow = objDS.dtEmpLeave.NewRow();
                        strEmpId = dRow["EMPID"].ToString().Trim();
                                                                       
                        nRow["EMPID"] = dRow["EMPID"].ToString().Trim();
                        nRow["FullName"] = dRow["FullName"].ToString().Trim();
                        nRow["PostingDivName"] = dRow["PostingDivName"].ToString().Trim();
                        nRow["DeptName"] = dRow["DeptName"].ToString().Trim();                     

                        objDS.dtEmpLeave.Rows.Add(nRow);
                        objDS.dtEmpLeave.AcceptChanges();                        
                    }
                }
            }

            grLeaveProfile.DataSource = objDS.dtEmpLeave;
            grLeaveProfile.DataBind();


            foreach (GridViewRow gRow in grLeaveProfile.Rows)
            {
                gRow.Cells[0].Text = i.ToString();
                dclCountALBal = CountLeaveBalance("EmpId='" + gRow.Cells[1].Text.Trim() + "' and LAbbrName='EL'", dtLvProfile);
                gRow.Cells[5].Text = Decimal.Round(dclCountALBal,1).ToString();

                dclCountCLBal = CountLeaveBalance("EmpId='" + gRow.Cells[1].Text.Trim() + "' and LAbbrName='CL'", dtLvProfile);
                gRow.Cells[6].Text = Decimal.Round(dclCountCLBal,1).ToString();

                dclCountSLBal = CountLeaveBalance("EmpId='" + gRow.Cells[1].Text.Trim() + "' and LAbbrName='SL'", dtLvProfile);
                gRow.Cells[7].Text = Decimal.Round(dclCountSLBal,1).ToString();
                i++;
                
            }
        }
    }

    private Decimal CountLeaveBalance(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        if (foundRows.Length > 0)
            return (Convert.ToDecimal(foundRows[0]["LCarryOverd"] == DBNull.Value ? "0" : foundRows[0]["LCarryOverd"])
                + Convert.ToDecimal(foundRows[0]["lvPrevYearCarry"] == DBNull.Value ? "0" : foundRows[0]["lvPrevYearCarry"])
                + Convert.ToDecimal(foundRows[0]["LEntitled"] == DBNull.Value ? "0" : foundRows[0]["LEntitled"])
                - Convert.ToDecimal(foundRows[0]["LeaveEnjoyed"] == DBNull.Value ? "0" : foundRows[0]["LeaveEnjoyed"])
                - Convert.ToDecimal(foundRows[0]["lvOpening"] == DBNull.Value ? "0" : foundRows[0]["lvOpening"])
                );
        else
            return 0;
    }
}
