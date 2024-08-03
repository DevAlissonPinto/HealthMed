# FIAP Pós Tech - Hackathon - Fase Final - Visão Geral Projeto - Sistema de Agendamento

O Sistema de Agendamento (Health&Med) é uma plataforma que permite que o paciente realize agendamentos para um determinado profissional da saúde.

## Contribute

[Alisson da Silva Pinto](https://github.com/DevAlissonPinto)
[Diego Ivan Mendes de Oliveira](https://github.com/diegoivanmendes)
[Vinicius Roberto de Oliveira Santos](https://github.com/vroliveira)


## Repositories
* ASP.NET CORE BLAZOR  [link](https://dev.azure.com/diegoivanmendesfiap/_git/Hackathon)
* Teste de Interações de Lógicas de negócio, camadas de aplicação e banco de dados [link](https://dev.azure.com/diegoivanmendesfiap/_git/Hackathon/HealthMed.Tests)

## Development 

Para executar esse projeto você precisa seguir as etapas abaixo:

* Abrir a solution HealthMed.sln com o visual studio 2022
* Configurar o projeto HealthMed.Web para serem executado como startup projects
* Abri Package Manager Console e deixar como Defult project: '4 - Infra\HealthMed.Infra.Repository' e executar o comando Update-Database
* Executar o projeto pressionando o F5

### HealthMed.Tests

Este projeto de teste é construído usando xUnit.NET para testar o aplicativo ASP.NET Core HealthMed, Ele contém testes automatizados para garantir o funcionamento correto do aplicativo.

## Configuração

Certifique-se de ter .NET Core 8 SDK instalado e VS 2022 community.

### Como Executar os Testes

1. Abra o terminal.
2. Navegue até o diretório do projeto de teste (HealthMed.Tests).
3. Execute o seguinte comando: dotnet test

## Entregáveis Mínimos
1. Desenvolvimento de um MVP da solução, contemplando os requisitos
2. funcionais e não funcionais listados acima.
3.  Pipeline CI/CD
3.1 Demonstração do pipeline de deploy da aplicação.
4.  Testes unitários
4.1 Implantação de testes unitários que garantam o funcionamento da solução
5. Não há a necessidade do desenvolvimento do Frontend da Solução.
6. Formato da entregável: Vídeo gravado que demonstre o funcionamento do sistema cumprindo os requisitos solicitados e Documentação escrita (README ou Arquivo)
7. A duração máxima do vídeo deverá ser de, no máximo, 10 minutos. Vídeos mais longos não serão corrigidos.