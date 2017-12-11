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

public partial class Leave_LeaveSummaryRpt : System.Web.UI.Page
{
    EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strDivId = "";
        string strDeptId = "";
        string strParams = Request.QueryString["params"];
        string[] strVal = new string[6];
        if (string.IsNullOrEmpty(strParams) == false)
        {
            char[] splitter ={ ',' };
            strVal = Common.str_split(strParams, splitter);
        }
        lblFrom.Text = Common.DisplayDate(Common.SetDate(DateTime.Now.ToString()));

        strDivId = strVal[1] == "99999" ? "0" : strVal[1];
        strDeptId = strVal[2] == "99999" ? "0" : strVal[2];

        // Split by '/' and then generate LTypeList for Select Query
        string strLType = "";
        string[] strLTypeArr = strVal[4].Split('/');
        foreach (string str in strLTypeArr)
        {
            if (strLType == "")
                strLType = str;
            else
                strLType = strLType + "," + str;
        }

        if (strVal.Length == 6)
        {
            DataTable dtLvProfile = objLevProMgr.SelectEmpLeaveSummaryRpt(strVal[3], strDivId, strDeptId, strVal[5]);
            grLeaveMaster.DataSource = dtLvProfile;
            grLeaveMaster.DataBind();

            int i = 1;
            string strEmpId = "";
            Decimal dclCountALBal = 0;
            Decimal dclCountCLBal = 0;
            Decimal dclCountSLBal = 0;
            // FORMAT THE DATE FIELD
            dsEmployeeLeave objDS = new dsEmployeeLeave();
            foreach (DataRow dRow in dtLvProfile.Rows)
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

            grLeaveMaster.DataSource = objDS.dtEmpLeave;
            grLeaveMaster.DataBind();

            foreach (GridViewRow gRow in grLeaveMaster.Rows)
            {
                GridView grLvDet = new GridView();
                grLvDet = (GridView)gRow.Cells[4].FindControl("grLeaveDetails");

                DataTable dtEmpLeaveDet = objLevProMgr.SelectEmpWiseLeaveStats(gRow.Cells[1].Text.Trim(), strLType);
                grLvDet.DataSource = dtEmpLeaveDet;
                grLvDet.DataBind();

                gRow.Cells[0].Text = i.ToString();
                i++;
                int j = 0;
                foreach (GridViewRow gChRow in grLvDet.Rows)
                {
                    dclCountALBal = CountLeaveBalance("LTypeId= " + grLvDet.DataKeys[j].Values[0].ToString().Trim(), dtEmpLeaveDet);
                    gChRow.Cells[4].Text = Decimal.Round(dclCountALBal, 1).ToString();
                    j++;
                }

                dtEmpLeaveDet.Rows.Clear();
                dtEmpLeaveDet.Dispose();
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
