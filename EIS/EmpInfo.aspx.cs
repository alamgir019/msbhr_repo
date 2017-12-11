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


public partial class EmpInfoSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    UserManager objUserMgr = new UserManager();

    Byte[] imgByte = null;
    Byte[] imgSignByte = null;
    
    DataTable dtEmpInfo = new DataTable();   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            //this.OpenRecord();      
            Common.FillDropDownList_Nil(objMasMgr.SelectHomeDistrict(0), ddlPreDistrict);
            Common.FillDropDownList_Nil(objMasMgr.SelectCountry(0), ddlPreCountry);   
            Common.FillDropDownList_Nil(objMasMgr.SelectHomeDistrict(0), ddlPerDistrict);
            Common.FillDropDownList_Nil(objMasMgr.SelectCountry(0), ddlPerCountry);            
            Common.FillDropDownList_Nil(objMasMgr.SelectBloodGroupList(0), ddlBloodGroup);
            Common.FillDropDownList_Nil(objMasMgr.SelectReligionList(0), ddlReligion);
            Common.FillDropDownList_Nil(objMasMgr.SelectRelationList(0), ddlRelation);
            Common.FillDropDownList_Nil(objEmpInfoMgr.SelectDegree(0, "Y", ""), ddlHighestEdu);
            Common.FillDropDownList_Nil(objEmpInfoMgr.SelectDegree(0,"Y","Y"), ddlProffDegree );
            Common.FillDropDownList_Nil(objMasMgr.SelectSpecialSkill(0), ddlSpecialSkill);
            ddlPerCountry.SelectedValue = Common.RetrieveddL(ddlPerCountry, "2", "99999");          
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
            //txtEmpID.ReadOnly = true;
            txtOldEmpID.ReadOnly = true; 
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
            txtEmpID.ReadOnly = false;
            txtOldEmpID.ReadOnly = false;
            this.GetMaxEmpID();
            imgEmp.ImageUrl = ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg";
            hfEmpImage.Value = "";
            ImgSign.ImageUrl = ConfigurationManager.AppSettings["EmpSignPath"].Trim() + "/NoImage.jpg";

            FileUpload1.Dispose();
            FileUpload2.Dispose();
            imgByte = null;
            imgSignByte = null;
            Session["imgByte"] = "";
            Session["imgSignByte"] = "";
        }
    }

    protected void GetMaxEmpID()
    {
        txtEmpID.Text = objEmpInfoMgr.GetMaxEmpID("E");
    }
     
    private void SaveData(string IsDelete)
    {
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            clsEmpInfo objEmpInfo = new clsEmpInfo(
                txtEmpID.Text.ToString(),
                ddlTitle.SelectedValue.ToString(),
                txtFullName.Text.Trim(),
                txtFirstName.Text.Trim(),
                txtMiddleName.Text.Trim(),
                txtLastName.Text.Trim(),
                txtFatherName.Text.Trim(),
                txtMotherName.Text.Trim(),
                txtPreAddress.Text.Trim(),
                txtPrePhone.Text.Trim(),
                txtPreFax.Text.Trim(),
                txtPerAddress.Text.Trim(),
                txtPerPhone.Text.Trim(),
                txtPerFax.Text.Trim(),
                ddlPerDistrict.SelectedValue.ToString(),
                ddlPerCountry.SelectedValue.ToString(),
                ddlGender.SelectedValue.ToString(),
                txtDob.Text.Trim(),
                ddlReligion.SelectedValue.ToString(),
                ddlBloodGroup.SelectedValue.ToString(),
                ddlMaritalStatus.SelectedValue.ToString(),
                txtMarriageDate.Text.Trim(),
                txtNationality.Text.Trim(),
                txtNationalId.Text.Trim(),
                txtDOBId.Text.Trim(),
                txtTINNo.Text.Trim(),
                txtCircle.Text.Trim(),
                txtZone.Text.Trim(),
                txtPassportNo.Text.Trim(),
                txtPassExpDate.Text.Trim(),
                txtPasportIssOff.Text.Trim(),
                txtSkypeID.Text.Trim(),
                txtOffPhExt.Text.Trim(),
                txtOfficeEmail.Text.Trim(),
                txtCellPhone.Text.Trim(),
                txtLandPhone.Text.Trim(),
                txtPersonalEmail.Text.Trim(),
                ddlHighestEdu.SelectedValue.ToString(),
                ddlProffDegree.SelectedValue.ToString(),
                ddlSpecialSkill.SelectedValue.ToString(),
                chkIsRelativeSC.Checked == true ? "Y" : "N",
                ddlRelation.SelectedValue.ToString(),
                chkIsSpectacled.Checked == true ? "Y" : "N",
                txtLicenseNo.Text.Trim(),
                txtLicenseExpDate.Text.Trim(),
                hfEmpImage.Value.ToString(),
                txtRelativeInfo.Text.Trim(),
                ddlTNTPosition.SelectedValue.ToString().Trim(),
                ddlNature.SelectedValue.ToString(),
                txtBMDCRegNo.Text.Trim(),
                txtBMDCExDate.Text.Trim(),
                ddlPreDistrict.SelectedValue.ToString(),
                ddlPreCountry.SelectedValue.ToString(),
                txtPrePostCode.Text.Trim(),
                txtPerPostCode.Text.Trim(),
                txtTNTCode.Text.Trim(),
                txtNoofLiveChild.Text.Trim(),
                txtOldEmpID.Text.Trim(),
                txtSpouseName.Text.Trim()   
                );

            // Upload Employee Image
            if (hfIsUpadate.Value == "N")
            {
                this.UploadImage();
            }
            else
            {
                if (FileUpload1.HasFile && FileUpload1.PostedFile != null)
                {
                    this.UploadImage();
                }
                else
                {
                        imgByte = (byte[])Session["imgByte"];
                }
            }
            // Upload Employee Signature Image
            if (hfIsUpadate.Value == "N")
            {
                this.UploadSignImage();
            }
            else
            {
                if (FileUpload2.HasFile && FileUpload2.PostedFile != null)
                {
                    this.UploadSignImage();
                }
                else
                {
                        imgSignByte = (byte[])Session["imgSignByte"];
                }
            }

            objEmpInfoMgr.InsertEmpInfo(objEmpInfo, hfIsUpadate.Value, IsDelete, imgByte, imgSignByte);

            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);          
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
            if (hfIsUpadate.Value == "Y")
            {
                dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpID.Text.Trim());
                if (dtEmpInfo.Rows.Count == 0)
                {
                    lblMsg.Text = "This Employee Id is not valid.";
                    txtEmpID.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtFullName.Text) == true)
            {
                lblMsg.Text = "Please Insert Employee Name.";
                txtFullName.Focus();
                return false;
            }
           
            if (hfIsUpadate.Value == "N")
            {
                if (Common.CheckDuplicate("EmpInfo", "EmpId", txtEmpID.Text.Trim(), "", "", false) == true)
                {
                    lblMsg.Text = "This Employee Id is Already Exist.";
                    txtEmpID.Focus();
                    return false;
                }
            }
            else
            {
                if (Common.CheckDuplicate("EmpInfo", "EmpId", txtEmpID.Text.Trim(), "EmpId", hfID.Value, true) == true)
                {
                    lblMsg.Text = "This Employee Id is Already Exist.";
                    txtEmpID.Focus();
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
        this.RefreshControl();
    }

    protected void RefreshControl()
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        lblMsg.Text = "";
        imgEmp.ImageUrl = "";
        ImgSign.ImageUrl = "";        
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            if (string.IsNullOrEmpty(txtEmpID.Text) == false)
            {
                this.SaveData("Y");
                lblMsg.Text = "Employee Information is deleted successfully";
            }
            else
            {
                lblMsg.Text = "Insert EmpID to Delete.";
            }
        }
        this.EntryMode(false);
    }

    protected void cmdFind_Click(object sender, EventArgs e)
    {
        this.FindEmployeeInfo(txtEmpID.Text.Trim());
        
    }

    private void FindEmployeeInfo(string strEmpId)
    {
        if (strEmpId != "")
        {
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(strEmpId);
            if (dtEmpInfo.Rows.Count == 0)
            {
                lblMsg.Text = "Invalid Emp. No .";
                //this.EntryMode(false);
                return;
            }
            else
            {
                foreach (DataRow dRow in dtEmpInfo.Rows)
                {                                       
                    hfID.Value = Common.CheckNullString(dRow["EmpId"].ToString().Trim());
                    if (Common.CheckNullString(dRow["Title"].ToString().Trim()) != "")
                        ddlTitle.SelectedValue = dRow["Title"].ToString().Trim();
                    
                    txtFirstName.Text = Common.CheckNullString(dRow["FirstName"].ToString().Trim());
                    txtMiddleName.Text = Common.CheckNullString(dRow["MiddleName"].ToString().Trim());
                    txtLastName.Text = Common.CheckNullString(dRow["LastName"].ToString().Trim());
                    txtFullName.Text = Common.CheckNullString(dRow["FullName"].ToString().Trim());

                    ddlGender.Text = Common.RetrieveddL(ddlGender, dRow["Gender"].ToString(), "N");
                    ddlReligion.SelectedValue = Common.RetrieveddL(ddlReligion, dRow["ReligionId"].ToString(), "99999");
                    ddlBloodGroup.SelectedValue = Common.RetrieveddL(ddlBloodGroup, dRow["BloodGroupId"].ToString(), "99999");

                    if (Common.CheckNullString(dRow["DOB"].ToString()) != "")
                    {
                        txtDob.Text = Common.DisplayDate(dRow["DOB"].ToString());
                        this.CalculateAge(txtDob.Text);
                    }

                    txtFatherName.Text = Common.CheckNullString(dRow["FatherName"].ToString().Trim());
                    txtMotherName.Text = Common.CheckNullString(dRow["MotherName"].ToString().Trim());

                    txtPreAddress.Text = Common.CheckNullString(dRow["PreAddress"].ToString().Trim());
                    txtPrePhone.Text = Common.CheckNullString(dRow["PrePhone"].ToString().Trim());
                    txtPreFax.Text = Common.CheckNullString(dRow["PreFax"].ToString().Trim());
                    ddlPreDistrict.SelectedValue = Common.RetrieveddL(ddlPreDistrict, dRow["PreDistrictID"].ToString(), "99999");
                    ddlPreCountry.SelectedValue = Common.RetrieveddL(ddlPreCountry, dRow["PreCountryID"].ToString(), "99999");
                    txtPrePostCode.Text = Common.CheckNullString(dRow["PrePostCode"].ToString().Trim());

                    txtPerAddress.Text = Common.CheckNullString(dRow["PerAddress"].ToString().Trim());
                    txtPerPhone.Text = Common.CheckNullString(dRow["PerPhone"].ToString().Trim());
                    txtPerFax.Text = Common.CheckNullString(dRow["PerFax"].ToString().Trim());
                    ddlPerDistrict.SelectedValue = Common.RetrieveddL(ddlPerDistrict, dRow["PerDistrictID"].ToString(), "99999");
                    ddlPerCountry.SelectedValue = Common.RetrieveddL(ddlPerCountry, dRow["PerCountryID"].ToString(), "99999");
                    txtPerPostCode.Text = Common.CheckNullString(dRow["PerPostCode"].ToString().Trim());

                    txtOffPhExt.Text = Common.CheckNullString(dRow["OfficeExt"].ToString().Trim());
                    txtOfficeEmail.Text = Common.CheckNullString(dRow["OfficeEmail"].ToString().Trim());
                    txtPersonalEmail.Text = Common.CheckNullString(dRow["PersonalEmail"].ToString().Trim());                   
                    
                    ddlMaritalStatus.SelectedValue = Common.RetrieveddL(ddlMaritalStatus, dRow["MaritalStatus"].ToString(), "N");
                    txtMarriageDate.Text = Common.DisplayDate(dRow["MarriageDate"].ToString().Trim());

                    //Common.FillDropDownList_Nil(objEmpInfoMgr.SelectEmpEducation(0, txtEmpID.Text.Trim()   , "Y"), ddlHighestEdu);
                    ddlHighestEdu.SelectedValue = Common.RetrieveddL(ddlHighestEdu, dRow["EduId"].ToString(), "99999");
                    ddlProffDegree.SelectedValue = Common.RetrieveddL(ddlProffDegree, dRow["ProffDegreeId"].ToString(), "99999");
                    ddlSpecialSkill.SelectedValue = Common.RetrieveddL(ddlSpecialSkill, dRow["SpecialSkillId"].ToString(), "99999");
                    chkIsSpectacled.Checked = dRow["IsSpectacled"].ToString().Trim() == "Y" ? true : false;

                    txtLicenseNo.Text = Common.CheckNullString(dRow["DrivingLicense"].ToString().Trim());
                    if (Common.CheckNullString(dRow["LicenseRenewDate"].ToString().Trim()) != "")
                        txtLicenseExpDate.Text = Common.DisplayDate(dRow["LicenseRenewDate"].ToString().Trim());

                    chkIsRelativeSC.Checked = dRow["IsRelativeInSC"].ToString().Trim() == "Y" ? true : false;
                    ddlRelation.SelectedValue = Common.RetrieveddL(ddlRelation, dRow["RelationId"].ToString(), "99999");
                    txtTINNo.Text = Common.CheckNullString(dRow["TINNo"].ToString().Trim());
                    txtPassportNo.Text = Common.CheckNullString(dRow["PassportNo"].ToString().Trim());

                    if (Common.CheckNullString(dRow["PassExpDate"].ToString().Trim()) != "")
                        txtPassExpDate.Text = Common.DisplayDate(dRow["PassExpDate"].ToString().Trim());

                    txtCircle.Text = Common.CheckNullString(dRow["Circle"].ToString().Trim());
                    txtZone.Text = Common.CheckNullString(dRow["Zone"].ToString().Trim());

                    txtPasportIssOff.Text = dRow["PassportIssueOffice"].ToString().Trim();
                    txtNationalId.Text = Common.CheckNullString(dRow["NationalId"].ToString().Trim());
                    txtNationality.Text = Common.CheckNullString(dRow["Nationality"].ToString().Trim());

                    txtDOBId.Text = Common.CheckNullString(dRow["DOBId"].ToString().Trim());
                    txtSkypeID.Text = Common.CheckNullString(dRow["SkypeId"].ToString().Trim());
                    txtCellPhone.Text = Common.CheckNullString(dRow["CellPhone"].ToString().Trim());
                    txtLandPhone.Text = Common.CheckNullString(dRow["LandPhone"].ToString().Trim());
                    txtRelativeInfo.Text = Common.CheckNullString(dRow["RelativeInfo"].ToString().Trim());

                    ddlNature.SelectedValue = Common.RetrieveddL(ddlNature, dRow["Nature"].ToString(), "99999");
                    ddlTNTPosition.SelectedValue = Common.CheckNullString(dRow["TNTPosition"].ToString().Trim());
                    txtBMDCRegNo.Text = Common.CheckNullString(dRow["BMDCRegNo"].ToString().Trim());
                    txtBMDCExDate.Text = Common.DisplayDate(dRow["BMDCRegDate"].ToString().Trim());
                    txtTNTCode.Text = Common.CheckNullString(dRow["TNTCode"].ToString().Trim());
                    txtNoofLiveChild.Text = Common.CheckNullString(dRow["NoofLiveChild"].ToString().Trim());
                    txtSpouseName.Text = Common.CheckNullString(dRow["SpouseName"].ToString().Trim());
                    //txtOldEmpID.Text   = Common.CheckNullString(dRow["OldEmpId"].ToString().Trim());
                    txtEmpID.ReadOnly = true;
                    txtOldEmpID.ReadOnly = true;  

                    if (Common.CheckNullString(dRow["EmpImage"].ToString().Trim()) != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])dRow["EmpImage"]);
                        imgByte = ms.ToArray();

                        string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                        imgEmp.ImageUrl = "data:image/png;base64," + base64String;
                        Session["imgByte"] = dRow["EmpImage"];
                    }
                    else
                    {
                        imgByte = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg"));
                        imgEmp.ImageUrl = ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg";
                        hfEmpImage.Value = "";
                        Session["imgByte"] = imgByte;
                    }
                    if (Common.CheckNullString(dRow["EmpSignImage"].ToString().Trim()) != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])dRow["EmpSignImage"]);
                        imgSignByte = ms.ToArray();

                        string base64StringSign = Convert.ToBase64String(imgSignByte, 0, imgSignByte.Length);
                        ImgSign.ImageUrl = "data:image/png;base64," + base64StringSign;
                        Session["imgSignByte"] = dRow["EmpSignImage"];
                    }
                    else
                    {
                        imgSignByte = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg"));
                        ImgSign.ImageUrl = ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg";
                        hfEmpSignImage.Value = "";
                        Session["imgSignByte"] = imgSignByte;
                    }

                    if (Common.CheckNullString(dRow["EmpStatus"].ToString()) == "I")
                        lblMsg.Text = "This Staff Has Been Separated.";
                    else
                        lblMsg.Text = "";
                }
                this.EntryMode(true);
            }
        }
    }
   
    protected void btnCalculate_Click(object sender, EventArgs e)
    {       
        this.CalculateAge(txtDob.Text);
    }

    public string CalculateAge(string birthDate)
    {
        int years;

        // compute & return the difference of two dates,
        // returning years, months & days
        // d1 should be the larger (newest) of the two dates
        
        string dt1 = Common.SetDateTime(DateTime.Now.ToString());
        string dt2 = Common.ReturnDate(birthDate);
        DateTime d1 = Convert.ToDateTime(dt1);
        DateTime d2 = Convert.ToDateTime(dt2);

        int months = 0;
        int days = 0;
        if (d1 < d2)
        {
            DateTime d3 = d2;
            d2 = d1;
            d1 = d3;
        }

        // compute difference in total months
        months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

        // based upon the 'days', adjust months & compute actual days difference
        if (d1.Day < d2.Day)
        {
            months--;
            days = GetDaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
        }
        else
        {
            days = d1.Day - d2.Day;
        }

        // compute years & actual months
        years = months / 12;
        months -= years * 12;
        string CompleteAge = "";
     
        CompleteAge = years + " Years " + months + " Months " + days + " Days";
        txtyear.Text = years + " Years ";
        txtmonth.Text = months + " Months ";
        txtday.Text = days + " Days";
       
        return CompleteAge;
    }


    private static int GetDaysInMonth(int year, int month)
    {
        // this is also available from Calendar class, but just as easy to do ourselves

        if (month < 1 || month > 12)
        {
            throw new ArgumentException("month value must be from 1-12");
        }

        // 1 2 3 4 5 6 7 8 9 10 11 12
        int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (((year / 400 * 400) == year) ||
        (((year / 4 * 4) == year) && (year % 100 != 0)))
        {
            days[2] = 29;
        }

        return days[month];
    }   

    protected void chkVoluntary_CheckedChanged(object sender, EventArgs e)
    {
        //this.GetMaxEmpID();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //if (FileUpload1.HasFile)
        //{
        //    //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        //    string flName = txtEmpID.Text.Trim() + ".jpg";// + FileUpload1.PostedFile.FileName;
        //    string[] arInfo = new string[4];

        //    char[] splitter = { '.' };
        //    arInfo = Common.str_split(flName, splitter);

        //    string FolderPath = ConfigurationManager.AppSettings["EmpImagePath"];
        //    string FilePath = Server.MapPath(FolderPath + "/" + flName);
        //    FileUpload1.SaveAs(FilePath);
        //    imgEmp.ImageUrl = "~/EmpImage/" + flName;
        //    hfEmpImage.Value = flName;
        //}    

        this.UploadImage();
    }

    private void UploadImage()
    {
        if (FileUpload1.HasFile && FileUpload1.PostedFile != null)
        {
            HttpPostedFile File = FileUpload1.PostedFile;
            imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);

            string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);            
        }
        else
        {            
            imgByte = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg"));
        }
    }

    protected void btnUploadSign_Click(object sender, EventArgs e)
    {
        this.UploadSignImage();
    }

    private void UploadSignImage()
    {
        if (FileUpload2.HasFile && FileUpload2.PostedFile != null)
        {
            HttpPostedFile File = FileUpload2.PostedFile;
            imgSignByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgSignByte, 0, File.ContentLength);

            string base64StringSign = Convert.ToBase64String(imgSignByte, 0, imgSignByte.Length);           
        }
        else
        {           
            imgSignByte = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["EmpImagePath"].Trim() + "/NoImage.jpg"));
        }
    }  

    private void GetTaskPermissionContract()
    {        
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "301", "T101");
        if (dtConsTaskPermission.Rows.Count > 0)
        {
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            lnkBtnEmpHRInfo.Visible = true;
        }
        else
        {
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            lnkBtnEmpHRInfo.Visible = false;
        }
    }

    protected void ddlPerCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPerCountry.SelectedValue == "2")
            txtNationality.Text = "Bangladeshi";
        else
            txtNationality.Text = "";
    }
    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTitle.SelectedIndex == 1)
            ddlGender.SelectedIndex = 1;
        else if (ddlTitle.SelectedIndex == 2)
            ddlGender.SelectedIndex = 2;
    }
    protected void lnkBtnEmpHRInfo_Click(object sender, EventArgs e)
    {
        this.RedirectResponse(txtEmpID.Text.Trim(), "EmpHRInfo.aspx");
    }

    private void RedirectResponse(string strValue, string strRedPage)
    {
        Session["HREMPID"] = txtEmpID.Text.Trim();
        Response.Redirect(strRedPage);    
    }
  
    protected void txtFirstName_TextChanged(object sender, EventArgs e)
    {
        this.MergeEmpName();
    }

    protected void txtMiddleName_TextChanged(object sender, EventArgs e)
    {
        this.MergeEmpName();
    }

    protected void txtLastName_TextChanged(object sender, EventArgs e)
    {
        this.MergeEmpName();
    }

    private void MergeEmpName()
    {
        //if (string.IsNullOrEmpty(txtMiddleName.Text.Trim()) ==false )
        //    txtFullName.Text = txtFirstName.Text.Trim() + " " + txtMiddleName.Text.Trim() + " " + txtLastName.Text.Trim(); 
        //else
        //    txtFullName.Text = txtFirstName.Text.Trim() +  " " + txtLastName.Text.Trim(); 
    }
    protected void cmdFindOldEmpInfo_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim() ) == false)
        {
            if (txtEmpID.Text.Trim() == txtOldEmpID.Text.Trim())
            {
                lblMsg.Text = "New & old emlpoyee id can not be same.Please enter new id at first.";
                return;
            }
            else
            {
                string strExistedEmpId = objEmpInfoMgr.SelectOldEmpInfo(txtOldEmpID.Text.Trim());
                if (strExistedEmpId != "")
                {
                    lblMsg.Text = "This Old Id has alreay assigned to another staff with id " + strExistedEmpId + ". So please process with " + strExistedEmpId + " id.";
                    return;
                }
                this.FindEmployeeInfo(txtOldEmpID.Text.Trim());
                this.EntryMode(false);
            }
        }
        else
        {
            this.RefreshControl();
            lblMsg.Text = "Please enter new emp id before retrieving old information";
            return;
        }
    }
}
