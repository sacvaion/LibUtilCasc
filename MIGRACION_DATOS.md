# Migración de Datos Encriptados - LibUtilCasc

## Situación Actual

### Antes (ECB Legacy)
- Algoritmo: Rijndael 256-bit con modo **ECB** (inseguro)
- Formato: Base64 simple
- Clase: `UtilPass` (substitución simple) o `UtilEncription` (ECB)
- Problema: **ECB es vulnerable** - patrones iguales generan ciphertext igual

### Después (CBC Nuevo)
- Algoritmo: Rijndael 256-bit con modo **CBC** (seguro)
- IV Aleatorio: Nuevo para cada mensaje
- Formato: `BASE64(IV):BASE64(CipherText)`
- Clase: `UtilEncription.Encriptar()` / `Decriptar()`
- Ventaja: **Mismo plaintext → diferente ciphertext cada vez**

---

## ✅ Buena Noticia: Backward Compatibility

El método `UtilEncription.Decriptar()` **detecta automáticamente** si el mensaje es:
- **CBC nuevo** (contiene `:`) → desencripta con CBC
- **ECB legacy** (sin `:`) → desencripta con ECB

**Por lo tanto: No se requiere migración de datos existentes.**

---

## Estrategia de Migración

### Fase 1: Convivencia (Semanas 1-4)

```
Datos Legacy (ECB) ──────────────────► Continúan funcionando
                                       (UtilEncription detecta y desencripta)

Datos Nuevos ────────────────────────► Se encriptan con CBC automáticamente
```

**Acciones:**
1. ✅ Compilar con nuevas librerías
2. ✅ Tests pasan (backward compat verificado)
3. ✅ Datos legacy se leen sin problemas

### Fase 2: Migración Gradual (Semanas 5-12)

```
Datos Legacy (ECB) ──► Leer ──► Desencriptar ECB ──► Re-encriptar CBC ──► Guardar
```

**Pasos:**
1. Identificar todos los datos encriptados en BD/archivos
2. Crear script de migración (ver más abajo)
3. Ejecutar en horario off-peak
4. Validar integridad de datos

### Fase 3: Deprecación (Semana 13+)

```
Datos Legacy (ECB) ──► ELIMINAR (después de 3 meses de backup)

Datos New (CBC) ─────► Continúan operando normalmente
```

---

## Script de Migración

### Para Base de Datos SQL Server

```sql
-- Crear tabla de backup (ANTES de migrar)
CREATE TABLE [dbo].[EncryptedDataBackup]
(
    BackupID INT PRIMARY KEY IDENTITY(1,1),
    OriginalID INT,
    OriginalEncryptedValue NVARCHAR(MAX),
    BackupDate DATETIME DEFAULT GETDATE()
);

-- Backup de datos existentes
INSERT INTO [dbo].[EncryptedDataBackup] (OriginalID, OriginalEncryptedValue)
SELECT ID, EncryptedField
FROM [YourTable]
WHERE EncryptedField IS NOT NULL;

-- Verificar backup
SELECT COUNT(*) as BackedUpRecords FROM [dbo].[EncryptedDataBackup];
```

### Script C# de Migración

```csharp
using System;
using System.Data.SqlClient;
using LibUtilCasc;

public class DataMigrationScript
{
    private const string CONNECTION_STRING = "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=true;";
    private const string ENCRYPTION_KEY = "MiClaveSecreta123";

    public static void Main()
    {
        Console.WriteLine("=== Migración de Datos ECB → CBC ===");
        Console.WriteLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine();

        int totalRecords = 0;
        int successCount = 0;
        int errorCount = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                // 1. Contar registros a migrar
                using (SqlCommand countCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM YourTable WHERE EncryptedField IS NOT NULL",
                    conn))
                {
                    totalRecords = (int)countCmd.ExecuteScalar();
                }

                Console.WriteLine($"📋 Total registros a migrar: {totalRecords}");
                Console.WriteLine("Procesando...\n");

                // 2. Procesar registros
                using (SqlCommand selectCmd = new SqlCommand(
                    "SELECT ID, EncryptedField FROM YourTable WHERE EncryptedField IS NOT NULL ORDER BY ID",
                    conn))
                {
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string encryptedValue = reader.GetString(1);

                            try
                            {
                                // Desencriptar con ECB legacy
                                string plaintext = UtilEncription.Decriptar(encryptedValue, ENCRYPTION_KEY);

                                // Re-encriptar con CBC nuevo
                                string newEncryptedValue = UtilEncription.Encriptar(plaintext, ENCRYPTION_KEY);

                                // Actualizar en BD
                                using (SqlCommand updateCmd = new SqlCommand(
                                    "UPDATE YourTable SET EncryptedField = @newValue WHERE ID = @id",
                                    conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@newValue", newEncryptedValue);
                                    updateCmd.Parameters.AddWithValue("@id", id);
                                    updateCmd.ExecuteNonQuery();
                                }

                                successCount++;
                                Console.Write($"\r✓ Migrados: {successCount}/{totalRecords}");
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"\n❌ Error en registro ID={id}: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error de conexión: {ex.Message}");
            return;
        }

        // 3. Resumen
        Console.WriteLine("\n\n=== Resumen de Migración ===");
        Console.WriteLine($"✓ Exitosos: {successCount}");
        Console.WriteLine($"✗ Errores: {errorCount}");
        Console.WriteLine($"⏱ Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        if (errorCount == 0)
        {
            Console.WriteLine("\n✅ Migración completada SIN ERRORES");
        }
        else
        {
            Console.WriteLine($"\n⚠️ Migración completada CON {errorCount} ERRORES - Revisar log");
        }
    }
}
```

