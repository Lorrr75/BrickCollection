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

### Notes
- Local dev credentials are stored in a git-ignored `Secrets.cs` file, not `appsettings.local.json` as originally documented — see `docs/CONFIGURATION.md` / Le credenziali di sviluppo locale sono salvate in un file `Secrets.cs` escluso da git, non in `appsettings.local.json` come originariamente documentato — vedi `docs/CONFIGURATION.md`
