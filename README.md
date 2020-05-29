# Desafio Easynvest 

  

O objetivo desse projeto é criar uma api em .NET Core utilizando as melhores práticas de desenvolvimento para resolver um desafio proposto pela easynvest: 

  
Links de acesso:
API: https://easynvestapi.azurewebsites.net/api/Investimentos/ConsultarValorTotalInvestido
Swagger: https://easynvestapi.azurewebsites.net/swagger/index.html 
Github: https://github.com/AndersonGomesOSF/easynvest-entrevista
  

##  Camadas do projeto 

  

Pensando em boas práticas na modelagem do projeto, foi utilizado o Domain-Driven Design (DDD), que é uma abordagem de desenvolvimento de software em que o design é orientado pelo domínio. 

  

### Camada API 

  

A camada de API é responsável por disponibilizar o endpoint de consulta do valor total do investimento do cliente com a lista dos seus investimentos com o Valor do IR e Resgates calculados com a data atual.

Implementações feitas na camada de API: 

  

- Foi criado a classe ***AplicacaoOptions*** representa todas as configurações que estão no *appsettings.json*, essa classe pode usada por injeção de dependências nas outras camadas do projeto. 

  

- A documentação da API foi disponibilizada usando o ***Swagger***. 

  

- Foram utilizadas as bibliotecas ***Refit*** com o ***Polly*** para realizar chamadas htttp nos endpoints de serviços com políticas de circuit breaker (timeout, retries e wait) configurados no appsettings.json.

  

- Foi criado o Middleware ***TratarExcecoesMiddleware*** para tratar os erros da aplicação e mostrar uma mensagem amigável caso o ambiente não seja de desenvolvimento. 

   

- A comunicação dos endpoins da API para a camada de Aplicação é utilizado a biblioteca ***MediatR*** para implementação do Mediator Pattern. 

   

### Camada Aplicação 

A camada de aplicação é responsável por realizar regras de negócio que envolvem mais de um domínio da aplicação e onde estão definidas as classes responsáveis pela comunicação com serviços externos. 

Pastas: 

- **Queries**: Pensando em desenvolvimento CQRS, cada ação de consulta é um Query e qualquer alteração de estado do domínio(criar, atualizar e remover) é considerado um Command, então cada ação deverá ser separados por responsabilidades únicas na aplicação. Nessa pasta está a classe Query que representa os parámetros enviados para o endpoint, a classe de retorno da consulta e o Handle que manipula os eventos enviados pelo MediatR.

- **Services**: As classes de acesso aos serviços externos usando o Refit para manipular as requisições htttp e o Polly para definir regras de circuit breaker.

- **Options**: A classe que representa as configurações da camada de serviços 

  

### Dominio 

A camada de domínio contém as classes de entidades, dtos e interfaces. 

Pastas: 
  

- **Interfaces**: As interfaces de cálculos de resgate e imposto de renda. 

- **Ir**: A classe concreta para cálculo de imposto de renda. 

- **Resgate**: A classe concreta para cálculo do valor de Resgate. 

- **Models**: Entidades da aplicação.

  

### Infra 

As consultas dos serviços da aplicação estão utilizando cache com período de expiração até o dia seguinte às 00:00. 

Foi utilizado o cache Redis do Azure (solução NoSQL e open source) 

Pastas: 

  

- **Cache**:  Contém as classes e interfaces genéricas para recuperar e salvar as chaves no cache do Redis do azure. 

  

### Teste 

  

Foi criado testes de unidade usando os frameworks **xUnit** com **FluenteAssertions** por serem os mais utilizados no mercado e deixar a leitura dos testes mais próximos do mundo real.  

Pastas: 

  

- **Domínio**:  Contém as classes de testes para os cálculos de Imposto de Renda e Valor total para resgate. 

 