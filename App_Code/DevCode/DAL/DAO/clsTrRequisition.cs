using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsTrRequisition
/// </summary>
public class clsTrRequisition
{

    private string _ReqID;
    private string _ScheduleID;
    private string _TrainId;
    private string _SignDesig1;
    private string _SignDesig2;
    private string _SeenBy;
    private string _ReviewBy;
    private string _RecommendBy;
    private string _ApprovBy;
    private string _IsActive;
    private string _Remarks;

    public string ReqID       { get { return _ReqID;      } set { _ReqID = value;      } }
    public string ScheduleID  { get { return _ScheduleID; } set { _ScheduleID = value; } }
    public string TrainId     { get { return _TrainId;    } set { _TrainId = value;    } }
    public string SignDesig1  { get { return _SignDesig1; } set { _SignDesig1 = value; } }
    public string SignDesig2  { get { return _SignDesig2; } set { _SignDesig2 = value; } }
    public string SeenBy      { get { return _SeenBy;     } set { _SeenBy = value;     } }
    public string ReviewBy    { get { return _ReviewBy;   } set { _ReviewBy = value;   } }
    public string RecommendBy { get { return _RecommendBy;} set { _RecommendBy = value;} }
    public string  ApprovBy   { get { return _ApprovBy;   } set { _ApprovBy = value;   } }
    public string IsActive    { get { return _IsActive;   } set { _IsActive = value;   } }
    public string Remarks     { get { return _Remarks;    } set { _Remarks = value; } }

     public clsTrRequisition( string ReqID,string ScheduleID,string TrainId,string SignDesig1,string SignDesig2,string SeenBy,
                              string ReviewBy,string RecommendBy,string ApprovBy,string IsActive,string Remarks)
                             {
                                 this.ReqID = ReqID;
                                 this.ScheduleID = ScheduleID;
                                 this.TrainId = TrainId;
                                 this.SignDesig1 = SignDesig1;
                                 this.SignDesig2 = SignDesig2;
                                 this.SeenBy = SeenBy;
                                 this.ReviewBy = ReviewBy;
                                 this.RecommendBy = RecommendBy;
                                 this.ApprovBy = ApprovBy;
                                 this.IsActive = IsActive;
                                 this.Remarks = Remarks;
                             }

}