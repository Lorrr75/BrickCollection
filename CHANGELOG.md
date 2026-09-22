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

### Fixed
- Resolved `DllNotFoundException: e_sqlite3` / `libc.so.6 not found` crash on Android, caused by extraneous direct `PackageReference`s to `SQLitePCLRaw.lib.e_sqlite3` and `SQLitePCLRaw.lib.e_sqlite3.android` conflicting with `SQLitePCLRaw.bundle_green`'s own bundled native libraries — see `docs/TROUBLESHOOTING.md` / Risolto crash `DllNotFoundException: e_sqlite3` / `libc.so.6 not found` su Android, causato da `PackageReference` dirette superflue a `SQLitePCLRaw.lib.e_sqlite3` e `SQLitePCLRaw.lib.e_sqlite3.android` in conflitto con le librerie native già incluse da `SQLitePCLRaw.bundle_green` — vedi `docs/TROUBLESHOOTING.md`
- Resolved `InvalidOperationException: Both MainPage was set and CreateWindow was overridden` crash caused by setting `MainPage` in `App()` while also overriding `CreateWindow` / Risolto crash `InvalidOperationException: Both MainPage was set and CreateWindow was overridden` causato dall'impostare `MainPage` in `App()` avendo anche sovrascritto `CreateWindow`

### Notes
- Local dev credentials are stored in a git-ignored `Secrets.cs` file, not `appsettings.local.json` as originally documented — see `docs/CONFIGURATION.md` / Le credenziali di sviluppo locale sono salvate in un file `Secrets.cs` escluso da git, non in `appsettings.local.json` come originariamente documentato — vedi `docs/CONFIGURATION.md`
