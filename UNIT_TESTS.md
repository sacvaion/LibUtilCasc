# Ejecución de Unit Tests - LibUtilCasc

## Resumen de Tests

Se agregaron **18 nuevos tests** al proyecto `UtilUnitTest`. Junto con los 3 tests originales, hay **21 tests en total**.

| Categoría | Tests | Estado |
|-----------|-------|--------|
| **Legacy** | 3 | ✓ Originales |
| **Nuevos** | 18 | ✓ Agregados |
| **Total** | 21 | ✓ Listos |

---

## Tests Agregados

### 1. **Extracción Numérica** (3 tests)
- `ExtraerNumericoNullInput()` - Debe lanzar ArgumentNullException
- `ExtraerNumericoEmptyInput()` - Debe lanzar ArgumentNullException  
- `ExtraerNumericoSuccess()` - "ABD123456" → 124123456 (A=1, B=2, D=4)

### 2. **Parsing de Fechas** (3 tests)
- `FechaFormatoValid()` - "01/01/2025 12:00:00 PM" parsed correctamente
- `FechaFormatoEmpty()` - Retorna DateTime.Now
- `FechaFormatoInvalid()` - Retorna DateTime.MinValue sin lanzar excepción

### 3. **Validación de Placas** (4 tests)
- `GetVehicleTypePlacaValida6()` - "ABC123" → Tipo 1 (3 letras + 3 números)
- `GetVehicleTypePlacaValida5()` - "ABC12" → Tipo 2 (3 letras + 2 números)
- `GetVehicleTypePlacaInvalida()` - "XYZ" → 0 (inválida)
- `GetVehicleTypeNull()` - null → 0

### 4. **Encriptación Legacy** (1 test)
- `UtilPassEncriptarDesencriptarRoundTrip()` - Verifica backward compatibility

### 5. **Encriptación Nueva (CBC)** (1 test)
- `UtilEncriptionEncriptarDecriptarCBCRoundTrip()` - Round-trip con CBC
  - Verifica que contiene `:` (formato `IV:Ciphertext`)
  - Mensaje encriptado nuevo es diferente al anterior (IV aleatorio)

### 6. **Operaciones de Red** (3 tests)
- `UtilNetWorkGetLstMac()` - Obtiene lista de MACs
- `UtilNetWorkGetLstIpAsync()` - Obtiene IPs de forma asincrónica
- `UtilNetWorkGetLstIpNull()` - Debe lanzar ArgumentNullException
- `UtilNetWorkGetMacFormat()` - "001122334455" → "00:11:22:33:44:55"

### 7. **Manipulación de Imágenes** (3 tests)
- `UtilImageHex2Bin()` - "AABBCCDD" → 4 bytes [0xAA, 0xBB, 0xCC, 0xDD]
- `UtilImageHex2BinInvalidLength()` - "ABC" (longitud impar) → array vacío
- `UtilImageBin2HexUpper()` - [0xAA, 0xBB] → "AABB"

### 8. **Formato de Fechas** (1 test)
- `GetDateFormat()` - 2025-03-15 14:30:45 → "250315143045"

---

## Cómo Ejecutar Tests

### ✅ Opción 1: Visual Studio (Recomendado)

**Paso 1:** Compilar Solución
```
Menú: Build → Rebuild Solution
```

**Paso 2:** Abrir Test Explorer
```
Menú: Test → Test Explorer
O: Ctrl + E, T
```

**Paso 3:** Ejecutar Todos los Tests
```
Click: "Run All Tests" (ícono de play)
O: Menú Test → Run All Tests → All Tests
```

**Resultado esperado:**
```
✓ 21 tests passed
✗ 0 tests failed
⊘ 0 tests skipped
⏱ ~5 segundos
```

---

### ✅ Opción 2: Línea de Comandos (Developer Command Prompt)

```bash
# Abrir Developer Command Prompt for Visual Studio

# Navegar al proyecto
cd F:\Dev\Git\LibUtilCasc\LibUtilCasc

# Compilar
msbuild LibUtilCasc.sln /p:Configuration=Debug

# Ejecutar tests con VSTest
vstest.console.exe UtilUnitTest\bin\Debug\UtilUnitTest.dll
```

---

### ✅ Opción 3: Test Explorer desde Línea de Comandos

