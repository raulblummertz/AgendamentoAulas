# 🏋️ Sistema de Agendamento de Aulas - Academia

Sistema para gerenciamento de agendamento de aulas coletivas em academias, desenvolvido com ASP.NET Core Web API e Supabase (PostgreSQL).

## 📋 Sobre o Projeto

O **AgendamentoAulas** é uma API REST que facilita o controle de agendamentos de aulas coletivas em academias, gerenciando cadastro de alunos com planos de limitação mensal, criação de aulas com capacidade máxima e sistema inteligente de validações para evitar overbooking.

### Funcionalidades

- ✅ **Cadastro de Alunos**: Registro com nome e tipo de plano (com limitação de aulas mensais)
- ✅ **Cadastro de Aulas**: Criação de aulas com tipo, data, hora e capacidade máxima de participantes
- ✅ **Sistema de Agendamento**: Reserva de vagas com validações automáticas de:
  - Limite mensal de aulas do aluno
  - Capacidade máxima da aula
  - Conflitos de horário
- ✅ **Relatórios**: Análise de frequência mensal do aluno e tipos de aulas mais frequentados

## 🛠️ Tecnologias Utilizadas

### Backend
- **[.NET 8.0](https://dotnet.microsoft.com/)** - Framework principal para desenvolvimento da API
- **[ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)** - Arquitetura RESTful
- **[Entity Framework Core 9.0.7](https://learn.microsoft.com/ef/core/)** - ORM para mapeamento objeto-relacional
- **[Npgsql 9.0.4](https://www.npgsql.org/)** - Provider do Entity Framework para PostgreSQL

### Banco de Dados
- **[Supabase](https://supabase.com/)** - Backend-as-a-Service com PostgreSQL gerenciado
- **[PostgreSQL](https://www.postgresql.org/)** - Sistema de gerenciamento de banco de dados relacional

### Documentação e Testes
- **[Swagger/OpenAPI 9.0.3](https://swagger.io/)** - Documentação interativa da API

### Segurança
- **[User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)** - Armazenamento seguro de credenciais em desenvolvimento

### Arquitetura e Padrões
- **Repository Pattern** - Separação de responsabilidades
- **Dependency Injection** - Injeção de dependências nativa do ASP.NET Core
- **Code-First Migrations** - Versionamento do schema do banco de dados

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- Conta no [Supabase](https://supabase.com/) (gratuita)
- Editor de código (recomendado: [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/))
- (Opcional) [Cloudflare WARP](https://1.1.1.1/) - Caso tenha problemas de DNS com Supabase
- (Opcional) [TablePlus](https://tableplus.com/) ou [pgAdmin](https://www.pgadmin.org/) para gerenciar o banco de dados

## 🚀 Instalação e Configuração

### 1. Clone o Repositório

```bash
git clone https://github.com/raulblummertz/AgendamentoAulas.git
cd AgendamentoAulas
```

### 2. Configure o Projeto no Supabase

1. Acesse [https://supabase.com/dashboard](https://supabase.com/dashboard)
2. Clique em **"New Project"**
3. Preencha:
   - **Name**: AgendamentoAulas
   - **Database Password**: Escolha uma senha forte (guarde-a!)
   - **Region**: Escolha a mais próxima do Brasil (ex: South America)
4. Aguarde a criação do projeto (~2 minutos)

### 3. Obtenha a Connection String do Supabase

No dashboard do Supabase:

1. Vá em **Settings** → **Database**
2. Role até **Connection String**
3. Selecione a aba **URI** ou **Connection parameters**
4. Copie as informações de conexão

Você verá algo como:
```
Host: db.xxxxxxxxxxxxx.supabase.co
Database: postgres
Port: 5432
User: postgres
```

### 4. Configure o arquivo appsettings.json

Edite o arquivo `Agendamento.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db.xxxxxxxxxxxxx.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=;SslMode=Require;Trust Server Certificate=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**⚠️ Importante:** 
- Substitua `db.xxxxxxxxxxxxx.supabase.co` pelo host do SEU projeto Supabase
- Deixe `Password=` vazio (a senha será configurada via User Secrets)

### 5. Configure User Secrets para a Senha

```bash
# Entre na pasta do projeto
cd Agendamento01

# Inicialize User Secrets (só precisa fazer uma vez)
dotnet user-secrets init

# Adicione a senha do banco
dotnet user-secrets set "SupabasePassword" "sua_senha_do_supabase_aqui"

# Verifique se foi configurado corretamente
dotnet user-secrets list
```

**Por que User Secrets?**
- ✅ Mantém credenciais fora do Git
- ✅ Seguro para desenvolvimento local
- ✅ Fácil de gerenciar

### 6. Restaure as Dependências

```bash
dotnet restore
```

### 7. Execute as Migrations

Crie as tabelas no banco de dados do Supabase:

```bash
dotnet ef database update
```

Este comando criará automaticamente as tabelas:
- `Alunos`
- `Aulas`
- `Agendamentos`
- `__EFMigrationsHistory` (controle de versões)

### 8. Execute o Projeto

```bash
dotnet run
```

A API estará disponível em:
- **HTTPS:** `https://localhost:5001`
- **HTTP:** `http://localhost:5000`
- **Swagger UI:** `https://localhost:5001/swagger`

## 📖 Uso da API

### Acessando a Documentação Interativa

Após iniciar o projeto, acesse o **Swagger** no navegador:

```
https://localhost:5001/swagger
```

O Swagger fornece interface visual para testar todos os endpoints da API.

### Endpoints Principais

> Observação: a rota base usa o padrão `[Route("[controller]/[action]")]`, então os endpoints seguem `/Controller/Action`.

#### Alunos (`AlunosController`)
- `POST /Alunos/CadastroAluno` - Cadastrar novo aluno
- `PUT /Alunos/EditarAluno/{id}` - Editar aluno
- `GET /Alunos/ListarAlunos` - Listar todos os alunos
- `GET /Alunos/ListarAluno/{id}` - Buscar aluno por ID
- `DELETE /Alunos/ApagarAluno/{id}` - Apagar aluno

#### Agendamentos (`AgendamentoController`)
- `POST /Agendamento/AddAgendamento` - Criar agendamento (parâmetros: `alunoId`, `aulaId`)
- `PUT /Agendamento/AtualizarAgendamento/{id}` - Atualizar agendamento (envia `AgendamentoDto`)
- `GET /Agendamento/ListarAgendamentos` - Listar todos os agendamentos
- `GET /Agendamento/ListarAgendamentoPorId/{id}` - Buscar agendamento por ID
- `DELETE /Agendamento/ApagarAgendamento/{id}` - Apagar agendamento

## 🗂️ Estrutura do Projeto

```
AgendamentoAulas/
├── .github/
├── src/
│   ├── Agendamento.API/
│   │   ├── Controllers/
│   │   ├── Properties/
│   │   ├── Agendamento.API.csproj
│   │   ├── Agendamento.API.http
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json
│   ├── Agendamento.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Agendamento.Application.csproj
│   ├── Agendamento.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Interfaces/
│   │   └── Agendamento.Domain.csproj
│   └── Agendamento.Infrastructure/
│       ├── Data/
│       │   ├── Migrations/
│       │   └── AgendamentoContext.cs
│       ├── Repositories/
│       └── Agendamento.Infrastructure.csproj
├── .gitattributes
├── .gitignore
├── AgendamentoAulas.sln
└── README.md
```

## 🔧 Comandos Úteis

### User Secrets

```bash
# Listar todos os secrets configurados
dotnet user-secrets list

# Adicionar/atualizar um secret
dotnet user-secrets set "SupabasePassword" "nova_senha"

# Remover um secret
dotnet user-secrets remove "SupabasePassword"

# Limpar todos os secrets
dotnet user-secrets clear
```

### Entity Framework Core

```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations pendentes
dotnet ef database update

# Reverter última migration
dotnet ef database update NomeMigrationAnterior

# Remover última migration (não aplicada)
dotnet ef migrations remove

# Visualizar SQL gerado
dotnet ef migrations script
```

### Build e Execução

```bash
# Compilar o projeto
dotnet build

# Executar em modo desenvolvimento
dotnet run

# Executar em modo produção
dotnet run --configuration Release

# Executar testes (se houver)
dotnet test
```

## 🗄️ Gerenciamento do Banco com Supabase Dashboard

Acesse o dashboard do Supabase para:

1. **Visualizar tabelas**: Settings → Database → Tables
2. **Executar queries SQL**: SQL Editor
3. **Ver logs**: Logs → Database
4. **Monitorar uso**: Home → Usage

## 🗄️ Gerenciamento do Banco com TablePlus

1. Abra o TablePlus
2. Clique em **Create a new connection**
3. Selecione **PostgreSQL**
4. Configure com os dados do Supabase:
   - **Host:** `db.xxxxxxxxxxxxx.supabase.co`
   - **Port:** `5432`
   - **User:** `postgres`
   - **Password:** sua senha do Supabase
   - **Database:** `postgres`
   - **SSL:** `Require`
5. Clique em **Connect**

## ⚠️ Troubleshooting

### Erro: "Este host não é conhecido" ou DNS não resolve

**Solução:**
- Instale o [Cloudflare WARP](https://1.1.1.1/) para resolver problemas de DNS
- Ou use um DNS público: Google (8.8.8.8) ou Cloudflare (1.1.1.1)
- Verifique se o projeto Supabase está **Active** (não pausado)

### Erro: "password authentication failed"

**Causas possíveis:**
- Senha incorreta no User Secrets
- Verifique com: `dotnet user-secrets list`
- Reconfigure: `dotnet user-secrets set "SupabasePassword" "senha_correta"`

### Erro: "relation does not exist"

**Solução:**
- Execute as migrations: `dotnet ef database update`
- Verifique no dashboard do Supabase se as tabelas foram criadas

### Erro: "SSL connection required"

**Solução:**
- Certifique-se que a connection string tem: `SslMode=Require`
- Ou adicione: `Trust Server Certificate=true`

### Projeto Supabase pausado

Projetos gratuitos pausam após 7 dias de inatividade:
1. Acesse o dashboard do Supabase
2. Clique em **"Restore"** no projeto
3. Aguarde ~2 minutos para reativar

### Porta já em uso

**Solução:**
- Altere a porta em `Properties/launchSettings.json`

## 📝 Regras de Negócio

1. **Limite de Aulas por Plano:**
   - Cada aluno possui um tipo de plano que define quantas aulas pode frequentar por mês
   - O sistema valida automaticamente se o aluno atingiu seu limite antes de confirmar agendamento

2. **Capacidade Máxima:**
   - Cada aula possui capacidade máxima de participantes
   - Não é permitido agendar quando a capacidade está completa

3. **Relatórios:**
   - Contabilização mensal automática de aulas por aluno
   - Identificação dos tipos de aulas mais frequentados

## 🔐 Segurança

### Desenvolvimento
- ✅ Senhas armazenadas em **User Secrets** (fora do Git)
- ✅ Connection string sem credenciais no repositório
- ✅ SSL/TLS obrigatório na conexão com Supabase

### Produção
Configure variáveis de ambiente:

```bash
# Linux/macOS
export SupabasePassword="senha_producao"

# Windows PowerShell
$env:SupabasePassword="senha_producao"

# Azure App Service / AWS / Docker
# Configure via painel de controle ou arquivo .env
```

## ✍️ Próximos Passos

### Testes unitários

Implementação de testes unitários para os endpoints

## 📄 Licença

Este projeto é de código aberto e está disponível para uso educacional e comercial.

## 👤 Autor

**Raul Lummertz**
- GitHub: [@raulblummertz](https://github.com/raulblummertz)
- LinkedIn: [Raul Lummertz](https://www.linkedin.com/in/raul-lummertz/)
- Localização: Brasil - SC

## 🔗 Links Úteis

- [Documentação ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Supabase Documentation](https://supabase.com/docs)
- [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [Swagger/OpenAPI](https://swagger.io/docs/)
- [Cloudflare WARP](https://1.1.1.1/)

---
