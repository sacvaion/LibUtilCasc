# Tab Red - Guía de Uso

## 📋 Descripción

La pestaña **Red** permite probar funciones de red de la clase `UtilNetWork`:
- Obtener direcciones MAC del sistema
- Resolver hostnames a direcciones IP (síncrono y asíncrono)
- Formatear direcciones MAC

## 🎮 Controles

### 1. Obtener MACs

```
┌────────────────────────────────────────┐
│ [Obtener MACs]                         │
└────────────────────────────────────────┘
```

**Entrada:** Ninguna (usa adaptadores de red del sistema)

**Función:** `UtilNetWork.GetLstMac()`

**Resultado:** Lista de direcciones MAC de todos los adaptadores de red

**Ejemplo:**
```
MACs encontradas (2):
  - 00-11-22-33-44-55
  - AA-BB-CC-DD-EE-FF
```

**Casos:**
- Si hay NIC: Retorna lista de MACs
- Si no hay NIC: Retorna lista vacía

---

### 2. Obtener IPs (Síncrono)

```
┌────────────────────────────────────────┐
│ Hostname: [localhost              ]    │
│           [Obtener IPs]                │
└────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "localhost"
- Cualquier hostname válido

**Función:** `UtilNetWork.GetLstIp(hostname)`

**Resultado:** Lista de direcciones IP del hostname

**Ejemplo:**
```
Entrada: "localhost"
Resultado:
  - 127.0.0.1
  - ::1 (IPv6 loopback)

Entrada: "google.com"
Resultado:
  - 142.250.185.46
  - 2607:f8b0:4004:809::200e (IPv6)
```

---

### 3. Obtener IPs (Asíncrono)

```
┌────────────────────────────────────────┐
│ [Obtener IPs Async]                    │
└────────────────────────────────────────┘
```

**Entrada:** Usa el mismo **Hostname** que sección anterior

**Función:** `async UtilNetWork.GetLstIpAsync(hostname)`

**Comportamiento:**
- No bloquea la UI mientras resuelve
- Útil para hostnames lejanos o DNS lento
- Resultado aparece en Panel de Resultados cuando termina

**Ejemplo:**
```
19:30:15 - Obteniendo IPs (async)...
19:30:18 - ✓ IPs encontradas (async) para google.com (2):
           - 142.250.185.46
           - 2607:f8b0:4004:809::200e
```

---

### 4. Formatear MAC

```
┌────────────────────────────────────────┐
│ MAC (12 hex): [001122334455         ]  │
│               [Formatear MAC]          │
└────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "001122334455" (12 caracteres hex sin separadores)
- Cualquier MAC en formato continuo

**Función:** `UtilNetWork.GetMacFormat(mac)`

**Proceso:**
- Convierte: "001122334455" → "00:11:22:33:44:55"
- Inserta ":" cada 2 caracteres

**Ejemplo:**
```
Entrada: "001122334455"
Resultado: "00:11:22:33:44:55"

Entrada: "AABBCCDDEEFF"
Resultado: "AA:BB:CC:DD:EE:FF"
```

---

## 📖 Casos de Uso

### 1. Obtener identificador único de máquina

```
Sistema de licencias:
→ Click [Obtener MACs]
→ Usar primera MAC como identificador único
→ Ejemplo: "00-11-22-33-44-55"
→ Vincular licencia a esa MAC
```

### 2. Resolver DNS para conectarse a servidor

```
Conexión a API remota:
Usuario ingresa: "api.miempresa.com"
→ Click [Obtener IPs]
→ Resultado: "192.168.1.100"
→ Conectar a esa IP
```

### 3. Validar conectividad a servidor

```
Monitoreo de red:
Loop cada 5 minutos:
  → Click [Obtener IPs Async] para "google.com"
  → Si resuelve: Internet activo ✓
  → Si no resuelve: Sin internet ✗
```

### 4. Mostrar información de red en UI

```
Panel de información de sistema:
→ Obtener MACs
→ Mostrar: "MAC address: 00:11:22:33:44:55"
→ Obtener IPs para "localhost"
→ Mostrar: "IP local: 127.0.0.1"
```

---

## 🔍 Detalles Técnicos

### GetLstMac

```csharp
public static List<string> GetLstMac()
{
    // Obtiene todos los adaptadores de red del sistema
    // Retorna sus direcciones MAC
    // Formato: "00-11-22-33-44-55"
}
```

**Implementación:**
```csharp
NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
foreach (var nic in nics)
{
    string mac = nic.GetPhysicalAddress().ToString(); // "001122334455"
    // Formatear a "00-11-22-33-44-55"
}
```

