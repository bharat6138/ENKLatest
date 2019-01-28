using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ENKService
{
    [DataContract]
    public class SUsers
    {
        Int32 UserID = 0;
        Int32 DistributorID = 0;
        Int32 RoleID = 0;
        string UserName = "";
        string FirstName = "";
        string LastName = "";
        string Pwd = "";
        string ContactNo = "";
        DateTime? ActiveFrom;
        DateTime? ActiveTo;
        string EmailID;
        bool IsActive = true;
        string UserType = "";

        [DataMember]
        public string userType
        {
            get { return UserType; }
            set { UserType = value; }
        }



        [DataMember]
        public DateTime? activeFrom
        {
            get { return ActiveFrom; }
            set { ActiveFrom = value; }
        }

        [DataMember]
        public DateTime? activeTo
        {
            get { return ActiveTo; }
            set { ActiveTo = value; }
        }

        [DataMember]
        public int userID
        {
            get { return UserID; }
            set { UserID = value; }
        }

        [DataMember]
        public int distributorID
        {
            get { return DistributorID; }
            set { DistributorID = value; }
        }

        [DataMember]
        public int roleID
        {
            get { return RoleID; }
            set { RoleID = value; }
        }

        [DataMember]
        public string userName
        {
            get { return UserName; }
            set { UserName = value; }
        }

        [DataMember]
        public string firstName
        {
            get { return FirstName; }
            set { FirstName = value; }
        }

        [DataMember]
        public string lastName
        {
            get { return LastName; }
            set { LastName = value; }
        }

        [DataMember]
        public string pwd
        {
            get { return Pwd; }
            set { Pwd = value; }
        }

        [DataMember]
        public bool isActive
        {
            get { return IsActive; }
            set { IsActive = value; }
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
    }
}