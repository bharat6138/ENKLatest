using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class cReport : DataBase
    {
        public int ClientID;
        public int ClientTypeID;
        public int LoginID;
        public int PurchaseID;
        public string Action = string.Empty;
        public DateTime FromDate;
        public DateTime ToDate;
        public string SearchText;
        // add by akash 
        public int NetworkID;

        public string RechargeVia;

        public string MobileNo;

        public string TxnID;
        public int checkMainDistributor = 0;

        public DataSet GetReportInventoryPurchase()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportInventoryPurchase"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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


        public DataSet GETLEVELDistributor(Int32 distributorID, int treeLevel)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGETLEVELDistributor"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@distributorID", SqlDbType.BigInt).Value = distributorID;
                    objCmd.Parameters.Add("@treeLevel", SqlDbType.Int).Value = treeLevel;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetPurchaseReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchPurchaseReport"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    objCmd.Parameters.Add("@PurchaseID", SqlDbType.BigInt).Value = PurchaseID;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataSet GetReportInventoryStatus()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportInventoryStatus"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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

        public DataSet GetReportSalesReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportSales"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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

        public DataSet GetReportActivationSIM()
        {
            try
            {
                // change by akash 
                //using (SqlCommand objCmd = new SqlCommand("pFetchReportActivationSIM"))
                using (SqlCommand objCmd = new SqlCommand("[pFetchReportActivationSIM_LevelWise]"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    //objCmd.Parameters.Add("@checkMainDistributor", SqlDbType.Int).Value = checkMainDistributor;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.Int).Value = NetworkID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetReportRechargeFilterwise()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportRechargeFilterwise"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = MobileNo;
                    objCmd.Parameters.Add("@TxnID", SqlDbType.VarChar).Value = TxnID;

                    objCmd.Parameters.Add("@RechargeVia", SqlDbType.VarChar).Value = RechargeVia;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataSet GetRechargeSIMReport()
        //{
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("pFetchReportRechargeSIM"))
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
        //            objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
        //            objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

        //            objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
        //            objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
        //            objCmd.Parameters.Add("@RechargeVia", SqlDbType.VarChar).Value = RechargeVia;


        //            return ReturnDataSet(objCmd);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public DataSet GetRechargeSIMReport()
        {
            try
            {
                // change by akash (08-01-2019)
                //using (SqlCommand objCmd = new SqlCommand("pFetchReportRechargeSIM"))
                using (SqlCommand objCmd = new SqlCommand("pFetchReportRechargeSIM_LevelWise"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    //objCmd.Parameters.Add("@RechargeVia", SqlDbType.VarChar).Value = RechargeVia;
                    // add by akash 
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.Int).Value = NetworkID;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetReportAcivationFail()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportAcivationFail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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



        public DataSet GetRechargeFailReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportRechargeFail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    objCmd.Parameters.Add("@RechargeVia", SqlDbType.VarChar).Value = RechargeVia;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int UpdateStatusRecharge(int RechargeID, int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateStatusRecharge";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@RechargeID", SqlDbType.BigInt).Value = RechargeID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;


                a = RunExecuteNoneQuery(cmd);
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateStatusActivation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ActivationID", SqlDbType.BigInt).Value = ActivationID;
                cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;


                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }



        public DataSet GetRequesrRechargeSIMReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetRequestRechargeReport"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    objCmd.Parameters.Add("@RechargeVia", SqlDbType.VarChar).Value = RechargeVia;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetRegulatery()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetRegulatery"))
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

        public DataSet GetReportSIMHistory()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportSIMHistory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

                    objCmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = SearchText;



                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSaleReportForActivationAndPortIn()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSaleReportForActivationAndPortIn"))
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

        public DataSet GetSearch(string SerachFor, string SearchText, int ClientID, int ClientTypeID, string Email, string DateType, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetSearchData"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@SerachFor", SqlDbType.VarChar).Value = SerachFor;
                    objCmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = SearchText;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Email;
                    objCmd.Parameters.Add("@DateType", SqlDbType.VarChar).Value = DateType;
                    if (DateType == "")
                    {
                        objCmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = DBNull.Value;
                        objCmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        objCmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
                        objCmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;

                    }
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetImportFile(string xmlSTR)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetImportFile"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@xml", SqlDbType.VarChar).Value = xmlSTR;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetImportFileDetails(DataTable dt, string Action)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetImportFileDetails"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetReportDeduct()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchDeductReport"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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
        public DataSet GetReportTopup()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportTopupLedger"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

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
        public DataSet GetReportTopupLedger()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spAccountLedger"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = ClientID;
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
        public DataSet GetLedgerReport()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchLedgerReport"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = ClientID;
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

        public DataSet GetReportActivationLedger(Int32 CurrentLogin)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchReportActivationLedgerLevelWise"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    objCmd.Parameters.Add("@CurrentLogin", SqlDbType.Int).Value = CurrentLogin;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCommissionDetail(long DistributorID, int Month, int Year, string MonthYear)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetCommissionDetailsDEV1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@month", SqlDbType.Int).Value = Month;
                    objCmd.Parameters.Add("@year", SqlDbType.Int).Value = Year;
                    objCmd.Parameters.Add("@CommissionProcessMonth", SqlDbType.VarChar).Value = MonthYear;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public DataSet GetRechargeActivationCancelReport(DateTime CancelDate)
        //{
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("pGetCancelRechargeActivationDetail"))
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            // objCmd.Parameters.Add("@CancelType", SqlDbType.VarChar).Value = DistributorID;
        //            objCmd.Parameters.Add("@CancelDate", SqlDbType.Date).Value = CancelDate;


        //            return ReturnDataSet(objCmd);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public DataSet GetRechargeActivationCancelReport(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetCancelRechargeActivationDetailDev1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    // objCmd.Parameters.Add("@CancelType", SqlDbType.VarChar).Value = DistributorID;
                    objCmd.Parameters.Add("@CancelDateFrom", SqlDbType.Date).Value = FromDate;
                    objCmd.Parameters.Add("@CancelDateTo", SqlDbType.Date).Value = ToDate;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetImageUrl(int ImgFlag)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetImage"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ImgFlag", SqlDbType.Int).Value = ImgFlag;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet DeleteImage(int ImageID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spDeleteImage"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ImageID", SqlDbType.Int).Value = ImageID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet pUpdateImagePosition(int ImageID, int Position)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spUpdateImagePosition"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ImageID", SqlDbType.Int).Value = ImageID;
                    objCmd.Parameters.Add("@Position", SqlDbType.Int).Value = Position;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet getImage1FromDB(int ImgFlag)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetImage1FromDB"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ImgFlag", SqlDbType.Int).Value = ImgFlag;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTransactionReport(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetTransactionReport"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;

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
