using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Principal;
using System.Security.Cryptography;
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    // variable must be static
    DBConnector objDB = new DBConnector();
	public Common()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    /// <summary>
    /// This method is used to convert date and send to db
    /// </summary>
    /// <param name="txt">type:string</param>
    /// <returns></returns>
    public static string ReturnDate(string txt)
    {
        string[] arInfo = new string[4];
         string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate;
       
        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strYear + "/" + strMon + "/" + strDay;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }

    public static void EmptyTextBoxValues(Control parent, TextBox txt)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                EmptyTextBoxValues(c, txt);
            }
            else if (c.GetType() == typeof(TextBox))
            {
                if (((TextBox)(c)) != txt)
                    ((TextBox)(c)).Text = "";
            }
            else if (c.GetType() == typeof(DropDownList))
            {
                ((DropDownList)(c)).SelectedIndex = 0;
            }
            else if (c.GetType() == typeof(CheckBox))
            {
                ((CheckBox)(c)).Checked = false;
            }
            else if (c.GetType() == typeof(HiddenField))
            {
                ((HiddenField)(c)).Value = "";
            }
            //else if (c.GetType() == typeof(Label))
            //{
            //    ((Label)(c)).Text = "";
            //}

            //modified by Murad 17.10.2011
            else if (c.GetType() == typeof(RadioButton))
            {
                ((RadioButton)(c)).Checked = false;
            }

        }
    }

    public static string GetPreviousYear(string strCurrMonth, string strPrevMonth, string strYear)
    {
        int inCurrMonth = Convert.ToInt32(strCurrMonth);
        int inYear = Convert.ToInt32(strYear);
        switch (inCurrMonth)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                switch (Convert.ToInt32(strPrevMonth))
                {
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        inYear = inYear - 1;
                        break;
                }

                break;
        }
        return inYear.ToString();
    }

    public static int GetMonthDiffTillJune(string strMonth)
    {
        int inMonth = Convert.ToInt32(strMonth);
        int inDiff = 0;
        switch (inMonth)
        {
            case 1:
                inDiff = 5;
                break;
            case 2:
                inDiff = 4;
                break;
            case 3:
                inDiff = 3;
                break;
            case 4:
                inDiff = 2;
                break;
            case 5:
                inDiff = 1;
                break;
            case 6:
                inDiff = 12;
                break;
            case 7:
                inDiff = 11;
                break;
            case 8:
                inDiff = 10;
                break;
            case 9:
                inDiff = 9;
                break;
            case 10:
                inDiff = 8;
                break;
            case 11:
                inDiff = 7;
                break;
            case 12:
                inDiff = 6;
                break;
        }
        return inDiff;
    }

    public static DateTime DBTOGUIDate(string txt)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate;

        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strYear + "/" + Common.retMonthName(strMon) + "/" + strDay;
        return Convert.ToDateTime(strDate);
        //string strDay=(txt.Text.Trim(),'-');
    }
    
    public static string ReturnDateTime(string txt)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate,strTime;
        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        strTime = arInfo[1] + " " + arInfo[2];
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strYear + "/" + strMon + "/" + strDay + " " + strTime;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }
    public static string ReturnDateTime24(string txt)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate, strTime;
        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        strTime = arInfo[1] ;
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strYear + "/" + strMon + "/" + strDay + " " + strTime;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }
    public static string SetDate(string txt)
    {
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate;
        DateTime dtDate;
        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        dtDate = Convert.ToDateTime(arInfo[0]);
        if (dtDate.Day<=9)
            strDay ="0"+ dtDate.Day.ToString();
        else
            strDay = dtDate.Day.ToString();
        if (dtDate.Month <= 9)
            strMon = "0" + dtDate.Month.ToString();
        else
            strMon = dtDate.Month.ToString();
        //strMon = dtDate.Month.ToString();
        strYear = dtDate.Year.ToString();
        strDate = strYear + "/" + strMon + "/" + strDay;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }


    public static string SetDateTime(string txt)
    {
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate,strTime;
        DateTime dtDate;
        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        strTime = arInfo[1] + " " + arInfo[2];
        dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = dtDate.Day.ToString();
        strMon = dtDate.Month.ToString();
        strYear = dtDate.Year.ToString();
        strDate = strYear + "/" + strMon + "/" + strDay + " " + strTime;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }

    public static string DisplayDate(string str)
    {
        //return str;
        if (CheckNullString(str) != "")
        {
            string[] arInfo = new string[4];
            string strDay, strMon, strYear, strDate;
            DateTime dtDate;
            char[] splitter = { ' ' };
            arInfo = str_split(str, splitter);
            dtDate = Convert.ToDateTime(arInfo[0]);
            strDay = dtDate.Day.ToString();
            strMon = dtDate.Month.ToString();
            strYear = dtDate.Year.ToString();
            if (strDay.Length < 2)
            {
                strDay = "0" + strDay;
            }
            if (strMon.Length < 2)
            {
                strMon = "0" + strMon;
            }
            strDate = strDay + "/" + strMon + "/" + strYear;
            return strDate;
        }
        return "";
    }
    public static string DisplayDateIIS(string str)
    {
        //return str;
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate;
        DateTime dtDate;
        char[] splitter = { ' ' };
        arInfo = str_split(str, splitter);
        dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = dtDate.Day.ToString();
        strMon = dtDate.Month.ToString();
        strYear = dtDate.Year.ToString();
        if (strDay.Length < 2)
        {
            strDay = "0" + strDay;
        }
        if (strMon.Length < 2)
        {
            strMon = "0" + strMon;
        }
        strDate = strMon + "/" + strDay + "/" + strYear;
        return strDate;
    }


    public static string DisplayDateTime(string str)
    {
        //return str;
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate,strTime;
        DateTime dtDate;
        char[] splitter = { ' ' };
        arInfo = str_split(str, splitter);
        strTime = arInfo[1] + " " + arInfo[2];
        dtDate = Convert.ToDateTime(arInfo[0]);
        
        strDay = dtDate.Day.ToString();
        strMon = dtDate.Month.ToString();
        strYear = dtDate.Year.ToString();
        if (strDay.Length < 2)
        {
            strDay = "0" + strDay;
        }
        if (strMon.Length < 2)
        {
            strMon = "0" + strMon;
        }
        strDate = strDay + "/" + strMon + "/" + strYear + " " + strTime;
        return strDate;
    }


    // Display Only Time
    public static string DisplayTime(string str)
    {
        //return str;
        string[] arInfo = new string[4];
        string  strTime="";
        char[] splitter = { ' ' };
        arInfo = str_split(str, splitter);
        if(arInfo.Length==3)
            strTime = arInfo[1] + " " + arInfo[2];
        else if (arInfo.Length == 2)
            strTime = str;
        arInfo = null;
        return strTime;
    }
    /// <summary>
    /// This methos is use for split a string on spacefic character basic.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitter"></param>
    /// <returns></returns>
    /// 
    public static string DisplayMothYear(string str)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate;

        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(str, splitter);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strMon + "/" + strYear;
        return strDate;
        //return str;
        //string[] arInfo = new string[4];
        //string strDay, strMon, strYear, strDate;
        //DateTime dtDate;
        //char[] splitter = { ' ' };
        //arInfo = str_split(str, splitter);
        //dtDate = Convert.ToDateTime(arInfo[0]);
        //strDay = dtDate.Day.ToString();
        //strMon = dtDate.Month.ToString();
        //strYear = dtDate.Year.ToString();
        //if (strDay.Length < 2)
        //{
        //    strDay = "0" + strDay;
        //}
        //if (strMon.Length < 2)
        //{
        //    strMon = "0" + strMon;
        //}
        //strDate = strMon + "/" + strYear;
        //return strDate;
    }

    public static string ReturnDay(string txt)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate;

        //string strUpperTime="00:00:00 AM";
        //string strLowerTime=""
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
        // dtDate = Convert.ToDateTime(arInfo[0]);
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        strDate = strDay + "/" + strMon;
        return strDate;
        //string strDay=(txt.Text.Trim(),'-');
    }

    public static string[] str_split(string str, char[] splitter)
    {
        string[] arInfo = new string[4];
        arInfo = str.Split(splitter);

        return arInfo;
    }

    public static string retMonthName(string str)
    {
        if (str == "0")
        {
            return "";
        }
        else if (str == "1" || str == "01")
        {
            return "Jan";
        }
        else if (str == "2" || str == "02")
        {
            return "Feb";
        }
        else if (str == "3" || str == "03")
        {
            return "Mar";
        }
        else if (str == "4" || str == "04")
        {
            return "Apr";
        }
        else if (str == "5" || str == "05")
        {
            return "May";
        }
        else if (str == "6" || str == "06")
        {
            return "Jun";
        }
        else if (str == "7" || str == "07")
        {
            return "Jul";

        }
        else if (str == "8" || str == "08")
        {
            return "Aug";
        }
        else if (str == "9" || str == "09")
        {
            return "Sep";
        }

        else if (str == "10")
        {
            return "Oct";
        }
        else if (str == "11")
        {
            return "Nov";
        }
        else if (str == "12")
        {
            return "Dec";
        }
        else
            return "";
    }

    public static string ReturnFullMonthName(string str)
    {
        if (str == "0")
        {
            return "";
        }
        else if (str == "1" || str == "01")
        {
            return "January";
        }
        else if (str == "2" || str == "02")
        {
            return "February";
        }
        else if (str == "3" || str == "03")
        {
            return "March";
        }
        else if (str == "4" || str == "04")
        {
            return "April";
        }
        else if (str == "5" || str == "05")
        {
            return "May";
        }
        else if (str == "6" || str == "06")
        {
            return "Jun";
        }
        else if (str == "7" || str == "07")
        {
            return "July";

        }
        else if (str == "8" || str == "08")
        {
            return "August";
        }
        else if (str == "9" || str == "09")
        {
            return "September";
        }

        else if (str == "10")
        {
            return "October";
        }
        else if (str == "11")
        {
            return "November";
        }
        else if (str == "12")
        {
            return "December";
        }
        else
            return "";
    }

    public static string ReturnTotalDay(string Day)
    {
        string[] arInfo = new string[2];
        string strDay;

        char[] splitter = { '.' };
        arInfo = Common.str_split(Day, splitter);

        strDay = arInfo[0];

        return strDay;
    }
    //To Reset Only TextBOX Values
    public static void RestTextBoxValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                EmptyTextBoxValues(c);
            }
            else if (c.GetType() == typeof(TextBox))
            {
                ((TextBox)(c)).Text = "";
            }
            else if (c.GetType() == typeof(CheckBox))
            {
                ((CheckBox)(c)).Checked = false;
            }
        }
    }
    /// <summary>
    /// This methos is use for re-Initialize the form TextBox,DropDownList and CheckBox basic.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitter"></param>
    /// <returns></returns>
    public static void EmptyTextBoxValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                EmptyTextBoxValues(c);
            }
            else if (c.GetType() == typeof(TextBox))
            {
                ((TextBox)(c)).Text = "";
            }
            else if (c.GetType() == typeof(DropDownList))
            {
                ((DropDownList)(c)).SelectedIndex = 0;
            }
            else if (c.GetType() == typeof(CheckBox))
            {
                ((CheckBox)(c)).Checked = false;
            }
            else if (c.GetType() == typeof(HiddenField))
            {
                ((HiddenField)(c)).Value = "";
            }
           
            //else if (c.GetType() == typeof(Label))
            //{
            //    ((Label)(c)).Text = "";
            //}

        }
    }

    //Edited By Amit
    //To Reset Only TextBOX Values
    public static void ResetTextBoxValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                ResetTextBoxValues(c);
            }
            else if (c.GetType() == typeof(TextBox))
            {
                ((TextBox)(c)).Text = "";
            }
            else if (c.GetType() == typeof(CheckBox))
            {
                ((CheckBox)(c)).Checked = false;
            }
        }
    }

    //Edited by AMIT
    //To Fill DropdownList

    public static void FillDropDownList(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillDropDownListWithAll(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        lst.Text = "All";
        lst.Value = "-1";
        ddl.Items.Add(lst);
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillDropDownListWithAllM(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        lst.Text = "All";
        lst.Value = "-1";
        ddl.Items.Add(lst);
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillDropDownList(DataTable dt, DropDownList ddl, int TextCol, int ValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[TextCol].ToString();
            lst.Value = row[ValueCol].ToString();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillDropDownList(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue, string strIniValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = strIniValue;
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString().Trim();
            lst.Value = row[strValueCol].ToString().Trim();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillDropDownListWithGross(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString();
            ddl.Items.Add(lst);
        }

        lst = new ListItem();
        lst.Text = "Gross Salary";
        lst.Value = "99";
        ddl.Items.Add(lst);

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }
    //Date:07-Jun-2009
    //Edited By Amit
    //For Finding Text by Value of Dropdwon List
    public static string FindInDdlTextData(DropDownList ddl, string strValue)
    {
        string strText = "";
        foreach (ListItem itm in ddl.Items)
        {
            if (string.Compare(itm.Value.ToString(), strValue, true) == 0)
            {
                strText = itm.Text;
                break;
            }
        }
        return strText;
    }

    //Date:07-April-2014
    //Edited By Nabab
    //For Finding Value by Text of Dropdwon List
    public static string FindInDdlValueData(DropDownList ddl, string strValue)
    {
        string strText = "";
        foreach (ListItem itm in ddl.Items)
        {
            if (string.Compare(itm.Text.Trim(), strValue, true) == 0)
            {
                strText = itm.Value;
                break;
            }
        }
        return strText;
    }

    public static string getMaxIdVar(string TableName, string ColumnName)
    {
        DBConnector objDB1= new DBConnector();
        string strRetValue = "";
        strRetValue=objDB1.GetMaxIdVar(TableName, ColumnName);
        return strRetValue;
    }
    public static string getMaxIdVar(string TableName, string ColumnName, int inLen)
    {
        DBConnector objDB1 = new DBConnector();
        string strRetValue = "";
        strRetValue = objDB1.GetMaxIdVar(TableName, ColumnName, inLen);
        return strRetValue;
    }

    public static string getMaxId(string TableName, string ColumnName)
    {
        DBConnector objDB1 = new DBConnector();
        long intRetValue = 0;
        intRetValue = objDB1.GerMaxIDNumber(TableName, ColumnName);
        return intRetValue.ToString();
    }

    public static void FillDropDownListNA(DataSet ds, string TableName, DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        lst.Text = "N/A";
        lst.Value = "-1";
        ddl.Items.Add(lst);
        foreach (DataRow row in ds.Tables[TableName].Rows)
        {
            lst = new ListItem();
            lst.Text = row[1].ToString();
            lst.Value = row[0].ToString();
            ddl.Items.Add(lst);
        }
    }

    public static string GetDdlValue(string strValue)
    {
        string strRetValue="";
        if (string.IsNullOrEmpty(strValue) == false)
        {
            strRetValue = strValue;
        }
        else
        {
            strRetValue = "-1";
        }
        return strRetValue;
    }

    public static string[] SpllitedTime(string strTime)
    {
        string[] arInfo1 = new string[4];
        string[] arInfo2 = new string[4];
        string[] arInfo3 = new string[3];
        char[] splitter1 = { ' ' };
        arInfo1 = str_split(strTime, splitter1);
        char[] splitter2 = { ':' };
        arInfo2 = str_split(arInfo1[0], splitter2);
        arInfo3[0] = arInfo2[0];
        arInfo3[1] = arInfo2[1];
        arInfo3[2] = arInfo1[1];
        arInfo1 = null;
        arInfo2 = null;
        return arInfo3;
    }

    public static string[] SpllitedDate(string strDate)
    {
        string[] arInfo1 = new string[4];
        string[] arInfo2 = new string[4];
        string[] arInfo3 = new string[3];
        char[] splitter1 = { ' ' };
        arInfo1 = str_split(strDate, splitter1);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo1[0], splitter2);
        if (string.Compare(arInfo1[0], arInfo2[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo1[0], splitter3);
        }
        arInfo3[0] = Convert.ToString(Convert.ToInt32( arInfo2[0]));
        arInfo3[1] = Convert.ToString(arInfo2[1]);
        arInfo3[2] = Convert.ToString(Convert.ToInt32(arInfo2[2]));
        arInfo1 = null;
        arInfo2 = null;
        return arInfo3;
    }

    public static void FillDayList(int intYear, int intMonth, DropDownList ddl)
    {
        bool IsLeapYear = false;
        int i = 0;
        int intRem = 0;
        Math.DivRem(intYear, 4, out intRem);

        if (intRem == 0)
            IsLeapYear = true;
        ddl.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "--Day--";
        li.Value = "-1";
        ddl.Items.Add(li);
        if (intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12)
        {
            FillDayListItem(31, ddl);
        }
        else if (intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11)
        {
            FillDayListItem(30, ddl);
        }
        else if (intMonth == 2)
        {
            if (IsLeapYear == true)
                FillDayListItem(29, ddl);
            else
                FillDayListItem(28, ddl);
        }
    }

    private static void FillDayListItem(int intRange, DropDownList ddl)
    {
        int i = 0;
        for (i = 1; i <= intRange; i++)
        {
            ListItem lst = new ListItem();
            if (i <= 9)
            {
                lst.Text = "0" + i.ToString();
                lst.Value = "0" + i.ToString();
            }
            else
            {
                lst.Text =  i.ToString();
                lst.Value = i.ToString();
            }
            ddl.Items.Add(lst);
        }
    }

    private static bool ColumnEqual(object A, object B)
    {
        // Compares two values to see if they are equal. Also compares DBNULL.Value.
        // Note: If your DataTable contains object fields, then you must extend this
        // function to handle them in a meaningful way if you intend to group on them.

        if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
            return true;
        if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
            return false;
        return (A.Equals(B));  // value type standard comparison
    }

    public static DataTable SelectDistinct(string TableName, DataTable SourceTable, string TextFieldName,string ValueFieldName)
    {
        bool IsSingleField = false;
        if (string.Compare(TextFieldName, ValueFieldName) == 0)
            IsSingleField = true;
        DataTable dt = new DataTable(TableName);
        dt.Columns.Add(TextFieldName, SourceTable.Columns[TextFieldName].DataType);
        if (IsSingleField==false)
            dt.Columns.Add(ValueFieldName, SourceTable.Columns[ValueFieldName].DataType);

        object LastValue = null;
        foreach (DataRow dr in SourceTable.Select("", ValueFieldName))
        {
            if (LastValue == null || !(Common.ColumnEqual(LastValue, dr[ValueFieldName])))
            {
                LastValue = dr[ValueFieldName];
                if (IsSingleField == false)
                {
                    DataRow row = dt.NewRow();
                    row[TextFieldName] = dr[TextFieldName];
                    row[ValueFieldName] = dr[ValueFieldName];
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                }
                else
                {
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
        }
        //if (ds != null)
        //    ds.Tables.Add(dt);
        return dt;
    }

    public static string CheckNullString(string str)
    {
        if ((string.IsNullOrEmpty(str) == false) && str != "&nbsp;")
            return str;
        else
            return "";
    }

    public static string ReturnZeroForNull(string str)
    {
        if ((string.IsNullOrEmpty(str) == false) && str != "&nbsp;")
            return str;
        else
            return "0";
    }

    /// Edited By Sulata
    public static bool CompareHashValues(string sSourceData1, string sSourceData2)
    {
        byte[] tmpSource;
        byte[] tmpHash;
        //sSourceData = "MySourceData";
        //Create a byte array from source data
        tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData1);

        //Compute hash based on source data
        tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        //Console.WriteLine(ByteArrayToString(tmpHash));

        //sSourceData = "NotMySourceData";
        tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData2);

        byte[] tmpNewHash;

        tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

        bool bEqual = false;
        if (tmpNewHash.Length == tmpHash.Length)
        {
            int i = 0;
            while ((i < tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
            {
                i += 1;
            }
            if (i == tmpNewHash.Length)
            {
                bEqual = true;
            }
        }

        if (bEqual)
            return true;
        else
            return false;
    }

    public static string getHashValue(string sSourceData)
    {
        byte[] tmpSource;
        byte[] tmpHash;
        tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

        //Compute hash based on source data
        tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        return ByteArrayToString(tmpHash);
    }

    public static string ByteArrayToString(byte[] inputArray)
    {
        StringBuilder output = new StringBuilder("");
        for (int i = 0; i < inputArray.Length; i++)
        {
            output.Append(inputArray[i].ToString("X2"));
        }
        return output.ToString();
    }

    public static bool AdjustApostopi(string vData, string ch)
    {
        //(') Single quot character does not effect any problem when saving data
        //Problem occurs when retriving the data that time this function should use
        bool IsExist = false;
        char chr = Convert.ToChar(ch);
        foreach (char c in vData.ToCharArray())
        {
            if (chr == c)
            {
                IsExist = true;
                break;
            }
            else
            {
                IsExist = false;
            }
        }
        return IsExist;
    }

    public static bool CheckDuplicate(string TableName, string ValueField, string TestValueField, string IdField, string TestIdField, bool IsUpdate)
    {
        DBConnector objDB1 = new DBConnector();
        if (objDB1.IsDuplicate(TableName, ValueField, TestValueField, IdField, TestIdField, IsUpdate) == true)
            return true;
        else
            return false;
    }

    public static bool CheckStartEndDate(string strStartDate, string strEndDate)
    {
        char[] splitter = { '-' };
        string[] arinfo = Common.str_split(strStartDate, splitter);
        if (arinfo.Length == 1)
        {
            char[] splitter2 = { '/' };
            arinfo = Common.str_split(strStartDate, splitter2);
        }
        DateTime dtStartDate=new DateTime();
        DateTime dtEndDate = new DateTime();
        double dblTotDay = 0;
        if (arinfo.Length == 3)
        {
            dtStartDate = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
            arinfo = null;
        }
        arinfo = Common.str_split(strEndDate, splitter);
        if (arinfo.Length == 1)
        {
            char[] splitter2 = { '/' };
            arinfo = Common.str_split(strEndDate, splitter2);
        }        
        if (arinfo.Length == 3)
        {
            dtEndDate = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
            arinfo = null;
        }

        TimeSpan Dur = dtEndDate.Subtract(dtStartDate);

        //dblTotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        dblTotDay = Math.Round(Convert.ToDouble(Dur.Days), 0);
        if (dblTotDay < 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Set Time Limit
    /// True:Upper and False:Lower
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="IsUpper"></param>
    /// <returns></returns>
    public static string ReturnDateFormat(string txt, bool IsUpper)
    {
        string[] arInfo = new string[4];
        string strDay, strMon, strYear, strDate;
        string strUpperTime = " 23:59";
        string strLowerTime = " 00:00";
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        char[] splitter2 = { '/' };
        arInfo = str_split(arInfo[0], splitter2);
        strDay = arInfo[0];
        strMon = arInfo[1];
        strYear = arInfo[2];
        strDate = strYear + "/" + strMon + "/" + strDay;
        if (IsUpper == true)
            strDate = strDate + strUpperTime;
        else if (IsUpper == false)
            strDate = strDate + strLowerTime;

        return strDate;
    }
    ///End of Sulata
    //edited by Asique
    public static void FillDropDownList_All(DataTable dt, DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        lst.Text = "All";
        lst.Value = "-1";

        ddl.Items.Add(lst);

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[1].ToString();
            lst.Value = row[0].ToString();
            ddl.Items.Add(lst);
        }
    }

    public static void FillDropDownList(DataTable dt, DropDownList ddl, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[1].ToString(); 
            lst.Value = row[0].ToString();
            ddl.Items.Add(lst);
        }
    }

    public static void FillIdNameDropDownList(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strValueCol].ToString().Trim() + " (" + row[strTextCol].ToString() + ")";
            lst.Value = row[strValueCol].ToString().Trim();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillIdNameDropDownList3(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString().Trim() + " (" + row[strValueCol].ToString() + ")";
            lst.Value = row[strValueCol].ToString().Trim();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillIdNameDropDownList_Nil(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Nil";
            lst.Value = "";
            ddl.Items.Add(lst);
        }

        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString().Trim() + " [" + row[strValueCol].ToString() + "]";
            lst.Value = row[strValueCol].ToString().Trim();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    public static void FillIdNameDropDownList2(DataTable dt, DropDownList ddl, string strTextCol, string strValueCol, bool ShowInitialValue)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        DataTable dtTemp = new DataTable();
        if (ShowInitialValue == true)
        {
            lst.Text = "Select";
            lst.Value = "-1";
            ddl.Items.Add(lst);
        }


        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString().Trim();
            ddl.Items.Add(lst);
        }

        dtTemp.Rows.Clear();
        dtTemp.Dispose();
    }

    // return database data and time with upper and lower
    public static string ReturnDateFormat_ddmmyyyy(string txt, bool IsUpper)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strDay, strMon, strYear, strDate;
        string strUpperTime = " 23:59";
        string strLowerTime = " 00:00";
        char[] splitter = { ' ' };
        arInfo = str_split(txt, splitter);
        char[] splitter2 = { '/' };
        arInfo2 = str_split(arInfo[0], splitter2);
        if (string.Compare(arInfo2[0], arInfo[0]) == 0)
        {
            char[] splitter3 = { '-' };
            arInfo2 = str_split(arInfo[0], splitter3);
        }
           
        strDay = arInfo2[0];
        strMon = arInfo2[1];
        strYear = arInfo2[2];
        //strDate = strYear + "/" + strMon + "/" + strDay;
        strDate = strYear + "/" + strMon + "/" + strDay;
        if (IsUpper == true)
            strDate = strDate + strUpperTime;
        else if (IsUpper == false)
            strDate = strDate + strLowerTime;

        return strDate;
    }
   
    // end of ashique


    // Creating a Distinct String from a String of data seperated by comma.
    public static string ShowDistinctValueFromString(int ValueCount, string strText)
    {
        string strRetValue = "";
        string[] arInfo = new string[ValueCount];
        char[] splitter = { ',' };
        arInfo = str_split(strText, splitter);
        foreach (string str in arInfo)
        {
            if (strRetValue == "")
            {
                strRetValue = str;
            }
            else
            {
                if (FindInString(str, str_split(strRetValue, splitter)) == false)
                    strRetValue = strRetValue + "," + str;
            }
        }
        return strRetValue;
    }
    // To Find a Specific String into a String Array List
    public static bool FindInString(string str, string[] strArray)
    {
        bool blValue = false;
        foreach (string strValue in strArray)
        {
            if (str == strValue)
            {
                blValue = true;
                break;
            }
        }
        return blValue;
    }

    public static int GetMonthDay(DateTime dtDate)
    {
        int intDay = 0;
        switch (dtDate.Month.ToString())
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
                decimal a = Convert.ToDecimal(dtDate.Year);
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

    public static string DisplayTime24h(string str)
    {
        //return str;
        if (String.IsNullOrEmpty(str) == true)
            return "";
        DateTime dtTime = Convert.ToDateTime(str);
        string strTime = dtTime.ToString("HH:mm");
        return strTime;
    }

    public static void FillDropDownList_Nil(DataTable dt, DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        lst.Text = "Nil";
        lst.Value = "99999";
        ddl.Items.Add(lst);
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[1].ToString();
            lst.Value = row[0].ToString();
            ddl.Items.Add(lst);
        }
    }

    public static void FillCheckBoxList(DataTable dt, CheckBoxList chkL,string strTextCol,string strValueCol)
    {
        chkL.Items.Clear();
        ListItem lst;
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[strTextCol].ToString();
            lst.Value = row[strValueCol].ToString();
            chkL.Items.Add(lst);
        }
    }

    public static void FillDropDownValue(DataTable dt, DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        foreach (DataRow row in dt.Rows)
        {
            lst = new ListItem();
            lst.Text = row[1].ToString();
            lst.Value = row[0].ToString();
            ddl.Items.Add(lst);
        }
    }

    public static string RetrieveddL(DropDownList ddl, string srcVal, string dfltValue)
    {
        string strRet = dfltValue;
        foreach (ListItem ddlitem in ddl.Items)
        {
            if (ddlitem.Value == srcVal)
            {
                strRet = ddlitem.Value;
                break;
            }
        }
        return strRet;
    }

    public static string ReturnSeparateValueFromDate(string strDate, string strType)// dd-MM-yyyy
    {
        string[] arinfo=new string[4];
        string[] arinfo2=new string[4];
        char[] splitter={ '/' };
        arinfo=str_split(strDate,splitter);
        if(arinfo[0]==strDate)
        {
          char[]  splitter1={ '-' };
          arinfo=null;
          arinfo=str_split(strDate,splitter1);
        }
        if (strType == "m")
            return arinfo[1];
        else if (strType == "d")
            return arinfo[0];
        else if (strType == "y")
            return arinfo[2];
        else
            return "";
    }
    // finding a value in to a data table
    public static bool FindInDataTable(DataTable dt, string strValue)
    {
        bool retFlag = false;
        foreach (DataRow dRow in dt.Rows)
        {
            if (dRow["DEPTID"].ToString().Trim() == strValue)
            {
                retFlag = true;
                break;
            }
        }
        return retFlag;
    }

    // finding a value in to a data table
    public static bool FindInDataTable(DataTable dt, string strValue,string strColName)
    {
        bool retFlag = false;
        foreach (DataRow dRow in dt.Rows)
        {
            if (dRow[strColName].ToString().Trim() == strValue)
            {
                retFlag = true;
                break;
            }
        }
        return retFlag;
    }

    public static string FindInDataTable(DataTable dt, string strCol, string strValue, string strRetCol)
    {
        string strRetValue = "";
        foreach (DataRow dRow in dt.Rows)
        {
            if (dRow[strCol].ToString().Trim() == strValue)
            {
                strRetValue = dRow[strRetCol].ToString().Trim();
                break;
            }
        }
        return strRetValue;
    }

    ////Date:07-Jun-2009
    ////Edited By Amit
    ////For Finding Text by Value of Dropdwon List
    //public static string FindInDdlTextData(DropDownList ddl, string strValue)
    //{
    //    string strText = "";
    //    foreach (ListItem itm in ddl.Items)
    //    {
    //        if (string.Compare(itm.Value.ToString(), strValue, true) == 0)
    //        {
    //            strText = itm.Text;
    //            break;
    //        }
    //    }
    //    return strText;
    //}

    // Edited By Amit For Dynamic Menu Load
    public static void GetMenuItem(Menu[] mnu, string userid, string strIsAdmin)
    {
        int i = 0;
        string sql = "";
        DataRow[] mRows;
        DataRow[] cRows;
        DBConnector objDB1 = new DBConnector();
        //sql = "Select v.ViewId,v.ViewName,v.ShowToPage,v.ParentId from ViewInfo v, userprivTMP up, userinfo ui where v.ViewId=up.ViewId AND ui.id=up.id AND up.V='Y' AND ui.Userid='" + userid + "'";
        sql = "Select v.ViewId,v.ViewName,v.ShowToPage,v.ParentId from ViewName v, userprivs up, userinfo ui where v.ViewId=up.VIEWID AND ui.USERID=up.USERID AND up.A='Y' AND v.VIEWID<>1 AND ui.Userid='" + userid.Trim() + "' order by viewid";

        DataTable dtMnuMaster = objDB1.CreateDT(sql, "MnuMaster");
        mRows = FindInMenuItem(dtMnuMaster, "ParentId='0'");
        foreach (DataRow row in mRows)
        {
            if (i == 8)
                return;
            MenuItem masterItem = new MenuItem(row["ViewName"].ToString(), row["ShowToPage"].ToString());
            GetChildMenuItem(dtMnuMaster, row, masterItem);
            mnu[i].Items.Add(masterItem);
            i++;
        }
    }

    public static DataRow[] FindInMenuItem(DataTable dtMnuMaster, string strExpr)
    {
        DataRow[] foundRows;
        foundRows = dtMnuMaster.Select(strExpr);
        return foundRows;
    }

    public static MenuItem GetChildMenuItem(DataTable dtMnuMaster, DataRow row, MenuItem masterItem)
    {
        DataRow[] cRows;
        cRows = null;
        cRows = FindInMenuItem(dtMnuMaster, "ParentId='" + row["ViewId"].ToString() + "'");
        foreach (DataRow rowc in cRows)
        {
            MenuItem childItem = new MenuItem(rowc["ViewName"].ToString(), rowc["ShowToPage"].ToString());
            masterItem.ChildItems.Add(childItem);
            GetChildMenuItem(dtMnuMaster, rowc, childItem);
        }
        return masterItem;
    }

    public static int GetMonthDay(string sMonth, string sYear)
    {
        int intDay = 0;
        //switch (ddlMonth.SelectedValue.ToString())
        switch (sMonth)
        {
            case "1":
            case "01":
            case "3":
            case "03":
            case "5":
            case "05":
            case "7":
            case "07":
            case "8":
            case "08":
            case "10":
            case "12":
                intDay = 31;
                break;
            case "4":
            case "04":
            case "6":
            case "06":
            case "9":
            case "09":
            case "11":
                intDay = 30;
                break;
            case "2":
            case "02":
                //decimal a = Convert.ToDecimal(ddlYear.SelectedValue);
                decimal a = Convert.ToDecimal(sYear);
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

    public static int GetMonthDay(int sMonth, string sYear)
    {
        int intDay = 0;
        //switch (ddlMonth.SelectedValue.ToString())
        switch (sMonth)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                intDay = 31;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                intDay = 30;
                break;
            case 2:
                //decimal a = Convert.ToDecimal(ddlYear.SelectedValue);
                decimal a = Convert.ToDecimal(sYear);
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

    public static void FillMonthList(DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        for(int i=1;i<=12;i++)
        {
            lst = new ListItem();
            switch (i)
            {
                case 1:
                    lst.Text = "January";
                    break;
                case 2:
                    lst.Text = "February";
                    break;
                case 3:
                    lst.Text = "March";
                    break;
                case 4:
                    lst.Text = "April";
                    break;
                case 5:
                    lst.Text = "May";
                    break;
                case 6:
                    lst.Text = "June";
                    break;
                case 7:
                    lst.Text = "July";
                    break;
                case 8:
                    lst.Text = "August";
                    break;
                case 9:
                    lst.Text = "September";
                    break;
                case 10:
                    lst.Text = "October";
                    break;
                case 11:
                    lst.Text = "November";
                    break;
                case 12:
                    lst.Text = "December";
                    break;
            }
            lst.Value = i.ToString();
            ddl.Items.Add(lst);
        }
    }

    public static void FillYearList(Int32 Range,DropDownList ddl)
    {
        ddl.Items.Clear();
        int i = 0;
        int CurrentYear = DateTime.Today.Year;
        for (i = 1; i <= Range*2; i++)
        {
            if(Math.Abs(i)<Range)
                ddl.Items.Add(Convert.ToString(CurrentYear - (Range-i)));
            else if(Math.Abs(i)==Range)
                 ddl.Items.Add(Convert.ToString(CurrentYear));
            else if(Range<Math.Abs(i))
                 ddl.Items.Add(Convert.ToString(CurrentYear + (i - Range)));
        }
    }

    public static void FillYearListBackward(Int32 Range, DropDownList ddl)
    {
        ddl.Items.Clear();
        int i = 0;
        int CurrentYear = DateTime.Today.Year;
        for (i = 1; i <= Range * 2; i++)
        {
            if (Math.Abs(i) < Range)
                ddl.Items.Add(Convert.ToString(CurrentYear - (Range - i)));
            else if (Math.Abs(i) == Range)
                ddl.Items.Add(Convert.ToString(CurrentYear));
        }
    }

    public static string GetMonthNameShort(string strMon)
    {
        string strRetValue = "";
        switch (strMon)
        {
            case "1":
            case "01":
                strRetValue = "Jan";
                break;
            case "2":
            case "02":
                strRetValue = "Feb";
                break;
            case "3":
            case "03":
                strRetValue = "Mar";
                break;
            case "4":
            case "04":
                strRetValue = "Apr";
                break;
            case "5":
            case "05":
                strRetValue = "May";
                break;
            case "6":
            case "06":
                strRetValue = "Jun";
                break;
            case "7":
            case "07":
                strRetValue = "Jul";
                break;
            case "8":
            case "08":
                strRetValue = "Aug";
                break;
            case "9":
            case "09":
                strRetValue = "Sep";
                break;
            case "10":
                strRetValue = "Oct";
                break;
            case "11":
                strRetValue = "Nov";
                break;
            case "12":
                strRetValue = "Dec";
                break;

        }
        return strRetValue;
    }


   public static bool IsWeekendDay(DateTime dtDate,DataTable dtWeekend)
    {
      bool IsWeekendDay=false;
        switch (dtDate.DayOfWeek.ToString())
        {
            case "Sunday":
                if (FindInDataTable(dtWeekend, "Y", "WESun") == true)
                    IsWeekendDay = true;
                break;
            case "Monday":
                if (FindInDataTable(dtWeekend, "Y", "WEMon") == true)
                    IsWeekendDay = true;
                break;
            case "Tuesday":
                if (FindInDataTable(dtWeekend, "Y", "WETues") == true)
                    IsWeekendDay = true;
                break;
            case "Wednesday":
                if (FindInDataTable(dtWeekend, "Y", "WEWed") == true)
                    IsWeekendDay = true;
                break;
            case "Thursday":
                if (FindInDataTable(dtWeekend, "Y", "WETue") == true)
                    IsWeekendDay = true;
                break;
            case "Friday":
                if (FindInDataTable(dtWeekend, "Y", "WEFri") == true)
                    IsWeekendDay = true;
                break;
            case "Saturday":
                if (FindInDataTable(dtWeekend, "Y", "WESat") == true)
                    IsWeekendDay = true;
                break;
        }
        return IsWeekendDay;
    }

    public static decimal RoundDecimal(string strValue, int decPoint)
    {
        if (CheckNullString(strValue) == "")
            strValue = "0";
        decimal decVal = Convert.ToDecimal(strValue.Trim());
        decVal = Math.Round(decVal, decPoint);
        return decVal;
    }

    public static decimal RoundDecimal5T1(string strValue, int decPoint)
    {
        if (CheckNullString(strValue) == "")
            strValue = "0";
        decimal decVal = Convert.ToDecimal(strValue.Trim());
        decVal = Math.Round(decVal, decPoint, MidpointRounding.AwayFromZero);
        return decVal;
    }

    public static string GetPreviousMonth(string strMonth)
    {
        int inMonth = Convert.ToInt32(strMonth);
        if (inMonth == 1)
            return "12";
        else
            strMonth = Convert.ToString(inMonth - 1);

        return strMonth;
    }

    public static string GetGridControlValue(GridViewRow gRow, int inCellIndx, string strCntlName)
    {
        Control c = (Control)gRow.Cells[inCellIndx].FindControl(strCntlName);
        if (c.GetType() == typeof(TextBox))
        {
            TextBox txtB = (TextBox)gRow.Cells[inCellIndx].FindControl(strCntlName);
            return txtB.Text.Trim();
        }
        else if (c.GetType() == typeof(HiddenField))
        {
            HiddenField hfB = (HiddenField)gRow.Cells[inCellIndx].FindControl(strCntlName);
            return hfB.Value;
        }
        else if (c.GetType() == typeof(CheckBox))
        {
            CheckBox chkB = (CheckBox)gRow.Cells[inCellIndx].FindControl(strCntlName);
            return chkB.Checked == true ? "Y" : "N";
        }
        else
            return "";
    }

    public static void HighLightGridControl(GridViewRow gRow, int inCellIndx, string strCntlName)
    {
        Control c = (Control)gRow.Cells[inCellIndx].FindControl(strCntlName);
        if (c.GetType() == typeof(TextBox))
        {
            TextBox txtB = (TextBox)gRow.Cells[inCellIndx].FindControl(strCntlName);
            txtB.ForeColor = System.Drawing.Color.Orange;
        }
        if (c.GetType() == typeof(CheckBox))
        {
            CheckBox chkB = (CheckBox)gRow.Cells[inCellIndx].FindControl(strCntlName);
            chkB.ForeColor = System.Drawing.Color.Orange;
        }
    }
    public static DataTable AddGridRow(GridView gv, string ddlDesigId, string ddlDesigName, string txtPeriod, string Flag, DataTable _datatable) 
    {
        switch (Flag)
        {
            case "Add":
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    if (gv.DataKeys[i].Values[0].ToString() != ddlDesigId) 
                    {
                     _datatable.Rows.Add(gv.DataKeys[i].Values[0].ToString(), gv.DataKeys[i].Values[1].ToString(), gv.DataKeys[i].Values[2].ToString());
                    }
                }
                _datatable.Rows.Add(ddlDesigId, ddlDesigName, txtPeriod);
                break;
            case "Copy":
                for (int i = 0; i < gv.Rows.Count; i++) 
                {
                 _datatable.Rows.Add(gv.DataKeys[i].Values[0].ToString(), gv.DataKeys[i].Values[1].ToString(), gv.DataKeys[i].Values[2].ToString());
                }
                break;
        }
        return _datatable;
    }
    public static string getMaxIdWithCast(string TableName, string ColumnName)
    {
        DBConnector objDB1 = new DBConnector();
        long intRetValue = 0;
        intRetValue = objDB1.GerMaxIDNumberWithCast(TableName, ColumnName);
        return intRetValue.ToString();
    }

    public static string GetMessage(string IsUpdate, string IsDelete)
    {
        string msg = "";
        if (IsUpdate == "N" && IsDelete == "N")
            msg = "Record Saved Successfully";
        else if (IsUpdate == "Y" && IsDelete == "N")
            msg = "Record Updated Successfully";
        else if (IsUpdate == "Y" && IsDelete == "Y")
            msg = "Record Deleted Successfully";

        return msg;
    }

    public static string GetMessage(string CmdType)
    {
        string msg = "";
        if (CmdType == "I")
            msg = "Record Saved Successfully";
        else if (CmdType == "U")
            msg = "Record Updated Successfully";
        else if (CmdType == "D")
            msg = "Record Deleted Successfully";

        return msg;
    }

    public static string CalculateYearMonthDay(string StartDate,int onlyMonth=0)
    {
        int years;

        // compute & return the difference of two dates,
        // returning years, months & days
        // d1 should be the larger (newest) of the two dates

        string dt1 = Common.SetDateTime(DateTime.Now.ToString());
        string dt2 =Common.ReturnDate(StartDate);
        DateTime d1 = Convert.ToDateTime(dt1);
        DateTime d2 = Convert.ToDateTime(dt2);

        string strYear = "";
        string strMonth = "";
        string strDay = "";

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
        string CompleteDay = "";

        CompleteDay = years + " Years " + months + " Months " + days + " Days";
        strYear = years + " Years ";
        strMonth = months + " Months ";
        strDay = days + " Days";
        if (onlyMonth==1)
        {
            return months.ToString();
        }
        return CompleteDay;
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

    public static string CalculateServiceYear(string StartDate,string strEndDate)
    {
        int years;

        // compute & return the difference of two dates,
        // returning years, months & days
        // d1 should be the larger (newest) of the two dates

        string dt1 = Common.ReturnDate(strEndDate);
        string dt2 = Common.ReturnDate(StartDate);
        DateTime d1 = Convert.ToDateTime(dt1);
        DateTime d2 = Convert.ToDateTime(dt2);

        string strYear = "";
        string strMonth = "";
        string strDay = "";

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
        decimal dclCompleteYear;

        dclCompleteYear = years;

        return years.ToString() ;
    }
    public static void FillMonthList_All(DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem lst;
        lst = new ListItem();
        lst.Text = "All";
        lst.Value = "-1";
        ddl.Items.Add(lst);

        for (int i = 1; i <= 12; i++)
        {
            lst = new ListItem();
            switch (i)
            {
                case 1:
                    lst.Text = "January";
                    break;
                case 2:
                    lst.Text = "February";
                    break;
                case 3:
                    lst.Text = "March";
                    break;
                case 4:
                    lst.Text = "April";
                    break;
                case 5:
                    lst.Text = "May";
                    break;
                case 6:
                    lst.Text = "June";
                    break;
                case 7:
                    lst.Text = "July";
                    break;
                case 8:
                    lst.Text = "August";
                    break;
                case 9:
                    lst.Text = "September";
                    break;
                case 10:
                    lst.Text = "October";
                    break;
                case 11:
                    lst.Text = "November";
                    break;
                case 12:
                    lst.Text = "December";
                    break;
            }
            lst.Value = i.ToString();
            ddl.Items.Add(lst);
        }
    }

    public static string CalculateDateDiff(string FromDate, string ToDate)
    {
        int years;

        // compute & return the difference of two dates,
        // returning years, months & days
        // d1 should be the larger (newest) of the two dates


        if (FromDate != "")
        {
            //string dt1 = Common.ReturnDate(ToDate);
            //string dt2 = Common.ReturnDate(FromDate);
            string dt1 = ToDate;
            string dt2 = FromDate;
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
                days = days + 1;
            }
            else
            {
                days = d1.Day - d2.Day;
                days = days + 1;
            }

            // compute years & actual months
            years = months / 12;
            months -= years * 12;
            string CompleteYear = "";
            if (years > 0 && months > 0)
                CompleteYear = years + " Years " + months + " Months " + days + " Days";
            else if (years == 0 && months > 0)
                CompleteYear = months + " Months " + days + " Days";
            else if (years == 0 && months == 0)
                CompleteYear = days + " Days";

            return CompleteYear;
        }
        else
            return "0";
    }


    public static string CalculateTotalDays(string strFromDate, string strToDate)
    {
        DateTime dtFrom=new DateTime ();
        DateTime dtTo=new DateTime ();       
        double TotDay = 0;
        if (string.IsNullOrEmpty(strToDate) == false && string.IsNullOrEmpty(strFromDate) == false)
        {
            char[] splitter = { '/' };
            string[] arinfo = Common.str_split(strFromDate, splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(strToDate, splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;

        }
        return TotDay.ToString() ;
    }
}

