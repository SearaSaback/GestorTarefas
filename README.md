﻿# 🗂️ GestorTarefas API

API RESTful desenvolvida em .NET 8 para gerenciamento de projetos e tarefas, com regras de negócio aplicadas e cobertura de testes de unidade superior a 80%.

---

## 📋 Sumário

- [ Sobre o Projeto](#sobre-o-projeto)
- [ Tecnologias Utilizadas](#tecnologias-utilizadas)
- [ Funcionalidades da API](#funcionalidades-da-api)
- [ Regras de Negócio](#regras-de-negócio)
- [ Docker](#docker)
- [ Como Rodar o Projeto](#como-rodar-o-projeto)
- [ Endpoints Relatórios](#endpoints-relatorios)
- [ Refinamento](#refinamento)
- [ Final](#final)

---

## Sobre o Projeto

O `GestorTarefas` é uma API para controle de projetos e tarefas, idealizada para ambientes corporativos onde é necessário:

- Controle de regras para alterações
- Validações de regras de negócio específicas
- Geração de relatórios de produtividade
- Controle de regras por perfil de usuário

---

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq4)
- [FluentAssertions](https://fluentassertions.com/)
- [SQLite InMemory](https://learn.microsoft.com/en-us/ef/core/testing/in-memory/)
- [Docker](https://www.docker.com/)

---

## Funcionalidades da API

- CRUD de Projetos
- CRUD de Tarefas
- Comentários em Tarefas
- Histórico de alterações
- Relatórios de produtividade

---

## Regras de Negócio

1. **Prioridade Imutável:**  
   Após a criação da tarefa, a prioridade não pode ser alterada.

2. **Remoção de Projetos:**  
   Não é possível excluir um projeto que contenha tarefas pendentes.

3. **Histórico de Atualizações:**  
   Toda modificação relevante em uma tarefa gera um registro de histórico (data, campo, valor antigo e novo, usuário).

4. **Limite de Tarefas por Projeto:**  
   Cada projeto pode ter no máximo 20 tarefas.

5. **Relatórios de Desempenho:**  
   Disponível somente para usuários com papel "gerente", inclui dados como tarefas concluídas por usuário nos últimos 30 dias.

6. **Comentários com Histórico:**  
   Comentários adicionados às tarefas são registrados no histórico como uma alteração.

---

## Docker

Comandos para executar o Docker

```bash
docker build -t gestortarefas .
```

```bash
docker run -d -p 5000:80 gestortarefas
```

---

## Como Rodar o Projeto

- Pré-requisitos

.NET 8 SDK

Visual Studio Code

Docker

- Executar localmente

```bash
dotnet restore
dotnet build
dotnet run --project GestorTarefas
```

---

## Endpoints Relatórios

| Método | Rota                       | Descrição                                                           |
| ------ | -------------------------- | ------------------------------------------------------------------- |
| GET    | /api/relatorios/desempenho | Número médio de tarefas concluídas por usuário nos últimos 30 dias. |

---

## Refinamento

- Outros possíveis filtros para tarefas, como usuário, período.
- Se somente quem criar a tarefa, poderá alterar.
- Se terá limite para número de comentários por tarefa.

---

## Final

- Integração com ferramentas como Calendários, Jira, Trello ou Slack.
