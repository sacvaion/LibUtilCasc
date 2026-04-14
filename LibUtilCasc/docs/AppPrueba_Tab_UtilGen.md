# Tab UtilGen - Guía de Uso

## 📋 Descripción

La pestaña **UtilGen** permite probar 8 funciones utilitarias generales de la clase `UtilGen`:
- Extracción de números desde texto alfanumérico
- Identificación de tipos de placas vehiculares
- Parsing y formateo de fechas
- Generación de IDs de transacción
- Consulta de configuración
- Serialización JSON

## 🎮 Controles

### 1. Extraer Numérico

```
┌────────────────────────────────────────────────┐
│ Documento: [ABD123456              ]           │
│            [Extraer Numérico]                  │
└────────────────────────────────────────────────┘
```

**Entrada:** TextBox con valor default "ABD123456"

**Proceso:**
- Extrae números: 123456
- Convierte letras a posición en alfabeto: A=1, B=2, D=4
- Resultado: 124123456 (concatenado)

**Ejemplo:**
```
Entrada: "ABD123456"
         A=1, B=2, D=4, 123456
Resultado: 124123456 (tipo decimal)

Entrada: "XYZ"
         X=24, Y=25, Z=26
Resultado: 242526
```

---

### 2. Tipo de Vehículo (Placa)

```
┌────────────────────────────────────────────────┐
│ Placa: [ABC123                 ]               │
│        [Tipo Vehículo]                         │
└────────────────────────────────────────────────┘
```

**Entrada:** TextBox con valor default "ABC123" (6 caracteres)

**Tipos de placa:**
| Tipo | Formato | Ejemplo | Resultado |
|------|---------|---------|-----------|
| 1 | AAA### (3 letras + 3 números) | ABC123 | 1 |
| 2 | AAA## (3 letras + 2 números) | ABC12 | 2 |
| 0 | Inválido | XYZ | 0 |

**Ejemplo:**
```
Entrada: "ABC123" → Tipo 1 (AAA###)
Entrada: "ABC12"  → Tipo 2 (AAA##)
Entrada: "XYZ"    → Tipo 0 (inválido)
```

---

### 3. Parsear Fecha

```
┌────────────────────────────────────────────────────────┐
│ Fecha: [01/01/2025 12:00:00 PM              ]         │
│        [Parsear Fecha] [Fecha Actual]                  │
└────────────────────────────────────────────────────────┘
```

**Entrada:**
- TextBox con default: "01/01/2025 12:00:00 PM"
- Formato: `dd/MM/yyyy HH:mm:ss tt` (AM/PM)

**Botones:**
| Botón | Función |
|-------|---------|
| **Parsear Fecha** | Convierte string a DateTime |
| **Fecha Actual** | Formatea DateTime.Now |

**Resultado:**
```
Entrada: "01/01/2025 12:00:00 PM"
Resultado: 2025-01-01 12:00:00
          Año: 2025, Mes: 1, Día: 1

Entrada vacío: Retorna DateTime.Now

Entrada inválida: Retorna DateTime.MinValue
```

---

### 4. ID Transacción

```
┌────────────────────────────────────────────────┐
│ [Crear ID Transacción]                         │
└────────────────────────────────────────────────┘
```

**Entrada:** Usa DateTime.Now automáticamente

**Proceso:**
1. Obtiene último segmento de MAC address (4 caracteres)
2. Convierte fecha/hora a hexadecimal
3. Concatena: MAC + DateHex

**Ejemplo:**
```
MAC: 001122334455
     Últimos 4: 4455

Hora: 2025-04-13 19:25:30 → 250413192530 (YYMMDDHHMMSS)
      250413192530 en hex: 3A37FB588

Resultado: 44553A37FB588
```

---

### 5. GetDate

```
┌────────────────────────────────────────────────┐
│ [GetDate]                                      │
└────────────────────────────────────────────────┘
```

**Entrada:** Usa DateTime.Now automáticamente

**Formato:** YYMMDDHHMMSS (12 caracteres)

**Ejemplo:**
```
Hora: 2025-04-13 19:25:30
Resultado: "250413192530"
          YY=25, MM=04, DD=13, HH=19, MM=25, SS=30
```

---

### 6. Obtener Key AppSettings

```
┌────────────────────────────────────────────────┐
│ Key AppSettings: [ImagePath              ]    │
│                  [Obtener Key]                │
└────────────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "ImagePath"
- Nombre de la clave en App.config

**Keys disponibles:**
```xml
<appSettings>
  <add key="ImagePath" value="D:\HTMLFOTOS\ImgEmpleados" />
  <add key="GuidInternalDLL" value="c2095be7-491f-4aec-99d6-debe6b3ac6db" />
  <add key="RaspUsername" value="pi" />
  <add key="RaspPassword" value="raspberry" />
</appSettings>
```

**Ejemplo:**
```
Key: "ImagePath"
Resultado: "D:\HTMLFOTOS\ImgEmpleados"

Key: "GuidInternalDLL"
Resultado: "c2095be7-491f-4aec-99d6-debe6b3ac6db"

