# Instrucciones de Compilación - LibUtilCasc

## Paso 1: Compilar Proyecto

### Opción A: Visual Studio (Recomendado)
1. Abrir `LibUtilCasc.sln` en Visual Studio 2019 o superior
2. Menú: **Build** → **Rebuild Solution**
3. Esperar a que compile. Debe ver: `Build succeeded.` sin errores

### Opción B: Línea de Comandos (Developer Command Prompt)
```bash
cd F:\Dev\Git\LibUtilCasc\LibUtilCasc
msbuild LibUtilCasc.sln /p:Configuration=Debug /p:Platform=AnyCPU
```

### ✅ Esperado
- `0 Error(s)`
- Puede haber algunos warnings de [Obsolete], eso es normal
- La DLL se genera en: `LibUtilCasc\bin\Debug\LibUtilCasc.dll`

---

## Paso 2: Restaurar Paquetes NuGet

### En Visual Studio
1. Menú: **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Ejecutar: `Update-Package -Reinstall`
3. O simplemente: **Build** → **Rebuild Solution** (restaura automáticamente)

### Alternativa: Command Line
```bash
cd F:\Dev\Git\LibUtilCasc\LibUtilCasc
nuget restore
```

### ✅ Verificar
Debe descargar:
- `NLog 5.2.8`
- `Newtonsoft.Json 13.0.3`
- `EntityFramework 6.5.1`
- Otros paquetes existentes

Carpeta: `LibUtilCasc\packages\`

---

## Paso 3: Configurar AppSettings

Ver archivo `APPCONFIG.md` incluido en este directorio.

---

## Paso 4: Ejecutar Unit Tests

### En Visual Studio
1. Menú: **Test** → **Test Explorer** (o Ctrl+E, T)
2. Click en **Run All Tests**
3. Deben pasar **18 tests** (3 legacy + 15 nuevos)

### Alternativa: Command Line
```bash
cd F:\Dev\Git\LibUtilCasc\LibUtilCasc
# Con MSTest
vstest.console.exe UtilUnitTest\bin\Debug\UtilUnitTest.dll
```

### ✅ Esperado
```
18 Passed ✓
0 Failed ✗
0 Skipped ⊘
```

---

## Paso 5: Validar Migración de Datos

Ver archivo `MIGRACION_DATOS.md` para instrucciones detalladas.

---

## Troubleshooting

### Error: "NLog not found"
- Menú: **Tools** → **NuGet Package Manager** → **Manage NuGet Packages for Solution**
- Buscar "NLog"
- Instalar versión 5.2.8

### Error: "The type or namespace 'NLog' does not exist"
- Asegurar que `NLog` está instalado (paso 2)
- Menú: **Build** → **Clean Solution** → **Rebuild Solution**

### Warning: "Type X is obsolete..."
- Normal y esperado. El código legacy sigue funcionando.
- Migrar gradualmente a los nuevos métodos.

### Tests fallan
- Asegurar que `NLog.config` está en la carpeta raíz de `LibUtilCasc`
- Ejecutar: **Build** → **Rebuild Solution**
- Hacer click derecho en TestProject → **Properties** → Output Path → `bin\Debug\`
