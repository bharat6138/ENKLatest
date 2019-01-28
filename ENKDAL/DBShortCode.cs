using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ENKDAL
{
    public class DBShortCode:DataBase
    {
        public DataSet GetShortCode(string Condition)
        {
            DataSet ds = null;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetShortCode"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Condition", SqlDbType.VarChar).Value = Condition;
                    ds = ReturnDataSet(objCmd);
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
