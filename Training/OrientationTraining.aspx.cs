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

public partial class Training_OrientationTraining : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtSBU = new DataTable();
    DataTable dtDepartment = new DataTable();

    TrainingManager objTM = new TrainingManager();
    DataTable dtTN = new DataTable();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DataTable dtEmpInfo = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtSBU.Rows.Clear();
            dtSBU.Dispose();           
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord(txtEmpID.Text.Trim());
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
            this.ClearControl();
        }
    }

    private void OpenRecord(string empid)
    {
        DataTable dt = objTM.SelectOrientationTraining(empid);
        if (dt.Rows.Count > 0)
        {
            grOriTrain.DataSource = dt;
            grOriTrain.DataBind();

            foreach (GridViewRow gRow in grOriTrain.Rows)
            {
                if (gRow.Cells[1].Text == "Y")
                    gRow.Cells[1].Text = "Yes";
                else
                    gRow.Cells[1].Text = "No";

                if (gRow.Cells[3].Text == "Y")
                    gRow.Cells[3].Text = "Yes";
                else
                    gRow.Cells[3].Text = "No";

                if (gRow.Cells[4].Text == "Y")
                    gRow.Cells[4].Text = "Yes";
                else
                    gRow.Cells[4].Text = "No";

            }
        }
        else
        {
            grOriTrain.DataSource = null;
            grOriTrain.DataBind();
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

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            StringBuilder strSBUID = new StringBuilder();

            MasterTablesManager MasMgr = new MasterTablesManager();
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("OrientationTraining", "OriTrainingID");
            else
                lngID = Convert.ToInt32(hfID.Value);
            string strFirstDayOri=this.chkFDayOrien.Checked==true? "Y" : "N" ;
            string strFirstDayOriDate=this.txtFDayOTDate.Text;
            string strAngChiSeftyPol=this.chkAnCSPori.Checked==true? "Y" : "N" ;
            string strOT=this.chkOT.Checked==true? "Y" : "N" ;
            string strOTDate=this.txtOTDate.Text;
            string strRemark = this.txtRemarks.Text;

            objTM.InsertOrientaTraining(lngID.ToString(), txtEmpID.Text.Trim(), strFirstDayOri, strFirstDayOriDate,strAngChiSeftyPol,
                      strOT, strOTDate, strRemark, IsDelete, Session["USERID"].ToString(), hfIsUpdate.Value.ToString());

            lblMsg.Text=Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
            //Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord(txtEmpID.Text.Trim());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
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
            if ((string.IsNullOrEmpty(txtFDayOTDate.Text) == false) && (string.IsNullOrEmpty(txtOTDate.Text) == false))
            {
                if (Common.CheckStartEndDate(txtFDayOTDate.Text.Trim(), txtOTDate.Text.Trim()) == true)
                {
                    lblMsg.Text = "Start Date can not be greater than end date.";
                    txtFDayOTDate.Focus();
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblName.Text = "";
        lblJobTitle.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord(txtEmpID.Text.Trim());
        grOriTrain.DataSource = null;
        grOriTrain.DataBind();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {
        chkFDayOrien.Checked = false;
        chkAnCSPori.Checked = false;
        chkOT.Checked = false;
        txtFDayOTDate.Text = "";
        txtOTDate.Text = "";
        txtRemarks.Text = "";
    }

    protected void grOriTrain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        { 
            case ("DoubleClick"):
                    this.chkFDayOrien.Checked=grOriTrain.DataKeys[_gridView.SelectedIndex].Values[1].ToString()== "Y" ? true:false;
                    
                    if (grOriTrain.DataKeys[_gridView.SelectedIndex].Values[2].ToString() != "")
                    {
                        this.txtFDayOTDate.Text =Convert.ToDateTime(grOriTrain.DataKeys[_gridView.SelectedIndex].Values[2].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        this.txtFDayOTDate.Text = "";
                    }
                    this.chkAnCSPori.Checked=grOriTrain.DataKeys[_gridView.SelectedIndex].Values[3].ToString()== "Y" ? true:false;
                    this.chkOT.Checked=grOriTrain.DataKeys[_gridView.SelectedIndex].Values[4].ToString()== "Y" ? true:false;


                    if (grOriTrain.DataKeys[_gridView.SelectedIndex].Values[5].ToString() != "")
                    {
                        this.txtOTDate.Text =Convert.ToDateTime(grOriTrain.DataKeys[_gridView.SelectedIndex].Values[5].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        this.txtOTDate.Text = "";

                    }
                this.txtRemarks.Text = grOriTrain.DataKeys[_gridView.SelectedIndex].Values[6].ToString();
                    hfID.Value = grOriTrain.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
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
            lblMsg.Text = "Select a training first from the list then try to delete.";
        }
        this.OpenRecord(txtEmpID.Text.Trim());
        this.EntryMode(false);
    }
}

