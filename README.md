# Task Manager - ASP.NET Core MVC

Aplicação web desenvolvida como teste técnico, com o objetivo de gerenciar tarefas de forma simples e eficiente.

---

# Tecnologias Utilizadas

* ASP.NET Core MVC (.NET 8)
* C#
* Entity Framework Core
* SQLite
* Bootstrap

---

# Funcionalidades

*  Criar tarefas
*  Listar tarefas
*  Editar tarefas (título e descrição)
*  Excluir tarefas
*  Marcar tarefa como concluída
*  Reabrir tarefa (voltar para pendente)
*  Filtrar tarefas:

  * Todas
  * Pendentes
  * Concluídas
  * Exibição de datas:

  * Data de criação
  * Data de conclusão

---

# Regras de Negócio

* O título da tarefa é obrigatório
* A descrição é opcional:

  * Caso não seja informada, o sistema define automaticamente como **"-Sem descrição-"**
* Ao concluir uma tarefa:

  * `IsCompleted = true`
  * `CompletedAt = DateTime.Now`
* Ao reabrir:

  * `IsCompleted = false`
  * `CompletedAt = null`

---

# Estrutura do Projeto

```
TaskManager/
│
├── Controllers/
│   └── TaskItemsController.cs
│
├── Models/
│   └── TaskItem.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Views/
│   └── TaskItems/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       └── Edit.cshtml
│
├── wwwroot/
├── Program.cs
└── tasks.db
```

---

# Como Executar o Projeto

# Pré-requisitos:

* .NET 8 SDK instalado

---

# Passos:

```bash
git clone https://github.com/LucasYamazaki/TaskManager.git
cd TaskManager
dotnet restore
dotnet ef database update
dotnet run
```

---

# Acesso:

Após rodar, acesse:

```
https://localhost:7011
```

---

# Banco de Dados

* Utiliza SQLite (arquivo local)
* Arquivo: `tasks.db`
* Gerenciado via Entity Framework Core (Migrations)

---

# 💡 Diferenciais do Projeto

* Implementação manual de controller (sem scaffolding automático)
* Separação clara de responsabilidades (MVC)
* Regras de negócio tratadas no backend
* Filtro dinâmico de tarefas
* Interface com Bootstrap
* Controle completo de estados (pendente/concluído/reaberto)

---

# Autor

Desenvolvido por Lucas Yamazaki
