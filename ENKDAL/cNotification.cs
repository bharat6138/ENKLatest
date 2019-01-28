using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{
    public class cNotification:DataBase
    {
        //public DataSet SaveAndSendNotification(long LoginID,long DistributorID, DataTable dt, string NotificationText, string Status)
        //{
        //    try
        //    {
        //        using (SqlCommand objCmd = new SqlCommand("pSaveNotification"))
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
        //            objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
        //            objCmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
        //            objCmd.Parameters.Add("@NotificationText", SqlDbType.VarChar).Value = NotificationText;
        //            objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

        //            return ReturnDataSet(objCmd);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataSet SaveAndSendNotification(long LoginID, long DistributorID, DataTable dt, string NotificationText, string Status, string Action, int id)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pSaveNotification"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@dt", SqlDbType.Structured).Value = dt;
                    objCmd.Parameters.Add("@NotificationText", SqlDbType.VarChar).Value = NotificationText;
                    objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;
                    objCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = Action;
                    objCmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetNotificationPagingWise(long LoginID, long DistributorID, int PageNo, string Status)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetNotificationPagingWise"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@LoginID", SqlDbType.BigInt).Value = LoginID;
                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@PageNo", SqlDbType.Int).Value = PageNo;
                    objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetNotification(long DistributorID, int MsgId)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetNotification"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorID;
                    objCmd.Parameters.Add("@MsgID", SqlDbType.Int).Value = MsgId;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ViewNotification(long Createdby, int MsgId)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pViewNotification"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@Createdby", SqlDbType.BigInt).Value = Createdby;
                    objCmd.Parameters.Add("@MsgID", SqlDbType.Int).Value = MsgId;


                    return ReturnDataSet(objCmd);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetHoldReason(long DistributorId)
        {
            try
            {
                using (SqlCommand objCmd = new SqlCommand("pGetHoldMsgForDistributor"))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@DistributorID", SqlDbType.BigInt).Value = DistributorId;
                    
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


