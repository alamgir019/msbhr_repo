using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsEmpSalaryCharging
/// </summary>
public class clsEmpSalaryCharging
{
    public string SalChargeId { get; set; }
    public string EmpId { get; set; }
    public string EntryDate { get; set; }
    public string SalarySourceId { get; set; }
    public string Percentage { get; set; }
    public string IsActive { get; set; }
    public string TotalCharge { get; set; }        
    public string InsertedBy { get; set; }
    public string InsertedDate { get; set; }

	public clsEmpSalaryCharging()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}