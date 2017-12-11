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
using System.Net;

public partial class Appraisal_PerformanceAppraisal : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    AppraisalManager objAppMgr = new AppraisalManager();

    DataTable dtEmpInfo = new DataTable();
    
    DataTable dtAppDet=new DataTable();
    DataTable dtAppMstGr = new DataTable();

    dsAppraisal objdsAPAAc = new dsAppraisal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "F"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlTaskType );
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlTask);
            Common.FillDropDownList_Nil(objMasMgr.SelectDivision(0), ddlGrade );
            TabContainer1.ActiveTabIndex = 0;
            ddlFiscalYr.SelectedIndex = 2;
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
            this.ClearControls();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }        
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDesignation.Text = "";
       
        lblJoiningDate.Text = "";
        
        
     
        grPerformance.DataSource = null;
        grPerformance.DataBind();
    }

    private void ClearSubControls()
    {
        txtActivityName.Text = "";
        
        
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee Id .";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblJoiningDate.Text = dRow["JoiningDate"].ToString().Trim();
                
                //lblSuperId.Text = dRow["SupervisorId"].ToString().Trim();
                //lblSuperName.Text = objEmpMgr.GetSupervisorName(dRow["SupervisorId"].ToString().Trim());
            }
            
            OpenRecordAppraisalMst();
            grPerformance.DataSource = null;
            grPerformance.DataBind();
        }

       
    }

    private void OpenRecordAppraisalMst() {

        string strEmpID = txtEmpID.Text.Trim();
         
        dtAppMstGr = objAppMgr.SelectAppraisalMstGrd( strEmpID);

        if (dtAppMstGr.Rows.Count > 0)
        {
            grAppraisalMstList.DataSource = dtAppMstGr;
            grAppraisalMstList.DataBind();
        }
        else
        {
            grAppraisalMstList.DataSource = null;
            grAppraisalMstList.DataBind();

        } 
    }
   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
        this.EntryMode(false);
        this.ClearControls();
        this.ClearSubControls();
        lblMsg.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.AddToEmpGridView();
        this.ClearSubControls();
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void AddToEmpGridView()
    {
        //bool DataExist = false;
        if (grPerformance.Rows.Count == 0)
        {
            if (string.IsNullOrEmpty(txtActivityName.Text.Trim()) == false)
            {
                DataRow nRow = objdsAPAAc.dtAPAActivity.NewRow();
                nRow["ActivityName"] = txtActivityName.Text.Trim();
                //nRow["ActivityDesc"] = txtDesc.Text.Trim();                
                objdsAPAAc.dtAPAActivity.Rows.Add(nRow);
            }
        }
        else
        {
            int i = 0;
            foreach (GridViewRow gRow in grPerformance.Rows)
            {
                DataRow nRow = objdsAPAAc.dtAPAActivity.NewRow();
                if (gRow.Cells[1].Text.Trim() != txtActivityName.Text.Trim())
                {
                    nRow["ActivityName"] = gRow.Cells[1].Text.Trim();
                    nRow["ActivityDesc"] = gRow.Cells[2].Text.Trim();                    
                    objdsAPAAc.dtAPAActivity.Rows.Add(nRow);
                }
                i++;
            }

            DataRow nRow2 = objdsAPAAc.dtAPAActivity.NewRow();
            nRow2["ActivityName"] = txtActivityName.Text.Trim();
            //nRow2["ActivityDesc"] = txtDesc.Text.Trim();



            objdsAPAAc.dtAPAActivity.Rows.Add(nRow2);
        }
        objdsAPAAc.dtAPAActivity.AcceptChanges();
        grPerformance.DataSource = objdsAPAAc.dtAPAActivity;
        grPerformance.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
            
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (ddlFiscalYr.SelectedIndex ==-1)
            {
                lblMsg.Text = "Please select fiscal year.";
                ddlFiscalYr.Focus();
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
    private void SaveData()
    {
        try
        {
            string strAppraisalDate = "";
            string strTerm = "";
            if (string.IsNullOrEmpty(txtAppraisalDate.Text.Trim()) == false)
                strAppraisalDate = Common.ReturnDate(txtAppraisalDate.Text.Trim());

            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("AppraisalMst", "AppId");
            


              //objAppMgr.InsertAppraisal(hfId.Value.ToString(), txtEmpID.Text.Trim(), strEntryDate, ddlFiscalYr.SelectedValue.ToString(), strTerm, txtTotalRating.Text, txtOverallRating.Text ,
              //txtRemarks.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), grPerformance, hfIsUpdate.Value.ToString());
           
            //if (hfIsUpdate.Value == "N")
            //    lblMsg.Text = "Record Saved Successfully";
            //else
            //    lblMsg.Text = "Record Updated Successfully";

              clsPerformanceAppraisal objPAppraisal = new clsPerformanceAppraisal();
              objPAppraisal.AppId = hfId.Value;
              objPAppraisal.EmpId = txtEmpID.Text.Trim();
              objPAppraisal.EntryDate = strAppraisalDate;
              objPAppraisal.FiscalYrId = ddlFiscalYr.SelectedValue.ToString();
              objPAppraisal.IsMidTerm = strTerm;      
                       
              objPAppraisal.InsertedBy = Session["USERID"].ToString();
              objPAppraisal.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());
              objAppMgr.InsertAppraisal(objPAppraisal,grPerformance, hfIsUpdate.Value.ToString());

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), "N");
            this.EntryMode(false);
            this.ClearControls();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

  
    protected void grActivityList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grActivityList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                foreach (GridViewRow gRow in grPerformance.Rows)
                {
                    if (grPerformance.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() != grPerformance.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim())
                    {
                        DataRow nRow = objdsAPAAc.dtAPAActivity.NewRow();
                        nRow["ActivityName"] = grPerformance.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();
                        nRow["ActivityDesc"] = gRow.Cells[2].Text.Trim();
                        nRow["Rating"] = gRow.Cells[3].Text.Trim();
                        objdsAPAAc.dtAPAActivity.Rows.Add(nRow);
                        objdsAPAAc.dtAPAActivity.AcceptChanges();
                    }                    
                }
                grPerformance.DataSource = objdsAPAAc.dtAPAActivity;
                grPerformance.DataBind();
               break;


        }

    }
    protected void grAppraisalMstList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):


                hfId.Value = grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();

                txtEmpID.Text = grAppraisalMstList.SelectedRow.Cells[1].Text;
               // Convert.ToDateTime(grPerformance.DataKeys[_gridView.SelectedIndex].Values[2].ToString()).ToString("dd/MM/yyyy");
                //if (txtConfirmDate.Text!="")
                //    txtConfirmDate.Text = Convert.ToDateTime(grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[3].ToString()).ToString("dd/MM/yyyy");

               // txtEntryDate.Text = Common.CheckNullString(grAppraisalMstList.SelectedRow.Cells[2].Text.Trim());
                ddlFiscalYr.SelectedValue = grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                if (grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "Y")
                {
                    txtActivityName.Enabled = false;
                             
                    btnAdd.Enabled = false;                   
                }
                else if (grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "N")
                {                   
                    txtActivityName.Enabled = true;
                        
                    btnAdd.Enabled = true;
                }
                
            //AppraisalDetail;
         hfId.Value = grAppraisalMstList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
        dtAppDet = objAppMgr.SelectAppraisalDet(Convert.ToInt32(hfId.Value));
        if (dtAppDet.Rows.Count > 0)
        {
            grPerformance.DataSource = dtAppDet;
            grPerformance.DataBind();
        }
        else
        {
            grPerformance.DataSource = null;
            grPerformance.DataBind();

        }
        this.EntryMode(true);
                break;
           


        }

    }
    protected void lnkOpenPDF_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dtCurrYear = DateTime.Now;
            int iCurrYear = dtCurrYear.Year;

            string FileName = txtEmpID.Text.Trim() + ".pdf";

            string FolderPath = ConfigurationManager.AppSettings["AppraisalPath"];
            string FilePath = Server.MapPath(FolderPath + "/" + iCurrYear + "/" + FileName);

            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "File not found.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Font.Bold = true;
        }
    }
}
