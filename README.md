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

A solução *back-end* do LibHouse foi construída seguindo boa parte dos conceitos propostos pela *Clean Archictecure* e por *Ports and Adapters*, de modo que as regras de negócio estejam totalmente isoladas das outras camadas. Com isso, o código-fonte está segmentado conforme as responsabilidades existentes. Por exemplo, todo o controle de acesso a dados acabou sendo distribuído na camada *Data*, enquanto as preocupações relacionadas a autenticação de usuários ficaram dedicadas na camada *Authentication*. A principal ideia dessa divisão é facilitar a manutenção da solução, contribuíndo também para uma base de código mais testável, intercambiável e extensível.

## LibHouse API

O projeto *API* foi criado com o *sdk* *web* do *.NET Core*, com o intuito de expor através do protocolo *http* os *endpoints* para consumo do *front-end* da plataforma LibHouse. Mediante o uso do pacote *Microsoft.AspNetCore.Mvc.Versioning*, as *controllers* estão devidamente versionadas, permitindo que atualizações dos principais recursos publicados na *API* sejam feitas sem provocar um grande impacto nos clientes. 

Esta camada possui ainda outras funções pertinentes, a começar pela implementação de *AspNetLoggedUser*, que segue o contrato da interface *ILoggedUser* definida em *Authentication* com o objetivo de fornecer todos os dados de maior relevância do usuário autenticado. Dessa maneira, a cada requisição realizada para qualquer *endpoint* da *API*, o sistema é capaz de identificar o usuário, sem a necessidade de consultar fontes externas, como o banco de dados.

Não menos importante, a injeção das dependências utilizadas nas demais camadas da solução estão organizadas no *namespace* *Configurations*. Desse jeito, quando a *API* é inicializada, as interfaces e classes são mapeadas adequadamente, e o *framework* garante que os objetos serão construídos sempre que forem solicitados nas funções construtoras.

O *Swagger* está instalado e configurado nesta camada, habilitando a documentação dos *endpoints* da *API* a partir das notações *XML* do C#. Além disso, o recurso em questão permite a testabilidade do serviço de uma forma muito mais ágil com a *Swagger UI*, que foi personalizada para suportar o envio de requisições autenticadas com o *token* *JWT*, bem como exibir as diferentes respostas esperadas em cada rota das *controllers*.

## LibHouse Business

O projeto *Business* reúne as classes com maior valor de negócio da solução. Portanto, esta camada não possui qualquer tipo de dependência, destacando-se por ser a mais isolada de todas. 

As **entidades** estão agrupadas logicamente em um *namespace* próprio. Cada classe desse tipo foi desenvolvida para representar um conceito único de negócio, de modo a separar as responsabilidades. Vale ressaltar que a ideia de *entidades* utilizada neste projeto está alinhada, sobretudo, com as definições da obra *Clean Architecture*. Sendo assim, existem classes semelhantes aos **objetos de valor** retratados no livro *Domain-Driven Design*, que também foram incluídas nesta parte de *Business*.

De maneira complementar, dentro do *namespace* *Application*, os **casos de uso** estão implementados para orquestrar a interação entre as *entidades*. Isso garante que as regras de negócio possam ser reaproveitadas e até mesmo substituíveis, caso necessário. Novamente, a literatura da *Clean Architecture* influenciou esta camada. Logo, cada caso de uso expõe somente um método na sua interface, separando as regras em componentes que facilitam a testabilidade e que deixam claro sobre qual problema será resolvido na sua execução. Todo caso de uso recebe um objeto *input* e retorna um objeto *output* exclusivo. Isso é feito para desacoplar a sua estrutura das *entidades*, que podem mudar por razões diferentes.

Validações com um grau de complexidade maior e que não fazem sentido estar nos construtores e métodos das *entidades*, foram migradas para o *namespace* *Validations*. As classes de validação costumam se associar diretamente a apenas uma entidade, e elaboram as suas respectivas regras com o uso da biblioteca *FluentValidation*. 

## LibHouse Data

