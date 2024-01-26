using IPS_CALC.Models;

namespace IPS_CALC.Services.Interfaces
{
    public interface ICalculate<TResult>
    {
        /// <summary>
        /// Вернуть калькуляцию давления
        /// </summary>
        /// <param name="Conditions">Условия</param>
        /// <param name="RequiredPressure">Запрашиваемое давление</param>
        /// <param name="SelectedIps">Выбранная ипс</param>
        /// <returns></returns>
        TResult Calc(EnvironmentalСonditions Conditions, double RequiredPressure, IPS.DAL.IPS SelectedIps);
    }
}