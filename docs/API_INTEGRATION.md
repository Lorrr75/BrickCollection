# Brickset API Integration Notes / Note Integrazione API Brickset

**[English](#english) | [Italiano](#italiano)**

---

## English

Practical findings from integrating the [Brickset REST API v3](https://brickset.com/api/v3.asmx) (docs version 1.09, May 2026), beyond what the official documentation states directly.

### Endpoints used so far

**`login`** (POST, form-urlencoded)
- Params: `apiKey`, `username`, `password`
- Success: `{"status":"success","hash":"..."}`
- Error: `{"status":"error","message":"..."}`
- The returned hash is used as `userHash` in subsequent authenticated calls

**`getSets`** (POST, form-urlencoded)
- Params: `apiKey`, `userHash`, `params` (a JSON string, e.g. `{"owned":1,"pageSize":500}`)
- Success: `{"status":"success","matches":<n>,"sets":[...]}`
- Each set object includes `setID`, `number`, `name`, `year`, `theme`, `pieces`, `minifigs`, and a nested `image` object with `thumbnailURL`/`imageURL`
- `pageSize:500` is enough to fetch an entire personal collection (tested with 27 sets) in a single call

**`setCollection`** (POST, form-urlencoded)
- Params: `apiKey`, `userHash`, `setID`, `params` (a JSON string, e.g. `{"want":"1"}`)
- Supported keys inside `params` (per the official docs): `own`, `want`, `qtyOwned`, `qtyWanted`, `notes`, `rating`, `flag1`..`flag8`
- Success: `{"status":"success"}`; errors use the usual `{"status":"error","message":"..."}`
- **Gotcha**: the official examples pass the values as **strings** (`{"own":"1","qtyOwned":"6"}`). Our first attempt sent numbers (`{"want":1}`) and nothing changed on Brickset; switching to strings (`{"want":"1"}`) made the update work. Keep values as strings.
- `own` and `want` are independent flags: a set can be both owned and on the wishlist at the same time
- Like `login`, `setCollection` does not count against the `getSets` daily quota

### Rate limit behavior (important)

The 100 calls/day limit applies **only to `getSets`**. `login` and the newer `getCollection` method (added Jan 2026) do not count against it. This means:
- Fetching the full collection with one `getSets` call per session is cheap relative to the limit
- `getCollection` (lighter payload: setID + owned/wanted/qty only, no name/image/theme) could be used for frequent "did anything change?" checks without touching the quota — not yet implemented, potential future optimization
- As of Step 5, the app caches results locally (SQLite) and only calls `getSets` when the user explicitly taps "Refresh", so real-world consumption is minimal regardless

### JSON deserialization notes

`System.Text.Json` requires `PropertyNameCaseInsensitive = true` in `JsonSerializerOptions` because Brickset's JSON fields are camelCase (`setID`, `pieces`) while C# model properties are PascalCase (`SetID`, `Pieces`) — case-insensitive matching handles this without needing `[JsonPropertyName]` attributes on every property.

## Italiano

Riscontri pratici emersi integrando le [Brickset REST API v3](https://brickset.com/api/v3.asmx) (documentazione versione 1.09, maggio 2026), oltre a quanto dichiarato direttamente nella documentazione ufficiale.

### Endpoint usati finora

**`login`** (POST, form-urlencoded)
- Parametri: `apiKey`, `username`, `password`
- Successo: `{"status":"success","hash":"..."}`
- Errore: `{"status":"error","message":"..."}`
- Lo hash restituito viene usato come `userHash` nelle chiamate autenticate successive

**`getSets`** (POST, form-urlencoded)
- Parametri: `apiKey`, `userHash`, `params` (una stringa JSON, es. `{"owned":1,"pageSize":500}`)
- Successo: `{"status":"success","matches":<n>,"sets":[...]}`
- Ogni oggetto set include `setID`, `number`, `name`, `year`, `theme`, `pieces`, `minifigs`, e un oggetto annidato `image` con `thumbnailURL`/`imageURL`
- `pageSize:500` è sufficiente per scaricare un'intera collezione personale (testato con 27 set) in un'unica chiamata

**`setCollection`** (POST, form-urlencoded)
- Parametri: `apiKey`, `userHash`, `setID`, `params` (una stringa JSON, es. `{"want":"1"}`)
- Chiavi supportate dentro `params` (secondo la documentazione ufficiale): `own`, `want`, `qtyOwned`, `qtyWanted`, `notes`, `rating`, `flag1`..`flag8`
- Successo: `{"status":"success"}`; gli errori usano il solito `{"status":"error","message":"..."}`
- **Attenzione**: gli esempi ufficiali passano i valori come **stringhe** (`{"own":"1","qtyOwned":"6"}`). Il nostro primo tentativo inviava numeri (`{"want":1}`) e su Brickset non cambiava nulla; passando alle stringhe (`{"want":"1"}`) l'aggiornamento ha funzionato. Mantieni i valori come stringhe.
- `own` e `want` sono flag indipendenti: un set può essere contemporaneamente posseduto e nella lista desideri
- Come `login`, `setCollection` non consuma la quota giornaliera di `getSets`

### Comportamento dei limiti di chiamata (importante)

Il limite di 100 chiamate/giorno si applica **solo a `getSets`**. `login` e il più recente metodo `getCollection` (aggiunto a gennaio 2026) non consumano la quota. Questo significa:
- Scaricare l'intera collezione con una chiamata `getSets` per sessione è economico rispetto al limite
- `getCollection` (payload più leggero: solo setID + owned/wanted/quantità, niente nome/immagine/tema) potrebbe essere usato per controlli frequenti "è cambiato qualcosa?" senza toccare la quota — non ancora implementato, possibile ottimizzazione futura
- Dallo Step 5, l'app mette in cache i risultati localmente (SQLite) e chiama `getSets` solo quando l'utente tocca esplicitamente "Aggiorna", quindi il consumo reale è minimo in ogni caso

### Note sulla deserializzazione JSON

`System.Text.Json` richiede `PropertyNameCaseInsensitive = true` in `JsonSerializerOptions` perché i campi JSON di Brickset sono camelCase (`setID`, `pieces`) mentre le proprietà dei modelli C# sono PascalCase (`SetID`, `Pieces`) — il confronto case-insensitive gestisce questo senza bisogno di attributi `[JsonPropertyName]` su ogni proprietà.
