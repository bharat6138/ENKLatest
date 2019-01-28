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
    public class DBApi : DataBase
    {
        //Ankit Singh
        //Get status Of Mapping And Display Report of Mapping 
        public DataSet GetApiDetails(long DistributorID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetAPIDetail"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;

                    return ReturnDataSet(objCmd);
                }


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Ankit Singh
        // save and update- Same method for Api Mapping and Update api Mapping
        public int SaveApiMapping(long DistributorID, int clientcode, DataTable dtApi, int chkApiStatus)
        {
            int a = 0;
            DataSet ds = null;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSaveAPIDistributorMapping"))
                {
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@Clientcode", SqlDbType.Int).Value = clientcode;
                    objCmd.Parameters.Add("@dtApi", SqlDbType.Structured).Value = dtApi;
                    //objCmd.Parameters.Add("@dtIP", SqlDbType.Structured).Value = dtIp;
                    objCmd.Parameters.Add("@chkApiStatus", SqlDbType.Bit).Value = chkApiStatus;
                    //objCmd.Parameters.Add("@chkRistrictIP", SqlDbType.Bit).Value = chkRistrictIP;

                    //a = RunExecuteNoneQuery(objCmd);
                    //return a;
                     ds=ReturnDataSet(objCmd);
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


    }
}
