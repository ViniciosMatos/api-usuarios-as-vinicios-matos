# DOCUMENTAÇÃO - API DE USUARIOS

## Descrição do Trabalho
Este repositório contém uma API de usuários capaz de realizar métodos CRUD. Esta API faz parte de uma avaliação da faculdade de Análise e Desenvolvimento de Sistemas, no curso de Desenvolvimento Back End.
Este projeto tem como objetivos, botar em prática e adaptar os aprendizados obtidos durante o semestre, buscar novos conhecimentos e utilizá-los de forma clara e da maneira correta. 
Esta, é a documentação da API. Aqui se encontram as Tecnologias Utilizadas, Implementações, e como a API foi feita.

## Tecnologias Utilizadas
    - .NET 9.0
    - ASP NET Core
    - Entity Framework Core
    - SQLite
    - FluentValidation
    - Postman
    - Git
    - GitHub

## Padrões de Projeto Implementados
    - Repository Pattern
    - Service Pattern 
    - DTO Pattern
    - Dependency Injection
    - Factory Method

## Como Executar o Projeto


### Pré-Requisitos
    - .NET 9.0
    - Postman

### Passo-a-Passo
1. Clone o repositório
2. Eecute as migrations
3. Execute o projeto
4. Abra o Postman
5. Importe o postman_collection
6. Teste a aplicação

## Requisições

As requisições disponíveis nesta API são:
**GET ALL**
    *Exemplo de Requisição:* 
        URL: http://localhost:5752/usuarios/

**GET BY ID**
    *Exemplo de Requisição:* 
        URL: http://localhost:5752/usuarios/3

**POST**
    *Exemplo de Requisição:* 
        URL: http://localhost:5752/
        BODY: {
                "Nome": "Luiz otavio",
                "Email": "LUIZ@gmail.com",
                "Senha": "123456",
                "Telefone": "(51) 99999-9999"***,
                "DataNascimento": "2001-02-07"
              }
*** Atributo opcional para a criação

**PUT**
    *Exemplo de Requisição:* 
        URL: http://localhost:5752/
        BODY: {
                "Nome": "Luiz otavio",
                "Email": "LUIZ@gmail.com",
                "Senha": "123456",
                "Telefone": "(51) 99999-9999"***,
                "DataNascimento": "2001-02-07",
                "Ativo": true
              }
*** Atributo opcional para a criação

**DELETE**
    *Exemplo de Requisição:* 
        URL: http://localhost:5752/3


## Estrutura do Projeto
├── conteudo-api/
|  └── Application/
|     ├── DTOs/
|     |  ├── UsuarioCreateDto.cs
|     |  ├── UsuarioReadDto.cs
|     |  └── UsuarioUpdateDto.cs
|     |
|     ├── Interfaces/
|     |  ├── IUsuarioRepository.cs
|     |  └── IUsuarioService.cs
|     |  
|     ├── Services/
|     |  ├── MappingExtensions.cs
|     |  ├── UsuarioFactory.cs
|     |  └── UsuarioService.cs
|     |
|     ├── Validators/
|     |  ├── UsuarioCreateDtoValidator.cs
|     |  └── UsuarioUpdateDtoValidator.cs
|     |
|     ├── Infrastructure/
|     |  ├── Persistence/
|     |     └── AppDbContext.cs
|     |  |
|     |  └── Repositories/
|     |     └── UsuarioRepository.cs
|     |
|     ├── Migrations/
|     |  └── (arquivos gerados automaticamente)
|     |
|     ├── (Pastas geradas automáticamente)
|     |
|     |
|     ├── Program.cs
|     ├── app.db
|     └── (Arquivos gerados automáticamente)
|
├── .gitignore
├── API-Usuarios-AS.postman_collection.json
└── README.md

## Autor
Vinicios Magnus de Matos
RA: 
Curso: Análise e Desnvolvimento de Sistemas

