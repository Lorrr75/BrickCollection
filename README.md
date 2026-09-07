# BrickCollection

**[English](README.md) | [Italiano](README.it.md)**

![Platform](https://img.shields.io/badge/platform-Android-3DDC84?logo=android)
![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-C%23-512BD4?logo=dotnet)
![License](https://img.shields.io/badge/license-EUPL--1.2-blue)
![Status](https://img.shields.io/badge/status-in%20development-yellow)

A personal LEGO® collection manager for Android, built with .NET MAUI, that connects to your [Brickset](https://brickset.com) account to browse, track, and analyze your collection.

> LEGO® is a trademark of the LEGO Group, which does not sponsor, authorize, or endorse this project.

## Features

- Sign in with your Brickset account
- Browse your owned and wanted sets
- View detailed set information (pieces, minifigs, year, theme, retail price)
- Collection statistics (total sets, total pieces, spend by theme/year)
- Offline-friendly local caching (SQLite) to minimize API calls
- Multi-language UI: English, Italian, Spanish, French, German

## Screenshots

*(coming soon)*

## Getting Started

### Prerequisites

- Visual Studio 2022/2026 with the **.NET Multi-platform App UI development** workload
- Android SDK (API 34+) and an emulator or physical device
- A free [Brickset API key](https://brickset.com/tools/webservices/requestkey)
- A Brickset account (for authentication and personal collection access)

### Setup

1. Clone the repository
   ```
   git clone https://github.com/<your-username>/BrickCollection.git
   ```
2. Open `BrickCollection.sln` in Visual Studio
3. Add your Brickset API key (see [Configuration](docs/CONFIGURATION.md))
4. Set the target to an Android emulator or device
5. Build and run

## Project Architecture

See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) for details on the app structure, data flow, and design decisions.

## Roadmap

Development follows an incremental, step-by-step approach:

1. Authentication (Brickset login + user hash)
2. Collection fetch (REST + JSON parsing)
3. List UI (owned/wanted sets)
4. Set detail view + basic statistics
5. Local SQLite caching
6. Write operations (update owned/wanted status)
7. Advanced statistics & charts

See [docs/ROADMAP.md](docs/ROADMAP.md) for the full breakdown.

## Contributing

This is currently a personal project, but suggestions and bug reports are welcome — see [CONTRIBUTING.md](CONTRIBUTING.md).

## License

This project is licensed under the **European Union Public Licence v1.2 (EUPL-1.2)** — see [LICENSE](LICENSE) for details.

## Acknowledgments

- [Brickset](https://brickset.com) for the API that makes this project possible
- Built with design and development support from Claude (Anthropic)
