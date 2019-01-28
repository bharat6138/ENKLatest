using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
   public class RechargeAndroid : DataBase
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
        public string UserType = "";

        public DataSet SubscriberForgetPassword(string EmailID)
        {
            try
            {
                 
                
                using (SqlCommand objCmd = new SqlCommand("spsubscriberForgetPassword"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                     objCmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = EmailID;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public DataSet GetRechargePlan(int NetworkID, string State)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchProductRechargeApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                    objCmd.Parameters.Add("@State", SqlDbType.VarChar).Value = State;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public DataSet GetRechargeDistributorPlan(int NetworkID,  int DistributorID)
       {
           try
           {
               using (SqlCommand objCmd = new SqlCommand("pFetchDistributorProductRechargeApp"))
               {
                   objCmd.CommandType = CommandType.StoredProcedure;
                   objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                   objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;

                   return ReturnDataSet(objCmd);
               }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataSet GetDistributorActivationPlanApp(int NetworkID, int DistributorID)
       {
           try
           {
               using (SqlCommand objCmd = new SqlCommand("pFetchDistributorActivationPlanApp"))
               {
                   objCmd.CommandType = CommandType.StoredProcedure;
                   objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                   objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;

                   return ReturnDataSet(objCmd);
               }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        public DataSet GetNetworkApp()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchVendorApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetStateApp()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetstateApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CheckRechargeDuplicate(int NetworkID, string SerialNumber, int TariffID, string InvoiceNo)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pCheckRechargeDuplicate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;

                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;

                ds = ReturnDataSet(cmd);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet SaveTransactionDetails(int NetworkID, int ProductID, string TransactionType, string ProductPinID, string SIMNumber, string InvoiveNumber, string Amount, string Currency, string City, string Zip, string NPA, int CreatedBy, string ChargeAmount)
        {
            DataSet ds = null;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spTransactionDetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                cmd.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
                cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar).Value = TransactionType;
                cmd.Parameters.Add("@ProductPinID", SqlDbType.VarChar).Value = ProductPinID;
                cmd.Parameters.Add("@SIMNumber", SqlDbType.VarChar).Value = SIMNumber;
                cmd.Parameters.Add("@InvoiveNumber", SqlDbType.VarChar).Value = InvoiveNumber;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;
                cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = Currency;

                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City;
                cmd.Parameters.Add("@Zip", SqlDbType.VarChar).Value = Zip;
                cmd.Parameters.Add("@NPA", SqlDbType.VarChar).Value = NPA;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                cmd.Parameters.Add("@ChargeAmount", SqlDbType.VarChar).Value = ChargeAmount;


                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public int UpdateAccountBalanceAfterRechargeAPP(int PaymentID,int NetworkID, int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string ZipCode, string RechargeStatus, string RechargeVia, string RechargeRequest, string RechargeResponse, int LoginID, int PaymentFrom, string PaymentMode, string TransactionId, int Currency, string TransactionStatus, int TransactionStatusId, string PiNumber, string State, string TxnID, string Tax, string TotalAmount, string InvoiceNo, string StatusVia, string Regulatery,int IsWalet)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterRechargeAPP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = ZipCode;

                cmd.Parameters.Add("@RechargeStatus", SqlDbType.BigInt).Value = RechargeStatus;

                cmd.Parameters.Add("@RechargeVia", SqlDbType.BigInt).Value = RechargeVia;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = RechargeRequest;
                cmd.Parameters.Add("@RechargeResponse", SqlDbType.VarChar).Value = RechargeResponse;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;

                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@PiNumber", SqlDbType.VarChar).Value = PiNumber;

                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = State;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnID;
                cmd.Parameters.Add("@Tax", SqlDbType.VarChar).Value = Tax;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.VarChar).Value = TotalAmount;

                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;
                cmd.Parameters.Add("@StatusVia", SqlDbType.VarChar).Value = StatusVia;
                cmd.Parameters.Add("@Regulatery", SqlDbType.VarChar).Value = Regulatery;
                cmd.Parameters.Add("@PaymentID", SqlDbType.BigInt).Value = PaymentID;
                cmd.Parameters.Add("@IsWalet", SqlDbType.BigInt).Value = IsWalet;
                

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public int UpdateAccountBalanceAfterRechargeSubscriberAPP(int PaymentID, int NetworkID, int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string ZipCode, string RechargeStatus, string RechargeVia, string RechargeRequest, string RechargeResponse, int LoginID, int PaymentFrom, string PaymentMode, string TransactionId, int Currency, string TransactionStatus, int TransactionStatusId, string PiNumber, string State, string TxnID, string Tax, string TotalAmount, string InvoiceNo, string StatusVia, string Regulatery, int IsWalet, string IMEI, string Location, string IPAddress)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterRechargeSubscriberAPP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = ZipCode;

                cmd.Parameters.Add("@RechargeStatus", SqlDbType.BigInt).Value = RechargeStatus;

                cmd.Parameters.Add("@RechargeVia", SqlDbType.BigInt).Value = RechargeVia;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = RechargeRequest;
                cmd.Parameters.Add("@RechargeResponse", SqlDbType.VarChar).Value = RechargeResponse;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;

                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@PiNumber", SqlDbType.VarChar).Value = PiNumber;

                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = State;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnID;
                cmd.Parameters.Add("@Tax", SqlDbType.VarChar).Value = Tax;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.VarChar).Value = TotalAmount;

                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;
                cmd.Parameters.Add("@StatusVia", SqlDbType.VarChar).Value = StatusVia;
                cmd.Parameters.Add("@Regulatery", SqlDbType.VarChar).Value = Regulatery;
                cmd.Parameters.Add("@PaymentID", SqlDbType.BigInt).Value = PaymentID;
                cmd.Parameters.Add("@IsWalet", SqlDbType.BigInt).Value = IsWalet;
           
                cmd.Parameters.Add("@IMEI", SqlDbType.VarChar).Value = IMEI;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
                cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = IPAddress;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
       
       
       
       
       public DataSet CreateSuscriberUser(string FirstName, string LastName, string EmailID, string MobileNumber, string Password, string UserType)
        {
            try
            {
                

                using (SqlCommand objCmd = new SqlCommand("pSaveSuscriberUser"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Fname", SqlDbType.VarChar).Value = FirstName;
                    objCmd.Parameters.Add("@Lname", SqlDbType.VarChar).Value = LastName;
                    objCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;
                    objCmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = Password;
                    objCmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = MobileNumber;
                    objCmd.Parameters.Add("@UserType", SqlDbType.VarChar).Value = UserType;
 
                    return ReturnDataSet(objCmd);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ValidateSuscriberLogin(string UserName, string Pass)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSuscriberUserLogin"))
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

        public DataSet ValidateSubscriberFacebookLogin(string EmailID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFacebookSuscriberUserLogin"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;
                     
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SubscriberUserVerification(string UserName, string OTP)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSubscriberUserVerification"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = UserName;
                    objCmd.Parameters.Add("@OTPCode", SqlDbType.VarChar).Value = OTP;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ResetSubscriberPassword(string UserID, string Password)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spResetSubscriberPassword";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetSingleTariffDetailForActivationAPP(int LoginID, int DistributorID, int ClientTypeID, int TariffID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleTariffDetailForActivationApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
