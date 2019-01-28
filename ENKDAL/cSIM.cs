using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class cSIM:DataBase
    {
        public int DistributorID =0;
        public int UserID = 0;
        public int VendorID = 0;
        public DateTime PurchaseDate;
        public String PurchaseNo = string.Empty;
        public String InvoiceNo = string.Empty;
        public int BranchID = 0;
        public DataTable MobileDT;
        public DataTable SIMDt;
        public string SIMNo = string.Empty;
        public string MSISDNNo = string.Empty;
        public string TransferType = string.Empty;
        public int ClientID = 0;
        public int NewClientID = 0;
        public long MSISDN_SIM_ID = 0;
        public int ClientTypeID = 0;
        public int TariffID = 0;
        public string Action;
        public string Notes="";
        public DateTime FromDate;
        public DateTime ToDate;


        public int SaveInventory()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSavePurchaseInventory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@LogInID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@DistributorID", SqlDbType.Int).Value = DistributorID;
                    objCmd.Parameters.AddWithValue("@VendorID", SqlDbType.Int).Value = VendorID;
                    objCmd.Parameters.AddWithValue("@PurchaseDate", SqlDbType.DateTime).Value = PurchaseDate;
                    objCmd.Parameters.AddWithValue("@PurchaseNo", SqlDbType.VarChar).Value = PurchaseNo;
                    objCmd.Parameters.AddWithValue("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;
                    objCmd.Parameters.AddWithValue("@BranchId", SqlDbType.Int).Value = BranchID;
                    objCmd.Parameters.AddWithValue("@MobileDetails", SqlDbType.Structured).Value = MobileDT.Rows[0]["MobileNo"] == "" ? null : MobileDT;
                    objCmd.Parameters.AddWithValue("@SIMDetails", SqlDbType.Structured).Value = SIMDt.Rows[0]["SimNo"] == "" ? null : SIMDt;
                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SaveInventoryTransfer(string checkInventoryWAY)
        {
            try
            {
                //using (SqlCommand objCmd = new SqlCommand("pInventoryTransfer"))
                using (SqlCommand objCmd = new SqlCommand("pInventoryTransferOneANDTwoWAY"))
                
                {
                   // @loginID bigint, @ClientID Bigint,@NewClientID bigint=null,@TransferType varchar(10), @MSISDN [dbo].[dtTransferMSISDN] readonly,@SIM [dbo].[dtTransferSIM] readonly
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@loginID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.AddWithValue("@NewClientID", SqlDbType.Int).Value = NewClientID;
                    objCmd.Parameters.AddWithValue("@TransferType", SqlDbType.VarChar).Value = TransferType;
                    objCmd.Parameters.AddWithValue("@MSISDN", SqlDbType.Structured).Value = MobileDT.Rows[0]["MSISDN_SIM_ID"] == "" ? null : MobileDT;
                    objCmd.Parameters.AddWithValue("@SIM", SqlDbType.Structured).Value = SIMDt.Rows[0]["SIMID"] == "" ? null : SIMDt;

                    objCmd.Parameters.AddWithValue("@InventoryWAY", SqlDbType.VarChar).Value = checkInventoryWAY;
                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetStockTransferReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetStockLedger"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet CheckAlreadySuccessfullPortIn(string SimNumber, string PhoneNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pCheckAlreadySuccessfullPortIn"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@SimNumber", SqlDbType.VarChar).Value = SimNumber;
                    objCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = PhoneNumber;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTariffForActivation_ForSubscriber(string simnumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetTariffForActivation_forSubscriber"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@SIMNUMBER", SqlDbType.VarChar).Value = simnumber;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int SaveDistributorNotes()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSaveDistributorNotes"))
                {
                    // @loginID bigint, @ClientID Bigint,@NewClientID bigint=null,@TransferType varchar(10), @MSISDN [dbo].[dtTransferMSISDN] readonly,@SIM [dbo].[dtTransferSIM] readonly
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@LoginID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@DistributorID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.AddWithValue("@Notes", SqlDbType.VarChar).Value = Notes;
               
                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetDistributorNotes()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetDistributorNotes"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataSet ValidatePreloadedSim(string SimNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("ValidatePreloadedSim"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@SimNumber", SqlDbType.VarChar).Value = SimNumber;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetInventory1(string SimNumber, string FromSimNumber, string ToSimNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventory1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    objCmd.Parameters.Add("@SimNumber", SqlDbType.VarChar).Value = SimNumber;
                    objCmd.Parameters.Add("@FromSimNumber", SqlDbType.VarChar).Value = FromSimNumber;
                    objCmd.Parameters.Add("@ToSimNumber", SqlDbType.VarChar).Value = ToSimNumber;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetInventory()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetInventoryForAccept()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryForAccept"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@LoginClientID", SqlDbType.Int).Value = DistributorID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetInventoryForSIMReplacement()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryForSIMReplacement"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@MSISDNNo", SqlDbType.VarChar).Value = MSISDNNo;
                    objCmd.Parameters.Add("@SIMNo", SqlDbType.VarChar).Value = SIMNo;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SIMReplacement()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSIMReplacement"))
                {
                    // @loginID bigint, @ClientID Bigint,@NewClientID bigint=null,@TransferType varchar(10), @MSISDN [dbo].[dtTransferMSISDN] readonly,@SIM [dbo].[dtTransferSIM] readonly
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@loginID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.AddWithValue("@MSISDN_SIM_ID", SqlDbType.BigInt).Value = MSISDN_SIM_ID;
                    objCmd.Parameters.AddWithValue("@SIMNo", SqlDbType.VarChar).Value = SIMNo;
                    
                    return RunExecuteNoneQuery(objCmd);
                    
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet CheckDuplicateSIMActivation(string SimNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pCheckDuplicateSIMActivation"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SimNumber;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet CheckSimActivation(int DistributorID, int ClientTypeID, string SimNumber, string Action)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetActivationSIMCheck"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@ClientID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@SIMNumber", SqlDbType.VarChar).Value = SimNumber;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet CheckSimPortIN(int DistributorID, int ClientTypeID, string SimNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetPortINSIMCheck"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@ClientID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@SIMNumber", SqlDbType.VarChar).Value = SimNumber;
                    


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable PreloadedSIMCheck(string SimNo)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pPreloadedSIMCheck"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@SimNo", SqlDbType.Int).Value = SimNo;
                    return ReturnDataSet(objCmd).Tables[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTransactionID()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchTransactionID"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    return ReturnDataSet(objCmd).Tables[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveSimTariffMapping(int Months)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSimTariffMapping"))
                {
                    // @loginID bigint, @ClientID Bigint,@NewClientID bigint=null,@TransferType varchar(10), @MSISDN [dbo].[dtTransferMSISDN] readonly,@SIM [dbo].[dtTransferSIM] readonly
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@loginID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.AddWithValue("@TariffID", SqlDbType.Int).Value = TariffID;
                    objCmd.Parameters.AddWithValue("@SIM", SqlDbType.Structured).Value = SIMDt.Rows[0]["SIMID"] == "" ? null : SIMDt;
                    objCmd.Parameters.AddWithValue("@Months", SqlDbType.Int).Value = Months;
                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Ankit Singh
        public void SaveModifiedPortinDetails(string Request, string APIResponse, DateTime RequestedTime, int Requestedby, string MSISDN)
        {
            try
            {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SaveModifiedPortinDetails";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Request", SqlDbType.VarChar).Value = Request;
                    cmd.Parameters.Add("@Response", SqlDbType.VarChar).Value = APIResponse;
                    cmd.Parameters.Add("@RequestedTime", SqlDbType.DateTime).Value = RequestedTime;
                    cmd.Parameters.Add("@Requestedby", SqlDbType.Int).Value = Requestedby;
                    cmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    RunExecuteNoneQuery(cmd);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetInventoryBulkTransfer()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryBulkTransfer"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@SIMDt", SqlDbType.Structured).Value = SIMDt.Rows[0]["SIMNO"] == "" ? null : SIMDt;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetInventoryBulkTransfer1()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryBulkTransfer1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@SIMDt", SqlDbType.Structured).Value = SIMDt.Rows[0]["SIMNO"] == "" ? null : SIMDt;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetInventoryForAcceptOnDasboard()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryForAcceptOnDasboard"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    //objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@LoginClientID", SqlDbType.Int).Value = DistributorID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetMsimDetails(string MSIM, string MNPNo)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("GetMsimDetails"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@MSIM", SqlDbType.VarChar).Value = MSIM;
                    objCmd.Parameters.Add("@MNP", SqlDbType.VarChar).Value = MNPNo;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerifySimNumber(string OldSimNumber, string NewSimNumber)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pVerifySimNumber"))
                {

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@OldSimNumber", SqlDbType.VarChar).Value = OldSimNumber;
                    objCmd.Parameters.AddWithValue("@NewSimNumber", SqlDbType.VarChar).Value = NewSimNumber;

                    return Convert.ToInt32(RunCommand(objCmd));

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateSwapSim(string MSISDN, string OldSimNumber, string NewSimNumber, long Createdby)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pUpdateSwapSim"))
                {

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    objCmd.Parameters.AddWithValue("@OldSimNumber", SqlDbType.VarChar).Value = OldSimNumber;
                    objCmd.Parameters.AddWithValue("@NewSimNumber", SqlDbType.VarChar).Value = NewSimNumber;
                    objCmd.Parameters.AddWithValue("@Createdby", SqlDbType.BigInt).Value = Createdby;

                    return RunExecuteNoneQuery(objCmd);

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet FetchInventory(int ClientID, string Action, string SimNumber, string FromSim, string ToSim, int NetworkID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchInventory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    objCmd.Parameters.Add("@SimNumber", SqlDbType.VarChar).Value = SimNumber;
                    objCmd.Parameters.Add("@FromSimNumber", SqlDbType.VarChar).Value = FromSim;
                    objCmd.Parameters.Add("@ToSimNumber", SqlDbType.VarChar).Value = ToSim;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.Int).Value = NetworkID;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SwapSimRequest(string MSISDN, string OldSimNumber, string NewSimNumber, int Createdby, string Request, string Response, string Status)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSwapsimLogDetail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    objCmd.Parameters.Add("@OldSimNumber", SqlDbType.VarChar).Value = OldSimNumber;
                    objCmd.Parameters.Add("@NewSimNumber", SqlDbType.VarChar).Value = NewSimNumber;
                    objCmd.Parameters.Add("@Createdby", SqlDbType.BigInt).Value = Convert.ToUInt64(Createdby);
                    objCmd.Parameters.Add("@Request", SqlDbType.VarChar).Value = Request;
                    objCmd.Parameters.Add("@Response", SqlDbType.VarChar).Value = Response;
                    objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet RechargeActivationCancelDetail(string MSISDN,string SerialNumber, string Action)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetActivationRechargeMSISDNdetail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    objCmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = SerialNumber;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    
                    return ReturnDataSet(objCmd);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int SaveRechargeActivationCancelDetails(int ID, string MSISDN, int TariffID, Decimal Rental, Decimal RegulatoryFee, string CancelType, Int16 CancelMonth)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pInsertRechargeActivationCancellation"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@RechargeActivationID", SqlDbType.Int).Value = ID;
                    objCmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.VarChar).Value = TariffID;
                    objCmd.Parameters.Add("@Rental", SqlDbType.Decimal).Value = Rental;
                    objCmd.Parameters.Add("@RegulatoryFee", SqlDbType.Decimal).Value = RegulatoryFee;
                    objCmd.Parameters.Add("@CancelType", SqlDbType.VarChar).Value = CancelType;
                    objCmd.Parameters.Add("@CancelMonth", SqlDbType.TinyInt).Value = CancelMonth;
                    return RunExecuteNoneQuery(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add by akash starts
        public DataSet GetCountryByAreaCode(int _AreaCode)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetCityList"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@AreaCode", SqlDbType.Int).Value = _AreaCode;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // add by akash ends


    }
}
