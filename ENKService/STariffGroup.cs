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
    public class STariffGroup
    {
        int TariffId;
        double comission = 0;
        string groupName;

        [DataMember]
        public int TariffID
        {
            get { return TariffId; }
            set { TariffId = value; }
        }
        [DataMember]
        public double Comission
        {
            get { return comission; }
            set { comission = value; }
        }
        [DataMember]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

    }
}