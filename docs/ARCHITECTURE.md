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
| Local Storage | SQLite (`sqlite-net-pcl`) — planned, Step 5 |
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
│       │   └── BricksetAuthService.cs # LoginAsync, GetOwnedSetsAsync
│       ├── MainPage.xaml(.cs)         # Collection list (CollectionView)
│       ├── Secrets.cs                 # git-ignored: API key + credentials
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

Planned additions: `Views/SetDetailPage.xaml` (Step 4), `Services/CacheService.cs` (Step 5).

### Data Flow (current)

1. **Authentication**: `MainPage` calls `BricksetAuthService.LoginAsync()` with credentials from `Secrets.cs` → Brickset `login` method → user hash
2. **Fetch**: `BricksetAuthService.GetOwnedSetsAsync()` calls `getSets` with `owned:1, pageSize:500` → JSON → deserialized into `BricksetSet` list
3. **Display**: `MainPage.xaml.cs` sets `SetsCollectionView.ItemsSource` directly to the fetched list (no ViewModel/MVVM layer yet — introduced pragmatically as the app grows, likely around Step 4-5)

### Key Design Decisions

- **Android-only target**: simplifies the `.csproj` and avoids maintaining iOS/Mac-specific code paths
- **No MVVM framework yet**: for the first 3 steps, direct code-behind binding was simpler and kept each step independently testable, per the incremental development approach. MVVM (or at least a lightweight ViewModel layer) is expected to be introduced once the detail page and stats add enough logic to justify it
- **SQLite caching is mandatory, not optional (Step 5)**: given the API rate limit (100 calls/day for `getSets`), the app must function primarily from local cache, refreshing only on explicit user action
- **Secrets via git-ignored `Secrets.cs`**: simpler than config-file parsing for a single-user personal app in early development; see `docs/CONFIGURATION.md` for the reasoning and future refactor plan

---

## Italiano

### Panoramica

BrickCollection è un'applicazione Android .NET MAUI che utilizza le [Brickset REST API v3](https://brickset.com/api/v3.asmx) per gestire una collezione personale di set LEGO.

### Stack Tecnologico

| Livello | Tecnologia |
|---|---|
| Framework UI | .NET MAUI (solo Android) |
| Linguaggio | C# |
| Storage Locale | SQLite (`sqlite-net-pcl`) — pianificato, Step 5 |
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
│       │   └── BricksetAuthService.cs # LoginAsync, GetOwnedSetsAsync
│       ├── MainPage.xaml(.cs)         # Lista collezione (CollectionView)
│       ├── Secrets.cs                 # escluso da git: API key + credenziali
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

Aggiunte pianificate: `Views/SetDetailPage.xaml` (Step 4), `Services/CacheService.cs` (Step 5).

### Flusso Dati (attuale)

1. **Autenticazione**: `MainPage` chiama `BricksetAuthService.LoginAsync()` con le credenziali da `Secrets.cs` → metodo `login` Brickset → user hash
2. **Fetch**: `BricksetAuthService.GetOwnedSetsAsync()` chiama `getSets` con `owned:1, pageSize:500` → JSON → deserializzato in lista `BricksetSet`
3. **Visualizzazione**: `MainPage.xaml.cs` imposta `SetsCollectionView.ItemsSource` direttamente sulla lista scaricata (nessun layer ViewModel/MVVM ancora — verrà introdotto pragmaticamente man mano che l'app cresce, probabilmente verso lo Step 4-5)

### Decisioni di Design Chiave

- **Target solo Android**: semplifica il `.csproj` ed evita di mantenere percorsi di codice specifici iOS/Mac
- **Nessun framework MVVM per ora**: per i primi 3 step, il binding diretto nel code-behind è stato più semplice e ha mantenuto ogni step testabile indipendentemente, coerentemente con l'approccio di sviluppo incrementale. MVVM (o almeno un layer ViewModel leggero) è previsto una volta che la pagina dettaglio e le statistiche aggiungano abbastanza logica da giustificarlo
- **Cache SQLite obbligatoria, non opzionale (Step 5)**: dato il limite di chiamate API (100/giorno per `getSets`), l'app deve funzionare principalmente dalla cache locale, aggiornando solo su azione esplicita dell'utente
- **Segreti tramite `Secrets.cs` escluso da git**: più semplice del parsing di file di configurazione per un'app personale mono-utente in fase iniziale di sviluppo; vedi `docs/CONFIGURATION.md` per il ragionamento e il piano di refactor futuro
