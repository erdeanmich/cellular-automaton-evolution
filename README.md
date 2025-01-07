# Cellular Automaton Evolution 
This repo contains the code and some related information for my "Wissenschaftliches Projekt". The goal is to develop a cellular automaton which produces dungeonlike structures for games with a PCG approach. The parameters of the automaton will be evolved by a search based genetic algorithm to reach more satisfying results. 
The project will be developed with Unity and C#. 

Below you can see the result of one run. The parameters of the automaton were produced by the genetic algorithm at hand. 
The fitness function prefers large, coherent floor areas which are ideally formed by a branched network of obstacles. Unreachable floor areas are disregarded. 

![](https://github.com/erdeanmich/cellular-automaton-evolution/blob/main/ca.gif)

## Requirements for the CA 
* Easy (moddable) cell representation
* Cell states: Floor (Must), Wall(Must), Startpoint(Must), Endpoint(Must) Items (Optional)
* At least size of 50x50 
* Export of whole automaton is possible 
* Easy and moddable rule representation 


## Usage 
The application consists of two interfaces. One for running the genetic algorithm and one for checking out the results of the genetic algorithm in a visualised automaton. 

### Cellular Automaton 
You can use the parameters r, n, T and M to play around with the parameters of the cellular automaton. For more info about the theory of CAs check out ![wikipedia](https://en.wikipedia.org/wiki/Cellular_automaton). 
You are able to import and export your generated results. You can use the seed to replicate results.

![](https://github.com/erdeanmich/cellular-automaton-evolution/blob/main/running-ca-app.png)

### Running the genetic algorithm
If you want to find out what "favorable" parameters are existing for the cellular automaton you can use the genetic algorithm. Feel free to play around with the fitness function to create new results.
![](https://github.com/erdeanmich/cellular-automaton-evolution/blob/main/ea-app.png) 
