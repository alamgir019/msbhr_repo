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
using System.Text;
using System.Net;

public partial class Training_TrainingServiceAgrmnt : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    TrainingManager objTrmMgr = new TrainingManager();
    DataTable dtTS = new DataTable();
    DataTable dtDepartment = new DataTable();

    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DataTable dtEmpInfo = new DataTable();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            //dtTS.Rows.Clear();
            //dtTS.Dispose();
            //Common.EmptyTextBoxValues(this);
            //lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            Common.FillDropDownList_Nil(objMasMgr.SelectTrainingName(0,"Y"), ddlTraining);
            Common.FillDropDownList_Nil(objMasMgr.SelectLearningArea(0), ddlLAreaId);
            Common.FillDropDownList_Nil(objMasMgr.SelectResourcePersonList(0,"Y"), ddlResourcePersonId);
            Common.FillDropDownList_Nil(objMasMgr.SelectCountry(0), ddlCountry);
            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);           
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
            txtRunningRate.Text = Session["USDRATE"].ToString();
            txtRateToUse.Text = Session["USDRATE"].ToString();
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));   
        }
    }

    private void ClearAllControl(string strAllRefresh)
    {
        if (strAllRefresh == "Y")
        {
            this.ClearControl();
            this.ClearEmpInfo();
        }
        else
        {
            this.ClearEmpInfo();
            txtEmpID.Text = "";
        }
        this.EntryMode(false);
    }

    private void OpenRecord()
    {
        grList.Dispose();
        dtTS = objMasMgr.SelectTrainingServiceList(0,txtEmpID.Text.Trim());
        grList.DataSource = dtTS;
        grList.DataBind();
    }

    
    private clsTrainingService BindObject(string maxTrnid,string MaxResourceId)
    {
        clsTrainingService obj = new clsTrainingService();
       
        obj.TraServiceID = hfID.Value;
        obj.EmpId = txtEmpID.Text.Trim();
        obj.TrainType = ddlTrainType.SelectedValue.ToString();
        obj.FiscalYrID = ddlFiscalYr.SelectedValue.ToString();
        obj.TrainingID =(maxTrnid != "" ? maxTrnid : ddlTraining.SelectedValue.ToString());
        obj.LAreaId = ddlLAreaId.SelectedValue.ToString();
        obj.ResourcePersonId = (MaxResourceId != "" ? MaxResourceId : ddlResourcePersonId.SelectedValue.ToString());
        //obj.ResourcePersonId = ddlResourcePersonId.SelectedValue.ToString();
        obj.CountryID = ddlCountry.SelectedValue.ToString();
        obj.ContactDtl = txtContactDtl.Text.Trim();
        obj.TrnStartDate =txtTrnStartDate.Text.Trim();
        obj.TrnEndDate = txtTrnEndDate.Text.Trim();
        obj.Remarks = txtRemarks.Text.Trim();
        obj.NeedType = ddlNeedType.SelectedValue.ToString();
        obj.RunningRate = txtRunningRate.Text.Trim();
        obj.RateToUse = txtRateToUse.Text.Trim();
        obj.ServAgreement = ddlServAgreement.SelectedValue.ToString();

        obj.AgrStartDate =txtAgrStartDate.Text.Trim();
        obj.AgrEndDate =txtAgrEndDate.Text.Trim();
        obj.AgrPeriod = txtAgrPeriod.Text.Trim();
        obj.EstAgrAmtBDT = txtEstAgrAmtBDT.Text.Trim();
        obj.EstAgrAmtUSD = txtEstAgrAmtUSD.Text.Trim();
        obj.ActAgrAmtBDT = txtActAgrAmtBDT.Text.Trim();
        obj.ActAgrAmtUSD = txtActAgrAmtUSD.Text.Trim();
        obj.AgrRemarks = txtAgrRemarks.Text.Trim();

        obj.TrainingCostBDT = txtTrainingCostBDT.Text.Trim();
        obj.TrainingCostUSD = txtTrainingCostUSD.Text.Trim();
        obj.SponsoredBy = ddlSponsoredBy.Text.Trim();
        obj.SCCostPercent = txtSCCostPercent.Text.Trim();
        obj.SCCostBDT = txtSCCostBDT.Text.Trim();
        obj.SCCostUSD = txtSCCostUSD.Text.Trim();
        obj.OtherCostPercent = txtOtherCostPercent.Text.Trim();
        obj.OtherCostPerBDT = txtOtherCostPerBDT.Text.Trim();
        obj.OtherCostPerUSD = txtOtherCostPerUSD.Text.Trim();       
        return obj;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
            this.ClearAllControl("N");
        }
    }

    protected void btnSaveRefresh_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
            this.ClearAllControl("Y");
        }
    }
    protected bool ValidateAndSave()
    {
        try
        {
            if (txtEmpID.Text == "")
            {
                lblMsg.Text = "You have to press find button with this employee id";
                txtEmpID.Focus();
                return false;
            }
            if (ddlTrainType.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select training type.";
                ddlTrainType.Focus();
                return false;
            }
            if ((ddlTraining.SelectedIndex == 0) && (txtTrainingName.Text == ""))
            {
                lblMsg.Text = "Please select training name or type training title.";
                ddlTrainType.Focus();
                return false;
            }
            if (txtTrainingName.Text != "")
            {
                if (Common.CheckDuplicate("TrainingList", "TrainingName", txtTrainingName.Text.Trim(), "", "", false) == true)
                {
                    lblMsg.Text = "This Training Name has Already Exist.";
                    txtTrainingName.Focus();
                    return false;
                }
            }
            if ((ddlResourcePersonId.SelectedIndex == 0) && (txtResourcePerson.Text == ""))
            {
                lblMsg.Text = "Please select resource person name or type title.";
                ddlResourcePersonId.Focus();
                return false;
            }
            if (txtResourcePerson.Text != "")
            {
                if (Common.CheckDuplicate("ResourcePersonList", "ResourcePersonName", txtResourcePerson.Text.Trim(), "", "", false) == true)
                {
                    lblMsg.Text = "This resource person name has already exist.";
                    txtResourcePerson.Focus();
                    return false;
                }
            }
            if (ddlLAreaId.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select learning area.";
                ddlLAreaId.Focus();
                return false;
            }
            if (ddlNeedType.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select need type.";
                ddlServAgreement.Focus();
                return false;
            }
            if (ddlServAgreement.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select service agreement.";
                ddlServAgreement.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtRunningRate.Text) == true)
            {
                lblMsg.Text = "Please mention running rate.";
                txtRunningRate.Focus();
                return false;
            }
            if ((string.IsNullOrEmpty(txtRunningRate.Text) == true) || (txtTrainingCostBDT.Text == "0.00"))
            {
                lblMsg.Text = "Please mention training cost";
                txtTrainingCostBDT.Focus();
                return false;
            }

            if ((string.IsNullOrEmpty(txtTrnStartDate.Text) == false) && (string.IsNullOrEmpty(txtTrnEndDate.Text) == false))
            {
                if (Common.CheckStartEndDate(txtTrnStartDate.Text.Trim(), txtTrnEndDate.Text.Trim()) == true)
                {
                    lblMsg.Text = "Training start Date can not be greater than end date.";
                    txtTrnStartDate.Focus();
                    return false;
                }
            }

            if (hfIsUpdate.Value == "N")
                hfID.Value = objDB.GerMaxIDNumber("TrainingService", "TraServiceID").ToString();
            else
                hfID.Value = Convert.ToInt32(hfID.Value).ToString();

            string strTrainId = ddlTraining.SelectedValue.ToString();
            string strTraServiceID = "";
            if (hfIsUpdate.Value == "N")
                strTraServiceID =  "";
            else
                strTraServiceID = hfID.Value.ToString();
            if (objTrmMgr.IsDuplicateTrainingEntry(txtEmpID.Text.Trim(), strTrainId, Common.ReturnDate(txtTrnStartDate.Text), strTraServiceID) == true)
            {
                lblMsg.Text = "Duplicate training information has found for this staff.";
                txtTrainingName.Focus();
                return false;
            }
            
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            lblMsg.Text = ex.Message;
            throw (ex);
        }
    }

    private void SaveData(string IsDelete)
    {
        //if (hfIsUpdate.Value == "N")
        //    hfID.Value = objDB.GerMaxIDNumber("TrainingService", "TraServiceID").ToString();
        //else
        //    hfID.Value = Convert.ToInt32(hfID.Value).ToString();
        try
        {
            string maxTrnid = "";
            clsCommonSetup objTrainSetup = new clsCommonSetup();
            if (this.txtTrainingName.Text.Trim() != "")
            {
                maxTrnid = Common.getMaxId("TrainingList", "TrainingId");

                objTrainSetup = new clsCommonSetup(maxTrnid, this.txtTrainingName.Text.Trim(), "Y", "N",
                    Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);
                //objTrmMgr.InsertTraining(objTrainSetup, "N", IsDelete);
            }
            string MaxResourceId = "";
            clsCommonSetup objResourceSetup = new clsCommonSetup();
            if (this.txtResourcePerson.Text.Trim() != "")
            {
                MaxResourceId = Common.getMaxId("ResourcePersonList", "ResourcePersonId");

                objResourceSetup = new clsCommonSetup(MaxResourceId, this.txtResourcePerson.Text.Trim(), "Y", "N",
                    Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);
                //objTrmMgr.InsertResourcePerson(objResourceSetup, "N", IsDelete);
            }
            objTrmMgr.InsertTrainingService(this.BindObject(maxTrnid, MaxResourceId), objTrainSetup, objResourceSetup, Session["USERID"].ToString(), hfIsUpdate.Value, IsDelete);
            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
            //Common.EmptyTextBoxValues(this);

            //this.EntryMode(false);
            if (ddlTraining.SelectedIndex == 0)
            {
                Common.FillDropDownList_Nil(objMasMgr.SelectTrainingName(0, "Y"), ddlTraining);
                txtTrainingName.Text = ""; 
            }
            if (ddlResourcePersonId.SelectedIndex == 0)
            {
                Common.FillDropDownList_Nil(objMasMgr.SelectResourcePersonList(0,"Y"), ddlResourcePersonId);
                txtResourcePerson.Text = ""; 
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ClearAllControl("Y");
    }

    private void ClearControl()
    {
        Common.EmptyTextBoxValues(this);
        //this.EntryMode(false);
        this.OpenRecord();        
    }

    private void ClearEmpInfo()
    {
        lblName.Text = "";
        lblJobTitle.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
    }

    protected void grList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grList.PageIndex = e.NewPageIndex;
        OpenRecord();
        TabContainer1.ActiveTabIndex = 1;
    }
   
    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                DataTable dt = objMasMgr.SelectTrainingServiceList(Convert.ToInt32(grList.DataKeys[_gridView.SelectedIndex].Values[0]), grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim());
                
                txtEmpID.Text = dt.Rows[0]["EmpId"].ToString();
                this.FillEmpInfo();
                txtEmpID.ToolTip = dt.Rows[0]["EmpId"].ToString();
                ddlTrainType.SelectedValue = dt.Rows[0]["TrainType"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["FiscalYrID"].ToString()) == false)
                    ddlFiscalYr.SelectedValue = dt.Rows[0]["FiscalYrID"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["TrainingID"].ToString()) == false)
                    ddlTraining.SelectedValue = dt.Rows[0]["TrainingID"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["LAreaId"].ToString()) == false)
                    ddlLAreaId.SelectedValue = dt.Rows[0]["LAreaId"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["ResourcePersonId"].ToString()) == false)
                    ddlResourcePersonId.SelectedValue = dt.Rows[0]["ResourcePersonId"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["CountryID"].ToString()) == false)
                    ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();

                txtContactDtl.Text = dt.Rows[0]["ContactDtl"].ToString();
                txtTrnStartDate.Text = dt.Rows[0]["TrnStartDate"].ToString();
                txtTrnEndDate.Text = dt.Rows[0]["TrnEndDate"].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                ddlNeedType.SelectedValue = dt.Rows[0]["NeedType"].ToString();
                txtRunningRate.Text = Common.RoundDecimal(dt.Rows[0]["RunningRate"].ToString(), 2).ToString();
                txtRateToUse.Text = Common.RoundDecimal(dt.Rows[0]["RateToUse"].ToString(), 2).ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["ServAgreement"].ToString()) == false)
                    ddlServAgreement.SelectedValue = dt.Rows[0]["ServAgreement"].ToString();
               
                    txtAgrStartDate.Text =dt.Rows[0]["AgrStartDate"].ToString();               
                    txtAgrEndDate.Text = dt.Rows[0]["AgrEndDate"].ToString();

                txtAgrPeriod.Text = dt.Rows[0]["AgrPeriod"].ToString();
                txtEstAgrAmtBDT.Text = Common.RoundDecimal(dt.Rows[0]["EstAgrAmtBDT"].ToString(), 2).ToString();
                txtEstAgrAmtUSD.Text = Common.RoundDecimal(dt.Rows[0]["EstAgrAmtUSD"].ToString(), 2).ToString();
                txtActAgrAmtBDT.Text = Common.RoundDecimal(dt.Rows[0]["ActAgrAmtBDT"].ToString(), 2).ToString();
                txtActAgrAmtUSD.Text = Common.RoundDecimal(dt.Rows[0]["ActAgrAmtUSD"].ToString(), 2).ToString();
                txtAgrRemarks.Text = dt.Rows[0]["AgrRemarks"].ToString();

                txtTrainingCostBDT.Text = Common.RoundDecimal(dt.Rows[0]["TrainingCostBDT"].ToString(), 2).ToString();
                txtTrainingCostUSD.Text = Common.RoundDecimal(dt.Rows[0]["TrainingCostUSD"].ToString(), 2).ToString();
                ddlSponsoredBy.Text = dt.Rows[0]["SponsoredBy"].ToString();
                txtSCCostPercent.Text = Common.RoundDecimal(dt.Rows[0]["SCCostPercent"].ToString(), 2).ToString();
                txtSCCostBDT.Text = Common.RoundDecimal(dt.Rows[0]["SCCostBDT"].ToString(), 2).ToString();
                txtSCCostUSD.Text = Common.RoundDecimal(dt.Rows[0]["SCCostUSD"].ToString(), 2).ToString();
                txtOtherCostPercent.Text = Common.RoundDecimal(dt.Rows[0]["OtherCostPercent"].ToString(), 2).ToString();
                txtOtherCostPerBDT.Text = Common.RoundDecimal(dt.Rows[0]["OtherCostPerBDT"].ToString(), 2).ToString();
                txtOtherCostPerUSD.Text = Common.RoundDecimal(dt.Rows[0]["OtherCostPerUSD"].ToString(), 2).ToString();

                hfID.Value = dt.Rows[0]["TraServiceID"].ToString();

                if (dt.Rows[0]["ServAgreement"].ToString() == "Y")
                    ddlServAgreement_SelectedIndexChanged(null, null);

                this.TabContainer1.ActiveTabIndex = 0;

                this.EntryMode(true);
                break;
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
            lblMsg.Text = "Select a Training Service first from the list then try to delete.";
        }
        this.OpenRecord();
        this.EntryMode(false);
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.FillEmpInfo();
    }

    private void FillEmpInfo()
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No .";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblJobTitle.Text = dRow["JobTitleName"].ToString().Trim();
                lblSector.Text = dRow["SectorName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }
    protected void ddlServAgreement_SelectedIndexChanged(object sender, EventArgs e)
    {
        string txtAgree = this.ddlServAgreement.SelectedItem.Value.ToString().Trim();
        if (txtAgree == "Y")
            PanelAgreement.Visible = true;
        else
            PanelAgreement.Visible = false;
    }

    protected void txtActAgrAmtBDT_TextChanged(object sender, EventArgs e)
    {
        this.txtActAgrAmtUSD.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtActAgrAmtBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtActAgrAmtUSD_TextChanged(object sender, EventArgs e)
    {
        this.txtActAgrAmtBDT.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtActAgrAmtUSD.Text) * Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtTrainingCostBDT_TextChanged(object sender, EventArgs e)
    {
        //this.txtTrainingCostUSD.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtTrainingCostBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();

        //DateTime dtTrnStartDt = new DateTime();
        //DateTime dtAgrStartDt = new DateTime();
        //DateTime dtAgrEndDt = new DateTime();

        //char[] splitter = { '/' };
        //string[] arinfo = Common.str_split(txtTrnEndDate.Text, splitter);

        //if (arinfo.Length == 3)
        //    dtTrnStartDt = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
        //else
        //{
        //    char[] splitter2 = { '-' };
        //    string[] arinfo2 = Common.str_split(txtTrnEndDate.Text, splitter2);
        //    dtTrnStartDt = Convert.ToDateTime(arinfo2[2] + "-" + arinfo2[1] + "-" + arinfo2[0]);
        //}
        
        //dtAgrStartDt = dtTrnStartDt.AddDays(1);
        //txtAgrStartDate.Text = Common.DisplayDate(dtAgrStartDt.ToString() ); 
        //if ((Convert.ToInt16(txtTrainingCostBDT.Text) >= 1000) && (Convert.ToInt16(txtTrainingCostBDT.Text) <= 2000))
        //{
        //    dtAgrEndDt = dtAgrStartDt.AddMonths(6);
        //    txtAgrPeriod.Text = "180"; 
        //}
        //else if ((Convert.ToInt16(txtTrainingCostBDT.Text) >= 2001) && (Convert.ToInt16(txtTrainingCostBDT.Text) <= 3000))
        //{
        //    dtAgrEndDt = dtAgrStartDt.AddMonths(9);
        //    txtAgrPeriod.Text = "270";
        //}
        //else
        //{
        //    dtAgrEndDt = dtAgrStartDt.AddMonths(12);
        //    txtAgrPeriod.Text = "360";
        //}
        //txtAgrEndDate.Text = Common.DisplayDate(dtAgrEndDt.ToString());
        //txtEstAgrAmtBDT.Text = txtTrainingCostBDT.Text; 
        //txtEstAgrAmtUSD.Text = txtTrainingCostUSD.Text; 
    }
    protected void txtTrainingCostUSD_TextChanged(object sender, EventArgs e)
    {
        this.txtTrainingCostBDT.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtTrainingCostUSD.Text) * Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
        txtEstAgrAmtBDT.Text = txtTrainingCostBDT.Text; 
    }
    protected void txtSCCostBDT_TextChanged(object sender, EventArgs e)
    {
        this.txtSCCostUSD.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtSCCostBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtSCCostUSD_TextChanged(object sender, EventArgs e)
    {
        this.txtSCCostBDT.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtSCCostUSD.Text) * Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtOtherCostPerBDT_TextChanged(object sender, EventArgs e)
    {
        this.txtOtherCostPerUSD.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtOtherCostPerBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtOtherCostPerUSD_TextChanged(object sender, EventArgs e)
    {
        this.txtOtherCostPerBDT.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtOtherCostPerUSD.Text) * Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }
    protected void txtAgrPeriod_TextChanged(object sender, EventArgs e)
    {
        double txtPeriod = Convert.ToDouble("0" + txtAgrPeriod.Text);
        double txtBDT = Convert.ToDouble(Common.RoundDecimal((Convert.ToDouble("0" + txtRunningRate.Text) * txtPeriod).ToString(), 2));
        this.txtEstAgrAmtBDT.Text = txtBDT.ToString();
        this.txtEstAgrAmtUSD.Text = Common.RoundDecimal((txtBDT / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }

    protected void txtSCCostPercent_TextChanged(object sender, EventArgs e)
    {        
        this.txtSCCostBDT.Text = Common.RoundDecimal(((Convert.ToDouble(txtTrainingCostBDT.Text) * Convert.ToDouble(txtSCCostPercent.Text)) / 100).ToString(), 2).ToString();
        this.txtSCCostUSD.Text = Common.RoundDecimal((Convert.ToDouble(txtSCCostBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }

    protected void txtOtherCostPercent_TextChanged(object sender, EventArgs e)
    {
        this.txtOtherCostPerBDT.Text = Common.RoundDecimal(((Convert.ToDouble(txtTrainingCostBDT.Text) * Convert.ToDouble(txtOtherCostPercent.Text)) / 100).ToString(), 2).ToString();
        this.txtOtherCostPerUSD.Text = Common.RoundDecimal((Convert.ToDouble(txtOtherCostPerBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();
    }

    protected void lnkOpenPPDF_Click(object sender, EventArgs e)
    {  
        try
        {  
            DateTime dtCurrYear = DateTime.Now;
            int iCurrYear = dtCurrYear.Year;

            string FileName = txtEmpID.Text.Trim() + ".pdf";

            string FolderPath = ConfigurationManager.AppSettings["TrainingPath"];
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

    //protected void ddlResourcePersonId_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (this.ddlResourcePersonId.SelectedItem.Value.ToString().Trim() != "99999")       
    //        this.txtResourcePerson.ReadOnly = true;        
    //    else
    //        this.txtResourcePerson.ReadOnly = false;
    //}
    //protected void ddlTraining_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (this.ddlTraining.SelectedItem.Value.ToString().Trim() != "99999")        
    //        this.txtTrainingName.ReadOnly = true;        
    //    else
    //        this.txtTrainingName.ReadOnly = false;
    //}
    //protected void ddlTrainType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (this.ddlTrainType.SelectedIndex.ToString ()== "-1")
    //        this.ddlCountry.Enabled = true;
    //    else
    //    {
    //        this.ddlCountry.SelectedIndex = -1;
    //        this.ddlCountry.Enabled = false;
    //    }
    //}



    protected void ddlTrainType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        //this.txtTrainingCostUSD.Text = Common.RoundDecimal((Convert.ToDouble("0" + txtTrainingCostBDT.Text) / Convert.ToDouble(Session["USDRATE"].ToString())).ToString(), 2).ToString();

        DateTime dtTrnStartDt = new DateTime();
        DateTime dtAgrStartDt = new DateTime();
        DateTime dtAgrEndDt = new DateTime();

        if( (string.IsNullOrEmpty(txtTrnEndDate.Text) == false) && string.IsNullOrEmpty( txtTrainingCostUSD.Text)==false )
        {
            char[] splitter = { '/' };
            string[] arinfo = Common.str_split(txtTrnEndDate.Text, splitter);

            if (arinfo.Length == 3)
                dtTrnStartDt = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
            else
            {
                char[] splitter2 = { '-' };
                string[] arinfo2 = Common.str_split(txtTrnEndDate.Text, splitter2);
                dtTrnStartDt = Convert.ToDateTime(arinfo2[2] + "-" + arinfo2[1] + "-" + arinfo2[0]);
            }

            dtAgrStartDt = dtTrnStartDt.AddDays(1);
            txtAgrStartDate.Text = Common.DisplayDate(dtAgrStartDt.ToString());
            if ((Convert.ToInt16(txtTrainingCostUSD.Text) >= 1000) && (Convert.ToInt16(txtTrainingCostUSD.Text) <= 2000))
            {
                dtAgrEndDt = dtAgrStartDt.AddMonths(6);
                txtAgrPeriod.Text = "180";
            }
            else if ((Convert.ToInt16(txtTrainingCostUSD.Text) >= 2001) && (Convert.ToInt16(txtTrainingCostUSD.Text) <= 3000))
            {
                dtAgrEndDt = dtAgrStartDt.AddMonths(9);
                txtAgrPeriod.Text = "270";
            }
            else
            {
                dtAgrEndDt = dtAgrStartDt.AddMonths(12);
                txtAgrPeriod.Text = "360";
            }
            txtAgrEndDate.Text = Common.DisplayDate(dtAgrEndDt.ToString());
            txtEstAgrAmtBDT.Text = txtTrainingCostBDT.Text;
            txtEstAgrAmtUSD.Text = txtTrainingCostUSD.Text;
        }
    }
}

