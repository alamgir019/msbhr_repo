using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsClinicType
/// </summary>
public class clsClinicType
{


    public clsClinicType(string strClinicTypeId, string strClinicTypeName, string strIsActive, string strIsDeleted, string strInsertedBy, string strInsertedDate,string strUpdatedBy, string strUpdatedDate,string strLastUpdatedFrom)
	    {
            this.ClinicTypeId = strClinicTypeId;
            this.ClinicTypeName = strClinicTypeName;
            this.IsActive = strIsActive;
            this.IsDeleted = strIsDeleted;
            this.InsertedBy = strInsertedBy;
            this.InsertedDate = strInsertedDate;
            this.UpdatedBy = strUpdatedBy;
            this.UpdatedDate = strUpdatedDate;
            this.LastUpdatedFrom = strLastUpdatedFrom;
	    }

     private string _ClinicTypeId;
     private string _ClinicTypeName;
     private string _IsActive;
     private string _IsDeleted;
     private string _InsertedBy;
     private string _InsertedDate;
     private string _LastUpdatedFrom;
     private string _UpdatedBy;
     private string _UpdatedDate;

     public string ClinicTypeId     {get { return _ClinicTypeId;    }    set { _ClinicTypeId = value;    }}
     public string ClinicTypeName   {get { return _ClinicTypeName;  }    set { _ClinicTypeName = value;  }}
     public string IsActive         {get { return _IsActive;        }    set { _IsActive = value;        }}
     public string IsDeleted        {get { return _IsDeleted;       }    set { _IsDeleted = value;       }}
     public string InsertedBy       {get { return _InsertedBy;      }    set { _InsertedBy = value;      }}
     public string InsertedDate     {get { return _InsertedDate;    }    set { _InsertedDate = value;    }}
     public string LastUpdatedFrom  {get { return _LastUpdatedFrom; }    set { _LastUpdatedFrom = value; }}
     public string UpdatedBy        {get { return _UpdatedBy;       }    set { _UpdatedBy = value;       }}
     public string UpdatedDate      {get { return _UpdatedDate;     }    set { _UpdatedDate = value;     }}
}
