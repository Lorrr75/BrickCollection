# Configuration / Configurazione

**[English](#english) | [Italiano](#italiano)**

---

## English

### Brickset API Key

1. Request a free API key at https://brickset.com/tools/webservices/requestkey
2. **Never commit your API key to the repository.** Store it in a local, git-ignored file:
   - Create `src/BrickCollection/appsettings.local.json` (already excluded via `.gitignore`)
   - Add:
     ```json
     {
       "BricksetApiKey": "YOUR_KEY_HERE"
     }
     ```
3. The app reads this value at startup; if missing, authentication calls will fail with a clear error message

### Rate Limits

The Brickset API limits individual API keys to 100 calls/day for `getSets`. The app's local SQLite cache is designed to minimize calls — avoid disabling or bypassing the cache during development/testing.

## Italiano

### API Key Brickset

1. Richiedi una API key gratuita su https://brickset.com/tools/webservices/requestkey
2. **Non versionare mai la tua API key nel repository.** Salvala in un file locale escluso da git:
   - Crea `src/BrickCollection/appsettings.local.json` (già escluso via `.gitignore`)
   - Aggiungi:
     ```json
     {
       "BricksetApiKey": "LA_TUA_CHIAVE_QUI"
     }
     ```
3. L'app legge questo valore all'avvio; se mancante, le chiamate di autenticazione falliranno con un messaggio d'errore chiaro

### Limiti di Chiamate

L'API Brickset limita le API key individuali a 100 chiamate/giorno per `getSets`. La cache SQLite locale dell'app è pensata per minimizzare le chiamate — evita di disabilitarla o bypassarla durante sviluppo/test.
