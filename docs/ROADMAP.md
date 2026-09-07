# Roadmap

**[English](#english) | [Italiano](#italiano)**

---

## English

Development follows a strict incremental approach: each step is built, tested, and verified independently before moving to the next.

- [ ] **Step 1 — Authentication**
  Brickset login (`getUserHash`), secure storage of the hash, console/debug verification
- [ ] **Step 2 — Collection Fetch**
  `getSets` API call, JSON deserialization into C# models, console-only verification (no UI yet)
- [ ] **Step 3 — List UI**
  `CollectionView` displaying owned sets (name, piece count, year, thumbnail)
- [ ] **Step 4 — Detail View & Basic Stats**
  Set detail page; aggregate stats (total sets, total pieces, total spend)
- [ ] **Step 5 — Local Caching**
  SQLite persistence, manual refresh only, to respect API rate limits
- [ ] **Step 6 — Write Operations**
  Update owned/wanted status back to Brickset
- [ ] **Step 7 — Advanced Statistics**
  Charts by theme/year, collection value trends

## Italiano

Lo sviluppo segue un approccio rigorosamente incrementale: ogni step viene costruito, testato e verificato singolarmente prima di passare al successivo.

- [ ] **Step 1 — Autenticazione**
  Login Brickset (`getUserHash`), salvataggio sicuro dello hash, verifica via console/debug
- [ ] **Step 2 — Fetch Collezione**
  Chiamata API `getSets`, deserializzazione JSON in modelli C#, verifica solo da console (nessuna UI ancora)
- [ ] **Step 3 — UI Lista**
  `CollectionView` con i set posseduti (nome, numero pezzi, anno, miniatura)
- [ ] **Step 4 — Vista Dettaglio e Statistiche Base**
  Pagina dettaglio set; statistiche aggregate (totale set, totale pezzi, spesa totale)
- [ ] **Step 5 — Cache Locale**
  Persistenza SQLite, refresh solo manuale, per rispettare i limiti dell'API
- [ ] **Step 6 — Operazioni di Scrittura**
  Aggiornamento stato posseduto/desiderato verso Brickset
- [ ] **Step 7 — Statistiche Avanzate**
  Grafici per tema/anno, andamento valore collezione
