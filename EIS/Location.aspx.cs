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
    public partial class LocationSetup : System.Web.UI.Page
    {
       // String strName;
        DBConnector objDB = new DBConnector();    
        MasterTablesManager objMasMgr = new MasterTablesManager();
        DataTable dtLocation = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfIsUpadate.Value = "N";
                dtLocation.Rows.Clear();
                dtLocation.Dispose();
                grLocation.DataSource = null;
                grLocation.DataBind();
                Common.EmptyTextBoxValues(this);
                lblMsg.Text = "";
                this.EntryMode(false);
                this.OpenRecord();
             
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
                //grEmpType.DataSource =
                //grEmpType.DataBind();
        }

        protected void EntryMode(bool IsUpdate)
        {
            if (IsUpdate == true)
            {
                btnSave.Text = "Update";
                hfIsUpadate.Value = "Y";
            }
            else
            {
                btnSave.Text = "Save";
                hfIsUpadate.Value = "N";
            }
        }

        private void OpenRecord()
        {                   
            dtLocation = objMasMgr.SelectLocation(0); 
            grLocation.DataSource = dtLocation;
            grLocation.DataBind();
        }
        private void SaveData(string IsDelete)
        {
            long lngID=0;
            try
            {
                MasterTablesManager MasMgr = new MasterTablesManager();
                //Filling Class Properties with values
                if (hfIsUpadate.Value == "N")
                    lngID = objDB.GerMaxIDNumber("PostingPlaceList", "PostingPlaceId");
                else
                    lngID = Convert.ToInt32(hfID.Value);

                Location objLoc = new Location(lngID.ToString(), txtLocation.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N");

                MasMgr.InsertLocation(objLoc, hfIsUpadate.Value, IsDelete, chkInActive.Checked == true ? "N" : "Y");

                if ( hfIsUpadate.Value  == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";               
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

      

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData("N");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);            
            this.OpenRecord();
        }
        protected void grLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grLocation.PageIndex = e.NewPageIndex;
            this.OpenRecord();
        }
        protected void grLocation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;
            switch (_commandName)
            {
                case ("DoubleClick"):

                    txtLocation.Text = grLocation.SelectedRow.Cells[1].Text;
                    //txtLocCode.Text = Common.CheckNullString(grLocation.SelectedRow.Cells[2].Text.Trim());
                    chkInActive.Checked = grLocation.SelectedRow.Cells[2].Text == "N" ? true : false;
                    hfID.Value = grLocation.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    this.EntryMode(true);
                    break;
            }
        }
        protected void grLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfID.Value) == false)
            {
                this.SaveData("Y");
                lblMsg.Text = "Record Deleted Successfully";
            }
            else
            {
                lblMsg.Text = "Select a record first then try to delete.";
            }
           
            this.EntryMode(false);
        }
}
