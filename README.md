# Desafio Easynvest 

  

O objetivo dessa demo é criar uma api em .NET utilizando as melhores práticas de desenvolvimento para resolver um desafio proposto pela easynvest: 

  

O link para acessar o endereço da API através do swagger: https://easynvestapi.azurewebsites.net/swagger/index.html 

  

##  Camadas do projeto 

  

Pensando em boas práticas na modelagem do projeto, foi utilizado o Domain-Driven Design (DDD), que é uma abordagem de desenvolvimento de software em que o design é orientado pelo domínio. 

  

### Camada API 

  

A camada de API é responsável por disponibilizar o endpoint de consulta do valor total do investimento do cliente com a lista dos seus investimentos.  

Implementações feitas na camada de API: 

  

- Foi criado a classe ***AplicacaoOptions*** que carrega todas as configurações que estão no *appsettings.json*, essa classe pode usada por injeção de dependências nas outras camadas da aplicação. 

  

- A documentação da API foi disponibilizada usando o ***Swagger***. 

  

- Foram utilizadas as bibliotecas ***Refit*** com o ***Polly*** para realizar chamadas htttp nos endpoints de serviços com políticas de timeout, retries e wait. 

  

- Foi criado o Middleware ***TratarExcecoesMiddleware*** para tratar os erros da aplicação e mostrar uma mensagem amigável caso o ambiente não seja de desenvolvimento. 

   

- A comunicação dos endpoins da API para a camada de Aplicação é utilizada a implementação do Mediator Pattern com a biblioteca ***MediatR***. 

   

### Camada Aplicação 

A camada de aplicação é responsável por realizar regras de negócio que envolve mais de um domínio da aplicação e onde estão definidas as classes responsáveis pela comunicação com serviços externos. 

Pastas: 

- **Queries**: Pensando em desenvolvimento CQRS, onde cada ação de consulta (query) e comando de criar, atualizar e remover (command) deverão ser separados por responsabilidades únicas na aplicação. Nessa pasta estão as classes de envio de parametros da consulta do método, objeto de retorno e o quem manipula os eventos enviados pelo MediatR, que é a classe *ConsultarValorTotalInvestidoHandler*. 

- **Services**: As classes de acesso aos serviços externos. 

- **Options**: A classe que representa as configurações da camada de serviços 

  

### Dominio 

A camada de domínio contém as classes de entidades, dtos e interfaces. 

Pastas: 

  

- **Interfaces**: As interfaces de cálculos de resgate e imposto de renda. 

- **Ir**: A classe concreta de cálculo de imposto de renda. 

- **Resgate**: A classe concreta de cálculo do valor de Resgate. 

- **Models**: Entidades da aplicação. 

  

### Infra 

As consultas dos serviços da aplicação estão utilizando cache com período de expiração até o dia seguinte às 00:00. 

Foi utilizado o cache Redis do Azure (solução NoSQL e open source) 

Pastas: 

  

- **Cache**:  Contém as classes de interface e classe genéricas para recuperar e salvar as chaves no cache do Redis. 

  

### Teste 

  

Foi criado testes de unidade usando os frameworks **xUnit** com **FluenteAssertions** por serem os mais utilizados no mercado e deixar a leitura dos testes mais próximos do mundo real.  

Pastas: 

  

- **Domínio**:  Contém as classes de testes para os cálculos de Imposto de Renda e Valor total para resgate. 

 