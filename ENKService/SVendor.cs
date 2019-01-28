using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace ENKService
{
    [DataContract]
    public class SVendor
    {
        [DataMember]
        public int VendorID { get; set; }

        [DataMember]
        public string VendorCode { get; set; }

        [DataMember]
        public string VendorName { get; set; }

        [DataMember]
        public string VendorEmail { get; set; }

        [DataMember]
        public string VendorContactPerson { get; set; }

        [DataMember]
        public string VendorAddress { get; set; }

        [DataMember]
        public string VendorMobile { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
         
    }
}