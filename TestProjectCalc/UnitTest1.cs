using IPS_CALC.Models;
using IPS_CALC.Services.CalculatorIps;
using IPS_CALC.Services.Interfaces;
using System;
using Xunit;

namespace TestProjectCalc
{
    public class UnitTest1
    {
        [Fact]
        public void TestResultCalc()
        {
            ICalculate<CalculationResult> calculator = new CalculatorWeightGuage();

            var conditions = new Environmental—onditions
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
                AlfaCoefficient = (decimal)8e10 - 6,
                BettaCoefficient = (decimal)10.855e-13
            };
            var result = calculator.Calc(conditions, 60, ips);
            var exp = Math.Round(result.EstimatedWeight, 8);
            Assert.Equal(30.57178055, exp);

        }
    }
}