Key inexistente: ""
```

---

### 7. Serializar JSON

```
┌────────────────────────────────────────────────┐
│ [Serializar JSON]                              │
└────────────────────────────────────────────────┘
```

**Entrada:** Automático - objeto `{ Name="Carlos", Year=2025, Company="UtilCasc" }`

**Proceso:** Serializa objeto a JSON usando Newtonsoft.Json

**Resultado:**
```json
{
  "Name": "Carlos",
  "Year": 2025,
  "Company": "UtilCasc"
}
```

---

## 📖 Casos de Uso

### 1. Validar cédula/documento

```
Entrada: "1234567890A"
         (Documento colombiano)

→ Click [Extraer Numérico]
→ Resultado: 1234567890 + A(1) = 12345678901
→ Usar para validaciones o checksum
```

### 2. Identificar tipo de placa vehicular

```
Sistema de tránsito:
- Usuario escanea placa
- Click [Tipo Vehículo]
- Si Tipo=1 → Vehículo particular
- Si Tipo=2 → Moto
- Si Tipo=0 → Error, reintentar
```

### 3. Convertir fecha de usuario a DateTime

```
Usuario ingresa: "25/12/2024 03:30:00 PM"
→ Click [Parsear Fecha]
→ Resultado: 2024-12-25 15:30:00
→ Usar en operaciones de base de datos
```

### 4. Generar referencia única de transacción

```
Transacción bancaria:
→ Click [Crear ID Transacción]
→ Resultado: "44553A37FB588"
→ Usar como referencia única en el sistema
```

### 5. Consultar ruta de imágenes

```
Necesito ruta de imágenes:
Key: "ImagePath"
→ Click [Obtener Key]
→ Resultado: "D:\HTMLFOTOS\ImgEmpleados"
→ Usar para cargar fotos
```

---

## 🔍 Detalles Técnicos

### ExtraerNumerico

```csharp
public static decimal ExtraerNumerico(string VNoDocumento, string idTipoDocumento)
{
    // Extrae dígitos: "123456"
    // Convierte letras: A→1, B→2, ..., Z→26
    // Concatena: "A"="1", "B"="2", "D"="4"
    // Resultado: "1" + "2" + "4" + "123456" = "124123456"
    // Retorna: decimal 124123456
}
```

**Limitaciones:**
- Solo soporta letras mayúsculas (convierte automáticamente)
- Máximo 38 dígitos (límite de decimal en C#)
- Caracteres no alfanuméricos se ignoran

### GetVehicleType

```csharp
public static int GetVehicleType(string sPlate)
{
    // Longitud 6: "ABC123" → Tipo 1
    // Longitud 5: "ABC12" → Tipo 2
    // Otros: → Tipo 0
}
```

**Validaciones:**
- Primeros 3 caracteres deben ser letras
- Posiciones 4-6 deben ser números
- No soporta placas con caracteres especiales

### CreateIdTransaction

```csharp
public static string CreateIdTransaction(DateTime date)
{
    // 1. Obtiene lista de MACs del sistema
    // 2. Toma la primera disponible
    // 3. Extrae últimos 4 caracteres: "4455"
    // 4. Formatea fecha: "250413192530"
    // 5. Convierte a hex: "3A37FB588"
    // 6. Concatena: "44553A37FB588"
}
```

**Falla si:**
- No hay MACs en el sistema
- DateTime es DateTime.MinValue

---

## ⚠️ Casos Especiales

### Documento muy largo

```
Entrada: "A" * 50 + "123456"
Resultado: 50 repeticiones de 1 + 123456 = muy largo
Limitación: decimal máximo ≈ 7.9 × 10^28
```

### Placa con caracteres especiales

```
Entrada: "ABC-123"
Resultado: Tipo 0 (inválido)
Solución: Remover guiones primero
```

### Fecha con formato incorrecto

```
Entrada: "2025-04-13" (ISO format)
Resultado: DateTime.MinValue (formato no esperado)
Esperado: "13/04/2025 12:00:00 PM"
```

### Key no existe

```
Key: "InvalidKey"
Resultado: "" (string vacío)
Comportamiento: Retorna empty en lugar de null
```

---

## 🧪 Pruebas Sugeridas

1. **ExtraerNumerico**: "XYZ123" → Resultado: 242526123
2. **GetVehicleType Tipo1**: "ABC123" → Resultado: 1
3. **GetVehicleType Tipo2**: "ABC12" → Resultado: 2
4. **GetVehicleType Inválido**: "XYZ" → Resultado: 0
5. **FechaFormato válida**: "01/01/2025 12:00:00 PM" → Parsea correctamente
6. **FechaFormato inválida**: "invalid" → DateTime.MinValue
7. **CreateIdTransaction**: Click → Genera ID único con 12+ caracteres
8. **GetDate**: Click → Formato YYMMDDHHMMSS (12 chars)
9. **GetKey existente**: "ImagePath" → Retorna valor del config
10. **ConvertJson**: Click → JSON formateado y válido

---

## 🔗 Referencias

- [Clase UtilGen](../LibUtilCasc/UtilGen.cs)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [Formato de fechas .NET](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0
