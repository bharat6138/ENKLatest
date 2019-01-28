using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENKDAL
{
    public class DBH2O : DataBase
    {
        public DataSet GetCountryByAreaCode(int _AreaCode)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetCityList"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@AreaCode", SqlDbType.Int).Value = _AreaCode;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetH2OStatesList()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetH2OStates"))
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

        public DataSet GetH2OServiceProviderList()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetH2OServiceProviderList"))
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
    }
}
