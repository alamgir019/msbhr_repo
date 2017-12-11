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
using System.Data.OleDb;
using System.IO;

public partial class File_EmailTaskAlert : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector(); 
    TaskAlertMgr objTaskAlt = new TaskAlertMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtEmpInfo = new DataTable();
    DataTable dtPendingAlt = new DataTable();
    DataTable dtCompletedAlt = new DataTable();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    static string ModalPopupExtenderSelected = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            this.FillEmpInfo();
            hfIsUpdate.Value = "N";            
            this.OpenRecord();
            txtTo.Text = Session["TOEMAILID"].ToString().Trim();
            txtSubject.Text = Session["SUBJECT"].ToString().Trim();
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtTo.Text = "";
            txtCC.Text = "";
            txtBCC.Text = "";
            txtSubject.Text = "";
            FreetxtTask.Text = "";
            hfToAddr.Value = "";
            hfCCAddr.Value = "";
            hfBCCAddr.Value = "";
            TabContainer1.ActiveTabIndex = 0;
            chkGroupAttach.Checked = false;
            chkGroupAttach.Visible = true;
            chkAttachFile.Text = "";
            chkAttachFile.Checked = false;
            chkAttachFile.Visible = false;
        }
    }
    private void FillEmpInfo()
    {
        DataTable dtEmpInfo = objEmpInfoMgr.GetEmployeeForTaskAlert();
        if (dtEmpInfo.Rows.Count > 0)
        {
            grEmpSearch.DataSource = dtEmpInfo;
            grEmpSearch.DataBind();
        }
        else
        {
            grEmpSearch.DataSource = null;
            grEmpSearch.DataBind();
        }
        dtEmpInfo.Rows.Clear();
        dtEmpInfo.Dispose();
    }
    private void OpenRecord()
    {
        dtPendingAlt = objTaskAlt.SelectEmailTaskAlert(0, "0","P");
        grTaskAlertList.DataSource = dtPendingAlt;
        grTaskAlertList.DataBind();

        dtCompletedAlt = objTaskAlt.SelectEmailTaskAlert(0, "1", "C");
        grCompletedAlertList.DataSource = dtCompletedAlt;
        grCompletedAlertList.DataBind();

        this.FormatGridDate(grTaskAlertList);
        this.FormatGridDate(grCompletedAlertList); 
    }

    protected void FormatGridDate(GridView grName)
    {
        int SlNo = 0;
        if (grName.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grName.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[1].Text = SlNo.ToString();
                gRow.Cells[7].Text = Common.DisplayDateTime(gRow.Cells[7].Text);
            }
        }
        SlNo = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
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
            if (txtTo.Text.Trim()=="")
            {
                lblMsg.Text = "Please Insert Valid To Address.";
                txtTo.Focus();  
                return false;
            }

            // To Email Validate
            string[] strToEmail = txtTo.Text.Trim().Split(',');
            if (strToEmail.Length > 1)
            {
                if (this.ValidateEmailArray(strToEmail,"To") == false)
                {
                    strToEmail = null;
                    return false;
                }
            }
            strToEmail = null;
            // End of ToEmail

            // CC Email Validate
            string[] strCCEmail = txtCC.Text.Trim().Split(',');
            if (strCCEmail.Length > 1)
            {
                if (this.ValidateEmailArray(strCCEmail,"CC") == false)
                {
                    strCCEmail = null;
                    return false;
                }
            }
            strCCEmail = null;
            // End of CC Email

            // BCC Email Validate
            string[] strBCCEmail = txtBCC.Text.Trim().Split(',');
            if (strBCCEmail.Length > 1)
            {
                if (this.ValidateEmailArray(strBCCEmail, "BCC") == false)
                {
                    strBCCEmail = null;
                    return false;
                }
            }
            strBCCEmail = null;
            // End of BCC Email

            // Attach File
            if (chkGroupAttach.Checked == false)
            {
                string FolderPath = ConfigurationManager.AppSettings["AttachFilePath"];
                string status = "";
                if (FileUpload1.HasFile == false)
                {
                    lblMsg.Text = "Error - a file name must be specified.";
                    return false;
                }
                if (FileUpload1.HasFile == true)
                {
                    string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string FilePath = Server.MapPath(FolderPath + "/" + fn);
                    FileInfo File = new FileInfo(FilePath);
                    if (File.Exists == true)
                    {
                        File.Delete();
                    }
                    FileUpload1.PostedFile.SaveAs(FilePath);
                    hfAttach.Value = fn;
                    hfAttachPath.Value = System.IO.Path.GetDirectoryName(FileUpload1.PostedFile.FileName);
                }

            }

            //string[] arInfo = new string[4];
            //char[] splitter = { '\\' };



            //if (FileUpload1.PostedFile.FileName != "")
            //{
            //    if (FileUpload1.HasFile)
            //    {
            //        string FilePath = FileUpload1.PostedFile.FileName;
            //        arInfo = Common.str_split(FilePath, splitter);
            //        hfAttach.Value = arInfo[2];
            //    }

            //    if (arInfo.Length <= 4)
            //    {
            //        if ((arInfo[0] != "E:") || (arInfo[1] != "PLAN_Attach_File"))
            //        {
            //            lblMsg.Text = "Attached file location should be E:\\PLAN_Attach_File";
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        lblMsg.Text = "Attached file location should be E:\\PLAN_Attach_File";
            //        return false;
            //    }
            //}
            // End of Attach file

            if (string.IsNullOrEmpty(txtAltertDate.Text) == false)
            {
                double TotDay = 0;
                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                char[] splitter2 ={ '/' };
               
                string strSysDate = Common.DisplayDate(Common.SetDateTime(DateTime.Now.ToString()));
                string[] arinfo2 = Common.str_split(strSysDate, splitter2);
                if (arinfo2.Length == 3)
                {
                    dtFrom = Convert.ToDateTime(arinfo2[2] + "/" + arinfo2[1] + "/" + arinfo2[0]);
                    arinfo2 = null;
                }
                arinfo2 = Common.str_split(txtAltertDate.Text.Trim(), splitter2);
                if (arinfo2.Length == 3)
                {
                    dtTo = Convert.ToDateTime(arinfo2[2] + "/" + arinfo2[1] + "/" + arinfo2[0]);
                    arinfo2 = null;
                }

                TimeSpan Dur = dtTo.Subtract(dtFrom);

                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0);
                if (TotDay < 0)
                {
                    lblMsg.Text = "Alert date should be greater than current date.";
                    return false;
                }
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
        string strTransID = "";
        if (hfIsUpdate.Value == "N")
        {
            if(hfID.Value !="")
            {
                hfIsUpdate.Value="Y";
            }
            else
            {
                hfIsUpdate.Value="N";
            }


        }
        //    hfIsUpdate.Value = objTaskAlt.GetTaskAlert(Session["TRANSID"].ToString());

        try
        {            
            string strAlertDateTime = Common.ReturnDate(txtAltertDate.Text.Trim()) + " " + ddlAlertHour.SelectedValue.ToString() + ":" + ddlAlertMin.SelectedValue.ToString();
            DateTime dateTimeAlert = Convert.ToDateTime(strAlertDateTime);
            if (chkGroupAttach.Checked == true)
            {
                string FolderPath = ConfigurationManager.AppSettings["AttachFilePath"];
                string[] strToEmpArr = txtTo.ToolTip.Split(',');
                string[] strToEmailArr = txtTo.Text.Split(',');
                string strAttach = "";
                string strSubStr = "";
                bool IsAttachFound = false;
                if (strToEmpArr.Length > 0)
                {
                    ETaskAlert[] objETaskAlert = new ETaskAlert[strToEmpArr.Length];
                    for (int i = 0; i < strToEmpArr.Length;i++ )
                    {
                        IsAttachFound = false ;
                        string[] fileEntries = Directory.GetFiles(ConfigurationManager.AppSettings["AttachTempFilePath"]);
                        foreach (string strFilePath in fileEntries)
                        {
                            string[] strFiles = strFilePath.Split('\\');
                            if(strFiles.Length>0)
                                strSubStr = strFiles[strFiles.Length-1].Substring(0, 8);
                            if (strToEmpArr[i].ToUpper().Trim() == strSubStr.ToUpper().Trim())
                            {
                                strAttach = strFiles[strFiles.Length - 1];
                                // Client File Path
                                FileInfo orgFile = new FileInfo(ConfigurationManager.AppSettings["AttachTempFilePath"] + "/" + strFiles[strFiles.Length - 1]);


                                string FilePath = Server.MapPath(FolderPath + "/" + strFiles[strFiles.Length - 1]);
                                // Server File Path
                                FileInfo File = new FileInfo(FilePath);
                                if (File.Exists == true)
                                {
                                    File.Delete();
                                }
                                // Upload to Server
                                orgFile.CopyTo(FilePath);

                                strAlertDateTime = Common.SetDateTime(dateTimeAlert.ToString());

                                objETaskAlert[i] = new ETaskAlert(Session["TRANSID"].ToString(), Session["EMPID"].ToString().Trim(), Session["EMAILID"].ToString().Trim(),strToEmpArr[i], strToEmailArr[i], txtCC.Text.Trim(),
                                    txtBCC.Text.Trim(), txtSubject.Text.Trim(), strAttach, FreetxtTask.Text.Trim(), strAlertDateTime, "0", Session["USERID"].ToString(),
                                    Common.SetDateTime(DateTime.Now.ToString()));
                                
                                dateTimeAlert.AddSeconds(5);
                                IsAttachFound = true;
                            }
                        }
                        if (IsAttachFound == false)
                        {
                            strAlertDateTime = Common.SetDateTime(dateTimeAlert.ToString());

                            objETaskAlert[i] = new ETaskAlert(Session["TRANSID"].ToString(), Session["EMPID"].ToString().Trim(), Session["EMAILID"].ToString().Trim(), strToEmpArr[i], strToEmailArr[i], txtCC.Text.Trim(),
                                txtBCC.Text.Trim(), txtSubject.Text.Trim(), "", FreetxtTask.Text.Trim(), strAlertDateTime, "0", Session["USERID"].ToString(),
                                Common.SetDateTime(DateTime.Now.ToString()));

                            dateTimeAlert.AddSeconds(5);
                        }

                    }
                    if (objETaskAlert.Length > 0)
                        objTaskAlt.InsertGroupEmailTaskAlert(objETaskAlert, hfIsUpdate.Value, IsDelete);

                }

                //ETaskAlert[] objETaskAlert = new ETaskAlert(Session["TRANSID"].ToString(), Session["EMPID"].ToString().Trim(), Session["EMAILID"].ToString().Trim(), txtTo.Text.Trim(), txtCC.Text.Trim(),
                //    txtBCC.Text.Trim(), txtSubject.Text.Trim(), hfAttach.Value.ToString().Trim(), FreetxtTask.Text.Trim(), strAlertDateTime, "0", Session["USERID"].ToString(),
                //    Common.SetDateTime(DateTime.Now.ToString()));
            }
            else
            {

                ETaskAlert objETaskAlert = new ETaskAlert(Session["TRANSID"].ToString(), Session["EMPID"].ToString().Trim(), Session["EMAILID"].ToString().Trim(),txtTo.ToolTip.ToString(), txtTo.Text.Trim(), txtCC.Text.Trim(),
                    txtBCC.Text.Trim(), txtSubject.Text.Trim(), hfAttach.Value.ToString().Trim(), FreetxtTask.Text.Trim(), strAlertDateTime, "0", Session["USERID"].ToString(),
                    Common.SetDateTime(DateTime.Now.ToString()));

                objTaskAlt.InsertEmailTaskAlert(objETaskAlert, hfIsUpdate.Value, IsDelete,hfID.Value);
            }
            if ((hfIsUpdate.Value == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((hfIsUpdate.Value == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if (IsDelete == "Y")
                lblMsg.Text = "Record Deleted Successfully";
            this.OpenRecord();
            hfID.Value = "";
            //Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select an alert information from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }
    protected void grEmpSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (ModalPopupExtenderSelected == "To")
        {
            hfToAddr.Value = "";
            foreach (GridViewRow tt in grEmpSearch.Rows)
            {
                Boolean chkSelect = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkSelect")).Checked);
                if (chkSelect == true)
                {
                    if (hfToAddr.Value == "")
                    {
                        hfToAddr.Value = grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                        hfToEmpID.Value = grEmpSearch.DataKeys[i].Values[0].ToString().Trim();
                    }
                    else
                    {
                        hfToAddr.Value = hfToAddr.Value + "," + grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                        hfToEmpID.Value = hfToEmpID.Value + "," + grEmpSearch.DataKeys[i].Values[0].ToString().Trim();
                    }
                }
                i++;
            }
            txtTo.Text = hfToAddr.Value;
            txtTo.ToolTip = hfToEmpID.Value;
        }
        else if (ModalPopupExtenderSelected == "CC")
        {
            hfCCAddr.Value = "";
            foreach (GridViewRow tt in grEmpSearch.Rows)
            {
                Boolean chkSelect = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkSelect")).Checked);
                if (chkSelect == true)
                {
                    if (hfCCAddr.Value == "")
                        hfCCAddr.Value = grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                    else
                        hfCCAddr.Value = hfCCAddr.Value + "," + grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                }
                i++;
            }
            txtCC.Text = hfCCAddr.Value;
        }

        else if (ModalPopupExtenderSelected == "BCC")
        {
            hfBCCAddr.Value = "";
            foreach (GridViewRow tt in grEmpSearch.Rows)
            {
                Boolean chkSelect = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkSelect")).Checked);
                if (chkSelect == true)
                {
                    if (hfBCCAddr.Value == "")
                        hfBCCAddr.Value = grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                    else
                        hfBCCAddr.Value = hfBCCAddr.Value + "," + grEmpSearch.DataKeys[i].Values[1].ToString().Trim();
                }
                i++;
            }
            txtBCC.Text = hfBCCAddr.Value;
        }
        this.UnCheckedEmployee();
    }
    protected void btnCC_Click(object sender, EventArgs e)
    {
        ModalPopupTree.TargetControlID = "btnCC";
        ModalPopupExtenderSelected = "CC";
        ModalPopupTree.Show();  
    }
    protected void btnTo_Click(object sender, EventArgs e)
    {
        ModalPopupTree.TargetControlID = "btnTo";
        ModalPopupExtenderSelected = "To";
        ModalPopupTree.Show();
    }
    protected void btnBCC_Click(object sender, EventArgs e)
    {
        ModalPopupTree.TargetControlID = "btnBCC";
        ModalPopupExtenderSelected = "BCC";
        ModalPopupTree.Show();  
    }

    private void UnCheckedEmployee()
    {
        foreach (GridViewRow gRow in grEmpSearch.Rows)
        {
            CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkSelect");
            chk.Checked = false;
        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
    }
    protected void grTaskAlertList_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            case ("DoubleClick"):

                hfID.Value = grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                txtTo.Text = Common.CheckNullString(grTaskAlertList.SelectedRow.Cells[3].Text.Trim());
                txtCC.Text = Common.CheckNullString(grTaskAlertList.SelectedRow.Cells[4].Text.Trim());
                txtBCC.Text  = Common.CheckNullString(grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[2].ToString());
                txtSubject.Text = Common.CheckNullString(grTaskAlertList.SelectedRow.Cells[5].Text.Trim());
                hfAttach.Value =Common.CheckNullString(grTaskAlertList.SelectedRow.Cells[6].Text.Trim());
                FreetxtTask.Text = Common.CheckNullString(grTaskAlertList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim());
                txtAltertDate.Text = Common.DisplayDate(Common.ReturnDate(grTaskAlertList.SelectedRow.Cells[7].Text.Trim()));

                
            
                ////Assign Attach file name in the check box
                if (hfAttach.Value != "")
                {
                    chkAttachFile.Visible = true;
                    chkAttachFile.Checked = true;
                    chkAttachFile.Text = hfAttach.Value.ToString();
                    chkGroupAttach.Checked = false;
                    chkGroupAttach.Visible = false;
                }
                else
                {
                    chkAttachFile.Visible = false;
                    chkAttachFile.Checked = false;
                    
                }

                string FileName2 = hfAttach.Value.ToString();  
                //string[] arInfo = new string[4];

                //char[] splitter = { '.' };
                //arInfo = Common.str_split(FileName, splitter);

                //string FolderPath = "E:\\PLAN_Attach_File";
                //string FilePath = Server.MapPath(FolderPath + "\\" + FileName);
                //FileUpload1.



                //Get Alert Hour & Minute
                string[] arinfo = new string[4];
                char[] splitter ={ ' ' };

                arinfo  = Common.str_split(grTaskAlertList.SelectedRow.Cells[7].Text.Trim(), splitter);

                string[] arinfo2 = new string[4];
                char[] splitter2 ={ ':' };

                arinfo2 = Common.str_split(arinfo[1], splitter2);
                ddlAlertHour.SelectedValue  = arinfo2[0].ToString();
                ddlAlertMin.SelectedValue = arinfo2[1].ToString();

                arinfo = null;
                arinfo2 = null;
                splitter = null;
                splitter2 = null; 
                this.EntryMode(true);
                TabContainer1.ActiveTabIndex = 0;                
                break;
        }
    }
    protected bool ValidateEmailArray(string[] strEmails, string  strEmailType)
    {
        foreach (string strEmail in strEmails)
        {
            if (this.ValidateEmailID(strEmail) == false)
            {
                lblMsg.Text = strEmailType + " Email ID: " + strEmail + " is Invalid ";
                strEmails = null;
                return false;
            }
        }
        strEmails = null;
        return true;
    }

    protected bool ValidateEmailID(string strEmail)
    {
        string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        System.Text.RegularExpressions.Regex RegX = new System.Text.RegularExpressions.Regex(pattern);
        if(RegX.IsMatch(strEmail))
            return true;
        else
            return false;
    }
    protected void grCompletedAlertList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grCompletedAlertList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;

        switch (_commandName)
        {
            case ("DoubleClick"):

                hfID.Value = grCompletedAlertList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtTo.Text = Common.CheckNullString(grCompletedAlertList.SelectedRow.Cells[3].Text.Trim());
                txtCC.Text = Common.CheckNullString(grCompletedAlertList.SelectedRow.Cells[4].Text.Trim());
                txtBCC.Text = Common.CheckNullString(grCompletedAlertList.DataKeys[_gridView.SelectedIndex].Values[2].ToString());
                txtSubject.Text = Common.CheckNullString(grCompletedAlertList.SelectedRow.Cells[5].Text.Trim());
                hfAttach.Value = Common.CheckNullString(grCompletedAlertList.SelectedRow.Cells[6].Text.Trim());
                FreetxtTask.Text = Common.CheckNullString(grCompletedAlertList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim());
                txtAltertDate.Text = Common.DisplayDate(Common.ReturnDate(grCompletedAlertList.SelectedRow.Cells[7].Text.Trim()));

                //Get Alert Hour & Minute
                string[] arinfo = new string[4];
                char[] splitter ={ ' ' };

                arinfo = Common.str_split(grCompletedAlertList.SelectedRow.Cells[7].Text.Trim(), splitter);

                string[] arinfo2 = new string[4];
                char[] splitter2 ={ ':' };

                arinfo2 = Common.str_split(arinfo[1], splitter2);
                ddlAlertHour.SelectedValue = arinfo2[0].ToString();
                ddlAlertMin.SelectedValue = arinfo2[1].ToString();

                arinfo = null;
                arinfo2 = null;
                splitter = null;
                splitter2 = null;
                this.EntryMode(true);
                TabContainer1.ActiveTabIndex = 0;
                break;
        }
    }
    protected void txtTask_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Session["SUBJECT"] = "";
        Session["TOEMPID"] = "";
        Session["TOEMAILID"] = "";
        Session["SUBJECT"] = "";
    }
}

