using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for clsRemoteAllowannce
/// </summary>
public class Payroll_RemoteAllowannce
{
    public string AllowanceID { get; set; }
    public string EmpId { get; set; }
    public string PostingDivID { get; set; }
    public string SalLocId { get; set; }
    public string PostingPlaceId { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public string NoOfDays { get; set; }
    public string Basic { get; set; }
    public string Percentage { get; set; }
    public string Amount { get; set; }
    public string Remarks { get; set; }
    public string InsertedBy { get; set; }
    public string InsertedDate { get; set; }

    public Payroll_RemoteAllowannce()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}