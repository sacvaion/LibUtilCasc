# Tab Raspberry Pi - Guía de Uso

## 📋 Descripción

La pestaña **Raspberry Pi** permite probar funciones de control remoto de Raspberry Pi de la clase `UtilRaspberry`:
- Conectarse a Raspberry Pi por SSH
- Ejecutar comandos predefinidos (LED, motor)
- Inicializar conexión SSH
- Ejecución síncrona y asíncrona

## ⚠️ Requisitos Previos

```
✓ REQUERIDO para que funcione:
  - Raspberry Pi conectada a la red
  - SSH habilitado en Raspberry Pi
  - Credenciales SSH válidas (default: pi/raspberry)
  - Scripts Python en /var/www/html/leds/gpio/

✗ NO FUNCIONARÁ SIN:
  - Raspberry Pi disponible
  - Conectividad SSH
  - Scripts en ubicación correcta
```

## 🎮 Controles

### 1. Inicializar Conexión

```
┌────────────────────────────────────────┐
│ [Inicializar (Init)]                   │
└────────────────────────────────────────┘
```

**Entrada:** Ninguna

**Función:** `UtilRaspberry.Init()`

**Proceso:**
1. Lee credenciales desde App.config
   - RaspUsername (default: "pi")
   - RaspPassword (default: "raspberry")
2. Configura comandos predefinidos
3. Inicializa diccionario interno

**Resultado:**
```
19:30:15 - ✓ UtilRaspberry inicializado
```

**Nota:** Este botón solo prepara las credenciales. NO conecta aún.

---

### 2. Configurar IP de Raspberry

```
┌────────────────────────────────────────┐
│ IP Raspberry: [192.168.0.24         ]  │
└────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "192.168.0.24"
- Dirección IP o hostname de Raspberry Pi

**Ejemplos válidos:**
- "192.168.0.24" (IP en red local)
- "192.168.1.100"
- "10.0.0.50"
- "raspberry.local" (si mDNS está disponible)

---

### 3. Seleccionar Comando

```
┌────────────────────────────────────────┐
│ Comando: [encenderLed               ▼] │
│          [apagarLed                  ]  │
│          [encenderMotor              ]  │
│          [apagarMotor                ]  │
└────────────────────────────────────────┘
```

**Comandos predefinidos:**

| Comando | Función | Script |
|---------|---------|--------|
| **encenderLed** | Enciende LED GPIO 4 | `/var/www/html/leds/gpio/4/enciende.py` |
| **apagarLed** | Apaga LED GPIO 4 | `/var/www/html/leds/gpio/4/apaga.py` |
| **encenderMotor** | Enciende motor GPIO 4 | `/var/www/html/leds/gpio/4/motor.py` |
| **apagarMotor** | Apaga motor GPIO 4 | `/var/www/html/leds/gpio/4/downmotor.py` |

**Nota:** Todos operan en GPIO 4 (modificable en código)

---

### 4. Ejecutar Comando (Síncrono)

```
┌────────────────────────────────────────┐
│ [Ejecutar Comando]                     │
└────────────────────────────────────────┘
```

**Entrada:**
- IP Raspberry (TextBox)
- Comando (ComboBox)

**Función:** `UtilRaspberry.ConectarRaspberry(ip, comando)`

**Proceso:**
1. Lee credenciales del diccionario (Init debe haber sido llamado)
2. Conecta SSH a IP especificada
3. Ejecuta el comando Python
4. Desconecta
5. Retorna "Exitoso" o mensaje de error

**Resultado:**
```
19:30:20 - Comando: encenderLed
19:30:22 - Resultado: Exitoso