---

## Validación de Migración

### Test 1: Verificar Formato de Datos

```csharp
[TestMethod]
public void VerifyNewEncryptionFormat()
{
    string original = "TestData123";
    string key = "SecretKey";

    string encrypted = UtilEncription.Encriptar(original, key);

    // Nuevo formato debe contener ":"
    Assert.IsTrue(encrypted.Contains(":"), "Nuevo formato debe ser IV:Ciphertext");

    // Debe desencriptarse correctamente
    string decrypted = UtilEncription.Decriptar(encrypted, key);
    Assert.AreEqual(original, decrypted);
}
```

### Test 2: Backward Compatibility

```csharp
[TestMethod]
public void VerifyLegacyECBDecryption()
{
    string legacyECBEncrypted = "ABC123DEF456..."; // Dato legacy real de BD

    // Debe desencriptarse sin error
    string decrypted = UtilEncription.Decriptar(legacyECBEncrypted, "YourKey");
    Assert.IsNotNull(decrypted);
    Assert.IsTrue(decrypted.Length > 0);
}
```

### Test 3: Verificar Integridad Post-Migración

```sql
-- Comparar registros ANTES y DESPUÉS
SELECT 
    t1.ID,
    t1.EncryptedField AS OldValue,
    t2.EncryptedField AS NewValue,
    CASE 
        WHEN t1.EncryptedField = t2.EncryptedField THEN 'ERROR: Mismo valor (no re-encriptó)'
        WHEN t2.EncryptedField LIKE '%:%' THEN 'OK: Nuevo formato CBC'
        ELSE 'ERROR: Formato desconocido'
    END AS Status
FROM [EncryptedDataBackup] t1
INNER JOIN [YourTable] t2 ON t1.OriginalID = t2.ID;
```

---

## Checklist de Migración

### Pre-Migración
- [ ] Backup completo de base de datos
- [ ] Verificar script en ambiente de testing
- [ ] Comunicar con stakeholders
- [ ] Validar llave de encriptación

### Migración
- [ ] Ejecutar script en horario off-peak
- [ ] Monitorear ejecución (duración, errores)
- [ ] NO interrumpir mid-process

### Post-Migración
- [ ] Ejecutar validaciones SQL
- [ ] Verificar que aplicación sigue leyendo datos
- [ ] Comparar 10 registros al azar desencriptados
- [ ] Revisar logs de errores

### Rollback Plan
- [ ] Si hay >1% de errores: ROLLBACK inmediato
- [ ] Restore from backup
- [ ] Investigar causa de error
- [ ] Reintentar en horario diferente

---

## Timeline Recomendado

```
Semana 1:  ✓ Deploy nuevo código con backward compat
           ✓ Verificar tests pasan
           ✓ Datos legacy se leen sin problemas

Semana 2:  ✓ Preparar script de migración
           ✓ Testing en ambiente dev/staging
           ✓ Documentar proceso

Semana 3:  ✓ Ejecutar migración en off-peak
           ✓ Validar integridad
           ✓ Monitorear aplicación

Semana 4:  ✓ Mantener backups por 4 semanas más
           ✓ Sin cambios de código requeridos

Semana 5+: ✓ Opcional: Eliminar código legacy UtilPass
           ✓ Opcional: Remover soporte ECB
```

---

## FAQ de Migración

**P: ¿Qué pasa si me olvido de migrar un registro?**
R: No hay problema. `Decriptar()` detecta automáticamente ECB vs CBC. Seguirá funcionando.

**P: ¿Necesito re-encriptar con la misma llave?**
R: SÍ. Usar la misma llave que se usó para el ECB original.

**P: ¿Se puede migrar en background sin downtime?**
R: Sí, siempre que la tabla no tenga lock exclusivo.

**P: ¿Cuánto tiempo tarda migrar 1 millón de registros?**
R: ~10-30 segundos (depende del servidor SQL). Prueba en staging.

**P: ¿Qué pasa con datos en archivos (no BD)?**
R: Usar mismo script C# pero iterando sobre archivos en lugar de BD.

---

## Referencia: Diferencia ECB vs CBC

### ECB (Inseguro)
```
Plaintext:  [HELLO123] [HELLO123] [WORLD456]
                ↓           ↓          ↓
            [ECB Encrypt]  
                ↓           ↓          ↓
Ciphertext: [ABC00123] [ABC00123] [DEF45678]
                       ↑ MISMO (Patrón repetido!)
```

### CBC (Seguro)
```
Plaintext:  [HELLO123] [HELLO123] [WORLD456]
                ↓           ↓          ↓
            [CBC Encrypt con IV aleatorio]
                ↓           ↓          ↓
Ciphertext: [XYZ11111] [PQR22222] [STU33333]
                       ↑ DIFERENTE (Seguro - nunca se repite)
```

---

## Soporte

Si necesita ayuda con la migración:
1. Revisar MIGRACION_DATOS.md (este archivo)
2. Ejecutar test: `UtilEncriptionEncriptarDecriptarCBCRoundTrip()`
3. Contactar al equipo de desarrollo

