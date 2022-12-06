# Koios Proxy

Simple proxy to make requests to the [Koios API](https://api.koios.rest).

## Usage

1. Navigate to the `KoiosProxy` directory: `cd KoiosProxy`
2. Start the proxy server: `dotnet run`
3. Any request to `http://localhost:5001/*` will be forwarded to `https://api.koios.rest/*`

## TODO

- [x] Reverse proxy for local frontend development using the Koios API
- [ ] Configure rate limits using YARP
- [ ] Add `appsettings.Production.json` with CORS
- [ ] Dockerize app

## Attributions

- powered by [YARP](https://github.com/microsoft/reverse-proxy)
