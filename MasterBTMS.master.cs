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

public partial class MasterBTMS : System.Web.UI.MasterPage
{
    DBConnector objDB = new DBConnector();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Session["USERID"].ToString()) == false)
            {
                Menu[] mnu = new Menu[8];
                mnu[0] = Menu1;
                mnu[1] = Menu2;
                mnu[2] = Menu3;
                mnu[3] = Menu4;
                mnu[4] = Menu5;
                mnu[5] = Menu6;
                mnu[6] = Menu7;
                mnu[7] = Menu8;
                Common.GetMenuItem(mnu, HttpContext.Current.Session["USERID"].ToString(), Session["ISADMIN"].ToString().Trim());
                // GetMenuItem(Menu1, "admin");
                lblUser.Text = HttpContext.Current.Session["USERID"].ToString();
                
                //*Unchecked
                if (string.IsNullOrEmpty(Session["OFFICE"].ToString()) == false)
                    lblOffice.Text = Session["OFFICE"].ToString();
                else
                    lblOfficeCaption.Visible = false;
                if (string.IsNullOrEmpty(Session["PROGRAM"].ToString()) == false)
                    lblProgram.Text = Session["PROGRAM"].ToString();
                else
                    lblProgCaption.Visible = false;

                if (string.IsNullOrEmpty(Session["TEAM"].ToString()) == false)
                    lblTeam.Text = Session["TEAM"].ToString();
                else
                    lblTeamCaption.Visible = false;

                //*Unchecked
            }
            else
            {
                Response.Redirect("../Index.aspx");
            }
        }
    }

    //public void GetMenuItem(Menu mnu, string userid)
    //{

    //    string sql = "";
    //    DataRow[] mRows;
    //    DataRow[] cRows;
    //    sql = "Select v.ViewId,v.ViewName,v.ShowToPage,v.ParentId from ViewName v, userprivs up, userinfo ui where v.ViewId=up.SCREEN_ID AND ui.USERID=up.USERID AND up.A='Y' AND ui.Userid='" + userid.Trim() + "' order by viewid";
    //   // sql = "Select v.ViewId,v.ViewName,v.ShowToPage,v.ParentId from ViewName v Order By v.ViewId";
    //    DataTable dtMnuMaster = objDB.CreateDT(sql, "MnuMaster");
    //    mRows = FindInMenuItem("ParentId='0'", dtMnuMaster);
    //    foreach (DataRow row in mRows)
    //    {
    //        MenuItem masterItem = new MenuItem(row["ViewName"].ToString(), row["ShowToPage"].ToString());
    //        //mnu.Items.Add(masterItem);
    //        cRows = FindInMenuItem("ParentId='" + row["ViewId"].ToString() + "'", dtMnuMaster);
    //        foreach (DataRow rowc in cRows)
    //        {
    //            MenuItem childItem = new MenuItem(rowc["ViewName"].ToString(), rowc["ShowToPage"].ToString());
    //            masterItem.ChildItems.Add(childItem);
    //        }
    //        mnu.Items.Add(masterItem);
    //    }
    //}

    //public DataRow[] FindInMenuItem(string strExpr, DataTable dtMnuMaster)
    //{
    //    DataRow[] foundRows;
    //    foundRows = dtMnuMaster.Select(strExpr);
    //    return foundRows;
    //}

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {        
        Response.Redirect("~/" + Menu1.SelectedValue.ToString());
    }

    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu2.SelectedValue.ToString());
    }

    protected void Menu3_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu3.SelectedValue.ToString());
    }

    protected void Menu4_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu4.SelectedValue.ToString());
    }

    protected void Menu5_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu5.SelectedValue.ToString());
    }

    protected void Menu6_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu6.SelectedValue.ToString());
    }

    protected void Menu7_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu7.SelectedValue.ToString());
    }
    protected void Menu8_MenuItemClick(object sender, MenuEventArgs e)
    {
        Response.Redirect("~/" + Menu8.SelectedValue.ToString());
    }
  
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        UserManager objUserMgr = new UserManager();
        if (string.IsNullOrEmpty(Session["USERID"].ToString().Trim()) == false)
            objUserMgr.InsertUserInOutHistory(Session["LogInId"].ToString(), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), 
                Common.SetDateTime(DateTime.Now.ToString()),"S", "Y");
        Response.Cookies.Clear();
        Response.Redirect("~/index.aspx");      

        Session["USERID"] = "";
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
        Session["FISCALYRID"] = "";
    }    
}
