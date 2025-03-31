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
A identidade visual do sistema seguirá uma paleta de cores frias, priorizando modernidade e inovação:

| Cor           | Hex       |
|---------------|---------- |
| Azul-petróleo | ![#1E3A8A](https://via.placeholder.com/20/1E3A8A/000000?text=+) `#1E3A8A` |
| Lilás         | ![#A78BFA](https://via.placeholder.com/20/A78BFA/000000?text=+) `#A78BFA` |
| Cinza-claro   | ![#E5E7EB](https://via.placeholder.com/20/E5E7EB/000000?text=+) `#E5E7EB` |
| Branco        | ![#FFFFFF](https://via.placeholder.com/20/FFFFFF/000000?text=+) `#FFFFFF` |


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

## Ferramentas
- Para edição de texto e desenvolvimento, foi utilizada a IDE Visual Studio.

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
- Professores.
- Disciplinas.
- Salas.
- Turmas.
- Horários.
- Planejamento de Aulas.
- Restrições.

## Contato
Eduardo Guedes  
Email: eduardoguedeslibra@gmail.com  
LinkedIn: [Eduardo Guedes Pereira](https://www.linkedin.com/in/eduardoguedespereira/)
