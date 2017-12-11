using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsTrainingService
/// </summary>
public class clsTrainingService
{

        public string TraServiceID  { get; set; }
		public string EmpId         { get; set; }
		public string TrainType      { get; set; }
        public string FiscalYrID { get; set; }

		public string TrainingID    { get; set; }
		public string LAreaId       { get; set; }
		public string ResourcePersonId { get; set; }
		public string CountryID     { get; set; }
		public string ContactDtl    { get; set; }
		public string TrnStartDate  { get; set; }
		public string TrnEndDate    { get; set; }
		public string Remarks       { get; set; }
		public string NeedType      { get; set; }
		public string RunningRate   { get; set; }
		public string RateToUse     { get; set; }
		public string ServAgreement { get; set; }

		public string AgrStartDate  { get; set; }
		public string AgrEndDate    { get; set; }
		public string AgrPeriod     { get; set; }
		public string EstAgrAmtBDT  { get; set; }
		public string EstAgrAmtUSD  { get; set; }
		public string ActAgrAmtBDT  { get; set; }
		public string ActAgrAmtUSD  { get; set; }
		public string AgrRemarks    { get; set; }

		public string TrainingCostBDT { get; set; }
		public string TrainingCostUSD { get; set; }
		public string SponsoredBy   { get; set; }
		public string SCCostPercent { get; set; }
		public string SCCostBDT     { get; set; }
		public string SCCostUSD     { get; set; }
		public string OtherCostPercent { get; set; }
		public string OtherCostPerBDT  { get; set; }
        public string OtherCostPerUSD  { get; set; }
}