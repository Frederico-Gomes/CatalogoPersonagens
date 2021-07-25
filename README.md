# CatalogoPersonagens
Implementação de uma API para o projeto "Criando um catálogo de jogos usando boas práticas de arquitetura com .NET" da digital innovation one (digitalinvoation.one). 
Ao invés de jogos, foi implementado uma API para um catalogo de personagens.

### Features

- [x] Busca paginada por personagens.
- [x] Inserção de personagens.
- [x] Remoção de personagens.
- [x] Atualização dos dados dos personagens.

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina o
[Dotnet](https://dotnet.microsoft.com/). 

Além disto recomendo editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/)

### Configurando Bancos de Dados

A aplicação utiliza [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) 

* Para configuração do banco de dados é necessário atualizar a connection string localizada no appsepttings.

* Para criar a tabela dos personagens sugiro a query: 
create table Personagem(
Id uniqueidentifier primary key,
Nome varchar(100),
Ator varchar(100),
Filme varchar(100),
Importancia TINYINT,
Existencia TINYINT
);

