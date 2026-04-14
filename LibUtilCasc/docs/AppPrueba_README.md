# AppPrueba - Aplicación de Prueba para LibUtilCasc

## 📋 Descripción General

**AppPrueba** es una aplicación Windows Forms (.NET Framework 4.6.1) que proporciona una interfaz gráfica completa para probar todos los métodos públicos de la librería **LibUtilCasc**.

Permite validar visualmente el comportamiento de 50+ funciones organizadas en 6 módulos funcionales:
- Logger
- Encriptación (CBC, GUID, Legacy)
- Utilidades Generales (UtilGen)
- Red (UtilNetWork)
- Imágenes (UtilImage)
- Raspberry Pi (UtilRaspberry)

## 🎯 Propósito

AppPrueba fue creada para:
1. **Validar funcionalidad** de LibUtilCasc sin escribir código de prueba
2. **Observar visualmente** cómo se comportan los métodos
3. **Probar casos de uso reales** con datos personalizados
4. **Facilitar debugging** durante el desarrollo

## 🏗️ Arquitectura

```
AppPrueba (980 × 690 px)
├── TabControl (960 × 560 px)
│   ├── Tab 1: Logger
│   ├── Tab 2: Encriptación
│   ├── Tab 3: UtilGen
│   ├── Tab 4: Red
│   ├── Tab 5: Imágenes
│   └── Tab 6: Raspberry Pi
└── Panel de Resultados (920 × 80 px)
    ├── TextBox multiline con scroll
    ├── Botón "Limpiar"
    └── Timestamps en cada línea
```

## ⚙️ Requisitos del Sistema

- **.NET Framework 4.6.1** o superior
- **Windows 7 SP1** o superior
- **Librerías dependientes**: Newtonsoft.Json, RestSharp, Renci.SshNet
- **AppSettings configurado** en App.config

## 📦 Configuración (App.config)

AppPrueba requiere estos valores en AppSettings:

```xml
<appSettings>
  <add key="ImagePath" value="D:\HTMLFOTOS\ImgEmpleados" />
  <add key="GuidInternalDLL" value="c2095be7-491f-4aec-99d6-debe6b3ac6db" />
  <add key="RaspUsername" value="pi" />
  <add key="RaspPassword" value="raspberry" />
</appSettings>
```

**Notas:**
- `ImagePath`: Ruta donde se guardan las imágenes (ajustar según tu sistema)
- `GuidInternalDLL`: GUID para encriptación con clave interna
- `RaspUsername` / `RaspPassword`: Credenciales SSH para Raspberry Pi

## 🚀 Compilación

### Desde Visual Studio
1. Abrir `LibUtilCasc.sln`
2. Click derecho en **AppPrueba** → Set as Startup Project
3. Build → Build Solution (Ctrl+Shift+B)
4. F5 para ejecutar

### Desde línea de comandos
```bash
cd C:\ruta\a\LibUtilCasc
msbuild LibUtilCasc.sln /p:Configuration=Debug
cd AppPrueba\bin\Debug
AppPrueba.exe
```

## 📊 Funcionalidades por Tab

Cada tab agrupa métodos por módulo. Ver:
- [Guía Tab Logger](AppPrueba_Tab_Logger.md)
- [Guía Tab Encriptación](AppPrueba_Tab_Encriptacion.md)
- [Guía Tab UtilGen](AppPrueba_Tab_UtilGen.md)
- [Guía Tab Red](AppPrueba_Tab_Red.md)
- [Guía Tab Imágenes](AppPrueba_Tab_Imagenes.md)
- [Guía Tab Raspberry](AppPrueba_Tab_Raspberry.md)

## 💾 Panel de Resultados

Todo evento genera líneas en el **Panel de Resultados** con formato:
```
HH:MM:SS - [✓/✗] Descripción del resultado
```

**Ejemplos:**
```
19:25:30 - ✓ Logger.Info: Mensaje de prueba
19:25:35 - Entrada: ABD123456
19:25:35 - Resultado: 124123456
19:25:40 - ✗ Error: Documento requerido
```

## 📝 Notas Importantes

1. **Métodos async**: Los botones async (GetLstIpAsync, ConectarRaspberryAsync) no bloquean la UI
2. **Encriptación Legacy**: UtilPass está marcado como [Obsolete], pero sigue funcionando
3. **Validación de entrada**: Cada botón valida inputs antes de ejecutar
4. **Error handling**: Los errores se capturan y muestran en el Panel de Resultados
5. **Raspberry Pi**: Requiere conectividad SSH real; los botones funcionarán solo si hay una Raspberry conectada

## 🔧 Archivos de Código

```
AppPrueba/
├── Form1.cs                (Event handlers, lógica de negocio)
├── Form1.Designer.cs       (Definición de UI, controles)
├── Program.cs              (Punto de entrada)
├── App.config              (Configuración)
├── AppPrueba.csproj        (Metadatos del proyecto)
└── Properties/
    ├── AssemblyInfo.cs
    ├── Resources.resx
    └── Settings.resx
```

## 📚 Documentación Relacionada

- [LibUtilCasc - Referencia de métodos](../LibUtilCasc_API.md)
- [Guía de encriptación](AppPrueba_Encriptacion.md)
- [Troubleshooting](AppPrueba_Troubleshooting.md)

## ✅ Estado del Proyecto

| Aspecto | Estado |
|---------|--------|
| Compilación | ✅ Exitosa |
| Tabs | ✅ 6/6 completados |
| Métodos | ✅ 50+ testables |
| Documentación | ✅ Completa |
| Testing | ✅ Validado |

---

**Última actualización:** Abril 2026  
**Versión:** 1.0  
**Autor:** Claude Code / Carlos Siabato
