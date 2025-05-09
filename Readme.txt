Comecei o projeto estruturando as camadas de acordo com a base da arquitetura limpa. Iniciei criando as entidades, depois os DTOs,
camada de repositório, serviço para implementar as regras e as controllers. Após isso criei o banco de dados usando o EF usando
migrations. 

-------------------------------------
 Tecnologias Utilizadas
Backend: ASP.NET Core 8.0

Banco de Dados: PostgreSQL

Autenticação: JWT (JSON Web Tokens)

ORM: Entity Framework Core

Arquitetura: Clean Architecture
-------------------------------------
EndPoints:
Autenticação
POST /api/auth/register - Criar usuário

POST /api/auth/login - Obter token JWT

Carteira
GET /api/wallet/balance - Consultar saldo

POST /api/wallet/add - Adicionar saldo

Transferências
POST /api/transfers - Criar transferência

GET /api/transfers - Listar transferências (com filtro de datas opcional)

---------------------------------------
Instalação
Clone o repositório

Configure a connection string no arquivo appsettings.json

Execute as migrations:

------------------------------------------

Query para popular o banco de dados:

BEGIN;

-- Inserir usuários
INSERT INTO "AspNetUsers" (
    "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "Name",
    "PasswordHash", "EmailConfirmed", "PhoneNumberConfirmed", "TwoFactorEnabled", 
    "LockoutEnabled", "AccessFailedCount"
) VALUES
(gen_random_uuid(), 'ana.silva@example.com', 'ANA.SILVA@EXAMPLE.COM', 'ana.silva@example.com', 'ANA.SILVA@EXAMPLE.COM', 'Ana Silva', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'joao.souza@example.com', 'JOAO.SOUZA@EXAMPLE.COM', 'joao.souza@example.com', 'JOAO.SOUZA@EXAMPLE.COM', 'João Souza', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'maria.lima@example.com', 'MARIA.LIMA@EXAMPLE.COM', 'maria.lima@example.com', 'MARIA.LIMA@EXAMPLE.COM', 'Maria Lima', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'carlos.pereira@example.com', 'CARLOS.PEREIRA@EXAMPLE.COM', 'carlos.pereira@example.com', 'CARLOS.PEREIRA@EXAMPLE.COM', 'Carlos Pereira', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'juliana.alves@example.com', 'JULIANA.ALVES@EXAMPLE.COM', 'juliana.alves@example.com', 'JULIANA.ALVES@EXAMPLE.COM', 'Juliana Alves', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'lucas.martins@example.com', 'LUCAS.MARTINS@EXAMPLE.COM', 'lucas.martins@example.com', 'LUCAS.MARTINS@EXAMPLE.COM', 'Lucas Martins', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'fernanda.costa@example.com', 'FERNANDA.COSTA@EXAMPLE.COM', 'fernanda.costa@example.com', 'FERNANDA.COSTA@EXAMPLE.COM', 'Fernanda Costa', 'PasswordHash', true, false, false, true, 0),
(gen_random_uuid(), 'rafael.oliveira@example.com', 'RAFAEL.OLIVEIRA@EXAMPLE.COM', 'rafael.oliveira@example.com', 'RAFAEL.OLIVEIRA@EXAMPLE.COM', 'Rafael Oliveira', 'PasswordHash', true, false, false, true, 0);

-- Inserir carteiras
INSERT INTO "Wallets" ("Id", "ApplicationUserId", "Balance") VALUES
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'ana.silva@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'joao.souza@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'maria.lima@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'carlos.pereira@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'juliana.alves@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'lucas.martins@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'fernanda.costa@example.com'), 1000),
(gen_random_uuid(), (SELECT "Id" FROM "AspNetUsers" WHERE "Email" = 'rafael.oliveira@example.com'), 1000);

-- Inserir 15 transações com UUIDs válidos
INSERT INTO "Transactions" (
    "Id", "FromWalletId", "ToWalletId", "Amount", "Timestamp"
) VALUES
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 1), 150, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 1), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 2), 100, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 2), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 3), 50, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 3), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 4), 200, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 4), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 5), 75, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 5), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 6), 120, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 6), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 7), 90, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 7), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 0), 130, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 1), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 4), 85, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 2), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 5), 60, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 3), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 6), 95, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 4), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 7), 110, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 5), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 1), 70, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 6), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 2), 40, NOW()),
(gen_random_uuid(), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 7), (SELECT "Id" FROM "Wallets" WHERE "Balance" = 1000 LIMIT 1 OFFSET 3), 55, NOW());

COMMIT;


