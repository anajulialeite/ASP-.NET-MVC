#!/bin/bash
set -e

# Em ambientes Docker, inicializa o banco de dados através do script init-db.sh
if [ "${INIT_DATABASE}" = "true" ]; then
    echo "Inicializando banco de dados via SQL (ambiente Docker)..."
    /app/scripts/init-db.sh
fi

echo "Iniciando a aplicação ASP.NET Core..."
# Define a variável de ambiente DOTNET_RUNNING_IN_CONTAINER para que a aplicação 
# saiba que está rodando em um contêiner Docker
export DOTNET_RUNNING_IN_CONTAINER=true

# Inicia a aplicação ASP.NET
exec dotnet LanchesMac.dll