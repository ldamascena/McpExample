# MCP Server - Students Management

Este projeto demonstra a integração de um **MCP Server** com uma **API de gerenciamento de estudantes**. Ele permite criar, listar, atualizar e deletar estudantes através de **tools do MCP Server**.

---

## 🔹 Estrutura do Projeto

- **MCPServer**
  - Contém os **tools** que expõem funcionalidades para o MCP Inspector.
  - `StudentsTools.cs` → ferramentas MCP para CRUD de estudantes.
  - `Clients/StudentsApiClient.cs` → cliente HTTP para comunicação com a API.

- **StudentManager.Api**
  - API RESTful para gerenciamento de estudantes.
  - Endpoints expostos:
    - `GET /api/students`
    - `POST /api/students/create`
    - `PUT /api/students/update`
    - `DELETE /api/students/delete/{id}`

- **Program.cs (MCPServer)**
  - Configuração do MCP Server:
    ```csharp
    builder.Services
        .AddMcpServer(mcp =>
        {
            mcp.ServerInfo = new Implementation { Name = "DotnetMCPServer", Version = "1.0.0" };
        })
        .WithStdioServerTransport()
        .WithToolsFromAssembly();

    builder.Services.AddHttpClient<StudentsApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7007/api/");
    });

    var app = builder.Build();
    await app.RunAsync();
    ```

---

## 🔹 Endpoints da API

### 1. Listar estudantes
- **Método:** GET
- **URL:** `/api/students`
- **Exemplo de retorno:**
```json
[
  {
    "id": "ae5ba5fc-56cf-47e9-933e-678e137835ec",
    "name": "Leonardo",
    "age": 36
  }
]


# MCP Server - Students Management

Este projeto demonstra a integração de um **MCP Server** com uma **API de gerenciamento de estudantes**. Ele permite criar, listar, atualizar e deletar estudantes através de **tools do MCP Server**.

---

## 🔹 Estrutura do Projeto

- **MCPServer**
  - Contém os **tools** que expõem funcionalidades para o MCP Inspector.
  - `StudentsTools.cs` → ferramentas MCP para CRUD de estudantes.
  - `Clients/StudentsApiClient.cs` → cliente HTTP para comunicação com a API.

- **StudentManager.Api**
  - API RESTful para gerenciamento de estudantes.
  - Endpoints expostos:
    - `GET /api/students`
    - `POST /api/students/create`
    - `PUT /api/students/update`
    - `DELETE /api/students/delete/{id}`

- **Program.cs (MCPServer)**
  - Configuração do MCP Server:
    ```csharp
    builder.Services
        .AddMcpServer(mcp =>
        {
            mcp.ServerInfo = new Implementation { Name = "DotnetMCPServer", Version = "1.0.0" };
        })
        .WithStdioServerTransport()
        .WithToolsFromAssembly();

    builder.Services.AddHttpClient<StudentsApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7007/api/");
    });

    var app = builder.Build();
    await app.RunAsync();
    ```

---

## 🔹 Endpoints da API

### 1. Listar estudantes
- **Método:** GET
- **URL:** `/api/students`
- **Exemplo de retorno:**
```json
[
  {
    "id": "ae5ba5fc-56cf-47e9-933e-678e137835ec",
    "name": "Leonardo",
    "age": 36
  }
]
# MCP Server - Students Management

Este projeto demonstra a integração de um **MCP Server** com uma **API de gerenciamento de estudantes**. Ele permite criar, listar, atualizar e deletar estudantes através de **tools do MCP Server**.

---

## 🔹 Estrutura do Projeto

- **MCPServer**
  - Contém os **tools** que expõem funcionalidades para o MCP Inspector.
  - `StudentsTools.cs` → ferramentas MCP para CRUD de estudantes.
  - `Clients/StudentsApiClient.cs` → cliente HTTP para comunicação com a API.

- **StudentManager.Api**
  - API RESTful para gerenciamento de estudantes.
  - Endpoints expostos:
    - `GET /api/students`
    - `POST /api/students/create`
    - `PUT /api/students/update`
    - `DELETE /api/students/delete/{id}`

- **Program.cs (MCPServer)**
  - Configuração do MCP Server:
    ```csharp
    builder.Services
        .AddMcpServer(mcp =>
        {
            mcp.ServerInfo = new Implementation { Name = "DotnetMCPServer", Version = "1.0.0" };
        })
        .WithStdioServerTransport()
        .WithToolsFromAssembly();

    builder.Services.AddHttpClient<StudentsApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7007/api/");
    });

    var app = builder.Build();
    await app.RunAsync();
    ```

---

## 🔹 Endpoints da API

### 1. Listar estudantes
- **Método:** GET
- **URL:** `/api/students`
- **Exemplo de retorno:**
```json
[
  {
    "id": "ae5ba5fc-56cf-47e9-933e-678e137835ec",
    "name": "Leonardo",
    "age": 36
  }
]


### 2. Criar estudante
- **Método:** POST  
- **URL:** `/api/students/create`  
- **Body:**
```json
{
  "name": "Alice",
  "age": 1
}


### 3. Atualizar estudante
- **Método:** PUT  
- **URL:** `/api/students/update`  
- **Body:**
```json
{
  "id": "ae5ba5fc-56cf-47e9-933e-678e137835ec",
  "name": "Alice Updated",
  "age": 2
}


### 4. Deletar estudante
- **Método:** DELETE  
- **URL:** `/api/students/{id}`  
- **Parâmetro:** `id` (GUID do estudante a ser deletado)  
- **Retorno:** Sucesso ou erro


### 🔹 MCP Tools Disponíveis

| Tool           | Descrição                        | Parâmetros                                 |
|----------------|---------------------------------|-------------------------------------------|
| GetStudents    | Lista todos os estudantes        | Nenhum                                     |
| GetStudent     | Busca um estudante por ID        | `Guid id`                                  |
| CreateStudent  | Cria um estudante                | `StudentsRequest { Name, Age }`           |
| UpdateStudent  | Atualiza um estudante            | `UpdateStudentRequest { Id, Name, Age }`  |
| DeleteStudent  | Deleta um estudante por ID       | `Guid id`                                  |

### 🔹 Observações

- Certifique-se de que a API (`https://localhost:7007/api/`) esteja rodando antes de chamar os tools do MCP Server.
- `BaseAddress` do `HttpClient` deve terminar com `/api/` para concatenar corretamente as rotas.
- Create e Update retornam apenas o ID do estudante para evitar problemas de desserialização com MCP Server.
- Ferramentas MCP não exibem detalhes completos do estudante, apenas strings simples para compatibilidade com o MCP Inspector.


