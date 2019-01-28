using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class DecryptPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private void DecryptPwd(string Pwd)
        {
            string pass = Encryption.Decrypt(Pwd);
            lblMessage.Style.Add("display", "block");
            // lblwarning.Attributes.Add("display", "block");
            lblMessage.Text = pass;
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            string Pwd = txtPassword.Text;
            DecryptPwd(Pwd);
        }
    }
}