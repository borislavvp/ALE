# ALE 1 Fontys 
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/borislavvp/ALE/.NET) [![Codacy Badge](https://app.codacy.com/project/badge/Coverage/0934dea52b734f63b4679540b426fc84)](https://www.codacy.com/gh/borislavvp/ALE/dashboard?utm_source=github.com&utm_medium=referral&utm_content=borislavvp/ALE&utm_campaign=Badge_Coverage) 

## About
**The project is about an application which can parse and evaluate expressions in prefixn notation. Building and visualizing expression trees, truth tables and simplifying them following the Quine McCluskey algorithm.**

The following logical operators are supported:
* Disjunction
* Conjunction
* Negation
* Nand
* Implication
* Biimplication

The repository contains 4 projects, each with different purpose. 

The **client** folder is responsible for the front-end application made with **Vue.js** and **Typescript**.

The rest of the projects are done in **ASP.NET Core**.

The **server** folder contains the Presentation layer of the backend structure, where the endpoints for the expression service are specified.

The **logic** folder contains all of the logic needed for evaluating an expression.

The **tests** folder contains unit tests regarding the logic of the expression service.

## Development setup

If you have docker installed on your computer, you can easily get the whole project going by typing the following command into the terminal. 
```
docker-compose up
```
Make sure you ```cd``` in the folder of the solution.
The URL of the **client** application is http://localhost:8081.

If you want to run the projects separately and debug them:

Run the following commands into the **client** folder

```sh
npm install
npm run serve
```
Then run the following commands into the **server** folder, if you have **dotnet cli** installed:

```sh
dotnet run --project server
```
Otherwise open the solution with Visual Studio, choose the server project as a startup project and run it with IIS Express.
The server is configured to run on http://localhost:44390.

If you with to run the tests you can either open the Test Explorer in Visual Studio and do it from there or if you have the dotnet cli installed, you can type in the console 
``` 
dotnet test
```
