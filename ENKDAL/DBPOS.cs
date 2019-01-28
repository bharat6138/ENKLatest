using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class DBPOS : DataBase
    {
        public Int64 MSISDN_SIM_ID = 0;
        public string CoustomerName = "";
        public string Email = "";
        public string Address = "";
        public string MobileNumber = "";

        public int SavePOS(int DistributorID ,int LoginID)
        {
            int a = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pSavePOS";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MSISDN_SIM_ID", SqlDbType.BigInt).Value = MSISDN_SIM_ID;
                cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = CoustomerName;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
                cmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = MobileNumber;
                cmd.Parameters.Add("@DistributorID", SqlDbType.Int).Value = DistributorID;
                cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                a = RunExecuteNoneQuery(cmd);
                return a;
            }
            catch (Exception ex)
            {
                return a;
            }
        }

        public DataSet GetPOS(int DistbutorID, int ClientTypeID)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetPOSInventory"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ClientTypeID", SqlDbType.TinyInt).Value = ClientTypeID;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.TinyInt).Value = DistbutorID;
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
