using IPS_CALC.EnumsAndDictinary;
using IPS_CALC.Models;
using IPS_CALC.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IPS_CALC.Services.CalculatorIps
{
    public class CalculatorWeightGuage : ICalculate<CalculationResultCargo>
    {
        /// <summary>
        /// Требуемое давление
        /// </summary>
        private double _RequiredPressure;
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
        /// <summary>
        /// Привидение к температуре
        /// </summary>
        private double _ReducedSquareToTemperature => 
            _Square *
            (1 + _TemperatureCoefficientOfLinearExpansion * (_Temperature - 20));
        /// <summary>
        /// Расчет массы
        /// </summary>
        private double _EstimatedWeight => 
            ((_ReducedSquareToTemperature * _RequiredPressure * 100000 / 9.814507) *
                                  (1 + 1.2 / 8000) *
                                  (1 + _DeformationCoefficient * _RequiredPressure * 100000)) / 10000;


        public CalculationResultCargo Calc(
            EnvironmentalСonditions Conditions,
            double RequiredPressure, 
            IPS.DAL.IPS SelectedIps)
        {
            _Square = (double)SelectedIps.Square;
            _TemperatureCoefficientOfLinearExpansion = (double)SelectedIps.AlfaCoefficient * 1e-6;
            _Temperature = Conditions.Temperature;
            _DeformationCoefficient = (double)(SelectedIps.BettaCoefficient) * 1e-13;
            _RequiredPressure = RequiredPressure;


            var estimated_weight = _EstimatedWeight;

            var carg = SelectionCargo(SelectedIps, estimated_weight);

            return new CalculationResultCargo()
            {
                EstimatedWeight = estimated_weight,
                Cargoes = carg,
                CargoesWhereKettlebell = carg.Where(x => x.Type == (int)CargoType.Kettlebell),
                CargoesWhereNOKettlebell = carg.Where(x => x.Type != (int)CargoType.Kettlebell)
            };
        }




        private IEnumerable<IPS.DAL.Cargo> SelectionCargo(IPS.DAL.IPS Ips, double TargetWeight)
        {
            //все грузы
            var all_cargos = Ips.IPS2Cargoes.
                Select(x => x.Cargo).
                OrderBy(x => x.OrderNumerical).
                ToList();
            //колокол
            var kol = all_cargos.FirstOrDefault(x => x.Type == (int)CargoType.Bell);
            //Проверяем массу ИПС + Кол <> TargetWeight
            //Проверяем массу ИПС + ТП <> TargetWeight

            //Добавляем подходящий груз в коллекцию масс
            //Если условия не подходят выдаем массу ИПС (или ошибку лучше дать это декаратору)

            var cargoes = all_cargos.Where(x => x.Type != (int)CargoType.Bell &&
                            x.Type != (int)CargoType.PlateIsTransitional);

            var group = cargoes
                .GroupBy(x => x.NominalWeight)
                .OrderByDescending(x => x.Key).ToList();


            var allSum = cargoes.Sum(x => x.Weight);

            var selectedcargoes = new List<IPS.DAL.Cargo>
            {
                kol
            };

            foreach (var nominal in group)
            {

                foreach (var wieght in nominal)
                {
                    //Будующая суммарная масса
                    var newsumm_cargo = (double)
                        (wieght.Weight + Ips.Weight + selectedcargoes.Sum(x => x.Weight));

                    if (TargetWeight - newsumm_cargo > 0)
                    {
                        selectedcargoes.Add(wieght);
                    }
                    else break;
                }
            }
            //var sum = selectedcargoes.Sum(x => x.Weight) + Ips.Weight;

            return selectedcargoes;
        }

    }
}