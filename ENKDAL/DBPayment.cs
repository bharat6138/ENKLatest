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
    public class DBPayment : DataBase
    {

        public decimal ChargedAmount;
        public int IsWalet = 0;
        public int PaymentId;
        public int PaymentFrom;
        public int PayeeID;
        public int PaymentType;
        public string OrderId = "";
        public string PaymentMode = "";
        public string TransactionId = "";
        public string TransactionStatus = "";
        public string Mid = "";
        public string TxnId = "";
        public string TxnAmount = "";
        public int Currency;
        public string RespCode = "";
        public string RespMsg = "";
        public string TxnDate = "";
        public int ActivationType;
        public int ActivationStatus;
        public int ActivationVia;
        public string ActivationResp = "";
        public string ActivationRequest = "";
        public int TariffID;
        public string CheckSumm = "";
        public string Remarks = "";
        public string ALLOCATED_MSISDN = "";

        public string ReceiptId = "";
        public string PayerId = "";
        public int TransactionStatusId;
        public string EmailID = "";
        public string Address = "";
        public string Mobile = "";
        public string CusName = "";
        public Decimal Regulatery = 0;
        public int Month = 1;



        public DataSet GetSimNetwork(string SimNo, String Action = "Activate")
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pFetchSimNetwork";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@SimNo", SqlDbType.VarChar).Value = SimNo;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int UpdateAccountDingRecharge(int distributorID, int LoginID, string MobileNumber, string channel, string lang, string SkuTariffCode, string TariffDescription, string TransferRef, decimal TxnAmount, string RespCode, string RespMsg, string TxnDate, string Country, string Operator, string SendCurrencyIso)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertGlobalRechargeDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TransferRef;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.Decimal).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = SendCurrencyIso;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;
                cmd.Parameters.Add("@RechargeType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@RechargeStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@RechargeVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@RechargeResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.VarChar).Value = SkuTariffCode;
                cmd.Parameters.Add("@TariffDescription", SqlDbType.VarChar).Value = TariffDescription;
                cmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = MobileNumber;
                cmd.Parameters.Add("@Month", SqlDbType.BigInt).Value = 1;
                cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = Country;
                cmd.Parameters.Add("@Operator", SqlDbType.VarChar).Value = Operator;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet CheckDuplicatePaypalTxnID(string TxnID)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pCheckDuplicatePaypalTxnID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TxnID", SqlDbType.VarChar).Value = TxnID;
                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet UpdatePurchaseSimNetwork(int VendorID, string SimNo, long PurchaseID)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pUpdatePurchaseSimNetwork";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@VendorID", SqlDbType.BigInt).Value = VendorID;
                cmd.Parameters.Add("@SimNo", SqlDbType.VarChar).Value = SimNo;
                cmd.Parameters.Add("@PurchaseID", SqlDbType.BigInt).Value = PurchaseID;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int InsertTopupPayment(int distributorID, int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertCompanyTopupBalance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateAccountBalanceActivation(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, int NetworkID,
 int DataAddOnID = 0, int InternationalID = 0, decimal DataAddOnValue = 0, decimal DataAddOnDiscountedAmount = 0, decimal DataAddOnDiscountPercent = 0, decimal InternationalCreditValue = 0, decimal InternationalCreditDiscountedAmount = 0, decimal InternationalCreditDiscountPercent = 0, string MNPNO = "")
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceH2OLYCA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = ALLOCATED_MSISDN;
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = zipcode;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;

                cmd.Parameters.Add("@ActivationType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@ActivationResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@ActivationRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = sim;
                cmd.Parameters.Add("@Regulatery", SqlDbType.Decimal).Value = Regulatery;
                cmd.Parameters.Add("@Month", SqlDbType.Decimal).Value = Month;


                cmd.Parameters.Add("@DataAddOnID", SqlDbType.Int).Value = DataAddOnID;
                cmd.Parameters.Add("@InternationalID", SqlDbType.Int).Value = InternationalID;

                //cmd.Parameters.Add("@DataAddOnID", SqlDbType.Int).Value = DataAddOnID;
                cmd.Parameters.Add("@DataAddOnValue", SqlDbType.Decimal).Value = DataAddOnValue;
                cmd.Parameters.Add("@DataAddOnDiscountedAmount", SqlDbType.Decimal).Value = DataAddOnDiscountedAmount;
                cmd.Parameters.Add("@DataAddOnDiscountPercent", SqlDbType.Decimal).Value = DataAddOnDiscountPercent;
                //cmd.Parameters.Add("@InternationalCreditID", SqlDbType.Int).Value = InternationalID;
                cmd.Parameters.Add("@InternationalCreditValue", SqlDbType.Decimal).Value = InternationalCreditValue;
                cmd.Parameters.Add("@InternationalCreditDiscountedAmount", SqlDbType.Decimal).Value = InternationalCreditDiscountedAmount;
                cmd.Parameters.Add("@InternationalCreditDiscountPercent", SqlDbType.Decimal).Value = InternationalCreditDiscountPercent;
                cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@NetworkID_from_FE", SqlDbType.Int).Value = NetworkID;
                cmd.Parameters.Add("@MNPNO", SqlDbType.VarChar).Value = MNPNO;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateAccountBalance(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = ALLOCATED_MSISDN;
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = zipcode;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;

                cmd.Parameters.Add("@ActivationType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@ActivationResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@ActivationRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = sim;
                cmd.Parameters.Add("@Regulatery", SqlDbType.Decimal).Value = Regulatery;
                cmd.Parameters.Add("@Month", SqlDbType.Decimal).Value = Month;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateAccountBalanceApp(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceApp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = ALLOCATED_MSISDN;
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = zipcode;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;

                cmd.Parameters.Add("@ActivationType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@ActivationResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@ActivationRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = sim;
                cmd.Parameters.Add("@Regulatery", SqlDbType.Decimal).Value = Regulatery;
                cmd.Parameters.Add("@IsWalet", SqlDbType.BigInt).Value = IsWalet;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdatePayPalAccountBalance(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdatePayPalAccountBalance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = ALLOCATED_MSISDN;
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = zipcode;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;

                cmd.Parameters.Add("@ActivationType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@ActivationResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@ActivationRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = sim;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int InsertSubscriberDetail(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertSubscriberDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;

                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;

                cmd.Parameters.Add("@OrderrId", SqlDbType.VarChar).Value = OrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Mid", SqlDbType.VarChar).Value = Mid;

                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@RespCode", SqlDbType.VarChar).Value = RespCode;
                cmd.Parameters.Add("@RespMsg", SqlDbType.VarChar).Value = RespMsg;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = ALLOCATED_MSISDN;
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = zipcode;
                cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = channel;
                cmd.Parameters.Add("@Lang", SqlDbType.VarChar).Value = lang;

                cmd.Parameters.Add("@ActivationType", SqlDbType.BigInt).Value = ActivationType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@ActivationResp", SqlDbType.VarChar).Value = ActivationResp;
                cmd.Parameters.Add("@ActivationRequest", SqlDbType.VarChar).Value = ActivationRequest;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = sim;

                cmd.Parameters.Add("@CusName", SqlDbType.VarChar).Value = CusName;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailID;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
                cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = Mobile;
                cmd.Parameters.Add("@Regulatery", SqlDbType.BigInt).Value = Regulatery;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet InsertPaypalPayment(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaypalTopupBalance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet InsertPaypalPaymentAPP(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaypalTopupBalanceAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InitiatePaymentWaletRechargeApp(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaymentWaletRechargeAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InitiatePaymentWaletActivationApp(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaymentWaletActivationAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet InitiatePaymentPaypalActivationApp(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaymentPayPalActivationAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InitiatePaymentPaypalRechargeApp(int distributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertPaymentPaypalRechargeAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PayeeID", SqlDbType.BigInt).Value = PayeeID;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@Currency", SqlDbType.BigInt).Value = Currency;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@ActivationVia", SqlDbType.BigInt).Value = ActivationVia;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int UpdatePaypalPayment(int distributorID, int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdatePaypalTopupBalance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@ReceiptId", SqlDbType.VarChar).Value = ReceiptId;
                cmd.Parameters.Add("@PayerId", SqlDbType.VarChar).Value = PayerId;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public int UpdatePaypalPaymentAPP(int distributorID, int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdatePaypalTopupBalanceAPP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@ReceiptId", SqlDbType.VarChar).Value = ReceiptId;
                cmd.Parameters.Add("@PayerId", SqlDbType.VarChar).Value = PayerId;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdatePaypalActivationPayment(int distributorID, int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdatePaypalActivationBalance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PaymentType", SqlDbType.BigInt).Value = PaymentType;
                cmd.Parameters.Add("@ActivationStatus", SqlDbType.BigInt).Value = ActivationStatus;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = PaymentId;
                cmd.Parameters.Add("@TxnId", SqlDbType.VarChar).Value = TxnId;
                cmd.Parameters.Add("@TxnAmount", SqlDbType.VarChar).Value = TxnAmount;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@ReceiptId", SqlDbType.VarChar).Value = ReceiptId;
                cmd.Parameters.Add("@PayerId", SqlDbType.VarChar).Value = PayerId;
                cmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;
                cmd.Parameters.Add("@CheckSumm", SqlDbType.VarChar).Value = CheckSumm;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        //public DataSet GetTopupPaymentDetails(int distributorID, int LoginID, int ClientTypeID, DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("spGetTopupPayment"))
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add("@distributorID", SqlDbType.Int).Value = distributorID;
        //            objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
        //            objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;

        //            objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
        //            objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
        //            return ReturnDataSet(objCmd);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataSet GetTopupPaymentDetails(int distributorID, int LoginID, int ClientTypeID, string FromDate, string ToDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetTopupPaymentDEV1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@distributorID", SqlDbType.Int).Value = distributorID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = ToDate;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID)
        //{
        //    int a = 0;
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("pChangeStatusForTopUp"))
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add("@LoginClientID", SqlDbType.TinyInt).Value = distributorID;
        //            objCmd.Parameters.Add("@LoginID", SqlDbType.TinyInt).Value = LoginID;
        //            objCmd.Parameters.Add("@ClientTypeID", SqlDbType.TinyInt).Value = ClientTypeID;

        //            objCmd.Parameters.Add("@PaymentID", SqlDbType.BigInt).Value = PaymentID;
        //            a = RunExecuteNoneQuery(objCmd);
        //            return a;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return a;
        //    }
        //}

        public int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID, Int32 StatusManual, string SRemark)
        {
            int a = 0;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pChangeStatusForTopUpDev1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginClientID", SqlDbType.Int).Value = distributorID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;

                    objCmd.Parameters.Add("@PaymentID", SqlDbType.BigInt).Value = PaymentID;
                    objCmd.Parameters.Add("@StatusManual", SqlDbType.VarChar).Value = Convert.ToString(StatusManual);
                    // add by akash 
                    objCmd.Parameters.Add("@SRemark", SqlDbType.VarChar).Value = SRemark;

                    a = RunExecuteNoneQuery(objCmd);
                    return a;
                }
            }
            catch (Exception ex)
            {
                return a;
            }
        }


        //Ankit Singh
        public DataSet GetPrintRecipt(string tansactionId)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PrintDetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = tansactionId;
                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
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

        public DataSet SaveProductMaster(int NetworkID, int ProductID, string ShortName, string ProductDescription, string Currency, string Amount, int CreatedBy)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spProductMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                cmd.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
                cmd.Parameters.Add("@ShortName", SqlDbType.VarChar).Value = ShortName;
                cmd.Parameters.Add("@ProductDescription", SqlDbType.VarChar).Value = ProductDescription;
                cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = Currency;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
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

        public int UpdateAccountBalanceAfterRecharge(int NetworkID, int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string ZipCode, string RechargeStatus, string RechargeVia, string RechargeRequest, string RechargeResponse, int LoginID, int PaymentFrom, string PaymentMode, string TransactionId, int Currency, string TransactionStatus, int TransactionStatusId, string PiNumber, string State, string TxnID, string Tax, string TotalAmount, string InvoiceNo, string StatusVia, string Regulatery)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterRecharge";
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


                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }


        public int UpdateAccountBalanceAfterRechargeNew(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest, string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterRecharge";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;

                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;

                cmd.Parameters.Add("@RechargeStatus", SqlDbType.BigInt).Value = RechargeStatus;

                cmd.Parameters.Add("@RechargeVia", SqlDbType.BigInt).Value = RechargeVia;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = RechargeRequest;
                cmd.Parameters.Add("@RechargeResponse", SqlDbType.VarChar).Value = RechargeResponse;
                cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;

                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;

                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = Convert.ToInt16(Month);

                cmd.Parameters.Add("@tariffamount", SqlDbType.Decimal).Value = Convert.ToDecimal(TotalAmount);


                cmd.Parameters.Add("@Regulatery", SqlDbType.VarChar).Value = Regulatery;
                cmd.Parameters.Add("@DataAddOnID", SqlDbType.Int).Value = Convert.ToInt32(DataAddOnID);
                cmd.Parameters.Add("@InternationalID", SqlDbType.Int).Value = Convert.ToInt32(InternationalID);
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        // h2o recharge
        public int UpdateAccountBalanceAfterRechargeNewForH2O(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest, string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID)
        {
            int a = 0;
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterRechargeForH2O";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = ChargedAmount;
                cmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                cmd.Parameters.Add("@RechargeStatus", SqlDbType.BigInt).Value = RechargeStatus;
                cmd.Parameters.Add("@RechargeVia", SqlDbType.BigInt).Value = RechargeVia;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = RechargeRequest;
                cmd.Parameters.Add("@RechargeResponse", SqlDbType.VarChar).Value = RechargeResponse;
                cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                cmd.Parameters.Add("@PaymentFrom", SqlDbType.BigInt).Value = PaymentFrom;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@TransactionStatus", SqlDbType.VarChar).Value = TransactionStatus;
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.BigInt).Value = TransactionStatusId;
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = Convert.ToInt16(Month);
                cmd.Parameters.Add("@tariffamount", SqlDbType.Decimal).Value = Convert.ToDecimal(TotalAmount);
                cmd.Parameters.Add("@Regulatery", SqlDbType.VarChar).Value = Regulatery;
                cmd.Parameters.Add("@DataAddOnID", SqlDbType.Int).Value = Convert.ToInt32(DataAddOnID);
                cmd.Parameters.Add("@InternationalID", SqlDbType.Int).Value = Convert.ToInt32(InternationalID);
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        // h2o recharge

        public int SaveRechageRequest(int NetworkID, int TariffID, string SerialNumber, string ZipCode, string RechargeRequest, string EmailID, string City, decimal Amount, decimal TaxAmount, decimal TotalAmount, decimal Regulatery, int CreatedBy, int DistributorID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pSaveRechageRequest";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SerialNumber;
                cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = ZipCode;
                cmd.Parameters.Add("@RechargeRequest", SqlDbType.VarChar).Value = RechargeRequest;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailID;
                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
                cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = TaxAmount;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = TotalAmount;
                cmd.Parameters.Add("@Regulatery", SqlDbType.Decimal).Value = Regulatery;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = CreatedBy;
                cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public void UpdateAccountBalanceAfterCancelPortIn(string MNPRefNumber, string POrtInCancelRequest, string PortInCancelResponnse, int UserID)
        {
            int a = 0;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateAccountBalanceAfterCancelPortIn";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MNPRefNumber", SqlDbType.VarChar).Value = MNPRefNumber;
                cmd.Parameters.Add("@POrtInCancelRequest", SqlDbType.VarChar).Value = POrtInCancelRequest;
                cmd.Parameters.Add("@PortInCancelResponnse", SqlDbType.VarChar).Value = PortInCancelResponnse;
                cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = UserID;
                RunExecuteNoneQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetActivationsWithoutMSISDN()
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pGetActivationsWithoutMSISDN";
                cmd.CommandType = CommandType.StoredProcedure;

                
                ds = ReturnDataSet(cmd);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public void UpdateActivationMSISDN(long ActivationID, string MSISDN)
        {
            int a = 0;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pUpdateMSISDN_Activation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ActivationID", SqlDbType.BigInt).Value = ActivationID;

                cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;

                RunExecuteNoneQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int pGetImeurl(string Url, int ImgFlag)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_tbannerImg";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@Url", SqlDbType.VarChar).Value = Url;
                cmd.Parameters.Add("@ImgFlag", SqlDbType.Int).Value = ImgFlag;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet CheckSimAndMobileExistance(string SimNumber, string MobileNumber)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CheckSimAndMobileExistance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@SimNumber", SqlDbType.VarChar).Value = SimNumber;
                cmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = MobileNumber;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        // ak
       
        public DataSet GetCurrentbalance(int DistributorID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetAccountBalanceForDistributor"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = DistributorID;
                    return ReturnDataSet(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Manual Commission Processing"

        public int SaveITRFile(DataTable dt, string filename)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSaveITRFile"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@fileName", SqlDbType.VarChar).Value = filename;
                    objCmd.Parameters.Add("@dtITRtable", SqlDbType.Structured).Value = dt;

                    return RunExecuteNoneQuery(objCmd);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ProcessingManualCommission(string stepNo)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pProcessManualCommssion"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = stepNo;

                    return RunExecuteNoneQuery(objCmd);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
  
    }
}
