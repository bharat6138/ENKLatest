using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ENKDAL
{

    public class cVendor : DataBase
    {
       public int UserID = 0;

       public DataTable GetVendor()
       {
           try
           {
               return FetchData("pFetchVendor " + UserID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable PerticularAPIDOWN(string APINAME)
       {
           try
           {
               return FetchData("GetPerticular_M_API_Down " + APINAME);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       
    }
}
