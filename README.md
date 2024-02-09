# School Plataform

A aplicação é uma plataforma de registro de alunos, onde apenas os usuários autorizados da instituição têm permissão para inserir, editar, listar e excluir os dados dos alunos no sistema.

## 1. Tecnologias

### Tecnologias Utilizadas FrontEnd
- Vue.js: Um framework JavaScript progressivo para a construção de interfaces de usuário.
  
- Axios: Uma biblioteca para realizar requisições HTTP.

- Vuetify: Interfaces de usuário com componentes estilizados.

- Vue Toasted: Uma biblioteca para notificações/toasts no Vue.js.

### Tecnologias Utilizadas BackEnd

- ASP.NET Core: Um framework de desenvolvimento da API.
- Entity Framework: O ORM para mapear objetos de domínio junto ao banco de dados.
- PostgreSQL: O banco de dados utilizado.
- Microsoft Authentication JWT Bearer: Autenticação JWT.

Testes:
- AutoMock: Biblioteca para simplificar o uso de mocks em testes.
- UnitTest: Framework de teste unitário utilizado.
- Swagger: Utilizado para a documentação do projeto.

# 2. Estrutura do Projeto

## FrontEnd
- Api: Configuração do Axios para comunicação com o backend

- Assets: Recursos estáticos da aplicação, como imagens e fontes.

- Components: Contém componentes reutilizáveis que compõem a aplicação.

- Interface: Modelos de interfaces utilizados para tipagem no projeto.

- Pages: Contém as páginas principais da aplicação, como Dashboard e Home.

- Plugins: Configuração do Vuetify

- Routes: Configuração das rotas do projeto, incluindo a implementação de guards para controle de acesso.

- Services: Utilização do Axios para fazer requisições com o backend

- Styles: Arquivos de estilos globais da aplicação.

- App.vue: O componente raiz que contém a estrutura principal da aplicação.

- Main.ts: Ponto de entrada para a aplicação Vue.js.

## BackEnd
- Configurations: Contém as configurações das entidades que possuem relacionamentos com o banco de dados.

- Controllers: Abriga os controladores da API, incluindo StudentsController, UserController e LoginController.

- DTO: Contém os modelos de dados das entidades, facilitando a transferência de informações entre diferentes partes da aplicação.

- Entity: Esta pasta inclui as entidades fundamentais do sistema, como User e Student.

- Enums: Define o PermitionsTypes, determinando os papéis dos usuários no projeto.

- Interface: Contém interfaces utilizadas na configuração dos repositórios.

- Logging: Configurações relacionadas a logs da aplicação.

- Migrations: Armazena as migrações do Entity Framework, utilizadas para atualizações no banco de dados.

- Repository: Responsável pela integração com o banco de dados, contém a classe ApplicationDbContext e arquivos de configuração de acesso ao banco por entidade.

- Services: Oferece serviços que auxiliam na lógica do projeto, incluindo PasswordHasher para manipulação de senhas e a geração de tokens JWT para autenticação.
 
## 3. Funcionalidades  e Propriedades de Classes

1. Registro de Alunos:

- A aplicação permite que os usuários autorizados insiram os dados dos alunos, como nome, e-mail, RA e CPF.

2. Lista de Alunos:
- O usuário têm acesso a uma lista de todos os alunos registrados.

3. Edição de Alunos:

- É possível editar as informações(nome e email) dos alunos já registrados.

4. Exclusão de Alunos:
- O usuário pode excluir registros de alunos.


## 3.1 Propriedades da Classe User:

* **Id:**
  - Representa um identificador único para cada usuário.
  - Possibilita uma identificação única e inequívoca de cada instância da classe `User`.

* **Name:**
  - Armazena o nome do usuário, proporcionando uma referência mais amigável.

* **Email:**
  - Reservada para armazenar o endereço de e-mail associado a cada usuário.
  - Crucial como meio de comunicação e identificação na aplicação.
  
* **CPF:**
  - Reservada para armazenar o CPF de cada usuário, identificação única.

* **Password:**
  - Mantém a senha associada a cada usuário.
  - Armazenada de forma criptografada ou hash por razões de segurança.

* **Permitions:**
  - Indica os níveis de acesso ou autorizações concedidos a um usuário.
  - Controla quais recursos e operações um usuário está autorizado a acessar.
 

## 3.2 Propriedades da Classe Students

* **RA:**
   - Representa um identificador único para cada aluno.
  - Possibilita uma identificação única e inequívoca de cada instância da classe `Student`.
	
* **Name:**
  - Armazena o nome do aluno.

* **Email:**
  - Reservada para armazenar o endereço de e-mail associado a cada aluno.  
* **CPF:**
  - Reservada para armazenar o CPF de cada aluno, identificação única.
  

## 4. Configuração do Projeto

### FrontEnd

Clone o repositório:

```
git clone https://github.com/annekarolle/orbita-challenge-full-stack-web.git
```
Abra o projeto:

- Navegue para o diretório do projeto backend:
```
cd orbita-challenge-full-stack-web/frontEnd
```
- Abra o projeto em sua IDE favorita. Por exemplo, utilizando o Visual Studio Code:
```
code .
```
- Instalação de Dependências:
```
npm install
```

- Executando o Projeto

```
npm run dev
```

- Preparar o projeto para produção

```
npm run build
```

### BackEnd


Para inicializar o projeto backend, siga os passos abaixo:

Clone o repositório:

```
git clone https://github.com/annekarolle/orbita-challenge-full-stack-web.git
```
Abra o projeto:

- Navegue para o diretório do projeto backend:
```
cd orbita-challenge-full-stack-web/BackEnd
```
- Abra o projeto em sua IDE favorita. Por exemplo, utilizando o Visual Studio Code:
```
code .
```
### Configure a Connection String:

- Abra o arquivo appsettings.json no diretório do projeto.
Encontre a seção ConnectionStrings e ajuste a string de conexão para o seu banco de dados PostgreSQL.
Instale as Dependências:

- Abra um terminal no diretório do projeto e execute o seguinte comando para restaurar as dependências:
```
dotnet restore
```
- Execute as Migrações do Banco de Dados:

- Execute o seguinte comando para aplicar as migrações e criar as tabelas no banco de dados:
  

```
Update-Database
```
- Rode o Projeto:

Execute o seguinte comando para iniciar o servidor:
```
dotnet run
```

- No backend, acho que o principal e melhorar a configuração do projeto. 
  


