# Changelog

All notable changes to this project will be documented in this file.
Tutte le modifiche rilevanti a questo progetto saranno documentate in questo file.

Format based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- Initial repository structure and documentation / Struttura iniziale del repository e documentazione
- Brickset authentication service (`login` method) / Servizio di autenticazione Brickset (metodo `login`)
- Collection fetch via `getSets` (owned sets, full details) / Fetch della collezione tramite `getSets` (set posseduti, dettagli completi)
- Collection list UI (`CollectionView` with thumbnail, name, year, piece count) / UI lista collezione (`CollectionView` con miniatura, nome, anno, numero pezzi)
- Set detail page with navigation on tap / Pagina dettaglio set con navigazione al tap
- Basic aggregate statistics (total pieces, total minifigs) / Statistiche aggregate base (totale pezzi, totale minifigure)
- Local SQLite caching with cache-first load and manual refresh / Cache locale SQLite con caricamento cache-first e refresh manuale
- Write operation: "Wishlist" switch on the detail page updates the `wanted` flag on Brickset via `setCollection`, with write-through to the local cache / Operazione di scrittura: interruttore "Lista desideri" nella pagina dettaglio che aggiorna il flag `wanted` su Brickset tramite `setCollection`, con write-through sulla cache locale
- Cache now stores `Wanted` / `QtyWanted` so the switch reflects the correct state when loading from cache / La cache ora salva `Wanted` / `QtyWanted` così l'interruttore riflette lo stato corretto quando si carica dalla cache

### Fixed
- Resolved `DllNotFoundException: e_sqlite3` / `libc.so.6 not found` crash on Android, caused by extraneous direct `PackageReference`s to `SQLitePCLRaw.lib.e_sqlite3` and `SQLitePCLRaw.lib.e_sqlite3.android` conflicting with `SQLitePCLRaw.bundle_green`'s own bundled native libraries — see `docs/TROUBLESHOOTING.md` / Risolto crash `DllNotFoundException: e_sqlite3` / `libc.so.6 not found` su Android, causato da `PackageReference` dirette superflue a `SQLitePCLRaw.lib.e_sqlite3` e `SQLitePCLRaw.lib.e_sqlite3.android` in conflitto con le librerie native già incluse da `SQLitePCLRaw.bundle_green` — vedi `docs/TROUBLESHOOTING.md`
- Resolved `InvalidOperationException: Both MainPage was set and CreateWindow was overridden` crash caused by setting `MainPage` in `App()` while also overriding `CreateWindow` / Risolto crash `InvalidOperationException: Both MainPage was set and CreateWindow was overridden` causato dall'impostare `MainPage` in `App()` avendo anche sovrascritto `CreateWindow`
- `setCollection` showed no change on Brickset when the `params` JSON carried numeric values (`{"want":1}`); sending string values as in the official examples (`{"want":"1"}`) fixed it / `setCollection` non produceva alcuna modifica su Brickset quando il JSON di `params` conteneva valori numerici (`{"want":1}`); inviare valori stringa come negli esempi ufficiali (`{"want":"1"}`) ha risolto
- The wishlist switch only wrote to the server, so a cache-loaded list showed stale state after a restart; the write now also updates the cache row and the in-memory object / L'interruttore scriveva solo sul server, quindi una lista caricata da cache mostrava uno stato non aggiornato dopo un riavvio; ora la scrittura aggiorna anche la riga di cache e l'oggetto in memoria

### Notes
- Local dev credentials are stored in a git-ignored `Secrets.cs` file, not `appsettings.local.json` as originally documented — see `docs/CONFIGURATION.md` / Le credenziali di sviluppo locale sono salvate in un file `Secrets.cs` escluso da git, non in `appsettings.local.json` come originariamente documentato — vedi `docs/CONFIGURATION.md`
