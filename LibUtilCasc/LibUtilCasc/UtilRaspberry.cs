﻿using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibUtilCasc
{
    public static class UtilRaspberry
    {
        /*
        static string Host = "192.168.0.24";
        static string Username = "pi";
        static string Password = "raspberry";
        static string encenderLed = "sudo python /var/www/html/leds/gpio/4/enciende.py";
        static string apagarLed = "sudo python /var/www/html/leds/gpio/4/apaga.py";
        static string encenderMotor = "sudo python /var/www/html/leds/gpio/4/motor.py";
        static string apagarMotor = "sudo python /var/www/html/leds/gpio/4/downmotor.py";
        */
        static Dictionary<string, string> comanRasp = new Dictionary<string, string>();

        public static string ConectarRaspberry(string IpComedor,string comando1)
        {
            try
            {
                Inic();
                PasswordAuthenticationMethod authMethod = new PasswordAuthenticationMethod(comanRasp["Username"], comanRasp["Password"]);
                ConnectionInfo connectionInfo = new ConnectionInfo(IpComedor, comanRasp["Username"], authMethod);
                using (var ssh = new SshClient(connectionInfo))
                {
                    ssh.Connect();
                    var cmd = ssh.CreateCommand(string.Format(comanRasp[comando1])).Execute();
                    ssh.Disconnect();
                }
                return "Exitoso";
            }
            catch (Exception ex)
            {
                return "Error enviando Comando : " + ex.ToString();
            }
        }

        public static string ConectarRaspberry(string IpComedor, string comando1, string comando2)
        {
            try
            {
                Inic();
                PasswordAuthenticationMethod authMethod = new PasswordAuthenticationMethod(comanRasp["Username"], comanRasp["Password"]);
                ConnectionInfo connectionInfo = new ConnectionInfo(IpComedor, comanRasp["Username"], authMethod);
                using (var ssh = new SshClient(connectionInfo))
                {
                    ssh.Connect();
                    var cmd = ssh.CreateCommand(string.Format(comanRasp[comando1])).Execute();
                    var cmd2 = ssh.CreateCommand(string.Format(comanRasp[comando2])).Execute();
                    ssh.Disconnect();
                }
                return "Exitoso";
            }
            catch (Exception ex)
            {
                return "Error enviando Comando : " + ex.ToString();
            }
        }

        public static void Inic()
        {
            if(comanRasp.Count()<=0)
            {
                comanRasp.Add("Username", "pi");
                comanRasp.Add("Password", "raspberry");
                comanRasp.Add("encenderLed", "sudo python /var/www/html/leds/gpio/4/enciende.py");
                comanRasp.Add("apagarLed", "sudo python /var/www/html/leds/gpio/4/apaga.py");
                comanRasp.Add("encenderMotor", "sudo python /var/www/html/leds/gpio/4/motor.py");
                comanRasp.Add("apagarMotor", "sudo python /var/www/html/leds/gpio/4/downmotor.py");
            }
        }
    }
}
