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
public partial class Training_TrainingNeedTypeSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    TrainingManager objTM = new TrainingManager();
    DataTable dtTN = new DataTable();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DataTable dtEmpInfo = new DataTable();
   // DataTable dtSubTypw = new DataTable();//objTM.TrnSubTypeListByType(typeID);
            //  dtSubTypw=objTM.TrnSubTypeListByType();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtTN.Rows.Clear();
            dtTN.Dispose();
            grTrainingNeed.DataSource = null;
            grTrainingNeed.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            Common.FillDropDownList_Nil(objTM.SelectTrainNeedTierList(), ddlTrnneedTier);

            Common.FillDropDownList_Nil(objTM.SelectTrnNeedTypeList(0), ddltrnNeedType);
            Common.FillDropDownList_Nil(objTM.SelectTrainMode(), ddlTrnMode);
            Common.FillYearList(5, ddlTrnNeedYear);
            ddlTrnNeedYear.SelectedValue= System.DateTime.Now.ToString("yyyy");
            this.OpenRecord("0");
        }
    }

    private void loadDDlSubType(int typeID)
    {
        ddlTrnNeedSubType.Items.Clear();
        DataTable dt = (DataTable)objTM.TrnSubTypeListByType(typeID);  

        if (dt.Rows.Count > 0)
        {
            ddlTrnNeedSubType.DataSource = dt;
            ddlTrnNeedSubType.DataValueField = "TrainingSubTypeId";
            ddlTrnNeedSubType.DataTextField = "TrainingSubTypeName";
            ddlTrnNeedSubType.DataBind();
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
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
            this.OpenRecord(txtEmpID.Text.Trim());
        }
    }
    private void OpenRecord(string empID)
    {
        dtTN = objTM.SelectTrainingNeedList(empID);
        grTrainingNeed.DataSource = dtTN;
        grTrainingNeed.DataBind();
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
        ddlTrnneedTier.SelectedIndex = -1;
        ddltrnNeedType.SelectedIndex = -1;
        ddlTrnNeedSubType.SelectedIndex = -1;
        ddlTrnMode.SelectedIndex = -1;
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
            if (txtEmpID.Text.Trim() == "")
            {
                lblMsg.Text = "Please Employee ID.";
                txtEmpID.Focus();
                return false;
            }
            if (ddlTrnneedTier.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Training Need Tier From The List.";
                ddlTrnneedTier.Focus();
                return false;
            }
            if (ddltrnNeedType.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Training Need Type From The List.";
                ddltrnNeedType.Focus();
                return false;
            }
            if (ddlTrnMode.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Training Mode From The List.";
                ddlTrnMode.Focus();
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
        long lngID = 0;
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("TrainingNeed", "TrainingNeedID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            objTM.InsertTrnNeedSubType(lngID.ToString(), txtEmpID.Text.Trim(), ddlTrnneedTier.SelectedValue.ToString(), ddltrnNeedType.SelectedValue.ToString(),
                  ddlTrnNeedSubType.SelectedValue.ToString(), ddlTrnMode.SelectedValue.ToString(),ddlTrnNeedYear.SelectedValue.ToString(),
                  IsDelete, Session["USERID"].ToString(), hfIsUpdate.Value.ToString());

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
            this.EntryMode(false);
            this.OpenRecord(txtEmpID.Text.Trim());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblName.Text = "";
        lblJobTitle.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";        
        lblMsg.Text = "";
        this.EntryMode(false);
        Common.EmptyTextBoxValues(this);
        this.ClearControl();
        grTrainingNeed.DataSource = null;
        grTrainingNeed.DataBind();
    }

    private void ClearControl()
    {
        ddlTrnneedTier.SelectedIndex = -1;
        ddltrnNeedType.SelectedIndex = -1;
        ddlTrnNeedSubType.SelectedIndex = -1;
        ddlTrnMode.SelectedIndex = -1;
        ddlTrnNeedYear.SelectedIndex = -1;        
    }

    protected void grPayCharging_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfID.Value = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ddlTrnneedTier.SelectedValue = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                ddltrnNeedType.SelectedValue = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                if (grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[3].ToString() != "")
                { this.loadDDlSubType(Convert.ToInt32(grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[2].ToString())); }
                
                ddlTrnNeedSubType.SelectedValue = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[3].ToString();

                ddlTrnMode.SelectedValue = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                ddlTrnNeedYear.SelectedValue = grTrainingNeed.DataKeys[_gridView.SelectedIndex].Values[5].ToString();
                
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
            lblMsg.Text = "Select a payment charging first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void ddltrnNeedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.loadDDlSubType(Convert.ToInt32(this.ddltrnNeedType.SelectedItem.Value));
    }
}
