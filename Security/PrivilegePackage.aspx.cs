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
using System.Data.SqlClient;

public partial class PrivilegePackage : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();    
    UserManager objUserMgr = new UserManager(); 

     DataTable dtUser = new DataTable();
     DataTable dtScr = new DataTable();
     DataTable dtPriv = new DataTable();
     DataTable dtpage = new DataTable();
    DataTable dtPrvPack = new DataTable();   
    protected static string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            dtUser.Rows.Clear();
            dtUser.Dispose();
            grPriv.DataSource = null;
            grPriv.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.LoadPriviliage();
            this.OpenRecord(0);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        hfPrivPackID.Value = "";
       
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
            btnSave.Text = "Update";
        else
        {
            btnSave.Text = "Save";
            hfPrivPackID.Value = "";
            Common.EmptyTextBoxValues(this);
        }
       
    }

    protected void OpenRecord(int intID)
    {
        dtPrvPack = objUserMgr.SelectPrivPack(intID);
        if (intID == 0)
        {
            grPrivPack.DataSource = dtPrvPack;
            grPrivPack.DataBind();
        }
    }

    private void LoadPriviliage()
    {
        DataTable dtUserPrivs = objUserMgr.SelectScreenInfo();
        grPriv.DataSource = GroupScreenNames(dtUserPrivs);
        grPriv.DataBind();
        DesignGridView();
    }
    protected void DesignGridView()
    {
        for (int i = 0; i < grPriv.Rows.Count; i++)
        {
            if (grPriv.DataKeys[i].Values[3].ToString() == "0")
            {
                grPriv.Rows[i].BackColor = System.Drawing.Color.FromArgb(255, 153, 051);
                grPriv.Rows[i].Font.Bold = true;
                grPriv.Rows[i].Cells[0].ForeColor = System.Drawing.Color.White;
            }
            else if (grPriv.DataKeys[i].Values[3].ToString() == "1")
            {
                grPriv.Rows[i].BackColor = System.Drawing.Color.FromArgb(102, 153, 051);
                grPriv.Rows[i].Font.Bold = true;
                grPriv.Rows[i].Cells[0].ForeColor = System.Drawing.Color.White;
                grPriv.Rows[i].Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + grPriv.Rows[i].Cells[0].Text;
            }
            else 
            {
                grPriv.Rows[i].Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + grPriv.Rows[i].Cells[0].Text;
            }
            if (grPriv.DataKeys[i].Values[0].ToString() == "1")
            {
                CheckBox chk = (CheckBox)grPriv.Rows[i].FindControl("chkAll");
                chk.Checked = true;
                chk.Enabled = false;
                grPriv.Rows[i].BackColor = System.Drawing.Color.FromArgb(51, 102, 204);
                grPriv.Rows[i].Font.Bold = true;
                grPriv.Rows[i].Cells[0].ForeColor = System.Drawing.Color.White;
            }
        }
    }
    protected DataTable GroupScreenNames(DataTable dtUP)
    {
        DataTable dtTmp = dtUP.Clone();

        DataRow[] MasterRows;
        //DataRow[] ChildRows;

        //DataTable dtMnuMaster = objDB1.CreateDT(sql, "MenuItems");
        MasterRows = FindInDataTable(dtUP, "ParentId='0'");
        foreach (DataRow row in MasterRows)
        {
            dtTmp.ImportRow(row);
            GetChildRow(dtUP, row, dtTmp);
            dtTmp.AcceptChanges();
        }
        return dtTmp;
    }

    public static DataRow[] FindInDataTable(DataTable dtUP, string strExpr)
    {
        DataRow[] foundRows;
        foundRows = dtUP.Select(strExpr);
        return foundRows;
    }
    public static void GetChildRow(DataTable dtUP, DataRow row, DataTable dtTmp)
    {
        DataRow[] ChildRows;
        ChildRows = null;
        ChildRows = FindInDataTable(dtUP, "ParentId='" + row["ViewId"].ToString() + "'");
        foreach (DataRow rowc in ChildRows)
        {
            dtTmp.ImportRow(rowc);
            GetChildRow(dtUP, rowc, dtTmp);
            dtTmp.AcceptChanges();
        }
    }
   

    protected char chkvalchar(bool chk)
    {
        if (chk == true)
            return 'Y';
        else
            return 'N';
    }

    protected bool CheckedAll(string chk)
    {
        return true; 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserPrevilege objPriv = new UserPrevilege();
        if (hfPrivPackID.Value=="")
            hfPrivPackID.Value=Common.getMaxId("PrivPackage","PrivPackID");

        objUserMgr.InsertPrivPackage(grPriv, hfPrivPackID.Value, txtPackName.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Previlage Package Created Successfully.";
        this.EntryMode(false);
        this.OpenRecord(0);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }

  
    protected void grPrivPack_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtPackName.Text = grPrivPack.SelectedRow.Cells[1].Text;
                hfPrivPackID.Value = grPrivPack.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.OpenRecord(Convert.ToInt32(hfPrivPackID.Value));

                if (dtPrvPack.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (GridViewRow gRow in grPriv.Rows)
                    {
                        CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkAll");
                        DataRow[] foundRows;
                        int inViewID=Convert.ToInt32(grPriv.DataKeys[i].Values[0].ToString().Trim());
                        foundRows=dtPrvPack.Select("VIEWID=" + inViewID );
                        if (foundRows.Length>0)
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                        i++;
                    }
                }
                this.EntryMode(true);
                break;
            case ("SyncClick"):
                DataTable dtUser = objUserMgr.GetPrivPackWiseUser(grPrivPack.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                //this.OpenRecord(Convert.ToInt32(hfPrivPackID.Value));
                objUserMgr.SynchronizeUserPrivelege(dtUser, grPrivPack.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                lblMsg.Text = "User Synchronization has been completed successfully";
                break;

        }
    }
    protected void grPrivPack_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
