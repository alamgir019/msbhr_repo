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
using System.Configuration;
using System.Data.SqlClient;
public partial class Attendance_RoasterShiftShedule : System.Web.UI.Page
{
    dsRoaster objRs = new dsRoaster();
    int TotalDay = 0;
    AttnPolicyTableManager objAttn = new AttnPolicyTableManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtEAttn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (string.IsNullOrEmpty(Session["Division"].ToString()) == false)
            //{
            //    Common.FillDropDownListWithAll(objMasMgr.SelectSBUWiseDept(Convert.ToInt32(Session["SBUID"].ToString())), ddlDept, "DeptName", "DeptId");
            //}
            //else
            //{
            //    Common.FillDropDownListWithAll(objMasMgr.SelectSBUWiseDept(0), ddlDept, "DeptName", "DeptId");
            //}
        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"]; 
                

            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension,"Yes");
        }
    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        grRoaster.Caption = Path.GetFileName(FilePath);
        grRoaster.DataSource = ValidateData(dt);
        grRoaster.DataBind();
        this.FormateHeader();
        this.ColorMarkErrorRow();
    }

    protected DataTable ValidateData(DataTable dt)
    {
        int i = 0;
        TotalDay = GetMonthDay();
        DataTable dtAttn = objAttn.GetData("0");
        foreach (DataRow dRow in dt.Rows)
        {
            if (i != 0)
            {
                if (string.IsNullOrEmpty(dRow[0].ToString())==false)
                {
                    DataRow nRow = objRs.dtRoaster.NewRow();
                    nRow["EmpId"] = dRow[0];
                    nRow["FullName"] = dRow[1];
                    nRow["ID1"] = GetAttnPolicyId(dtAttn, dRow[2].ToString());
                    nRow["Day1"] = GetPolicyName(dRow[2].ToString(), nRow["ID1"].ToString());

                    nRow["ID2"] = GetAttnPolicyId(dtAttn, dRow[3].ToString());
                    nRow["Day2"] = GetPolicyName(dRow[3].ToString(), nRow["ID2"].ToString());

                    nRow["ID3"] = GetAttnPolicyId(dtAttn, dRow[4].ToString());
                    nRow["Day3"] = GetPolicyName(dRow[4].ToString(), nRow["ID3"].ToString());

                    nRow["ID4"] = GetAttnPolicyId(dtAttn, dRow[5].ToString());
                    nRow["Day4"] = GetPolicyName(dRow[5].ToString(), nRow["ID4"].ToString());

                    nRow["ID5"] = GetAttnPolicyId(dtAttn, dRow[6].ToString());
                    nRow["Day5"] = GetPolicyName(dRow[6].ToString(), nRow["ID5"].ToString());

                    nRow["ID6"] = GetAttnPolicyId(dtAttn, dRow[7].ToString());
                    nRow["Day6"] = GetPolicyName(dRow[7].ToString(), nRow["ID6"].ToString());


                    nRow["ID7"] = GetAttnPolicyId(dtAttn, dRow[8].ToString());
                    nRow["Day7"] = GetPolicyName(dRow[8].ToString(), nRow["ID6"].ToString());

                    nRow["ID8"] = GetAttnPolicyId(dtAttn, dRow[9].ToString());
                    nRow["Day8"] = GetPolicyName(dRow[9].ToString(), nRow["ID8"].ToString());

                    nRow["ID9"] = GetAttnPolicyId(dtAttn, dRow[10].ToString());
                    nRow["Day9"] = GetPolicyName(dRow[10].ToString(), nRow["ID9"].ToString());

                    nRow["ID10"] = GetAttnPolicyId(dtAttn, dRow[11].ToString());
                    nRow["Day10"] = GetPolicyName(dRow[11].ToString(), nRow["ID10"].ToString());

                    nRow["ID11"] = GetAttnPolicyId(dtAttn, dRow[12].ToString());
                    nRow["Day11"] = GetPolicyName(dRow[12].ToString(), nRow["ID11"].ToString());



                    nRow["ID12"] = GetAttnPolicyId(dtAttn, dRow[13].ToString());
                    nRow["Day12"] = GetPolicyName(dRow[13].ToString(), nRow["ID12"].ToString());



                    nRow["ID13"] = GetAttnPolicyId(dtAttn, dRow[14].ToString());
                    nRow["Day13"] = GetPolicyName(dRow[14].ToString(), nRow["ID13"].ToString());

                    nRow["ID14"] = GetAttnPolicyId(dtAttn, dRow[15].ToString());
                    nRow["Day14"] = GetPolicyName(dRow[15].ToString(), nRow["ID14"].ToString());

                    nRow["ID15"] = GetAttnPolicyId(dtAttn, dRow[16].ToString());
                    nRow["Day15"] = GetPolicyName(dRow[16].ToString(), nRow["ID15"].ToString());

                    nRow["ID16"] = GetAttnPolicyId(dtAttn, dRow[17].ToString());
                    nRow["Day16"] = GetPolicyName(dRow[17].ToString(), nRow["ID16"].ToString());

                    nRow["ID17"] = GetAttnPolicyId(dtAttn, dRow[18].ToString());
                    nRow["Day17"] = GetPolicyName(dRow[18].ToString(), nRow["ID17"].ToString());

                    nRow["ID18"] = GetAttnPolicyId(dtAttn, dRow[19].ToString());
                    nRow["Day18"] = GetPolicyName(dRow[19].ToString(), nRow["ID18"].ToString());

                    nRow["ID19"] = GetAttnPolicyId(dtAttn, dRow[20].ToString());
                    nRow["Day19"] = GetPolicyName(dRow[20].ToString(), nRow["ID19"].ToString());

                    nRow["ID20"] = GetAttnPolicyId(dtAttn, dRow[21].ToString());
                    nRow["Day20"] = GetPolicyName(dRow[21].ToString(), nRow["ID20"].ToString());

                    nRow["ID21"] = GetAttnPolicyId(dtAttn, dRow[22].ToString());
                    nRow["Day21"] = GetPolicyName(dRow[22].ToString(), nRow["ID21"].ToString());

                    nRow["ID22"] = GetAttnPolicyId(dtAttn, dRow[23].ToString());
                    nRow["Day22"] = GetPolicyName(dRow[23].ToString(), nRow["ID20"].ToString());

                    nRow["ID23"] = GetAttnPolicyId(dtAttn, dRow[24].ToString());
                    nRow["Day23"] = GetPolicyName(dRow[24].ToString(), nRow["ID23"].ToString());

                    nRow["ID24"] = GetAttnPolicyId(dtAttn, dRow[25].ToString());
                    nRow["Day24"] = GetPolicyName(dRow[25].ToString(), nRow["ID24"].ToString());

                    nRow["ID25"] = GetAttnPolicyId(dtAttn, dRow[26].ToString());
                    nRow["Day25"] = GetPolicyName(dRow[26].ToString(), nRow["ID25"].ToString());

                    nRow["ID26"] = GetAttnPolicyId(dtAttn, dRow[27].ToString());
                    nRow["Day26"] = GetPolicyName(dRow[27].ToString(), nRow["ID26"].ToString());

                    nRow["ID27"] = GetAttnPolicyId(dtAttn, dRow[28].ToString());
                    nRow["Day27"] = GetPolicyName(dRow[28].ToString(), nRow["ID27"].ToString());

                    nRow["ID28"] = GetAttnPolicyId(dtAttn, dRow[29].ToString());
                    nRow["Day28"] = GetPolicyName(dRow[29].ToString(), nRow["ID28"].ToString());

                    if (TotalDay == 28)
                    {
                        nRow["ID28"] = GetAttnPolicyId(dtAttn, dRow[29].ToString());
                        nRow["Day28"] = GetPolicyName(dRow[29].ToString(), nRow["ID28"].ToString());

                    }
                    else if (TotalDay == 29)
                    {
                        nRow["ID28"] = GetAttnPolicyId(dtAttn, dRow[29].ToString());
                        nRow["Day28"] = GetPolicyName(dRow[29].ToString(), nRow["ID28"].ToString());

                        nRow["ID29"] = GetAttnPolicyId(dtAttn, dRow[30].ToString());
                        nRow["Day29"] = GetPolicyName(dRow[30].ToString(), nRow["ID29"].ToString());
                    }
                    else if (TotalDay == 30)
                    {
                        nRow["ID28"] = GetAttnPolicyId(dtAttn, dRow[29].ToString());
                        nRow["Day28"] = GetPolicyName(dRow[29].ToString(), nRow["ID28"].ToString());


                        nRow["ID29"] = GetAttnPolicyId(dtAttn, dRow[30].ToString());
                        nRow["Day29"] = GetPolicyName(dRow[30].ToString(), nRow["ID29"].ToString());

                        nRow["ID30"] = GetAttnPolicyId(dtAttn, dRow[31].ToString());
                        nRow["Day30"] = GetPolicyName(dRow[31].ToString(), nRow["ID30"].ToString());
                    }
                    else if (TotalDay == 31)
                    {
                        nRow["ID28"] = GetAttnPolicyId(dtAttn, dRow[29].ToString());
                        nRow["Day28"] = GetPolicyName(dRow[29].ToString(), nRow["ID28"].ToString());


                        nRow["ID29"] = GetAttnPolicyId(dtAttn, dRow[30].ToString());
                        nRow["Day29"] = GetPolicyName(dRow[30].ToString(), nRow["ID29"].ToString());

                        nRow["ID30"] = GetAttnPolicyId(dtAttn, dRow[31].ToString());
                        nRow["Day30"] = GetPolicyName(dRow[31].ToString(), nRow["ID30"].ToString());

                        nRow["ID31"] = GetAttnPolicyId(dtAttn, dRow[32].ToString());
                        nRow["Day31"] = GetPolicyName(dRow[32].ToString(), nRow["ID31"].ToString());

                    }
                    objRs.dtRoaster.Rows.Add(nRow);
                    objRs.dtRoaster.AcceptChanges();
                }
            }
            i++;
        }
        return objRs.Tables[0];
    }
    
    protected string GetAttnPolicyId(DataTable dtAttn,string strName)
    {
        if ((strName == "W") || (strName == "H") || (strName == "LV"))
        {
            return strName;
        }
        if (dtAttn.Rows.Count > 0)
        {
            DataRow[] foundRows;
            foundRows = dtAttn.Select("PolicyName='" + strName + "'");
            if (foundRows.Length > 0)
                return foundRows[0]["AttnPolicyId"].ToString();
            else
                return "";
        }
        else
            return "";
    }
    protected string GetPolicyName(string strName, string strId)
    {
        if ((strId == "W") || (strId == "H") || (strId == "LV"))
        {
            return strId;
        }
        if (string.IsNullOrEmpty(strId) == false)
            return strName;
        else
            return "";
    }

    protected int GetMonthDay()
    {
        int intDay = 0;
        switch (ddlMonth.SelectedValue.ToString())
        {
            case "1":
            case "3":
            case "5":
            case "7":
            case "8":
            case "10":
            case "12":
                intDay = 31;
                break;
            case "4":
            case "6":
            case "9":
            case "11":
                intDay = 30;
                break;
            case "2":
                decimal a= Convert.ToDecimal( ddlYear.SelectedValue);
                decimal b = 4;

               
               decimal Rem;
               Rem = decimal.Remainder(a, b);
               if (Rem == 0)
               {
                   intDay = 29;
               }
               else
               {
                   intDay = 28;
               }
               break;
        }
        return intDay;

    }
    //protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
    //    string FileName = grRoaster.Caption;
    //    string Extension = Path.GetExtension(FileName);
    //    string FilePath = Server.MapPath(FolderPath + FileName);

    //    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
    //    grRoaster.PageIndex = e.NewPageIndex;
    //    grRoaster.DataBind();
    //}
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        string FileName = grRoaster.Caption;
        string Extension = Path.GetExtension(FileName);
        string FilePath = Server.MapPath(FolderPath + FileName);
        FileInfo File = new FileInfo(FilePath);
        File.Delete();
        DataTable dt = null;
        grRoaster.DataSource = dt;
        grRoaster.DataBind();  
    }

    public void FormateHeader()
    {
        int i=0;
        string strDate = "";
        DateTime Date1;
        for (i = 0; i < grRoaster.Columns.Count - 2; i++)
        {
            if (i <= TotalDay - 1)
            {
                strDate = Convert.ToString(i + 1) + "-" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedValue;
                Date1 = Convert.ToDateTime(strDate);
                strDate = strDate+" [ "+ Date1.DayOfWeek.ToString().Substring(0,3).ToUpper()+" ]";
                grRoaster.HeaderRow.Cells[i + 2].Text = strDate;
            }
            
            
        }
    }

    public void ColorMarkErrorRow()
    {
        int i = 0;
        string strEmpId = "";
        foreach (GridViewRow gRow in  grRoaster.Rows)
        {
            for (i = 2; i <= grRoaster.Columns.Count ; i++)
            {
                if (i <= TotalDay - 1)
                {
                    if ( Common.CheckNullString(gRow.Cells[i].Text) == "")
                    {
                        gRow.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    }
                }
                if (i <= DateTime.Today.Day+1)
                {
                    if (gRow.Cells[i].BackColor != System.Drawing.Color.LightPink)
                        gRow.Cells[i].BackColor = System.Drawing.Color.LightGray;
                }
            }
            if (strEmpId == "")
            {
                strEmpId ="'"+ gRow.Cells[0].Text.Trim()+"'";
            }
            else
            {
                strEmpId = strEmpId + ",'" + gRow.Cells[0].Text.Trim()+"'";
            }
            i++;
        }
        hfEmpId.Value = strEmpId;
    }

   
    protected void SaveData()
    {
        string strEmpId = "";
        string strFromDate = "";
        string strToDate = "";
         string strDeptId="";
        DataTable dtEmpAttn=new DataTable();
        string strUserId="'"+Session["USERID"].ToString()+"'";
        if (ddlDept.SelectedValue.ToString() != "0")
            strDeptId = "'" + ddlDept.SelectedValue.ToString() + "'";
        else
            strDeptId = ddlDept.SelectedValue.ToString();
        int MonthDay = 0;
        RoasterManager objRs=new RoasterManager();
        DataTable dtEmp = objRs.GetEmpData(hfEmpId.Value, strDeptId, strUserId);
        foreach (DataRow dRow in dtEmp.Rows)
        {
            if (strEmpId == "")
            {
                strEmpId = "'" + dRow["EmpId"].ToString().Trim() + "'";
            }
            else
            {
                strEmpId = strEmpId + ",'" + dRow["EmpId"].ToString().Trim() + "'";
            }
        }
        int Day=DateTime.Today.Day;
        int SelMonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int CurMonth = DateTime.Today.Month;
        if (SelMonth == CurMonth)
        {
            Day++;
            strFromDate = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + Day.ToString();
            MonthDay = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

            strToDate = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + MonthDay.ToString();
        }
        else if (SelMonth < CurMonth)
        {
            return;
        }
        else if (SelMonth > CurMonth)
        {
            strFromDate = ddlYear.SelectedValue.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" +"1";
            MonthDay = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue.ToString()), Convert.ToInt32(ddlMonth.SelectedValue.ToString()));

            strToDate = ddlYear.SelectedValue.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" + MonthDay.ToString();
        }
        DateTime dtToday = DateTime.Today;

        if(strEmpId!="")
            dtEmpAttn = objRs.GetEmpAttnData(strEmpId, strFromDate, strToDate);
       
        int cout = dtEmp.Rows.Count;
        int attncnt = dtEmpAttn.Rows.Count;
        DataTable dtAttn = objAttn.GetData("0");
        if (strEmpId != "")
        {
            objRs.InsertRoasterShift(dtEmpAttn, dtAttn, grRoaster, strFromDate, strToDate);
            lblMsg.Text = "Roaster Shift Record Successfully";
            foreach (GridViewRow gRow in grRoaster.Rows)
            {
                char[] splitter ={ ',' };
                string[] arinfo = Common.str_split(strEmpId, splitter);
                if (Common.FindInString("'" + gRow.Cells[0].Text.Trim() + "'", arinfo) == true)
                    gRow.BackColor = System.Drawing.Color.BurlyWood;
            }

        }
        else
            lblMsg.Text = "No Employee is under your department/SBU";
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }
}


