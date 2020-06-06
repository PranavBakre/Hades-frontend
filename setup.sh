#!/usr/bin/env bash

# shellcheck disable=SC2154

export DEBIAN_FRONTEND="noninteractive"
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update -y
sudo apt install apt-transport-https -y
sudo apt update -y
sudo apt install dotnet-sdk-3.1 -y

cd /tmp || exit
sudo certbot --noninteractive --nginx --agree-tos --email akhilnarang@thescriptgroup.in --domain charon.thescriptgroup.in
cat << EOF | sudo tee /etc/nginx/sites-available/charon.thescriptgroup.in
server {
    listen 80;
    server_name charon.thescriptgroup.in;
    location ^~ /.well-known/acme-challenge/ {
        root /var/www/html;
    }

    location / {
        return 301 https://charon.thescriptgroup.in$request_uri;
    }
}

server {
    listen 443 ssl;
    server_name charon.thescriptgroup.in;
    ssl_certificate /etc/letsencrypt/live/charon.thescriptgroup.in/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/charon.thescriptgroup.in/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;
    root /home/deploy/Hades-frontend/Hades-frontend/bin/Release/netstandard2.1/publish/wwwroot;
    index index.html;

    location / {
        try_files \$uri \$uri/ =404;
    }
}

EOF
sudo ln -s /etc/nginx/sites-available/charon.thescriptgroup.in /etc/nginx/sites-enabled/charon.thescriptgroup.in
sudo rm -fv /etc/nginx/sites-{available,enabled}/default
sudo nginx -s reload
cd - || exit
