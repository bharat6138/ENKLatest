using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class DBVendor:DataBase
    {
        
        public int VendorID ;
        public string VendorCode;
        public string VendorName;
        public string VendorEmail;
        public string VendorContactPerson;
        public string VendorAddress;
        public string VendorMobile;
       
        public Boolean IsActive;

        public int InsertVendor(int DistributorID, int LoginID,int ClientTypeID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertVendor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DistributorID", SqlDbType.VarChar).Value = DistributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = LoginID;

                cmd.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = VendorCode;
                cmd.Parameters.Add("@VendorName", SqlDbType.VarChar).Value = VendorName;
                cmd.Parameters.Add("@VendorEmail", SqlDbType.VarChar).Value = VendorEmail;
                cmd.Parameters.Add("@VendorContactPerson", SqlDbType.VarChar).Value = VendorContactPerson;
                cmd.Parameters.Add("@VendorAddress", SqlDbType.VarChar).Value = VendorAddress;
                cmd.Parameters.Add("@VendorMobile", SqlDbType.VarChar).Value = VendorMobile;
                
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public int UpdateVendor(int DistributorID, int LoginID, int ClientTypeID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spUpdateVendor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DistributorID", SqlDbType.VarChar).Value = DistributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = LoginID;
                cmd.Parameters.Add("@VendorID", SqlDbType.BigInt).Value = VendorID;
                cmd.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = VendorCode;
                cmd.Parameters.Add("@VendorName", SqlDbType.VarChar).Value = VendorName;
                cmd.Parameters.Add("@VendorEmail", SqlDbType.VarChar).Value = VendorEmail;
                cmd.Parameters.Add("@VendorContactPerson", SqlDbType.VarChar).Value = VendorContactPerson;
                cmd.Parameters.Add("@VendorAddress", SqlDbType.VarChar).Value = VendorAddress;
                cmd.Parameters.Add("@VendorMobile", SqlDbType.VarChar).Value = VendorMobile;
               
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetVendorList( )
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetVendorList"))
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

        public DataSet GetSingleVendor()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetSingleVendor"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@VendorID", SqlDbType.BigInt).Value = VendorID;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateBulkNetwork(int NetworkID, DataTable  dt)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pUpdateBulkNetwork"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                    objCmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                    return ReturnDataSet(objCmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CheckSimNumber(int NetworkID, DataTable dt)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pCheckSimNumber"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@NetworkID", SqlDbType.BigInt).Value = NetworkID;
                    objCmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
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
