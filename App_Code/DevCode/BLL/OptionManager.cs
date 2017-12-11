using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for LeaveManager
/// </summary>
public class OptionManager
{

    DBConnector objDC = new DBConnector();
    #region Insert Update Delete From Tables By Store procedure
     //Insert or Update  or Delete Data of Location table

    //public void InsertOptionBag(clsOptionBag opt)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_OptionBag");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_SbuId = command.Parameters.Add("SbuId", SqlDbType.BigInt);
    //    p_SbuId.Direction = ParameterDirection.Input;
    //    p_SbuId.Value = opt.SbuId;

    //    SqlParameter p_OptID = command.Parameters.Add("OptID", SqlDbType.Char);
    //    p_OptID.Direction = ParameterDirection.Input;
    //    p_OptID.Value = opt.OptID;

    //    SqlParameter p_OptOThour3Rate = command.Parameters.Add("OptOThour3Rate", SqlDbType.Decimal);
    //    p_OptOThour3Rate.Direction = ParameterDirection.Input;
    //    p_OptOThour3Rate.Value = opt.OptOThour3Rate;

    //    SqlParameter p_OptOThour4Rate = command.Parameters.Add("OptOThour4Rate", SqlDbType.Decimal);
    //    p_OptOThour4Rate.Direction = ParameterDirection.Input;
    //    p_OptOThour4Rate.Value = opt.OptOThour4Rate;

    //    SqlParameter p_OptOThour6Rate = command.Parameters.Add("OptOThour6Rate", SqlDbType.Decimal);
    //    p_OptOThour6Rate.Direction = ParameterDirection.Input;
    //    p_OptOThour6Rate.Value = opt.OptOThour6Rate;

    //    SqlParameter p_OptOThour3RateHoli = command.Parameters.Add("OptOThour3RateHoli", SqlDbType.Decimal);
    //    p_OptOThour3RateHoli.Direction = ParameterDirection.Input;
    //    if (opt.OptOThour3RateHoli != "")
    //        p_OptOThour3RateHoli.Value = opt.OptOThour3RateHoli;
    //    else
    //        p_OptOThour3RateHoli.Value = 0;

    //    SqlParameter p_OptOThour4RateHoli = command.Parameters.Add("OptOThour4RateHoli", SqlDbType.Decimal);
    //    p_OptOThour4RateHoli.Direction = ParameterDirection.Input;
    //    if (opt.OptOThour4RateHoli != "")
    //        p_OptOThour4RateHoli.Value = opt.OptOThour4RateHoli;
    //    else
    //        p_OptOThour4RateHoli.Value = 0;

    //    SqlParameter p_OptOThour6RateHoli = command.Parameters.Add("OptOThour6RateHoli", SqlDbType.Decimal);
    //    p_OptOThour6RateHoli.Direction = ParameterDirection.Input;
    //    if (opt.OptOThour6RateHoli != "")
    //        p_OptOThour6RateHoli.Value = opt.OptOThour6RateHoli;
    //    else
    //        p_OptOThour6RateHoli.Value = 0;

    //    SqlParameter p_BreakFastTK = command.Parameters.Add("BreakFastTK", SqlDbType.Decimal);
    //    p_BreakFastTK.Direction = ParameterDirection.Input;
    //    p_BreakFastTK.Value = opt.BreakFastTK;

    //    SqlParameter p_LunchTK = command.Parameters.Add("LunchTK", SqlDbType.Decimal);
    //    p_LunchTK.Direction = ParameterDirection.Input;
    //    p_LunchTK.Value = opt.LunchTK;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        command = null;
    //    }
    //}

   
#endregion 
   
    
    #region Select Queries From Tables By store procedure
    //Select Option Bag

    public DataTable SelectOptionBag(string OptId)
    {
        SqlCommand command = new SqlCommand("proc_Select_OptionBag");
       
        SqlParameter p_OptId = command.Parameters.Add("OptID", SqlDbType.Char);
        p_OptId.Direction = ParameterDirection.Input;
        p_OptId.Value = OptId;

        objDC.CreateDSFromProc(command, "OptionBag");
        return objDC.ds.Tables["OptionBag"];
    }
   


#endregion
}