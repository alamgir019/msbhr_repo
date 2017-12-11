
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

public partial class index : System.Web.UI.Page
{
    string invalch = "";
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lblMsg.Text = DateTime.Now.ToString();
            invalch = Request.Params["inval"];
            Session["USERID"] = "";
            if (invalch != null)
            {
                if (invalch == "Y")
                {
                    lblMsg.Text = "Invalid User ID or Password.";
                }
                if (invalch == "L")
                {
                    lblMsg.Text = "User has Logout.";
                }
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string strFiscalYear = "";
        string strFiscalStartDate = "";       
        string userid = txtuserid.Text.ToString();
        string password = txtpassword.Text.ToString();
        string strInputPwd = Common.getHashValue(password);
        DataTable dtUser = new DataTable();
        UserManager objUserMgr = new UserManager();
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

        dtUser = objUserMgr.SelectUserInfo(userid, "Y");
        
        // Payroll Fiscal Year
        DataTable dtPayOpt = objOptMgr.SelectpaySlipOption("OC03");
        if (dtPayOpt.Rows.Count > 0)
        {
            strFiscalYear = dtPayOpt.Rows[0]["OPTVALUE"].ToString().Trim();
            strFiscalStartDate = dtPayOpt.Rows[0]["PAYROLLVALIDFROM"].ToString().Trim();
        }

        if (dtUser.Rows.Count > 0)
        {
            Session["LOGINID"] = Common.getMaxId("UserInOutHistory", "LogInId");
            foreach (DataRow row in dtUser.Rows)
            {
                //if (strInputPwd != "")
                //{
                if (string.Compare(row["Password"].ToString().Trim(), strInputPwd) == 0)
                {
                    if (strInputPwd != "0")
                    {
                        Session["USERID"] = userid.ToString();
                        Session["USERNAME"] = row["FullName"].ToString();
                        Session["EMPID"] = row["EMPID"].ToString();
                        Session["EMAILID"] = row["OfficeEmail"].ToString();
                        Session["OFFICE"] = row["DivisionName"].ToString();
                        Session["TEAM"] = row["DEPTNAME"].ToString();
                        Session["TEAMID"] = row["DEPTID"].ToString();
                        Session["EMPLOYEEID"] = row["EmpId"].ToString().Trim();
                        Session["ISADMIN"] = row["IsAdmin"].ToString().Trim();
                        Session["ISPAYADMIN"] = row["IsPayAdmin"].ToString().Trim();
                        Session["USERROLE"] = row["UserRole"].ToString().Trim();
                        Session["DESIGNATION"] = row["JobTitleName"].ToString().Trim();
                        Session["FISCALYRID"] = strFiscalYear;
                        Session["FISCALSTARTDATE"] = strFiscalStartDate;
                        Session["USDRATE"] = Convert.ToDouble(objPayMgr.SelectUSDRate());
                        objUserMgr.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()),
                                Common.SetDateTime(DateTime.Now.ToString()), "S", "N");
                        DataTable dtTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "1", "T103");
                        if (dtTaskPermission.Rows.Count > 0)
                            Response.Redirect("File/Home.aspx");
                        else
                            Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Session["USERID"] = "";
                        Session["USERNAME"] = "";
                        Session["EMPID"] = "";
                        Session["EMAILID"] = "";
                        Session["OFFICE"] = "";
                        Session["TEAM"] = "";
                        Session["TEAM"] = "";
                        Session["EMPLOYEEID"] = "";
                        Session["ISADMIN"] = "";
                        Session["ISPAYADMIN"] = "";
                        Session["TEAMID"] = "";
                        Session["DESIGNATION"] = "";
                        Session["ISADMIN"] = "";
                        Session["FISCALYRID"] = "";
                        Session["FISCALSTARTDATE"] = "";
                        Session["USDRATE"] = "";
                        Response.Redirect("index.aspx?inval=Y");
                        lblMsg.Text = "";
                        objUserMgr.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()),
                                 Common.SetDateTime(DateTime.Now.ToString()), "U", "N");

                        this.FillOptionValue();  
                    }
                }
                else
                {
                    objUserMgr.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()),
                                Common.SetDateTime(DateTime.Now.ToString()), "U", "N");
                    lblMsg.Text = "Invalid User Id or Password.";
                }
            }
        }
        else
        {
            //Session["USERID"] = "";
            Session["USERNAME"] = "";
            Session["EMPID"] = "";
            Session["EMAILID"] = "";
            Session["COUNTRYDIRECTOR"] = "";
            Session["OFFICE"] = "";
            Session["PROGRAM"] = "";
            Session["OFFICEID"] = "";
            Session["PROGRAMID"] = "";
            Session["TEAM"] = "";
            Session["TEAMID"] = "";
            Session["EMPLOYEEID"] = "";
            Session["ISADMIN"] = "";
            Session["ISSHIFTINCHR"] = "";
            Session["DESIGNATION"] = "";
            Session["LOCATION"] = "";
            // Payroll
            Session["FISCALYRID"] = "";
            Session["USERID"] = txtuserid.Text.Trim();
            //objUserMgr.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()),
            //                   Common.SetDateTime(DateTime.Now.ToString()), "U", "N");
            Response.Redirect("index.aspx?inval=Y");
            lblMsg.Text = "";

        }
    }

    protected void FillOptionValue()
    {
        DataTable dtOpt = new DataTable();
        dtOpt = objMasMgr.SelectOptionBag("");
        if (dtOpt.Rows.Count > 0)
        {
            foreach (DataRow Row in dtOpt.Rows)
            {
                if (Row["OptId"].ToString() == "OC01".ToString())
                    Session["OptRetAge"] = Row["OptValue"].ToString();
                else if (Row["OptId"].ToString() == "OC02")
                    Session["OptBasicPercent"] = Convert.ToInt16(Row["OptValue"]);               
            }
        }
    }    
}
