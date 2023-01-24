# LibHouse (back-end)

Este documento busca explicar e detalhar, sobretudo, as informações técnicas do projeto *back-end* da plataforma LibHouse.

# Índice

- [Sobre o projeto](#sobre-o-projeto)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Banco de dados](#banco-de-dados)
- [Configuração do projeto](#configuração-do-projeto)
- [Estrutura da solução](#estrutura-da-solução)
  - [LibHouse.API](#libhouse-api)
  - [LibHouse.Business](#libhouse-business)
  - [LibHouse.Data](#libhouse-data)
  - [LibHouse.Infrastructure.Authentication](#libhouse-authentication)
  - [LibHouse.Infrastructure.Cache](#libhouse-cache)
  - [LibHouse.Infrastructure.Controllers](#libhouse-controllers)
  - [LibHouse.Infrastructure.Email](#libhouse-email)
  - [LibHouse.Infrastructure.WebClients](#libhouse-webclients)
- [Endpoints](#endpoints)
  - [Addresses](#addresses)
    - [GetAddressByPostalCode](#get-address-by-postal-code)
  - [Users](#users)
    - [RegisterUser](#register-user)
    - [ConfirmUserRegistration](#confirm-user-registration)
    - [LoginUser](#login-user)
    - [LogoutUser](#logout-user)
    - [RefreshToken](#refresh-token)
    - [RequestPasswordReset](#request-password-reset)
    - [ConfirmPasswordReset](#confirm-password-reset)
  - [Residents](#residents)
    - [RegisterResidentRoomPreferences](#register-resident-room-preferences)
    - [RegisterResidentServicesPreferences](#register-resident-services-preferences)
    - [RegisterResidentChargePreferences](#register-resident-charge-preferences)
    - [RegisterResidentGeneralPreferences](#register-resident-general-preferences)
    - [RegisterResidentLocalizationPreferences](#register-resident-localization-preferences)
- [Testes](#testes)
  - [Testes unitários](#testes-unitários)
  - [Testes de integração](#testes-de-integração)
- [Infraestrutura](#infraestrutura)
- [Monitoramento](#monitoramento)
- [Análise estática](#análise-estática)
- [Roadmap](#roadmap)

# Sobre o Projeto

Criado durante o ano de 2022 pelos desenvolvedores Lucas Dirani e Matheus Jesus, o projeto LibHouse surgiu com o objetivo de fornecer uma plataforma *web* para dois tipos de usuários - sendo estes moradores e proprietários de moradias. Para os moradores, a proposta da solução é permitir que eles busquem e encontrem, de acordo com as suas preferências, casas com vagas de aluguel disponíveis. Já para os proprietários, a proposta da solução é habilitar, da mesma forma, a busca de potenciais moradores que se adequem às regras de cada uma das suas propriedades.

Diante desse cenário, o público alvo do LibHouse está concentrado, principalmente, em estudantes que desejam morar em locais como repúblicas. Todavia, o *website* está aberto para qualquer pessoa que precise alugar uma casa e esteja disposta a compartilhar o espaço com outros residentes. O mesmo princípio é válido aos proprietários que queiram anunciar as suas moradias, desde que aceitem mais de um inquilino. Ou seja, expandindo a visão, o LibHouse é aderente ao formato de moradias compartilhadas.

[(Voltar para o topo)](#índice)

# Tecnologias utilizadas

O *back-end* está sendo desenvolvido com a versão 9.0 da linguagem de programação C#, tendo como base o *framework* .NET 5.0. Além disso, as seguintes dependências do *nuget* foram instaladas para possibilitar a construção da *API* e das demais bibliotecas de classes referenciadas na solução do Visual Studio (somente as mais importantes estão listadas):

- **Ardalis.GuardClauses**: usado para validar, de maneira mais elegante e legível, os parâmetros que são passados para os métodos das classes. 
- **AutoMapper**: usado em cenários mais complexos de conversão de *Data Transfer Objects* e entidades de negócio.
- **AWSSDK.SecretsManager**: usado para consumir as configurações sensíveis da *API* do serviço da *AWS*, como por exemplo, a string de conexão do banco de dados.
- **FluentValidation.AspNetCore**: usado para regras de validação envolvendo as entidades de negócio.
- **KissLog**: usado para registrar os logs gerados pela *API* na ferramenta de monitoramento *KissLog*.
- **MailtKit**: usado para fazer o envio de e-mails para os usuários da plataforma.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: usado para a criação e gerenciamento de *tokens* *JWT* consumidos pela *API*.
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore**: usado para integrar o *Entity Framework Core* com o serviço de autenticação *Identity* na camada de infraestrutura da solução.
- **Microsoft.AspNetCore.Mvc.Versioning**: usado para o versionamento das *controllers** da *API*.
- **Microsoft.EntityFrameworkCore.***: usado para fazer o acesso a dados da aplicação, conectando-se diretamente no *Sql Server*.
- **Microsoft.Extensions.Caching.Memory**: usado para armazenar em *cache* (na memória) recursos frequentemente solicitados nos *endpoints* da *API*.
- **Moq**: usado para *mockar* dependências nos testes de integração da *API*.
- **Swashbuckle.AspNetCore.***: usado para habilitar o *Swagger* na *API*.
- **xunit**: usado para construir e executar os testes unitários e de integração.

[(Voltar para o topo)](#índice)

# Banco de dados

O banco de dados consumido pelo *back-end* foi criado no *Sql Server*, adotando portanto uma modelagem relacional. 

A base está dividida em dois *schemas*, identificados como *Business* e *Authentication*. O primeiro deles define um conjunto de objetos (tabelas, visões, etc.) focados exclusivamente em persistir e gerenciar dados relacionados com o domínio da aplicação. Enquanto isso, o segundo *schema* citado se encarrega apenas de lidar com os dados voltados para a autenticação e autorização de usuários, separando assim as responsabilidades.

Cabe salientar que as mudanças nos *schemas* são (e sempre devem ser) realizadas com o auxílio do *Entity Framework Core*, que reflete as alterações feitas nas classes da solução diretamente no banco de dados local. Já em ambiente de produção, as modificações nas tabelas e outras estruturas da base são executadas através do próprio script *SQL*, que pode ser gerado também com o suporte da linha de comando do *Entity Framework Core*. Inclusive, os scripts dos dois *schemas* descritos nesta seção estão acessíveis no [repositório do projeto](https://github.com/libhouse/back-end/tree/main/scripts).

[(Voltar para o topo)](#índice)

# Configuração do projeto

O primeiro passo para configurar localmente o projeto *back-end* do LibHouse é clonar o código-fonte na sua máquina. A branch *main* sempre terá a versão mais estável do serviço.

```
git clone https://github.com/libhouse/back-end.git
```

Uma vez que o projeto foi baixado, basta abrir o arquivo de extensão *.sln* localizado no diretório *src*. Certifique-se de que todas as dependências da solução foram recuperadas do *nuget*. Após isso, clique com o botão direito no serviço *LibHouse.Api* e selecione *Gerenciar Segredos do Usuário* para criar o arquivo *secrets.json* que possui alguns dados de configuração sensíveis.

``` json5
{
  "AccessTokenSettings.ExpiresInSeconds": 0,
  "AccessTokenSettings.Issuer": "",
  "AccessTokenSettings.Secret": "",
  "AccessTokenSettings.ValidIn": "",
  "KissLog.ApiUrl": "",
  "KissLog.ApplicationId": "",
  "KissLog.OrganizationId": "",
  "MailSettings.DisplayName": "",
  "MailSettings.Host": "",
  "MailSettings.Mail": "",
  "MailSettings.Password": "",
  "MailSettings.Port": 0,
  "RefreshTokenSettings.ExpiresInMonths": 0,
  "RefreshTokenSettings.TokenLength": 0
}
```
O conteúdo do *secrets.json* deve possuir exatamente as chaves retratadas acima para que a aplicação seja inicializada e executada com sucesso.

| Chave | Descrição | Tipo |
| --- | --- | --- |
| AccessTokenSettings.ExpiresInSeconds | Tempo de expiração em segundos do *access token* gerado pela *API*. | int |
| AccessTokenSettings.Issuer | Nome do emissor do *access token* gerado pela *API*. O valor padrão para esta chave costuma ser *"LibHouse"*. | string |
| AccessTokenSettings.Secret | O segredo usado para gerar o *access token* fornecido pela *API*. | string |
| AccessTokenSettings.ValidIn | O endereço de validade do *access token* gerado pela *API*. O valor padrão para esta chave em ambiente local costuma ser *"https://localhost"*. | string |
| KissLog.ApiUrl | O endereço da *API* do serviço de logs *KissLog* usado para registrar todas as ações realizadas no *back-end* do LibHouse. O valor padrão para esta chave costuma ser *"https://api.kisslog.net"*. | string |
| KissLog.ApplicationId | O identificador único da aplicação no serviço de logs *KissLog*. Os detalhes sobre esta configuração estão disponíveis adiante nesta seção. | string |
| KissLog.OrganizationId | O identificador único da organização no serviço de logs *KissLog*. Os detalhes sobre esta configuração estão disponíveis adiante nesta seção. | string |
| MailSettings.DisplayName | O nome de contato que será exibido para os usuários que receberem um e-mail do serviço *back-end* do LibHouse. O valor padrão para esta chave costuma ser *"LibHouse Team"*. | string |
| MailSettings.Host | O endereço do serviço do *host* utilizado para enviar e-mails aos usuários da plataforma LibHouse. O valor padrão para esta chave costuma ser *"smtp.gmail.com"*, caso o endereço de e-mail tenha sido criado no gmail. | string |
| MailSettings.Mail | O endereço de e-mail utilizado para enviar e-mails aos usuários da plataforma LibHouse. | string |
| MailSettings.Password | O *password* que autoriza a *API* acessar o serviço de e-mail para fazer o envio de mensagens aos usuários. Caso o gmail tenha sido escolhido como *host*, a configuração desta senha pode ser realizada [clicando aqui](https://support.google.com/mail/answer/185833?hl=en-GB).  | string |
| MailSettings.Port | O número da porta do serviço do *host* utilizado para enviar e-mails aos usuários da plataforma LibHouse. O valor padrão para esta chave costuma ser *"587"*, caso o endereço de e-mail tenha sido criado no gmail. | int |
| RefreshTokenSettings.ExpiresInMonths | Tempo de expiração em meses do *refresh token* gerado pela *API*. | int |
| RefreshTokenSettings.TokenLength | Comprimento do *refresh token* gerado pela *API*. | int |

Ainda no projeto *LibHouse.Api*, abra o arquivo de configuração do ambiente local, denominado *appsettings.Staging.json*. Nele, edite, se preciso, as chaves contendo as *strings de conexão* com o banco de dados. Elas estão identificadas como *LibHouseAuthConnectionString* e *LibHouseConnectionString*, e foram separadas caso haja uma necessidade de mover os *schemas* de autenticação e de negócio para bases distintas. Feito isso, já é possível rodar os comandos do *Entity Framework Core* que irão criar todas as tabelas do serviço *back-end* na sua instância do *Sql Server*.

1. Configurar o ambiente para *Staging* 

```
$env:ASPNETCORE_ENVIRONMENT='Staging'
```

2. Aplicar todas as migrações do contexto de autenticação

```
dotnet ef database update -c AuthenticationContext -p .\LibHouse.Infrastructure.Authentication\LibHouse.Infrastructure.Authentication.csproj -s .\LibHouse.API\LibHouse.API.csproj
```

3. Aplicar todas as migrações do contexto de negócios

```
dotnet ef database update -c LibHouseContext -p .\LibHouse.Data\LibHouse.Data.csproj -s .\LibHouse.API\LibHouse.API.csproj
```

[(Voltar para o topo)](#índice)

# Estrutura da solução

## LibHouse API

## LibHouse Business

## LibHouse Data

## LibHouse Authentication

## LibHouse Cache

## LibHouse Controllers

## LibHouse Email

## LibHouse WebClients

[(Voltar para o topo)](#índice)

# Endpoints

## Addresses

### Get Address By Postal Code

## Users

### Register User

### Confirm User Registration

### Login User

### Logout User

### Refresh Token

### Request Password Reset

### Confirm Password Reset

## Residents

### Register Resident Room Preferences

### Register Resident Services Preferences

### Register Resident Charge Preferences

### Register Resident General Preferences

### Register Resident Localization Preferences

[(Voltar para o topo)](#índice)

# Testes

## Testes unitários

## Testes de integração

[(Voltar para o topo)](#índice)

# Infraestrutura

[(Voltar para o topo)](#índice)

# Monitoramento

[(Voltar para o topo)](#índice)

# Análise estática

[(Voltar para o topo)](#índice)

# Roadmap

[(Voltar para o topo)](#índice)
