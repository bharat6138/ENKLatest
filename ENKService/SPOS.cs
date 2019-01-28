using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace ENKService
{
     [DataContract]
    public class SPOS
    {
        Int64 mSISDN_SIM_ID = 0;
        string customername = "";
        string email = "";
        string address = "";
        string mobilenumber = "";

      
        [DataMember]
        public Int64 MSISDN_SIM_ID
        {
            get { return mSISDN_SIM_ID; }
            set { mSISDN_SIM_ID = value; }
        }
        [DataMember]
        public string CoustomerName
        {
            get { return customername; }
            set { customername = value; }
        }
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        [DataMember]
        public string MobileNumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value; }
        }
    }
}