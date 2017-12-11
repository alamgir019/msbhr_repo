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

public partial class Leave_EnjoyedLeaveRecordsRpt : System.Web.UI.Page
{
    EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strDivId = "";
        string strDeptId = "";
        string strEmpId = "";
        string strEmpType = "";
        Decimal dclCount = 0;
        // Decimal dclCountCLBal = 0;
        //Decimal dclCountSLBal = 0;

        string strParams = Request.QueryString["params"];
        string[] strVal = new string[9];
        if (string.IsNullOrEmpty(strParams) == false)
        {
            char[] splitter ={ ',' };
            strVal = Common.str_split(strParams, splitter);
        }
        lblFrom.Text = Common.DisplayDate(strVal[7]) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To : " + Common.DisplayDate(strVal[8]);

        strDivId = strVal[1] == "99999" ? "0" : strVal[1];
        strDeptId = strVal[2] == "99999" ? "0" : strVal[2];
        strEmpType = strVal[9] == "99999" ? "0" : strVal[9];
        hfFrom.Value = strVal[7];
        hfTo.Value = strVal[8];
        hfLType.Value = strVal[5];
        if (strDivId != "0")
        {
            lblDivision.Text = strVal[11];
        }
        else
        {
            lblDivision.Text = "";
        }
        if (strDeptId != "0")
        {
            lblDept.Text = strVal[12];
        }
        else
        {
            lblDept.Text = "";
        }
        
        if (strEmpType != "0")
        {
            lblSubTitle.Text = "(" + strVal[10] + ")";
        }
        else
        {
            lblSubTitle.Text = "";
        }

        if (strVal.Length == 13)
        {
            DataTable dtLvProfile = objLevProMgr.SelectEnjoyedLeaveRecordsRpt(strVal[3], strDivId, strDeptId, strVal[4], strVal[7], strVal[8], strVal[5], strEmpType);
            grLeaveMaster.DataSource = dtLvProfile;
            grLeaveMaster.DataBind();
            // Get Carry Over Balance from Current Profile
            DataTable dtCurProfile=objLevProMgr.SelectEnjoyedLeaveRecordsRpt_CarryOverFromCurrentProfile(strVal[3], strDivId, strDeptId, strVal[4], strVal[7], strVal[8], strVal[5], strEmpType);
            DataRow[] foundCurRows;
            // Get Carry Over Balance from Current Profile
            DataTable dtHisProfile=objLevProMgr.SelectEnjoyedLeaveRecordsRpt_CarryOverFromHistoryProfile(strVal[3], strDivId, strDeptId, strVal[4], strVal[7], strVal[8], strVal[5], strEmpType);
            DataRow[] foundHisRows;

            if (grLeaveMaster.Rows.Count > 0)
            {
                grLeaveMaster.HeaderRow.Cells[6].Text = strVal[6];
                int i = 1;
                foreach (GridViewRow gRow in grLeaveMaster.Rows)
                {
                    gRow.Cells[0].Text = i.ToString();
                    if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
                        dclCount = dclCount + Convert.ToDecimal(gRow.Cells[6].Text.Trim());

                    // Carryover From Current Profile
                    foundCurRows = dtCurProfile.Select("EMPID='" + gRow.Cells[1].Text.Trim() + "'");
                    if (foundCurRows.Length > 0)
                    {
                        gRow.Cells[5].Text = foundCurRows[0]["LCarryOverd"].ToString().Trim();
                    }
                    else
                    // Carryover From History Profile
                    {
                        foundHisRows=dtHisProfile.Select("EMPID='" + gRow.Cells[1].Text.Trim() + "'");
                        if (foundHisRows.Length > 0)
                        {
                            gRow.Cells[5].Text = foundHisRows[0]["LCarryOverd"].ToString().Trim();
                        }
                        else
                        {
                            gRow.Cells[5].Text = "";
                        }
                    }
                    

                    i++;
                }
                grLeaveMaster.FooterRow.Cells[3].Text = "Total :";
                grLeaveMaster.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grLeaveMaster.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                grLeaveMaster.FooterRow.Cells[6].Text = Convert.ToString(Math.Round(dclCount, 1));
                grLeaveMaster.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
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

    protected void grLeaveMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        string[] arInfo = new string[3];
        switch (_commandName)
        {
            case ("DoubleClick"):
                ModalPopupTree.Show();
                lblEmpID.Text = grLeaveMaster.SelectedRow.Cells[1].Text.Trim();
                lblEmpName.Text = grLeaveMaster.SelectedRow.Cells[2].Text.Trim();
                lblLeaveTitle.Text = grLeaveMaster.HeaderRow.Cells[4].Text;
                lblFromTo.Text = lblFrom.Text;
                DataTable dtDays = new DataTable();
                dtDays = objLevProMgr.SelectEnjoyedLeaveRecordsDays(lblEmpID.Text.Trim(), hfLType.Value.ToString(), hfFrom.Value.ToString(), hfTo.Value.ToString());
                grLeaveDetls.DataSource = dtDays;
                grLeaveDetls.DataBind();
                if (dtDays != null)
                {
                    dtDays.Rows.Clear();
                    dtDays.Dispose();
                }

                foreach (GridViewRow gRow in grLeaveDetls.Rows)
                {
                    if (Common.CheckNullString(gRow.Cells[0].Text) != "")
                        gRow.Cells[0].Text = Common.DisplayDate(gRow.Cells[0].Text);
                    if (Common.CheckNullString(gRow.Cells[1].Text) != "")
                        gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
                    if (Common.CheckNullString(gRow.Cells[5].Text) != "")
                        gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
                }

                break;
        }


    }
}
