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
public partial class EIS_ProfessionalDegreeSetup : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DataTable dtList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            dtList.Rows.Clear();
            dtList.Dispose();
            grprofessional.DataSource = null;
            grprofessional.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
           // this.EntryMode(false);
            this.OpenRecord();

        }
    }


    private void OpenRecord()
    {
        dtList = objEmpMgr.SelectDegree(0,"A","");
        grprofessional.DataSource = dtList;
        grprofessional.DataBind();
        int i=0;
        foreach (GridViewRow gRow in grprofessional.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            string IsProfessional = dtList.Rows[i]["IsProfessional"].ToString();
            if (IsProfessional=="Y")
            {
                chkBox.Checked = true;
            }
            else
            {
                chkBox.Checked = false;
            }
            i++;
        }

    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            Desigation objCnt = new Desigation(lngID.ToString(), "", "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", "Y");
            MasMgr.UpdateProfessionalDegree(grprofessional, Session["USERID"].ToString());
            lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            //this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }




        //  clsCommonSetup objCommonSetup = new clsCommonSetup(hfId.Value.ToString(), txtName.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N",
        //        Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

        //   objEmpMgr.InsertDegree(objCommonSetup, hfIsUpdate.Value, IsDelete);
        //   lblMsg.Text = "Record Updated Successfully";
        //    Common.EmptyTextBoxValues(this);
        //    this.EntryMode(false);
        //    this.OpenRecord();
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = "";
        //    throw (ex);
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

   

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView _gridView = (GridView)sender;
        //// Get the selected index and the command name
        //int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        //string _commandName = e.CommandName;
        //_gridView.SelectedIndex = _selectedIndex;
        //switch (_commandName)
        //{
        //    case ("DoubleClick"):

        //        txtName.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim ());
        //        hfId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
        //        if (Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim()) == "Y")
        //            chkInActive.Checked = false;
        //        else
        //            chkInActive.Checked = true;
        //        this.EntryMode(true);
        //        break;
        //}
    }
}
