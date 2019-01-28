using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace ENKService
{
    [DataContract]
    public class SIM
    {
        [DataMember]
        public int DistributorID { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int VendorID { get; set; }
        [DataMember]
        public DateTime PurchaseDate { get; set; }
        [DataMember]
        public String PurchaseNo { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public int BranchID { get; set; }
        [DataMember]
        public DataTable MobileDT { get; set; }
        [DataMember]
        public DataTable SIMDt { get; set; }
        [DataMember]
        public string SIMNo { get; set; }
        [DataMember]
        public string TransferType { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int NewClientID { get; set; }
        [DataMember]
        public long MSISDN_SIM_ID { get; set; }
        [DataMember]
        public int TariffID { get; set; }



    }
}