O en caso de error:
19:30:20 - Comando: encenderLed
19:30:25 - ✗ Error: No se puede conectar a 192.168.0.24:22
```

---

### 5. Ejecutar Comando (Asíncrono)

```
┌────────────────────────────────────────┐
│ [Ejecutar Async]                       │
└────────────────────────────────────────┘
```

**Entrada:** Misma que Ejecutar Comando

**Función:** `async UtilRaspberry.ConectarRaspberryAsync(ip, cmd)`

**Diferencia:**
- No bloquea la UI mientras se conecta y ejecuta
- Ideal para Raspberry Pi lenta o lejana
- Resultado aparece cuando termina

**Resultado:**
```
19:30:20 - Ejecutando (async)...
19:30:25 - ✓ Comando (async): encenderLed
19:30:25 - Resultado: Exitoso
```

---

### 6. Advertencia

```
⚠️ Requiere Raspberry Pi conectada a la red con SSH habilitado
```

Etiqueta informativa roja/naranja recordando requerimientos.

---

## 📖 Casos de Uso

### 1. Control de LED remoto

```
Sistema de alarma:
1. IP: "192.168.0.24"
2. Comando: "encenderLed"
3. Click [Ejecutar Comando]
4. → LED se enciende en Raspberry
5. → "Exitoso"
```

### 2. Encender y apagar en secuencia

```
Demostración de control:
1. Seleccionar "encenderLed" → Click [Ejecutar]
2. Esperar 2 segundos
3. Seleccionar "apagarLed" → Click [Ejecutar]
4. → LED parpadea
```

### 3. Control de motor con async

```
Sistema IoT sin congelamiento:
1. IP: "raspberrypi.local"
2. Comando: "encenderMotor"
3. Click [Ejecutar Async]
4. → UI sigue respondiendo mientras se conecta
5. → Motor arranca en Raspberry
6. → Resultado aparece cuando termina
```

### 4. Validar conectividad SSH

```
Prueba de conectividad:
1. Init (solo una vez)
2. Intentar cualquier comando
3. Si "Exitoso" → SSH funciona ✓
4. Si error → Verificar IP, credenciales, firewall ✗
```

---

## 🔍 Detalles Técnicos

### UtilRaspberry.Init

```csharp
public static void Init()
{
    // Leer desde App.config
    string username = ConfigurationManager.AppSettings["RaspUsername"] ?? "pi";
    string password = ConfigurationManager.AppSettings["RaspPassword"] ?? "raspberry";
    
    // Guardar en diccionario estático
    comanRasp["Username"] = username;
    comanRasp["Password"] = password;
    
    // Configurar comandos
    comanRasp["encenderLed"] = "sudo python /var/www/html/leds/gpio/4/enciende.py";
    comanRasp["apagarLed"] = "sudo python /var/www/html/leds/gpio/4/apaga.py";
    comanRasp["encenderMotor"] = "sudo python /var/www/html/leds/gpio/4/motor.py";
    comanRasp["apagarMotor"] = "sudo python /var/www/html/leds/gpio/4/downmotor.py";
}
```

---

### ConectarRaspberry (Síncrono)

```csharp
public static string ConectarRaspberry(string ipRaspberry, string comando)
{
    try
    {
        Init(); // Asegura que está inicializado
        
        string username = comanRasp["Username"];
        string password = comanRasp["Password"];
        
        // Crear conexión SSH
        PasswordAuthenticationMethod auth = 
            new PasswordAuthenticationMethod(username, password);
        ConnectionInfo connInfo = 
            new ConnectionInfo(ipRaspberry, 22, username, auth);
        
        // Ejecutar comando
        using (var ssh = new SshClient(connInfo))
        {
            ssh.Connect();
            string commandText = comanRasp[comando];
            ssh.CreateCommand(commandText).Execute();
            ssh.Disconnect();
        }
        
        return "Exitoso";
    }
    catch (Exception ex)
    {
        Logger.Error($"Error en ConectarRaspberry", ex);
        return "Error: " + ex.Message;
    }
}
```

---

### ConectarRaspberryAsync (Asíncrono)

```csharp
public static async Task<string> ConectarRaspberryAsync(string ip, string cmd)
{
    return await Task.Run(() => ConectarRaspberry(ip, cmd));
}
```

**Ventaja:** No bloquea thread de UI

---

## ⚠️ Casos Especiales

### IP inválida o inaccesible

```
IP: "192.168.0.999" (inválida)
→ Resultado: ✗ Error: Unable to resolve host name
```

### SSH deshabilitado en Raspberry

```
IP: "192.168.0.24" (no tiene SSH)
→ Resultado: ✗ Error: Connection refused (port 22)
```

### Credenciales incorrectas

```
Username: "admin" (incorrecto)
Password: "wrongpass"
→ Resultado: ✗ Error: Permission denied (publickey,password)
```

### Script no existe

```
Comando: "encenderLed"
Script: /var/www/html/leds/gpio/4/enciende.py (no existe)
→ Resultado: ✗ Error: python: can't open file
```

### Timeout (Raspberry muy lenta)

```
Raspberry offline o muy lenta:
→ Espera 30 segundos
→ Resultado: ✗ Error: Operation timed out
```

### Sin permisos en script

```
Script existe pero sin permisos de ejecución:
→ Resultado: ✗ Error: Permission denied
Solución en Raspberry: chmod +x enciende.py
```

---

## 🧪 Pruebas Sugeridas (si tienes Raspberry disponible)

1. **Test Init**: Click [Init] → Mensaje ✓ inicializado
2. **Test conectividad**: IP válida, comando cualquiera → "Exitoso"
3. **Test LED on**: Comando "encenderLed" → LED enciende en Raspberry
4. **Test LED off**: Comando "apagarLed" → LED apaga
5. **Test Motor on**: Comando "encenderMotor" → Motor gira
6. **Test Motor off**: Comando "apagarMotor" → Motor para
7. **Test Async**: IP lejana → Async no bloquea, sync bloquea
8. **Test IP incorrecta**: "999.999.999.999" → Error conexión
9. **Test comando inexistente**: Modificar ComboBox → Error
10. **Test rapidez**: Múltiples comandos seguidos → Todos ejecutan

---

## 🔧 Configuración Necesaria

### En App.config (Windows)

```xml
<appSettings>
  <add key="RaspUsername" value="pi" />
  <add key="RaspPassword" value="raspberry" />
