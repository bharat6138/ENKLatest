using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ENKService
{
    [DataContract]
    public class Distributor
    {
        Int32 LoginID = 0;
        public Int32 DistributorID = 0;
        string DistributorName = "";
        Int32 DistributorCode = 0;
        Int32 CompanyType = 0;
        string CompanyTypeName = "";
        Int32 Parent = 0;
        Int32 RelationManager = 0;
        string VatNo = "";
        double VatPer = 0;
        string ServiceTAxNo = "";
        double ServiceTAxPer = 0;
        string ContactPerson = "";
        string ContactNo = "";
        string WebSiteName = "";
        string EmailID = "";

        string Address = "";
        string City = "";
        string State = "";
        string Zip = "";
        int CountryId = 0;
        string ParentDistributor = "";
        int Noofblank = 0;
        int NoofActivation = 0;
        bool IsServiceTaxExmpted = true;

        //ANKIT SINGH
        //double BalanceAmount = 0.0;
        decimal BalanceAmount = Convert.ToDecimal(0.00);
        //
        bool IsActive = true;
        string password = "";
        string _EIN = "";
        string _SSN = "";
        string _PanNumber = "";
        int tariffGroupID = 0;
        string _document = "";
        string _certificate = "";
        string isHold = "";
        string _createddate = "";
        string _modifieddate = "";

        [DataMember]
        public Int32 TariffGroupID
        {
            get { return tariffGroupID; }
            set { tariffGroupID = value; }
        }


        [DataMember]
        public Int32 loginID
        {
            get { return LoginID; }
            set { LoginID = value; }
        }

        [DataMember]
        public Int32 distributorID
        {
            get { return DistributorID; }
            set { DistributorID = value; }
        }


        [DataMember]
        public string distributorName
        {
            get { return DistributorName; }
            set { DistributorName = value; }
        }

        [DataMember]
        public Int32 distributorCode
        {
            get { return DistributorCode; }
            set { DistributorCode = value; }
        }


        [DataMember]
        public Int32 companyType
        {
            get { return CompanyType; }
            set { CompanyType = value; }
        }

        [DataMember]
        public string companyTypeName
        {
            get { return CompanyTypeName; }
            set { CompanyTypeName = value; }
        }

        [DataMember]
        public Int32 parent
        {
            get { return Parent; }
            set { Parent = value; }
        }

        [DataMember]
        public Int32 relationManager
        {
            get { return RelationManager; }
            set { RelationManager = value; }
        }

        [DataMember]
        public string vatNo
        {
            get { return VatNo; }
            set { VatNo = value; }
        }

        [DataMember]
        public double vatPer
        {
            get { return VatPer; }
            set { VatPer = value; }
        }

        [DataMember]
        public string serviceTAxNo
        {
            get { return ServiceTAxNo; }
            set { ServiceTAxNo = value; }
        }

        [DataMember]
        public double serviceTAxPer
        {
            get { return ServiceTAxPer; }
            set { ServiceTAxPer = value; }
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
        public decimal balanceAmount
        {
            get { return BalanceAmount; }
            set { BalanceAmount = value; }
        }

        [DataMember]
        public string parentDistributor
        {
            get { return ParentDistributor; }
            set { ParentDistributor = value; }
        }

        [DataMember]
        public Int32 NoOfBlankSim
        {
            get { return Noofblank; }
            set { Noofblank = value; }
        }

        [DataMember]
        public Int32 NoOfActivation
        {
            get { return NoofActivation; }
            set { NoofActivation = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string EIN
        {
            get { return _EIN; }
            set { _EIN = value; }
        }

        [DataMember]
        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }

        [DataMember]
        public string PanNumber
        {
            get { return _PanNumber; }
            set { _PanNumber = value; }
        }

        
        [DataMember]
        public string Document
        {
            get { return _document; }
            set { _document = value; }
        }

        [DataMember]
        public string Certificate
        {
            get { return _certificate; }
            set { _certificate = value; }
        }

        [DataMember]
        public string Holdstatus
        {
            get { return isHold; }
            set { isHold = value; }
        }

        [DataMember]
        public string CreatedDate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }

        [DataMember]
        public string ModifiedDate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
        }
    }
}