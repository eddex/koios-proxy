# Koios Proxy

Simple proxy to make requests to the public Cardano [Koios API](https://api.koios.rest)
from your web3 Dapp frontend.

## Run from project

1. `dotnet run --project .\KoiosProxy\`
2. Any request to `http://localhost:5001/*` will be forwarded to `https://api.koios.rest/*`

## Using Docker

1. Build image: `docker build -t eddex/koios-proxy -f .\KoiosProxy\Dockerfile .`
2. Run container: `docker run --rm -p 5001:80 eddex/koios-proxy:latest`

**Environment variables**

| Name                   | Default      | Valid values                                           | Description                                               |
| ---------------------- | ------------ | ------------------------------------------------------ | --------------------------------------------------------- |
| ASPNETCORE_ENVIRONMENT | `Production` | `Development`, `Production`                            | Defines which appsettings.json configuration file is used |
| ALLOWED_ORIGINS        | `*`          | Comma separated list of allowed domains (`*`=wildcard) | CORS allowed origins                                      |

Example: Use `appsettings.Development.json` and allow requests from `a.io` as well as all subdomains of `b.ch`.

```bash
docker run --rm -p 5001:80 \
-e "ASPNETCORE_ENVIRONMENT=Development" \
-e "ALLOWED_ORIGINS=a.io,*.b.ch" \
eddex/koios-proxy:latest
```

## Attributions

- powered by [YARP](https://github.com/microsoft/reverse-proxy)
