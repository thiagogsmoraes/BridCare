<h1 align="center">BridCare</h1>

<p align="center">
  Plataforma web para gerenciamento de plantões e contratação de cuidadores por instituições de cuidado.
</p>

---

## Sobre o Projeto

O BridCare é uma aplicação web desenvolvida para conectar instituições de cuidado e cuidadores profissionais por meio de uma plataforma centralizada para gerenciamento de plantões.

A plataforma permite que instituições publiquem oportunidades de trabalho e que cuidadores visualizem, candidatem-se e participem dos plantões disponíveis.

Além do gerenciamento de oportunidades, o sistema contempla mecanismos de avaliação entre as partes envolvidas, contribuindo para a construção de um ambiente baseado em confiança e qualidade dos serviços prestados.

---

## Objetivos

- Centralizar a divulgação e gestão de plantões.
- Facilitar o processo de contratação de cuidadores.
- Organizar o gerenciamento de profissionais e instituições.
- Promover transparência por meio de avaliações após a execução dos serviços.
- Fornecer uma plataforma digital para apoio às atividades de cuidado.

---

## Funcionalidades

### Instituições

- Cadastro e gerenciamento de perfil institucional.
- Cadastro e gerenciamento de idosos.
- Criação e gerenciamento de plantões.
- Análise de candidaturas.
- Seleção de cuidadores.
- Avaliação dos profissionais contratados.

### Cuidadores

- Cadastro e gerenciamento de perfil profissional.
- Consulta de plantões disponíveis.
- Candidatura a oportunidades.
- Histórico de candidaturas e plantões.
- Avaliação de instituições após a conclusão dos serviços.

### Plataforma

- Autenticação e controle de acesso.
- Gerenciamento de usuários.
- Controle do ciclo de vida dos plantões.
- Sistema de avaliações entre participantes.

---

## Fluxo do Sistema

```text
Instituição
     │
     ▼
Cria Plantão
     │
     ▼
Cuidador se candidata
     │
     ▼
Instituição seleciona
     │
     ▼
Plantão executado
     │
     ▼
Avaliação mútua
```

---

## Arquitetura da Aplicação

```text
ASP.NET Core MVC
        │
        ▼
Controllers
        │
        ▼
Services
        │
        ▼
Entity Framework Core
        │
        ▼
SQL Server
```

A aplicação segue o padrão MVC (Model-View-Controller), utilizando o Entity Framework Core como camada de acesso a dados e o SQL Server como sistema gerenciador de banco de dados.

---

## Modelo Entidade Relacionamento (MER)

<p align="center">
  <img width="1653" height="1633" alt="Cuidado" src="https://github.com/user-attachments/assets/965b6063-7ff3-4fbb-8565-7732a71c3023" />
</p>

O modelo de dados foi estruturado para suportar o gerenciamento de usuários, instituições, cuidadores, idosos, plantões, candidaturas e avaliações.

As principais entidades do sistema são:

| Entidade | Descrição |
|-----------|------------|
| Users | Responsável pela autenticação e identificação dos usuários. |
| Caregivers | Dados específicos dos cuidadores. |
| Institutions | Dados específicos das instituições. |
| Elderlies | Idosos vinculados às instituições. |
| Shifts | Plantões disponibilizados pelas instituições. |
| Applications | Candidaturas realizadas pelos cuidadores. |
| Reviews | Avaliações realizadas após a conclusão dos plantões. |

---

## Regras de Negócio

- Um plantão pode receber múltiplas candidaturas.
- Apenas um cuidador pode ser selecionado para cada plantão.
- Um cuidador pode candidatar-se a diversos plantões.
- Uma instituição pode criar diversos plantões.
- Avaliações somente podem ser realizadas após a conclusão do plantão.
- Não são permitidas candidaturas duplicadas para o mesmo plantão.
- Não é permitido selecionar mais de um cuidador para o mesmo plantão.
- Apenas o proprietário do perfil pode alterar seus dados.
- Usuários somente podem avaliar participantes envolvidos no plantão.

---

## Tecnologias Utilizadas

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Bootstrap
- HTML5
- CSS3
- JavaScript

---

## Estrutura do Projeto

```text
BridCare
│
├── Controllers
├── Models
├── Services
├── Data
├── Views
├── wwwroot
├── Migrations
└── Program.cs
```

---

## Status do Projeto

O projeto encontra-se em desenvolvimento como parte de um Trabalho de Conclusão de Curso (TCC), com foco na aplicação de conceitos de desenvolvimento web, modelagem de dados e arquitetura de software utilizando a plataforma .NET.
