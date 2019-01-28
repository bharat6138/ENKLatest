<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargePrint.aspx.cs" Inherits="ENK.RechargePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #content {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
      
        //window.onbeforeunload = function () {

        //    alert('hello3333');
        //    if (confirm('If you refresh this page your transaction will intrupt? Do you want to refresh?')) {
        //        window.location.reload();
        //    }
        //    else {
        //        var uri = window.location.toString();
        //        if (uri.indexOf("?") > 0) {
        //            var clean_uri = uri.substring(0, uri.indexOf("?"));
        //            window.history.replaceState({}, document.title, clean_uri);
        //        }
        //        //Display the new URL without any querystrings.
        //        //alert(document.URL);
        //        window.location.href = "http://www.ENK.com/subscriberrecharge";
        //    }
        //}


       
      



        function JScriptConfirmationSuccess() {
           // alert("Subscriber Recharge Successfull.");
            //---url blank
            var uri = window.location.toString();
            if (uri.indexOf("?") > 0) {
                var clean_uri = uri.substring(0, uri.indexOf("?"));
                window.history.replaceState({}, document.title, clean_uri);
            }
           // ocument.write("You will be redirected to a new page in 5 seconds");
            setTimeout('JScriptConfirmationSuccess()', 5000);
            window.location.href = "http://www.ENK.com/subscriberrecharge";
          
           
        }




        function RemoveQueryString() {
            var uri = window.location.toString();
            if (uri.indexOf("?") > 0) {
                var clean_uri = uri.substring(0, uri.indexOf("?"));
                window.history.replaceState({}, document.title, clean_uri);
            }
            //Display the new URL without any querystrings.
            alert(document.URL);
        }
        function JScriptConfirmation() {
            window.location.href = "RechargePrint.aspx";
        }
  </script>
 <script type = "text/javascript" >
     (
         function (window, location) {
              
         history.replaceState(null, document.title, location.pathname + "#!/stealingyourhistory");
         history.pushState(null, document.title, location.pathname);

         window.addEventListener("popstate", function () {
             if (location.hash === "#!/stealingyourhistory") {
                 history.replaceState(null, document.title, location.pathname);
                 setTimeout(function () {
                     location.replace("http://www.ENK.com/subscriberrecharge");
                 }, 0);
             }
         }, false);
         }(window, location));
      


</script>
</head>
<body onkeydown="return (event.keyCode == 154)">
    <form id="form1" runat="server">
    <div id="content" class="container">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">   
                <h5> 
                    <label class="label  pull-right" style="color:#208ac4!important;font-size: large!important; text-align:center">Recharge</label>
                    <hr />
                     
                    <div class="row" style="border:black solid 2px; width: 400px;    padding: 10px; margin: auto;">
                                  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">  
                                 

                                      <div id="divPaymentDetail" runat="server">
                                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-12">
                                                            <asp:Label ID="lblTransactionAmount" runat="server" Style="color:#208ac4!important;text-transform: uppercase;"  Text=""></asp:Label>
                                                            </div>
                                                          <div class="col-xs-12 col-sm-3 col-md-3 col-lg-12">
                                                            <asp:Label ID="lblRechageAmount" runat="server" Style="color:#208ac4!important;text-transform: uppercase;"  Text=""></asp:Label>
                                                            </div>
                                                         
                                                           
                                                         
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblPayerName" runat="server" Style="color:#208ac4!important;text-transform: uppercase;" Text=""></asp:Label>
                                                            </div>
                                                        
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblTxnID" runat="server" Style="color:#208ac4!important;text-transform: uppercase;" Text=""></asp:Label>
                                                            </div>
                                           <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label style="color:#208ac4!important; text-align:center;" ID="lblmsg" runat="server"  Text=""></asp:Label>
                                                            </div>
                                                             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblTransactionDate" runat="server" Style="color:#208ac4!important;text-transform: uppercase;" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                  
                                                            <h5> <asp:Label ID="lblMessage" runat="server" Text="" Style="color: green;font-size: large;"></asp:Label></h5>
                                                        <br />
                                                         </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" id="img" runat="server" visible="false">                                  
                                            <img src="img/thankyou.png" class="pull-right display-image" alt="" style="width:300px;  margin-top: -27px!important">
                                        </div>
                                       
                                      
                                       
                                       </div>
                                 
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                             
                                                            <asp:HyperLink ID="lnkback" NavigateUrl="http://www.ENK.com/subscriberrecharge/" Class="pull-right" runat="server">Back to Recharge</asp:HyperLink>
                                                         
                                                        
                                                    </div>
                             </div>
                </h5>
                     </div>
    </div>
    </form>
</body>
</html>
