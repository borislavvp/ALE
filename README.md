# ALE - Automata & Logic Engineering
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/borislavvp/ALE/.NET) [![Codacy Badge](https://app.codacy.com/project/badge/Coverage/0934dea52b734f63b4679540b426fc84)](https://www.codacy.com/gh/borislavvp/ALE/dashboard?utm_source=github.com&utm_medium=referral&utm_content=borislavvp/ALE&utm_campaign=Badge_Coverage) 

This repository includes solution for two projects - ALE 1 and ALE 2. Below is given short information regarding both of them.

## ALE 1
**The project is about an application which can parse and evaluate expressions in prefixn notation. Building and visualizing expression trees, truth tables and simplifying them following the Quine McCluskey algorithm.**
![image](https://user-images.githubusercontent.com/46525030/113195058-8033d000-926a-11eb-8cb6-47d41574f1a7.png)

The following logical operators are supported:
* Disjunction
* Conjunction
* Negation
* Nand
* Implication
* Biimplication

## ALE 2
**The project is about an application which can evaluate instructions in predefined format and regex expressions utilizing tompson processing algorithm and others. Building and visualizing multi directed finite automata graphs and verifying whether a word can be formed or whether the automata is finite with giving all possible words. Furthermore, automatas can be converted into DFAs if they are not already.**
![image](https://user-images.githubusercontent.com/46525030/123305276-9a5b1280-d528-11eb-8df2-518a7a6a9d1b.png)

The following instructions are needed in order a valid automata to be build:
* alphabet
* states 
* final 
* stack - Optional - Only for push down automatas
* transitions

The following test cases are supported:
* words
* dfa
* finite

## Solution

The repository contains 4 projects, each with different purpose. 

The **client** folder is responsible for the front-end application made with **Vue.js** and **Typescript**.

The rest of the projects are done in **ASP.NET Core**.

The **server** folder contains the Presentation layer of the backend structure, where the endpoints for the expression service are specified.

The **logic** folder contains all of the logic needed for evaluating an expression.

The **tests** folder contains unit tests regarding the logic of the expression service.
