using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for employeelist
/// </summary>

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class employeelist : System.Web.Services.WebService
{
    public employeelist()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public List<clsEmpInfo> GetEmployee(string empname)
    {
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        DataRow[] emprows = objEmpInfoMgr.SelectEmpNameWithID("").Select("EMPNAME like '%"+empname+"%'");
        List<clsEmpInfo> emplist = new List<clsEmpInfo>();
        int i = 0;
        foreach (var item in emprows)
        {
            i++;
            clsEmpInfo emp = new clsEmpInfo(item["EMPID"].ToString(),item["DesigName"].ToString(),item["EMPNAME"].ToString(),"","","","","",
        "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","",
        "","","","","","","","","","","","","","");
            emp.DeptName = item["DeptName"].ToString();
            emplist.Add(emp);
            if (i>100)
            {
                break;
            }
        }
        return emplist;
    }

}