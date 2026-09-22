# Architecture / Architettura

**[English](#english) | [Italiano](#italiano)**

---

## English

### Overview

BrickCollection is a .NET MAUI Android application that consumes the [Brickset REST API v3](https://brickset.com/api/v3.asmx) to manage a personal LEGO set collection.

### Tech Stack

| Layer | Technology |
|---|---|
| UI Framework | .NET MAUI (Android only) |
| Language | C# |
| Local Storage | SQLite (`sqlite-net-pcl` + `SQLitePCLRaw.bundle_green`) |
| HTTP / JSON | `HttpClient` + `System.Text.Json` |
| Localization | `.resx` resource files (5 languages) — planned |

### Project Structure (current)

```
BrickCollection/
├── BrickCollection.sln
├── src/
│   └── BrickCollection/
│       ├── Models/
│       │   └── BricksetModels.cs      # BricksetSet, BricksetImage, raw response DTOs
│       ├── Services/
│       │   ├── BricksetAuthService.cs # LoginAsync, GetOwnedSetsAsync
│       │   └── CacheService.cs        # SQLite persistence (CachedSet, SaveSetsAsync, GetCachedSetsAsync)
│       ├── Views/
│       │   └── SetDetailPage.xaml(.cs)
│       ├── MainPage.xaml(.cs)         # Collection list (CollectionView), cache-first load, refresh toolbar button
│       ├── App.xaml.cs                # SQLitePCL.Batteries_V2.Init(); CreateWindow(...) override
│       ├── Secrets.cs                 # git-ignored: API key + credentials
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

Planned additions: write operations back to Brickset (Step 6), charts/advanced stats (Step 7).

### Data Flow (current)

1. **App startup**: `App()` calls `SQLitePCL.Batteries_V2.Init()` before anything else touches SQLite; `CreateWindow(...)` is the sole source of the initial page (do not also set `MainPage` — see `docs/TROUBLESHOOTING.md`)
2. **Cache-first load**: `MainPage.InitializeAsync()` checks `CacheService.GetCachedSetsAsync()` first; if the cache has data, it's displayed immediately with no API call
3. **Refresh (manual)**: a toolbar button triggers `RefreshFromApiAsync()` → `BricksetAuthService.LoginAsync()` → `GetOwnedSetsAsync()` → `CacheService.SaveSetsAsync()` (overwrites the cache) → re-display
4. **Detail navigation**: tapping a list item calls `Navigation.PushAsync(new SetDetailPage(selectedSet))`, passing the already-fetched `BricksetSet` object directly (no extra API/DB call needed)

### Key Design Decisions

- **Android-only target**: simplifies the `.csproj` and avoids maintaining iOS/Mac-specific code paths
- **No MVVM framework yet**: direct code-behind binding has been simpler and kept each step independently testable, per the incremental development approach. MVVM (or at least a lightweight ViewModel layer) is expected to be introduced once Step 6-7 add enough logic to justify it
- **Cache-first, manual refresh only**: given the API rate limit (100 calls/day for `getSets`), the app loads from local SQLite cache on every startup and only calls the API when the user explicitly taps "Refresh" — never automatically on launch
- **Secrets via git-ignored `Secrets.cs`**: simpler than config-file parsing for a single-user personal app in early development; see `docs/CONFIGURATION.md` for the reasoning and future refactor plan
- **`SQLitePCLRaw.bundle_green` only, no extra native package references**: adding direct references to `SQLitePCLRaw.lib.e_sqlite3*` packages alongside the bundle causes native library conflicts on Android — see `docs/TROUBLESHOOTING.md`

---

## Italiano

### Panoramica

BrickCollection è un'applicazione Android .NET MAUI che utilizza le [Brickset REST API v3](https://brickset.com/api/v3.asmx) per gestire una collezione personale di set LEGO.

### Stack Tecnologico

| Livello | Tecnologia |
|---|---|
| Framework UI | .NET MAUI (solo Android) |
| Linguaggio | C# |
| Storage Locale | SQLite (`sqlite-net-pcl` + `SQLitePCLRaw.bundle_green`) |
| HTTP / JSON | `HttpClient` + `System.Text.Json` |
| Localizzazione | File di risorse `.resx` (5 lingue) — pianificato |

### Struttura del Progetto (attuale)

```
BrickCollection/
├── BrickCollection.sln
├── src/
│   └── BrickCollection/
│       ├── Models/
│       │   └── BricksetModels.cs      # BricksetSet, BricksetImage, DTO risposte grezze
│       ├── Services/
│       │   ├── BricksetAuthService.cs # LoginAsync, GetOwnedSetsAsync
│       │   └── CacheService.cs        # Persistenza SQLite (CachedSet, SaveSetsAsync, GetCachedSetsAsync)
│       ├── Views/
│       │   └── SetDetailPage.xaml(.cs)
│       ├── MainPage.xaml(.cs)         # Lista collezione (CollectionView), caricamento cache-first, pulsante refresh in toolbar
│       ├── App.xaml.cs                # SQLitePCL.Batteries_V2.Init(); override CreateWindow(...)
│       ├── Secrets.cs                 # escluso da git: API key + credenziali
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

Aggiunte pianificate: operazioni di scrittura verso Brickset (Step 6), grafici/statistiche avanzate (Step 7).

### Flusso Dati (attuale)

1. **Avvio app**: `App()` chiama `SQLitePCL.Batteries_V2.Init()` prima che qualsiasi altro codice tocchi SQLite; `CreateWindow(...)` è l'unica fonte della pagina iniziale (non impostare anche `MainPage` — vedi `docs/TROUBLESHOOTING.md`)
2. **Caricamento cache-first**: `MainPage.InitializeAsync()` controlla prima `CacheService.GetCachedSetsAsync()`; se la cache ha dati, vengono mostrati immediatamente senza chiamata API
3. **Refresh (manuale)**: un pulsante in toolbar attiva `RefreshFromApiAsync()` → `BricksetAuthService.LoginAsync()` → `GetOwnedSetsAsync()` → `CacheService.SaveSetsAsync()` (sovrascrive la cache) → nuova visualizzazione
4. **Navigazione dettaglio**: toccare un elemento della lista chiama `Navigation.PushAsync(new SetDetailPage(selectedSet))`, passando direttamente l'oggetto `BricksetSet` già scaricato (nessuna chiamata API/DB aggiuntiva necessaria)

### Decisioni di Design Chiave

- **Target solo Android**: semplifica il `.csproj` ed evita di mantenere percorsi di codice specifici iOS/Mac
- **Nessun framework MVVM per ora**: il binding diretto nel code-behind è stato più semplice e ha mantenuto ogni step testabile indipendentemente, coerentemente con l'approccio di sviluppo incrementale. MVVM (o almeno un layer ViewModel leggero) è previsto una volta che gli Step 6-7 aggiungano abbastanza logica da giustificarlo
- **Cache-first, refresh solo manuale**: dato il limite di chiamate API (100/giorno per `getSets`), l'app carica dalla cache SQLite locale a ogni avvio e chiama l'API solo quando l'utente tocca esplicitamente "Aggiorna" — mai automaticamente all'avvio
- **Segreti tramite `Secrets.cs` escluso da git**: più semplice del parsing di file di configurazione per un'app personale mono-utente in fase iniziale di sviluppo; vedi `docs/CONFIGURATION.md` per il ragionamento e il piano di refactor futuro
- **Solo `SQLitePCLRaw.bundle_green`, nessun riferimento nativo extra**: aggiungere riferimenti diretti ai pacchetti `SQLitePCLRaw.lib.e_sqlite3*` insieme al bundle causa conflitti tra librerie native su Android — vedi `docs/TROUBLESHOOTING.md`