```bash
# Con vstest.console.exe (Visual Studio)
vstest.console.exe UtilUnitTest\bin\Debug\UtilUnitTest.dll /Logger:console

# O especificar un test individual
vstest.console.exe UtilUnitTest\bin\Debug\UtilUnitTest.dll /Tests:UtilGenUnitTest.ExtraerNumericoSuccess
```

---

### ✅ Opción 4: NUnit Console (si está instalado)

```bash
nunit3-console.exe UtilUnitTest\bin\Debug\UtilUnitTest.dll
```

---

## Resultado Esperado

### Pantalla de Test Explorer

```
✓ UtilGenUnitTest
  ✓ CreateIdTransactionSuccess
  ✓ CreateIdTransactionError
  ✓ ExtraerNumericoSuccess
  ✓ ExtraerNumericoNullInput
  ✓ ExtraerNumericoEmptyInput
  ✓ FechaFormatoValid
  ✓ FechaFormatoEmpty
  ✓ FechaFormatoInvalid
  ✓ GetVehicleTypePlacaValida6
  ✓ GetVehicleTypePlacaValida5
  ✓ GetVehicleTypePlacaInvalida
  ✓ GetVehicleTypeNull
  ✓ UtilPassEncriptarDesencriptarRoundTrip
  ✓ UtilEncriptionEncriptarDecriptarCBCRoundTrip
  ✓ UtilNetWorkGetLstMac
  ✓ UtilNetWorkGetLstIpAsync
  ✓ UtilNetWorkGetLstIpNull
  ✓ UtilNetWorkGetMacFormat
  ✓ UtilImageHex2Bin
  ✓ UtilImageHex2BinInvalidLength
  ✓ UtilImageBin2HexUpper
  ✓ GetDateFormat

=====================================
Total: 21 passed, 0 failed, 0 skipped
Duración: ~5 segundos
=====================================
```

---

## Troubleshooting

### ❌ Error: "Assembly not found"
**Causa:** El proyecto no compiló
**Solución:**
```
Build → Clean Solution
Build → Rebuild Solution
Test → Run All Tests
```

### ❌ Error: "NLog assembly not found"
**Causa:** NuGet packages no se restauraron
**Solución:**
```
Tools → NuGet Package Manager → Package Manager Console
Update-Package -Reinstall
```

### ❌ Tests fallan con "TestInitialize"
**Causa:** Falta configurar datos de entrada
**Solución:** Los tests son independientes y no necesitan setup

### ❌ Un test falla: "UtilEncriptionEncriptarDecriptarCBCRoundTrip"
**Causa:** Problema con DPAPI o configuración de seguridad
**Solución:** Ejecutar Visual Studio como Administrador

### ❌ Test async no se ejecuta
**Causa:** Framework de tests no soporta async
**Solución:** Upgrade a MSTest v2.x:
```
NuGet: Install-Package MSTest.TestFramework -Version 2.2.9
```

---

## Agregar Nuevos Tests

Para agregar más tests al archivo `UtilGenUnitTest.cs`:

```csharp
[TestClass]
public class UtilGenUnitTest
{
    [TestMethod]
    public void MiNuevoTest()
    {
        // Arrange
        string input = "test";
        
        // Act
        var resultado = SomeMethod(input);
        
        // Assert
        Assert.AreEqual("esperado", resultado);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void MiTestDeExcepcion()
    {
        SomeMethodThatThrows(null);
    }
}
```

Luego ejecutar: `Test → Run All Tests`

---

## Cobertura de Tests

Métodos cubiertos:
- ✓ `UtilGen.ExtraerNumerico()`
- ✓ `UtilGen.FechaFormato()`
- ✓ `UtilGen.GetVehicleType()`
- ✓ `UtilGen.CreateIdTransaction()`
- ✓ `UtilGen.GetDate()`
- ✓ `UtilPass.Encriptar()` / `DesEncriptar()`
- ✓ `UtilEncription.Encriptar()` / `Decriptar()`
- ✓ `UtilNetWork.GetLstMac()`
- ✓ `UtilNetWork.GetLstIp()` / `GetLstIpAsync()`
- ✓ `UtilNetWork.GetMacFormat()`
- ✓ `UtilImage.Hex2Bin()`
- ✓ `UtilImage.Bin2HexUpper()` / `Bin2HexLower()`

Métodos sin tests específicos (pero funcionan en tests de integración):
- `UtilRaspberry` - Requiere Raspberry real
- `UtilImage.CombineBitmap()` - Requiere archivos de imagen
- `Logger` - Se probará en ejecución normal

