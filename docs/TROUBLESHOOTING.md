# Troubleshooting Log / Registro Problemi Risolti

**[English](#english) | [Italiano](#italiano)**

---

## English

Real issues hit during development, with root cause and fix, kept here so we don't re-debug them later.

### Android emulator: "Failed to create AVD" / snapshot `.ini` file not found

**Symptom**: creating an AVD (`pixel_7_-_api_36_0`) failed with an I/O error, the referenced `.ini` file never existed, and `sdkmanager`/`avdmanager` from the legacy `sdk/tools/bin` folder crashed with `NoClassDefFoundError: javax/xml/bind/annotation/XmlSchema`.

**Cause**: the legacy Android command-line tools (`tools/bin`) depend on `javax.xml.bind` (JAXB), removed from the JDK since Java 11. With a modern JDK installed, these legacy tools can't run at all — so the AVD creation silently failed upstream, before even reaching a real system image.

**Fix**: install the modern **Command Line Tools** package from https://developer.android.com/studio#command-tools, extracted so that `sdkmanager.bat` ends up at `sdk/cmdline-tools/latest/bin/`. Then install packages explicitly:
```
sdkmanager --install "platform-tools" "platforms;android-36" "build-tools;36.0.0" "emulator" "system-images;android-36;google_apis_playstore;x86_64"
avdmanager create avd -n <name> -k "system-images;android-36;google_apis_playstore;x86_64"
```
The old `tools/` folder can be removed once the new `cmdline-tools` path is confirmed working, to avoid Visual Studio getting confused between the two.

### SQLite crash on Android: `DllNotFoundException: e_sqlite3` / `dlopen failed: library "libc.so.6" not found`

**Symptom**: `SQLitePCL.Batteries_V2.Init()` threw `System.TypeInitializationException` → `DllNotFoundException: e_sqlite3`. Logcat showed the real cause: `dlopen failed: library "libc.so.6" not found: needed by .../libe_sqlite3.so`.

**Cause**: `libc.so.6` is the glibc (desktop Linux) libc SONAME — Android's Bionic libc is just `libc.so`. This meant the wrong native `.so` (built for desktop Linux x86_64, not Android x86_64) ended up in the APK. Root cause: two extraneous, manually-added `PackageReference`s —
```xml
<PackageReference Include="SQLitePCLRaw.lib.e_sqlite3" Version="3.53.3" />
<PackageReference Include="SQLitePCLRaw.lib.e_sqlite3.android" Version="2.1.13" />
```
— conflicting with the native libraries already bundled correctly by `SQLitePCLRaw.bundle_green`.

**Fix**: remove both extraneous `PackageReference`s, keep only:
```xml
<PackageReference Include="sqlite-net-pcl" Version="..." />
<PackageReference Include="SQLitePCLRaw.bundle_green" Version="..." />
```
then do a full clean (`dotnet nuget locals all --clear`, delete `bin`/`obj`), restore, and rebuild — a stale build can keep the wrong native library even after fixing the `.csproj`.

**Lesson**: never add direct `PackageReference`s to `SQLitePCLRaw.lib.e_sqlite3*` packages alongside `SQLitePCLRaw.bundle_green` — the bundle already includes the correct per-platform native libraries.

### MAUI crash: `InvalidOperationException: Both MainPage was set and CreateWindow was overridden`

**Symptom**: setting `MainPage = new AppShell();` in `App()`'s constructor, while the modern MAUI template also overrides `CreateWindow(...)`, causes this exception at Activity creation.

**Cause**: the .NET 10 MAUI template uses `CreateWindow` as the single source of truth for the initial page/window. Also setting `MainPage` directly is a legacy pattern from older MAUI versions — using both together is explicitly rejected by the framework.

**Fix**: remove the `MainPage = new AppShell();` line from `App()`. Keep only the `CreateWindow` override:
```csharp
protected override Window CreateWindow(IActivationState? activationState)
{
    return new Window(new AppShell());
}
```

## Italiano

Problemi reali incontrati durante lo sviluppo, con causa e soluzione, tenuti qui per non doverli ridebuggare in futuro.

### Emulatore Android: "Impossibile creare l'AVD" / file `.ini` dello snapshot non trovato

**Sintomo**: la creazione di un AVD (`pixel_7_-_api_36_0`) falliva con un errore di I/O, il file `.ini` referenziato non esisteva mai, e `sdkmanager`/`avdmanager` dalla cartella legacy `sdk/tools/bin` andavano in crash con `NoClassDefFoundError: javax/xml/bind/annotation/XmlSchema`.

**Causa**: gli strumenti a riga di comando Android legacy (`tools/bin`) dipendono da `javax.xml.bind` (JAXB), rimosso dal JDK dalla versione 11 in poi. Con un JDK moderno installato, questi tool legacy non possono proprio funzionare — quindi la creazione dell'AVD falliva silenziosamente a monte, prima ancora di arrivare a una vera system image.

**Soluzione**: installare il pacchetto moderno **Command Line Tools** da https://developer.android.com/studio#command-tools, estratto in modo che `sdkmanager.bat` finisca in `sdk/cmdline-tools/latest/bin/`. Poi installare i pacchetti esplicitamente:
```
sdkmanager --install "platform-tools" "platforms;android-36" "build-tools;36.0.0" "emulator" "system-images;android-36;google_apis_playstore;x86_64"
avdmanager create avd -n <nome> -k "system-images;android-36;google_apis_playstore;x86_64"
```
La vecchia cartella `tools/` può essere rimossa una volta confermato che il nuovo percorso `cmdline-tools` funziona, per evitare che Visual Studio si confonda tra i due.

### Crash SQLite su Android: `DllNotFoundException: e_sqlite3` / `dlopen failed: library "libc.so.6" not found`

**Sintomo**: `SQLitePCL.Batteries_V2.Init()` lanciava `System.TypeInitializationException` → `DllNotFoundException: e_sqlite3`. Logcat mostrava la vera causa: `dlopen failed: library "libc.so.6" not found: needed by .../libe_sqlite3.so`.

**Causa**: `libc.so.6` è il SONAME della libc di glibc (Linux desktop) — la libc Bionic di Android si chiama semplicemente `libc.so`. Questo significava che nell'APK era finita la libreria nativa `.so` sbagliata (compilata per Linux desktop x86_64, non per Android x86_64). Causa radice: due `PackageReference` extra aggiunte manualmente —
```xml
<PackageReference Include="SQLitePCLRaw.lib.e_sqlite3" Version="3.53.3" />
<PackageReference Include="SQLitePCLRaw.lib.e_sqlite3.android" Version="2.1.13" />
```
— in conflitto con le librerie native già incluse correttamente da `SQLitePCLRaw.bundle_green`.

**Soluzione**: rimuovere entrambe le `PackageReference` extra, lasciando solo:
```xml
<PackageReference Include="sqlite-net-pcl" Version="..." />
<PackageReference Include="SQLitePCLRaw.bundle_green" Version="..." />
```
poi fare una pulizia completa (`dotnet nuget locals all --clear`, cancellare `bin`/`obj`), ripristinare e ricompilare — una build non pulita può mantenere la libreria nativa sbagliata anche dopo aver corretto il `.csproj`.

**Lezione**: non aggiungere mai `PackageReference` dirette ai pacchetti `SQLitePCLRaw.lib.e_sqlite3*` insieme a `SQLitePCLRaw.bundle_green` — il bundle include già le librerie native corrette per ogni piattaforma.

### Crash MAUI: `InvalidOperationException: Both MainPage was set and CreateWindow was overridden`

**Sintomo**: impostare `MainPage = new AppShell();` nel costruttore di `App()`, mentre il template MAUI moderno sovrascrive già anche `CreateWindow(...)`, causa questa eccezione alla creazione dell'Activity.

**Causa**: il template MAUI di .NET 10 usa `CreateWindow` come unica fonte di verità per la pagina/finestra iniziale. Impostare anche `MainPage` direttamente è un pattern legacy di versioni precedenti di MAUI — usarli entrambi insieme viene esplicitamente rifiutato dal framework.

**Soluzione**: rimuovere la riga `MainPage = new AppShell();` da `App()`. Lasciare solo l'override di `CreateWindow`:
```csharp
protected override Window CreateWindow(IActivationState? activationState)
{
    return new Window(new AppShell());
}
```
