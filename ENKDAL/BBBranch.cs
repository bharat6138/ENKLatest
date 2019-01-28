using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENKDAL
{
    class BBBranch
    {
       public Int32 BranchID = 0;
       public string BranchName = "";
       public Int32 BranchCode = 0;
       public Int32 BranchType = 0;
       public string BranchTypeName = "";
       public Int32 BranchParent = 0;

       public string ContactPerson = "";
       public string ContactNo = "";
       public string WebSiteName = "";
       public string EmailID = "";

       public string Address = "";
       public string City = "";
       public string State = "";
       public string Zip = "";
       public int CountryId = 0;

       public bool IsServiceTaxExmpted = true;
       public double BalanceAmount = 0.0;
       public bool IsActive = true;

    }
}
