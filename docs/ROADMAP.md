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
- [ ] **Step 6 — Write Operations**
  Update owned/wanted status back to Brickset
- [ ] **Step 7 — Advanced Statistics**
  Charts by theme/year, collection value trends

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
- [ ] **Step 6 — Operazioni di Scrittura**
  Aggiornamento stato posseduto/desiderato verso Brickset
- [ ] **Step 7 — Statistiche Avanzate**
  Grafici per tema/anno, andamento valore collezione
