# Configuración AppSettings - LibUtilCasc

## Localización
Archivo: `LibUtilCasc\App.config`

## Claves Disponibles

### 🔐 Raspberry Pi (UtilRaspberry)

```xml
<!-- Credenciales SSH para Raspberry Pi -->
<add key="RaspUsername" value="pi" />
<add key="RaspPassword" value="raspberry" />
```

**Parámetros:**
- `RaspUsername`: Usuario SSH de la Raspberry Pi (default: "pi")
- `RaspPassword`: Contraseña SSH (default: "raspberry")

**Uso en código:**
```csharp
// UtilRaspberry.Init() lee automáticamente estas claves
string resultado = UtilRaspberry.ConectarRaspberry("192.168.1.100", "encenderLed");
```

**⚠️ Seguridad:**
- En producción, considerar usar variables de entorno
- NO commitar contraseñas reales en git
- Usar secrets management (Azure Key Vault, etc.)

---

### 🖼️ Rutas de Imágenes (UtilImage)

```xml
<!-- Ruta base para almacenar/leer imágenes -->
<!-- <add key="ImagePath" value="D:\HTMLFOTOS\ImgEmpleados\" /> -->
```

**Estado:** Actualmente comentado (usa hardcoding en código)

**Para habilitar:**
1. Descomentar la línea
2. Actualizar `UtilImage.GetPathImagen()` para leer desde AppSettings:
```csharp
public static string GetPathImagen(int TipoFoto, string NombreImagen)
{
    string basePath = ConfigurationManager.AppSettings["ImagePath"] ?? "D:\\HTMLFOTOS\\ImgEmpleados\\";
    return basePath + NombreImagen + ".jpg";
}
```

---

## Cómo Acceder en Código

```csharp
using System.Configuration;

// Leer un valor
string valor = ConfigurationManager.AppSettings["RaspUsername"];

// Con valor por defecto si no existe
string usuario = ConfigurationManager.AppSettings["RaspUsername"] ?? "pi";
```

---

## Diferencia entre App.config y web.config

| Tipo | Ubicación | Uso |
|------|-----------|-----|
| `App.config` | Aplicación Console/Desktop | LibUtilCasc (Librería/TestApp) |
| `web.config` | Aplicación Web ASP.NET | Si se usa en WebComedorMascota |

---

## Ejemplos de Configuración Avanzada

### Para Desarrollo
```xml
<add key="RaspUsername" value="pi" />
<add key="RaspPassword" value="raspberry" />
<add key="ImagePath" value="C:\Dev\Images\" />
```

### Para Producción
```xml
<!-- Usar variables de entorno o secrets -->
<add key="RaspUsername" value="${RASP_USER}" />
<add key="RaspPassword" value="${RASP_PASS}" />
```

### Con Ambiente
```xml
<configuration>
  <appSettings file="AppSettings.{Environment}.config">
    <add key="RaspUsername" value="pi" />
  </appSettings>
</configuration>
```

---

## ⚠️ Notas Importantes

1. **AppSettings es de solo lectura en runtime** - No se puede modificar durante la ejecución
2. **Cambios requieren reinicio** de la aplicación
3. **LibUtilCasc es una librería** - El App.config debe estar en el proyecto **consumidor** (AppPrueba, WebComedorMascota, etc.)
4. **NLog.config es separado** - Se configura independientemente en `LibUtilCasc\NLog.config`

---

## Troubleshooting

### ❌ Error: "ConfigurationManager.AppSettings returns null"
**Causa:** App.config no está en el proyecto que ejecuta
**Solución:** Copiar/crear App.config en el proyecto ejecutable (AppPrueba)

### ❌ Error: "AppSettings key not found"
**Causa:** Clave no existe en App.config
**Solución:** Agregar la clave manualmente o usar valor por defecto con `??`

```csharp
string valor = ConfigurationManager.AppSettings["ClaveNoExiste"] ?? "valor_por_defecto";
```

---

## Referencias
- Documentación MS: https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationsettings
- App.config vs web.config: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2008/1x0zf2yc(v=vs.90)
