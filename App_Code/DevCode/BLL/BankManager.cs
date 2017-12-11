using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BankManager
/// </summary>
public class BankManager
{
    DBConnector objDC = new DBConnector();
    #region Instert,Update
    public void InsertBankData(string strSLID, string strBankCode, string strBankName, string strBranchName
        , string strBranchCode, string strDistrict, string strIsUpdate,string strDOS)
    {
        string strSQL = "";
        SqlCommand cmd = new SqlCommand();
        if (strIsUpdate == "N")
        {
            strSQL = "INSERT INTO BANKLIST(SLID,BANKCODE,BANKNAME,BRANCHNAME,DISTRICT,ROUTINGNO,DOS) "
                    + " VALUES(@SLID,@BANKCODE,@BANKNAME,@BRANCHNAME,@DISTRICT,@ROUTINGNO,@DOS)";
        }
        else
        {
            strSQL = "UPDATE BANKLIST SET  BANKCODE=@BANKCODE,BANKNAME=@BANKNAME,BRANCHNAME=@BRANCHNAME,"
                + " DISTRICT=@DISTRICT,ROUTINGNO=@ROUTINGNO,DOS=@DOS WHERE SLID=@SLID";
        }
        cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SLID = cmd.Parameters.Add("SLID", SqlDbType.BigInt);
        p_SLID.Direction = ParameterDirection.Input;
        p_SLID.Value = strSLID;

        SqlParameter p_BANKCODE = cmd.Parameters.Add("BANKCODE", SqlDbType.Char);
        p_BANKCODE.Direction = ParameterDirection.Input;
        p_BANKCODE.Value = strBankCode;

        SqlParameter p_BANKNAME = cmd.Parameters.Add("BANKNAME", SqlDbType.VarChar);
        p_BANKNAME.Direction = ParameterDirection.Input;
        p_BANKNAME.Value = strBankName;

        SqlParameter p_BRANCHNAME = cmd.Parameters.Add("BRANCHNAME", SqlDbType.Char);
        p_BRANCHNAME.Direction = ParameterDirection.Input;
        p_BRANCHNAME.Value = strBranchName;

        SqlParameter p_DISTRICT = cmd.Parameters.Add("DISTRICT", SqlDbType.Char);
        p_DISTRICT.Direction = ParameterDirection.Input;
        p_DISTRICT.Value = strDistrict;

        SqlParameter p_ROUTINGNO = cmd.Parameters.Add("ROUTINGNO", SqlDbType.Char);
        p_ROUTINGNO.Direction = ParameterDirection.Input;
        p_ROUTINGNO.Value = strBranchCode;

        SqlParameter p_DOS = cmd.Parameters.Add("DOS", SqlDbType.Char);
        p_DOS.Direction = ParameterDirection.Input;
        p_DOS.Value = strDOS;

        objDC.ExecuteQuery(cmd);
    }

    
    #endregion
    #region Select
    public DataTable GetDistinctBank()
    {
        string strSQL = "SELECT DISTINCT BANKCODE,BANKNAME FROM BANKLIST ORDER BY BANKNAME";
        return objDC.CreateDT(strSQL, "DistinctBankName");
    }


    public DataTable GetBankData(string strBankCode)
    {
        string strSQL="";
        if(strBankCode != "-1")
            strSQL = "SELECT * FROM BANKLIST WHERE BANKCODE=@BANKCODE ORDER BY BANKNAME,BRANCHNAME ";
        else
            strSQL = "SELECT * FROM BANKLIST ORDER BY BANKNAME,BRANCHNAME ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        if (strBankCode != "-1")
        {
            SqlParameter p_BANKCODE = cmd.Parameters.Add("BANKCODE", SqlDbType.Char);
            p_BANKCODE.Direction = ParameterDirection.Input;
            p_BANKCODE.Value = strBankCode;
        }
        return objDC.CreateDT(cmd, "BANKDATA");
    }

    public bool IsDataExist(string strRoutingNo,string strSLId)
    {
        string strRetValue = "";
        string strSQL="";

        if (strSLId == "0" || strSLId == "")
            strSQL = "SELECT ROUTINGNO FROM BANKLIST WHERE ROUTINGNO=@ROUTINGNO";
        else
            strSQL = "SELECT ROUTINGNO FROM BANKLIST WHERE ROUTINGNO=@ROUTINGNO AND SLId<>@SLId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_ROUTINGNO = cmd.Parameters.Add("ROUTINGNO", SqlDbType.Char);
        p_ROUTINGNO.Direction = ParameterDirection.Input;
        p_ROUTINGNO.Value = strRoutingNo;

        if (strSLId != "0" && strSLId != "")
        {
            SqlParameter p_SLId = cmd.Parameters.Add("SLId", SqlDbType.BigInt);
            p_SLId.Direction = ParameterDirection.Input;
            p_SLId.Value = strSLId;
        }
        strRetValue = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strRetValue) == true)
            return false;
        else
            return true;
    }
    #endregion

    public BankManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
