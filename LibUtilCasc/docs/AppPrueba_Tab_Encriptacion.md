# Tab Encriptación - Guía de Uso

## 📋 Descripción

La pestaña **Encriptación** permite probar tres métodos de encriptación:
1. **CBC Mode** (moderno y seguro)
2. **GUID Mode** (encriptación con GUID interno)
3. **Legacy Mode** (UtilPass ECB - para backward compatibility)

## 🎮 Controles

### Sección 1: Encriptación CBC (Recomendado)

```
┌──────────────────────────────────────────────────┐
│ Mensaje: [                                      ] │
│ Clave:   [MiClaveSecreta123                    ] │
│          [Encriptar CBC] [Desencriptar CBC]    │
└──────────────────────────────────────────────────┘
```

#### Entrada
- **Mensaje**: Texto a encriptar (ej: "Información confidencial")
- **Clave**: Contraseña para encriptación (default: "MiClaveSecreta123")

#### Botones
| Botón | Método | Descripción |
|-------|--------|-------------|
| **Encriptar CBC** | `UtilEncription.Encriptar(msg, key)` | Encripta con CBC + IV aleatorio |
| **Desencriptar CBC** | `UtilEncription.Decriptar(cipher, key)` | Desencripta (detecta automáticamente CBC) |

#### Formato del resultado
- **Encriptado**: `IV_Base64:CipherText_Base64`
- Contiene ":" como separador entre IV y texto cifrado

**Ejemplo:**
```
19:25:30 - ✓ Encriptado (CBC): wLXq9K...base64...==:m8jF2n...base64...==
19:25:35 - ✓ Desencriptado (CBC): Información confidencial
```

### Sección 2: Encriptación con GUID Interno

```
┌──────────────────────────────────────────────────┐
│          [Encriptar GUID] [Desencriptar GUID]   │
└──────────────────────────────────────────────────┘
```

#### Entrada
- Usa el **Mensaje** y **Clave** de la sección anterior
- La clave es automáticamente el GUID de App.config
- Default GUID: `c2095be7-491f-4aec-99d6-debe6b3ac6db`

#### Botones
| Botón | Método | Descripción |
|-------|--------|-------------|
| **Encriptar GUID** | `UtilEncription.EncriptarGuidInternal(msg)` | Encripta con GUID interno |
| **Desencriptar GUID** | `UtilEncription.DecriptarGuidInternal(cipher)` | Desencripta con GUID interno |

#### Uso
Cuando necesitas encriptar con una clave conocida por la aplicación (sin que el usuario la proporcione).

**Ejemplo:**
```
19:25:40 - ✓ Encriptado (GUID): aB12...base64...==:xY34...base64...==
19:25:45 - ✓ Desencriptado (GUID): Información confidencial
```

### Sección 3: Encriptación Legacy (Obsoleto)

```
┌──────────────────────────────────────────────────┐
│ Legacy (UtilPass - Obsoleto):                    │
│ [Encriptar Legacy] [Desencriptar Legacy]        │
└──────────────────────────────────────────────────┘
```

#### Entrada
- Usa el **Mensaje** de la sección 1
- Algoritmo: UtilPass ECB (inseguro, solo para compatibilidad)

#### Botones
| Botón | Método | Descripción |
|-------|--------|-------------|
| **Encriptar Legacy** | `UtilPass.Encriptar(msg)` | Encripta ECB (deprecated) |
| **Desencriptar Legacy** | `UtilPass.DesEncriptar(cipher)` | Desencripta ECB (deprecated) |

#### Advertencia
⚠️ **Estos métodos están marcados como [Obsolete]**
- No usar en nuevo código
- Solo para migración de sistemas legados
- **Usar UtilEncription (CBC) en su lugar**

**Ejemplo:**
```
19:25:50 - ✓ Encriptado (Legacy): aB12cD34eF56...base64...==
19:25:55 - ✓ Desencriptado (Legacy): Información confidencial
```

### Panel de Resultado

```
┌──────────────────────────────────────────────────┐
│ Resultado:                                       │
│ [                                              ] │
│ [                                              ] │
└──────────────────────────────────────────────────┘
```

TextBox multiline que muestra el texto encriptado completo para copiar/pegar.

## 📖 Casos de Uso

### 1. Encriptar y desencriptar con CBC (recommended)

```
Paso 1: Escribir mensaje
  Mensaje: "Password123!"
  Clave: "MySecretKey"

Paso 2: Click [Encriptar CBC]
  Resultado: "wLXq9K/j2m...base64...==:m8jF2n/k3o...base64...=="

Paso 3: El resultado se copia a txtEncResult automáticamente

Paso 4: Click [Desencriptar CBC]
  Resultado: "Password123!"
```

### 2. Encriptar con GUID interno

