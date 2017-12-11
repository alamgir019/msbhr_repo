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
using System.Xml;
using System.Xml.XPath;

public partial class Payroll_Payroll_PaySlipDetails : System.Web.UI.Page
{
    //XPathNavigator nav;
    //XPathDocument docNav;
    //XPathNodeIterator NodeIter;
    //String strExpression;
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();
    XmlDocument myXmlDocument = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strArray = strParams.Split(',');
            if (strArray.Length > 0)
            {
                this.GetPaySlipDetails(strArray[0]);
                lblName.Text = strArray[1]; //+ ", " + strArray[2];
                lblName.ToolTip = strArray[0];
                lblSalPack.Text = strArray[2];
                //lblAbsent.Text = strArray[3];
                //lblLWP.Text = strArray[4];
            }
        }
    }

    protected void GetPaySlipDetails(string strEmpID)
    {
        string FolderPath = ConfigurationManager.AppSettings["XMLFilePath"];
        string FilePath = Server.MapPath(FolderPath + "/" + "XMLPaySlipDets.xml");
        DataSet dsTemp = new DataSet();
        dsTemp.ReadXml(FilePath);
        if (dsTemp.Tables[0].Rows.Count > 0)
        {
            DataRow[] foundRows = dsTemp.Tables[0].Select("EmployeeID='" + strEmpID + "'");
            foreach (DataRow dRow in foundRows)
            {
                objPayslip.Tables["dtPaySlipDets"].ImportRow(dRow);
            }
            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
        }
        grPaySlipDetls.DataSource = objPayslip.Tables["dtPaySlipDets"];
        grPaySlipDetls.DataBind();
        this.FormatGridView();
    }

    protected void FormatGridView()
    {
        int i = 0;
        Decimal dclNetSalary = 0;
        dclNetSalary = Math.Round(dclNetSalary, 4);
        for (i = 0; i < grPaySlipDetls.Rows.Count; i++)
        {
            grPaySlipDetls.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            TextBox txtAmt = (TextBox)grPaySlipDetls.Rows[i].Cells[3].FindControl("txtPayAmnt");
            dclNetSalary = dclNetSalary + Convert.ToDecimal(txtAmt.Text.Trim());
            dclNetSalary = Math.Round(dclNetSalary, 0);
        }
        if (grPaySlipDetls.Rows.Count > 0)
        {
            grPaySlipDetls.FooterRow.Cells[1].Text = "Net Salary";
            grPaySlipDetls.FooterRow.Cells[3].Text = dclNetSalary.ToString();
        }
    }

    protected void UpdatePaySlipDetails(string strEmpID)
    {
        bool SelectedNodeFound = false;
        int i = 0;
        string FolderPath = ConfigurationManager.AppSettings["XMLFilePath"];
        string FilePath = Server.MapPath(FolderPath + "/" + "XMLPaySlipDets.xml");
        myXmlDocument.Load(FilePath);
        XmlNode node;
        node = myXmlDocument.DocumentElement;

        foreach (XmlNode node1 in node.ChildNodes)
            foreach (XmlNode node2 in node1.ChildNodes)
            {
                if (node2.Name == "EmployeeID")
                {
                    if (node2.InnerText.Trim() == strEmpID)
                    {
                        SelectedNodeFound = true;
                    }
                }
                if (SelectedNodeFound == true)
                {
                    if (node2.Name == "PayAmnt")
                    {
                        node2.InnerText = this.GetPayAmount(i);
                        SelectedNodeFound = false;
                        i++;
                    }
                }
            }

        myXmlDocument.Save(FilePath);
    }

    protected string GetPayAmount(int Indx)
    {
        TextBox txtAmt = (TextBox)grPaySlipDetls.Rows[Indx].Cells[3].FindControl("txtPayAmnt");
        return txtAmt.Text.Trim();
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        this.UpdatePaySlipDetails(lblName.ToolTip.ToString());
        FormatGridView();
    }
}
