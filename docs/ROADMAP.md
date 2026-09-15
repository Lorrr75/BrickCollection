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
- [ ] **Step 4 — Detail View & Basic Stats**
  Separate detail page (navigation on tap); aggregate stats (total sets, total pieces, total spend)
- [ ] **Step 5 — Local Caching**
  SQLite persistence, manual refresh only, to respect API rate limits
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
- [ ] **Step 4 — Vista Dettaglio e Statistiche Base**
  Pagina dettaglio separata (navigazione al tap); statistiche aggregate (totale set, totale pezzi, spesa totale)
- [ ] **Step 5 — Cache Locale**
  Persistenza SQLite, refresh solo manuale, per rispettare i limiti dell'API
- [ ] **Step 6 — Operazioni di Scrittura**
  Aggiornamento stato posseduto/desiderato verso Brickset
- [ ] **Step 7 — Statistiche Avanzate**
  Grafici per tema/anno, andamento valore collezione
