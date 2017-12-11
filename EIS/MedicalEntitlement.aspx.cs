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

public partial class EIS_MedicalEntitlement : System.Web.UI.Page
{   
    DataTable dtMed = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    MedicalManager objMedMgr=new MedicalManager ();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
  
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        long sl = 0;
        DataTable dtEmp = objMasMgr.SelectEmployee("","A");
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();
        dtEmp.Rows.Clear();
        dtEmp.Dispose();
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();

        foreach (GridViewRow grow in grEmployee.Rows)
        {
            sl = sl + 1;
            grow.Cells[1].Text = sl.ToString();

            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)grow.Cells[0].FindControl("chkBox");
            chBox.Checked = true;
            grow.Cells[0].Enabled = false;
        }

        dtMed = objMedMgr.SelectMedicalPeriod();
        GridView1.DataSource = dtMed;
        GridView1.DataBind();
        this.FormatGridview();

        GridView2.DataSource = dtMed;
        GridView2.DataBind();
        this.FormatGridview2();

        if (dtMed.Rows.Count > 0)
        {
            foreach (DataRow row in dtMed.Rows)
            {
                DateTime strCurrStYr = Convert.ToDateTime(row[0].ToString());
                DateTime strCurrEndYr = Convert.ToDateTime(row[1].ToString());

                if ((Convert.ToInt32(strCurrStYr.Year.ToString()) + 1) != Convert.ToInt32(DateTime.Now.Year.ToString()))
                {
                    lblMsg.Text = "Medical renew has been already completed. So medical renew can be done on next year, 1st " + Common.retMonthName(strCurrStYr.Month.ToString());
                    btnStart.Enabled = false;
                    break;
                }
                else
                {
                    lblMsg.Text = "Please press on start button for medical renewing.";
                    btnStart.Enabled = true;
                }

                if ((Convert.ToInt32(strCurrStYr.Month.ToString())) != Convert.ToInt32(DateTime.Now.Month.ToString()))
                {
                    lblMsg.Text = "Medical renew has been already completed. So medical renew can be done on next year, 1st " + Common.retMonthName(strCurrStYr.Month.ToString());
                    btnStart.Enabled = false;
                    break;
                }
                else
                {
                    lblMsg.Text = "Please press on start button for medical renewing.";
                    btnStart.Enabled = true;
                }
            }
        }
    }


    private void FormatGridview()
    {
        foreach (GridViewRow gRow in GridView1.Rows)
        {
            gRow.Cells[0].Text = Common.DisplayDate(gRow.Cells[0].Text);
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
        }
    }

    private void FormatGridview2()
    {
        foreach (GridViewRow gRow in GridView2.Rows)
        {
            gRow.Cells[0].Text = Common.DisplayDate(gRow.Cells[0].Text);
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
        }
    }
    
    protected void cmdStart_Click(object sender, EventArgs e)
    {
        try
        {
            objMedMgr.UpdateMedicalProfile(grEmployee);           
            dtMed = objMedMgr.SelectMedicalPeriod();
            btnStart.Enabled = false;
            GridView1.DataSource = dtMed;
            GridView1.DataBind();
            this.FormatGridview();

            GridView2.DataSource = dtMed;
            GridView2.DataBind();
            this.FormatGridview2();                       
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        lblMsg.Text = "Medical renew process has been completed successfully";
    }
}
