# Refúgio da Mimi

Site de reservas em ASP.NET Core 8.0 com integração Stripe e MySQL.

## Desenvolvimento

1. Configure `appsettings.json` com a ligação MySQL e chaves Stripe.
2. Execute as migrações:

```bash
dotnet ef migrations add Init
dotnet ef database update
```

3. Inicie a aplicação:

```bash
dotnet run
```

A área de administração está em `/Admin/Reservas`.

## Deploy

```bash
dotnet publish -c Release
```
Crie um serviço `systemd` apontando para o executável em `bin/Release/net8.0/publish` e configure o Nginx/Apache como reverse proxy para Kestrel.