O projeto *Data* disponibiliza todas as operações voltadas ao acesso a dados da aplicação. Para se conectar com o banco de dados relacional *Sql Server*, e fazer o mapeamento entre as tabelas e as classes do mundo orientado a objetos, esta camada se beneficia do *Entity Framework Core*. O *framework* da Microsoft elimina a obrigatoriedade de escrever consultas *Sql*, bem como de ter que construir mecanismos de tradução entre os diferentes paradigmas de programação. Logo, o *ORM* viabiliza que os principais esforços de desenvolvimento sejam direcionados às regras de negócio do sistema.

Todas as configurações que envolvem a criação de tabelas, colunas, índices e outras estruturas do banco de dados, estão definidas no *namespace* *Configurations*. Aproveitando-se da interface *IEntityTypeConfiguration*, as entidades e objetos de valor existentes na camada *Business* são mapeadas em classes de configuração exclusivas, auxiliando assim a organização do código.

Por sua vez, o *namespace* *Context* possui somente uma única classe, a **LibHouseContext**, que herda do tipo abstrato *DbContext* fornecido pelo *Entity Framework Core*. Essa classe de contexto é a responsável por configurar o modelo de dados do projeto, gerenciar a conexão com o banco de dados, consultar e persistir os dados, fazer a rastreabilidade dos objetos, materializar o resultado e fazer o cache das consultas.

No diretório identificado como *Migrations*, o histórico completo dos arquivos de migração do banco de dados está disponível. Isso significa que com o uso das ferramentas de linha de comando do *Entity Framework Core*, versões antigas do modelo de dados podem ser restauradas a qualquer momento. Ademais, qualquer mudança nos objetos do banco de dados também pode ser gerada a partir de novas migrações, que consideram as alterações feitas nas classes do *namespace* *Configurations* para criar os *scripts* *Sql*.

Enfim, o *namespace* *Repositories* reúne a implementação das interfaces de repositório declaradas no projeto *Business*. Portanto, são nessas classes que os comandos *Sql* de acesso a dados são elaborados de maneira indireta com as instruções *LINQ* do C#. Complementarmente, o *namespace* *Transactions* apresenta a classe *UnitOfWork*, que interage junto aos repositórios para criar operações consistentes e atômicas quando mais de uma entidade está envolvida em ações de *Insert*, *Update* e/ou *Delete*.

## LibHouse Authentication

O projeto *Authentication* isola as regras de negócio das funcionalidades que englobam a autenticação de usuários da plataforma. Tendo isso em vista, *Business* não precisa lidar com classes e outras estruturas voltadas para o login/logout, que naturalmente fugiriam do escopo da camada. 

Com o intuito de gerenciar os usuários cadastrados no website do *LibHouse*, *Authentication* faz o consumo do *framework* *Asp.Net Core Identity*, que providencia um conjunto de métodos já implementados para atender esta necessidade. De maneira complementar, o *Entity Framework Core* está sendo utilizado em parceria com o *Identity*, visando a persistência dos dados pertencentes aos usuários. Por isso, o *namespace* *Context* possui somente classes e arquivos que dizem respeito à conexão/configuração do banco de dados de autenticação, que é separado do banco de dados de negócio do projeto *Data*.

Os *namespaces* *Login*, *Logout*, *Password* e *Register* definem implementações que seguem os contratos de interfaces criadas em *Business* para logar, deslogar, trocar a senha e registrar um novo usuário, respectivamente. Essas responsabilidades são satisfeitas justamente a partir dos objetos das classes do *framework* *Asp.Net Identity*, que encapsulam os comportamentos mencionados.

Finalizando a lista de *namespaces* com maior importância do projeto, *Token* compreende todas as classes que controlam o ciclo de vida dos *access tokens* e *refresh tokens* atrelados aos usuários. Logo, o *sub namespace* *Generators* reune a lógica de geração de ambos os tipos de *token*, cada uma em sua própria classe. Já *Services* apresenta exclusivamente a classe *IdentityRefreshTokenService*, que expõe métodos utilitários para manipular um *refresh token* específico. *Validations* segue um caminho parecido, verificando com a classe *RefreshTokenValidator* se um *refresh token* está em um estado válido.

