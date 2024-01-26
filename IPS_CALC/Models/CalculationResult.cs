using System.Collections;
using System.Collections.Generic;

namespace IPS_CALC.Models
{
    public class CalculationResult
    {
        /// <summary>
        /// Расчетная масса
        /// </summary>
        public double EstimatedWeight { get; set; }
        /// <summary>
        /// Коллекция грузов
        /// </summary>
        public IEnumerable<IPS.DAL.Cargo> Cargoes { get; set; }
    }
}