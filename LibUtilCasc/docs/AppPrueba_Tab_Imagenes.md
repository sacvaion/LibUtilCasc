# Tab Imágenes - Guía de Uso

## 📋 Descripción

La pestaña **Imágenes** permite probar funciones de manipulación de imágenes y conversión hex/bytes de la clase `UtilImage`:
- Conversión hexadecimal ↔ bytes
- Resolución de rutas de imágenes
- Carga de imágenes con preview
- Conversión imagen → bytes (JPEG y PNG)

## 🎮 Controles

### 1. Conversión Hex → Bytes

```
┌────────────────────────────────────────────────┐
│ Hex: [AABBCCDD                 ]               │
│      [Hex → Bytes]                             │
└────────────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "AABBCCDD"
- Valores hexadecimales (0-9, A-F)
- Longitud DEBE ser par (2 caracteres = 1 byte)

**Función:** `UtilImage.Hex2Bin(hex)`

**Proceso:**
- "AA" → 0xAA (170 decimal)
- "BB" → 0xBB (187 decimal)
- "CC" → 0xCC (204 decimal)
- "DD" → 0xDD (221 decimal)
- Resultado: array [170, 187, 204, 221]

**Ejemplo:**
```
Entrada: "AABBCCDD"
Resultado: Bytes (4): [AA][BB][CC][DD]
```

**Almacenamiento:** Los bytes se guardan en variable `currentImageBytes` para usar en siguientes operaciones.

---

### 2. Conversión Bytes → Hex Upper

```
┌────────────────────────────────────────────────┐
│ [Bytes → Hex Upper]                            │
└────────────────────────────────────────────────┘
```

**Entrada:** Usa `currentImageBytes` del paso anterior

**Función:** `UtilImage.Bin2HexUpper(bytes)`

**Proceso:** Convierte array de bytes a string hexadecimal mayúscula

**Ejemplo:**
```
Entrada bytes: [170, 187, 204, 221]
Resultado: "AABBCCDD"
```

---

### 3. Obtener Ruta de Imagen

```
┌────────────────────────────────────────────────┐
│ Nombre imagen: [test_image              ]     │
│                [Obtener Ruta]                  │
└────────────────────────────────────────────────┘
```

**Entrada:**
- TextBox default: "test_image"
- Nombre del archivo sin extensión

**Función:** `UtilImage.GetPathImagen(0, nombre)`

**Proceso:**
1. Lee `ImagePath` desde App.config
2. Agrega ".jpg" al nombre
3. Retorna ruta completa

**Ejemplo:**
```
Entrada: "test_image"
ImagePath: "D:\HTMLFOTOS\ImgEmpleados"
Resultado: "D:\HTMLFOTOS\ImgEmpleados\test_image.jpg"
```

---

### 4. Cargar Imagen

```
┌────────────────────────────────────────────────┐
│ [Cargar Imagen]                                │
│ ┌──────────────────────────────────────────┐  │
│ │                                          │  │
│ │        PictureBox Preview (300x250)      │  │
│ │                                          │  │
│ └──────────────────────────────────────────┘  │
└────────────────────────────────────────────────┘
```

**Entrada:** OpenFileDialog (abre navegador de archivos)

**Filtros soportados:**
- .jpg, .jpeg
- .png
- .bmp

**Resultado:**
- Imagen se muestra en PictureBox (zoom automático)
- Bytes se guardan en `currentImageBytes`
- Tamaño en bytes se muestra en Panel de Resultados

**Ejemplo:**
```
Selecciona: "C:\Users\Carlos\Pictures\foto.jpg"
→ Imagen aparece en PictureBox
→ "✓ Imagen cargada: C:\Users\Carlos\Pictures\foto.jpg (245,632 bytes)"
```

---

### 5. Imagen → Bytes (JPEG)

```
┌────────────────────────────────────────────────┐
│ [Image → Bytes (JPEG)]                         │
└────────────────────────────────────────────────┘
```

**Entrada:** `picBox.Image` (imagen cargada en PictureBox)

**Función:** `UtilImage.ImageToByteArray(image)`

**Proceso:**
1. Convierte Image a MemoryStream
2. Guarda como JPEG (compresión estándar)
3. Retorna array de bytes

**Resultado:** Tamaño en bytes del JPEG

**Ejemplo:**
```
Imagen: foto.png (original 500KB)
→ Click [Image → Bytes (JPEG)]
→ Resultado: "Image → Bytes (JPEG): 125,400 bytes"
   (Compresión JPEG reduce tamaño)
