using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class DBDING : DataBase
    {
        public DataTable GetCountriesForDing()
        {
            try
            {
                return FetchData("GetCountriesForDing ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable OperatorAginstCountry(string countryISO)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("OperatorAginstCountry"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("@countryISO", SqlDbType.Int).Value = countryISO;
                    return ReturnDataSet(objCmd).Tables[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
