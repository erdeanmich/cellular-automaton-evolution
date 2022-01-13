namespace CellularAutomatonEvolution
{
    public interface IFitnessEvaluator
    {
        int DetermineFitness(int[,] cellsToInvestigate);
    }
}