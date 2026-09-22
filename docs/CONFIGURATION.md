# Configuration / Configurazione

**[English](#english) | [Italiano](#italiano)**

---

## English

### Brickset Credentials (local development)

The app needs three secrets to talk to Brickset: your API key, and your Brickset username/password (used once to obtain a session hash).

> **Note**: earlier drafts of this doc described an `appsettings.local.json` approach. The actual implementation uses a simpler git-ignored C# file instead — updated below.

1. Request a free API key at https://brickset.com/tools/webservices/requestkey
2. In the project root (`src/BrickCollection/`), create a file named `Secrets.cs`:
   ```csharp
   namespace BrickCollection;

   internal static class Secrets
   {
       public const string BricksetApiKey = "YOUR_API_KEY_HERE";
       public const string BricksetUsername = "YOUR_USERNAME_HERE";
       public const string BricksetPassword = "YOUR_PASSWORD_HERE";
   }
   ```
3. **This file is excluded via `.gitignore`** — verify `Secrets.cs` is listed there before your first commit involving it
4. `MainPage.xaml.cs` reads these constants to call `BricksetAuthService.LoginAsync(...)`

This is a pragmatic approach for a single-user personal app in early development. If the app is ever distributed to other users, this should be replaced with a proper per-user login flow using `SecureStorage` instead of compile-time constants (planned refactor, not yet scheduled).

### Rate Limits

The Brickset API limits individual API keys to 100 calls/day, but **only calls to `getSets` count against this quota** — `login` and `getCollection` do not. The app fetches the full collection in a single `getSets` call (`pageSize:500`) rather than paginating, to keep usage minimal, and caches results locally (SQLite) so the API is only called when the user explicitly taps "Refresh".

## Italiano

### Credenziali Brickset (sviluppo locale)

L'app ha bisogno di tre segreti per comunicare con Brickset: la tua API key, e username/password Brickset (usati una volta per ottenere uno hash di sessione).

> **Nota**: versioni precedenti di questo documento descrivevano un approccio con `appsettings.local.json`. L'implementazione reale usa invece un file C# più semplice, escluso da git — aggiornato di seguito.

1. Richiedi una API key gratuita su https://brickset.com/tools/webservices/requestkey
2. Nella root del progetto (`src/BrickCollection/`), crea un file chiamato `Secrets.cs`:
   ```csharp
   namespace BrickCollection;

   internal static class Secrets
   {
       public const string BricksetApiKey = "LA_TUA_API_KEY_QUI";
       public const string BricksetUsername = "IL_TUO_USERNAME_QUI";
       public const string BricksetPassword = "LA_TUA_PASSWORD_QUI";
   }
   ```
3. **Questo file è escluso tramite `.gitignore`** — verifica che `Secrets.cs` sia elencato lì prima del tuo primo commit che lo coinvolge
4. `MainPage.xaml.cs` legge queste costanti per chiamare `BricksetAuthService.LoginAsync(...)`

Questo è un approccio pragmatico per un'app personale mono-utente in fase iniziale di sviluppo. Se l'app venisse mai distribuita ad altri utenti, andrebbe sostituito con un vero flusso di login per-utente usando `SecureStorage` invece di costanti compilate (refactor pianificato, non ancora programmato).

### Limiti di Chiamate

L'API Brickset limita le API key individuali a 100 chiamate/giorno, ma **solo le chiamate a `getSets` consumano questa quota** — `login` e `getCollection` no. L'app scarica l'intera collezione in un'unica chiamata `getSets` (`pageSize:500`) invece di paginare, per minimizzare il consumo, e mette in cache i risultati localmente (SQLite) così l'API viene chiamata solo quando l'utente tocca esplicitamente "Aggiorna".
