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
    public class DBCurrency:DataBase
    {
        public int InsertCurrency(string Name)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spInsertCurrency";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;

                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetCurrencyList(int DistributorID, int ClientTypeID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetCurrencyList"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DistributorID", SqlDbType.VarChar).Value = DistributorID;
                    cmd.Parameters.Add("@ClientTypeID", SqlDbType.VarChar).Value = ClientTypeID;
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
