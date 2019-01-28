using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ENKService
{
    [DataContract]
    public class SBranch
    {

        Int32 BranchID = 0;
        string BranchName = "";
        Int32 BranchCode = 0;
        Int32 BranchType = 0;
        string BranchTypeName = "";
        Int32 BranchParent = 0;

        string ContactPerson = "";
        string ContactNo = "";
        string WebSiteName = "";
        string EmailID = "";

        string Address = "";
        string City = "";
        string State = "";
        string Zip = "";
        int CountryId = 0;

        bool IsServiceTaxExmpted = true;
        double BalanceAmount = 0.0;
        bool IsActive = true;

       

        [DataMember]
        public Int32 branchID
        {
            get { return BranchID; }
            set { BranchID = value; }
        }


        [DataMember]
        public string branchName
        {
            get { return BranchName; }
            set { BranchName = value; }
        }

        [DataMember]
        public Int32 branchCode
        {
            get { return BranchCode; }
            set { BranchCode = value; }
        }


        [DataMember]
        public Int32 branchType
        {
            get { return BranchType; }
            set { BranchType = value; }
        }

        [DataMember]
        public string branchTypeName
        {
            get { return BranchTypeName; }
            set { BranchTypeName = value; }
        }

        [DataMember]
        public Int32 branchparent
        {
            get { return BranchParent; }
            set { BranchParent = value; }
        }

        [DataMember]
        public string contactPerson
        {
            get { return ContactPerson; }
            set { ContactPerson = value; }
        }

        [DataMember]
        public string contactNo
        {
            get { return ContactNo; }
            set { ContactNo = value; }
        }

        [DataMember]
        public string emailID
        {
            get { return EmailID; }
            set { EmailID = value; }
        }

        [DataMember]
        public int countryid
        {
            get { return CountryId; }
            set { CountryId = value; }
        }

        [DataMember]
        public string address
        {
            get { return Address; }
            set { Address = value; }
        }

        [DataMember]
        public string city
        {
            get { return City; }
            set { City = value; }
        }

        [DataMember]
        public string state
        {
            get { return State; }
            set { State = value; }
        }

        [DataMember]
        public string zip
        {
            get { return Zip; }
            set { Zip = value; }
        }

        [DataMember]
        public string webSiteName
        {
            get { return WebSiteName; }
            set { WebSiteName = value; }
        }

        [DataMember]
        public bool isServiceTaxExmpted
        {
            get { return IsServiceTaxExmpted; }
            set { IsServiceTaxExmpted = value; }
        }

        [DataMember]
        public bool isActive
        {
            get { return IsActive; }
            set { IsActive = value; }
        }


        [DataMember]
        public double balanceAmount
        {
            get { return BalanceAmount; }
            set { BalanceAmount = value; }
        }


    }
}