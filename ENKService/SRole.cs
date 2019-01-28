using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ENKService
{
    public class SRole
    {

        Int32 roleID = 0;

        string roleName = "";
        bool isActive = true;

        [DataMember]
        public Int32 RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        [DataMember]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        [DataMember]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }
}