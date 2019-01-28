using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ENKService
{

    [DataContract]
    public class SLoginHistory
    {
        string ipaddress1 = "";
        string ipaddress2 = "";
        string ipaddress3 = "";
        string browser = "";
        string location = "";       
        Int32 loginID = 0;
        DateTime? loginTime;
        string ipDetail;
        string browser1="";

        [DataMember]
        public string Browser1
        {
            get { return browser1; }
            set { browser1 = value; }
        }

        [DataMember]
        public string IpDetail
        {
            get { return ipDetail; }
            set { ipDetail = value; }
        }

        [DataMember]
        public string IpAddress1
        {
            get { return ipaddress1; }
            set { ipaddress1 = value; }
        }

        [DataMember]
        public string IpAddress2
        {
            get { return ipaddress2; }
            set { ipaddress2 = value; }
        }
        [DataMember]
        public string IpAddress3
        {
            get { return ipaddress3; }
            set { ipaddress3 = value; }
        }

        [DataMember]
        public string BrowserName
        {
            get { return browser; }
            set { browser = value; }
        }

        [DataMember]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        [DataMember]
        public Int32 LoginID
        {
            get { return loginID; }
            set { loginID = value; }
        }

        [DataMember]
        public DateTime? LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }
        
    }
}