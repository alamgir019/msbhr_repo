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

public partial class File_TaskAlert : System.Web.UI.Page
{    
    TaskAlertMgr objTaskAlt = new TaskAlertMgr();
    //MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtTaskAlt = new DataTable();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtEmpInfo=objEmpInfoMgr.SelectEmpInfo("");
            Common.FillIdNameDropDownList3(dtEmpInfo , ddlEmpId,"FullName","EmpId",true);
            ddlEmpId.SelectedValue = Session["EMPID"].ToString().Trim();  
            this.EntryMode(false);                   
            this.OpenRecord();
            this.FormatGridViewDate();
        }
    }

    private void FormatGridViewDate()
    {
        foreach (GridViewRow gRow in grTaskAlertList.Rows)
        {
            foreach (ListItem itm in ddlEmpId.Items)
            {
                if (itm.Value.ToUpper().Trim() == gRow.Cells[4].Text.ToUpper().Trim())
                {
                    gRow.Cells[4].Text = itm.Text.Trim();
                }
            }
            TextBox txtGrReminderDate = (TextBox)gRow.Cells[3].FindControl("txtGrReminderDate");
            //if (string.IsNullOrEmpty(Common.CheckNullString(txtGrReminderDate.Text)) == false)
            //    return;

            
            //gRow.Cells[3].FindControl("txtGrReminderDate") = (TextBox)gRow.Cells[3].FindControl("txtGrReminderDate");
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
    }
    
    private void OpenRecord()
    {
        dtTaskAlt = objTaskAlt.SelectTaskAlert(0,"");
        grTaskAlertList.DataSource = dtTaskAlt;
        grTaskAlertList.DataBind();
    }    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (txtTitle.Text.Trim() == "")
            {
                lblMsg.Text = "Please mention task title.";
                txtTitle.Focus();
                return false;
            }

            if (ddlEmpId.SelectedValue   == "-1")
            {
                lblMsg.Text = "Please select resource from the list.";
                ddlEmpId.Focus();
                return false;
            } 
 
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData(string IsDelete)
    {
        string  strTransId = "";
        try
        {
            strTransId = Common.getMaxId("TaskAlert", "TransId");

            string strReminderDate;
            string strCompletedDate;
            if (txtReminderDate.Text.Trim() != "")
                strReminderDate = Common.ReturnDate(txtReminderDate.Text.Trim());
            else
                strReminderDate = "";

            if (txtCompletedDate.Text.Trim() != "")
                strCompletedDate = Common.ReturnDate(txtCompletedDate.Text.Trim());
            else
                strCompletedDate = "";

            objTaskAlt.InsertTaskAlert(strTransId, txtTitle.Text.Trim(), strReminderDate, strCompletedDate, ddlEmpId.SelectedValue.ToString().Trim(), "P",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N");

            lblMsg.Text = "Task alert saved successfully";
            
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void grTaskAlertList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("UpdateClick"):
                string strReminderDate="";
                string strCompletedDate = "";
                Session["TRANSID"] = grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();

                TextBox txtGrReminderDt = (TextBox)grTaskAlertList.SelectedRow.Cells[2].FindControl("txtGrReminderDate");
                strReminderDate = Common.CheckNullString(txtGrReminderDt.Text);
                if (strReminderDate != "")
                    strReminderDate = Common.SetDate(strReminderDate);

                TextBox txtGrCompletedDt = (TextBox)grTaskAlertList.SelectedRow.Cells[2].FindControl("txtGrCompletedDate");
                strCompletedDate = Common.CheckNullString(txtGrCompletedDt.Text);
                if (strCompletedDate != "")
                    strCompletedDate = Common.SetDate(strCompletedDate);

                objTaskAlt.InsertTaskAlert(Session["TRANSID"].ToString(), grTaskAlertList.SelectedRow.Cells[1].Text.Trim(),
                    strReminderDate, strCompletedDate, grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[1].ToString(), "C",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "Y");
                break;
            case ("SetAlertClick"):
                Session["TRANSID"] = grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                Session["TOEMPID"] = "";// grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                Session["TOEMAILID"] = "";//grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                Session["SUBJECT"] = grTaskAlertList.SelectedRow.Cells[1].Text.Trim();
                Response.Redirect("EmailTaskAlert.aspx");
                break;
        }
    }

   
}
