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
    public class DBIP : DataBase
    {
       

        //Ankit Singh     
        //public int SaveIpMapping(long DistributorID, DataTable dtIp, int chkApiStatus)
        //{
        //    int a = 0;
        //    DataSet ds = null;
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("pSaveIPDistributorMapping"))
        //        {
        //            objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
        //            objCmd.Parameters.Add("@dtIP", SqlDbType.Structured).Value = dtIp;
        //            objCmd.Parameters.Add("@chkRistrictIP", SqlDbType.Int).Value = chkApiStatus;
        //            ds = ReturnDataSet(objCmd);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            { a = 1; }
        //            return a;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return a;
        //    }

        //}

        public int SaveIpMapping(long DistributorID, string _IP, int chkApiStatus)
        {
            int a = 0;
            DataSet ds = null;
            try
            {
                //using (SqlCommand objCmd = new SqlCommand("pSaveIPDistributorMapping"))
                using (SqlCommand objCmd = new SqlCommand("pSaveIPDistributorMapping_Aka"))
                {
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = _IP;
                    objCmd.Parameters.Add("@chkRistrictIP", SqlDbType.Int).Value = chkApiStatus;
                    ds = ReturnDataSet(objCmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    { a = 1; }
                    return a;
                }
            }
            catch (Exception ex)
            {
                return a;
            }

        }

        public DataTable GetIPMapping(Int32 Distributorid)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetIPMapping"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;
                    return ReturnDataSet(objCmd).Tables[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add by akash starts
        public int DeleteMappingID(int ID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("DeleteMappingID"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    return RunExecuteNoneQuery(objCmd);
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
