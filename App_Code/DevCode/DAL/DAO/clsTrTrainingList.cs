using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for clsTrTrainingList
/// </summary>
public class clsTrTrainingList
{

    private string _TrainId;
    private string _TrainName;
    private string _TrCategoryId;
    private string _TentativeDay;
    private string _IsInHouse;
    private string _IsMedicos;
    private string _IndvCost;
    private string _InsertedBy;
    private string _InsertedDate;
    private string _IndvIncome;

    public string TrainId       { get { return _TrainId;      } set { _TrainId        = value; }}
    public string TrainName     { get { return _TrainName;    } set { _TrainName      = value; }}
    public string TrCategoryId  { get { return _TrCategoryId; } set { _TrCategoryId   = value; }}
    public string TentativeDay  { get { return _TentativeDay; } set { _TentativeDay   = value; }}
    public string IsInHouse     { get { return _IsInHouse;    } set { _IsInHouse      = value; }}
    public string IsMedicos     { get { return _IsMedicos;    } set { _IsMedicos      = value; }}
    public string IndvCost      { get { return _IndvCost;     } set { _IndvCost       = value; }}
    public string IndvIncome    { get { return _IndvIncome;   } set { _IndvIncome     = value; }}
    public string InsertedBy    { get { return _InsertedBy;   } set { _InsertedBy     = value; }}
    public string InsertedDate  { get { return _InsertedDate; } set { _InsertedDate   = value; }}


    public clsTrTrainingList(string TrainId,
                            string TrainName,
                            string TrCategoryId,
                            string TentativeDay,
                            string IsInHouse,
                            string IsMedicos,
                            string IndvCost,
                            string IndvIncome,
                            string InsertedBy,
                            string InsertedDate
                         )
	                    { 
                            this.TrainId = TrainId;
                            this.TrainName = TrainName;
                            this.TrCategoryId = TrCategoryId;
                            this.TentativeDay = TentativeDay;
                            this.IsInHouse = IsInHouse;
                            this.IsMedicos = IsMedicos;
                            this.IndvCost = IndvCost;
                            this.IndvIncome = IndvIncome;
                            this.InsertedBy = InsertedBy;
                            this.InsertedDate = InsertedDate; 
	                    }

}