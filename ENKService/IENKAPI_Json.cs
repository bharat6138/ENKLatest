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
    [ServiceContract]
    public interface IENKAPI_Json
    {
        #region "Andriod"
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ValidateLoginService_Andriod?UserName={UserName}&Pwd={Pwd}")]
        string ValidateLoginService_Andriod(string UserName, string Pwd);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetTariffForActivationService?LoginID={LoginID}&DistributorID={DistributorID}&ClientTypeID={ClientTypeID}")]
        string GetTariffForActivationService(string LoginID, string DistributorID, string ClientTypeID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetSingleDistributorService?DistributorID={DistributorID}")]
        string GetSingleDistributorService(string DistributorID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CheckBalance?DistributorID={DistributorID}")]
        Balance CheckBalance(string DistributorID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CheckSimActivationService?DistributorID={DistributorID}&ClientTypeID={ClientTypeID}&SimNumber={SimNumber}")]
        string CheckSimActivationService(string DistributorID, string ClientTypeID, string SimNumber);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetSingleTariffDetailForActivationService?LoginID={LoginID}&DistributorID={DistributorID}&ClientTypeID={ClientTypeID}&TariffID={TariffID}&Month={Month}")]
        string GetSingleTariffDetailForActivationService(string LoginID, string DistributorID, string ClientTypeID, string TariffID, string Month, string Action);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ActivateSIM?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&VoucherPIN={VoucherPIN}&TopUP={TopUP}")]
        string ActivateSIM(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode,string VoucherPIN="", string TopUP="");
        #endregion
       
        
        #region "Andriod Shadab Ali-20-Jun-2017"
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetNetwork")]
        string GetNetwork();
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetState")]
        string GetState();
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRechargePlan?NetworkID={NetworkID}&State={State}")]
        string GetRechargePlan(int NetworkID,string State);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ValidateSubscriberLogin?UserName={UserName}&Pwd={Pwd}")]
        string ValidateSubscriberLogin(string UserName, string Pwd);
        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateSubscriberUser?EmailID={EmailID}&MobileNumber={MobileNumber}&Password={Password}&UserType={UserType}")]
        string CreateSubscriberUser(string EmailID, string MobileNumber, string Password, string UserType);
        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SubscriberUserVerification?UserName={UserName}&OTP={OTP}")]
        string SubscriberUserVerification(string UserName, string OTP);
        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ValidateSubscriberFacebookLogin?EmailID={EmailID}")]
        string ValidateSubscriberFacebookLogin(string EmailID);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InitiateTopupPayment?loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}")]
        InitiatePaymentResponse InitiateTopupPayment(int loginID, int DistributorID, string ChargedAmount);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateTopupPayment?Response={Response}&TxnID={TxnID}&StatusID={StatusID}&PaymentId={PaymentId}&loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}")]
        ConfirmPaymentResponse UpdateTopupPayment(string Response, string TxnID, int StatusID, string PaymentId, string loginID, string DistributorID, string ChargedAmount);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InitiatePaymentWaletRecharge?loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}")]
        string InitiatePaymentWaletRecharge(int loginID, int DistributorID, string ChargedAmount);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InitiatePaymentPaypalRecharge?loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}")]
        string InitiatePaymentPaypalRecharge(int loginID, int DistributorID, string ChargedAmount);

        

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRechargeDistributorPlan?NetworkID={NetworkID}&DistributorID={DistributorID}")]
        string GetRechargeDistributorPlan(int NetworkID, int DistributorID);
         
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SubscriberForgetPassword?EmailID={EmailID}")]
        string SubscriberForgetPassword(string EmailID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Recharge?PaymentID={PaymentID}&NetworkID={NetworkID}&TariffCode={TariffCode}&MobileNo={MobileNo}&TotalAmount={TotalAmount}&EmailID={EmailID}&RechargeAmount={RechargeAmount}&State={State}&ZIPCode={ZIPCode}&TxnID={TxnID}&Tax={Tax}&Regulatery={Regulatery}&DistributorID={DistributorID}&LoginID={LoginID}&RechargeVia={RechargeVia}&PlanDescription={PlanDescription}&IsWalet={IsWalet}")]
        string Recharge(int PaymentID, string NetworkID, string TariffCode, string MobileNo, string TotalAmount, string EmailID, string RechargeAmount, string State, string ZIPCode, string TxnID, string Tax, string Regulatery, int DistributorID, int LoginID, string RechargeVia, string PlanDescription, int IsWalet);
       
        
        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetActivationPlan?NetworkID={NetworkID}&DistributorID={DistributorID}")]
        string GetActivationPlan(int NetworkID, int DistributorID);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ActivateSIMForLycaMobile?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&Month={Month}&IsWalet={IsWalet}&TariffPlan={TariffPlan}")]
        string ActivateSIMForLycaMobile(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Month, int IsWalet, string TariffPlan);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InitiatePaymentPaypalAcivationForLycaMobile?loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}&TariffID={TariffID}")]
        string InitiatePaymentPaypalAcivationForLycaMobile(int loginID, int DistributorID, string ChargedAmount, string TariffID);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ActivateSIMForLycaMobileWithPaypal?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&Month={Month}&PaymentID={PaymentID}&TariffPlan={TariffPlan}")]
        string ActivateSIMForLycaMobileWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Month, int PaymentID, string TariffPlan);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ActivateSIMForH2O?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&City={City}&IsWalet={IsWalet}&TariffPlan={TariffPlan}")]
        string ActivateSIMForH2O(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, int IsWalet, string TariffPlan);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ActivateSIMForH2OWithPaypal?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&City={City}&PaymentId={PaymentId}&TariffPlan={TariffPlan}")]
        string ActivateSIMForH2OWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City,  string PaymentId,string TariffPlan);
  
      
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SubscriberRecharge?PaymentID={PaymentID}&NetworkID={NetworkID}&TariffCode={TariffCode}&MobileNo={MobileNo}&TotalAmount={TotalAmount}&EmailID={EmailID}&RechargeAmount={RechargeAmount}&State={State}&ZIPCode={ZIPCode}&TxnID={TxnID}&Tax={Tax}&Regulatery={Regulatery}&PlanDescription={PlanDescription}&IMEI={IMEI}&Location={Location}&IPAddress={IPAddress}")]
        string SubscriberRecharge(int PaymentID, string NetworkID, string TariffCode, string MobileNo, string TotalAmount, string EmailID, string RechargeAmount, string State, string ZIPCode, string TxnID, string Tax, string Regulatery, string PlanDescription, string IMEI, string Location, string IPAddress);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PortInSimForLycaMobile?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&PhoneToPort={PhoneToPort}&AccountNumber={AccountNumber}&PIN={PIN}&IsWalet={IsWalet}&TariffPlan={TariffPlan}")]

        string PortInSimForLycaMobile(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string PhoneToPort, string AccountNumber, string PIN, int IsWalet, string TariffPlan, string Action);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InitiatePaymentPaypalPortInForLycaMobile?loginID={loginID}&DistributorID={DistributorID}&ChargedAmount={ChargedAmount}&TariffID={TariffID}")]
        string InitiatePaymentPaypalPortInForLycaMobile(int loginID, int DistributorID, string ChargedAmount, string TariffID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PortInSIMForLycaMobileWithPaypal?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&PhoneToPort={PhoneToPort}&AccountNumber={AccountNumber}&PIN={PIN}&PaymentID={PaymentID}&TariffPlan={TariffPlan}")]
        string PortInSIMForLycaMobileWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string PhoneToPort, string AccountNumber, string PIN, int PaymentID, string TariffPlan, string Action);
      

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PortInSIMForH2O?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&City={City}&PhoneToPort={PhoneToPort}&AccountNumber={AccountNumber}&PIN={PIN}&ServiceProvider={ServiceProvider}&State={State}&CustomerName={CustomerName}&Address={Address}&IsWalet={IsWalet}")]

        string PortInSIMForH2O(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, string Phonetoport, string AccountNumber, string PIN, string ServiceProvider, string State, string CustomerName, string Address, int IsWalet);
       
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PortInSIMForH2OWithPaypal?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&City={City}&PhoneToPort={PhoneToPort}&AccountNumber={AccountNumber}&PIN={PIN}&ServiceProvider={ServiceProvider}&State={State}&CustomerName={CustomerName}&Address={Address}&PaymentId={PaymentId}")]

        string PortInSIMForH2OWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, string Phonetoport, string AccountNumber, string PIN, string ServiceProvider, string State, string CustomerName, string Address, string PaymentId);
       

        

        #endregion
        #region "IOS"

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        List<Person> GetPlayers();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/ValidateLoginService?UserName={UserName}&Pwd={Pwd}&DeviceTokenID={DeviceTokenID}&DeviceType={DeviceType}")]
        List<User> ValidateLoginService(string UserName, string Pwd, string DeviceTokenID, string DeviceType);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "/ValidateLoginService_IOS1?UserName={UserName}&Pwd={Pwd}")]
        string ValidateLoginService_IOS1(string UserName, string Pwd);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/ValidateLoginService_IOS2?UserName={UserName}&Pwd={Pwd}")]
        string ValidateLoginService_IOS2(string UserName, string Pwd);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/GetTariffService?LoginID={LoginID}&DistributorID={DistributorID}&ClientTypeID={ClientTypeID}")]
        List<Tariff> GetTariffService(string LoginID, string DistributorID, string ClientTypeID);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/GetSingleDistributorService_IOS?DistributorID={DistributorID}")]
        string GetSingleDistributorService_IOS(string DistributorID);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/CheckBalance_IOS?DistributorID={DistributorID}")]
        string CheckBalance_IOS(string DistributorID);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/CheckSimActivationService_IOS?DistributorID={DistributorID}&ClientTypeID={ClientTypeID}&SimNumber={SimNumber}")]
        string CheckSimActivationService_IOS(string DistributorID, string ClientTypeID, string SimNumber);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/GetTariffDetailService?LoginID={LoginID}&DistributorID={DistributorID}&ClientTypeID={ClientTypeID}&TariffID={TariffID}")]
        List<SingleTariff> GetTariffDetailService(string LoginID, string DistributorID, string ClientTypeID, string TariffID, string Month, string Action);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/ActivateSIMService?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}")]
        List<Common> ActivateSIMService(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/PortInService?ClientTypeID={ClientTypeID}&DistributorID={DistributorID}&TariffID={TariffID}&SimNumber={SimNumber}&TariffAmount={TariffAmount}&LoginID={LoginID}&EmailID={EmailID}&ZipCode={ZipCode}&Pin={Pin}&Account={Account}&PhoneToPort={PhoneToPort}")]
        List<Common> PortInService(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Pin, string Account, string PhoneToPort, string Action);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/SimReplacementSerive?CurrentSimNumber={CurrentSimNumber}&CurrentMobileNumber={CurrentMobileNumber}&NewSimNumber={NewSimNumber}&DistributorID={DistributorID}&LoginID={LoginID}")]
        List<Common> SimReplacementSerive(string CurrentSimNumber, string CurrentMobileNumber, string NewSimNumber, string DistributorID, string LoginID);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/TopupService?DistributorID={DistributorID}&LoginID={LoginID}&Amount={Amount}&Status={Status}&TxnID={TxnID}&TxnDate={TxnDate}")]
        List<Topup> TopupService(string DistributorID, string LoginID, string Amount, string Status, string TxnID, string TxnDate);


        #endregion

       
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string DistributorID { get; set; }

        [DataMember]
        public string ClientType { get; set; }

        [DataMember]
        public string ClientTypeID { get; set; }

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string RoleID { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public string ActiveFromDtTm { get; set; }

        [DataMember]
        public string ActiveToDtTm { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string CurrencyId { get; set; }

        [DataMember]
        public string TotalTopup { get; set; }
        
    }

    [DataContract]
    public class Tariff
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string TariffID { get; set; }

        [DataMember]
        public string TariffCode { get; set; }
    }

    [DataContract]
    public class Balance
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Amount { get; set; }
    }

    [DataContract]
    public class Common
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Message { get; set; }
        
    }

    [DataContract]
    public class Topup
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string TotalTopup { get; set; }

    }

    [DataContract]
    public class InitiatePaymentResponse
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string PaymentID { get; set; }
    }

    [DataContract]
    public class ConfirmPaymentResponse
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

    }


    [DataContract]
    public class SingleTariff
    {
        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string TariffID { get; set; }

        [DataMember]
        public string TariffCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Rental { get; set; }

        [DataMember]
        public string ValidityDays { get; set; }

        [DataMember]
        public string isActive { get; set; }
        
        [DataMember]
        public string IsDefault { get; set; }

        [DataMember]
        public string TariffTypeID { get; set; }

        [DataMember]
        public string TariffType { get; set; }

        [DataMember]
        public string LycaAmount { get; set; }

        [DataMember]
        public string Months { get; set; }

    }

}