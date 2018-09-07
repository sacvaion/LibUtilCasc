using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public class UtilNetWork
    {
        /// <summary>
        /// Get List with MAC actives
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLstMac()
        {
            // obtener MAC
            List<string> lstMac = new List<string>();
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var mac in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                //mac.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                if (mac.OperationalStatus == OperationalStatus.Up && (!mac.Description.Contains("Virtual") && !mac.Description.Contains("Pseudo")) && mac.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (mac.GetPhysicalAddress().ToString() != "")                    
                        lstMac.Add(mac.GetPhysicalAddress().ToString());                    
                }
            }
            return lstMac;
        }

        /// <summary>
        /// Get List Ips Active in client
        /// </summary>
        /// <param name="strHostName"></param>
        /// <returns></returns>
        public  static List<string> GetLstIp(String strHostName)
        {
            //obtener IP
            List<string> lstIP = new List<string>();
            foreach (var ip in (Dns.GetHostEntry(strHostName)).AddressList.Where(p => p.IsIPv6LinkLocal == false))            
                lstIP.Add(ip.ToString());
				
            return lstIP;
        }

        /// <summary>
        /// Get MacAddress in format {XX:XX:XX:XX:XX:XX}
        /// </summary>
        /// <param name="macAddress">MacAddress Hostname</param>
        /// <returns></returns>
        public static string GetMacFormat(string macAddress)
        {
            string macAddressF = "";
            try
            {
                var regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
                var replace = "$1:$2:$3:$4:$5:$6";
                macAddressF = Regex.Replace(macAddress, regex, replace);
            }
            catch (Exception)
            {
            }
            return macAddressF;
        }

    }
}
