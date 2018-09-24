using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public static class UtilRaspberry
    {
        static string Host = "192.168.0.24";
        static string Username = "pi";
        static string Password = "raspberry";
        static string encenderLed = "sudo python /var/www/html/leds/gpio/4/enciende.py";
        static string apagarLed = "sudo python /var/www/html/leds/gpio/4/apaga.py";
        static string encenderMotor = "sudo python /var/www/html/leds/gpio/4/motor.py";
        static string apagarMotor = "sudo python /var/www/html/leds/gpio/4/downmotor.py";

        public static string conectarRaspberry(string comando1)
        {
            try
            {
                PasswordAuthenticationMethod authMethod = new PasswordAuthenticationMethod(Username, Password);
                ConnectionInfo connectionInfo = new ConnectionInfo(Host, Username, authMethod);

                using (var ssh = new SshClient(connectionInfo))
                {
                    ssh.Connect();
                    var cmd = ssh.CreateCommand(string.Format(comando1)).Execute();
                    ssh.Disconnect();
                }
                return "Exitoso";
            }
            catch (Exception ex)
            {
                return "Error enviando Comando Error: " + ex.ToString();
            }
        }

        public static string conectarRaspberry(string comando1, string comando2)
        {
            try
            {
                PasswordAuthenticationMethod authMethod = new PasswordAuthenticationMethod(Username, Password);
                ConnectionInfo connectionInfo = new ConnectionInfo(Host, Username, authMethod);

                using (var ssh = new SshClient(connectionInfo))
                {
                    ssh.Connect();
                    var cmd = ssh.CreateCommand(string.Format(comando1)).Execute();
                    var cmd2 = ssh.CreateCommand(string.Format(comando2)).Execute();
                    ssh.Disconnect();
                }
                return "Exitoso";
            }
            catch (Exception ex)
            {
                return "Error enviando Comando Error: " + ex.ToString();
            }
        }
    }
}
