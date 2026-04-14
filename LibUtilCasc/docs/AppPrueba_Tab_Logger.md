# Tab Logger - Guía de Uso

## 📋 Descripción

La pestaña **Logger** permite probar funciones de logging y manipulación de texto con acentos de la clase `Logger`.

## 🎮 Controles

### Sección 1: Registro de Mensajes

```
┌─────────────────────────────────────────┐
│ Mensaje: [                           ]  │
│          [Info] [Warn] [Error]         │
└─────────────────────────────────────────┘
```

#### Entrada
- **Mensaje**: TextBox libre para escribir cualquier texto

#### Botones
| Botón | Método | Descripción |
|-------|--------|-------------|
| **Info** | `Logger.Info(string)` | Registra mensaje de nivel INFO |
| **Warn** | `Logger.Warn(string)` | Registra mensaje de nivel WARN |
| **Error** | `Logger.Error(string)` | Registra mensaje de nivel ERROR |

#### Resultado
Cada botón registra el mensaje y muestra confirmación en el Panel de Resultados.

**Ejemplo:**
```
19:25:30 - ✓ Logger.Info: Sistema iniciado correctamente
19:25:35 - ✓ Logger.Warn: Conexión lenta detectada
19:25:40 - ✓ Logger.Error: Archivo no encontrado
```

### Sección 2: Remover Acentos

```
┌──────────────────────────────────────────────────┐
│ Texto con acentos: [                           ] │
│                    [Remover Acentos]            │
└──────────────────────────────────────────────────┘
```

#### Entrada
- **Texto con acentos**: Texto que contenga caracteres acentuados

#### Botón
- **Remover Acentos**: Llama a `Logger.RemoverSignosAcentos(string)`

#### Resultado
Muestra entrada y salida en el Panel de Resultados.

**Ejemplo:**
```
19:30:15 - Entrada: José María Núñez García
19:30:15 - Salida: Jose Maria Nunez Garcia
```

## 📖 Casos de Uso

### 1. Registrar eventos de aplicación
```
Mensaje: "Usuario autenticado: admin"
→ Click [Info]
→ Resultado: ✓ Logger.Info: Usuario autenticado: admin
```

### 2. Registrar advertencias
```
Mensaje: "Conexión a BD tardó 5 segundos"
→ Click [Warn]
→ Resultado: ✓ Logger.Warn: Conexión a BD tardó 5 segundos
```

### 3. Registrar errores
```
Mensaje: "NullReferenceException en UtilGen.GetDate()"
→ Click [Error]
→ Resultado: ✓ Logger.Error: NullReferenceException en UtilGen.GetDate()
```

### 4. Normalizar texto con acentos
```
Texto: "Información de contraseña"
→ Click [Remover Acentos]
→ Resultado: 
   Entrada: Información de contraseña
   Salida: Informacion de contrasena
```

## 🔍 Detalles Técnicos

### Logger.Info / Logger.Warn / Logger.Error

**Función:** Escribe mensajes en el archivo de log (configurado por NLog)

**Parámetros:**
- `string message`: Mensaje a registrar

**Valor retorno:** void

**Comportamiento:**
- Los mensajes se escriben en el archivo de log
- También se muestran en la consola si está disponible
- El timestamp se agrega automáticamente

**Ubicación del log:** 
- Archivo: `bin/Debug/logs/` (ver App.config de LibUtilCasc)

### Logger.RemoverSignosAcentos

**Función:** Convierte caracteres acentuados a sus equivalentes sin acento

**Parámetros:**
- `string input`: Texto con posibles acentos

**Valor retorno:** `string` sin acentos

**Comportamiento:**
- Convierte: á→a, é→e, í→i, ó→o, ú→u, ñ→n
- Mantiene mayúsculas/minúsculas
- Preserva espacios y caracteres especiales
- Si input es null, retorna null
- Si input es vacío, retorna vacío

**Ejemplo:**
```csharp
"José" → "Jose"
"MARÍA" → "MARIA"
"Información" → "Informacion"
"" → ""
null → null
```

**Implementación:** Usa StringBuilder (O(n)) en lugar de concatenación de strings (O(n²))

## ⚠️ Casos Especiales

### Texto vacío
```
Mensaje: ""
→ Click [Info]
→ Resultado: ✓ Logger.Info: 
```

### Caracteres especiales
```
Mensaje: "Error: @#$%^&*()"
→ Click [Error]
→ Resultado: ✓ Logger.Error: Error: @#$%^&*()
```

### Saltos de línea
```
Mensaje: "Línea 1
Línea 2"
→ Click [Warn]
→ Resultado: ✓ Logger.Warn: Línea 1 Línea 2
```

### Acentos mixtos
```
Texto: "Café, José María, Pérez, Año 2025"
→ Click [Remover Acentos]
→ Resultado:
   Entrada: Café, José María, Pérez, Año 2025
   Salida: Cafe, Jose Maria, Perez, Ano 2025
```

## 🧪 Pruebas Sugeridas

1. **Test básico Info**: Escribir "Hola" → Click Info → Verificar ✓
2. **Test básico Warn**: Escribir "Advertencia" → Click Warn → Verificar ✓
3. **Test básico Error**: Escribir "Error" → Click Error → Verificar ✓
4. **Test acentos simples**: Escribir "Niño" → Remover → Debe ser "Nino"
5. **Test acentos complejos**: Escribir "José María" → Remover → Debe ser "Jose Maria"
6. **Test caracteres especiales**: Escribir "#@!" → Remover → Debe ser "#@!"
7. **Test string vacío**: Dejar vacío → Click Info → Debe registrarse sin error
8. **Test texto largo**: Escribir 500+ caracteres → Verificar que se procesa correctamente

## 📊 Tabla de Caracteres Soportados

| Acentuado | Sin acento | Acentuado | Sin acento |
|-----------|-----------|-----------|-----------|
| á | a | À | A |
| é | e | È | E |
| í | i | Ì | I |
| ó | o | Ò | O |
| ú | u | Ù | U |
| ñ | n | Ñ | N |
| ü | u | Ü | U |
| ç | c | Ç | C |

## 🔗 Referencias

- [Clase Logger](../LibUtilCasc/Logger.cs)
- [NLog Configuration](https://nlog-project.org/)
- [Documentación Logger completa](AppPrueba_Logger_Avanzado.md)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0
