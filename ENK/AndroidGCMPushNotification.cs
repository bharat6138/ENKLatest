using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;

/// <summary>
/// Summary description for AndroidGCMPushNotification
/// </summary>
public class AndroidGCMPushNotification
{
	public AndroidGCMPushNotification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     public string SendNotification(string[] deviceId, string message)
    {
        try
        {
            string GoogleAppID = "AIzaSyDe1rlpUXNP-WKTUwqulwofa8Kru2khFCQ";
            var SENDER_ID = "737742163152";//"1052581621436";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "POST";
            //tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            StringBuilder DeviceRegID =new StringBuilder (String.Empty );

            foreach (String strDevice in deviceId )
            {
                DeviceRegID.Append("\"" + strDevice + "\",");

            }
            String strDeviceRegIds = DeviceRegID.ToString().Substring(0, DeviceRegID.ToString().Length - 1);
            string postData = "{\"registration_ids\":[" + strDeviceRegIds + "]," +
                                  "\"data\": {" +
                                    "\"message\": \""+value+"\","+
                                    "\"time\": \"" + System.DateTime.Now.ToString("HH:mm:ss") + "\"}}";
                                  

            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            //tRequest.ContentLength = byteArray.Length;
            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
        catch (Exception ex)
        {
            return "";
        }
     }
        public string SendNotificationDemo(string[] deviceId, string message)
        {
            try
            {
                string GoogleAppID = "AIzaSyDKADy2K-rc6eVvZ0ai_kYSrLzzYZ9jufM";
                var SENDER_ID = "1052581621436";
                var value = message;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "POST";
                //tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                StringBuilder DeviceRegID =new StringBuilder (String.Empty );

                foreach (String strDevice in deviceId )
                {
                    DeviceRegID.Append("\"" + strDevice + "\",");

                }
                String strDeviceRegIds = DeviceRegID.ToString().Substring(0, DeviceRegID.ToString().Length - 1);
                string postData = "{\"registration_ids\":[" + strDeviceRegIds + "]," +
                                        "\"data\": {" +
                                        "\"message\": \""+ value +"\","+
                                        "\"time\": \"" + System.DateTime.Now.ToString("HH:mm:ss") + "\"}}";
                                  

                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();
                //tRequest.ContentLength = byteArray.Length;
                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                return sResponseFromServer;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    
}