**Retorna:**
- List<string> con MACs de todos los NIC activos/inactivos
- Vacío si no hay adaptadores

---

### GetLstIp

```csharp
public static List<string> GetLstIp(string hostname)
{
    // Resuelve hostname a IPs usando DNS
    // Retorna IPv4 e IPv6
}
```

**Proceso:**
1. `Dns.GetHostAddresses(hostname)`
2. Convierte IPAddress a string
3. Retorna lista

**Excepciones:**
- `ArgumentNullException`: Si hostname es null
- `SocketException`: Si DNS no resuelve

---

### GetLstIpAsync

```csharp
public static async Task<List<string>> GetLstIpAsync(string hostname)
{
    // Versión asíncrona de GetLstIp
    // No bloquea el thread
}
```

**Ventaja:** No congela UI durante resolución DNS

---

### GetMacFormat

```csharp
public static string GetMacFormat(string mac)
{
    // Convierte "001122334455"
    // En "00:11:22:33:44:55"
}
```

**Algoritmo:**
```csharp
StringBuilder result = new StringBuilder();
for (int i = 0; i < mac.Length; i += 2)
{
    result.Append(mac.Substring(i, 2));
    if (i < mac.Length - 2)
        result.Append(":");
}
return result.ToString();
```

---

## ⚠️ Casos Especiales

### Sin adaptadores de red

```
Equipo desconectado o sin NIC:
→ Click [Obtener MACs]
→ Resultado: "No se encontraron direcciones MAC"
```

### Hostname inválido

```
Entrada: "thisdomainnotexist12345.com"
→ Click [Obtener IPs]
→ Resultado: "No se encontraron IPs para: thisdomainnotexist12345.com"
```

### Hostname sin conectividad

```
Entrada: "google.com"
Computadora sin internet:
→ Click [Obtener IPs]
→ Resultado: Timeout o error de red
```

### MAC con formato incorrecto

```
Entrada: "00:11:22:33:44:55" (con ":")
→ Click [Formatear MAC]
→ Resultado: Error o "00::11::22::33::44::55" (doble formato)
Solución: Remover ":" antes
```

### Async timeout

```
Hostname: "algo.dominio.muy.lento.com"
→ Click [Obtener IPs Async]
→ Después de 30 segundos: Timeout
→ Resultado: Error en Panel de Resultados
```

---

## 🧪 Pruebas Sugeridas

1. **Test MACs**: Click [Obtener MACs] → Debe listar al menos una MAC
2. **Test localhost**: Host="localhost" → [Obtener IPs] → Debe incluir 127.0.0.1
3. **Test google**: Host="google.com" → [Obtener IPs] → Debe resolver a IP
4. **Test async**: Host="localhost" → [Obtener IPs Async] → No debe bloquear
5. **Test MAC format**: MAC="001122334455" → Resultado: "00:11:22:33:44:55"
6. **Test MAC mayúsculas**: MAC="AABBCCDDEEFF" → Resultado: "AA:BB:CC:DD:EE:FF"
7. **Test hostname inválido**: Host="invalid123456.xyz" → Error/No encontrado
8. **Test IPv4 e IPv6**: Host="localhost" → Debe incluir ambas
9. **Test hostname vacío**: Host="" → Error requerido
10. **Test duplicados**: Si hay múltiples MACs, todas deben listarse

---

## 🔧 Configuración de Red

### Para testear correctamente:

```
✓ Requisitos:
- Computadora conectada a red (Ethernet o WiFi)
- Adaptador de red activo
- DNS configurado correctamente
- Acceso a Internet (opcional, para test de google.com)

✗ No funcionará en:
- VM sin adaptador de red
- Máquina desconectada
- Firewall bloqueando DNS
```

---

## 📊 Tabla de Resultados Esperados

| Entrada | Función | Resultado Esperado |
|---------|---------|-------------------|
| (ninguna) | GetLstMac | Lista de 1-3 MACs |
| "localhost" | GetLstIp | 127.0.0.1, ::1 |
| "google.com" | GetLstIp | IP pública de Google |
| "localhost" | GetLstIpAsync | Mismo que GetLstIp |
| "001122334455" | GetMacFormat | "00:11:22:33:44:55" |

---

## 🔗 Referencias

- [Clase UtilNetWork](../LibUtilCasc/UtilNetWork.cs)
- [System.Net.NetworkInformation](https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation)
- [RFC 5234 - MAC Address Format](https://tools.ietf.org/html/rfc5234)
- [DNS y resolución de hostnames](https://docs.microsoft.com/en-us/dotnet/api/system.net.dns)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0
