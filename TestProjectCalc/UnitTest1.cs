using IPS_CALC.Models;
using IPS_CALC.Services.CalculatorIps;
using IPS_CALC.Services.Interfaces;
using System;
using Xunit;

namespace TestProjectCalc
{
    public class UnitTest1
    {
        /// <summary>
        /// Проверка расчета массы по требуемому давлению
        /// </summary>
        [Fact]
        public void TestEstimatedWeight()
        {
            ICalculate<CalculationResultCargo> calculator = new CalculatorWeightGuage();

            var conditions = new EnvironmentalСonditions
            {
                Temperature = 20,
                Humidity = 50,
                Baro = 100
            };
            var ips = new IPS.DAL.IPS
            {
                Square = (decimal)0.5,
                LowLimit = (decimal)0,
                MaxLimit = (decimal)60,
                AlfaCoefficient = (decimal)8,
                BettaCoefficient = (decimal)10.855
            };
            var result = calculator.Calc(conditions, 60, ips);
            var exp = Math.Round(result.EstimatedWeight, 8);
            Assert.Equal(30.57178055, exp);
        }


        /// <summary>
        /// Проверка расчета давления по требуемой массе
        /// </summary>
        [Fact]
        public void TestCalcPressure()
        {
            ICalculate<CalculationResultPressure> calculator = new CalculatorPressureGuage();

            var conditions = new EnvironmentalСonditions
            {
                Temperature = 20,
                Humidity = 50,
                Baro = 100
            };
            var ips = new IPS.DAL.IPS
            {
                Square = (decimal)0.5,
                LowLimit = (decimal)0,
                MaxLimit = (decimal)60,
                AlfaCoefficient = (decimal)8,
                BettaCoefficient = (decimal)10.855
            };
            var result = calculator.Calc(conditions, 30, ips);
            var exp = Math.Round(result.ActualPressure, 5);
            Assert.Equal(58.87783, exp);
        }

    }
}
