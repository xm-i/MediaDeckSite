# MediaDeckSite

This repository contains the official website and documentation for **MediaDeck**, structured with a Vue 3 + Vite frontend (`frontend/`) and an ASP.NET Core (.NET 10) backend (`backend/`).

## Directory Structure

- `frontend/` — Vue 3 + TypeScript + Vite SPA
- `backend/` — ASP.NET Core (`net10.0`) host for `/api/*` endpoints and static SPA delivery
- `infra/` — Ubuntu production infrastructure configs (systemd service, Nginx reverse proxy, sudoers, setup guide)
- `MediaDeckSite.sln` — .NET solution file

## Recommended IDE Setup

[VS Code](https://code.visualstudio.com/) + [Vue (Official)](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (and disable Vetur), plus C# Dev Kit or Visual Studio / Rider for `backend/`.

## Recommended Browser Setup

- Chromium-based browsers (Chrome, Edge, Brave, etc.):
  - [Vue.js devtools](https://chromewebstore.google.com/detail/vuejs-devtools/nhdogjmejiglipccpnnnanhbledajbpd)
  - [Turn on Custom Object Formatter in Chrome DevTools](http://bit.ly/object-formatters)
- Firefox:
  - [Vue.js devtools](https://addons.mozilla.org/en-US/firefox/addon/vue-js-devtools/)
  - [Turn on Custom Object Formatter in Firefox DevTools](https://fxdx.dev/firefox-devtools-custom-object-formatters/)

## Type Support for `.vue` Imports in TS

TypeScript cannot handle type information for `.vue` imports by default, so we replace the `tsc` CLI with `vue-tsc` for type checking. In editors, we need [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) to make the TypeScript language service aware of `.vue` types.

## Customize configuration

See [Vite Configuration Reference](https://vite.dev/config/).

## Project Setup

### Frontend (`frontend/`)

```sh
cd frontend
npm install
```

#### Compile and Hot-Reload for Development

In development, Vite proxies `/api` requests to `http://localhost:5000`.

```sh
cd frontend
npm run dev
```

#### Type-Check, Compile and Minify for Production

```sh
cd frontend
npm run build
```

### Backend (`backend/`)

#### Build the Solution

```sh
dotnet build MediaDeckSite.sln
```

#### Run the Backend Server (`http://localhost:5000`)

```sh
dotnet run --project backend/MediaDeckSite.csproj
```

#### Publish Combined Application (`backend` + `frontend/dist` into `publish/`)

```sh
cd frontend && npm run build && cd ..
dotnet publish backend/MediaDeckSite.csproj -c Release -o publish
```
