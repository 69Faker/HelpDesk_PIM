# 🌐 Sistema de HelpDesk para Provedores de Internet (ISP)

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=%20PAUSADO&color=blue&style=for-the-badge)
![Badge .NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Badge Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![Badge SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## 📖 Sobre o Projeto

Este projeto consiste em um sistema de **Help Desk Inteligente** focado na experiência do cliente de Provedores de Internet (ISPs).O objetivo é substituir métodos manuais de atendimento por uma plataforma centralizada e automatizada, garantindo agilidade e eficiência no suporte técnico.

O sistema foi desenvolvido utilizando uma arquitetura moderna baseada em microsserviços e comunicação **RESTful**, onde uma API central gerencia toda a lógica de negócios e persistência de dados, servindo uma interface web interativa construída com **Blazor Server**.

---

## ⚙️ Arquitetura da Solução

O projeto segue uma estrutura de **Monorepo** com clara separação de responsabilidades (Separation of Concerns), dividida em três camadas principais:

### 1. 🧠 HelpDesk.Api (Back-end)
* **Tecnologia:** ASP.NET Core Web API.
* **Função:** Atua como o "cérebro" do sistema, centralizando as Regras de Negócio e o acesso a dados.
* **Padrões:** Utiliza o **Repository Pattern** para desacoplar a lógica de negócios da implementação do banco de dados e **Injeção de Dependência (DI)** nativa.
* **Documentação:** Swagger (OpenAPI) integrado para documentação e teste dos endpoints.

### 2. 💻 HelpDesk.Web (Front-end)
* **Tecnologia:** Blazor Server.
* **Função:** Portal do Cliente. Interface moderna e responsiva onde o usuário final realiza o autoatendimento.
* **Comunicação:** Consome a API via requisições HTTP/REST.

### 3. 📚 HelpDesk.Shared (Core/DTOs)
* **Função:** Biblioteca de classes (.NET Class Library) que atua como contrato único entre a API e o Web.
* **Conteúdo:** Contém os **DTOs (Data Transfer Objects)**, Enums e modelos compartilhados, garantindo que o Front-end e o Back-end "falem a mesma língua" e evitando duplicidade de código.

---

## 🚀 Funcionalidades (Portal do Cliente)

Baseado nos requisitos levantados, o sistema oferece as seguintes funcionalidades para o usuário final:

* **🔐 Autenticação Segura:** Login via CPF e Senha, garantindo a segurança dos dados do assinante.
* **🎫 Gestão de Chamados:**
    * Abertura de novos tickets de suporte (ex: Problemas de Rede, Financeiro).
    * Visualização do histórico e status dos chamados (Aberto, Em Atendimento, Finalizado).
    * Possibilidade de cancelamento de chamados pelo próprio usuário.
* **🤖 Integração com IA (Chatbot):** Estrutura preparada para triagem inicial e sugestão de soluções automáticas via endpoint dedicado de IA.
* **📄 Autoatendimento Financeiro:** Visualização de contratos e faturas (simulação).

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C# (.NET 8)
* **Framework Web:** ASP.NET Core & Blazor Server
* **Banco de Dados:** SQL Server (Azure SQL)
* **ORM:** Entity Framework Core
* **Design Patterns:** Repository Pattern, DTOs, Dependency Injection.
* **API Documentation:** Swagger UI.

---

## 🗄️ Modelo de Dados

O banco de dados foi modelado para garantir integridade e performance, seguindo as normas da **LGPD**. As principais entidades são:

* **Usuario/Cliente:** Dados cadastrais e de autenticação.
* **Chamado:** Centraliza as solicitações de suporte.
* **Mensagem:** Histórico de conversas dentro do chamado.
* **Contrato:** Vínculo do cliente com o plano contratado.
* **ClasseIA:** Tabela para categorização automática via Inteligência Artificial.

---

## 📸 Screenshots

### Documentação da API (Swagger)
> A API expõe endpoints organizados para Clientes, Chamados e Chatbot.
*(Insira aqui o print do Swagger que está na Figura 6 ou 15 do PDF)*

### Portal do Cliente (Blazor)
> Interface limpa e acessível para abertura de chamados.
*(Insira aqui prints das telas do Dashboard e Abertura de Chamado do Anexo II do PDF)*

---

## 📝 Licença

Este projeto foi desenvolvido como parte do **Projeto Integrado Multidisciplinar (PIM IV)** do curso de Análise e Desenvolvimento de Sistemas.
