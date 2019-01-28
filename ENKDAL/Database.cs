using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ENKDAL
{
    public class DataBase
    {
        SqlConnection con;
        SqlCommand cmd;

        //public DataBase()
        //{
        //    con = new SqlConnection(ENKDAL.Properties.Settings.Default.Connection);
        //}

        public DataBase()
        {

            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/ServerConfig.txt");

            string conString = reader.ReadLine();
            con = new SqlConnection(conString);

        }

        protected DataTable FetchData(string sqlQuery)
        {
            cmd = new SqlCommand(sqlQuery, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                DataTable dt = new DataTable("ResultDataTable");
                con.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                cmd.Dispose();
            }
        }

        protected object RunCommand(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 1800;
                con.Open();
                return cmd.ExecuteScalar();
                //return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        protected Int32 RunExecuteNoneQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 1800;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        protected DataTable ExecuteSqlData(string sqlQuery, SqlCommand cmd = null)
        {
            if (sqlQuery != String.Empty)
            {
                cmd = new SqlCommand(sqlQuery);
                cmd.CommandType = CommandType.Text;
            }

            try
            {
                DataTable dt = new DataTable("ResultDataTable");
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                cmd.Dispose();
            }
        }

        protected DataSet ReturnDataSet(SqlCommand objCmd)
        {
            try
            {
                DataSet objDS= new DataSet();
                SqlDataAdapter objDA = null;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = con;
                objCmd.CommandTimeout = 1800;
                con.Open();
                using (objDA=new SqlDataAdapter(objCmd))
                {
                    objDA.Fill(objDS);
                    return objDS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
               // cmd.Dispose();
            }
        }
    }
}