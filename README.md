# BancoNix
Este projeto faz parte de uma Avaliação.

Pré-requisitos (Windows):
- SDK .NET Core 3.1
- Visual Studio 2019 ou superior
- node
- VSCode

Este projeto funciona em Windows, macOS e Linux.

Tecnologias utilizadas:
- Swagger
- .NET Core 3.1
- Entity Framework Core
- XUnit
- AngularJS
- Webpack
- Sass

Padrões utilizados:
- Padrões Rest
- N-layers
- DDD

## Back-end
A api está configurada para executar nas portas http 5000 e https 5001.
Para abrir a documentação do swagger, rodar o projeto e abrir a URL: https://localhost:5001/index.html
A persistencia de dados está configurada para utilizar um provedor _InMemory_.

## Front-end
Para executar o site em modo de desenvolvimento é necessário os seguintes passos:
- Executar a api do back-end
- Navegar até a pasta raiz do projeto da camada de apresentação https://github.com/liserebollo/BancoNix/tree/master/BancoNix.Apresentacao/banco-nix
- Instalar as dependencias necessarias do projeto (npm install)
- Executar o projeto (npm run start)
