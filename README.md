# GuedesTime.MVC

Sistema web para gestão escolar focado em **planejamento de aulas**, organização de cadastros (instituições, séries, turmas, disciplinas, professores) e controle de acesso com **ASP.NET Core Identity**.

## Visão geral
- **Domínio**: organização de uma instituição de ensino com foco em manter o planejamento anual.
- **Contexto ativo**: as telas dependem de uma **instituição selecionada** via sessão (`InstituicaoId`), definida em `Instituicao/Definir`.
- **Autenticação**: `ASP.NET Core Identity` com cookie.
- **Autorização**: roles + permissões (claims/policies) para a área administrativa.

## Stack
- **.NET**: ASP.NET Core (MVC + Razor Pages para Identity)
- **Persistência**: MySQL (Pomelo EF Core)
- **UI**: Razor + assets em `wwwroot`
- **Healthchecks**: endpoints e UI

## Estrutura (alto nível)
- `src/GuedesTime.MVC`: UI (controllers, views, Identity, sessão e permissões)
- `src/GuedesTime.Domain`: modelos e contratos
- `src/GuedesTime.Data`: repositórios e contexto principal (`MeuDbContext`)
- `src/GuedesTime.Service`: serviços de domínio

## Regras de negócio (resumo)
### Instituição (contexto)
- **Seleção**: o usuário seleciona uma instituição em `Instituicao/Definir`, que grava `InstituicaoId` na sessão.
- **Acesso**: para controllers sensíveis, o sistema valida se a instituição selecionada pertence ao usuário autenticado.

### Cadastros base
- **Disciplinas**: cadastro e edição; suporte a cadastro múltiplo (separado por vírgula).
- **Séries**: suporte a cadastro múltiplo e edição individual; vínculo com disciplinas.
- **Professores**: cadastro e associação com instituição.
- **Turmas / Salas / Horários / Feriados**: cadastros complementares para o planejamento.

### Planejamento de aulas
- O planejamento depende dos cadastros de base e do contexto da instituição selecionada.
- O sistema evolui com telas específicas para operação e consulta do planejamento.

## Identidade e acesso
### Cookie do Identity
- Login baseado em cookie. Reiniciar o servidor não invalida o cookie no browser por padrão.
- O SecurityStamp é validado periodicamente para refletir mudanças de roles/claims.

### Permissões (Admin)
Permissões são modeladas como **claims** e expostas como **policies**.

Área `Admin`:
- `/Admin/Users`: listar usuários e atribuir roles
- `/Admin/Roles`: CRUD de roles + permissões por role

Permissões padrão:
- `Admin`
- `Roles.Read`, `Roles.Write`
- `Users.Read`, `Users.Write`

| Cor           | Exemplo  | Hex       |
|--------------|----------|-----------|
| Azul-petróleo | ![#1E3A8A](https://www.colorhexa.com/1e3a8a.png) | `#1E3A8A` |
| Lilás        | ![#A78BFA](https://www.colorhexa.com/A78BFA.png) | `#A78BFA` |
| Cinza-claro  | ![#E5E7EB](https://www.colorhexa.com/E5E7EB.png) | `#E5E7EB` |
| Branco       | ![#FFFFFF](https://www.colorhexa.com/FFFFFF.png) | `#FFFFFF` |

## Perfil do usuário (`/Usuario`)
- Exibe dados reais do usuário autenticado (Nome, Email, Roles, CPF opcional, membro desde).
- **Instituições relacionadas**: lista instituições do usuário e permite trocar o contexto chamando `Instituicao/Definir`.
- Edição de dados e avatar seguem o padrão do projeto: **partials carregadas no modal global**.

## Avatar (armazenamento)
- Avatares podem ser:
  - **pré-definidos** em `wwwroot/assets/avatar` (referência por URL)
  - **protegidos** com Data Protection e armazenados no banco (sem salvar arquivo em `wwwroot`)
- O avatar protegido é servido via endpoint autenticado: `GET /Usuario/Avatar`.

## Como rodar
### Pré-requisitos
- .NET SDK
- MySQL

### Estrutura do Banco de Dados
O banco de dados do sistema foi projetado com as seguintes tabelas principais:
- Instituicao
- Endereco
- Serie
- Turma
- Tarefas
- Sala
- Restrição
- Professor
- Restricões
- Horarios
- Planejamento de Aula
- Horário
- Histórico de Exportação
- Disciplina
- Configurações Genéricas
- Atividades
- HistoricoExportacao
- Instituição
- Feriados
- Log

### Executar
- Configure a connection string em `src/GuedesTime.MVC/appsettings.json`
- Rode:

```bash
dotnet run --project src/GuedesTime.MVC
```

## Migrações / Banco
Contextos:
- `ApplicationDbContext` (Identity)
- `MeuDbContext` (domínio)

Com `dotnet-ef` (ferramenta local via `dotnet-tools.json`):

```bash
dotnet tool run dotnet-ef database update --project src/GuedesTime.MVC --startup-project src/GuedesTime.MVC --context GuedesTime.MVC.Data.ApplicationDbContext
```

## Health checks
- UI: `/Saude-ui`
- Endpoints:
  - `/Saude`
  - `/Saude/db`
  - `/Saude/system`

## Contato
- Eduardo Guedes
- Email: `eduardoguedeslibra@gmail.com`
