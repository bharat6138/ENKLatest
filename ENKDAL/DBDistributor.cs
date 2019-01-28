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
    public class DBDistributor : DataBase
    {
        public Int32 LoginID = 0;
        public Int32 DistributorID = 0;
        public string DistributorName = "";
        public Int32 DistributorCode = 0;
        public Int32 CompanyType = 0;
        public string CompanyTypeName = "";
        public Int32 Parent = 0;
        public Int32 RelationManager = 0;
        public string VatNo = "";
        public double VatPer = 0;
        public string ServiceTAxNo = "";
        public double ServiceTAxPer = 0;
        public string ContactPerson = "";
        public string ContactNo = "";
        public string WebSiteName = "";
        public string EmailID = "";

        public string Address = "";
        public string City = "";
        public string State = "";
        public string Zip = "";
        public int CountryId = 0;

        public bool IsServiceTaxExmpted = true;

        //ANKIT SINGH
        //double BalanceAmount = 0.0;
        public decimal BalanceAmount = Convert.ToDecimal(0.00);
        //public double BalanceAmount = 0.0;
        //

        public string Password = "";
        public string EIN = "";
        public string SSN = "";
        public string PanNumber = "";
        public bool IsActive = true;
        public int TariffGroupID = 0;

        public string document = "";
        public string Certificate = "";


        public int StoredAPIRequestBeforeCall(string Title, string Request, Int32 DistributorId, string TransactionID, string Msisdn, string SIMNumber)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PStoredAPIRequestBeforeCall";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title;
                cmd.Parameters.Add("@Request", SqlDbType.VarChar).Value = Request;
                cmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorId;
                cmd.Parameters.Add("@TransactionID", SqlDbType.VarChar).Value = TransactionID;
                cmd.Parameters.Add("@Msisdn", SqlDbType.VarChar).Value = Msisdn;
                cmd.Parameters.Add("@SIMNumber", SqlDbType.VarChar).Value = SIMNumber;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public DataSet DeductDistributorTopUpAmount(int Distributorid, decimal Amount, string Remarks)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pDeductDistributorTopUpAmount"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;
                    objCmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
                    objCmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet CHECKDistributor(string UserID)
        {
            DataSet ds;
            try
            {
                using (SqlCommand cmd = new SqlCommand("CHECKUSERvalidation"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;

                    return ReturnDataSet(cmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        


        public DataSet SaveDistributor(int UserId, DataTable dt, DataTable dtRecharge, string UserName, int ChkSellr, int Chktariffgroup)
        {
            DataSet ds;
            try
            {
                using (SqlCommand cmd = new SqlCommand("spCreateistributor"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ACode", SqlDbType.BigInt).Value = DistributorCode;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = DistributorName;
                    cmd.Parameters.Add("@ClientType", SqlDbType.BigInt).Value = CompanyType;

                    cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = ContactPerson;
                    cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = ContactNo;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;
                    cmd.Parameters.Add("@Website", SqlDbType.VarChar).Value = WebSiteName;
                    cmd.Parameters.Add("@ParentDistributorID", SqlDbType.BigInt).Value = Parent;


                    cmd.Parameters.Add("@AddressLine", SqlDbType.VarChar).Value = Address;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar).Value = Zip;
                    cmd.Parameters.Add("@CountryId", SqlDbType.BigInt).Value = CountryId;
                    cmd.Parameters.Add("@TariffGroupId", SqlDbType.TinyInt).Value = TariffGroupID;


                    cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = UserId;
                    cmd.Parameters.Add("@CreatedDtTm", SqlDbType.DateTime).Value = DateTime.UtcNow.AddMinutes(330);

                    cmd.Parameters.Add("@AccountBalance", SqlDbType.VarChar).Value = BalanceAmount;
                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;

                    cmd.Parameters.Add("@Einnumber", SqlDbType.VarChar).Value = EIN;
                    cmd.Parameters.Add("@SSnumber", SqlDbType.VarChar).Value = SSN;
                    cmd.Parameters.Add("@PanNumber", SqlDbType.VarChar).Value = PanNumber;

                    cmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                    cmd.Parameters.Add("@dtNetwork_Recharge", SqlDbType.Structured).Value = dtRecharge;

                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserName;


                    cmd.Parameters.Add("@ChkSellr", SqlDbType.TinyInt).Value = ChkSellr;
                    cmd.Parameters.Add("@Chktariffgroup", SqlDbType.TinyInt).Value = Chktariffgroup;


                    cmd.Parameters.Add("@TaxDocument", SqlDbType.VarChar).Value = document;
                    cmd.Parameters.Add("@Certificate", SqlDbType.VarChar).Value = Certificate;

                    return ReturnDataSet(cmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateDistributor(int UserId, DataTable dt, DataTable dtRecharge, string Username, string passw, int ChkSellr, int Chktariffgroup)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateDistributor1";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                cmd.Parameters.Add("@ACode", SqlDbType.BigInt).Value = DistributorCode;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = DistributorName;
                cmd.Parameters.Add("@ClientType", SqlDbType.BigInt).Value = CompanyType;
                cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = ContactPerson;
                cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = ContactNo;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = EmailID;
                cmd.Parameters.Add("@Website", SqlDbType.VarChar).Value = WebSiteName;
                cmd.Parameters.Add("@ParentDistributorID", SqlDbType.BigInt).Value = Parent;
                cmd.Parameters.Add("@TariffGroupId", SqlDbType.TinyInt).Value = TariffGroupID;


                cmd.Parameters.Add("@AddressLine", SqlDbType.VarChar).Value = Address;
                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City;
                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
                cmd.Parameters.Add("@Zip", SqlDbType.VarChar).Value = Zip;
                cmd.Parameters.Add("@CountryId", SqlDbType.BigInt).Value = CountryId;

                cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = UserId;
                cmd.Parameters.Add("@ModifiedDtTm", SqlDbType.DateTime).Value = DateTime.UtcNow.AddMinutes(330);
                cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;

                cmd.Parameters.Add("@Einnumber", SqlDbType.VarChar).Value = EIN;
                cmd.Parameters.Add("@SSnumber", SqlDbType.VarChar).Value = SSN;
                cmd.Parameters.Add("@PanNumber", SqlDbType.VarChar).Value = PanNumber;
                cmd.Parameters.Add("@AccountBalance", SqlDbType.Decimal).Value = BalanceAmount;

                cmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                cmd.Parameters.Add("@dtNetwork_Recharge", SqlDbType.Structured).Value = dtRecharge;


                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = Username;
                cmd.Parameters.Add("@passw", SqlDbType.VarChar).Value = passw;


                cmd.Parameters.Add("@ChkSellr", SqlDbType.TinyInt).Value = ChkSellr;
                cmd.Parameters.Add("@Chktariffgroup", SqlDbType.TinyInt).Value = Chktariffgroup;


                cmd.Parameters.Add("@TaxDocument", SqlDbType.VarChar).Value = document;
                cmd.Parameters.Add("@Certificate", SqlDbType.VarChar).Value = Certificate;

                a = RunExecuteNoneQuery(cmd);
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
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "UpdateDistributorRIGHTSHierarchy";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
        //        cmd.Parameters.Add("@ChkSellr", SqlDbType.TinyInt).Value = ChkSellr;
        //        cmd.Parameters.Add("@Chktariffgroup", SqlDbType.TinyInt).Value = Chktariffgroup;

        //        a = RunExecuteNoneQuery(cmd);
        //        return a;
        //    }
        //    catch (Exception ex)
        //    {
        //        return a;
        //    }
        //}

        public DataSet GetDistributor1(int UserId, int Distrib, string TaxDocument, string ResellerCertificate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetDistributor1"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = UserId;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distrib;
                    objCmd.Parameters.Add("@TaxDocument", SqlDbType.VarChar).Value = TaxDocument;
                    objCmd.Parameters.Add("@ResellerCertificate", SqlDbType.VarChar).Value = ResellerCertificate;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDistributor(int UserId, int Distrib)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetDistributor"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = UserId;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distrib;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataSet GetSingleDistributor(int Distributorid)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleDistributor"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSingleDistributorTariff(int Distributorid)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleDistributorTariff"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDistributorDDL(int UserId, int Distrib)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetDistributorForDDl"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = UserId;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distrib;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTestData()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetResponse"))
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

        public DataSet DeactivateDistirbutor(int DistributorId, int LoginId, string Condition)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spDeactivateDistributor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorId;
                cmd.Parameters.Add("@LoginId", SqlDbType.BigInt).Value = LoginId;
                cmd.Parameters.Add("@condition", SqlDbType.VarChar).Value = Condition;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetDistributor(int Distributorid)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pGetDistributor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Distributorid", SqlDbType.Int).Value = Distributorid;

                ds = ReturnDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetDistributorofMappingwithPlan(int TariffID)
        {
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pGetDistributorofMappingwithPlan";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TariffID", SqlDbType.Int).Value = TariffID;

                ds = ReturnDataSet(cmd);
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pSaveDistributorofMappingwithPlan";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Rental", SqlDbType.Decimal).Value = Rental;
                cmd.Parameters.Add("@TariffID", SqlDbType.Int).Value = TariffID;

                cmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                a = RunExecuteNoneQuery(cmd);
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pSaveDistributorRechageBulk";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NetworkID", SqlDbType.Int).Value = NetworkID;
                cmd.Parameters.Add("@Rental", SqlDbType.Decimal).Value = Rental;
                cmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateHoldStatus(long DistributorID, int HoldStatus, string HoldReason)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pHolDistributor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = DistributorID;
                cmd.Parameters.Add("@isHold", SqlDbType.Bit).Value = HoldStatus;
                cmd.Parameters.Add("@HoldReason", SqlDbType.VarChar).Value = HoldReason;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }



        public DataSet GetDistributorServiceWithDate(int UserId, int Distrib, string TaxDocument, string ResellerCertificate, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetDistributorWithdate"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = UserId;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distrib;
                    objCmd.Parameters.Add("@TaxDocument", SqlDbType.VarChar).Value = TaxDocument;
                    objCmd.Parameters.Add("@ResellerCertificate", SqlDbType.VarChar).Value = ResellerCertificate;


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

        #region Update TopUp Option

        public int UpdateTopupOption(long DistributorID, decimal MinTopup, decimal MaxTopup, decimal PaypalTax, string PaypalTaxType)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update_Topup_Option";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = DistributorID;
                cmd.Parameters.Add("@MinTopup", SqlDbType.VarChar).Value = MinTopup;
                cmd.Parameters.Add("@MaxTopup", SqlDbType.VarChar).Value = MaxTopup;
                cmd.Parameters.Add("@PaypalTax", SqlDbType.VarChar).Value = PaypalTax;
                cmd.Parameters.Add("@PaypalTaxType", SqlDbType.VarChar).Value = PaypalTaxType;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }



        #endregion

        #region Get TopUp Information
        public DataSet GetDistributorInformation(int Distrib)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetDistributorInfo"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distrib;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataSet checkTaxId(string taxId, Int64 distributorID)
        {
            DataSet ds;
            try
            {
                using (SqlCommand cmd = new SqlCommand("pcheckTaxId"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@taxId", SqlDbType.VarChar).Value = taxId;
                    cmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = distributorID;
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
