using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace LibUtilCasc
{
    /// <summary>
    /// Utilidades para conectarse a Raspberry Pi por SSH y ejecutar comandos
    /// Las credenciales se leen desde AppSettings con valores por defecto
    /// </summary>
    public static class UtilRaspberry
    {
        static Dictionary<string, string> comanRasp = new Dictionary<string, string>();

        /// <summary>
        /// Método privado auxiliar para ejecutar comandos SSH
        /// </summary>
        private static string EjecutarComandoSSH(string ipRaspberry, params string[] nombresComandos)
        {
            try
            {
                Init();

                // Validar que todos los comandos existen
                foreach (string cmd in nombresComandos)
                {
                    if (!comanRasp.ContainsKey(cmd))
                    {
                        Logger.Warn($"Comando no configurado: {cmd}");
                        return $"Error: Comando '{cmd}' no configurado";
                    }
                }

                string username = comanRasp["Username"];
                string password = comanRasp["Password"];

                PasswordAuthenticationMethod authMethod = new PasswordAuthenticationMethod(username, password);
                ConnectionInfo connectionInfo = new ConnectionInfo(ipRaspberry, 22, username, authMethod);

                using (var ssh = new SshClient(connectionInfo))
                {
                    ssh.Connect();

                    // Ejecutar todos los comandos
                    foreach (string nombreComando in nombresComandos)
                    {
                        string commandText = comanRasp[nombreComando];
                        var cmd = ssh.CreateCommand(commandText).Execute();
                    }

                    ssh.Disconnect();
                }

                Logger.Info($"Comandos ejecutados exitosamente en {ipRaspberry}: {string.Join(", ", nombresComandos)}");
                return "Exitoso";
            }
            catch (Exception ex)
            {
                Logger.Error($"Error ejecutando comandos en {ipRaspberry}", ex);
                return "Error enviando Comando: " + ex.Message;
            }
        }

        /// <summary>
        /// Ejecuta un comando en Raspberry Pi
        /// </summary>
        /// <param name="IpRaspberry">Dirección IP de la Raspberry Pi</param>
        /// <param name="comando">Nombre del comando a ejecutar (ej: "encenderLed")</param>
        /// <returns>"Exitoso" si funciona, o mensaje de error</returns>
        /// <exception cref="ArgumentNullException">Si IP o comando son nulos/vacíos</exception>
        public static string ConectarRaspberry(string IpRaspberry, string comando)
        {
            if (string.IsNullOrEmpty(IpRaspberry))
                throw new ArgumentNullException(nameof(IpRaspberry), "IP de Raspberry no puede ser nula o vacía");
            if (string.IsNullOrEmpty(comando))
                throw new ArgumentNullException(nameof(comando), "Comando no puede ser nulo o vacío");

            return EjecutarComandoSSH(IpRaspberry, comando);
        }

        /// <summary>
        /// Ejecuta dos comandos secuencialmente en Raspberry Pi
        /// </summary>
        /// <param name="IpRaspberry">Dirección IP de la Raspberry Pi</param>
        /// <param name="comando1">Nombre del primer comando</param>
        /// <param name="comando2">Nombre del segundo comando</param>
        /// <returns>"Exitoso" si funciona, o mensaje de error</returns>
        /// <exception cref="ArgumentNullException">Si IP o comandos son nulos/vacíos</exception>
        public static string ConectarRaspberry(string IpRaspberry, string comando1, string comando2)
        {
            if (string.IsNullOrEmpty(IpRaspberry))
                throw new ArgumentNullException(nameof(IpRaspberry), "IP de Raspberry no puede ser nula o vacía");
            if (string.IsNullOrEmpty(comando1))
                throw new ArgumentNullException(nameof(comando1), "Comando 1 no puede ser nulo o vacío");
            if (string.IsNullOrEmpty(comando2))
                throw new ArgumentNullException(nameof(comando2), "Comando 2 no puede ser nulo o vacío");

            return EjecutarComandoSSH(IpRaspberry, comando1, comando2);
        }

        /// <summary>
        /// Ejecuta un comando en Raspberry Pi de forma asincrónica
        /// </summary>
        /// <param name="IpRaspberry">Dirección IP de la Raspberry Pi</param>
        /// <param name="comando">Nombre del comando a ejecutar</param>
        /// <returns>Tarea que retorna "Exitoso" o mensaje de error</returns>
        public static async Task<string> ConectarRaspberryAsync(string IpRaspberry, string comando)
        {
            if (string.IsNullOrEmpty(IpRaspberry))
                throw new ArgumentNullException(nameof(IpRaspberry));
            if (string.IsNullOrEmpty(comando))
                throw new ArgumentNullException(nameof(comando));

            return await Task.Run(() => ConectarRaspberry(IpRaspberry, comando)).ConfigureAwait(false);
        }

        /// <summary>
        /// Inicializa la configuración de comandos y credenciales
        /// Lee de AppSettings con valores por defecto: RaspUsername, RaspPassword
        /// Valores por defecto: usuario="pi", password="raspberry"
        /// </summary>
        public static void Init()
        {
            if (comanRasp.Count() <= 0)
            {
                // Leer credenciales desde AppSettings o usar defaults
                string username = ConfigurationManager.AppSettings["RaspUsername"] ?? "pi";
                string password = ConfigurationManager.AppSettings["RaspPassword"] ?? "raspberry";

                comanRasp.Add("Username", username);
                comanRasp.Add("Password", password);

                // Configurar comandos
                comanRasp.Add("encenderLed", "sudo python /var/www/html/leds/gpio/4/enciende.py");
                comanRasp.Add("apagarLed", "sudo python /var/www/html/leds/gpio/4/apaga.py");
                comanRasp.Add("encenderMotor", "sudo python /var/www/html/leds/gpio/4/motor.py");
                comanRasp.Add("apagarMotor", "sudo python /var/www/html/leds/gpio/4/downmotor.py");

                Logger.Info($"UtilRaspberry inicializado con usuario: {username}");
            }
        }
    }
}