</appSettings>
```

### En Raspberry Pi

```bash
# Habilitar SSH (Bullseye+)
sudo raspi-config
→ Interface Options → SSH → Enable

# O manualmente
sudo systemctl enable ssh
sudo systemctl start ssh

# Crear estructura de carpetas
sudo mkdir -p /var/www/html/leds/gpio/4

# Crear script enciende.py
cat > /var/www/html/leds/gpio/4/enciende.py << 'EOF'
#!/usr/bin/env python3
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
GPIO.setup(4, GPIO.OUT)
GPIO.output(4, GPIO.HIGH)
GPIO.cleanup()
EOF

# Dar permisos
chmod +x /var/www/html/leds/gpio/4/enciende.py

# Dar permisos sudo sin contraseña (opcional, para usuario pi)
sudo visudo
# Agregar: pi ALL=(ALL) NOPASSWD: /usr/bin/python*
```

---

## 📊 Tabla de Estados de Conexión

| Estado | Mensaje | Solución |
|--------|---------|----------|
| ✓ Exitoso | Comando ejecutado | N/A |
| ✗ Connection refused | SSH no habilitado | Habilitar SSH |
| ✗ Unable to resolve | IP/hostname inválido | Verificar IP |
| ✗ Permission denied | Credenciales incorrectas | Verificar user/pass |
| ✗ Operation timed out | Raspberry muy lenta/offline | Verificar conectividad |
| ✗ Can't open file | Script no existe | Crear script |

---

## 🔗 Referencias

- [Clase UtilRaspberry](../LibUtilCasc/UtilRaspberry.cs)
- [Renci.SshNet Documentation](https://sshnet.codeplex.com/)
- [Raspberry Pi SSH Setup](https://www.raspberrypi.com/documentation/computers/remote-access.html)
- [Raspberry Pi GPIO](https://www.raspberrypi.com/documentation/computers/gpio.html)
- [Python RPi.GPIO](https://pypi.org/project/RPi.GPIO/)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0

**Nota:** Esta funcionalidad requiere hardware (Raspberry Pi) y es opcional. Todas las demás tabs funcionan sin hardware adicional.
