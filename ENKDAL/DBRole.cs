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
   public class DBRole:DataBase
    {
        public Int32 RoleID = 0;
       
        public string RoleName = "";
        public bool IsActive = true;

        public DataSet GetRole()
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetRole"))
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

        public DataSet GetScreen(int roleID)
        {
            DataSet ds;
            try
            {
                using (SqlCommand objCmd = new SqlCommand("spGetScreen"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = roleID;


                    return ReturnDataSet(objCmd);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add by akash starts
        public DataTable GetRolewiseScreen(int LoginID = 0, int RoleID = 0)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetRolewiseScreen"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                    objCmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;

                    return ReturnDataSet(objCmd).Tables[0];
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
