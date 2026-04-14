# 📚 Documentación de LibUtilCasc

Bienvenido a la documentación completa del proyecto **LibUtilCasc** - una librería de utilidades .NET para encriptación, networking, procesamiento de imágenes y más.

## 📋 Tabla de Contenidos

### 1. AppPrueba - Aplicación de Prueba GUI

**AppPrueba** es una interfaz gráfica que permite probar todos los métodos de LibUtilCasc sin escribir código.

#### Guías Principales
- [📖 README AppPrueba](AppPrueba_README.md) - Descripción general, requisitos, compilación
- [📊 Arquitectura](AppPrueba_README.md#-arquitectura) - Estructura de ventanas y controles

#### Guías por Módulo (6 Tabs)

| Pestaña | Descripción | Guía |
|---------|-------------|------|
| **Logger** | Logging y manipulación de texto | [Tab Logger](AppPrueba_Tab_Logger.md) |
| **Encriptación** | CBC, GUID, Legacy (ECB) | [Tab Encriptación](AppPrueba_Tab_Encriptacion.md) |
| **UtilGen** | Utilidades generales (7 métodos) | [Tab UtilGen](AppPrueba_Tab_UtilGen.md) |
| **Red** | Networking: MACs, IPs, hostnames | [Tab Red](AppPrueba_Tab_Red.md) |
| **Imágenes** | Conversión Hex↔Bytes, carga/preview | [Tab Imágenes](AppPrueba_Tab_Imagenes.md) |
| **Raspberry Pi** | Control remoto por SSH | [Tab Raspberry](AppPrueba_Tab_Raspberry.md) |

---

## 🚀 Guía Rápida

### Para Principiantes

1. Leer [AppPrueba_README.md](AppPrueba_README.md) (5 min)
2. Compilar el proyecto (Visual Studio → Build)
3. Ejecutar AppPrueba.exe
4. Explorar cada tab (10 min por tab)
5. Leer la guía específica del tab que te interese

### Para Desarrolladores

1. Revisar [Arquitectura](AppPrueba_README.md#-arquitectura)
2. Leer [Form1.cs](../AppPrueba/Form1.cs) (event handlers)
3. Leer [Form1.Designer.cs](../AppPrueba/Form1.Designer.cs) (controles UI)
4. Revisar [App.config](../AppPrueba/App.config) (configuración)
5. Revisar [LibUtilCasc/](../LibUtilCasc/) (código fuente)

---

## 📱 Función de Cada Tab

### 1️⃣ Logger
- ✅ Registro de mensajes (Info, Warn, Error)
- ✅ Remover acentos de texto
- 📝 [Guía completa](AppPrueba_Tab_Logger.md)

### 2️⃣ Encriptación
- ✅ Encriptación CBC (moderno, seguro)
- ✅ Encriptación con GUID interno
- ✅ Legacy ECB (backward compatibility)
- 📝 [Guía completa](AppPrueba_Tab_Encriptacion.md)

### 3️⃣ UtilGen
- ✅ Extraer números de texto alfanumérico
- ✅ Identificar tipos de placas vehiculares
- ✅ Parser de fechas
- ✅ Generación de IDs únicos
- ✅ Consulta de configuración
- ✅ Serialización JSON
- 📝 [Guía completa](AppPrueba_Tab_UtilGen.md)

### 4️⃣ Red
- ✅ Obtener MACs del sistema
- ✅ Resolver hostnames a IPs (sync + async)
- ✅ Formatear direcciones MAC
- 📝 [Guía completa](AppPrueba_Tab_Red.md)

### 5️⃣ Imágenes
- ✅ Conversión Hex ↔ Bytes
- ✅ Resolución de rutas de imágenes
- ✅ Carga y preview de imágenes
- ✅ Conversión Imagen → Bytes (JPEG/PNG)
- 📝 [Guía completa](AppPrueba_Tab_Imagenes.md)

### 6️⃣ Raspberry Pi
- ✅ Control remoto por SSH
- ✅ Ejecución de comandos (sync + async)
- ✅ Gestión de LEDs y motores
- ⚠️ Requiere Raspberry Pi disponible
- 📝 [Guía completa](AppPrueba_Tab_Raspberry.md)

---

## 🔍 Búsqueda por Característica

### Si quieres encriptar datos
→ Ve a [Tab Encriptación](AppPrueba_Tab_Encriptacion.md)
- Recomendado: CBC mode
- Alternativo: GUID mode para datos internos

### Si quieres procesar imágenes
→ Ve a [Tab Imágenes](AppPrueba_Tab_Imagenes.md)
- Convierte imagen a bytes
- Preview de imágenes
- Soporte JPEG y PNG

### Si quieres conectarte a red
→ Ve a [Tab Red](AppPrueba_Tab_Red.md)
- Obtener MACs (identificador único)
- Resolver IPs (conectar a servidores)
- Async para no bloquear UI

### Si quieres trabajar con texto
→ Ve a [Tab Logger](AppPrueba_Tab_Logger.md) + [Tab UtilGen](AppPrueba_Tab_UtilGen.md)
- Logger: Registro y normalización
- UtilGen: Parsing, validación, formateo

### Si tienes Raspberry Pi
→ Ve a [Tab Raspberry](AppPrueba_Tab_Raspberry.md)
- Control de LEDs y motores
- Ejecución remota de comandos
- SSH automatizado

---

## 📚 Referencia Rápida de Métodos

### Logger
```csharp
Logger.Info(string)          // Registrar Info
Logger.Warn(string)          // Registrar Warning
Logger.Error(string)         // Registrar Error
Logger.RemoverSignosAcentos(string) → string
```

### Encriptación
```csharp
UtilEncription.Encriptar(msg, key) → string // CBC
UtilEncription.Decriptar(cipher, key) → string // Auto-detect
UtilEncription.EncriptarGuidInternal(msg) → string
UtilEncription.DecriptarGuidInternal(cipher) → string
UtilPass.Encriptar(msg) → string // Legacy, obsoleto
UtilPass.DesEncriptar(cipher) → string // Legacy, obsoleto
```

### UtilGen
```csharp
UtilGen.ExtraerNumerico(doc, type) → decimal
UtilGen.GetVehicleType(placa) → int (0=invalid, 1=AAA###, 2=AAA##)
UtilGen.FechaFormato(fecha) → DateTime
UtilGen.DateToString(date) → string
UtilGen.CreateIdTransaction(date) → string
UtilGen.GetDate(date) → string // YYMMDDHHMMSS
UtilGen.GetKey(key) → string // AppSettings
UtilGen.ConvertJson(obj) → string
```

### UtilNetWork
```csharp
UtilNetWork.GetLstMac() → List<string>
UtilNetWork.GetLstIp(hostname) → List<string>
UtilNetWork.GetLstIpAsync(hostname) → Task<List<string>>
UtilNetWork.GetMacFormat(mac) → string
```

### UtilImage
```csharp
UtilImage.Hex2Bin(hex) → byte[]
UtilImage.Bin2HexUpper(bytes) → string
UtilImage.GetPathImagen(id, nombre) → string
UtilImage.ImageToByteArray(image) → byte[] // JPEG
UtilImage.ImageToByteArrayPNG(image) → byte[] // PNG
```

### UtilRaspberry
```csharp
UtilRaspberry.ConectarRaspberry(ip, cmd) → string
UtilRaspberry.ConectarRaspberryAsync(ip, cmd) → Task<string>
UtilRaspberry.Init() → void
```

---

## ⚙️ Requisitos del Sistema

```
.NET Framework:      4.6.1 o superior
Sistema operativo:   Windows 7 SP1 o superior
Dependencias:        
  - Newtonsoft.Json 13.0.3
  - RestSharp 114.0.0
  - Renci.SshNet (para Raspberry Pi)
  - NLog (logging)
```

---

## 🛠️ Compilación y Ejecución

### Desde Visual Studio
```
1. Abrir LibUtilCasc.sln
2. Click derecho en AppPrueba → Set as Startup Project
3. Build → Build Solution (Ctrl+Shift+B)
4. F5 para ejecutar
```

### Desde línea de comandos
```bash
cd C:\ruta\a\LibUtilCasc
msbuild LibUtilCasc.sln /p:Configuration=Debug
cd AppPrueba\bin\Debug
AppPrueba.exe
```

---

## 📝 Notas Importantes

⚠️ **Encriptación Legacy**
- UtilPass (ECB) está marcado como [Obsolete]
- Usar UtilEncription (CBC) para nuevo código
- UtilPass solo para leer datos antiguos

⚠️ **Raspberry Pi**
- Opcional - AppPrueba funciona sin él
- Requiere SSH habilitado
- Requiere estructura de carpetas específica

⚠️ **AppSettings**
- Modificar App.config con valores reales
- ImagePath: Ajustar según tu sistema
- RaspUsername/Password: Cambiar si no son default

---

## 🔗 Índice de Archivos

```
LibUtilCasc/
├── docs/                           (Esta documentación)
│   ├── README.md                   (Este archivo)
│   ├── AppPrueba_README.md         (General AppPrueba)
│   ├── AppPrueba_Tab_Logger.md     (Tab 1)
│   ├── AppPrueba_Tab_Encriptacion.md (Tab 2)
│   ├── AppPrueba_Tab_UtilGen.md    (Tab 3)
│   ├── AppPrueba_Tab_Red.md        (Tab 4)
│   ├── AppPrueba_Tab_Imagenes.md   (Tab 5)
│   └── AppPrueba_Tab_Raspberry.md  (Tab 6)
│
├── LibUtilCasc/                    (Librería principal)
│   ├── Logger.cs
│   ├── UtilEncription.cs
│   ├── UtilGen.cs
│   ├── UtilNetWork.cs
│   ├── UtilImage.cs
│   ├── UtilPass.cs                 (Legacy)
│   ├── UtilRaspberry.cs
│   └── ...
│
└── AppPrueba/                      (Aplicación GUI)
    ├── Form1.cs                    (Event handlers)
    ├── Form1.Designer.cs           (Definición UI)
    ├── App.config                  (Configuración)
    ├── Program.cs                  (Punto entrada)
    └── ...
```

---

## 🎯 Próximos Pasos

1. **Principiante**: Leer [AppPrueba_README.md](AppPrueba_README.md)
2. **Usuario**: Explorar cada tab siguiendo sus guías
3. **Desarrollador**: Revisar código fuente en LibUtilCasc/
4. **Implementación**: Usar métodos en tu propio código

---

## 📞 Soporte

Para problemas técnicos:
1. Revisar la guía específica del tab
2. Verificar configuración en App.config
3. Revisar ejemplos en la sección "Casos de Uso"
4. Revisar "Casos Especiales" para problemas comunes

---

## 📊 Estadísticas del Proyecto

| Métrica | Valor |
|---------|-------|
| Métodos públicos | 50+ |
| Tabs funcionales | 6 |
| Documentación | 8 archivos |
| Líneas de código | 2,500+ |
| Tests unitarios | 22+ |
| Backward compatibility | 100% |

---

## 📄 Historial de Documentación

| Versión | Fecha | Cambios |
|---------|-------|---------|
| 1.0 | Abril 2026 | Documentación inicial completa |

---

**Última actualización:** Abril 2026  
**Versión:** 1.0  
**Autor:** Claude Code / Carlos Siabato

---

## 🎓 Créditos

- **Librería:** LibUtilCasc v2.0
- **Aplicación:** AppPrueba v1.0
- **Documentación:** Claude Code

¡Gracias por usar LibUtilCasc! 🙏
