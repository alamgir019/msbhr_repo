using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsPerformanceAppraisal
/// </summary>
public class clsPerformanceAppraisal
{
    public string AppId { get; set; }
    public string EmpId { get; set; }
    public string EntryDate { get; set; }
    public string FiscalYrId { get; set; }
    public string IsMidTerm { get; set; }
    public string TotlalRating { get; set; }
    public string Overallrating { get; set; }
    public string Remarks { get; set;}
    public string InsertedBy { get; set; }
    public string InsertedDate { get; set; }
	public clsPerformanceAppraisal()
	{
		
	}
}