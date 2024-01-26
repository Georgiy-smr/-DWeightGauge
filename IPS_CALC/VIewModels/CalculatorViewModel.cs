using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using IPS.Interfaces;
using Microsoft.EntityFrameworkCore;
using IPS_CALC.Models;
using IPS_CALC.Services.Interfaces;

namespace IPS_CALC.VIewModels
{
    public class CalculatorViewModel : ViewModel
    {
        private readonly ICalculate<CalculationResult> _Calculator;
        private readonly IRepository<IPS.DAL.IPS> _RepositoryIps;
        public CalculatorViewModel(
            IRepository<IPS.DAL.IPS> RepositoryIPS,
            ICalculate<CalculationResult> calculator)
        {
            _Calculator = calculator;
            _RepositoryIps = RepositoryIPS;
        }
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        private ObservableCollection<IPS.DAL.IPS> _CollectionIPS;
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        public ObservableCollection<IPS.DAL.IPS> CollectionIPS
        {
            get => _CollectionIPS;
            set => Set(ref _CollectionIPS, value);
        }
        /// <summary>
        /// Выбранная ИПС
        /// </summary>
        private IPS.DAL.IPS _IpsSelected;
        /// <summary>
        /// Выбранная ИПС
        /// </summary>
        public IPS.DAL.IPS IpsSelected
        {
            get => _IpsSelected;
            set
            {
                if (Set(ref _IpsSelected, value))
                    TargetPressure = 0;
            }
        }
        /// <summary>
        /// Температура
        /// </summary>
        private double _Temperature = 20;
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature
        {
            get => _Temperature;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Humidity, newvalue))
                    return;

                OnPropertyChanged(nameof(Result));
            }
        }
        /// <summary>
        /// Влажность
        /// </summary>
        private double _Humidity = 45;
        /// <summary>
        /// Влажность
        /// </summary>
        public double Humidity 
        {
            get => _Humidity;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Humidity, newvalue))
                    return;

                OnPropertyChanged(nameof(Result));
            }
        }
        /// <summary>
        /// Барометрическое давление
        /// </summary>
        private double _Baro = 99.96;
        /// <summary>
        /// Барометрическое давление
        /// </summary>
        public double Baro
        {
            get => _Baro;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Baro, newvalue))
                    return;

                OnPropertyChanged(nameof(Result));
            }
        }

        /// <summary>
        /// Необходимое давление
        /// </summary>
        private double _TargetPressure;
        /// <summary>
        /// Необходимое давление
        /// </summary>
        public double TargetPressure
        {
            get => _TargetPressure;
            set 
            {
                if (!
                    double.TryParse(value.ToString(), out double newvalue))
                    return;
                if(!
                    Set(ref _TargetPressure, newvalue))
                    return;

                OnPropertyChanged(nameof(Result));
            }
        }
        /// <summary>
        /// Результат расчета
        /// </summary>
        public CalculationResult Result
        {
            get
            {
                if(IpsSelected is null)
                    return null;

                return _Calculator.Calc(new EnvironmentalСonditions
                {
                    Temperature = this.Temperature,
                    Humidity = this.Humidity,
                    Baro = this.Baro
                }, _TargetPressure, IpsSelected);
            }
        }

        #region Команда запрашивающая коллекцию ИПС и Грузов

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? 
            new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(object p) => true;

        private async Task OnLoadIPSCommandExecuted(object p)
        {
            CollectionIPS = new ObservableCollection<IPS.DAL.IPS>(await _RepositoryIps.Items.ToArrayAsync());
        }

        #endregion
        
    }
}