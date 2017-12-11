using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

public partial class TimeSheetReportViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc; 
    private string ReportPath = "";
    ReportManager rptManager = new ReportManager();
    dsTimeSheet ds = new dsTimeSheet();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        string[] strVal = strParams.Split(',');
        this.GenerateReport(strVal[0], strVal[1], strVal[2],Convert.ToBoolean(strVal[3]));
    }

    protected void GenerateReport(string strEmpId, string strMonth, string strYear,bool blnIsRound)
    {
        ReportDoc = new ReportDocument();
        if (blnIsRound == false)
            ReportPath = Server.MapPath("../CrystalReports/rptTimeSheet.rpt");
        else
            ReportPath = Server.MapPath("../CrystalReports/rptTimeSheetRound.rpt");
        ReportDoc.Load(ReportPath);

        DataTable dtTimeSheetEmpInfo = rptManager.Get_TimeSheetEmpInfo(strEmpId, strMonth, strYear);

        DataTable dtTimeSheet = rptManager.Get_TimeSheetReport(strEmpId, strMonth, strYear, blnIsRound);

        ReportManager objRM1 = new ReportManager();
        DataTable dtTimeSheetHoliday = objRM1.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "H");

        ReportManager objRM2 = new ReportManager();
        DataTable dtTimeSheetSick = objRM2.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "SL");

        ReportManager objRM3 = new ReportManager();
        DataTable dtTimeSheetUnPaid = objRM3.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "LW");

        ReportManager objRM4 = new ReportManager();
        DataTable dtTimeSheetVacation = objRM4.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "V");

        ReportManager objRM5 = new ReportManager();
        DataTable dtTimeSheetWH = objRM5.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "WH");

        if (dtTimeSheetEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtTimeSheetEmpInfo.Rows)
            {
                DataRow nRow = ds.dtTimeSheet.NewRow();

                nRow["TIME_CODE"] = dRow["TIME_CODE"];
                nRow["SOF_CODE"] = dRow["SOF_CODE"];
                nRow["PROJECT_CODE"] = dRow["PROJECT_CODE"];
                nRow["EmpId"] = dRow["EmpId"];
                nRow["VYear"] = dRow["VYear"];
                nRow["VMonth"] = dRow["VMonth"];
                nRow["FullName"] = dRow["FullName"];
                nRow["DesigName"] = dRow["DesigName"];
                nRow["PostingPlaceName"] = dRow["PostingPlaceName"];

                DataRow[] foundRows = dtTimeSheet.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND SalarySourceId=" + dRow["SalarySourceId"].ToString().Trim());
                if (foundRows.Length  > 0)
                {
                    nRow["1"] = GetZeroIfNull(foundRows[0]["1"].ToString());
                    nRow["2"] = GetZeroIfNull(foundRows[0]["2"].ToString());
                    nRow["3"] = GetZeroIfNull(foundRows[0]["3"].ToString());
                    nRow["4"] = GetZeroIfNull(foundRows[0]["4"].ToString());
                    nRow["5"] = GetZeroIfNull(foundRows[0]["5"].ToString());
                    nRow["6"] = GetZeroIfNull(foundRows[0]["6"].ToString());
                    nRow["7"] = GetZeroIfNull(foundRows[0]["7"].ToString());
                    nRow["8"] = GetZeroIfNull(foundRows[0]["8"].ToString());
                    nRow["9"] = GetZeroIfNull(foundRows[0]["9"].ToString());
                    nRow["10"] = GetZeroIfNull(foundRows[0]["10"].ToString());
                    nRow["11"] = GetZeroIfNull(foundRows[0]["11"].ToString());
                    nRow["12"] = GetZeroIfNull(foundRows[0]["12"].ToString());
                    nRow["13"] = GetZeroIfNull(foundRows[0]["13"].ToString());
                    nRow["14"] = GetZeroIfNull(foundRows[0]["14"].ToString());
                    nRow["15"] = GetZeroIfNull(foundRows[0]["15"].ToString());
                    nRow["16"] = GetZeroIfNull(foundRows[0]["16"].ToString());
                    nRow["17"] = GetZeroIfNull(foundRows[0]["17"].ToString());
                    nRow["18"] = GetZeroIfNull(foundRows[0]["18"].ToString());
                    nRow["19"] = GetZeroIfNull(foundRows[0]["19"].ToString());
                    nRow["20"] = GetZeroIfNull(foundRows[0]["20"].ToString());
                    nRow["21"] = GetZeroIfNull(foundRows[0]["21"].ToString());
                    nRow["22"] = GetZeroIfNull(foundRows[0]["22"].ToString());
                    nRow["23"] = GetZeroIfNull(foundRows[0]["23"].ToString());
                    nRow["24"] = GetZeroIfNull(foundRows[0]["24"].ToString());
                    nRow["25"] = GetZeroIfNull(foundRows[0]["25"].ToString());
                    nRow["26"] = GetZeroIfNull(foundRows[0]["26"].ToString());
                    nRow["27"] = GetZeroIfNull(foundRows[0]["27"].ToString());
                    nRow["28"] = GetZeroIfNull(foundRows[0]["28"].ToString());
                    nRow["29"] = GetZeroIfNull(foundRows[0]["29"].ToString());
                    nRow["30"] = GetZeroIfNull(foundRows[0]["30"].ToString());
                    nRow["31"] = GetZeroIfNull(foundRows[0]["31"].ToString());
                    ds.dtTimeSheet.Rows.Add(nRow);
                }
                else
                {
                    nRow["1"] = "0";
                    nRow["2"] = "0";
                    nRow["3"] = "0";
                    nRow["4"] = "0";
                    nRow["5"] = "0";
                    nRow["6"] = "0";
                    nRow["7"] = "0";
                    nRow["8"] = "0";
                    nRow["9"] = "0";
                    nRow["10"] = "0";
                    nRow["11"] = "0";
                    nRow["12"] = "0";
                    nRow["13"] = "0";
                    nRow["14"] = "0";
                    nRow["15"] = "0";
                    nRow["16"] = "0";
                    nRow["17"] = "0";
                    nRow["18"] = "0";
                    nRow["19"] = "0";
                    nRow["20"] = "0";
                    nRow["21"] = "0";
                    nRow["22"] = "0";
                    nRow["23"] = "0";
                    nRow["24"] = "0";
                    nRow["25"] = "0";
                    nRow["26"] = "0";
                    nRow["27"] = "0";
                    nRow["28"] = "0";
                    nRow["29"] = "0";
                    nRow["30"] = "0";
                    nRow["31"] = "0";
                    ds.dtTimeSheet.Rows.Add(nRow);
                }
            }
        }
        #region hide else
        //else
        //{
        //    DataRow nRow = ds.dtTimeSheet.NewRow();

        //    nRow["TIME_CODE"] = "";
        //    nRow["SOF_CODE"] = "";
        //    nRow["PROJECT_CODE"] = "";
        //    nRow["EmpId"] = strEmpId;
        //    nRow["VYear"] = strYear;
        //    nRow["VMonth"] = strMonth;
        //    nRow["FullName"] = "";
        //    nRow["DesigName"] = "";
        //    nRow["PostingPlaceName"] = "";
        //    nRow["1"] = "0";
        //    nRow["2"] = "0";
        //    nRow["3"] = "0";
        //    nRow["4"] = "0";
        //    nRow["5"] = "0";
        //    nRow["6"] = "0";
        //    nRow["7"] = "0";
        //    nRow["8"] = "0";
        //    nRow["9"] = "0";
        //    nRow["10"] = "0";
        //    nRow["11"] = "0";
        //    nRow["12"] = "0";
        //    nRow["13"] = "0";
        //    nRow["14"] = "0";
        //    nRow["15"] = "0";
        //    nRow["16"] = "0";
        //    nRow["17"] = "0";
        //    nRow["18"] = "0";
        //    nRow["19"] = "0";
        //    nRow["20"] = "0";
        //    nRow["21"] = "0";
        //    nRow["22"] = "0";
        //    nRow["23"] = "0";
        //    nRow["24"] = "0";
        //    nRow["25"] = "0";
        //    nRow["26"] = "0";
        //    nRow["27"] = "0";
        //    nRow["28"] = "0";
        //    nRow["29"] = "0";
        //    nRow["30"] = "0";
        //    nRow["31"] = "0";
        //    ds.dtTimeSheet.Rows.Add(nRow);
        //}
        #endregion
        ds.dtTimeSheet.AcceptChanges();

        //Holiday
        this.GetHoliday(dtTimeSheetHoliday, ds.dtTimeSheetHoliday);

        //Sick
        this.GetHoliday(dtTimeSheetSick, ds.dtTimeSheetSick);

        //Un Paid
        this.GetHoliday(dtTimeSheetUnPaid, ds.dtTimeSheetUnPaid);

        //Vacation
        this.GetHoliday(dtTimeSheetVacation, ds.dtTimeSheetVacation);
                
        //Work Home
        this.GetHoliday(dtTimeSheetWH, ds.dtTimeSheetWH);

        ReportDoc.SetDataSource(ds);
        CRVA.ReportSource = ReportDoc;
    }

    private void GetHoliday(DataTable dtRecord, DataTable dtDS)
    {
        foreach (DataRow dRow in dtRecord.Rows)
        {
            DataRow nRow = dtDS.NewRow();
            nRow["EmpId"] = dRow["EmpId"];
            nRow["1"] = GetZeroIfNull(dRow["1"].ToString());
            nRow["2"] = GetZeroIfNull(dRow["2"].ToString());
            nRow["3"] = GetZeroIfNull(dRow["3"].ToString());
            nRow["4"] = GetZeroIfNull(dRow["4"].ToString());
            nRow["5"] = GetZeroIfNull(dRow["5"].ToString());
            nRow["6"] = GetZeroIfNull(dRow["6"].ToString());
            nRow["7"] = GetZeroIfNull(dRow["7"].ToString());
            nRow["8"] = GetZeroIfNull(dRow["8"].ToString());
            nRow["9"] = GetZeroIfNull(dRow["9"].ToString());
            nRow["10"] = GetZeroIfNull(dRow["10"].ToString());
            nRow["11"] = GetZeroIfNull(dRow["11"].ToString());
            nRow["12"] = GetZeroIfNull(dRow["12"].ToString());
            nRow["13"] = GetZeroIfNull(dRow["13"].ToString());
            nRow["14"] = GetZeroIfNull(dRow["14"].ToString());
            nRow["15"] = GetZeroIfNull(dRow["15"].ToString());
            nRow["16"] = GetZeroIfNull(dRow["16"].ToString());
            nRow["17"] = GetZeroIfNull(dRow["17"].ToString());
            nRow["18"] = GetZeroIfNull(dRow["18"].ToString());
            nRow["19"] = GetZeroIfNull(dRow["19"].ToString());
            nRow["20"] = GetZeroIfNull(dRow["20"].ToString());
            nRow["21"] = GetZeroIfNull(dRow["21"].ToString());
            nRow["22"] = GetZeroIfNull(dRow["22"].ToString());
            nRow["23"] = GetZeroIfNull(dRow["23"].ToString());
            nRow["24"] = GetZeroIfNull(dRow["24"].ToString());
            nRow["25"] = GetZeroIfNull(dRow["25"].ToString());
            nRow["26"] = GetZeroIfNull(dRow["26"].ToString());
            nRow["27"] = GetZeroIfNull(dRow["27"].ToString());
            nRow["28"] = GetZeroIfNull(dRow["28"].ToString());
            nRow["29"] = GetZeroIfNull(dRow["29"].ToString());
            nRow["30"] = GetZeroIfNull(dRow["30"].ToString());
            nRow["31"] = GetZeroIfNull(dRow["31"].ToString());
            dtDS.Rows.Add(nRow);
        }
        dtDS.AcceptChanges();
    }

    private decimal GetZeroIfNull(string strData)
    {
        if (string.IsNullOrEmpty(strData) == true)       
            return 0;        
        else        
            return Convert.ToDecimal(strData);        
    }

    protected void CRVA_BeforeRender(object source, CrystalDecisions.Web.HtmlReportRender.BeforeRenderEvent e)
    {
        Page.ClientScript.RegisterForEventValidation(CRVA.UniqueID);
    }

    protected void CRVA_Unload(object sender, EventArgs e)
    {
        ReportDoc.Close();
        ReportDoc.Dispose();
        ReportDoc = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}