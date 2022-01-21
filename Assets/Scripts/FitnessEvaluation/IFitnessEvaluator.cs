namespace CellularAutomatonEvolution
{
    public interface IFitnessEvaluator
    {
        double DetermineFitness(int[,] cellsToInvestigate);
    }
}