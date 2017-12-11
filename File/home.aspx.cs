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

public partial class home : System.Web.UI.Page
{
    MasterTablesManager objMst = new MasterTablesManager();
    MailManagerSmtpClient mailManager = new MailManagerSmtpClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //int iCurrMonth = DateTime.Now.Month;
            //string strStDate = "01" + "-" + iCurrMonth + "-" + DateTime.Now.Year ;           
            //int iMonthDay = Common.GetMonthDay(Convert.ToDateTime(strStDate));
            //string strEndDate = iMonthDay + "-" + iCurrMonth + "-" + DateTime.Now.Year;
            int iCurrMonth = DateTime.Now.Month;

            string strStDate = DateTime.Now.Year + "/" + iCurrMonth + "/" + "01";
            int iMonthDay = Common.GetMonthDay(Convert.ToDateTime(strStDate));
            string strEndDate = DateTime.Now.Year + "/" + iCurrMonth + "/" + iMonthDay;

            this.GetConfirmationEmp(strStDate, strEndDate);          
            this.GetEmpBirthday();
            this.GetLicenseExpireDate(strStDate, strEndDate);
            this.GetRetirementDate(strStDate, strEndDate);
            this.GetAddResponsibility(strStDate, strEndDate);
            this.GetRetrenchmentDate(strStDate, strEndDate);
            this.GetContractExtensionDate(strStDate, strEndDate);
            this.GetFestivalDate(strStDate, strEndDate);
            this.GetBMDCRegDate(strStDate, strEndDate);
            this.GetDrLicRenewDate(strStDate, strEndDate); 
        }
    }

    private void GetConfirmationEmp(string strStDate, string strEndDate)
    {
        grConfirmation.DataSource = objMst.GetConfirmationEmp(strStDate, strEndDate);
        grConfirmation.DataBind();

        foreach (GridViewRow gRow in grConfirmation.Rows)
        {
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            if (Convert.ToDecimal(Common.ReturnZeroForNull(gRow.Cells[7].Text)) == 0)
            {
                gRow.Cells[7].Text = "Today";
                gRow.Cells[7].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[7].Text) < 0)
                gRow.Cells[7].Text = "0";                

            if (gRow.Cells[5].Text == "-1")
                gRow.Cells[5].Text = "";
        }
        lblConfirmation.Text = grConfirmation.Rows.Count.ToString();

        if (grConfirmation.Rows.Count > 0)
            pnlConfirmation.Visible = true;
        else
            pnlConfirmation.Visible = false;
    }

    private void GetEmpBirthday()
    {
        grBirthday.DataSource = objMst.GetEmpBirthday();
        grBirthday.DataBind();

        foreach (GridViewRow gRow in grBirthday.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Convert.ToDecimal(gRow.Cells[5].Text) == 0)
            {
                gRow.Cells[5].Text = "Today";
                gRow.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
        }
        lblBirthday.Text = grBirthday.Rows.Count.ToString();

        if (grBirthday.Rows.Count > 0)        
            pnlBirthDay.Visible = true;        
        else      
            pnlBirthDay.Visible = false;       
    }

    private void GetLicenseExpireDate(string strStDate, string strEndDate)
    {
        grLicense.DataSource = objMst.GetLicenseExpireDate(strStDate, strEndDate);
        grLicense.DataBind();

        foreach (GridViewRow gRow in grLicense.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Convert.ToDecimal(gRow.Cells[5].Text) == 0)
            {
                gRow.Cells[5].Text = "Today";
                gRow.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[5].Text) < 0)
                gRow.Cells[5].Text = "0";                
        }
        lblLicense.Text = grLicense.Rows.Count.ToString();

        if (grLicense.Rows.Count > 0)       
            pnlLicense.Visible = true;       
        else       
            pnlLicense.Visible = false;        
    }

    private void GetRetirementDate(string strStDate, string strEndDate)
    {
        grRetirementDate.DataSource = objMst.GetRetirementDate(strStDate, strEndDate);
        grRetirementDate.DataBind();

        foreach (GridViewRow gRow in grRetirementDate.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Convert.ToDecimal(gRow.Cells[5].Text) == 0)
            {
                gRow.Cells[5].Text = "Today";
                gRow.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[5].Text) < 0)
                gRow.Cells[5].Text = "0";
        }
        lblRetirementDate.Text = grRetirementDate.Rows.Count.ToString();

        if (grRetirementDate.Rows.Count > 0)
            pnlRetirement.Visible = true;
        else
            pnlRetirement.Visible = false;
    }

    private void GetAddResponsibility(string strStDate, string strEndDate)
    {
        grAddResponsibility.DataSource = objMst.GetAddResponsibility(strStDate, strEndDate);
        grAddResponsibility.DataBind();

        foreach (GridViewRow gRow in grAddResponsibility.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Convert.ToDecimal(gRow.Cells[5].Text) == 0)
            {
                gRow.Cells[5].Text = "Today";
                gRow.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[5].Text) < 0)
                gRow.Cells[5].Text = "0";
        }
        lblAddResponsibility.Text = grAddResponsibility.Rows.Count.ToString();

        if (grAddResponsibility.Rows.Count > 0)
            pnlAddResponsibility.Visible = true;
        else
            pnlAddResponsibility.Visible = false;
    }

    private void GetRetrenchmentDate(string strStDate, string strEndDate)
    {
        grRetrenchmentDate.DataSource = objMst.GetRetirementDate(strStDate, strEndDate);
        grRetrenchmentDate.DataBind();

        foreach (GridViewRow gRow in grRetrenchmentDate.Rows)
        {
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (Convert.ToDecimal(gRow.Cells[5].Text) == 0)
            {
                gRow.Cells[5].Text = "Today";
                gRow.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[5].Text) < 0)
                gRow.Cells[5].Text = "0";                
        }
        lblRetrenchmentDate.Text = grRetrenchmentDate.Rows.Count.ToString();

        if (grRetrenchmentDate.Rows.Count > 0)        
            pnlRetrenchmentDate.Visible = true;        
        else        
            pnlRetrenchmentDate.Visible = false;        
    }

    private void GetContractExtensionDate(string strStDate, string strEndDate)
    {
        grContractExtension.DataSource = objMst.GetContractExpireDate(strStDate, strEndDate);
        grContractExtension.DataBind();

        foreach (GridViewRow gRow in grContractExtension.Rows)
        {
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            if (Convert.ToDecimal(gRow.Cells[7].Text) == 0)
            {
                gRow.Cells[7].Text = "Today";
                gRow.Cells[7].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[7].Text) < 0)
                gRow.Cells[7].Text = "0";

            if (gRow.Cells[5].Text == "-1")
                gRow.Cells[5].Text = "";
        }
        lblContractExtension.Text = grContractExtension.Rows.Count.ToString();

        if (grContractExtension.Rows.Count > 0)
            pnlContractExtension.Visible = true;
        else
            pnlContractExtension.Visible = false;
    }  

    private void GetFestivalDate(string strStDate, string strEndDate)
    {
        grFestivalDate.DataSource = objMst.GetFestivalDate(strStDate, strEndDate);
        grFestivalDate.DataBind();

        foreach (GridViewRow gRow in grFestivalDate.Rows)
        {
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            if (Convert.ToDecimal(gRow.Cells[3].Text) == 0)
            {
                gRow.Cells[3].Text = "Today";
                gRow.Cells[3].BackColor = System.Drawing.Color.LightGreen;
            }
        }
        lblFestivalDate.Text = grFestivalDate.Rows.Count.ToString();

        if (grFestivalDate.Rows.Count > 0)        
            pnlFestivalDate.Visible = true;       
        else        
            pnlFestivalDate.Visible = false;        
    }

    private void GetBMDCRegDate(string strStDate, string strEndDate)
    {
        grProfCertDate.DataSource = objMst.GetBMDCRegDate(strStDate, strEndDate);
        grProfCertDate.DataBind();

        foreach (GridViewRow gRow in grProfCertDate.Rows)
        {
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            if (Convert.ToDecimal(gRow.Cells[7].Text) == 0)
            {
                gRow.Cells[7].Text = "Today";
                gRow.Cells[7].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[7].Text) < 0)
                gRow.Cells[7].Text = "0";

            if (gRow.Cells[5].Text == "-1")
                gRow.Cells[5].Text = "";
        }
        lblContractExtension.Text = grProfCertDate.Rows.Count.ToString();

        if (grProfCertDate.Rows.Count > 0)
            pnlProfCertDate.Visible = true;
        else
            pnlProfCertDate.Visible = false;
    }

    private void GetDrLicRenewDate(string strStDate, string strEndDate)
    {
        grDrivingLicense.DataSource = objMst.GetDrLicRenewDate(strStDate, strEndDate);
        grDrivingLicense.DataBind();

        foreach (GridViewRow gRow in grDrivingLicense.Rows)
        {
            gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            if (Convert.ToDecimal(gRow.Cells[7].Text) == 0)
            {
                gRow.Cells[7].Text = "Today";
                gRow.Cells[7].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (Convert.ToDecimal(gRow.Cells[7].Text) < 0)
                gRow.Cells[7].Text = "0";

            if (gRow.Cells[5].Text == "-1")
                gRow.Cells[5].Text = "";
        }
        lblContractExtension.Text = grDrivingLicense.Rows.Count.ToString();

        if (grDrivingLicense.Rows.Count > 0)
            pnlProfCertDate.Visible = true;
        else
            pnlProfCertDate.Visible = false;
    }

    protected void btnContractExtEmail_Click(object sender, EventArgs e)
    {
        string strErrText = "";
        string strSupviserId = "";
        string strEmpName = "";
        string strEmpId = "";
        string strEmpDesignation = "";
        string strContractEndDate = "";
        lblContractExtension.Text = strErrText;

        int i = 1;
        string strGender = "";
        foreach (GridViewRow gRow in grContractExtension.Rows)
        {

            CheckBox chkBoxCE = new CheckBox();
            chkBoxCE = (CheckBox)gRow.Cells[0].FindControl("chkBoxCESelect");
            if (chkBoxCE.Checked)
            {
                if (i >= 1)
                {
                    strSupviserId = gRow.Cells[5].Text.Trim();
                    strEmpName = gRow.Cells[2].Text.Trim();
                    strEmpDesignation = gRow.Cells[3].Text.Trim();
                    strContractEndDate = gRow.Cells[6].Text.Trim();
                    strEmpId = gRow.Cells[1].Text.Trim();
                    mailManager.ContractExtensionReminder(strEmpId, strEmpName, strSupviserId, Session["USERID"].ToString(), strEmpDesignation, strContractEndDate, lblContractExtension);
                }
                i++;
            }
        }

        //int i = 0;
        //int inCount = 0;
        //string strSupervisor = "";
        //foreach (GridViewRow gRow in grContractExtension.Rows)
        //{
        //    string strEmpID = gRow.Cells[1].Text.Trim();

        //    CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBoxCESelect");
        //    if (chk.Checked == true)
        //    {
        //        if (strSupervisor == "")

        //            strSupervisor = gRow.Cells[5].Text.Trim();
        //           // strSupervisor = grContractExtension.DataKeys[gRow.DataItemIndex].Values[13].ToString().Trim();
        //        else
        //           // strSupervisor = strSupervisor + "," + grContractExtension.DataKeys[gRow.DataItemIndex].Values[13].ToString().Trim();
        //            strSupervisor = strSupervisor + "," + gRow.Cells[5].Text.Trim();
        //        inCount++;
        //    }
        //}

        //strSupervisor = Common.ShowDistinctValueFromString(inCount, strSupervisor);
        //string[] strSupervisors = strSupervisor.Split(',');
        //mailManager.SendContractExtensionReminder(strSupervisors, grContractExtension, Session["EMAILID"].ToString().Trim(), lblContractExtension, Session["USERNAME"].ToString(), Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString());


    }


    protected void btnSendEmailConfrm_Click(object sender, EventArgs e)
    {

        string strErrText = "";
        string strSupviserId = "";
        string strEmpName = "";
        string strEmpId = "";
        string strEmpDesignation = "";
        string strConfirmationDate = "";
        lblConfirmation.Text = strErrText;
        int i = 1;
        string strGender = "";
        foreach (GridViewRow gRow in grConfirmation.Rows)
        {
            CheckBox chkBoxCE = new CheckBox();
            chkBoxCE = (CheckBox)gRow.Cells[0].FindControl("chkBoxCDSelect");
            if (chkBoxCE.Checked)
            {
                if (i >= 1)
                {
                    strSupviserId = gRow.Cells[5].Text.Trim();
                    strEmpName = gRow.Cells[2].Text.Trim();
                    strEmpDesignation = gRow.Cells[3].Text.Trim();
                    strConfirmationDate = gRow.Cells[6].Text.Trim();
                    strEmpId = gRow.Cells[1].Text.Trim();
                    mailManager.ConfirmationDateReminder(strEmpId, strEmpName, strSupviserId, Session["USERID"].ToString(), strEmpDesignation, strConfirmationDate, lblConfirmation);
                }
                i++;
            }
        }
    }
}

