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
    public class DBClientType:DataBase
    {
        public DataSet GetClientType(int UserId, int DistributorID)
        {
            DataSet ds = null;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetClientType"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = UserId;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                   ds=ReturnDataSet(objCmd);
                   return ds;
                }

            }
            catch (Exception ex)
            {
                return ds;
                throw ex;
            }
        }
    }
}