## LibHouse Cache

O projeto *Cache* provê a infraestrutura básica para que as demais classes das camadas tenham suporte ao recurso de *cache*. Isto posto, em cenários nos quais um objeto seja muito requisitado pelos clientes da *API*, e que este objeto dificilmente mude de estado com o passar do tempo, a utilização de uma estrutura de *cacheamento* pode ajudar na melhora do desempenho, evitando idas desnecessárias ao banco de dados ou outras fontes externas.

Se considerarmos o *namespace* *Configurations*, nele temos as classes que recebem as configurações dinâmicas para os serviços de *cache*. Por exemplo, *MemoryCachingConfiguration* possui algumas propriedades que alteram o funcionamento do serviço de *cache* em memória existente no projeto, que é responsável por *cachear* os objetos no próprio servidor.

Na sequência, o *namespace* *Decorators* incorpora as classes que implementam o *design pattern* chamado *Decorator*. Elas são encarregadas de disponibilizar uma funcionalidade mais robusta de *cache*, na qual os seus consumidores não enxergam o uso do serviço de *cacheamento*. Para elucidar, *AddressMemoryCachingDecorator* é uma das classes que adotam o mecanismo em questão. Em seu construtor, ela recebe o repositório real que consulta a base de dados, bem como o serviço que recupera em memória os objetos *cacheados*. Desse jeito, os métodos de pesquisa da classe podem ter um comportamento interessante, aonde o *cache* é consultado em primeiro lugar, antes de que a *query* no banco de dados seja executada. A função *GetFirstAddressFromPostalCodeAsync* se comporta exatamente dessa maneira.

Por fim, o *namespace* *Providers* contém os tipos diferentes de serviços de *cache* que podem ser aplicados no sistema. A classe *MemoryCaching* se favorece do pacote *Microsoft.Extensions.Caching.Memory* para conceder funcionalidades que permitem verificar, consultar e armazenar quaisquer objetos na memória do servidor, sem exigir qualquer configuração adicional. 

## LibHouse Controllers

O projeto *Controllers* promove o desacoplamento entre o processamento das requisições/respostas dos clientes, e os recursos do *framework* *web* **.NET Core** utilizados na camada de *API*. Essa estratégia viabiliza um reaproveitamento completo do código deste projeto em outros *frameworks* da plataforma *.NET* construídos em cima do protocolo *http*, pois as classes que gerenciam os pedidos de execução das regras de negócio do sistema (**Controllers**) não possuem qualquer vínculo com uma tecnologia específica.

As classes do *namespace* *Attributes* criam atributos exclusivos de validação para algumas propriedades das *ViewModels* que exigem regras adicionais de checagem dos seus valores em comparação aos recursos padrão oferecidos pelas **Data Annotations**. Um bom exemplo disso é a classe *CpfAttribute*, a qual oferece a funcionalidade de verificação de CPF para as propriedades que forem decoradas com este atributo.

Já o *namespace* *Http*, o de maior destaque na camada, agrupa as classes de acordo com as entidades expostas pela aplicação aos seus clientes. Ou seja, *Users* publica as operações (cadastro, login, etc.) relacionadas apenas com os usuários da ferramenta, enquanto *Residents* publica as operações (cadastro/alteração de preferências, consulta de dados do perfil, etc.) pertinentes apenas aos moradores, e assim por diante. Geralmente, cada *sub namespace* desse acaba sendo dividido em mais três partes. *Interfaces* define os contratos que as classes adaptadoras em *Adapters* implementam para processar e responder as requisições dos consumidores da *API* do *website*. Em paralelo, *Converters* reúnem os tipos que mapeiam uma *ViewModel* para um objeto de *Input* dos casos de uso, ou de um objeto *Output* dos casos de uso para um objeto de *Response*. Concluindo, as classes com o sufixo *Controller* registram os métodos de negócio dos *use cases* para serem executados nas respectivas classes adaptadoras.

Os últimos *namespaces*, *Responses* e *ViewModels*, são complementares. O primeiro deles junta as classes que representam os objetos de resposta de cada *endpoint* das *Controllers*, enquanto o segundo deles agrega as classes que representam os objetos enviados nas requisições para cada um dos *endpoints* das *Controllers*.

