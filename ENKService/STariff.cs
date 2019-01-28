using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace ENKService
{
    [DataContract]
    public class STariff
    {

        int tarifID = 0;
        string tariffCode = "";
        string description = "";
        int validDays = 0;
        double rental = 0.0;
        int frequency = 0;
        int _TariffType = 0;
        bool isActive = true;
        string tarifName = "";
        double discount_on_Activation_PortIn = 0.0;
        double discount_on_Recharge = 0.0;
        int sellerID = 0;
        int networkID = 0;

        int TariffId;             //Added By Sarala
        double comission = 0;   //Added By Sarala
        string groupName = "";  //Added By Sarala
        int tariffGroupid = 0;  //Added By Sarala
        double H2oGeneralDiscount = 0;   //Added By Akash


        [DataMember]  //Added By Sarala
        public int TariffID
        {
            get { return TariffId; }
            set { TariffId = value; }
        }
        [DataMember]  //Added By Sarala
        public double Comission
        {
            get { return comission; }
            set { comission = value; }
        }
        [DataMember]  //Added By Sarala
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }
        [DataMember]  //Added By Sarala
        public int TariffGroupId
        {
            get { return tariffGroupid; }
            set { tariffGroupid = value; }
        }
        [DataMember]  //Added By Sarala
        public DataTable dtSpiffDetail { get; set; }

        [DataMember]
        public string TarifName
        {
            get { return tarifName; }
            set { tarifName = value; }
        }
        [DataMember]
        public double Discount_on_Activation_PortIn
        {
            get { return discount_on_Activation_PortIn; }
            set { discount_on_Activation_PortIn = value; }
        }
        [DataMember]
        public double Discount_on_Recharge
        {
            get { return discount_on_Recharge; }
            set { discount_on_Recharge = value; }
        
        }
        [DataMember]
        public int SellerID
        {
            get { return sellerID; }
            set { sellerID = value; }

        }
        [DataMember]
        public int NetworkID
        {
            get { return networkID; }
            set { networkID = value; }
        }

        [DataMember]
        public int TarifID
        {
            get { return tarifID; }
            set { tarifID = value; }
        }

        [DataMember]
        public int TariffType
        {
            get { return _TariffType; }
            set { _TariffType = value; }
        }

        [DataMember]
        public string TariffCode
        {
            get { return tariffCode; }
            set { tariffCode = value; }
        }

        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public int ValidDays
        {
            get { return validDays; }
            set { validDays = value; }
        }

        [DataMember]
        public double Rental
        {
            get { return rental; }
            set { rental = value; }
        }

        [DataMember]
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        [DataMember]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DataMember]  //Added By Akash
        public double H2OGeneralDiscount
        {
            get { return H2oGeneralDiscount; }
            set { H2oGeneralDiscount = value; }
        }


    }
}