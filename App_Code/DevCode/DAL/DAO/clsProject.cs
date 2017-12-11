using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsProject
/// </summary>
public class clsProject
{

    public clsProject(string strProjectId, string strProjectName, string strProjectCode, string strStartDate, string strEndDate,string strWeekEndID,
         string strIncrementType,string  strIncrementMonth,string strIncrementAfter,string strIsActive,
         string strIsDeleted,string strInsertedBy, string strInsertedDate)
	        {
                this.ProjectId = strProjectId;
                this.ProjectName = strProjectName;
                this.ProjectCode = strProjectCode;
                this.StartDate = strStartDate;
                this.EndDate = strEndDate;
                this.WeekEndID = strWeekEndID;
                this.IncrementType = strIncrementType;
                this.IncrementMonth = strIncrementMonth;
	            this.IncrementAfter = strIncrementAfter;                
                this.IsActive = strIsActive;
                this.IsDeleted = strIsDeleted;
                this.InsertedBy = strInsertedBy;
                this.InsertedDate = strInsertedDate;
	        }

        private string _ProjectId;
        private string _ProjectName;
        private string _ProjectCode;
        private string _StartDate;
        private string _EndDate;
   
        private string _WeekEndID;
        private string _IncrementType;
        private string _IncrementMonth;
        private string _IncrementAfter;      
        private string _IsActive;
        private string _IsDeleted;
        private string _InsertedBy;
        private string _InsertedDate;

        public string ProjectId { get { return _ProjectId; } set { _ProjectId = value; } }
        public string ProjectName { get { return _ProjectName; } set { _ProjectName = value; }}
        public string ProjectCode { get { return _ProjectCode; } set { _ProjectCode = value; } }
        public string StartDate{get { return _StartDate; } set { _StartDate = value; } }
        public string EndDate{get { return _EndDate; } set { _EndDate = value; } }
        public string WeekEndID{get { return _WeekEndID; } set { _WeekEndID = value; } }
        public string IncrementType{get { return _IncrementType; } set { _IncrementType = value; } }
        public string IncrementMonth{get { return _IncrementMonth; } set { _IncrementMonth = value; } }
        public string IncrementAfter{get { return _IncrementAfter; } set { _IncrementAfter = value; } }       
        public string IsActive {get { return _IsActive; } set { _IsActive = value; }}
        public string IsDeleted { get { return _IsDeleted; }set { _IsDeleted = value; }}
        public string InsertedBy { get { return _InsertedBy; } set { _InsertedBy = value; } }
        public string InsertedDate {get { return _InsertedDate; } set { _InsertedDate = value; }}
}