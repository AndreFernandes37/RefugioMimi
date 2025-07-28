# Deploy

1. Publique a aplicação:

```bash
dotnet publish -c Release
```

2. Copie o conteúdo da pasta `bin/Release/net8.0/publish` para o servidor.

3. Crie um serviço systemd `/etc/systemd/system/refugiodamimi.service`:

```ini
[Unit]
Description=Refugio da Mimi
After=network.target

[Service]
WorkingDirectory=/var/www/refugiodamimi
ExecStart=/var/www/refugiodamimi/RefugioMimi
Restart=always
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```

4. Ative o serviço:

```bash
sudo systemctl enable --now refugiodamimi
```

5. Configure Nginx como reverse proxy para `http://localhost:5000` e habilite SSL com Certbot.
