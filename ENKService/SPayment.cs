using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;


namespace ENKService
{
    public class SPayment
    {
        Decimal _Regulatery;
        [DataMember]
        public Decimal Regulatery
        {
            get { return _Regulatery; }
            set { _Regulatery = value; }
        }

        int _PaymentId;
        [DataMember]
        public int PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }

        int _Month;
        [DataMember]
        public int Month
        {
            get { return _Month; }
            set { _Month = value; }
        }


        int _PaymentFrom;
        [DataMember]
        public int PaymentFrom
        {
            get { return _PaymentFrom; }
            set { _PaymentFrom = value; }
        }

        int _PayeeID;
        [DataMember]
        public int PayeeID
        {
            get { return _PayeeID; }
            set { _PayeeID = value; }
        }

        int _PaymentType;
        [DataMember]
        public int PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }

        string _OrderId;
        [DataMember]
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        string _PaymentMode;
        [DataMember]
        public string PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }
        string _TransactionId;
        [DataMember]
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        
        decimal _ChargedAmount;
        [DataMember]
        public decimal ChargedAmount
        {
            get { return _ChargedAmount; }
            set { _ChargedAmount = value; }
        }
        string _TransactionStatus;
        [DataMember]
        public string TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }

        string _Mid;
        [DataMember]
        public string Mid
        {
            get { return _Mid; }
            set { _Mid = value; }
        }

        string _TxnId;
        [DataMember]
        public string TxnId
        {
            get { return _TxnId; }
            set { _TxnId = value; }
        }

        string _TxnAmount;
        [DataMember]
        public string TxnAmount
        {
            get { return _TxnAmount; }
            set { _TxnAmount = value; }
        }

        int _Currency;
        [DataMember]
        public int Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        string _RespCode;
        [DataMember]
        public string RespCode
        {
            get { return _RespCode; }
            set { _RespCode = value; }
        }

        string _RespMsg;
        [DataMember]
        public string RespMsg
        {
            get { return _RespMsg; }
            set { _RespMsg = value; }
        }

        string _TxnDate;
        [DataMember]
        public string TxnDate
        {
            get { return _TxnDate; }
            set { _TxnDate = value; }
        }

        string _mPaymentMode;
        [DataMember]
        public string mPaymentMode
        {
            get { return _mPaymentMode; }
            set { _mPaymentMode = value; }
        }

        string _CheckSumm;
        [DataMember]
        public string CheckSumm
        {
            get { return _CheckSumm; }
            set { _CheckSumm = value; }
        }

        string _Remarks;
        [DataMember]
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        int _ActivationType;
        [DataMember]
        public int ActivationType
        {
            get { return _ActivationType; }
            set { _ActivationType = value; }
        }
        int _ActivationStatus;
        [DataMember]
        public int ActivationStatus
        {
            get { return _ActivationStatus; }
            set { _ActivationStatus = value; }
        }

        int _ActivationVia;
        [DataMember]
        public int ActivationVia
        {
            get { return _ActivationVia; }
            set { _ActivationVia = value; }
        }

        string _ActivationResp;
        [DataMember]
        public string ActivationResp
        {
            get { return _ActivationResp; }
            set { _ActivationResp = value; }
        }

        string _ALLOCATED_MSISDN;
        [DataMember]
        public string ALLOCATED_MSISDN
        {
            get { return _ALLOCATED_MSISDN; }
            set { _ALLOCATED_MSISDN = value; }
        }

        int _TariffID;
        [DataMember]
        public int TariffID
        {
            get { return _TariffID; }
            set { _TariffID = value; }
        }

        string _ActivationRequest;
        [DataMember]
        public string ActivationRequest
        {
            get { return _ActivationRequest; }
            set { _ActivationRequest = value; }
        }

        string _EmailID;
        [DataMember]
        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }

        string _Address;
        [DataMember]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        string _Mobile;
        [DataMember]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        string _CusName;
        [DataMember]
        public string CusName
        {
            get { return _CusName; }
            set { _CusName = value; }
        }

        string _ReceiptId;
        [DataMember]
        public string ReceiptId
        {
            get { return _ReceiptId; }
            set { _ReceiptId = value; }
        }

        string _PayerId;
        [DataMember]
        public string PayerId    
        {
            get { return _PayerId; }
            set { _PayerId = value; }
        }

        int _TransactionStatusId;
        [DataMember]
        public int TransactionStatusId
        {
            get { return _TransactionStatusId; }
            set { _TransactionStatusId = value; }
        }
    }
}