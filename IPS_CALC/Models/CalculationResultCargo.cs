using System.Collections;
using System.Collections.Generic;

namespace IPS_CALC.Models
{
    public class CalculationResultCargo
    {
        /// <summary>
        /// Расчетная масса
        /// </summary>
        public double EstimatedWeight { get; set; }
        /// <summary>
        /// Коллекция всех грузов
        /// </summary>
        public IEnumerable<IPS.DAL.Cargo> Cargoes { get; set; }

        /// <summary>
        /// Коллекция гирь
        /// </summary>
        public IEnumerable<IPS.DAL.Cargo> CargoesWhereKettlebell { get; set; }

        /// <summary>
        /// Коллекция без гирь
        /// </summary>
        public IEnumerable<IPS.DAL.Cargo> CargoesWhereNOKettlebell { get; set; }

    }
}