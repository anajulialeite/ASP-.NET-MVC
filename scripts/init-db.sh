#!/bin/bash
set -e

# Carrega variáveis de ambiente
source /app/.env

# Espera o SQL Server estar pronto
echo "Aguardando SQL Server iniciar..."
sleep 15

# Verifica se o banco de dados existe, se não, cria
echo "Verificando banco de dados..."
/opt/mssql-tools/bin/sqlcmd -S $DB_HOST -U $DB_USER -P $DB_PASSWORD -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$DB_NAME') CREATE DATABASE $DB_NAME" -C

# Script para inicialização rápida do banco de dados em ambiente de contêiner
# Esta abordagem é otimizada para ambientes Docker onde queremos inicializar 
# o banco rapidamente antes da aplicação iniciar
echo "Criando estrutura inicial do banco de dados para ambiente Docker..."
/opt/mssql-tools/bin/sqlcmd -S $DB_HOST -U $DB_USER -P $DB_PASSWORD -d $DB_NAME -C -Q "
-- Verifica e cria tabela Categorias (de acordo com 20250419061146_InicialMigration)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categorias')
BEGIN
    CREATE TABLE Categorias (
        CategoriaId INT PRIMARY KEY IDENTITY(1,1),
        CategoriaNome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(200) NOT NULL
    );
    
    -- Inserir dados iniciais de categorias
    INSERT INTO Categorias(CategoriaNome, Descricao)
    VALUES('Normal', 'Lanche feito com ingredientes normais');

    INSERT INTO Categorias(CategoriaNome, Descricao)
    VALUES('Natural', 'Lanche feito com ingredientes integrais e naturais');
    
    INSERT INTO Categorias(CategoriaNome, Descricao)
    VALUES('Vegano', 'Lanche feito com ingredientes veganos');
END

-- Verifica e cria tabela Lanches (de acordo com 20250419061146_InicialMigration)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Lanches')
BEGIN
    CREATE TABLE Lanches (
        LancheId INT PRIMARY KEY IDENTITY(1,1),
        Nome NVARCHAR(80) NOT NULL,
        DescricaoCurta NVARCHAR(200) NOT NULL,
        DescricaoDetalhada NVARCHAR(200) NOT NULL,
        Preco DECIMAL(10,2) NOT NULL,
        ImagemUrl NVARCHAR(200) NULL,
        ImagemThumbnailUrl NVARCHAR(200) NULL,
        IsLanchePreferido BIT NOT NULL,
        EmEstoque BIT NOT NULL,
        CategoriaId INT NOT NULL,
        FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId)
    );

    CREATE INDEX IX_Lanches_CategoriaId ON Lanches(CategoriaId);
    
    -- Inserir dados iniciais de lanches
    INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco)
    VALUES(1,'Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',1, 'http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg','http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg', 0 ,'Cheese Salada', 12.50);
    
    INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco)
    VALUES(1,'Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.',1,'http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg',0,'Misto Quente', 8.00);
    
    INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco)
    VALUES(1,'Pão, hambúrger, presunto, mussarela e batalha palha','Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg',0,'Cheese Burger', 11.00);
    
    INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco)
    VALUES(2,'Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte','Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.',1,'http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg',1,'Lanche Natural Peito Peru', 15.00);
END

-- Verifica e cria tabela CarrinhoCompraItens (de acordo com 20250430210225_CarrinhoCompraItem)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CarrinhoCompraItens')
BEGIN
    CREATE TABLE CarrinhoCompraItens (
        CarrinhoCompraItemId INT PRIMARY KEY IDENTITY(1,1),
        LancheId INT NULL,
        Quantidade INT NOT NULL,
        CarrinhoCompraId NVARCHAR(200) NULL,
        FOREIGN KEY (LancheId) REFERENCES Lanches(LancheId)
    );

    CREATE INDEX IX_CarrinhoCompraItens_LancheId ON CarrinhoCompraItens(LancheId);
END

-- Verifica e cria tabela Pedidos (de acordo com 20250508204306_PedidoDetalhes)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Pedidos')
BEGIN
    CREATE TABLE Pedidos (
        PedidoId INT PRIMARY KEY IDENTITY(1,1),
        Nome NVARCHAR(50) NOT NULL,
        Sobrenome NVARCHAR(50) NOT NULL,
        Endereco1 NVARCHAR(100) NOT NULL,
        Endereco2 NVARCHAR(100) NOT NULL,
        Cep NVARCHAR(10) NOT NULL,
        Estado NVARCHAR(10) NOT NULL,
        Cidade NVARCHAR(50) NOT NULL,
        Telefone NVARCHAR(25) NOT NULL,
        Email NVARCHAR(50) NOT NULL,
        PedidoTotal DECIMAL(18,2) NOT NULL,
        TotalItensPedido INT NOT NULL,
        PedidoEnviado DATETIME2 NOT NULL,
        PedidoEntregueEm DATETIME2 NULL
    );
END

-- Verifica e cria tabela PedidoDetalhes (de acordo com 20250508204306_PedidoDetalhes)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PedidoDetalhes')
BEGIN
    CREATE TABLE PedidoDetalhes (
        PedidoDetalheId INT PRIMARY KEY IDENTITY(1,1),
        PedidoId INT NOT NULL,
        LancheId INT NOT NULL,
        Quantidade INT NOT NULL,
        Preco DECIMAL(18,2) NOT NULL,
        FOREIGN KEY (PedidoId) REFERENCES Pedidos(PedidoId),
        FOREIGN KEY (LancheId) REFERENCES Lanches(LancheId)
    );

    CREATE INDEX IX_PedidoDetalhes_LancheId ON PedidoDetalhes(LancheId);
    CREATE INDEX IX_PedidoDetalhes_PedidoId ON PedidoDetalhes(PedidoId);
END

-- Criar tabela de controle de migrações se não existir
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
    
    -- Registrar todas as migrações como aplicadas para que o EF Core saiba
    -- que estas migrações já foram aplicadas manualmente
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250419061146_InicialMigration', '8.0.2');
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250421164439_PopularCategorias', '8.0.2');
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250421195516_PopularLanches', '8.0.2');
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250430210225_CarrinhoCompraItem', '8.0.2');
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250508204306_PedidoDetalhes', '8.0.2');
END
"

echo "Estrutura e dados iniciais do banco de dados criados com sucesso!"