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
- [Endpoints](#endpoints)
  - [Users](#users)
    - [RegisterUser](#register-user)
    - [ConfirmUserRegistration](#confirm-user-registration)
    - [LoginUser](#login-user)
    - [LogoutUser](#logout-user)
    - [RefreshToken](#refresh-token)
    - [RequestPasswordReset](#request-password-reset)
    - [ConfirmPasswordReset](#confirm-password-reset)
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

[(Voltar para o topo)](#índice)

# Configuração do projeto

[(Voltar para o topo)](#índice)

# Estrutura da solução

## LibHouse API

## LibHouse Business

## LibHouse Data

## LibHouse Authentication

## LibHouse Cache

## LibHouse Controllers

## LibHouse Email

[(Voltar para o topo)](#índice)

# Endpoints

## Users

### Register User

### Confirm User Registration

### Login User

### Logout User

### Refresh Token

### Request Password Reset

### Confirm Password Reset

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
