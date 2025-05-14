# ASP .NET MVC

<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/csharp/csharp-original.svg" align="left" width="50" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/dotnetcore/dotnetcore-original.svg" align="left" width="50" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/visualstudio/visualstudio-original.svg" align="left" width="50" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/css3/css3-plain.svg" align="left" width="50" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/bootstrap/bootstrap-original.svg" align="left" width="50" height="50"/>        
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/html5/html5-plain.svg" align="left" width="50" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/javascript/javascript-plain.svg" align="center" width="50" height="50"/>      
          
Este curso foi feito para colocar em prática os conhecimentos em <strong>ASP .NET MVC</strong> em um site para vendas de lanche funcional. Vou criar do zero um site web dinâmico e aprender diversos conceitos relacionados ao desenvolvimento web usando a tecnologia <strong>ASP .NET MVC</strong> e o <strong>Entity Framework Core</strong>. Vou aprender a implementar o padrão <strong>MVC</strong>, definir as entidades do modelo de domínio usando o <strong>Entity Framework Core</strong>, definir a validação e configuração das entidades usando o <strong>Data Annotations</strong>, realizar a migração para criar o banco de dados e as tabelas usando a abordagem <strong>Code-First</strong>, cadastrar as tabelas do banco de dados, usar o padrão repository e o padrão <strong>ViewModel</strong>, trabalhar com <strong>Session</strong> criando um carrinho de compras, definir rotas na aplicação, usar <strong>Views Components</strong> no projeto, implementar a segurança usando <strong>ASP .NET Core Identity</strong> criando o Login, o registro e o Logout do usuário, criar e usar o <strong>Partial Views</strong>, realizar a paginação e filtro de dados, criar relatórios criando consultas <strong>LINQ</strong>, criar gráficos usando o <strong>GoogleChart</strong>, criar relatórios no formato PDF usando o <strong>FastReport OpenSource</strong>.

<img src="Imagem/LanchesMac.png" alt="lanches" align="center" width="300">

# Instruções para Executar

## Opção 1: Executar Localmente

### Configuração do Banco de Dados Local
Para executar a aplicação localmente, você pode:

1. **Usar o SQL Server local**: Configure o arquivo `appsettings.Local.json` com a conexão para sua instância do SQL Server

2. **Usar o SQL Server no Docker**: Execute apenas o contêiner do banco de dados:
   ```bash
   docker-compose up db
   ```

### Configuração dos Arquivos de Configuração
A aplicação usa diferentes arquivos de configuração:

- **appsettings.json**: Configuração base, usada quando nenhuma outra configuração específica é encontrada
- **appsettings.Development.json**: Configurações para ambiente de desenvolvimento
- **appsettings.Local.json**: Configurações para execução local (`dotnet run`)
- **appsettings.Production.json**: Configurações para ambiente de produção

> **Importante**: Para desenvolvimento local, copie e modifique o arquivo `appsettings.Local.json` para apontar para seu banco de dados local.

### Passos para Execução Local

Clone este repositório:
```bash
git clone https://github.com/anajulialeite/ASP-.NET-MVC.git
```

Navegue até a pasta do projeto:
```bash
cd ASP-.NET-MVC
```

Restaure as dependências:
```bash
dotnet restore
```

Compile o projeto:
```bash
dotnet build
```

Execute a aplicação:
```bash
dotnet run
```

Acesso no navegador:
```
https://localhost:7064
```

## Opção 2: Executar com Docker

### Configuração do Ambiente

1. Primeiro, copie o arquivo de exemplo de variáveis de ambiente:
```bash
cp .env.example .env
```

2. (Opcional) Edite o arquivo `.env` para personalizar as configurações:
```bash
nano .env  # ou use seu editor de texto preferido
```

3. Para ambiente de produção, crie também o arquivo `.env.prod`:
```bash
cp .env.example .env.prod
```
   E altere as configurações conforme necessário, especialmente a senha do banco de dados.

### Iniciar a Aplicação

1. Inicie a aplicação em ambiente de desenvolvimento:
```bash
docker-compose up -d
```

2. Ou, para ambiente de produção:
```bash
docker-compose -f docker-compose.prod.yml up -d
```

3. Acesse no navegador:
```
http://localhost:7064
```

# Instalação do Docker

## Windows

