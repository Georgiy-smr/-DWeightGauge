using IPS_CALC.Models;

namespace IPS_CALC.Services.Interfaces
{
    public interface ICalculate<TResult>
    {
        /// <summary>
        /// Вернуть калькуляцию давления
        /// </summary>
        /// <param name="Conditions">Условия</param>
        /// <param name="Required">Запрашиваемая величина(давление, масса)</param>
        /// <param name="SelectedIps">Выбранная ипс</param>
        /// <returns></returns>
        TResult Calc(EnvironmentalСonditions Conditions, double Required, IPS.DAL.IPS SelectedIps);
    }
}