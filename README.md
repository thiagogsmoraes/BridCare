<h1 align="center">BridCare</h1>

<p align="center">
  Sistema web para intermediação entre cuidadores de idosos e instituições de atendimento, permitindo o gerenciamento de plantões, candidaturas e futuras avaliações entre as partes.
</p>

---

## Sobre o Projeto

O BridCare é uma plataforma desenvolvida para conectar instituições de atendimento e cuidadores de idosos, centralizando o processo de divulgação de plantões, candidatura de profissionais e contratação.

O projeto foi desenvolvido utilizando ASP.NET Core MVC e tem como objetivo aplicar conceitos de arquitetura em camadas, modelagem relacional, autenticação de usuários e implementação de regras de negócio.

---

## Funcionalidades

### Instituições

* Cadastro e autenticação de usuários.
* Cadastro de instituições.
* Cadastro de idosos.
* Criação e gerenciamento de plantões.
* Seleção de cuidadores candidatos.

### Cuidadores

* Cadastro e autenticação de usuários.
* Cadastro de perfil profissional.
* Visualização de plantões disponíveis.
* Candidatura para plantões.

### Plataforma

* Controle de acesso por perfis.
* Gerenciamento de usuários.
* Controle de candidaturas.
* Aplicação de regras de negócio para contratação.

---

## Fluxo da Aplicação

```text
Instituição
    ↓
Cria Plantão
    ↓
Cuidadores Visualizam
    ↓
Cuidadores se Candidatam
    ↓
Instituição Seleciona
    ↓
Plantão Executado
    ↓
Avaliações
```

---

## Arquitetura

A aplicação segue o padrão MVC (Model-View-Controller), com separação das responsabilidades em camadas para facilitar manutenção e futura evolução da arquitetura.

```text
Controllers
      ↓
Services
      ↓
Entity Framework Core
      ↓
SQL Server
```

---

## Modelagem de Dados

<p align="center">
  <img width="1653" height="1633" alt="Cuidado" src="https://github.com/user-attachments/assets/965b6063-7ff3-4fbb-8565-7732a71c3023" />
</p>

### Principais Entidades

* Users
* Caregivers
* Institutions
* Elderlies
* Shifts
* Applications
* Reviews

---

## Tecnologias Utilizadas

### Back-end

* C#
* .NET 8
* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Identity
* LINQ

### Banco de Dados

* SQL Server

### Front-end

* HTML
* CSS
* JavaScript
* Bootstrap

### Ferramentas

* Git
* GitHub
* Visual Studio

---

## Conceitos Aplicados

Durante o desenvolvimento deste projeto foram aplicados:

* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Identity
* LINQ
* SQL Server
* Migrations
* Injeção de Dependência
* Arquitetura em Camadas
* Relacionamentos 1:N e N:N
* Autenticação e Autorização
* Regras de Negócio
* Padrão MVC

---

## Como Executar

### Pré-requisitos

* .NET 8 SDK
* SQL Server
* Visual Studio 2022 ou superior

### Passos

1. Clone o repositório

```bash
git clone https://github.com/thiagogsmoraes/BridCare.git
```

2. Acesse o diretório do projeto

```bash
cd BridCare
```

3. Configure a Connection String no arquivo:

```json
appsettings.json
```

4. Execute as migrations

```powershell
Update-Database
```

ou

```bash
dotnet ef database update
```

5. Execute a aplicação

```bash
dotnet run
```

6. Acesse pelo navegador

```text
https://localhost:xxxx
```

---

## Próximos Passos

O projeto encontra-se em fase de consolidação das regras de negócio e evolução arquitetural. As próximas etapas previstas incluem:

* Finalização do fluxo de candidaturas para plantões.
* Implementação do sistema de avaliações entre cuidadores e instituições.
* Consolidação e validação das regras de negócio da plataforma.
* Refatoração da camada de serviços visando maior desacoplamento entre interface e domínio.
* Evolução da arquitetura para disponibilização de API REST.
* Implementação de autenticação baseada em JWT.
* Documentação dos endpoints utilizando Swagger/OpenAPI.
* Desenvolvimento de uma aplicação web desacoplada consumindo a API.
* Implementação de testes automatizados.

---

## Roadmap

### Concluído

* [x] Autenticação e autorização com ASP.NET Identity
* [x] Cadastro de usuários
* [x] Cadastro de instituições
* [x] Cadastro de cuidadores
* [x] Cadastro de idosos
* [x] Criação e gerenciamento de plantões
* [x] Modelagem relacional completa da aplicação
* [x] Implementação da arquitetura MVC
* [x] Integração com SQL Server utilizando Entity Framework Core
* [x] Estruturação em camadas para futura evolução da aplicação

### Em Desenvolvimento

* [ ] Fluxo completo de candidaturas para plantões
* [ ] Processo de seleção de cuidadores
* [ ] Sistema de avaliações (Reviews)
* [ ] Consolidação e validação das regras de negócio
* [ ] Refatoração da camada de serviços

### Planejado

* [ ] API REST com ASP.NET Core Web API
* [ ] Autenticação JWT
* [ ] Documentação Swagger/OpenAPI
* [ ] Testes automatizados
* [ ] Aplicação web consumindo a API

---

## Autor

**Thiago Galvão de Souza Moraes**

* LinkedIn: [thiagogsmoraes](https://linkedin.com/in/thiagogsmoraes)
* GitHub: [thiagogsmoraes](https://github.com/thiagogsmoraes)

Projeto desenvolvido para fins de estudo, prática de desenvolvimento .NET e evolução profissional na área de Engenharia de Software.