## LibHouse Email

O projeto *Email* disponibiliza os mecanismos para o envio de mensagens dinâmicas de e-mail aos usuários da plataforma. Em função disso, as outras camadas não precisam conhecer os detalhes de implementação que envolvem a construção e disparo dos e-mails, fato que contribui novamente para o isolamento das regras de negócio e dos demais setores da arquitetura *back-end*.

Primeiramente, o *namespace* *Models* detém as classes que encapsulam os dados obrigatórios na montagem de cada mensagem de e-mail. Isto posto, *MailRequest* age como um modelo geral, declarando propriedades (*ToMail*, *Subject* e *Body*) que são utilizadas globalmente em todos os disparadores de e-mail.

Seguindo, o *namespace* *Senders* é dividido logicamente pelas entidades principais presentes no projeto *Business*. Isso porque as classes com o sufixo *Sender* implementam diretamente as interfaces criadas na camada de regras de negócio, que por sua vez desacoplam o *domínio* dos requisitos de infraestrutura. Cada classe *Sender* envia uma mensagem específica para os seus destinatários. Por exemplo, *MailKitUserRegistrationSender* despacha um e-mail notificando o usuário sobre o seu novo cadastro no *website*.

Tão relevante quanto o *namespace* anterior, *Services* possui um contrato chamado *IMailService*, que funciona como uma abstração de serviço de e-mail. A partir disso, a camada é capaz de fornecer *N* implementações de classes que se conectam com um servidor *SMTP* e fazem o envio das mensagens. Para elucidar, *MailKitService* consome a biblioteca *MailKit*, que provisiona uma *API* simples de comunicação com qualquer servidor de e-mails.

Por fim, o *namespace* *Settings* compreende as classes de configuração tanto para os serviços de e-mail, quanto para os *Senders*. Todas as suas propriedades são definidas como públicas, de modo a suportar os recursos da injeção de dependência do **.NET Core**. Exemplificando, *MailSettings* apresenta os atributos essenciais (*Mail*, *DisplayName*, *Password*, *Host* e *Port*) para a conexão com o servidor *SMTP* usado pela aplicação no envio das mensagens aos seus usuários.

## LibHouse WebClients

O projeto *WebClients* faz o consumo de *APIs* externas à solução principal, desassociando as regras de negócio das particularidades técnicas que envolvem as chamadas para os *endpoints* dos serviços. Além disso, essa separação permite que as entidades, objetos de valor e casos de uso de *Business* não se misturem com os conceitos retornados por cada rota das *APIs*, que muitas vezes são diferentes e exigem um mapeamento (conversão) das suas propriedades.

Os *namespaces* estão separados por *API*. Para tomar como exemplo, o *namespace* *ViaCep* é totalmente dedicado ao consumo da *API* de mesmo nome que fornece os dados de endereço (logradouro, bairro, cidade, estado, etc.) com base em um código de endereço postal. No geral, os *sub namespaces* são padronizados, mas podem variar conforme os detalhes do serviço *http* em questão.

As classes que habilitam configurações personalizadas na comunicação com as *APIs* (tipo de retorno esperado dos *endpoints*, *url* base do serviço, etc.) costumam ficar localizadas em *Configurations*. Já os objetos de resposta das rotas consumidas podem ser encontrados no *namespace* de *Outputs*. Em contrapartida, os objetos que encapsulam os parâmetros utilizados nas requisições pertencem ao *namespace* *Parameters*.

No que lhe diz respeito, o *namespace* de *Gateways* promove o desacoplamento entre a classe *Web Client* que de fato interage com a *API* e o caso de uso na camada de negócios. Para ilustrar, *ViaCepAddressGateway* consome o serviço da *ViaCep* através da sua dependência *ViaCepWebClient*. Depois disso, a classe *Gateway* se responsabiliza por converter a resposta dos *endpoints* em um objeto de *Output* que o caso de uso espera receber com o objetivo de continuar a orquestração das regras da aplicação.

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
