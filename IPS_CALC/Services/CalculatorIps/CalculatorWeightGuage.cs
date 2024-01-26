using IPS_CALC.Models;
using IPS_CALC.Services.Interfaces;

namespace IPS_CALC.Services.CalculatorIps
{
    public class CalculatorWeightGuage : ICalculate<CalculationResult>
    {
        /// <summary>
        /// Площадь Ипс
        /// </summary>
        private double _Square;
        /// <summary>
        /// Температурный коэффициент линейного расширения
        /// </summary>
        private double _TemperatureCoefficientOfLinearExpansion;
        /// <summary>
        /// Коэффициент деформации
        /// </summary>
        private double _DeformationCoefficient;
        /// <summary>
        /// Температура
        /// </summary>
        private double _Temperature;
        /// <summary>
        /// Влажность
        /// </summary>
        private double _Humidity;
        /// <summary>
        /// Атмосферное давление
        /// </summary>
        private double _Baro;

        private double _ReducedSquareToTemperature => 
            _Square *
            (1 + _TemperatureCoefficientOfLinearExpansion * (_Temperature - 20));
        
        public CalculationResult Calc(
            EnvironmentalСonditions Conditions,
            double RequiredPressure, 
            IPS.DAL.IPS SelectedIps)
        {
            _Square = (double)SelectedIps.Square;
            _TemperatureCoefficientOfLinearExpansion = (double)SelectedIps.AlfaCoefficient;
            _Temperature = Conditions.Temperature;
            _DeformationCoefficient = (double)SelectedIps.BettaCoefficient;
            
            return new CalculationResult()
            {
                EstimatedWeight = (_ReducedSquareToTemperature * RequiredPressure / 9.814507) *
                                  (1 + 1.2 / 7800) * (1 + _DeformationCoefficient * RequiredPressure) 
            };
        }
    }
}