```

---

### 6. Imagen → Bytes (PNG)

```
┌────────────────────────────────────────────────┐
│ [Image → Bytes (PNG)]                          │
└────────────────────────────────────────────────┘
```

**Entrada:** `picBox.Image` (imagen cargada)

**Función:** `UtilImage.ImageToByteArrayPNG(image)`

**Proceso:**
1. Convierte Image a MemoryStream
2. Guarda como PNG (sin pérdida)
3. Retorna array de bytes

**Resultado:** Tamaño en bytes del PNG

**Ejemplo:**
```
Imagen: foto.jpg (original 150KB)
→ Click [Image → Bytes (PNG)]
→ Resultado: "Image → Bytes (PNG): 180,256 bytes"
   (PNG sin pérdida puede ser más grande)
```

---

## 📖 Casos de Uso

### 1. Validar formato de bytes

```
Datos binarios recibidos por red:
Data: "48656C6C6F" (hex)

→ Click [Hex → Bytes]
→ Resultado: [72, 101, 108, 108, 111]
→ Convertir a string: "Hello"
```

### 2. Guardar imagen en base de datos

```
Usuario carga foto:
→ Click [Cargar Imagen]
→ Selecciona: "mi_foto.jpg"
→ Click [Image → Bytes (JPEG)]
→ Resultado: array de bytes
→ Guardar en BD como BLOB
```

### 3. Enviar imagen por red

```
Necesito enviar foto por HTTP:
→ Click [Cargar Imagen]
→ Click [Image → Bytes (JPEG)]
→ Codificar bytes a Base64
→ Enviar en JSON
```

### 4. Generar thumbnail

```
Sistema de galería:
→ Cargar imagen original
→ Convertir a bytes JPEG (compresión)
→ Guardar versión pequeña en caché
```

### 5. Validar integridad de datos

```
Descargué archivo.bin por FTP:
Content: "0F1E2D3C4B5A69789AB"

