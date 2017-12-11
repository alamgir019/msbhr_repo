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
using System.IO;

public partial class EmpFamilyInfo : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    //dsEmpTraining objDsT = new dsEmpTraining();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    EmpFamilyManager objEmpFaml = new EmpFamilyManager();
    DataTable dtEmpInfo = new DataTable();
    DataTable dtEmpFamilyInfo = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtEmpFamilyInfo.Rows.Clear();
            dtEmpFamilyInfo.Dispose();
            grEmpFamilyInfo.DataSource = null;
            grEmpFamilyInfo.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            int intFmID = grEmpFamilyInfo.Rows.Count + 1;
            hfFmId.Value = intFmID.ToString() ;

        }
    }
    private void OpenRecord()
    {
        dtEmpFamilyInfo = objEmpFaml.SelectEmpFamily(txtEmpID.Text);
        grEmpFamilyInfo.DataSource = dtEmpFamilyInfo;
        grEmpFamilyInfo.DataBind();
        foreach (GridViewRow gRow in grEmpFamilyInfo.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Common.CheckNullString(gRow.Cells[7].Text)!="")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
            if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        grEmpFamilyInfo.DataSource = null;
        grEmpFamilyInfo.DataBind();
    }
    protected void cmdFind_Click(object sender, EventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHR(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "This Employee Id is not vaild.";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow tt in dtEmpInfo.Rows)
            {
                txtEmpFullName.Text = tt["FullName"].ToString();
            }
            this.OpenRecord();
        }

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
            hfIsUpadate.Value = "N";
            hfFmId.Value = "";
            txtName.Text = "";
            txtOccupation.Text = "";
            ddlRelation.SelectedIndex = 0;
            txtDob.Text = "";
            ddlBloodGroup.SelectedIndex = 0;
            txtInsID.Text = "";
            txtIncusionDate.Text = "";
            txtRenewalDate.Text = "";
            hfFmImage.Value = "";
            imgEmp.ImageUrl = "~/EmployeeImage/NoImage.jpg";
        }
    }
    protected void SaveData(string IsDelete)
    {
        int intFmID = grEmpFamilyInfo.Rows.Count+1;
        if (hfIsUpadate.Value == "N")
            hfFmId.Value = intFmID.ToString() ;
        clsEmpFamilyInfo objFam = new clsEmpFamilyInfo(txtEmpID.Text, hfFmId.Value, txtName.Text,
        txtOccupation.Text, ddlRelation.SelectedValue, txtDob.Text, ddlBloodGroup.SelectedValue,
        chkIsDependant.Checked == true ? "Y" : "N",
        Session["USERID"].ToString(), Common.SetDate(DateTime.Now.ToString()),
        txtInsID.Text.Trim(),txtIncusionDate.Text.Trim(),txtRenewalDate.Text.Trim(),hfFmImage.Value.ToString());
        
        EmpFamilyManager objFamilyMgr = new EmpFamilyManager();
        objFamilyMgr.InsertEmpFamily(objFam, hfIsUpadate.Value, IsDelete);
         if ( hfIsUpadate.Value  == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";          
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void grEmpFamilyInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfFmId.Value = grEmpFamilyInfo.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                txtName.Text =Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[2].Text);
                ddlRelation.Text = Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[3].Text);
                if (Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[4].Text.Trim()) != "")
                    txtDob.Text = grEmpFamilyInfo.SelectedRow.Cells[4].Text;
                chkIsDependant.Checked = (grEmpFamilyInfo.SelectedRow.Cells[5].Text=="Y" ? true:false)  ;
                txtInsID.Text = Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[6].Text.Trim());
                if (Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[7].Text.Trim()) != "")
                    txtIncusionDate.Text = grEmpFamilyInfo.SelectedRow.Cells[7].Text.Trim();
                if (Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[8].Text.Trim()) != "")
                    txtRenewalDate.Text = grEmpFamilyInfo.SelectedRow.Cells[8].Text.Trim();
                hfFmImage.Value =Common.CheckNullString(grEmpFamilyInfo.SelectedRow.Cells[9].Text.Trim());

                if (string.IsNullOrEmpty(hfFmImage.Value) == false)
                {
                    imgEmp.ImageUrl = "~/EmployeeImage/" + hfFmImage.Value;
                    //hfEmpImage.Value = string.IsNullOrEmpty(tt["EmpPicLoc"].ToString().Trim()) == true ? "" : tt["EmpPicLoc"].ToString().Trim();
                }
                else
                {
                    imgEmp.ImageUrl = "~/EmployeeImage/NoImage.jpg";
                    hfFmImage.Value = "";
                }
                this.EntryMode(true);
                break;
            case ("RowDeleting"):
                clsEmpFamilyInfo objFam = new clsEmpFamilyInfo(grEmpFamilyInfo.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(),
                grEmpFamilyInfo.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim(), grEmpFamilyInfo.SelectedRow.Cells[2].Text,
                grEmpFamilyInfo.SelectedRow.Cells[3].Text, grEmpFamilyInfo.SelectedRow.Cells[4].Text,
                grEmpFamilyInfo.SelectedRow.Cells[5].Text, grEmpFamilyInfo.SelectedRow.Cells[6].Text,
                grEmpFamilyInfo.SelectedRow.Cells[7].Text, Session["USERID"].ToString(), Common.SetDate(DateTime.Now.ToString()), txtInsID.Text.Trim(), 
                txtIncusionDate.Text.Trim(), txtRenewalDate.Text.Trim(), hfFmImage.Value.ToString());

                EmpFamilyManager objMgr = new EmpFamilyManager();
                objMgr.InsertEmpFamily(objFam, "N", "Y");
                if (string.IsNullOrEmpty(hfFmImage.Value) == false)
                {
                    string FolderPath = ConfigurationManager.AppSettings["EmployeeImagePath"];
                    string FileName = hfFmImage.Value.ToString();
                    //string Extension = Path.GetExtension(FileName);
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileInfo File = new FileInfo(FilePath);
                    File.Delete();
                    imgEmp.ImageUrl = "";
                    hfFmImage.Value = "";
                }
                this.EntryMode(false);
                this.OpenRecord();
                break;
        }
    }
    protected void txtPassingYear_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (hfIsUpadate.Value == "N")
            hfFmId.Value = Convert.ToString(grEmpFamilyInfo.Rows.Count + 1);

        if (FileUpload1.HasFile)
        {
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FileName = txtEmpID.Text.Trim() +hfFmId.Value.ToString()+ Extension;
            string[] arInfo = new string[4];

            char[] splitter = { '.' };
            arInfo = Common.str_split(FileName, splitter);

            string FolderPath = ConfigurationManager.AppSettings["EmployeeImagePath"];
            string FilePath = Server.MapPath(FolderPath + "/" + FileName);
            FileUpload1.SaveAs(FilePath);
            imgEmp.ImageUrl = "~/EmployeeImage/" + FileName;
            hfFmImage.Value = FileName;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string FolderPath = ConfigurationManager.AppSettings["EmployeeImagePath"];
        string FileName = hfFmImage.Value.ToString();
        //string Extension = Path.GetExtension(FileName);
        string FilePath = Server.MapPath(FolderPath + FileName);
        FileInfo File = new FileInfo(FilePath);
        File.Delete();
        imgEmp.ImageUrl = "";
        hfFmImage.Value = "";
    }
}
