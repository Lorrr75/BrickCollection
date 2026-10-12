# Roadmap

**[English](#english) | [Italiano](#italiano)**

---

## English

Development follows a strict incremental approach: each step is built, tested, and verified independently before moving to the next.

- [x] **Step 1 — Authentication**
  Brickset login (`login` method), user hash retrieval, verified via Debug output. `BricksetAuthService.LoginAsync()`.
- [x] **Step 2 — Collection Fetch**
  `getSets` API call (`owned:1`, `pageSize:500`), JSON deserialization into `BricksetSet` models, verified via Debug output (25 → 27 sets, matching live changes made on Brickset.com).
- [x] **Step 3 — List UI**
  `CollectionView` displaying owned sets (thumbnail, name, number, year, piece count), verified with a status counter (27/27 loaded matching API total).
- [x] **Step 4 — Detail View & Basic Stats**
  Separate detail page (`SetDetailPage`, navigation on tap via `Navigation.PushAsync`); aggregate stats (total pieces, total minifigs) shown on the list page.
- [x] **Step 5 — Local Caching**
  SQLite persistence (`CacheService`), cache-first load with a manual "Refresh" toolbar button. Verified with a cold app restart (list loads instantly from cache, no API call). See `docs/TROUBLESHOOTING.md` for two packaging/init bugs hit and resolved along the way.
- [x] **Step 6 — Write Operations**
  `setCollection` call from the detail page: a "Wishlist" switch writes the `wanted` flag back to Brickset, with write-through to the local SQLite cache and the in-memory object. Verified on the server side and after a real app process kill (`adb shell am force-stop`). Only `wanted` is implemented so far; toggling `owned` is not (it would remove the set from the owned list on next refresh, so it deserves its own design).
- [ ] **Step 7 — Advanced Statistics**
  Charts by theme/year, collection value trends (planned library: `Microcharts.Maui`, SkiaSharp-based)

## Italiano

Lo sviluppo segue un approccio rigorosamente incrementale: ogni step viene costruito, testato e verificato singolarmente prima di passare al successivo.

- [x] **Step 1 — Autenticazione**
  Login Brickset (metodo `login`), recupero user hash, verificato via output Debug. `BricksetAuthService.LoginAsync()`.
- [x] **Step 2 — Fetch Collezione**
  Chiamata API `getSets` (`owned:1`, `pageSize:500`), deserializzazione JSON in modelli `BricksetSet`, verificato via output Debug (25 → 27 set, coerente con modifiche live fatte su Brickset.com).
- [x] **Step 3 — UI Lista**
  `CollectionView` con i set posseduti (miniatura, nome, numero, anno, numero pezzi), verificato con un contatore di stato (27/27 caricati, coerente col totale API).
- [x] **Step 4 — Vista Dettaglio e Statistiche Base**
  Pagina dettaglio separata (`SetDetailPage`, navigazione al tap via `Navigation.PushAsync`); statistiche aggregate (totale pezzi, totale minifigure) mostrate nella pagina lista.
- [x] **Step 5 — Cache Locale**
  Persistenza SQLite (`CacheService`), caricamento cache-first con pulsante "Aggiorna" manuale in toolbar. Verificato con riavvio a freddo dell'app (la lista carica istantaneamente da cache, nessuna chiamata API). Vedi `docs/TROUBLESHOOTING.md` per due bug di packaging/inizializzazione incontrati e risolti lungo il percorso.
- [x] **Step 6 — Operazioni di Scrittura**
  Chiamata `setCollection` dalla pagina dettaglio: un interruttore "Lista desideri" scrive il flag `wanted` su Brickset, con write-through sulla cache SQLite locale e sull'oggetto in memoria. Verificato lato server e dopo un vero kill del processo dell'app (`adb shell am force-stop`). Per ora è implementato solo `wanted`; l'alternanza di `owned` no (toglierebbe il set dalla lista dei posseduti al refresh successivo, quindi merita un design a parte).
- [ ] **Step 7 — Statistiche Avanzate**
  Grafici per tema/anno, andamento valore collezione (libreria prevista: `Microcharts.Maui`, basata su SkiaSharp)
