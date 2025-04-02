# Documentação do Projeto

## Objetivo
O objetivo deste projeto é desenvolver uma interface web para escolas integrais criarem e planejarem suas aulas ao longo do ano letivo. O sistema visa facilitar a organização do cronograma escolar, permitindo a distribuição eficiente de disciplinas e professores.

## Análise do Projeto
O projeto busca solucionar a dificuldade das escolas em gerenciar e distribuir a carga horária das disciplinas ao longo do ano letivo. Para isso, será desenvolvida uma interface intuitiva e funcional que permita a criação, edição e acompanhamento do planejamento escolar.

### Requisitos Funcionais
- Cadastro de disciplinas, professores e turmas.
- Planejamento de aulas distribuídas ao longo do ano letivo.
- Interface intuitiva para visualização e edição do cronograma.
- Exportação do planejamento em formatos acessíveis.
- Controle de acessos conforme o perfil do usuário.

### Requisitos Não Funcionais
- Responsividade para diferentes dispositivos.
- Segurança no armazenamento e manipulação dos dados.
- Performance otimizada para grandes volumes de dados.

## Paleta de Cores
A identidade visual do sistema seguirá uma paleta de cores frias, priorizando modernidade e inovação.

| Cor           | Exemplo  | Hex       |
|--------------|----------|-----------|
| Azul-petróleo | ![#1E3A8A](https://www.colorhexa.com/1e3a8a.png) | `#1E3A8A` |
| Lilás        | ![#A78BFA](https://www.colorhexa.com/A78BFA.png) | `#A78BFA` |
| Cinza-claro  | ![#E5E7EB](https://www.colorhexa.com/E5E7EB.png) | `#E5E7EB` |
| Branco       | ![#FFFFFF](https://www.colorhexa.com/FFFFFF.png) | `#FFFFFF` |

## Tecnologias Utilizadas
Para o desenvolvimento da plataforma, foram utilizadas as seguintes tecnologias:

### Front-end:
- HTML5
- CSS
- JavaScript
- Razor

### Back-end:
- MySQL
- C#
- .NET Framework
- Entity Framework
- LINQ

## Arquitetura do Sistema
O sistema seguirá a arquitetura MVC (Model-View-Controller), garantindo a separação das responsabilidades e melhor manutenção do código.

### Camadas:
- **Model**: Responsável pelos dados e regras de negócio.
- **View**: Interface gráfica para interação do usuário.
- **Controller**: Manipula as requisições e conecta a View ao Model.

## Health Check
Para garantir a estabilidade e disponibilidade do sistema, foi implementado um **Health Check** que monitora os principais serviços essenciais.

### **Configuração**
No `Program.cs`, os Health Checks foram configurados para verificar a API e seus serviços dependentes:

```csharp
app.UseHealthChecksUI(options => options.UIPath = "/Saude-ui");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```

### **Acesso**
- **Endpoint de monitoramento:** `/health`
- **Interface de visualização:** `/Saude-ui`

Essa funcionalidade ajuda na **observabilidade do sistema**, permitindo identificar falhas antes que impactem os usuários.

## Banco de Dados
Para gerenciamento do banco de dados, utilizamos os seguintes comandos:

```sh
Add-Migration InitialIdentity -Context ApplicationDbContext
Update-Database -Context ApplicationDbContext

Add-Migration InitialDatabase -Context MeuDbContext
Update-Database -Context MeuDbContext
```

### Estrutura do Banco de Dados
O banco de dados do sistema foi projetado com as seguintes tabelas principais:
- Turma
- Tarefas
- Sala
- Restrição
- Professor
- Planejamento de Aula
- Horário
- Histórico de Exportação
- Disciplina
- Configurações Genéricas
- Atividades
- Instituição
- Feriados
- Log

## Contato
Eduardo Guedes  
Email: eduardoguedeslibra@gmail.com  
LinkedIn: [Eduardo Guedes Pereira](https://www.linkedin.com/in/eduardoguedespereira/)
