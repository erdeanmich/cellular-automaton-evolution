using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CellularAutomaton;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace CellularAutomatonEvolution
{
    public class GeneticEvolutionSimulation
    {
        private CellularAutomatonEvolutionConfig cellularAutomatonEvolutionConfig;
        private CellularAutomatonSimulation cellularAutomatonSimulation;
        private Dictionary<CellularAutomatonSimulationConfig, int> populationWithFitness;
        private IFitnessEvaluator fitnessEvaluator;
        private string pathToExistingPopulation;
        private bool shouldRun = false;

        public GeneticEvolutionSimulation(CellularAutomatonEvolutionConfig cellularAutomatonEvolutionConfig,
            string pathToExistingPopulation = null)
        {
            this.cellularAutomatonEvolutionConfig = cellularAutomatonEvolutionConfig;
            this.pathToExistingPopulation = pathToExistingPopulation;
            cellularAutomatonSimulation = new CellularAutomatonSimulation();
            fitnessEvaluator = new CellularAutomatonFitnessCalculator();
            InitNewPopulation();
        }

        public void ExecuteEvolutionStep()
        {
            var newPopulation = new List<CellularAutomatonSimulationConfig>(populationWithFitness.Keys);
            var parents = ChooseParents();
            var offspring = CreateOffspringFromParents(parents);
            var mutatedOffspring = MutateOffspring(offspring);
            newPopulation.AddRange(mutatedOffspring);
            var newPopulationWithFitness = CalcFitnessForCurrentPopulation(newPopulation.ToArray());
            populationWithFitness = newPopulationWithFitness.OrderByDescending(x => x.Value)
                .Take(cellularAutomatonEvolutionConfig.PopulationSize)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public float GetAverageFitness()
        {
            float sum = populationWithFitness.Values.Sum();
            return sum / populationWithFitness.Count;
        }

        private List<CellularAutomatonSimulationConfig> MutateOffspring(List<CellularAutomatonSimulationConfig> offspring)
        {
            var rng = new Random();
            return offspring.ConvertAll(x => new CellularAutomatonSimulationConfig
            {
                Seed = x.Seed,
                GridSize = x.GridSize,
                M = rng.NextDouble() <= CellularAutomatonEvolutionConstants.MutationChance ? GetRandomM() : x.M,
                N = rng.NextDouble() <= CellularAutomatonEvolutionConstants.MutationChance ? GetRandomN() : x.N,
                T = rng.NextDouble() <= CellularAutomatonEvolutionConstants.MutationChance ? GetRandomT() : x.T,
                R = rng.NextDouble() <= CellularAutomatonEvolutionConstants.MutationChance ? GetRandomR() : x.R
            });
        }

        private List<CellularAutomatonSimulationConfig> CreateOffspringFromParents(List<CellularAutomatonSimulationConfig> parents)
        {
            List<CellularAutomatonSimulationConfig> offspring = new List<CellularAutomatonSimulationConfig>();
            
            foreach (var parent in parents)
            {
                CellularAutomatonSimulationConfig other;
                do
                {
                    var index = GetRandomIndividualIndex(parents);
                    other = parents[index];
                } while (other.Seed != parent.Seed);

                offspring.Add(RecombineParents(parent, other));
            }
            
            return offspring;
        }

        private CellularAutomatonSimulationConfig RecombineParents(CellularAutomatonSimulationConfig parent1, CellularAutomatonSimulationConfig parent2)
        {
            return new CellularAutomatonSimulationConfig
            {
                Seed = Time.time.ToString(CultureInfo.CurrentCulture).GetHashCode(),
                GridSize = cellularAutomatonEvolutionConfig.GridSize,
                M = GetRandomValueFromPair(parent1.M, parent2.M),
                R = GetRandomValueFromPair(parent1.R, parent2.R),
                N = GetRandomValueFromPair(parent1.N, parent2.N),
                T = GetRandomValueFromPair(parent1.T, parent2.T)
            };
        }

        private T GetRandomValueFromPair<T>(T first, T second)
        {
            var rng = new Random();
            var list = new List<T> { first, second };
            return list[rng.Next(0,list.Count)];
        }

        private List<CellularAutomatonSimulationConfig> ChooseParents()
        {
            List<CellularAutomatonSimulationConfig> parents = new List<CellularAutomatonSimulationConfig>();
            for (int i = 0; i < cellularAutomatonEvolutionConfig.PopulationSize / 2; i++)
            {
                parents.Add(ChooseParent());
            }

            return parents;
        }

        private CellularAutomatonSimulationConfig ChooseParent()
        {
            var tournamentList = new Dictionary<int, int>();
            do
            {
                var randomIndividual = GetRandomIndividualIndex(populationWithFitness);
                if (!tournamentList.ContainsKey(randomIndividual))
                {
                    tournamentList.Add(randomIndividual, populationWithFitness.ElementAt(randomIndividual).Value);
                }

            } while (tournamentList.Count < 3);

            var indexToParent = tournamentList.OrderByDescending(x => x.Value).First().Key;
            return populationWithFitness.ElementAt(indexToParent).Key;
        }

        private int GetRandomIndividualIndex(ICollection collection)
        {
            var rng = new Random();
            return rng.Next(collection.Count);
        }
        
        private void InitNewPopulation()
        {
            //TODO take path into account 
            CellularAutomatonSimulationConfig[] population = new CellularAutomatonSimulationConfig[cellularAutomatonEvolutionConfig.PopulationSize];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = CreateRandomCellularSimulationConfigWithinBounds();
            }
            populationWithFitness = CalcFitnessForCurrentPopulation(population);
        }

        private CellularAutomatonSimulationConfig CreateRandomCellularSimulationConfigWithinBounds()
        {
            var r = GetRandomR();
            var n = GetRandomN();
            var t = GetRandomT();
            var m = GetRandomM();
            var seed = GetRandomSeed();

            return new CellularAutomatonSimulationConfig
            {
                GridSize = cellularAutomatonEvolutionConfig.GridSize,
                M = m,
                N = n,
                T = t,
                R = r,
                Seed = seed
            };
        }

        private int GetRandomSeed()
        {
            return RandomUtils.RandomString().GetHashCode();
        }

        private int GetRandomT()
        {
            var rng = new Random();
            return rng.Next(cellularAutomatonEvolutionConfig.maxT + 1);
        }

        private int GetRandomN()
        {
            var rng = new Random();
            return rng.Next(cellularAutomatonEvolutionConfig.maxN + 1);
        }
        
        private int GetRandomM()
        {
            var rng = new Random();
            return rng.Next(cellularAutomatonEvolutionConfig.maxM + 1);
        }

        private double GetRandomR()
        {
            var rng = new Random();
            return rng.NextDouble() * cellularAutomatonEvolutionConfig.maxR;
        }

        private Dictionary<CellularAutomatonSimulationConfig,int> CalcFitnessForCurrentPopulation(CellularAutomatonSimulationConfig[] population)
        {
            Dictionary<CellularAutomatonSimulationConfig,int> fitnessForPopulation = new Dictionary<CellularAutomatonSimulationConfig, int>();
            foreach (CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig in population)
            {
                int fitness = CalcFitnessForCellularAutomaton(cellularAutomatonSimulationConfig);
                fitnessForPopulation.Add(cellularAutomatonSimulationConfig, fitness);
            }

            return fitnessForPopulation;
        }
        
        private int CalcFitnessForCellularAutomaton(CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig)
        {
            cellularAutomatonSimulation.SetConfig(cellularAutomatonSimulationConfig);
            cellularAutomatonSimulation.Simulate();
            int[,] cells = cellularAutomatonSimulation.GetCells();
            return fitnessEvaluator.DetermineFitness(cells);
        }
    }
}