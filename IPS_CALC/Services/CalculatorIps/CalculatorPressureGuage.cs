using IPS_CALC.Models;
using IPS_CALC.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Services.CalculatorIps
{
    public class CalculatorPressureGuage : ICalculate<CalculationResultPressure>
    {
        private EnvironmentalСonditions _Conditions;
        private IPS.DAL.IPS _SelectedIps;
        /// <summary>
        /// Расчет необходимого давления
        /// </summary>
        private double _Pressure
        {
            get
            {
                var ch1 = 4 * _Weight * 9.814507 * _Betacoef / _ReducedSquareToTemperature;
                var ch2 = 1 / (1 + (1.2 / 8000));
                var ch3 = 2 * _Betacoef;
                return (Math.Sqrt(1 + ch1 * ch2) - 1) / ch3;

            }
        }
        private double _ReducedSquareToTemperature => ((_Square / 10000) * (1 + _Alfacoef * (_Temp - 20)));


        private double _Temp => _Conditions.Temperature < 18 || _Conditions.Temperature > 23 ? 
            throw new ArgumentOutOfRangeException(nameof(_Conditions.Temperature), _Conditions.Temperature.ToString())
            : _Conditions.Temperature;
        private double _Hum => _Conditions.Humidity < 10 || _Conditions.Humidity > 90 ?
            throw new ArgumentOutOfRangeException(nameof(_Conditions.Humidity), _Conditions.Humidity.ToString())
            : _Conditions.Humidity;
        private double _Baro => _Conditions.Baro < 80 || _Conditions.Baro > 120 ?
            throw new ArgumentOutOfRangeException(nameof(_Conditions.Baro), _Conditions.Baro.ToString()) 
            : _Conditions.Baro;
        private double _Square => _SelectedIps.Square <= 0 ? 
            throw new ArgumentOutOfRangeException(nameof(_SelectedIps.Square), _SelectedIps.Square.ToString())
            : (double)_SelectedIps.Square;
        private double _Weight;
        private double _Alfacoef => _SelectedIps.AlfaCoefficient <= 0 ?
                  throw new ArgumentOutOfRangeException(nameof(_SelectedIps.AlfaCoefficient), _SelectedIps.AlfaCoefficient.ToString())
            : (double)_SelectedIps.AlfaCoefficient * 1e-6;
        private double _Betacoef => _SelectedIps.BettaCoefficient <= 0 ?
          throw new ArgumentOutOfRangeException(nameof(_SelectedIps.BettaCoefficient), _SelectedIps.BettaCoefficient.ToString())
            : (double)_SelectedIps.BettaCoefficient * 1e-13;


        public CalculationResultPressure Calc(EnvironmentalСonditions Conditions, double Required, IPS.DAL.IPS SelectedIps)
        {
            _Conditions = Conditions;
            _SelectedIps = SelectedIps;
            _Weight = Required <= 0 ?
                throw new ArgumentOutOfRangeException(nameof(Required), Required.ToString())
                : Required;

            var new_press = _Pressure;

            return new CalculationResultPressure { ActualPressure = new_press / 100000 };
        }
    }
}