# Censo
Este é um exemplo de sistema de censo dockerizado em .NET Core 3.1 com SQL Server. Logo abaixo, segue a documentação base do projeto juntamente com algumas observações em cada passo:

### Arquitetura

De forma simples, o projeto apresenta a camada de API e a camada de dados, sendo que o mecanismo nativo de injeção de dependência do .NET Core é usado para as devidas injeções. Basicamente temos uma API com consultas e escritas numa base de dados, onde a regra de negócio - para fins de simplificação - é realizada na camada de API.

A configuração do banco de dados é passada via variáveis  de ambiente, assim como o ambiente que a API está sendo executada. Desta forma, fica mais fácil mudar configurações na API sem precisar mudar o código e também é possível executar testes integrados da API de forma isolada - registrando a variável de ambiente ASPNETCORE_ENVIRONMENT=Test.

A API usa o **Entity Framework Core** como framework de ORM para comunicação com o banco, e isso permitiu subir, no ambiente de **Test** (conforme explicação da variável de ambiente acima) um falso banco de dados em memória para os testes integrados - a 'desvantagem' é que cada teste exige que o banco tenha dados específicos, mas isso permite que os testes integrados sejam executados de forma unitária, asism como os testes unitários.

Os testes unitários, por sua vez, usam o **Moq**  para a correta injeção de dependência do que não será testado, afinal estamos falando dos testes *unitários*. Hoje são **27 testes integrados** e **58 testes unitários** escritos, totalizando cerca de 85% de cobertura total de código.

### Docker build

De forma simples, estamos usando imagens docker do .NET Core e do MSSQL para a rápida montagem do ambiente, via **dockerfile** e **docker-compose**. O ambiente pode ser montado e desmontado facilmente e os dados iniciais do banco de dados são escritos na execução da API, que por sua vez espera através de um script a disponibilização do banco.

O banco de dados sobe na porta 1433, que é a porta padrão do SQL Server, e pode ser acessado de forma externa (ou seja, a porta é exportada para fora da imagem). Já a API é disponibilizada para acesso externo pela forma 8080.

Para que ambos os conteiners (banco e API) sejam executados, basta executar o comando ```docker-compose up``` dentro da pasta **Censo.API**.

### Implementação da API

Estamos falando de uma API restful feita sem autenticação, para fins de simplificação. Sua documentação pode ser acessada (após a execução dos respectivos containers) pelo endereço http://localhost:8080/swagger.

### Integração com serviço de CI

O repositório está integrado com o **CircleCI**, e o arquivo de configuração **./circleci/config.yml** é usado para a integração contínua. De forma simples, ele faz o build da solução e executa os testes unitários e integrados.



