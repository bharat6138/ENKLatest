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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GETLEVELDistributor(Int32 distributorID, int treeLevel);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateAccountDingRecharge(int distributorID, int LoginID, string MobileNumber, string channel, string lang, SPayment sp, string SkuTariffCode, string TariffDescription, string TransferRef, decimal TxnAmount, string RespCode, string RespMsg, string TxnDate, string Country, string Operator, string SendCurrencyIso);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable GetCountriesForDing();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable OperatorAginstCountry(string countryISO);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int StoredAPIRequestBeforeCall(string Title, string Request, Int32 DistributorId, string TransactionID, string Msisdn ,string SIMNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable CheckDuplicateRecharge(string MSISDN, int TariffID, Int32 DistributorId);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckAlreadySuccessfullPortIn(string SimNumber, string PhoneNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventory1(int ClientID, string Action, string SimNumber, string FromSimNumber, string ToSimNumber);




        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable GetIPMapping(Int32 Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetImportFile(string xmlSTR);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveTariffGroupSpiffMapping(STariff st, int LoginID, int TariffGroupId, string Action, decimal RechargeCommission, decimal H2ORechargeDiscount,decimal Comission, decimal H2OGeneralDiscount);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRegulatery();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDistributorNotes(int ClientID, int ClientTypeID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetStockTransferReport(int ClientID, int ClientTypeID, DateTime fromDate, DateTime Todate);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveDistributorNotes(int DistributorID, string Notes, int LoginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateStatusActivation(int ActivationID, int LoginID);
        //------shadab ali
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int ResetSubscriberPassword(string UserID, string Password);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportAcivationFail(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateStatusRecharge(int RechargeID, int LoginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRechargeFailReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, string RechargeVia);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet UpdateBulkNetwork(int NetworkID, DataTable dt);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckDuplicatePaypalTxnID(string TxnID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckSimNumber(int NetworkID, DataTable dt);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDistributor(int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveDistributorRechageBulk(decimal Rental, DataTable dt, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetLoginHistory(DateTime FromDate, DateTime ToDate, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportDeduct(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveRechageRequest(int NetworkID, int TariffID, string SerialNumber, string ZipCode, string RechargeRequest, string EmailID, string City, decimal Amount, decimal TaxAmount, decimal TotalAmount, decimal Regulatery, int CreatedBy, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetPurchaseCode();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetPurchaseReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int PurchaseID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]

        DataSet DeductDistributorTopUpAmount(int Distributorid, decimal Amount, string Remarks);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportRechargeFilterwise(int ClientID, int ClientTypeID, int LoginID, string MobileNo, string TxnID, string RechargeVia);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]

        int UpdateAccountBalanceAfterRecharge(int NetworkID, int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string ZipCode, string RechargeStatus, string RechargeVia, string RechargeRequest, string RechargeResponse, int LoginID, int PaymentFrom, string PaymentMode, string TransactionId, int Currency, string TransactionStatus, int TransactionStatusId, string PinNumber, string State, string TxnID, string Tax, string TotalAmount, string InvoiceNo, string StatusVia, string Regulatry);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateAccountBalanceAfterRechargeNew(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest,
            string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID);

        // h2o recharge
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateAccountBalanceAfterRechargeNewForH2O(int TariffID, string SerialNumber, decimal ChargedAmount, int distributorID, string RechargeStatus, string TransactionStatus, int TransactionStatusId, string RechargeVia, string RechargeRequest,
            string RechargeResponse, string TotalAmount, int LoginID, string TxnID, string Regulatery, string Month, int PaymentFrom, string PaymentMode, int DataAddOnID, int InternationalID);
        // h2o recharge




        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckRechargeDuplicate(int NetworkID, string SerialNumber, int TariffID, string InvoiceNo);





        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetProductRecharge(int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetState();


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSimNetwork(string SimNo, string Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportActivationLedger(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, Int32 CurrentLogin);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet UpdatePurchaseSimNetwork(int VendorID, string SimNo, long PurchaseID);




        //--------Vivek
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveInventory(SIM s, int UserID, Actions Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable GetVendor(int UserID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable PreloadedSIMCheck(string SimNo);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffService(int LoginID, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InventoryTransfer(SIM s, int UserID, Actions Action, string checkInventoryWAY);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventory(int ClientID, string Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventoryForAccept(int ClientID, int LoginClientID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventoryForSIMReplacement(string MSISDNNo, string SIMNo, int ClientID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SIMReplacement(SIM s, Actions Action);

      

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportInventoryStatus(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportSalesReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportActivationSIM(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int checkMainDistributor, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRechargeSIMReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRequesrRechargeSIMReport(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate, string RechargeVia);



        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportSIMHistory(int ClientID, int ClientTypeID, int LoginID, string SearchText);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SimTariffMapping(SIM s, int UserID, Actions Action, int Months);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSaleReportForActivationAndPortIn(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventoryBulkTransfer(int ClientID, DataTable SIMDt);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventoryForAcceptOnDasboard(int LoginClientID);

        //--------Pankaj


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int GetData(int value);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet AddDistirbutorService(Distributor distbtr, int UserID, DataTable dt, DataTable dtRecharge, string UserName, int ChkSellr, int Chktariffgroup);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateDistirbutorService(Distributor dist, int Userid, DataTable dt, DataTable dtRecharge, string Username, string passw, int ChkSellr, int Chktariffgroup);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        List<Distributor> GetDistributorService1(int Userid, int Distributorid, string TaxDocument, string ResellerCertificate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        List<Distributor> GetSingleDistributorService(int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSingleDistributorTariffService(int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetClientType(int loginID, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        List<Distributor> GetDistributorDDLService(int Userid, int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRoleService();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ValidateLoginService(string UserName, string Pwd);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ValidateLoginApp(Int64 LoginId);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int AddUserService(SUsers ud, int loginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateUserService(SUsers ud, int loginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetUserListService(int loginID, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetUserService(int UserID, int LoginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InsertCompanyTopupBalanceService(int distributorID, int LoginID, SPayment sp);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InsertLoginHistoryService(SLoginHistory sl);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetScreenService(int RoleID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSingleTariffService(int TariffID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int AddTariffService(STariff st, int LoginID, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateTariffService(STariff st, int LoginID, int DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetShortCodeService(string Condition);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffForActivationService(int LoginID, int DistributorID, int ClientTypeID);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateAccountBalanceServiceActivation(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, int NetworkID, SPayment sp, int DataAddOnID, int InternationalID, decimal DataAddOnValue, decimal DataAddOnDiscountedAmount, decimal DataAddOnDiscountPercent, decimal InternationalCreditValue, decimal InternationalCreditDiscountedAmount, decimal InternationalCreditDiscountPercent, string MNPNO, string Serialnumber = "");

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InsertSubscriberActivationDetailService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckTariffGroupExist(string GroupName);  // Added By Sarala

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ShowDashBoardDataService(int DistributorID, int ClientTypeID, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckSimActivationService(int DistributorID, int ClientTypeID, string SimNumber, string Action);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckSimPortINService(int DistributorID, int ClientTypeID, string SimNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffSpiffDetails(string mode, int Id); //By Sarala

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ShowDashBoardActivationDataService(int DistributorID, int ClientTypeID, int loginID, string Action, int month, int year, string FromDate, string ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SavePOSService(int DistributorID, int LoginID, SPOS sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ShowPOSService(int DistributorID, int ClientTypeID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InsertVendorService(int DistributorID, int LoginID, int ClientTypeID, SVendor svn);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int InsertCurrencyService(string Name);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetCurrencyService(int DistributorID, int ClientTypeID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet VerifyUserIDService(int DistributorID, int ClientTypeID, int LoginID, string UserID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ForgetPasswordService(string UserID, string Mobile);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ResetPasswordService(string UserID, string OldPass, string NewPass, int DistributorID, int LoginID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetVendorListService();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSingleTariffDetailForActivationService(int LoginID, int DistributorID, int ClientTypeID, int TariffID, int Month, string Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSingleVendorService(SVendor sVen);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateVendorService(int DistributorID, int LoginID, int ClientTypeID, SVendor svn);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable GetTransactionIDService();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet InsertPaypalTopupService(int distributorID, int LoginID, SPayment sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdatePaypalTopupService(int distributorID, int LoginID, SPayment sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTestDataService();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdatePaypalActivationService(int distributorID, int LoginID, SPayment sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdatePaypalAccountBalanceService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        //DataSet GetTopupPaymentDetailsService(int distributorID, int LoginID, int ClientTypeID, DateTime FromDate, DateTime ToDate);
        DataSet GetTopupPaymentDetailsService(int distributorID, int LoginID, int ClientTypeID, string FromDate, string ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet DeactivateDistirbutorService(int DistributorId, int LoginId, string Condition);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetSearch(string SerachFor, string SearchText, int ClientID, int ClientTypeID, string EmailID, string DateType, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        //int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID);
        int ChangeStatusForTopUp(int distributorID, int LoginID, int ClientTypeID, long PaymentID, int StatusManual, string SRemark);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDistributorofMappingwithPlan(int TariffID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveDistributorofMappingwithPlan(decimal Rental, DataTable dt, int TariffID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetImportFileDetailsService(DataTable dt, string Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportTopup(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportTopupLedger(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetLedgerReport(int ClientID, int ClientTypeID, DateTime FromDate, DateTime ToDate);



        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet SaveAndSendNotification(long LoginID, long DistributorID, DataTable dt, string NotificationText, string Status, string Action, int id);
        //DataSet SaveAndSendNotification(long LoginID,long DistributorID, DataTable dt, string NotificationText, string Status);
        // TODO: Add your service operations here

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet SaveProductMaster(int NetworkID, int ProductID, string ShortName, string ProductDescription, string Currency, string Amount, int CreatedBy);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet SaveTransactionDetails(int NetworkID, int ProductID, string TransactionType, string ProductPinID, string SIMNumber, string InvoiveNumber, string Amount, string Currency, string City, string Zip, string NPA, int CreatedBy, string ChargeAmount);
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffGroupService(int LoginID, int DistributorID);
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffGroupViewService(int TariffID);
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int AddNewTariffService(STariff st, int LoginID, int DistributorID);
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateTariffGroupService(STariff st, int LoginID, int DistributorID);

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        void UpdateAccountBalanceAfterCancelPortIn(string MNPRefNum, string POrtInCancelReq, string PortInCancelResp, int UserId);

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetPrintRecipt(string tansactionId);

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetMsimDetails(string MSIM, string MNPNo);

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        void SaveModifiedPortinDetails(string Request, string APIResponse, DateTime RequestedTime, int Requestedby, string MSISDN);

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetAddOnANDInternationCreadit();

        //Ankit Singh
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDiscountAndRental(int DistributorID, int tariffID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateAccountBalanceService(int distributorID, int LoginID, string sim, string zipcode, string channel, string lang, SPayment sp);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CHECKDistributor(string UserID);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        //DataSet ValidatePUK(string SimNumber, string PUK);
        DataSet ValidatePreloadedSim(string SimNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTariffForActivation_ForSubscriber(string simnumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetInventoryBulkTransfer1(int ClientID, DataTable SIMDt);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDistributor1(int UserId, int Distrib);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetActivationsWithoutMSISDN();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        void UpdateActivationMSISDN(long ActivationID, string MSISDN);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetNotification(long DistributorID, int MsgID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet ViewNotification(long Createdby, int MsgID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet FetchContactDetail(int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetRandomPassword(Int64 DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet SaveRandomPassword(Int64 DistributorID, string Password);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetCommissionDetail(Int64 DistributorID, int Month, int Year, string MonthYear);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int VerifySimNumber(string OldSimNumber, string NewSimNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateSwapSim(string MSISDN, string OldSimNumber, string NewSimNumber, Int64 Createdby);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet FetchInventory(int ClientID, string Action, string SimNumber, string FromSim, string ToSim, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SwapSimRequest(string MSISDN, string OldSimNumber, string NewSimNumber, int Createdby, string Request, string Response, string Status);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateHoldStatus(Int64 DistributorID, int HoldStatus, string HoldReason);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int UpdateTopupOption(Int64 DistributorID, decimal MinTopup, decimal MaxTopup, decimal PaypalTax, string PaypalTaxType);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet RechargeActivationCancelDetail(string MSISDN, string SerialNumber, string Action);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveRechargeActivationCancelDetails(int ID, string MSISDN, int TariffID, Decimal Rental, Decimal RegulatoryFee, string CancelType, Int16 CancelMonth);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetHoldReason(Int64 DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        //DataSet GetRechargeActivationCancelReport(DateTime CancelDate);
        DataSet GetRechargeActivationCancelReport(DateTime FromDate, DateTime ToDate);

        //  rudra
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetImageUrl(int ImgFlag);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int pGetImeurl(string Url, int ImgFlag);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet DeleteImage(int ImageID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet getImage1FromDB(int ImgFlag);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet pUpdateImagePosition(int ImageID, int Position);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetApiDetail(Int64 DistributorID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveApiMapping(long DistributorID, int clientcode, DataTable dtApi, int chkApiStatus);

        //[OperationContract]
        //[FaultContract(typeof(ServiceData))]
        //int SaveIpMapping(long DistributorID, DataTable dtIp, int chkRistrictIP);
        //  rudra

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CheckSimAndMobileExistance(string SimNumber, string MobileNumber);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetTransactionReport(DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        List<Distributor> GetDistributorServiceWithDate(int Userid, int Distributorid, string TaxDocument, string ResellerCertificate, DateTime FromDate, DateTime ToDate);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetDistributorInformation(int Distributorid);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet CountActivationPreloaded(int DistributorID, int ClientTypeID, int loginID, int month, int year, int NetworkID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet checkTaxId(string taxId, Int64 distributorID);

        // add by akash starts
        //[OperationContract]
        //[FaultContract(typeof(ServiceData))]
        //DataSet GetCountryByAreaCode(int _AreaCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetCurrentbalance(int DistributorID);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetReportInventoryPurchase(int ClientID, int ClientTypeID, int LoginID, DateTime FromDate, DateTime ToDate);
        // add by akash ends

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveIpMapping(long DistributorID, string _IP, int chkRistrictIP);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int DeleteMappingID(int ID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable GetRolewiseScreen(int LoginID, int RoleID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable PerticularAPIDOWN(string APINAME);

        #region "Manual Commission Processing"
        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int SaveITRFile(DataTable objitr, string filename);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        int ProcessingManualCommission(string stepNo);
        #endregion

        #region "H2O"

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetCountryByAreaCode(int _AreaCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetH2OStatesList();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataSet GetH2OServiceProviderList();

        #endregion
    }


    [DataContract]
    public class ServiceData
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }

    [DataContract]
    public enum Actions
    {
        [EnumMember]
        INSERT,
        [EnumMember]
        UPDATE,
        [EnumMember]
        DELETE
    };



}
