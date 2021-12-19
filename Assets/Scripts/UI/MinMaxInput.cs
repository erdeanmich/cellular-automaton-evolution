using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MinMaxInput : MonoBehaviour
    {
        [SerializeField] 
        private InputField min;

        [SerializeField] 
        private InputField max;

        public Tuple<int, int> GetMinMaxAsInt()
        {
            var minValue = Convert.ToInt32(min.text);
            var maxValue = Convert.ToInt32(max.text);
            return new Tuple<int, int>(minValue, maxValue);
        }
        
        public Tuple<double, double> GetMinMaxAsDouble()
        {
            var minValue = Convert.ToDouble(min.text);
            var maxValue = Convert.ToDouble(max.text);
            return new Tuple<double, double>(minValue, maxValue);
        }
    }
}