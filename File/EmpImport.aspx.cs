using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
public partial class File_EmpImport : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfo = new EmpInfoManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        Common.FillDropDownList(objMasMgr.SelectDesignation(0), ddlDesig, "DESIGNAME", "DESIGID", false);
        Common.FillDropDownList(objMasMgr.SelectSubDepartment(0), ddlSubDept, "SUBDEPTNAME", "SUBDEPTID", false);
        Common.FillDropDownList(objMasMgr.SelectClinic("Y"), ddlClinic, "CLINICNAME", "CLINICID", false);
        Common.FillDropDownList(objMasMgr.SelectLocationCategory(0), ddlLocationCategory, "LOCCATNAME", "LOCCATID", false);
        Common.FillDropDownList(objMasMgr.SelectDivision(0), ddlOrg, "DIVISIONSHORTNAME", "DIVISIONID", false);
        Common.FillDropDownList(objMasMgr.SelectEmpTypeList(0), ddlEmpType, "TYPENAME", "EMPTYPEID", false);
        Common.FillDropDownList(objEmpInfo.SelectDegree(0,"Y",""), ddlEducation, "DEGREENAME", "DEGREEID", false);
        Common.FillDropDownList(objMasMgr.SelectHomeDistrict(0), ddlDistrict, "DISTNAME", "DISTID", false);
        Common.FillDropDownList(objMasMgr.SelectBloodGroupList(0), ddlBloodGroup, "BLOODGROUPNAME", "BLOODGROUPID", false);
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SULATA BACK\\Final_Data_For_ BASE_20170330.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
         foreach (GridViewRow gRow in grPayroll.Rows)
            {
                 // Designation 4
                foreach (ListItem itm in ddlDesig.Items)
                {
                    if (gRow.Cells[4].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[4].Text = itm.Value.Trim();
                        gRow.Cells[4].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                // Sub Department (7)
                foreach (ListItem itm in ddlSubDept.Items)
                {
                    if (gRow.Cells[7].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[7].Text = itm.Value.Trim();
                        gRow.Cells[7].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }

                  //Clinic (9)
                foreach (ListItem itm in ddlClinic.Items)
                {
                    if (gRow.Cells[9].Text.Trim() == itm.Text.Trim())
                    {
                        gRow.Cells[9].Text = itm.Value.Trim();
                        gRow.Cells[9].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                // Location Category (11)
                foreach (ListItem itm in ddlLocationCategory.Items)
                {
                    if (gRow.Cells[11].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[11].Text = itm.Value.Trim();
                        gRow.Cells[11].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                // Organization (12)
                foreach (ListItem itm in ddlOrg.Items)
                {
                    if (gRow.Cells[12].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[12].Text = itm.Value.Trim();
                        gRow.Cells[12].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                //  Employee Type (15)
                foreach (ListItem itm in ddlEmpType.Items)
                {
                    if (gRow.Cells[15].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[15].Text = itm.Value.Trim();
                        gRow.Cells[15].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                //   Highest Education (20)
                foreach (ListItem itm in ddlEducation.Items)
                {
                    if (gRow.Cells[20].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[20].Text = itm.Value.Trim();
                        gRow.Cells[20].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
                //    District (27)
                foreach (ListItem itm in ddlDistrict.Items)
                {
                    if (gRow.Cells[27].Text.Trim().ToUpper() == itm.Text.Trim().ToUpper())
                    {
                        gRow.Cells[27].Text = itm.Value.Trim();
                        gRow.Cells[27].BackColor = System.Drawing.Color.Green;
                        break;
                    }
                }
              
              
           
        }
          
    }
}