```
Paso 1: Escribir mensaje
  Mensaje: "Datos sensibles"

Paso 2: Click [Encriptar GUID]
  Resultado: "aB12/cD34e...base64...==:fG56/hI78j...base64...=="

Paso 3: Click [Desencriptar GUID]
  Resultado: "Datos sensibles"
```

### 3. Validar compatibilidad backward (ECB legacy)

```
Paso 1: Mensaje antiguo (encriptado con UtilPass)
  txtEncResult: "aB12cD34eF56..."

Paso 2: Click [Desencriptar CBC]
  Comportamiento: Detecta automáticamente que NO contiene ":" 
                  → Asume formato ECB legacy
                  → Desencripta con UtilPass.DesEncriptar
  Resultado: "Mensaje original"
```

## 🔍 Detalles Técnicos

### Encriptación CBC (Moderno)

**Algoritmo:** Rijndael con CBC mode + PKCS7 padding + IV aleatorio

**Seguridad:**
- ✅ IV aleatorio para cada encriptación (no determinista)
- ✅ CBC mode previene patrones en ciphertext
- ✅ PKCS7 padding evita oracle attacks
- ✅ Clave de 256 bits

**Proceso:**
1. Generar IV aleatorio (16 bytes)
2. Derive clave de 256 bits desde string proporcionado
3. Encriptar mensaje con IV + clave
4. Retornar: `Base64(IV) + ":" + Base64(CipherText)`

**Formato:** `IV_Base64:CipherText_Base64`

**Ventaja:** Cada encriptación de mismo mensaje produce salida diferente

### Encriptación GUID

**Algoritmo:** Mismo que CBC pero con clave = GUID del config

**Caso de uso:** Encriptar datos internos sin exponerlos al usuario

**Flujo:**
1. Leer GuidInternalDLL desde App.config
2. Encriptar/Desencriptar con ese GUID como clave

### Encriptación Legacy (Obsoleto)

**Algoritmo:** DES-ECB (inseguro, no usar)

**Problemas:**
- ❌ Determinista (mismo mensaje → mismo ciphertext)
- ❌ Vulnerable a pattern analysis
- ❌ ECB mode es débil criptográficamente
- ❌ DES es débil (56-bit effective)

**Solo usar para:**
- Leer datos antiguos
- Migración de sistemas legacy
- Compatibilidad con código viejo

## ⚠️ Casos Especiales

### Mensaje vacío con CBC

```
Mensaje: ""
Clave: "MyKey"
→ Click [Encriptar CBC]
→ Resultado: Encripta correctamente (texto vacío es válido)
```

### Clave muy corta o muy larga

```
Clave: "a"
→ Pads a 16 bytes: "a           " (con espacios)

Clave: "MyClave1234567890ABCDEFGHIJKLMNOP"
→ Trunca a 16 bytes: "MyClave123456789"
```

### Intentar desencriptar sin clave

```
txtEncResult: "aB12..."
txtEncKey: ""
→ Click [Desencriptar CBC]
→ Error: Clave requerida
```

### Formato inválido

```
txtEncResult: "invalido:::base64:::"
→ Click [Desencriptar CBC]
→ Error: Formato inválido de mensaje encriptado
```

## 🔐 Recomendaciones de Seguridad

| Escenario | Recomendado | Por qué |
|-----------|-------------|--------|
| Datos nuevos | **CBC** | Moderno y seguro |
| Datos internos | **GUID** | Clave manejada por app |
| Datos legacy | **Legacy** | Solo lectura, no escribir |
| Archivo config | **No poner claves** | Usar GUID en lugar |
| Clave fuerte | **20+ caracteres** | Resistant a fuerza bruta |

## 🧪 Pruebas Sugeridas

1. **Test CBC roundtrip**: Encriptar "Hola" → Desencriptar → Debe ser "Hola"
2. **Test GUID**: Encriptar → Desencriptar con GUID → Debe recuperarse
3. **Test Legacy**:Encriptar con Legacy → Desencriptar con CBC → Detecta ECB y desencripta
4. **Test IV diferente**: Encriptar "Test" dos veces → Resultados deben ser diferentes
5. **Test clave vacía**: Dejar clave vacía → Debe funcionar (pad a 16)
6. **Test caracteres especiales**: Encriptar "!@#$%^&*()" → Desencriptar → Recuperar
7. **Test unicode**: Encriptar "测试中文" → Desencriptar → Debe ser igual
8. **Test muy largo**: Encriptar 10KB de texto → Desencriptar → Debe ser igual

## 🔗 Referencias

- [Clase UtilEncription](../LibUtilCasc/UtilEncription.cs)
- [Clase UtilPass (Legacy)](../LibUtilCasc/UtilPass.cs)
- [NIST SP 800-38A - CBC Mode](https://csrc.nist.gov/publications/detail/sp/800-38a/final)
- [Documentación Rijndael/AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard)

---

**Última actualización:** Abril 2026  
**Versión:** 1.0
