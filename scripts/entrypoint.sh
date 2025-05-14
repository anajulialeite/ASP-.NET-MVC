#!/bin/bash
set -e

# Verifica se o banco de dados precisa ser inicializado
if [ "$INIT_DATABASE" = "true" ]; then
  /app/scripts/init-db.sh
fi

# Inicia a aplicação
echo "Iniciando a aplicação ASP.NET Core..."
exec dotnet LanchesMac.dll