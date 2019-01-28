using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace ENKService
{
   
    [DataContract]
    public class sTariffSpiffMapping
    {
         //int TariffId;
         //double Spiff1 = 0;
         //double Spiff2 = 0;
         //double Spiff3 = 0;
         //double Spiff4 = 0;
         //double Spiff5 = 0;
         //double Spiff6 = 0;
         //double Spiff7 = 0;
         //double Spiff8 = 0;
         //double Spiff9 = 0;
         //double Spiff10 = 0;
         //double Spiff11 = 0;
         //double Spiff12 = 0;

         [DataMember]
         public int TariffId
         {
             get;
             set;
         }
        [DataMember]
        public double Spiff1
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff2
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff3
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff4
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff5
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff6
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff7
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff8
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff9
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff10
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff11
        {
            get;
            set;
        }
        [DataMember]
        public double Spiff12
        {
            get;
            set;
        }
    }
}