1. Baixe o Docker Desktop para Windows: [Download Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. Execute o instalador e siga as instruções na tela
3. O Docker Desktop inclui o Docker Engine, Docker CLI, Docker Compose, e outras ferramentas

Para mais detalhes, consulte a [documentação oficial do Docker para Windows](https://docs.docker.com/desktop/install/windows-install/)

## Ubuntu/Debian

```bash
# Instalar pacotes necessários
sudo apt-get update
sudo apt-get install ca-certificates curl gnupg lsb-release

# Adicionar a chave GPG oficial do Docker
sudo mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg

# Adicionar o repositório
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# Instalar Docker Engine e Docker Compose
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin
```

## Red Hat/CentOS/Fedora

```bash
# Instalar pacotes necessários
sudo yum install -y yum-utils

# Configurar o repositório
sudo yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo

# Instalar Docker Engine e Docker Compose
sudo yum install docker-ce docker-ce-cli containerd.io docker-compose-plugin
```

Para mais detalhes, consulte a:
- [Documentação Docker para Ubuntu](https://docs.docker.com/engine/install/ubuntu/)
- [Documentação Docker para CentOS](https://docs.docker.com/engine/install/centos/)

# Estrutura do Projeto Docker

## Arquivos de Configuração

### Arquivos de Configuração da Aplicação

#### appsettings.json
Arquivo de configuração base da aplicação:
- Contém configurações padrão usadas em todos os ambientes
- Usado quando configurações específicas não são encontradas

#### appsettings.Development.json
Configurações específicas para o ambiente de desenvolvimento:
- Usado quando a aplicação é executada com a variável de ambiente `ASPNETCORE_ENVIRONMENT=Development`
- Configurado para uso em ambiente Docker de desenvolvimento

#### appsettings.Local.json
Configurações para desenvolvimento local:
- Usado quando executando com `dotnet run` na máquina local do desenvolvedor
- Deve ser configurado para apontar para o banco SQL Server local ou em contêiner
- Este arquivo não é versionado no Git (está no .gitignore)

#### appsettings.Production.json
Configurações para ambiente de produção:
- Usado quando a aplicação é executada com a variável de ambiente `ASPNETCORE_ENVIRONMENT=Production`
- Contém configurações otimizadas para produção

### Arquivos de Configuração Docker

#### Dockerfile
O Dockerfile define como a imagem Docker da aplicação é construída:
- Usa a imagem oficial do .NET SDK para compilar a aplicação
- Usa a imagem .NET Runtime como base para a imagem final
- Instala ferramentas necessárias para o SQL Server
- Configura variáveis de ambiente e diretórios
- Define o ponto de entrada como o script entrypoint.sh

#### docker-compose.yml
Configura os serviços necessários para a aplicação em ambiente de desenvolvimento:
- **web**: O serviço da aplicação ASP.NET Core
- **db**: Banco de dados SQL Server
- Volumes para persistência de dados
- Configurações de rede e variáveis de ambiente

#### docker-compose.prod.yml
Configuração específica para ambiente de produção:
- Utiliza otimizações para produção
- Configurações de segurança adicionais
- Referencia arquivo .env.prod para configurações de ambiente

#### .env, .env.prod e .env.example
Arquivos de variáveis de ambiente:
- **.env.example**: Modelo com as configurações possíveis
- **.env**: Configurações para ambiente de desenvolvimento
- **.env.prod**: Configurações para ambiente de produção

## Scripts

### Inicialização do Banco de Dados

#### scripts/entrypoint.sh
Script principal que é executado quando o container da aplicação inicia:
- Define a variável `DOTNET_RUNNING_IN_CONTAINER=true` para que a aplicação saiba que está em contêiner
- Verifica se o banco de dados precisa ser inicializado com a variável `INIT_DATABASE=true` 
- Executa o script init-db.sh se necessário
- Inicia a aplicação ASP.NET Core

#### scripts/init-db.sh
Script responsável pela inicialização do banco de dados:
- Espera o SQL Server ficar disponível
- Cria o banco de dados se não existir
- Cria todas as tabelas necessárias e insere dados iniciais
- Cria a tabela `__EFMigrationsHistory` e registra todas as migrações como aplicadas
- Implementa lógica para evitar criação duplicada

### Abordagem de Migração de Banco de Dados

O projeto usa uma abordagem híbrida para gerenciar o banco de dados:

#### Em Ambiente Docker:
- Usa scripts SQL (`init-db.sh`) para criar rapidamente a estrutura do banco na inicialização
- Cria tabelas e insere dados iniciais em um único passo
- Registra todas as migrações como já aplicadas para evitar que o EF Core tente aplicá-las novamente
- Permite que novas migrações (adicionadas após a criação dos scripts) sejam aplicadas automaticamente

#### Em Desenvolvimento Local (dotnet run):
- Usa o Entity Framework Core Migrations para gerenciar o banco de dados
- A aplicação detecta que não está em um contêiner (ausência de `DOTNET_RUNNING_IN_CONTAINER=true`)
- As migrações do EF Core são aplicadas automaticamente ao iniciar a aplicação
- Permite ao desenvolvedor trabalhar com o fluxo tradicional de EF Core

# Pré-requisitos

Para execução local:
- .NET SDK 9.0 ou superior
- Um editor como Visual Studio 2022 ou Visual Studio Code

Para execução com Docker:
- Docker ([Instalação para Windows](https://docs.docker.com/desktop/install/windows-install/))
- Docker Compose (incluído no Docker Desktop para Windows)

# License

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE)
