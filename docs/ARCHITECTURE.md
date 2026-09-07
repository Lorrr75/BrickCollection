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
| Local Storage | SQLite (`sqlite-net-pcl`) |
| HTTP / JSON | `HttpClient` + `System.Text.Json` |
| Localization | `.resx` resource files (5 languages) |

### Project Structure

```
BrickCollection/
├── BrickCollection.sln
├── src/
│   └── BrickCollection/
│       ├── Models/           # Data models (Set, Theme, Collection stats...)
│       ├── Services/         # BricksetApiService, CacheService, AuthService
│       ├── Views/            # XAML pages
│       ├── ViewModels/       # MVVM view models
│       ├── Resources/
│       │   └── Strings/      # AppResources.resx + .it/.es/.fr/.de
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

### Data Flow

1. **Authentication**: username/password → Brickset `login` method → user hash stored securely (SecureStorage)
2. **Fetch**: `getSets` API call filtered by collection ownership → JSON → deserialized into `Set` model objects
3. **Cache**: results persisted to local SQLite to respect the 100 calls/day API limit
4. **Display**: ViewModels bind cached/fetched data to XAML views via MVVM

### Key Design Decisions

- **Android-only target**: simplifies the `.csproj` and avoids maintaining iOS/Mac-specific code paths
- **SQLite caching is mandatory, not optional**: given the API rate limit, the app must function primarily from local cache, refreshing only on explicit user action
- **MVVM pattern**: keeps API/data logic separate from UI, consistent with the incremental, testable development approach used across this author's other projects (see AsmWorkbench)

---

## Italiano

### Panoramica

BrickCollection è un'applicazione Android .NET MAUI che utilizza le [Brickset REST API v3](https://brickset.com/api/v3.asmx) per gestire una collezione personale di set LEGO.

### Stack Tecnologico

| Livello | Tecnologia |
|---|---|
| Framework UI | .NET MAUI (solo Android) |
| Linguaggio | C# |
| Storage Locale | SQLite (`sqlite-net-pcl`) |
| HTTP / JSON | `HttpClient` + `System.Text.Json` |
| Localizzazione | File di risorse `.resx` (5 lingue) |

### Struttura del Progetto

```
BrickCollection/
├── BrickCollection.sln
├── src/
│   └── BrickCollection/
│       ├── Models/           # Modelli dati (Set, Theme, statistiche collezione...)
│       ├── Services/         # BricksetApiService, CacheService, AuthService
│       ├── Views/            # Pagine XAML
│       ├── ViewModels/       # ViewModel MVVM
│       ├── Resources/
│       │   └── Strings/      # AppResources.resx + .it/.es/.fr/.de
│       └── Platforms/
│           └── Android/
├── docs/
└── .github/
```

### Flusso Dati

1. **Autenticazione**: username/password → metodo `login` Brickset → user hash salvato in modo sicuro (SecureStorage)
2. **Fetch**: chiamata API `getSets` filtrata per set posseduti → JSON → deserializzato in oggetti modello `Set`
3. **Cache**: risultati salvati in SQLite locale per rispettare il limite di 100 chiamate/giorno dell'API
4. **Visualizzazione**: i ViewModel legano i dati cache/fetch alle viste XAML tramite MVVM

### Decisioni di Design Chiave

- **Target solo Android**: semplifica il `.csproj` ed evita di mantenere percorsi di codice specifici iOS/Mac
- **Cache SQLite obbligatoria, non opzionale**: dato il limite di chiamate API, l'app deve funzionare principalmente dalla cache locale, aggiornando solo su azione esplicita dell'utente
- **Pattern MVVM**: mantiene separata la logica API/dati dalla UI, coerentemente con l'approccio di sviluppo incrementale e testabile usato negli altri progetti dell'autore (vedi AsmWorkbench)