→ Click [Hex → Bytes]
→ Verificar checksum con primeros bytes
→ Confirmar que descarga fue correcta
```

---

## 🔍 Detalles Técnicos

### Hex2Bin

```csharp
public static byte[] Hex2Bin(string hex)
{
    // Convierte pares de caracteres hex a bytes
    // "AA" = 0xAA = 170
    // "BB" = 0xBB = 187
}
```

**Algoritmo:**
```csharp
byte[] bytes = new byte[hex.Length / 2];
for (int i = 0; i < hex.Length; i += 2)
{
    string byteString = hex.Substring(i, 2);
    bytes[i / 2] = Convert.ToByte(byteString, 16);
}
return bytes;
```

**Validaciones:**
- Longitud DEBE ser par
- Solo caracteres 0-9, A-F, a-f válidos

---

### Bin2HexUpper

```csharp
public static string Bin2HexUpper(byte[] data)
{
    // Convierte array de bytes a string hex mayúscula
    // 170 = 0xAA = "AA"
}
```

**Algoritmo:**
```csharp
StringBuilder hex = new StringBuilder(data.Length * 2);
foreach (byte b in data)
{
    hex.Append(b.ToString("X2")); // "X2" = mayúscula, 2 dígitos
}
return hex.ToString();
```

---

### GetPathImagen

```csharp
public static string GetPathImagen(int idEmpleado, string nombreImagen)
{
    // Retorna: AppSettings["ImagePath"] + "/" + nombreImagen + ".jpg"
    string imagePath = ConfigurationManager.AppSettings["ImagePath"];
    return Path.Combine(imagePath, nombreImagen + ".jpg");
}
```

---

### ImageToByteArray

```csharp
public static byte[] ImageToByteArray(Image img)
{
    // Convierte Image a bytes en formato JPEG
    using (MemoryStream ms = new MemoryStream())
    {
        img.Save(ms, ImageFormat.Jpeg);
        return ms.ToArray();
    }
}
```

**Parámetros de compresión:** Estándar (calidad media)

---

### ImageToByteArrayPNG

```csharp
public static byte[] ImageToByteArrayPNG(Image img)
{
    // Convierte Image a bytes en formato PNG (sin pérdida)
    using (MemoryStream ms = new MemoryStream())
    {
        img.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}
```

**Ventaja:** Sin pérdida de calidad

---

## ⚠️ Casos Especiales

### Hex con longitud impar

```
Entrada: "ABC" (3 caracteres, impar)
→ Click [Hex → Bytes]
→ Resultado: Array vacío (error)
Solución: Agregar 0 inicial: "0ABC"
```

### Hex con letras minúsculas

```
Entrada: "aabbccdd" (minúsculas)
→ Click [Hex → Bytes]
→ Resultado: [0xAA, 0xBB, 0xCC, 0xDD] (funciona igual)
Salida [Bytes → Hex Upper]: "AABBCCDD" (mayúsculas)
```

### Sin imagen cargada

```
→ Click [Image → Bytes (JPEG)]
→ Resultado: "✗ Error: Debe cargar una imagen primero"
Solución: Click [Cargar Imagen] primero
```

### Imagen muy grande

```
Imagen: 10,000 x 10,000 pixels (100MB)
→ Click [Cargar Imagen]
→ Puede tomar tiempo o fallar por memoria
Solución: Usar imagen más pequeña
```

### Formato PNG vs JPEG

```
Imagen original: .bmp (sin compresión) → 5MB

JPEG: [Image → Bytes (JPEG)]
→ Resultado: 500KB (compresión con pérdida)

PNG: [Image → Bytes (PNG)]
→ Resultado: 800KB (sin pérdida)
```

---

## 🧪 Pruebas Sugeridas

1. **Test Hex2Bin**: "AABBCCDD" → [0xAA, 0xBB, 0xCC, 0xDD]
2. **Test Bin2Hex**: [0xAA, 0xBB, 0xCC, 0xDD] → "AABBCCDD"
3. **Test Hex2Bin → Bin2Hex**: Hex → Bytes → Hex → Debe ser igual
4. **Test GetPath**: "foto" → "D:\HTMLFOTOS\ImgEmpleados\foto.jpg"
5. **Test Cargar imagen JPG**: Seleccionar .jpg → Preview correcto
6. **Test Cargar imagen PNG**: Seleccionar .png → Preview correcto
7. **Test Image2Bytes JPEG**: Imagen → JPEG bytes → Mostrar tamaño
8. **Test Image2Bytes PNG**: Imagen → PNG bytes → Mostrar tamaño
9. **Test Hex inválido**: "ZZZZ" → Error/Array vacío
10. **Test Hex longitud impar**: "ABC" → Error/Array vacío

---

## 📊 Tabla de Formato de Imágenes

| Formato | Compresión | Mejor para | Tamaño |
|---------|-----------|-----------|--------|
| JPEG | Con pérdida | Fotos naturales | ↓ Pequeño |
| PNG | Sin pérdida | Gráficos, logos | ↑ Mediano |
| BMP | Sin compresión | Datos sin procesar | ↑↑ Grande |

---

## 🔗 Referencias

- [Clase UtilImage](../LibUtilCasc/UtilImage.cs)
- [System.Drawing.Image](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.image)
- [Formato JPEG](https://en.wikipedia.org/wiki/JPEG)
- [Formato PNG](https://en.wikipedia.org/wiki/Portable_Network_Graphics)
- [Hexadecimal](https://en.wikipedia.org/wiki/Hexadecimal)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0
