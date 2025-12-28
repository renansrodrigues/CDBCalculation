# CDB CALCULATION - INSTRUÇÕES DE EXECUÇÃO
======================================

VISÃO GERAL
-----------
Esta solução é composta por:
- Backend em .NET (Web API)
- Frontend em Angular (Standalone)
- Testes unitários no backend e frontend

------------------------------------------------------------

PRÉ-REQUISITOS
--------------
- .NET SDK 8 ou superior
- Node.js 18 ou superior
- NPM
- Angular CLI

------------------------------------------------------------

EXECUÇÃO DO BACKEND
-------------------
1. Acesse a pasta do backend:
   cd \src\CDBCalculation.Api

2. Restaure os pacotes:
   dotnet restore

3. Execute a aplicação:
   dotnet run

4. A API ficará disponível em:
   https://localhost:60357

------------------------------------------------------------

EXECUÇÃO DO FRONTEND
--------------------
1. Acesse a pasta do frontend:
   cd \src\CDBCalculation.Front

2. Instale as dependências:
   npm install

3. Execute a aplicação:
   npm start

4. A aplicação estará disponível em:
   http://localhost:4200

------------------------------------------------------------

EXECUÇÃO DOS TESTES UNITÁRIOS
-----------------------------

Backend:
--------
1. Acesse a pasta do backend:
   
   A partir da raiz da solução, execute:
   cd \CDBCalculation
   
2. Execute os testes:
   dotnet test

Frontend:
---------
1. Acesse a pasta do frontend:
   cd \src\CDBCalculation.Front

2. Execute os testes:
   npm run test

Para rodar os testes sem modo watch (CI):
   npm run test -- --watch=false --browsers=ChromeHeadless

------------------------------------------------------------

OBSERVAÇÕES
-----------
- As validações de negócio são realizadas no backend.
- O frontend realiza apenas validações para experiência do usuário.
- Exceptions são usadas apenas para erros técnicos.
- O CORS está liberado apenas para localhost em ambiente de desenvolvimento.

------------------------------------------------------------

FIM
