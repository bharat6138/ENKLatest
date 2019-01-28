using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
   public class DBUsers:DataBase
    {

      public Int32 UserID = 0;
      public Int32 DistributorID = 0;
      public Int32 RoleID = 0;
      public string UserName = "";
      public string FirstName = "";
      public string LastName = "";
      public string Pwd = "";
      public string ContactNo = "";
      public DateTime? ActiveFrom;
      public DateTime? ActiveTo;
      public string EmailID;
      public bool IsActive = true;


        public DataSet ValidateLogin(string UserName, string Pass)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spLogin"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = UserName;
                    objCmd.Parameters.Add("@Pass", SqlDbType.VarChar).Value = Pass;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ValidateLoginApp(Int64 LoginID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spLogin_ForApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginId", SqlDbType.BigInt).Value = LoginID;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ValidateLoginApp(string UserName, string Pass, string DeviceTokenID, string DeviceType)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spLoginApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = UserName;
                    objCmd.Parameters.Add("@Pass", SqlDbType.VarChar).Value = Pass;
                    objCmd.Parameters.Add("@DeviceTokenID", SqlDbType.VarChar).Value = DeviceTokenID;
                    objCmd.Parameters.Add("@DeviceType", SqlDbType.VarChar).Value = DeviceType;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUser(int LoginID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = RoleID;
                cmd.Parameters.Add("@Fname", SqlDbType.VarChar).Value = FirstName;
                cmd.Parameters.Add("@Lname", SqlDbType.VarChar).Value = LastName;
                cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = Pwd;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                cmd.Parameters.Add("@ActiveFrom", SqlDbType.DateTime).Value = ActiveFrom;
                cmd.Parameters.Add("@ActiveTo", SqlDbType.DateTime).Value = ActiveTo;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;               
                cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;

                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@CreatedDt", SqlDbType.DateTime).Value = DateTime.Now;

                int a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateUser(int LoginID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = UserID;
                cmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = RoleID;
                cmd.Parameters.Add("@Fname", SqlDbType.VarChar).Value = FirstName;
                cmd.Parameters.Add("@Lname", SqlDbType.VarChar).Value = LastName;
                cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = Pwd;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                cmd.Parameters.Add("@ActiveFrom", SqlDbType.DateTime).Value = ActiveFrom;
                cmd.Parameters.Add("@ActiveTo", SqlDbType.DateTime).Value = ActiveTo;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;                 
                cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;

                cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ModifiedDt", SqlDbType.DateTime).Value = DateTime.Now;

                int a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetUserList(int LoginID, int DistbutorID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetUserList"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = DistbutorID;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetUser(int usrID, int LoginID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetUser"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = usrID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                     
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet VerifyUserID(int DistributorID, int ClientTypeID, int LoginID, string UserID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spVerifyUser"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                  
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;

                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ForgetPassword(string UserID, string Mobile)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spForgetPassword"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
                    objCmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = Mobile;
                     

                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ResetPassword(string Userid, string OldPass, string NewPass, int Distributorid, int Loginid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spResetPassword"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Userid", SqlDbType.VarChar).Value = Userid;
                    cmd.Parameters.Add("@OldPass", SqlDbType.VarChar).Value = OldPass;
                    cmd.Parameters.Add("@NewPass", SqlDbType.VarChar).Value = NewPass;
                    cmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;
                    cmd.Parameters.Add("@Loginid", SqlDbType.BigInt).Value = Loginid;


                    return ReturnDataSet(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet FetchContactDetail(int Distributorid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("pfetchContactdetail"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DistributorId", SqlDbType.Int).Value = Distributorid;


                    return ReturnDataSet(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetRandomPassword(long DistributorID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("pRandomPassword"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;


                    return ReturnDataSet(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SaveRandomPassword(long DistributorID, string Password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("pRandomPasswordSave"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                    return ReturnDataSet(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
