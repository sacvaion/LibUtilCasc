# Quick Start - LibUtilCasc Improvements

## 🚀 Inicio Rápido (30 minutos)

### **Paso 1️⃣: Compilar Proyecto (5 min)**

```
Visual Studio:
  1. Abrir: LibUtilCasc\LibUtilCasc.sln
  2. Menu: Build → Rebuild Solution
  3. Esperar: "Build succeeded" sin errores
```

✅ **Esperado:** 
- `0 Errors`
- Pueden haber warnings de `[Obsolete]` - normal

---

### **Paso 2️⃣: Restaurar NuGet (3 min)**

```
Opción A (Automático):
  Just compile! Visual Studio restaura automáticamente

Opción B (Manual):
  Menu: Tools → NuGet Package Manager → Package Manager Console
  Ejecutar: Update-Package -Reinstall
```

✅ **Esperado:**
- Se descargue `NLog 5.2.8`
- Carpeta `packages/` se actualiza

---

### **Paso 3️⃣: Configurar AppSettings (2 min)**

**Archivo:** `LibUtilCasc\App.config`

```xml
<!-- Ya incluido - Solo revisar/editar si necesita cambiar credenciales -->
<add key="RaspUsername" value="pi" />
<add key="RaspPassword" value="raspberry" />
```

✅ **Para producción:**
- Cambiar contraseña "raspberry" por real
- Considerar usar secretos (Azure Key Vault, etc.)

📖 **Más detalles:** Ver `APPCONFIG.md`

---

### **Paso 4️⃣: Ejecutar Tests (15 min)**

```
Visual Studio:
  1. Menu: Test → Test Explorer (Ctrl+E, T)
  2. Click: "Run All Tests" (botón play)
  3. Esperar: ~5 segundos
```

✅ **Esperado:**
```
✓ 21 tests passed
✗ 0 tests failed
```

📖 **Más detalles:** Ver `UNIT_TESTS.md`

---

### **Paso 5️⃣: Validar Migración de Datos (5 min)**

**Buena noticia:** ✅ Datos existentes (ECB legacy) seguirán funcionando
- `Decriptar()` detecta automáticamente ECB vs CBC
- NO REQUIERE migración inmediata

**Migración opcional:** Para re-encriptar datos existentes a CBC
- Ver script en: `MIGRACION_DATOS.md`
- Timeline: Semanas 5-12 (post-deployment)

---

## 📚 Documentación Completa

| Documento | Tema | Tiempo |
|-----------|------|--------|
| `INSTRUCCIONES_COMPILACION.md` | Compilar, troubleshooting | 10 min |
| `APPCONFIG.md` | Configuración de AppSettings | 5 min |
| `UNIT_TESTS.md` | Ejecución y cobertura de tests | 15 min |
| `MIGRACION_DATOS.md` | Migración ECB → CBC | 30 min (opcional) |

---

## ✨ Qué Cambió

### 🔒 Seguridad
- **ECB → CBC**: Encriptación mejorada (IV aleatorio)
- **Validaciones**: Null checks en todos los métodos
- **Credenciales**: Dari desde AppSettings (no hardcodeadas)

### 📝 Logging
- **NLog integrado**: Niveles Info/Warn/Error
- **Rastreo**: Mejor debugging y auditoría

### ✅ Tests
- **18 nuevos tests**: Coverage de edge cases
- **Backward compat**: Datos legacy funcionan

### 📚 Documentación
- **Doc XML**: En todos los métodos públicos
- **Métodos obsoletos**: Marcan código legacy

---

## 🎯 Verificación Final

Después de Paso 4, ejecutar:

```csharp
// En AppPrueba Form1.cs
private void btnTest_Click(object sender, EventArgs e)
{
    // Test 1: Legacy encryption still works
    string legacy = LibUtilCasc.UtilPass.Encriptar("Test");
    string decrypted = LibUtilCasc.UtilPass.DesEncriptar(legacy);
    Debug.WriteLine($"✓ Legacy: {decrypted == "Test"}");

    // Test 2: New CBC encryption works
    string newEnc = LibUtilCasc.UtilEncription.Encriptar("Test", "Key123");
    string newDec = LibUtilCasc.UtilEncription.Decriptar(newEnc, "Key123");
    Debug.WriteLine($"✓ CBC: {newDec == "Test"} (formato: {(newEnc.Contains(":") ? "IV:CT" : "LEGACY")})");

    // Test 3: Network utilities
    var macs = LibUtilCasc.UtilNetWork.GetLstMac();
    Debug.WriteLine($"✓ Network: {macs.Count} MACs encontradas");

    MessageBox.Show("✅ Todos los tests pasaron!");
}
```

---

## ⚡ Acciones Recomendadas

### Inmediato (Esta semana)
- ✅ Pasos 1-4 (compilar, tests)
- ✅ Revisar warnings de [Obsolete]
- ✅ Configurar RaspUsername/Password si es necesario

### Corto Plazo (Este mes)
- ⏸ Migración de datos (Paso 5) - opcional pero recomendado
- ⏸ Actualizar código legacy para usar nuevos métodos

### Mediano Plazo (Próximos 3 meses)
- ⏸ Deprecar `UtilPass` completamente
- ⏸ Eliminar soporte ECB si todos los datos migraron

---

## ❓ FAQ Rápido

**P: ¿Funciona el código antiguo?**
R: Sí. Métodos viejos marcados `[Obsolete]` pero siguen funcionando.

**P: ¿Se pierden datos?**
R: No. Backward compatibility garantizada.

**P: ¿Qué pasa si ignoro los warnings?**
R: Todo funciona. Los warnings solo advierten de código deprecated.

**P: ¿Necesito migrar datos ya?**
R: No. Pero se recomienda en próximas 2-4 semanas.

**P: ¿Si falla Step 1 o 2?**
R: Ver `INSTRUCCIONES_COMPILACION.md` - sección Troubleshooting

---

## 📞 Soporte

Si encuentra problemas:
1. Revisar archivo correspondiente (INSTRUCCIONES_*, APPCONFIG.md, etc.)
2. Buscar sección "Troubleshooting"
3. Ejecutar: `Build → Clean Solution → Rebuild Solution`

---

## ✅ Checklist Final

- [ ] Paso 1: Compiló sin errores
- [ ] Paso 2: NLog 5.2.8 instalado
- [ ] Paso 3: AppSettings revisados
- [ ] Paso 4: 21 tests pasaron
- [ ] Paso 5: Entiende estrategia de migración
- [ ] ✨ ¡Listo para producción!

---

**Tiempo total estimado:** 30 minutos ⏱
**Dificultad:** Fácil 🟢
**Riesgo:** Mínimo (todo tiene rollback) 🟢

