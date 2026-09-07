# BrickCollection

**[English](README.md) | [Italiano](README.it.md)**

![Platform](https://img.shields.io/badge/platform-Android-3DDC84?logo=android)
![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-C%23-512BD4?logo=dotnet)
![License](https://img.shields.io/badge/license-EUPL--1.2-blue)
![Status](https://img.shields.io/badge/status-in%20sviluppo-yellow)

Un'app personale per la gestione della propria collezione LEGO® su Android, sviluppata in .NET MAUI, che si collega al proprio account [Brickset](https://brickset.com) per sfogliare, tracciare e analizzare la collezione.

> LEGO® è un marchio registrato del LEGO Group, che non sponsorizza, autorizza o approva questo progetto.

## Funzionalità

- Accesso tramite account Brickset
- Sfoglia i set posseduti e desiderati
- Visualizza informazioni dettagliate sui set (pezzi, minifigure, anno, tema, prezzo di listino)
- Statistiche sulla collezione (totale set, totale pezzi, spesa per tema/anno)
- Cache locale (SQLite) per un uso efficiente offline e ridurre le chiamate API
- Interfaccia multilingua: Inglese, Italiano, Spagnolo, Francese, Tedesco

## Screenshot

*(in arrivo)*

## Per Iniziare

### Prerequisiti

- Visual Studio 2026 con il carico di lavoro **.NET Multi-platform App UI development**
- Android SDK (API 34+) ed emulatore o dispositivo fisico
- Una [API key Brickset](https://brickset.com/tools/webservices/requestkey) gratuita
- Un account Brickset (per autenticazione e accesso alla collezione personale)

### Configurazione

1. Clona il repository
   ```
   git clone https://github.com/<tuo-username>/BrickCollection.git
   ```
2. Apri `BrickCollection.sln` in Visual Studio
3. Aggiungi la tua API key Brickset (vedi [Configurazione](docs/CONFIGURATION.md))
4. Imposta il target su un emulatore o dispositivo Android
5. Compila ed esegui

## Architettura del Progetto

Vedi [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) per dettagli sulla struttura dell'app, flusso dati e decisioni di design.

## Roadmap

Lo sviluppo segue un approccio incrementale, passo dopo passo:

1. Autenticazione (login Brickset + user hash)
2. Fetch della collezione (REST + parsing JSON)
3. UI lista (set posseduti/desiderati)
4. Vista dettaglio set + statistiche base
5. Cache locale SQLite
6. Operazioni di scrittura (aggiornamento stato posseduto/desiderato)
7. Statistiche avanzate e grafici

Vedi [docs/ROADMAP.md](docs/ROADMAP.md) per la suddivisione completa.

## Contribuire

Questo è attualmente un progetto personale, ma suggerimenti e segnalazioni di bug sono benvenuti — vedi [CONTRIBUTING.md](CONTRIBUTING.md).

## Licenza

Questo progetto è rilasciato sotto **European Union Public Licence v1.2 (EUPL-1.2)** — vedi [LICENSE](LICENSE) per i dettagli.

## Ringraziamenti

- [Brickset](https://brickset.com) per l'API che rende possibile questo progetto
- Sviluppato con il supporto di design e sviluppo di Claude (Anthropic)
