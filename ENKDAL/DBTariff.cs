using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ENKDAL
{
    public class DBTariff : DataBase
    {
        public int tarifID = 0;
        public string tariffCode = "";
        public string description = "";
        public int validDays = 0;
        public double rental = 0.0;
        public int frequency = 0;
        public int TariffType = 0;
        public bool isActive = true;
        public string tarifName = "";
        public double discount_on_Activation_PortIn = 0.0;
        public double discount_on_Recharge = 0.0;
        public int sellerID = 0;
        public string Action = "";
        public int loginID = 0;
        public int NetworkID = 0;

        public double commission = 0;  //Added By Sarala
        public string groupName = "";  //Added By Sarala
        public int tariffGroupid = 0;  //Added By Sarala
        public DataTable dtSpiff;  //Added By Sarala
                                   // add by akash ends

        public double H2oGeneralDiscount = 0;  // Added by Akash

        public DataSet GetProductRecharge(int NetworkID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchProductRecharge"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckDuplicateRecharge(string MSISDN, int TariffID, Int32 DistributorId)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("PCheckDuplicateRecharge"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@MSISDN", SqlDbType.VarChar).Value = MSISDN;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.Int).Value = TariffID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorId;
                    return ReturnDataSet(objCmd).Tables[0];
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

        public DataSet GetTariff(int LoginID, int DistributorID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetTariff"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTariffSpiffDetails(string mode, int Id)    // Added By Sarala
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pFetchTariffSpiffDetail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Mode", SqlDbType.VarChar).Value = mode;
                    objCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTariffGroup(int LoginID, int DistributorID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("Get_Tariff_Group"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.Int).Value = DistributorID;
                    objCmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSingleTariff(int TariffID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleTariff"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTariffGroup(int TariffID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetTariffGroup"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveTariff(int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertTariff";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TariffCode", SqlDbType.VarChar).Value = tariffCode;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@Rental", SqlDbType.Decimal).Value = rental;
                cmd.Parameters.Add("@ValidityDays", SqlDbType.BigInt).Value = validDays;
                cmd.Parameters.Add("@TariffType", SqlDbType.BigInt).Value = TariffType;
                cmd.Parameters.Add("@isActive", SqlDbType.VarChar).Value = isActive;
                cmd.Parameters.Add("@NetworkID", SqlDbType.VarChar).Value = NetworkID;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int SaveTariffGroupSpiffMapping(int LoginID, int TariggGroupId, string Action, decimal RechargeCommission, decimal H2ORechargeDiscount, decimal Comission1, decimal H2OGeneralDiscount1)   // Added By Sarala
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pCreateTariffSpiffMapping";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TariffGrpID", SqlDbType.BigInt).Value = TariggGroupId;
                cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = Comission1;
                cmd.Parameters.Add("@GroupName", SqlDbType.VarChar).Value = groupName;
                cmd.Parameters.Add("@dtSpiff", SqlDbType.Structured).Value = dtSpiff;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = LoginID;
                cmd.Parameters.Add("@RechargeCommission", SqlDbType.Decimal).Value = RechargeCommission;
                // add by akash starts
                cmd.Parameters.Add("@H2oGeneralDiscount", SqlDbType.Decimal).Value = H2OGeneralDiscount1;
                cmd.Parameters.Add("@H2ORechargeDiscount", SqlDbType.Decimal).Value = H2ORechargeDiscount;
                // add by akash ends


                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public int SaveNewTariff(int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                int _IsActive = 0;
                if (isActive == true)
                {
                    _IsActive = 1;

                }
                else
                {
                    _IsActive = 0;

                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SaveTariffGroup";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TariffID", SqlDbType.Int).Value = tarifID;
                cmd.Parameters.Add("@TariffName", SqlDbType.VarChar).Value = tarifName;
                cmd.Parameters.Add("@DiscoutActivation", SqlDbType.Decimal).Value = discount_on_Activation_PortIn;
                cmd.Parameters.Add("@DiscountOnRecharge", SqlDbType.Decimal).Value = discount_on_Recharge;
                cmd.Parameters.Add("@SellerID", SqlDbType.Int).Value = sellerID;
                cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = _IsActive;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = "Add";


                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public int UpdateTariffGroup(int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                int _IsActive = 0;
                if (isActive == true)
                {
                    _IsActive = 1;

                }
                else
                {
                    _IsActive = 0;

                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SaveTariffGroup";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TariffID", SqlDbType.Int).Value = tarifID;
                cmd.Parameters.Add("@TariffName", SqlDbType.VarChar).Value = tarifName;
                cmd.Parameters.Add("@DiscoutActivation", SqlDbType.Decimal).Value = discount_on_Activation_PortIn;
                cmd.Parameters.Add("@DiscountOnRecharge", SqlDbType.Decimal).Value = discount_on_Recharge;
                cmd.Parameters.Add("@SellerID", SqlDbType.Int).Value = sellerID;
                cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = _IsActive;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = "UPDATE";

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateTariff(int LoginID, int DistributorID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateTariff";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = tarifID;
                cmd.Parameters.Add("@TariffCode", SqlDbType.VarChar).Value = tariffCode;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@Rental", SqlDbType.Decimal).Value = rental;
                cmd.Parameters.Add("@ValidityDays", SqlDbType.BigInt).Value = validDays;
                cmd.Parameters.Add("@TariffType", SqlDbType.BigInt).Value = TariffType;
                cmd.Parameters.Add("@isActive", SqlDbType.VarChar).Value = isActive;
                cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;


                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        //Ankit Singh
        public DataSet GetAddOnANDInternationCreadit()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("getADDonAndInternationalCredits"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    //objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = LoginID;
                    //objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    //objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Ankit Singh
        public DataSet GetDiscountAndRental(int DistributorID, int tariffID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetDiscountAndRental"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    // objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = LoginID;
                    //objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@TeriffID", SqlDbType.BigInt).Value = tariffID;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataSet GetTariffForActivation(int LoginID, int DistributorID, int ClientTypeID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetTariffForActivation"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSingleTariffDetailForActivation(int LoginID, int DistributorID, int ClientTypeID, int TariffID, int Month, string Action)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleTariffDetailForActivation"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@TariffID", SqlDbType.BigInt).Value = TariffID;
                    objCmd.Parameters.Add("@Month", SqlDbType.TinyInt).Value = Month;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet CheckTariffGroupExist(string GroupName)   // Added By Sarala
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spCheckTariffGroupExist"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@GroupName", SqlDbType.VarChar).Value = GroupName;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Topup(int DistributorID, long LoginID, decimal Amount, string Status, string TxnID, string TxnDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pInsertTopupViaApp"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@distributorID", SqlDbType.Int).Value = DistributorID;
                    objCmd.Parameters.Add("@ChargedAmount", SqlDbType.Decimal).Value = Amount;
                    objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;
                    objCmd.Parameters.Add("@TxnID", SqlDbType.VarChar).Value = TxnID;
                    objCmd.Parameters.Add("@TxnDate", SqlDbType.VarChar).Value = TxnDate;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetState()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetstate"))
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

        public DataSet GetPurchaseCode()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetPurchaseCode"))
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

    }
}
