using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class DBLoginHistory:DataBase
    {
        public string IpAddress1 = "";
        public string IpAddress2 = "";
        public string IpAddress3 = "";
        public string Browser = "";
        public string Location = "";
        public Int32 LoginID = 0;
        public DateTime? LoginTime;
        public string IpDetail;
        public string Browser1 = "";
        public int InsertLoginHistory()
        {
            int a = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spInsertLoginHistory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IpAddress1", SqlDbType.VarChar).Value = IpAddress1;
            cmd.Parameters.Add("@IpAddress2", SqlDbType.VarChar).Value = IpAddress2;
            cmd.Parameters.Add("@IpAddress3", SqlDbType.VarChar).Value = IpAddress3;

            cmd.Parameters.Add("@Browser", SqlDbType.VarChar).Value = Browser;
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
            cmd.Parameters.Add("@LoginTime", SqlDbType.DateTime).Value = LoginTime;
            cmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
            cmd.Parameters.Add("@IpDetail", SqlDbType.VarChar).Value = IpDetail;
            cmd.Parameters.Add("@Browser1", SqlDbType.VarChar).Value = Browser1;
            a = RunExecuteNoneQuery(cmd);
            return a;
            
        }



        public DataSet GetLoginHistory(DateTime FromDate, DateTime ToDate, int Distributorid)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("GetLoginHistory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
                    objCmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
                    objCmd.Parameters.Add("@Distributorid", SqlDbType.BigInt).Value = Distributorid;

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
