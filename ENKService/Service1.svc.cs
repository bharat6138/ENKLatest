using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ENKDAL;
using System.Web.Services;

namespace ENKService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public DataSet GETLEVELDistributor(Int32 distributorID, int treeLevel)
        {
            DataSet ds = null;
            try
            {
                cReport cr = new cReport();
                ds = cr.GETLEVELDistributor(distributorID, treeLevel);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataTable GetCountriesForDing()
        {
            DataTable dt = null;
            try
            {
                DBDING DD = new DBDING();
                dt = DD.GetCountriesForDing();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public DataTable OperatorAginstCountry(string countryISO)
        {
            DataTable dt = null;
            try
            {
                DBDING DD = new DBDING();
                dt = DD.OperatorAginstCountry(countryISO);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public int UpdateAccountDingRecharge(int distributorID, int LoginID, string MobileNumber, string channel, string lang, SPayment sp, string SkuTariffCode, string TariffDescription, string TransferRef, decimal TxnAmount, string RespCode, string RespMsg, string TxnDate, string Country, string Operator, string SendCurrencyIso)
        {
            int a = 0;
            try
            {

                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationType = sp.ActivationType;
                dsp.PaymentType = sp.PaymentType;
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.ActivationResp = sp.ActivationResp;
                dsp.ActivationRequest = sp.ActivationRequest;
                dsp.TariffID = sp.TariffID;
                dsp.ALLOCATED_MSISDN = sp.ALLOCATED_MSISDN;
                dsp.TransactionId = sp.TransactionId;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.Regulatery = sp.Regulatery;
                dsp.Month = sp.Month;
                a = dsp.UpdateAccountDingRecharge(distributorID, LoginID, MobileNumber, channel, lang, SkuTariffCode, TariffDescription, TransferRef, TxnAmount, RespCode, RespMsg, TxnDate, Country, Operator, SendCurrencyIso);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int StoredAPIRequestBeforeCall(string Title, string Request, Int32 DistributorId, string TransactionID, string Msisdn, string SIMNumber)
        {
            int a = 0;
            try
            {
                DBDistributor dis = new DBDistributor();
                a = dis.StoredAPIRequestBeforeCall(Title, Request, DistributorId, TransactionID, Msisdn, SIMNumber);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }


        public int GetData(int value)
        {
            return 1;
        }


        public DataSet GetApiDetail(Int64 DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBApi ap = new DBApi();
                ds = ap.GetApiDetails(DistributorID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        public int SaveApiMapping(long DistributorID, int clientcode, DataTable dtApi, int chkApiStatus)
        {
            int a = 0;
            try
            {
                DBApi ap = new DBApi();
                a = ap.SaveApiMapping(DistributorID, clientcode, dtApi, chkApiStatus);
                return a;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public int SaveIpMapping(long DistributorID, DataTable dtIp, int chkRistrictIP)
        //{
        //    int a = 0;
        //    try
        //    {
        //        DBIP IP = new DBIP();
        //        a = IP.SaveIpMapping(DistributorID, dtIp, chkRistrictIP);
        //        return a;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //----------Vivek

        public DataSet GetRegulatery()
        {
            DataSet ds = null;
            try
            {
                cReport dbs = new cReport();
                ds = dbs.GetRegulatery();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet GetTariffForActivation_ForSubscriber(string simnumber)
        {
            DataSet ds = null;
            try
            {
                cSIM CSM = new cSIM();
                ds = CSM.GetTariffForActivation_ForSubscriber(simnumber);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public int SaveRechageRequest(int NetworkID, int TariffID, string SerialNumber, string ZipCode, string RechargeRequest, string EmailID, string City, decimal Amount, decimal TaxAmount, decimal TotalAmount, decimal Regulatery, int CreatedBy, int DistributorID)
        {


            int a = 0;


            try
            {
                DBPayment v = new DBPayment();

                a = v.SaveRechageRequest(NetworkID, TariffID, SerialNumber, ZipCode, RechargeRequest, EmailID, City, Amount, TaxAmount, TotalAmount, Regulatery, CreatedBy, DistributorID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }
        public int UpdateStatusActivation(int ActivationID, int LoginID)
        {
            int a = 0;
            try
            {
                cReport cS = new cReport();
                a = cS.UpdateStatusActivation(ActivationID, LoginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateStatusRecharge(int RechargeID, int LoginID)
        {
            int a = 0;
            try
            {
                cReport cS = new cReport();
                a = cS.UpdateStatusRecharge(RechargeID, LoginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public int ResetSubscriberPassword(string UserID, string Password)
        {
            int a = 0;
            try
            {
                RechargeAndroid cS = new RechargeAndroid();
                a = cS.ResetSubscriberPassword(UserID, Password);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet UpdateBulkNetwork(int NetworkID, DataTable dt)
        {
            DataSet ds = new DataSet();

            try
            {
                DBVendor dis = new DBVendor();
                ds = dis.UpdateBulkNetwork(NetworkID, dt);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet CheckDuplicatePaypalTxnID(string TxnID)
        {
            DataSet ds = new DataSet();
            try
            {
                DBPayment dis = new DBPayment();
                ds = dis.CheckDuplicatePaypalTxnID(TxnID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet CheckSimNumber(int NetworkID, DataTable dt)
        {
            DataSet ds = new DataSet();

            try
            {
                DBVendor dis = new DBVendor();
                ds = dis.CheckSimNumber(NetworkID, dt);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet GetDistributor(int Distributorid)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.GetDistributor(Distributorid);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet DeductDistributorTopUpAmount(int Distributorid, decimal Amount, string Remarks)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.DeductDistributorTopUpAmount(Distributorid, Amount, Remarks);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SaveInventory(SIM s, int UserID, Actions Action)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();

                cS.BranchID = s.BranchID;
                cS.DistributorID = s.DistributorID;
                cS.VendorID = s.VendorID;
                cS.UserID = UserID;
                cS.PurchaseDate = s.PurchaseDate;
                cS.PurchaseNo = s.PurchaseNo;
                cS.InvoiceNo = s.InvoiceNo;
                cS.MobileDT = s.MobileDT;
                cS.SIMDt = s.SIMDt;

                a = cS.SaveInventory();

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataTable CheckDuplicateRecharge(string MSISDN, int TariffID, Int32 DistributorId)
        {
            DataTable dt = null;
            try
            {
                DBTariff Dv = new DBTariff();
                dt = Dv.CheckDuplicateRecharge(MSISDN, TariffID, DistributorId);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetVendor(int UserID)
        {
            DataTable dt = null;
            try
            {
                cVendor v = new cVendor();
                v.UserID = UserID;

                dt = v.GetVendor();

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable PreloadedSIMCheck(string SimNo)
        {
            DataTable dt = null;
            try
            {
                cSIM c = new cSIM();
                dt = c.PreloadedSIMCheck(SimNo);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet GetSimNetwork(string SimNo, String Action)
        {
            DataSet dt = null;
            try
            {
                DBPayment v = new DBPayment();


                dt = v.GetSimNetwork(SimNo, Action);

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet GetReportActivationLedger(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, Int32 CurrentLogin)
        {

            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportActivationLedger(CurrentLogin);

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
                DBPayment v = new DBPayment();

                ds = v.CheckRechargeDuplicate(NetworkID, SerialNumber, TariffID, InvoiceNo);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }

        }



        public DataSet GetState()
        {
            DataSet dt = null;
            try
            {
                DBTariff v = new DBTariff();


                dt = v.GetState();

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public DataSet GetPurchaseCode()
        {
            DataSet dt = null;
            try
            {
                DBTariff v = new DBTariff();


                dt = v.GetPurchaseCode();

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet UpdatePurchaseSimNetwork(int VendorID, string SimNo, long PurchaseID)
        {
            DataSet dt = null;
            try
            {
                DBPayment v = new DBPayment();


                dt = v.UpdatePurchaseSimNetwork(VendorID, SimNo, PurchaseID);

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }



        public DataSet GetProductRecharge(int NetworkID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetProductRecharge(NetworkID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetTariffService(int LoginID, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetTariff(LoginID, DistributorID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int InventoryTransfer(SIM s, int UserID, Actions Action, string checkInventoryWAY)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();


                cS.ClientID = s.ClientID;
                cS.NewClientID = s.NewClientID;
                cS.UserID = UserID;
                cS.TransferType = s.TransferType;
                cS.MobileDT = s.MobileDT;
                cS.SIMDt = s.SIMDt;

                a = cS.SaveInventoryTransfer(checkInventoryWAY);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataSet GetInventory1(int ClientID, string Action, string SimNumber, string FromSimNumber, string ToSimNumber)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.Action = Action;

                ds = s.GetInventory1(SimNumber, FromSimNumber, ToSimNumber);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetInventory(int ClientID, string Action)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.Action = Action;

                ds = s.GetInventory();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetStockTransferReport(int ClientID, int ClientTypeID, DateTime fromDate, DateTime Todate)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.ClientTypeID = ClientTypeID;
                s.FromDate = fromDate;
                s.ToDate = Todate;
                ds = s.GetStockTransferReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SaveDistributorNotes(int DistributorID, string Notes, int LoginID)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();
                cS.ClientID = DistributorID;
                cS.Notes = Notes;
                cS.UserID = LoginID;
                a = cS.SaveDistributorNotes();

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }
        public DataSet GetDistributorNotes(int ClientID, int ClientTypeID)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.ClientTypeID = ClientTypeID;
                ds = s.GetDistributorNotes();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet ValidatePreloadedSim(string SimNumber)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                ds = s.ValidatePreloadedSim(SimNumber);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        //public DataSet ValidatePUK(string SimNumber, string PUK)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        cSIM s = new cSIM();
        //        ds = s.ValidatePUK(SimNumber, PUK);

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds;
        //    }
        //}




        public DataSet GetInventoryForAccept(int ClientID, int LoginClientID)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.DistributorID = LoginClientID;

                ds = s.GetInventoryForAccept();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetInventoryForSIMReplacement(string MSISDNNo, string SIMNo, int ClientID)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.MSISDNNo = MSISDNNo;
                s.SIMNo = SIMNo;
                s.ClientID = ClientID;

                ds = s.GetInventoryForSIMReplacement();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SIMReplacement(SIM s, Actions Action)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();


                cS.ClientID = s.ClientID;
                cS.SIMNo = s.SIMNo;
                cS.UserID = s.UserID;
                cS.MSISDN_SIM_ID = s.MSISDN_SIM_ID;


                a = cS.SIMReplacement();

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataSet GetReportInventoryPurchase(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportInventoryPurchase();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetPurchaseReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int PurchaseID)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.FromDate = FromDate;
                cR.ToDate = ToDate;
                cR.PurchaseID = PurchaseID;

                ds = cR.GetPurchaseReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportInventoryStatus(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportInventoryStatus();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportSalesReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportSalesReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportActivationSIM(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int checkMainDistributor, int NetworkID)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;
                cR.checkMainDistributor = checkMainDistributor;
                cR.NetworkID = NetworkID;
                ds = cR.GetReportActivationSIM();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportAcivationFail(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;

                ds = cR.GetReportAcivationFail();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetRechargeSIMReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int NetworkID)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;
                cR.NetworkID = NetworkID;


                ds = cR.GetRechargeSIMReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetRechargeFailReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, string RechargeVia)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;
                cR.RechargeVia = RechargeVia;


                ds = cR.GetRechargeFailReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetRequesrRechargeSIMReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, string RechargeVia)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;
                cR.RechargeVia = RechargeVia;


                ds = cR.GetRequesrRechargeSIMReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        public DataSet GetReportRechargeFilterwise(int ClientID, int ClientTypeID, int LoginID, string MobileNo, string TxnID, string RechargeVia)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;
                //cR.Action = Action;
                cR.MobileNo = MobileNo;
                cR.TxnID = TxnID;
                cR.RechargeVia = RechargeVia;


                ds = cR.GetReportRechargeFilterwise();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportSIMHistory(int ClientID, int ClientTypeID, int LoginID, string SearchText)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.SearchText = SearchText;



                ds = cR.GetReportSIMHistory();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SimTariffMapping(SIM s, int UserID, Actions Action, int Months)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();


                cS.ClientID = s.ClientID;
                cS.TariffID = s.TariffID;
                cS.UserID = UserID;
                cS.SIMDt = s.SIMDt;

                a = cS.SaveSimTariffMapping(Months);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }


        public void SaveModifiedPortinDetails(string Request, string APIResponse, DateTime RequestedTime, int Requestedby, string MSISDN)
        {
            try
            {
                cSIM cS = new cSIM();
                cS.SaveModifiedPortinDetails(Request, APIResponse, RequestedTime, Requestedby, MSISDN);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public DataSet GetSaleReportForActivationAndPortIn(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.FromDate = FromDate;
                cR.ToDate = ToDate;

                ds = cR.GetSaleReportForActivationAndPortIn();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetInventoryBulkTransfer(int ClientID, DataTable SIMDt)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.SIMDt = SIMDt;

                ds = s.GetInventoryBulkTransfer();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet GetInventoryBulkTransfer1(int ClientID, DataTable SIMDt)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                s.ClientID = ClientID;
                s.SIMDt = SIMDt;

                ds = s.GetInventoryBulkTransfer1();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetInventoryForAcceptOnDasboard(int LoginClientID)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                //s.ClientID = ClientID;
                s.DistributorID = LoginClientID;

                ds = s.GetInventoryForAcceptOnDasboard();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetTariffSpiffDetails(string mode, int Id) //By Sarala
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetTariffSpiffDetails(mode, Id);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        //---------Pankaj

        public DataSet ValidateLoginService(string UserName, string Pwd)
        {
            try
            {
                DBUsers ud = new DBUsers();
                DataSet ds = ud.ValidateLogin(UserName, Pwd);
                return ds;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        // Added by Uday Verma
        public DataSet ValidateLoginApp(Int64 LoginId)
        {
            try
            {
                DBUsers ud = new DBUsers();
                DataSet ds = ud.ValidateLoginApp(LoginId);
                return ds;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }


        public DataSet CHECKDistributor(string UserID)
        {
            try
            {
                DBDistributor dis = new DBDistributor();
                DataSet ds = dis.CHECKDistributor(UserID);
                return ds;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public DataSet AddDistirbutorService(Distributor dist, int Userid, DataTable dt, DataTable dtRecharge, string UserName, int ChkSellr, int Chktariffgroup)
        {
            DataSet ds = new DataSet();
            try
            {
                DBDistributor dis = new DBDistributor();


                dis.DistributorName = dist.distributorName;
                dis.DistributorCode = dist.distributorCode;
                dis.CompanyType = dist.companyType;
                dis.Parent = dist.parent;
                dis.RelationManager = dist.relationManager;
                dis.VatNo = dist.vatNo;
                dis.VatPer = dist.vatPer;
                dis.ServiceTAxNo = dist.serviceTAxNo;
                dis.ServiceTAxPer = dist.serviceTAxPer;
                dis.ContactPerson = dist.contactPerson;
                dis.ContactNo = dist.contactNo;
                dis.WebSiteName = dist.webSiteName;
                dis.EmailID = dist.emailID;

                dis.Address = dist.address;
                dis.City = dist.city;
                dis.State = dist.state;
                dis.Zip = dist.zip;
                dis.CountryId = dist.countryid;
                dis.TariffGroupID = dist.TariffGroupID;
                dis.Password = dist.Password;

                dis.IsServiceTaxExmpted = dist.isServiceTaxExmpted;
                dis.IsActive = dist.isActive;
                dis.BalanceAmount = dist.balanceAmount;

                dis.EIN = dist.EIN;
                dis.SSN = dist.SSN;
                dis.PanNumber = dist.PanNumber;

                dis.document = dist.Document;
                dis.Certificate = dist.Certificate;

                ds = dis.SaveDistributor(Userid, dt, dtRecharge, UserName, ChkSellr, Chktariffgroup);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }

        }

        public int UpdateDistirbutorService(Distributor dist, int Userid, DataTable dt, DataTable dtRecharge, string Username, string passw, int ChkSellr, int Chktariffgroup)
        {
            int a = 0;
            try
            {
                DBDistributor dis = new DBDistributor();
                dis.DistributorID = dist.distributorID;
                dis.DistributorName = dist.distributorName;
                dis.DistributorCode = dist.distributorCode;
                dis.CompanyType = dist.companyType;
                dis.Parent = dist.parent;
                dis.RelationManager = dist.relationManager;
                dis.VatNo = dist.vatNo;
                dis.VatPer = dist.vatPer;
                dis.ServiceTAxNo = dist.serviceTAxNo;
                dis.ServiceTAxPer = dist.serviceTAxPer;
                dis.ContactPerson = dist.contactPerson;
                dis.ContactNo = dist.contactNo;
                dis.WebSiteName = dist.webSiteName;
                dis.EmailID = dist.emailID;

                dis.Address = dist.address;
                dis.City = dist.city;
                dis.State = dist.state;
                dis.Zip = dist.zip;
                dis.CountryId = dist.countryid;

                dis.IsServiceTaxExmpted = dist.isServiceTaxExmpted;
                dis.IsActive = dist.isActive;
                dis.BalanceAmount = dist.balanceAmount;

                dis.EIN = dist.EIN;
                dis.SSN = dist.SSN;
                dis.PanNumber = dist.PanNumber;
                dis.TariffGroupID = dist.TariffGroupID;

                dis.document = dist.Document;
                dis.Certificate = dist.Certificate;
                a = dis.UpdateDistributor(Userid, dt, dtRecharge, Username, passw, ChkSellr, Chktariffgroup);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }



        //public int UpdateDistributorRIGHTSHierarchy(int DistributorID, int ChkSellr, int Chktariffgroup)
        //{
        //    int a = 0;
        //    try
        //    {
        //        DBDistributor dis = new DBDistributor();
        //        a = dis.UpdateDistributorRIGHTSHierarchy(DistributorID, ChkSellr, Chktariffgroup);

        //        return a;
        //    }
        //    catch (Exception ex)
        //    {
        //        return a;
        //    }
        //}

        public List<Distributor> GetDistributorService1(int Userid, int Distributorid, string TaxDocument, string ResellerCertificate)
        {
            DBDistributor dis = new DBDistributor();
            DataSet ds = dis.GetDistributor1(Userid, Distributorid, TaxDocument, ResellerCertificate);
            List<Distributor> ditribtr = new List<Distributor>();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Distributor d = new Distributor();
                    d.distributorID = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                    d.distributorName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    d.distributorCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Acode"]);
                    d.companyType = Convert.ToInt32(ds.Tables[0].Rows[i]["ClientType"]);
                    d.companyTypeName = Convert.ToString(ds.Tables[0].Rows[i]["ClientTypeName"]);
                    d.parent = Convert.ToInt32(ds.Tables[0].Rows[i]["ParentDistributorID"]);
                    d.contactPerson = Convert.ToString(ds.Tables[0].Rows[i]["ContactPerson"]);
                    d.contactNo = Convert.ToString(ds.Tables[0].Rows[i]["ContactNumber"]);
                    d.webSiteName = Convert.ToString(ds.Tables[0].Rows[i]["WebSite"]);
                    d.emailID = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                    d.address = Convert.ToString(ds.Tables[0].Rows[i]["AddressLine"]);
                    d.city = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    d.state = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    d.zip = Convert.ToString(ds.Tables[0].Rows[i]["Zip"]);
                    d.countryid = Convert.ToInt32(ds.Tables[0].Rows[i]["CountryId"]);
                    d.balanceAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["AccountBalance"]);
                    d.isActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                    d.parentDistributor = Convert.ToString(ds.Tables[0].Rows[i]["PName"]);
                    d.NoOfBlankSim = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfBlankSim"]);

                    d.NoOfActivation = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfActivation"]);
                    d.EIN = Convert.ToString(ds.Tables[0].Rows[i]["EIN"]);
                    d.SSN = Convert.ToString(ds.Tables[0].Rows[i]["SSN"]);
                    d.PanNumber = Convert.ToString(ds.Tables[0].Rows[i]["PanNumber"]);

                    d.Document = Convert.ToString(ds.Tables[0].Rows[i]["TaxDocument"]);
                    d.Holdstatus = Convert.ToString(ds.Tables[0].Rows[i]["isHold"]);
                    d.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDtTm"]);
                    d.ModifiedDate = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDtTm"]);
                    d.Certificate = Convert.ToString(ds.Tables[0].Rows[i]["ResellerCertificate"]);
                    ditribtr.Add(d);

                }

                return ditribtr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Distributor> GetSingleDistributorService(int Distributorid)
        {
            DBDistributor dis = new DBDistributor();
            DataSet ds = dis.GetSingleDistributor(Distributorid);
            List<Distributor> ditribtr = new List<Distributor>();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Distributor d = new Distributor();
                    d.distributorID = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                    d.distributorName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    d.distributorCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Acode"]);
                    d.companyType = Convert.ToInt32(ds.Tables[0].Rows[i]["ClientType"]);
                    d.parent = Convert.ToInt32(ds.Tables[0].Rows[i]["ParentDistributorID"]);
                    d.relationManager = 0;// Convert.ToInt32(ds.Tables[0].Rows[i][""]);

                    d.contactPerson = Convert.ToString(ds.Tables[0].Rows[i]["ContactPerson"]);
                    d.contactNo = Convert.ToString(ds.Tables[0].Rows[i]["ContactNumber"]);
                    d.webSiteName = Convert.ToString(ds.Tables[0].Rows[i]["WebSite"]);
                    d.emailID = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);

                    d.address = Convert.ToString(ds.Tables[0].Rows[i]["AddressLine"]);
                    d.city = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    d.state = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    d.zip = Convert.ToString(ds.Tables[0].Rows[i]["Zip"]);
                    d.countryid = Convert.ToInt32(ds.Tables[0].Rows[i]["CountryId"]);
                    d.balanceAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["AccountBalance"]);
                    d.TariffGroupID = Convert.ToInt32(ds.Tables[0].Rows[i]["TariffGroupID"]);
                    d.EIN = Convert.ToString(ds.Tables[0].Rows[i]["EIN"]);
                    d.SSN = Convert.ToString(ds.Tables[0].Rows[i]["SSN"]);
                    d.PanNumber = Convert.ToString(ds.Tables[0].Rows[i]["PanNumber"]);

                    d.isActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                    d.Document = Convert.ToString(ds.Tables[0].Rows[i]["TaxDocument"]);
                    ditribtr.Add(d);

                }

                return ditribtr;
            }
            catch (Exception ex)
            {
                return ditribtr;
            }
        }

        public DataSet GetSingleDistributorTariffService(int Distributorid)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.GetSingleDistributorTariff(Distributorid);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        //public DataTable GetDistributorEMAIL(int Distributorid) 
        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        DBDistributor dis = new DBDistributor();
        //        dt = dis.GetDistributorEMAIL(Distributorid);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        return dt;
        //    }
        //}


        public List<Distributor> GetDistributorDDLService(int loginID, int Distributorid)
        {
            DBDistributor dis = new DBDistributor();
            DataSet ds = dis.GetDistributorDDL(loginID, Distributorid);
            List<Distributor> ditribtr = new List<Distributor>();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Distributor d = new Distributor();
                    d.distributorID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                    d.distributorName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    d.distributorCode = Convert.ToInt32(ds.Tables[0].Rows[i]["ACode"]);

                    ditribtr.Add(d);

                }

                return ditribtr;
            }
            catch (Exception ex)
            {
                return ditribtr;
            }
        }

        public DataSet GetClientType(int loginID, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBClientType Clienttype = new DBClientType();
                ds = Clienttype.GetClientType(loginID, DistributorID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetRoleService()
        {
            DataSet ds = null;
            try
            {
                DBRole RoleType = new DBRole();
                ds = RoleType.GetRole();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int AddUserService(SUsers ud, int loginID)
        {
            int a = 0;
            try
            {
                DBUsers du = new DBUsers();
                du.FirstName = ud.firstName;
                du.LastName = ud.lastName;
                du.UserName = ud.userName;
                du.EmailID = ud.emailID;
                du.ContactNo = ud.contactNo;
                du.DistributorID = ud.distributorID;
                du.RoleID = ud.roleID;
                du.Pwd = ud.pwd;
                du.ActiveFrom = ud.activeFrom;
                du.ActiveTo = ud.activeTo;
                du.IsActive = ud.isActive;
                a = du.AddUser(loginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int UpdateUserService(SUsers ud, int loginID)
        {
            int a = 0;
            try
            {
                DBUsers du = new DBUsers();
                du.UserID = ud.userID;
                du.FirstName = ud.firstName;
                du.LastName = ud.lastName;
                du.UserName = ud.userName;
                du.EmailID = ud.emailID;
                du.ContactNo = ud.contactNo;
                du.DistributorID = ud.distributorID;
                du.RoleID = ud.roleID;
                du.Pwd = ud.pwd;
                du.ActiveFrom = ud.activeFrom;
                du.ActiveTo = ud.activeTo;
                du.IsActive = ud.isActive;
                a = du.UpdateUser(loginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataSet GetUserListService(int loginID, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBUsers User = new DBUsers();
                ds = User.GetUserList(loginID, DistributorID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetUserService(int UserID, int LoginID)
        {
            DataSet ds = null;
            try
            {
                DBUsers User = new DBUsers();
                ds = User.GetUser(UserID, LoginID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int InsertCompanyTopupBalanceService(int distributorID, int LoginID, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.CheckSumm = sp.CheckSumm;
                dsp.Remarks = sp.Remarks;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.Currency = sp.Currency;
                a = dsp.InsertTopupPayment(distributorID, LoginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int InsertLoginHistoryService(SLoginHistory sl)
        {
            int a = 0;
            try
            {
                DBLoginHistory dbsl = new DBLoginHistory();
                dbsl.IpAddress1 = sl.IpAddress1;
                dbsl.IpAddress2 = sl.IpAddress2;
                dbsl.IpAddress3 = sl.IpAddress3;
                dbsl.Browser = sl.BrowserName;
                dbsl.Location = sl.Location;
                dbsl.LoginID = sl.LoginID;
                dbsl.LoginTime = sl.LoginTime;
                dbsl.IpDetail = sl.IpDetail;
                dbsl.Browser1 = sl.Browser1;
                a = dbsl.InsertLoginHistory();
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetScreenService(int RoleID)
        {
            DataSet ds = null;
            try
            {
                DBRole ScreenType = new DBRole();
                ds = ScreenType.GetScreen(RoleID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetLoginHistory(DateTime FromDate, DateTime ToDate, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBLoginHistory ScreenType = new DBLoginHistory();
                ds = ScreenType.GetLoginHistory(FromDate, ToDate, DistributorID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetSingleTariffService(int TariffID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetSingleTariff(TariffID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetSingleTariffDetailForActivationService(int LoginID, int DistributorID, int ClientTypeID, int TariffID, int Month, string Action)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetSingleTariffDetailForActivation(LoginID, DistributorID, ClientTypeID, TariffID, Month, Action);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet CheckTariffGroupExist(string GroupName)  // Added By Sarala
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.CheckTariffGroupExist(GroupName);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public int SaveTariffGroupSpiffMapping(STariff st, int LoginID, int TariffGroupId, string Action, decimal RechargeCommission, decimal H2ORechargeDiscount,decimal Comission, decimal H2OGeneralDiscount)
        {
            int a = 0;
            try
            {
                DBTariff dst = new DBTariff();

                //dst.commission = st.Comission;
                // add by akash starts
                //dst.H2oGeneralDiscount = st.H2OGeneralDiscount;
                // add by akash ends
                dst.groupName = st.GroupName;
                dst.dtSpiff = st.dtSpiffDetail;

                a = dst.SaveTariffGroupSpiffMapping(LoginID, TariffGroupId, Action, RechargeCommission, H2ORechargeDiscount,Comission,H2OGeneralDiscount);

                return a;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw ex;
            }
        }
        public int AddTariffService(STariff st, int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                DBTariff dst = new DBTariff();
                dst.tariffCode = st.TariffCode;
                dst.tarifID = st.TarifID;
                dst.description = st.Description;
                dst.validDays = st.ValidDays;
                dst.rental = st.Rental;
                dst.TariffType = st.TariffType;
                dst.isActive = st.IsActive;
                dst.NetworkID = st.NetworkID;
                a = dst.SaveTariff(LoginID, DistributorID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateTariffService(STariff st, int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                DBTariff dst = new DBTariff();
                dst.tariffCode = st.TariffCode;
                dst.tarifID = st.TarifID;
                dst.description = st.Description;
                dst.validDays = st.ValidDays;
                dst.rental = st.Rental;
                dst.TariffType = st.TariffType;
                dst.isActive = st.IsActive;

                a = dst.UpdateTariff(LoginID, DistributorID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }





        public DataSet GetShortCodeService(string Condition)
        {
            DataSet ds = null;
            try
            {
                DBShortCode dbs = new DBShortCode();
                ds = dbs.GetShortCode(Condition);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        //Ankit Singh
        public DataSet GetAddOnANDInternationCreadit()
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetAddOnANDInternationCreadit();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        //Ankit Singh
        public DataSet GetDiscountAndRental(int DistributorID, int tariffID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetDiscountAndRental(DistributorID, tariffID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet GetTariffForActivationService(int LoginID, int DistributorID, int ClientTypeID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetTariffForActivation(LoginID, DistributorID, ClientTypeID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int UpdateAccountBalanceServiceActivation(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, int NetworkID,
 SPayment sp, int DataAddOnID = 0, int InternationalID = 0, decimal DataAddOnValue = 0, decimal DataAddOnDiscountedAmount = 0, decimal DataAddOnDiscountPercent = 0, decimal InternationalCreditValue = 0, decimal InternationalCreditDiscountedAmount = 0, decimal InternationalCreditDiscountPercent = 0, string MNPNO = "", string Serialnumber = "")
        {
            int a = 0;
            try
            {

                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationType = sp.ActivationType;
                dsp.PaymentType = sp.PaymentType;
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.ActivationResp = sp.ActivationResp;
                dsp.ActivationRequest = sp.ActivationRequest;
                dsp.TariffID = sp.TariffID;
                dsp.ALLOCATED_MSISDN = sp.ALLOCATED_MSISDN;
                dsp.TransactionId = sp.TransactionId;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.Regulatery = sp.Regulatery;
                dsp.Month = sp.Month;
                a = dsp.UpdateAccountBalanceActivation(distributorID, LoginID, sim, zipcode, channel, lang,NetworkID, DataAddOnID, InternationalID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int UpdateAccountBalanceService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationType = sp.ActivationType;
                dsp.PaymentType = sp.PaymentType;
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.ActivationResp = sp.ActivationResp;
                dsp.ActivationRequest = sp.ActivationRequest;
                dsp.TariffID = sp.TariffID;
                dsp.ALLOCATED_MSISDN = sp.ALLOCATED_MSISDN;
                dsp.TransactionId = sp.TransactionId;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.Regulatery = sp.Regulatery;
                dsp.Month = sp.Month;
                a = dsp.UpdateAccountBalance(distributorID, LoginID, sim, zipcode, channel, lang);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int InsertSubscriberActivationDetailService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationType = sp.ActivationType;
                dsp.PaymentType = sp.PaymentType;
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.ActivationResp = sp.ActivationResp;
                dsp.ActivationRequest = sp.ActivationRequest;
                dsp.TariffID = sp.TariffID;
                dsp.ALLOCATED_MSISDN = sp.ALLOCATED_MSISDN;
                dsp.TransactionId = sp.TransactionId;

                dsp.PaymentMode = sp.PaymentMode;
                dsp.PaymentId = sp.PaymentId;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;

                dsp.CusName = sp.CusName;
                dsp.EmailID = sp.EmailID;
                dsp.Address = sp.Address;
                dsp.Mobile = sp.Mobile;
                dsp.Regulatery = sp.Regulatery;


                a = dsp.InsertSubscriberDetail(distributorID, LoginID, sim, zipcode, channel, lang);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataSet ShowDashBoardDataService(int DistributorID, int ClientTypeID, int NetworkID)
        {
            DataSet ds = null;
            try
            {
                DBDashboard dash = new DBDashboard();
                ds = dash.ShowDashBoardData(DistributorID, ClientTypeID, NetworkID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet CheckSimActivationService(int DistributorID, int ClientTypeID, string SimNumber, string Action)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                ds = s.CheckSimActivation(DistributorID, ClientTypeID, SimNumber, Action);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet CheckSimPortINService(int DistributorID, int ClientTypeID, string SimNumber)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                ds = s.CheckSimPortIN(DistributorID, ClientTypeID, SimNumber);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet CheckAlreadySuccessfullPortIn(string SimNumber, string PhoneNumber)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                ds = s.CheckAlreadySuccessfullPortIn(SimNumber, PhoneNumber);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ShowDashBoardActivationDataService(int DistributorID, int ClientTypeID, int loginID, string Action, int month, int year, string FromDate, string ToDate)
        {
            DataSet ds = null;
            try
            {
                DBDashboard dash = new DBDashboard();
                ds = dash.ShowDashBoardActivationData(DistributorID, ClientTypeID, loginID, Action, month, year, FromDate, ToDate);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SavePOSService(int DistributorID, int LoginID, SPOS sp)
        {
            int a = 0;
            try
            {
                DBPOS pos = new DBPOS();

                pos.MSISDN_SIM_ID = sp.MSISDN_SIM_ID;
                pos.CoustomerName = sp.CoustomerName;
                pos.Email = sp.Email;
                pos.Address = sp.Address;
                pos.MobileNumber = sp.MobileNumber;
                a = pos.SavePOS(DistributorID, LoginID);


                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet ShowPOSService(int DistributorID, int ClientTypeID)
        {
            DataSet ds = null;
            try
            {
                DBPOS pos = new DBPOS();
                ds = pos.GetPOS(DistributorID, ClientTypeID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int InsertCurrencyService(string Name)
        {
            int a = 0;
            try
            {
                DBCurrency cn = new DBCurrency();
                a = cn.InsertCurrency(Name);


                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetCurrencyService(int DistributorID, int ClientTypeID)
        {
            DataSet ds = null;
            try
            {
                DBCurrency cn = new DBCurrency();
                ds = cn.GetCurrencyList(DistributorID, ClientTypeID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet VerifyUserIDService(int DistributorID, int ClientTypeID, int LoginID, string UserID)
        {
            DataSet ds = null;
            try
            {
                DBUsers du = new DBUsers();
                ds = du.VerifyUserID(DistributorID, ClientTypeID, LoginID, UserID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet ForgetPasswordService(string UserID, string Mobile)
        {
            DataSet ds = null;
            try
            {
                DBUsers User = new DBUsers();
                ds = User.ForgetPassword(UserID, Mobile);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet ResetPasswordService(string UserID, string OldPass, string NewPass, int DistributorID, int LoginID)
        {
            DataSet ds = null;
            try
            {
                DBUsers User = new DBUsers();
                ds = User.ResetPassword(UserID, OldPass, NewPass, DistributorID, LoginID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int InsertVendorService(int DistributorID, int LoginID, int ClientTypeID, SVendor svn)
        {
            int a = 0;
            try
            {
                DBVendor vn = new DBVendor();
                vn.VendorCode = svn.VendorCode;
                vn.VendorName = svn.VendorName;
                vn.VendorEmail = svn.VendorEmail;
                vn.VendorContactPerson = svn.VendorContactPerson;
                vn.VendorAddress = svn.VendorAddress;
                vn.VendorMobile = svn.VendorMobile;
                vn.IsActive = svn.IsActive;
                a = vn.InsertVendor(DistributorID, LoginID, ClientTypeID);


                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateVendorService(int DistributorID, int LoginID, int ClientTypeID, SVendor svn)
        {
            int a = 0;
            try
            {
                DBVendor vn = new DBVendor();
                vn.VendorID = svn.VendorID;
                vn.VendorCode = svn.VendorCode;
                vn.VendorName = svn.VendorName;
                vn.VendorEmail = svn.VendorEmail;
                vn.VendorContactPerson = svn.VendorContactPerson;
                vn.VendorAddress = svn.VendorAddress;
                vn.VendorMobile = svn.VendorMobile;
                vn.IsActive = svn.IsActive;
                a = vn.UpdateVendor(DistributorID, LoginID, ClientTypeID);


                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetVendorListService()
        {
            DataSet ds = null;
            try
            {
                DBVendor vend = new DBVendor();
                ds = vend.GetVendorList();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetSingleVendorService(SVendor sVen)
        {
            DataSet ds = null;
            try
            {
                DBVendor vend = new DBVendor();
                vend.VendorID = sVen.VendorID;
                ds = vend.GetSingleVendor();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataTable GetTransactionIDService()
        {
            DataTable dt = null;
            try
            {
                cSIM sim = new cSIM();
                dt = sim.GetTransactionID();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetIPMapping(Int32 Distributorid)
        {
            DataTable dt = null;
            try
            {
                DBIP D = new DBIP();
                dt = D.GetIPMapping(Distributorid);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }


        public DataSet GetMsimDetails(string MSIM, string MNPNo)
        {
            DataSet ds = null;
            try
            {
                cSIM sim = new cSIM();
                ds = sim.GetMsimDetails(MSIM, MNPNo);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        public DataSet GetTestDataService()
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.GetTestData();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InsertPaypalTopupService(int distributorID, int LoginID, SPayment sp)
        {
            DataSet ds = null;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.TxnDate = sp.TxnDate;
                dsp.Currency = sp.Currency;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.ActivationVia = sp.ActivationVia;
                ds = dsp.InsertPaypalPayment(distributorID, LoginID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }

        }

        public int UpdatePaypalTopupService(int distributorID, int LoginID, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentId = sp.PaymentId;
                dsp.TxnId = sp.TxnId;
                dsp.TxnAmount = sp.TxnAmount;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.ReceiptId = sp.ReceiptId;
                dsp.PayerId = sp.PayerId;
                dsp.TxnDate = sp.TxnDate;
                dsp.CheckSumm = sp.CheckSumm;
                dsp.Month = sp.Month;
                a = dsp.UpdatePaypalPayment(distributorID, LoginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int UpdatePaypalActivationService(int distributorID, int LoginID, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.PaymentType = sp.PaymentType;
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentId = sp.PaymentId;
                dsp.TxnId = sp.TxnId;
                dsp.TxnAmount = sp.TxnAmount;
                dsp.TransactionStatus = sp.TransactionStatus;
                dsp.TransactionStatusId = sp.TransactionStatusId;
                dsp.ReceiptId = sp.ReceiptId;
                dsp.PayerId = sp.PayerId;
                dsp.TxnDate = sp.TxnDate;
                dsp.CheckSumm = sp.CheckSumm;
                a = dsp.UpdatePaypalActivationPayment(distributorID, LoginID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int UpdatePaypalAccountBalanceService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp)
        {
            int a = 0;
            try
            {
                DBPayment dsp = new DBPayment();
                dsp.ChargedAmount = sp.ChargedAmount;
                dsp.PaymentType = sp.PaymentType;
                dsp.PayeeID = sp.PayeeID;
                dsp.PaymentFrom = sp.PaymentFrom;
                dsp.ActivationType = sp.ActivationType;
                dsp.PaymentType = sp.PaymentType;
                dsp.ActivationStatus = sp.ActivationStatus;
                dsp.ActivationVia = sp.ActivationVia;
                dsp.ActivationResp = sp.ActivationResp;
                dsp.ActivationRequest = sp.ActivationRequest;
                dsp.TariffID = sp.TariffID;
                dsp.ALLOCATED_MSISDN = sp.ALLOCATED_MSISDN;
                dsp.PaymentMode = sp.PaymentMode;
                dsp.TransactionId = sp.TransactionId;
                dsp.PaymentId = sp.PaymentId;
                dsp.TransactionStatusId = sp.TransactionStatusId;

                a = dsp.UpdatePayPalAccountBalance(distributorID, LoginID, sim, zipcode, channel, lang);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        //public DataSet GetTopupPaymentDetailsService(int distributorID, int LoginID, int ClientTypeID, DateTime FromDate, DateTime ToDate)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        DBPayment dsp = new DBPayment();
        //        ds = dsp.GetTopupPaymentDetails(distributorID, LoginID, ClientTypeID, FromDate, ToDate);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds;
        //    }
        //}

        public DataSet GetTopupPaymentDetailsService(int distributorID, int LoginID, int ClientTypeID, string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();

            try
            {
                DBPayment dsp = new DBPayment();
                ds = dsp.GetTopupPaymentDetails(distributorID, LoginID, ClientTypeID, FromDate, ToDate);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public DataSet DeactivateDistirbutorService(int DistributorId, int LoginId, string Condition)
        {
            DataSet ds = null;
            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.DeactivateDistirbutor(DistributorId, LoginId, Condition);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }

        }

        public DataSet GetSearch(string SerachFor, string SearchText, int ClientID, int ClientTypeID, string EmailID, string DateType, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cr = new cReport();
                ds = cr.GetSearch(SerachFor, SearchText, ClientID, ClientTypeID, EmailID, DateType, FromDate, ToDate);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        //public int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID)
        //{
        //    int a = 0;

        //    try
        //    {
        //        DBPayment dsp = new DBPayment();
        //        a = dsp.ChangeStatusForTopUp(distributorID, LoginID, ClientTypeID, PaymentID);
        //        return a;
        //    }
        //    catch (Exception ex)
        //    {
        //        return a;
        //    }
        //}

        public int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID, int StatusManual, string SRemark)
        {
            int a = 0;

            try
            {
                DBPayment dsp = new DBPayment();
                a = dsp.ChangeStatusForTopUp(distributorID, LoginID, ClientTypeID, PaymentID, StatusManual, SRemark);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }


        public DataSet GetDistributorofMappingwithPlan(int TariffID)
        {
            DataSet ds = null;
            try
            {
                DBDistributor d = new DBDistributor();
                ds = d.GetDistributorofMappingwithPlan(TariffID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SaveDistributorofMappingwithPlan(decimal Rental, DataTable dt, int TariffID)
        {
            int a = 0;
            try
            {
                DBDistributor d = new DBDistributor();

                a = d.SaveDistributorofMappingwithPlan(Rental, dt, TariffID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }
        public int SaveDistributorRechageBulk(decimal Rental, DataTable dt, int NetworkID)
        {
            int a = 0;
            try
            {
                DBDistributor d = new DBDistributor();

                a = d.SaveDistributorRechageBulk(Rental, dt, NetworkID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }
        public DataSet GetImportFile(string xmlSTR)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                ds = cR.GetImportFile(xmlSTR);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetImportFileDetailsService(DataTable dt, string Action)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                ds = cR.GetImportFileDetails(dt, Action);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportTopup(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportTopup();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetReportDeduct(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;
                cR.LoginID = LoginID;

                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportDeduct();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetReportTopupLedger(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;


                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetReportTopupLedger();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetLedgerReport(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cR = new cReport();

                cR.ClientID = ClientID;
                cR.ClientTypeID = ClientTypeID;


                cR.FromDate = FromDate;
                cR.ToDate = ToDate;


                ds = cR.GetLedgerReport();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        //public DataSet SaveAndSendNotification(long LoginID, long DistributorID, DataTable dt, string NotificationText, string Status)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        cNotification cN = new cNotification();

        //        ds = cN.SaveAndSendNotification(LoginID, DistributorID, dt, NotificationText, Status);

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds;
        //    }
        //}

        public DataSet SaveAndSendNotification(long LoginID, long DistributorID, DataTable dt, string NotificationText, string Status, string Action, int id)
        {
            DataSet ds = null;
            try
            {
                cNotification cN = new cNotification();

                ds = cN.SaveAndSendNotification(LoginID, DistributorID, dt, NotificationText, Status, Action, id);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetNotification(long DistributorID, int MsgID)
        {
            DataSet ds = null;
            try
            {
                cNotification cN = new cNotification();

                ds = cN.GetNotification(DistributorID, MsgID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }



        public DataSet ViewNotification(long Createdby, int MsgID)
        {
            DataSet ds = null;
            try
            {
                cNotification cN = new cNotification();

                ds = cN.ViewNotification(Createdby, MsgID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet FetchContactDetail(int Distributorid)
        {
            DataSet ds = null;
            try
            {
                DBUsers dU = new DBUsers();

                ds = dU.FetchContactDetail(Distributorid);

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
                DBPayment cN = new DBPayment();

                ds = cN.SaveProductMaster(NetworkID, ProductID, ShortName, ProductDescription, Currency, Amount, CreatedBy);

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
                DBPayment cN = new DBPayment();

                ds = cN.SaveTransactionDetails(NetworkID, ProductID, TransactionType, ProductPinID, SIMNumber, InvoiveNumber, Amount, Currency, City, Zip, NPA, CreatedBy, ChargeAmount);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public int UpdateAccountBalanceAfterRecharge(int NetworkID, int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string ZipCode, string RechargeStatus, string RechargeVia, string RechargeRequest, string RechargeResponse, int LoginID, int PaymentFrom, string PaymentMode, string TransactionId, int Currency, string TransactionStatus, int TransactionStatusId, string PinNumber, string State, string TxnID, string Tax, string TotalAmount, string InvoiceNo, string StatusVia, string Regulatery)
        {


            int a = 0;

            try
            {
                DBPayment dsp = new DBPayment();
                a = dsp.UpdateAccountBalanceAfterRecharge(NetworkID, TariffID, SerialNumber, ChargedAmount, distributorID, ZipCode, RechargeStatus, RechargeVia, RechargeRequest, RechargeResponse, LoginID, PaymentFrom, PaymentMode, TransactionId, Currency, TransactionStatus, TransactionStatusId, PinNumber, State, TxnID, Tax, TotalAmount, InvoiceNo, StatusVia, Regulatery);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }


        }



        public int UpdateAccountBalanceAfterRechargeNew(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest,
            string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID)
        {


            int a = 0;

            try
            {
                DBPayment dsp = new DBPayment();
                dsp.TransactionId = TxnID;
                a = dsp.UpdateAccountBalanceAfterRechargeNew(TariffID, SerialNumber, ChargedAmount, distributorID, RechargeStatus, TransactionStatus, TransactionStatusId, RechargeVia, RechargeRequest,
             RechargeResponse, TotalAmount, LoginID, TxnID, Regulatery, Month, PaymentFrom, PaymentMode, DataAddOnID, InternationalID);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }


        }

        // h2o recharge

        public int UpdateAccountBalanceAfterRechargeNewForH2O(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest,
           string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID)
        {


            int a = 0;

            try
            {
                DBPayment dsp = new DBPayment();
                dsp.TransactionId = TxnID;
                a = dsp.UpdateAccountBalanceAfterRechargeNewForH2O(TariffID, SerialNumber, ChargedAmount, distributorID, RechargeStatus, TransactionStatus, TransactionStatusId, RechargeVia, RechargeRequest,
             RechargeResponse, TotalAmount, LoginID, TxnID, Regulatery, Month, PaymentFrom, PaymentMode, DataAddOnID, InternationalID);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }


        }

        // h2o recharge

        public DataSet GetTariffGroupService(int LoginID, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetTariffGroup(LoginID, DistributorID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetTariffGroupViewService(int TariffID)
        {
            DataSet ds = null;
            try
            {
                DBTariff dbt = new DBTariff();

                ds = dbt.GetTariffGroup(TariffID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public int AddNewTariffService(STariff st, int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                DBTariff dst = new DBTariff();
                dst.tarifName = st.TarifName;
                dst.sellerID = st.SellerID;
                dst.discount_on_Activation_PortIn = st.Discount_on_Activation_PortIn;
                dst.discount_on_Recharge = st.Discount_on_Recharge;

                a = dst.SaveNewTariff(LoginID, DistributorID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public int UpdateTariffGroupService(STariff st, int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                DBTariff dst = new DBTariff();

                dst.tarifID = st.TarifID;
                dst.tarifName = st.TarifName;
                dst.sellerID = st.SellerID;
                dst.discount_on_Activation_PortIn = st.Discount_on_Activation_PortIn;
                dst.discount_on_Recharge = st.Discount_on_Recharge;
                dst.isActive = st.IsActive;

                a = dst.UpdateTariffGroup(LoginID, DistributorID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public void UpdateAccountBalanceAfterCancelPortIn(string MNPRefNum, string POrtInCancelReq, string PortInCancelResp, int UserId)
        {
            try
            {
                DBPayment Dbp = new DBPayment();
                Dbp.UpdateAccountBalanceAfterCancelPortIn(MNPRefNum, POrtInCancelReq, PortInCancelResp, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetPrintRecipt(string tansactionId)
        {
            DataSet ds = null;
            try
            {
                DBPayment Dbp = new DBPayment();
                ds = Dbp.GetPrintRecipt(tansactionId);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDistributor1(int UserId, int Distrib)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.GetDistributor(UserId, Distrib);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetActivationsWithoutMSISDN()
        {
            DataSet ds = new DataSet();
            try
            {
                DBPayment dis = new DBPayment();
                ds = dis.GetActivationsWithoutMSISDN();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public void UpdateActivationMSISDN(long ActivationID, string MSISDN)
        {
            try
            {
                DBPayment Dbp = new DBPayment();
                Dbp.UpdateActivationMSISDN(ActivationID, MSISDN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetRandomPassword(Int64 DistributorID)
        {
            DataSet ds = new DataSet();

            try
            {
                DBUsers dis = new DBUsers();
                ds = dis.GetRandomPassword(DistributorID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet SaveRandomPassword(Int64 DistributorID, string Password)
        {
            DataSet ds = new DataSet();

            try
            {
                DBUsers dis = new DBUsers();
                ds = dis.SaveRandomPassword(DistributorID, Password);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetCommissionDetail(Int64 DistributorID, int Month, int Year, string MonthYear)
        {
            DataSet ds = new DataSet();

            try
            {
                cReport dis = new cReport();
                ds = dis.GetCommissionDetail(DistributorID, Month, Year, MonthYear);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int VerifySimNumber(string OldSimNumber, string NewSimNumber)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();

                a = cS.VerifySimNumber(OldSimNumber, NewSimNumber);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public int UpdateSwapSim(string MSISDN, string OldSimNumber, string NewSimNumber, Int64 Createdby)
        {
            int a = 0;
            try
            {
                cSIM cS = new cSIM();
                a = cS.UpdateSwapSim(MSISDN, OldSimNumber, NewSimNumber, Createdby);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet FetchInventory(int ClientID, string Action, string SimNumber, string FromSim, string ToSim, int NetworkID)
        {
            DataSet ds = null;
            try
            {
                cSIM s = new cSIM();
                ds = s.FetchInventory(ClientID, Action, SimNumber, FromSim, ToSim, NetworkID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public int SwapSimRequest(string MSISDN, string OldSimNumber, string NewSimNumber, int Createdby, string Request, string Response, string Status)
        {
            int a = 0;
            try
            {
                cSIM s = new cSIM();
                a = s.SwapSimRequest(MSISDN, OldSimNumber, NewSimNumber, Createdby, Request, Response, Status);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }


        public int UpdateHoldStatus(Int64 DistributorID, int HoldStatus, string HoldReason)
        {
            int a = 0;
            try
            {
                DBDistributor d = new DBDistributor();

                a = d.UpdateHoldStatus(DistributorID, HoldStatus, HoldReason);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        #region Update Topup Option
        public int UpdateTopupOption(Int64 DistributorID, decimal MinTopup, decimal MaxTopup, decimal PaypalTax, string PaypalTaxType)
        {
            int a = 0;
            try
            {
                DBDistributor d = new DBDistributor();

                a = d.UpdateTopupOption(DistributorID, MinTopup, MaxTopup, PaypalTax, PaypalTaxType);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        #endregion


        public DataSet RechargeActivationCancelDetail(string MSISDN, string SerialNumber, string Action)
        {
            DataSet ds = null;
            try
            {
                cSIM cS = new cSIM();

                ds = cS.RechargeActivationCancelDetail(MSISDN, SerialNumber, Action);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int SaveRechargeActivationCancelDetails(int ID, string MSISDN, int TariffID, Decimal Rental, Decimal RegulatoryFee, string CancelType, Int16 CancelMonth)
        {
            int a = 0;
            try
            {
                cSIM cs = new cSIM();

                a = cs.SaveRechargeActivationCancelDetails(ID, MSISDN, TariffID, Rental, RegulatoryFee, CancelType, CancelMonth);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }



        public DataSet GetHoldReason(Int64 DistributorID)
        {
            DataSet ds = null;
            try
            {
                cNotification cn = new cNotification();

                ds = cn.GetHoldReason(DistributorID);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        //public DataSet GetRechargeActivationCancelReport(DateTime CancelDate)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        cReport cs = new cReport();

        //        ds = cs.GetRechargeActivationCancelReport(CancelDate);

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds;
        //    }
        //}


        public DataSet GetRechargeActivationCancelReport(DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                cReport cs = new cReport();

                ds = cs.GetRechargeActivationCancelReport(FromDate, ToDate);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public int pGetImeurl(string Url, int ImgFlag)
        {


            int a = 0;


            try
            {
                DBPayment v = new DBPayment();

                a = v.pGetImeurl(Url, ImgFlag);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        // akash change starts
        public DataSet pUpdateImagePosition(int ImageID, int Position)
        {
            DataSet ds = null;
            try
            {
                cReport dbs = new cReport();
                ds = dbs.pUpdateImagePosition(ImageID, Position);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        //public DataSet GetCountryByAreaCode(int _AreaCode)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        cSIM s = new cSIM();
        //        ds = s.GetCountryByAreaCode(_AreaCode);

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds;
        //    }
        //}

        public DataSet GetCurrentbalance(int DistributorID)
        {
            DataSet ds = null;
            try
            {
                DBPayment Dbp = new DBPayment();
                ds = Dbp.GetCurrentbalance(DistributorID);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // akash change ends


        public DataSet GetImageUrl(int ImgFlag)
        {
            DataSet ds = null;
            try
            {
                cReport dbs = new cReport();
                ds = dbs.GetImageUrl(ImgFlag);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet DeleteImage(int ImageID)
        {
            DataSet ds = null;
            try
            {
                cReport dbs = new cReport();
                ds = dbs.DeleteImage(ImageID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet getImage1FromDB(int ImgFlag)
        {
            DataSet ds = null;
            try
            {
                cReport dbs = new cReport();
                ds = dbs.getImage1FromDB(ImgFlag);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet CheckSimAndMobileExistance(string SimNumber, string MobileNumber)
        {
            try
            {
                DBPayment dp = new DBPayment();
                DataSet ds = dp.CheckSimAndMobileExistance(SimNumber, MobileNumber);
                return ds;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }


        public DataSet GetTransactionReport(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                cReport cr = new cReport();
                DataSet ds = cr.GetTransactionReport(FromDate, ToDate);
                return ds;
            }

            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }


        public List<Distributor> GetDistributorServiceWithDate(int Userid, int Distributorid, string TaxDocument, string ResellerCertificate, DateTime FromDate, DateTime ToDate)
        {
            DBDistributor dis = new DBDistributor();
            DataSet ds = dis.GetDistributorServiceWithDate(Userid, Distributorid, TaxDocument, ResellerCertificate, FromDate, ToDate);
            List<Distributor> ditribtr = new List<Distributor>();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Distributor d = new Distributor();
                    d.distributorID = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                    d.distributorName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    d.distributorCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Acode"]);
                    d.companyType = Convert.ToInt32(ds.Tables[0].Rows[i]["ClientType"]);
                    d.companyTypeName = Convert.ToString(ds.Tables[0].Rows[i]["ClientTypeName"]);
                    d.parent = Convert.ToInt32(ds.Tables[0].Rows[i]["ParentDistributorID"]);
                    d.contactPerson = Convert.ToString(ds.Tables[0].Rows[i]["ContactPerson"]);
                    d.contactNo = Convert.ToString(ds.Tables[0].Rows[i]["ContactNumber"]);
                    d.webSiteName = Convert.ToString(ds.Tables[0].Rows[i]["WebSite"]);
                    d.emailID = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                    d.address = Convert.ToString(ds.Tables[0].Rows[i]["AddressLine"]);
                    d.city = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    d.state = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    d.zip = Convert.ToString(ds.Tables[0].Rows[i]["Zip"]);
                    d.countryid = Convert.ToInt32(ds.Tables[0].Rows[i]["CountryId"]);
                    d.balanceAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["AccountBalance"]);
                    d.isActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                    d.parentDistributor = Convert.ToString(ds.Tables[0].Rows[i]["PName"]);
                    d.NoOfBlankSim = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfBlankSim"]);

                    d.NoOfActivation = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfActivation"]);
                    d.EIN = Convert.ToString(ds.Tables[0].Rows[i]["EIN"]);
                    d.SSN = Convert.ToString(ds.Tables[0].Rows[i]["SSN"]);
                    d.PanNumber = Convert.ToString(ds.Tables[0].Rows[i]["PanNumber"]);

                    d.Document = Convert.ToString(ds.Tables[0].Rows[i]["TaxDocument"]);
                    d.Holdstatus = Convert.ToString(ds.Tables[0].Rows[i]["isHold"]);
                    d.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDtTm"]);
                    d.ModifiedDate = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDtTm"]);
                    d.Certificate = Convert.ToString(ds.Tables[0].Rows[i]["ResellerCertificate"]);
                    ditribtr.Add(d);

                }

                return ditribtr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDistributorInformation(int Distributorid)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDistributor dis = new DBDistributor();
                ds = dis.GetDistributorInformation(Distributorid);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet CountActivationPreloaded(int DistributorID, int ClientTypeID, int loginID, int month, int year, int NetworkID)
        {
            DataSet ds = new DataSet();

            try
            {
                DBDashboard dis = new DBDashboard();
                ds = dis.CountActivationPreloaded(DistributorID, ClientTypeID, loginID, month, year, NetworkID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet checkTaxId(string taxId, Int64 distributorID)
        {
            DataSet ds = null;
            try
            {
                DBDistributor Dis = new DBDistributor();
                ds = Dis.checkTaxId(taxId, distributorID);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }


        public int SaveIpMapping(long DistributorID, string _IP, int chkRistrictIP)
        {
            int a = 0;
            try
            {
                DBIP IP = new DBIP();
                a = IP.SaveIpMapping(DistributorID, _IP, chkRistrictIP);
                return a;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add by akash starts

        public int DeleteMappingID(int ID)
        {
            int a = 0;
            try
            {
                DBIP objDTS = new DBIP();
                a = objDTS.DeleteMappingID(ID);

                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }


        public DataTable GetRolewiseScreen(int LoginID=0, int RoleID=0)
        {
            DataTable dt = new DataTable();
            try
            {
                DBRole objDTS = new DBRole();

                dt = objDTS.GetRolewiseScreen(LoginID, RoleID);

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        // ***
        public DataTable PerticularAPIDOWN(string APINAME)
        {
            DataTable dt = null;
            try
            {
                cVendor v = new cVendor();

                dt = v.PerticularAPIDOWN(APINAME);

                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
      
        // add by akash ends

        //
        #region "Manual Commission Processing"
        public int SaveITRFile(DataTable objitr, string filename)
        {
            try
            {
                DBPayment dbp = new DBPayment();

                return dbp.SaveITRFile(objitr, filename);
            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public int ProcessingManualCommission(string stepNo)
        {
            try
            {
                DBPayment dbp = new DBPayment();

                return dbp.ProcessingManualCommission(stepNo);
            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        #endregion

        #region "H2O"
        public DataSet GetCountryByAreaCode(int _AreaCode)
        {
            DataSet ds = null;
            try
            {
                DBH2O s = new DBH2O();
                ds = s.GetCountryByAreaCode(_AreaCode);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetH2OStatesList()
        {
            DataSet ds = null;
            try
            {
                DBH2O s = new DBH2O();
                ds = s.GetH2OStatesList();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetH2OServiceProviderList()
        {
            DataSet ds = null;
            try
            {
                DBH2O s = new DBH2O();
                ds = s.GetH2OServiceProviderList();

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        #endregion


    }
}
