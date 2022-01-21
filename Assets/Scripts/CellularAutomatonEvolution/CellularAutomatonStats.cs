using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace CellularAutomatonEvolution
{
    public class CellularAutomatonStats : MonoBehaviour
    {
        [SerializeField]
        private Text currentIterationCount;

        [SerializeField]
        private Text averageFitness;

        [SerializeField]
        private Text runningFor;

        private Stopwatch stopwatch = new Stopwatch();

        private void Update()
        {
            if (stopwatch.IsRunning)
            {
                runningFor.text = stopwatch.Elapsed.ToString("h'h 'm'm 's's'");
            }
        }

        public void UpdateStats(int iteration, double fitness)
        {
            currentIterationCount.text = iteration.ToString();
            averageFitness.text = fitness.ToString(CultureInfo.CurrentCulture);
        }

        public void StartEvolution()
        {
            stopwatch.Start();
            currentIterationCount.text = "0";
            averageFitness.text = "0";
        }

        public void StopEvolution()
        {
            stopwatch.Reset();
        }
    }
}