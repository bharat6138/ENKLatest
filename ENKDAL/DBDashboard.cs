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
    public class DBDashboard:DataBase
    {
        public DataSet ShowDashBoardData(int DistributorID, int ClientTypeID, int NetworkID)
        {
            DataSet ds = null;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetCountInventoryForActivationSIMandBlankSIM"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;                   
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@NetworkId", SqlDbType.Int).Value = NetworkID;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                return ds;
                throw ex;
            }
        }

        public DataSet ShowDashBoardActivationData(int DistributorID, int ClientTypeID,int loginID,string Action,int month, int year, string FromDate, string ToDate)
        {
            DataSet ds = null;
            try
            {
                //using (SqlCommand objCmd = new SqlCommand("pGetInventoryForDashBoard"))
                using (SqlCommand objCmd = new SqlCommand("pGetInventoryActivatedANDAvailable"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = loginID;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    objCmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    objCmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    objCmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = ToDate;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                return ds;
                throw ex;
            }
        }

        public DataSet CountActivationPreloaded(int DistributorID, int ClientTypeID, int loginID,  int month, int year, int NetworkID)
        {
            DataSet ds = new DataSet();
            try{
                using (SqlCommand objCmd = new SqlCommand("pCountSimActivationForDashBoard"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorId", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.BigInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = loginID;
                 
                    objCmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    objCmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    objCmd.Parameters.Add("@NetworkId", SqlDbType.Int).Value = NetworkID;
                    return ReturnDataSet(objCmd);
                }
            }
            catch(Exception ex)
            {
                return ds;
                throw ex;
            }
        }
 

    }
}
